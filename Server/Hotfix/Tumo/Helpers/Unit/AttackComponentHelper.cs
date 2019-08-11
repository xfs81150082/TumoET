using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class AttackComponentHelper
    {
        /// <summary>
        /// 攻击 CD 计时
        /// </summary>
        /// <param name="self"></param>
        public static void TakeAttack(this AttackComponent self)
        {
            if (self.GetParent<Unit>().GetComponent<RecoverComponent>().isDeath) return;

            GetAttackTarget(self);

            if (self.target != null)
            {
                self.attackDistance = SqrDistanceComponentHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);
                if (self.attackDistance < self.cdDistance)
                {
                    if (!self.startNull)
                    {
                        self.startTime = TimeHelper.ClientNowSeconds();
                        self.startNull = true;
                    }

                    long timeNow = TimeHelper.ClientNowSeconds();
                    if ((timeNow - self.startTime) > self.attcdTime)
                    {
                        if (self.GetParent<Unit>().GetComponent<RecoverComponent>().isDeath)
                        {
                            self.target = null;
                            return;
                        }
                        if (self.target.GetComponent<RecoverComponent>().isDeath)
                        {
                            self.target = null;
                            return;
                        }
                        //普通攻击，相当于施放技能41101，技能等级为0
                        SkillItem skillItem = ComponentFactory.CreateWithId<SkillItem>(41101);
                        skillItem.GetComponent<ChangeType>().CastId = self.GetParent<Unit>().Id;
                        //skillItem.GetComponent<ChangeType>().TargetIds.Add(self.target.Id);
                        skillItem.GetComponent<NumericComponent>().Set(NumericType.CaseBase, 14);

                        self.target.GetComponent<AttackComponent>().TakeDamage(skillItem);
                        self.startNull = false;
                    }
                }
                else
                {
                    if (self.startNull)
                    {
                        self.startTime = 0;
                        self.startNull = false;
                    }
                }
            }           
        }
   
        /// <summary>
        /// 单目标 得到敌人
        /// </summary>
        /// <param name="unit"></param>
        static void GetAttackTarget(this AttackComponent self)
        {
            Unit unit = self.GetParent<Unit>();

            if (unit.GetComponent<RayUnitComponent>() != null)
            {
                if (unit.GetComponent<RayUnitComponent>().target != null)
                {
                    unit.GetComponent<AttackComponent>().target = unit.GetComponent<RayUnitComponent>().target;
                }
                else
                {
                    ///正前方，5 米内，最近小怪
                }
            }
            else
            {
                if (unit.GetComponent<SeeComponent>() != null && unit.GetComponent<SeeComponent>().target != null)
                {
                    unit.GetComponent<AttackComponent>().target = unit.GetComponent<SeeComponent>().target;
                }
            }
        }
  
        /// <summary>
        /// 单目标 攻击技能 加入减伤列队
        /// </summary>
        /// <param name="self"></param>
        /// <param name="skillItem"></param>
        public static void TakeDamage(this AttackComponent self, SkillItem skillItem)
        {
            self.TakeDamages.Enqueue(skillItem);
            self.WhileTakeDamage();
        }

        /// <summary>
        /// 单目标 减伤列队 取出 循环执行
        /// </summary>
        /// <param name="self"></param>
        public static void WhileTakeDamage(this AttackComponent self)
        {
            while (self.TakeDamages.Count > 0)
            {
                SkillItem skillItem = self.TakeDamages.Dequeue();
                Unit myself = self.GetParent<Unit>();
                if (!self.attackers.Contains(skillItem.GetComponent<ChangeType>().CastId))
                {
                    self.attackers.Add(skillItem.GetComponent<ChangeType>().CastId);
                }

                NumericComponent numSk = skillItem.GetComponent<NumericComponent>();
                skillItem.Dispose();
                NumericComponent numSelf = myself.GetComponent<NumericComponent>();
                Random random = new Random();
                int dom = random.Next(0, 99);
                int domhp = numSk[NumericType.Case];
                if (dom < 26)
                {
                    numSelf[NumericType.ValuationAdd] -= domhp * 2;
                }
                else
                {
                    numSelf[NumericType.ValuationAdd] -= domhp;
                }

                Console.WriteLine(" TakeDamage-138-Myself(" + myself.UnitType + ") ： " + "-" + domhp + " / " + numSelf[NumericType.Valuation] + " /Count: " + self.TakeDamages.Count);
            }
        }


    }
}
