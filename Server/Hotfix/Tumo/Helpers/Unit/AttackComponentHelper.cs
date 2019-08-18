using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class AttackComponentHelper
    {
        #region 行为树模式
        /// <summary>
        /// 检查有无进入战斗状态
        /// </summary>
        /// <param name="unit"></param>
        public static bool CheckIsBattling(this AttackComponent self)
        {
            if (self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastDistance < self.battlingDis)
            {
                self.isBattling = true;

                return false;
            }
            else
            {
                self.isBattling = false;

                return true;
            }
        }

        /// <summary>
        /// 单目标 得到敌人
        /// </summary>
        /// <param name="unit"></param>
        public static bool CheckAttackTarget(this AttackComponent self)
        {
            Unit unit = self.GetParent<Unit>();

            if (unit.GetComponent<RayUnitComponent>() != null)
            {
                if (unit.GetComponent<RayUnitComponent>().target != null)
                {
                    self.target = unit.GetComponent<RayUnitComponent>().target;

                    return true;
                }
                else
                {
                    ///正前方，5 米内，最近小怪

                }
            }
            else
            {
                if (unit.GetComponent<SeekComponent>() != null && unit.GetComponent<SeekComponent>().target != null)
                {
                    self.targetDistance = SqrDistanceComponentHelper.Distance(unit.Position, unit.GetComponent<SeekComponent>().target.Position);
                    if (self.targetDistance < self.attackDis)
                    {
                        self.target = unit.GetComponent<SeekComponent>().target;

                        return true;
                    }
                }
            }

            self.target = null;

            return false;
        }

        /// <summary>
        /// 攻击 CD 计时
        /// </summary>
        /// <param name="self"></param>
        public static void AttackTarget(this AttackComponent self)
        {
            if (self.isDeath) return;

            if (self.target != null)
            {
                if (!self.startNull)
                {
                    self.startTime = TimeHelper.ClientNowSeconds();
                    self.startNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();
                if ((timeNow - self.startTime) > self.attcdTime)
                {
                    if (self.isDeath)
                    {
                        self.target = null;
                        return;
                    }
                    if (self.target.GetComponent<AttackComponent>().isDeath)
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
                    numSelf[NumericType.ValuationAdd] -= (domhp * 2);
                }
                else
                {
                    numSelf[NumericType.ValuationAdd] -= domhp;
                }

                //if (self.CheckDeath())
                //{
                //    //判断死亡结算
                //    self.RemoveUnit();

                //    self.GetExpAndCoin();

                //    break;
                //}

                Console.WriteLine(" TakeDamage-143-Myself(" + myself.UnitType + ") ： " + "-" + domhp + " / " + numSelf[NumericType.Valuation] + " /Count: " + self.TakeDamages.Count);
            }
        }

        /// <summary>
        /// 检查是否死亡
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool CheckDeath(this AttackComponent self)
        {
            NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();

            if (numC[NumericType.Valuation] > 0)
            {
                self.isDeath = false;

                return false;
            }

            self.isDeath = true;

            return true;
        }

        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        public static void RemoveUnit(this AttackComponent self)
        {
            Console.WriteLine(" AttackComponentHelper-172- type: " + self.GetParent<Unit>().UnitType + " 删除单元 Unit。");

            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.GetParent<Unit>().Id);
            actorLocationSender.Send(new Death_Map());
        }

        /// <summary>
        /// 发放工资 经验和金币
        /// </summary>
        /// <param name="self"></param>
        public static void GetExpAndCoin(this AttackComponent self)
        {
            if (self.isSettlement) return;

            Unit selfunit = self.GetParent<Unit>();
            NumericComponent numC = selfunit.GetComponent<NumericComponent>();

            if (self != null)
            {
                int addexp = numC[NumericType.Level] * numC[NumericType.Level] + 1;
                int addcoin = numC[NumericType.Level] + 1;
                NumericComponent numeric = null;
                ///我的类型，我敌人的类型是什么呢
                switch (selfunit.UnitType)
                {
                    case UnitType.Player:
                        if (self.attackers.Count > 0)
                        {
                            foreach (long tem in self.attackers.ToArray())
                            {
                                numeric = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<NumericComponent>();
                                numeric[NumericType.ExpAdd] += addexp;
                                numeric[NumericType.CoinAdd] += addcoin;
                            }
                            self.attackers.Clear();
                        }
                        Console.WriteLine(" DeathSettlement-205-type(得到经验和金币): " + numeric.GetParent<Unit>().UnitType + " addexp/exp: " + addexp + "/" + numeric[NumericType.Exp] + "  addcoin/coin: " + addcoin + "/" + numeric[NumericType.Coin]);
                        break;
                    case UnitType.Monster:
                        if (self.attackers.Count > 0)
                        {
                            foreach (long tem in self.attackers.ToArray())
                            {
                                numeric = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<NumericComponent>();
                                numeric[NumericType.ExpAdd] += addexp;
                                numeric[NumericType.CoinAdd] += addcoin;
                            }
                            self.attackers.Clear();
                        }
                        Console.WriteLine(" DeathSettlement-218-type(得到经验和金币): " + numeric.GetParent<Unit>().UnitType + " addexp/exp: " + addexp + "/" + numeric[NumericType.Exp] + "  addcoin/coin: " + addcoin + "/" + numeric[NumericType.Coin]);
                        break;
                }
            }
            self.isSettlement = true;
        }


        #endregion

        /// <summary>
        /// 攻击 CD 计时
        /// </summary>
        /// <param name="self"></param>
        //public static void TakeAttack(this AttackComponent self)
        //{
        //    if (self.isDeath) return;

        //    //GetAttackTarget(self);

        //    if (self.target != null)
        //    {
        //        self.attackDistance = SqrDistanceComponentHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);
        //        if (self.attackDistance < self.cdDistance)
        //        {
        //            if (!self.startNull)
        //            {
        //                self.startTime = TimeHelper.ClientNowSeconds();
        //                self.startNull = true;
        //            }

        //            long timeNow = TimeHelper.ClientNowSeconds();
        //            if ((timeNow - self.startTime) > self.attcdTime)
        //            {
        //                if (self.isDeath)
        //                {
        //                    self.target = null;
        //                    return;
        //                }
        //                if (self.target.GetComponent<AttackComponent>().isDeath)
        //                {
        //                    self.target = null;
        //                    return;
        //                }
        //                //普通攻击，相当于施放技能41101，技能等级为0
        //                SkillItem skillItem = ComponentFactory.CreateWithId<SkillItem>(41101);
        //                skillItem.GetComponent<ChangeType>().CastId = self.GetParent<Unit>().Id;
        //                //skillItem.GetComponent<ChangeType>().TargetIds.Add(self.target.Id);
        //                skillItem.GetComponent<NumericComponent>().Set(NumericType.CaseBase, 14);

        //                self.target.GetComponent<AttackComponent>().TakeDamage(skillItem);
        //                self.startNull = false;
        //            }
        //        }
        //        else
        //        {
        //            if (self.startNull)
        //            {
        //                self.startTime = 0;
        //                self.startNull = false;
        //            }
        //        }
        //    }           
        //}
   

        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        //public static void DeathSettlement(this AttackComponent self)
        //{
        //    Console.WriteLine(" AttackComponentHelper-153- type: " + self.GetParent<Unit>().UnitType + " 判断 结算。" + self.isSettlement);

        //    NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();

        //    if (numC[NumericType.Valuation] <= 0)
        //    {
        //        ///Hp小于0时，标记死亡状态
        //        self.isDeath = true;

        //        ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.GetParent<Unit>().Id);
        //        actorLocationSender.Send(new Death_Map());

        //        if (self.isSettlement) return;

        //        self.GetExpAndCoin();

        //        self.isSettlement = true;

        //        Console.WriteLine(" AttackComponentHelper-171- type: " + self.GetParent<Unit>().UnitType + " 完成 结算。" + self.isSettlement);
        //    }
        //}

    }
}
