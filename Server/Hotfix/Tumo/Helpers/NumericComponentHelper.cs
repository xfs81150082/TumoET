using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class NumericComponentHelper
    {
        /// <summary>
        ///  更新血量和法量, 更新断定死亡结算
        /// </summary>
        /// <param name="self"></param>
        public static void UpdateProperty(this NumericComponent self)
        {
            self.DeathSettlement();

            self.RecoverHp();

            self.RecoverMp();
        }

        /// <summary>
        /// 更新血量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverHp(this NumericComponent self)
        {
            if (self.isDeath || self.isWarring) return;

            if (self[NumericType.Hp] == self[NumericType.MaxHp]) return;

            if (self[NumericType.Hp] > self[NumericType.MaxHp])
            {
                self[NumericType.Hp] = self[NumericType.MaxHp];
                Console.WriteLine(" Hp: " + self[NumericType.Hp] + " MaxHp: " + self[NumericType.MaxHp]);
                return;
            }

            if (self[NumericType.Hp] < self[NumericType.MaxHp])
            {
                if (!self.hpNull)
                {
                    self.hptimer = TimeHelper.ClientNowSeconds();
                    self.hpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.hptimer) > self.reshpTime)
                {
                    self[NumericType.HpAdd] += (int)(self[NumericType.MaxHpBase] * self.reshp);
                    self.hpNull = false;
                }
            }
        }

        /// <summary>
        /// 更新法量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverMp(this NumericComponent self)
        {
            if (self.isDeath) return;

            if (self[NumericType.Mp] == self[NumericType.MaxMp]) return;

            if (self[NumericType.Mp] > self[NumericType.MaxMp])
            {
                self[NumericType.Mp] = self[NumericType.MaxMp];
                Console.WriteLine(" Mp: " + self[NumericType.Mp] + " MaxMp: " + self[NumericType.MaxMp]);
                return;
            }

            if (self[NumericType.Mp] < self[NumericType.MaxMp])
            {
                if (!self.mpNull)
                {
                    self.mptimer = TimeHelper.ClientNowSeconds();
                    self.mpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.mptimer) > self.resmpTime)
                {
                    self[NumericType.MpAdd] += (int)(self[NumericType.MaxMpBase] * self.resmp);
                    self.mpNull = false;
                }
            }
        }

        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        static async void DeathSettlement(this NumericComponent self)
        {
            if (self.isSettlement) return;

            if (self[NumericType.Hp] <= 0)
            {
                ///Hp小于0时，标记死亡状态
                self.isDeath = true;

                M2W_DeathActorResponse response = (M2W_DeathActorResponse)await ActorHelper.ActorLocation(self.GetParent<Unit>().Id).Call(new W2M_DeathActorRequest() { Info = "NumericComponentHelper-107" });                               

                self.isSettlement = true;
            }
        }

        public static void GetExpAndCoin(this NumericComponent self)
        {
            Unit selfUnit = self.GetParent<Unit>();
            AttackComponent targetAttack = selfUnit.GetComponent<AttackComponent>();

            if (selfUnit.GetComponent<AttackComponent>() != null)
            {
                int addexp = self[NumericType.Level] * self[NumericType.Level] + 1;
                int addcoin = self[NumericType.Level] + 1;
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
