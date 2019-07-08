using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AttackComponentUpdateSystem : UpdateSystem<AttackComponent>
    {
        public override void Update(AttackComponent self)
        {
            GetAttackTarget(self.GetParent<Unit>());
            AttackCdNowTime(self);
            DeathCdNowTime(self);
        }

        /// <summary>
        /// 得到 单目标 敌人
        /// </summary>
        /// <param name="unit"></param>
        void GetAttackTarget(Unit unit)
        {
            if (unit.GetComponent<RayUnitComponent>() != null)
            {
                if (unit.GetComponent<RayUnitComponent>().target != null)
                {
                    unit.GetComponent<AttackComponent>().target = unit.GetComponent<RayUnitComponent>().target;
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
        /// 攻击 CD 计时
        /// </summary>
        /// <param name="self"></param>
        void AttackCdNowTime(AttackComponent self)
        {
            if (self.target != null)
            {
                self.attackDistance = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);

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
                        TakeDamage(self.GetParent<Unit>(), self.target);
                        self.startNull = false;
                    }

                }
                else
                {
                    if (self.startNull)
                    {
                        self.startNull = false;
                    }
                }
            }
        }

        /// <summary>
        /// 单目标 普通攻击 减伤 TakeDamage
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target"></param>
        void TakeDamage(Unit self, Unit target)
        {
            AttackSkillHelper.DeathSettlement(target);

            AttackComponent attack = target.GetComponent<AttackComponent>();
            if (!attack.Attackers.Keys.Contains(self.Id))
            {
                attack.Attackers.Add(self.Id, self);
            }

            NumericComponent numTarget = target.GetComponent<NumericComponent>();
            NumericComponent numSelf = self.GetComponent<NumericComponent>();
            Random random = new Random();
            int dom = random.Next(0, 99);
            if (dom < 26)
            {
                numTarget[NumericType.HpAdd] -= numSelf[NumericType.Attack] * 2;
            }
            else
            {
                numTarget[NumericType.HpAdd] -= numSelf[NumericType.Attack];
            }

            AttackSkillHelper.DeathSettlement(target);
        }

        /// <summary>
        /// 小怪回到出生地，重生
        /// </summary>
        /// <param name="self"></param>
        void DeathCdNowTime(AttackComponent self)
        {
            if (!self.isDeath) return;

            if (!self.deathNull)
            {
                self.deathTime = TimeHelper.ClientNowSeconds();
                self.deathNull = true;
            }

            long timeNow = TimeHelper.ClientNowSeconds();

            if ((timeNow - self.deathTime) > self.lifeCdTime)
            {
                if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
                {
                    float dis = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.GetParent<Unit>().GetComponent<PatrolComponent>().spawnPosition);

                    if (dis < 0.01f)
                    {
                        self.isDeath = false;
                        self.deathNull = false;
                    }
                }
                else
                {
                    self.isDeath = false;
                    self.deathNull = false;
                }
            }
        }


    }
}