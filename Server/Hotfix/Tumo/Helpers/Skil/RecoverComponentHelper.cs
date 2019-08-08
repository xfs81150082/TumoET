using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class RecoverComponentHelper
    {
        /// <summary>
        ///  更新血量和法量, 更新断定死亡结算
        /// </summary>
        /// <param name="self"></param>
        public static void UpdateProperty(this RecoverComponent self)
        {
            self.DeathSettlement();

            self.RecoverHp();

            self.RecoverMp();
        }

        /// <summary>
        /// 更新血量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverHp(this RecoverComponent self)
        {
            if (self.isDeath || self.isWarring) return;

            NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();

            if (numC[NumericType.Valuation] == numC[NumericType.MaxValuation]) return;

            if (numC[NumericType.Valuation] > numC[NumericType.MaxValuation])
            {
                numC[NumericType.Valuation] = numC[NumericType.MaxValuation];
                Console.WriteLine(" Hp: " + numC[NumericType.Valuation] + " MaxHp: " + numC[NumericType.MaxValuation]);
                return;
            }

            if (numC[NumericType.Valuation] < numC[NumericType.MaxValuation])
            {
                if (!self.hpNull)
                {
                    self.hptimer = TimeHelper.ClientNowSeconds();
                    self.hpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.hptimer) > self.reshpTime)
                {
                    numC[NumericType.ValuationAdd] += (int)(numC[NumericType.MaxValuation] * self.reshp);
                    self.hpNull = false;
                }
            }
        }

        /// <summary>
        /// 更新法量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverMp(this RecoverComponent self)
        {
            if (self.isDeath) return;

            NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();
            if (numC[NumericType.Manage] == numC[NumericType.MaxManage]) return;

            if (numC[NumericType.Manage] > numC[NumericType.MaxManage])
            {
                numC[NumericType.Manage] = numC[NumericType.MaxManage];
                Console.WriteLine(" Mp: " + numC[NumericType.Manage] + " MaxMp: " + numC[NumericType.MaxManage]);
                return;
            }

            if (numC[NumericType.Manage] < numC[NumericType.MaxManage])
            {
                if (!self.mpNull)
                {
                    self.mptimer = TimeHelper.ClientNowSeconds();
                    self.mpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.mptimer) > self.resmpTime)
                {
                    numC[NumericType.ManageAdd] += (int)(numC[NumericType.MaxManage] * self.resmp);
                    self.mpNull = false;
                }
            }
        }

        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        static async void DeathSettlement(this RecoverComponent self)
        {
            if (self.isSettlement) return;

            NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();

            if (numC[NumericType.Valuation] <= 0)
            {
                ///Hp小于0时，标记死亡状态
                self.isDeath = true;

                M2W_DeathActorResponse response = (M2W_DeathActorResponse)await ActorHelper.ActorLocation(self.GetParent<Unit>().Id).Call(new W2M_DeathActorRequest() { Info = "NumericComponentHelper-107" });                               

                self.isSettlement = true;
            }
        }

        public static void GetExpAndCoin(this RecoverComponent self)
        {
            Unit selfUnit = self.GetParent<Unit>();

            NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();

            UnitSkillComponent targetAttack = selfUnit.GetComponent<UnitSkillComponent>();

            if (selfUnit.GetComponent<UnitSkillComponent>() != null)
            {
                int addexp = numC[NumericType.Level] * numC[NumericType.Level] + 1;
                int addcoin = numC[NumericType.Level] + 1;
                NumericComponent numeric = null;

                ///我的类型，我敌人的类型是什么呢
                switch (self.GetParent<Unit>().UnitType)
                {
                    case UnitType.Player:
                        if (targetAttack.attackers.Count > 0)
                        {
                            foreach (long tem in targetAttack.attackers.ToArray())
                            {
                                numeric = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<NumericComponent>();
                                numeric[NumericType.ExpAdd] += addexp;
                                numeric[NumericType.CoinAdd] += addcoin;

                                numeric.GetParent<Unit>().GetComponent<AoiUnitComponent>().playerIds.MovesSet.Remove(selfUnit.Id);
                            }
                            targetAttack.attackers.Clear();
                        }
                        Console.WriteLine(" DeathSettlement-165-type:addexp/exp addcoin/coin: " + numeric.GetParent<Unit>().UnitType + ": " + addexp + "/" + numeric[NumericType.Exp] + "  " + addcoin + "/" + numeric[NumericType.Coin]);
                        break;
                    case UnitType.Monster:
                        if (targetAttack.attackers.Count > 0)
                        {
                            foreach (long tem in targetAttack.attackers.ToArray())
                            {
                                numeric = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<NumericComponent>();
                                numeric[NumericType.ExpAdd] += addexp;
                                numeric[NumericType.CoinAdd] += addcoin;

                                numeric.GetParent<Unit>().GetComponent<AoiUnitComponent>().enemyIds.MovesSet.Remove(selfUnit.Id);
                            }
                            targetAttack.attackers.Clear();
                        }
                        Console.WriteLine(" DeathSettlement-183-type:addexp/exp addcoin/coin: " + numeric.GetParent<Unit>().UnitType + ": " + addexp + "/" + numeric[NumericType.Exp] + "  " + addcoin + "/" + numeric[NumericType.Coin]);
                        break;
                    case UnitType.Npcer:

                        break;
                }
            }
        }


    }
}
