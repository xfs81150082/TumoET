using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class LifeComponentHelper
    {
        /// <summary>
        ///  更新血量和法量, 更新断定死亡结算
        /// </summary>
        /// <param name="self"></param>
        public static void UpdateProperty(this LifeComponent self)
        {
            if (self.isDeath && self.isSettlement) return;

            self.DeathSettlement();

            self.RecoverHp();

            self.RecoverMp();
        }

        /// <summary>
        /// 更新血量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverHp(this LifeComponent self)
        {
            if (self.isDeath || self.isWarring) return;

            NumericComponent num = self.GetParent<Unit>().GetComponent<NumericComponent>();

            if (num[NumericType.Hp] == num[NumericType.MaxHp]) return;

            if (num[NumericType.Hp] > num[NumericType.MaxHp])
            {
                num[NumericType.Hp] = num[NumericType.MaxHp];
                Console.WriteLine(" Hp: " + num[NumericType.Hp] + " MaxHp: " + num[NumericType.MaxHp]);
                return;
            }

            if (num[NumericType.Hp] < num[NumericType.MaxHp])
            {
                if (!self.hpNull)
                {
                    self.hptimer = TimeHelper.ClientNowSeconds();
                    self.hpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.hptimer) > self.reshpTime)
                {
                    num[NumericType.HpAdd] += (int)(num[NumericType.MaxHpBase] * self.reshp);
                    self.hpNull = false;
                }
            }
        }

        /// <summary>
        /// 更新法量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverMp(this LifeComponent self)
        {
            if (self.isDeath) return;

            NumericComponent num = self.GetParent<Unit>().GetComponent<NumericComponent>();

            if (num[NumericType.Mp] == num[NumericType.MaxMp]) return;

            if (num[NumericType.Mp] > num[NumericType.MaxMp])
            {
                num[NumericType.Mp] = num[NumericType.MaxMp];
                Console.WriteLine(" Mp: " + num[NumericType.Mp] + " MaxMp: " + num[NumericType.MaxMp]);
                return;
            }

            if (num[NumericType.Mp] < num[NumericType.MaxMp])
            {
                if (!self.mpNull)
                {
                    self.mptimer = TimeHelper.ClientNowSeconds();
                    self.mpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.mptimer) > self.resmpTime)
                {
                    num[NumericType.MpAdd] += (int)(num[NumericType.MaxMpBase] * self.resmp);
                    self.mpNull = false;
                }
            }
        }

        #region  死亡判断 后结算
        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        static void DeathSettlement(this LifeComponent self)
        {
            if (self.isSettlement) return;

            Unit selfUnit = self.GetParent<Unit>();

            if (selfUnit.GetComponent<AttackComponent>() != null && selfUnit.GetComponent<NumericComponent>() != null)
            {
                AttackComponent targetAttack = selfUnit.GetComponent<AttackComponent>();
                NumericComponent targetNum = selfUnit.GetComponent<NumericComponent>();

                //Console.WriteLine(" DeathSettlement-116-isSettlement: " + self.isSettlement);

                if (targetNum[NumericType.Hp] <= 0)
                {
                    self.isDeath = true;

                    ///从格子 注销 因为已死亡
                    Game.Scene.GetComponent<AoiGridComponent>().RemoveUnitId(selfUnit.GetComponent<AoiUnitComponent>());

                    ///通知 播放 死亡录像
                    ///TOTO
                }

                if (self.isDeath)
                {
                    if (targetAttack.attackers.Count > 0)
                    {
                        int addexp = targetNum[NumericType.Level] * targetNum[NumericType.Level] + 1;
                        int addcoin = targetNum[NumericType.Level] + 1;
                        NumericComponent numeric;

                        foreach (long tem in targetAttack.attackers.ToArray())
                        {
                            ///我的类型，我敌人的类型是什么呢
                            switch (selfUnit.UnitType)
                            {
                                case UnitType.Player:
                                    numeric = Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).GetComponent<NumericComponent>();

                                    numeric[NumericType.ExpAdd] += addexp;
                                    numeric[NumericType.CoinAdd] += addcoin;

                                    numeric.GetParent<Unit>().GetComponent<AoiUnitComponent>().playerIds.MovesSet.Remove(selfUnit.Id);

                                    Console.WriteLine(" DeathSettlement-150-type:addexp/exp addcoin/coin: " + numeric.GetParent<Unit>().UnitType + ": " + addexp + "/" + numeric[NumericType.Exp] + "  " + addcoin + "/" + numeric[NumericType.Coin]);
                                    break;
                                case UnitType.Monster:
                                    numeric = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<NumericComponent>();

                                    numeric[NumericType.ExpAdd] += addexp;
                                    numeric[NumericType.CoinAdd] += addcoin;

                                    numeric.GetParent<Unit>().GetComponent<AoiUnitComponent>().enemyIds.MovesSet.Remove(selfUnit.Id);

                                    Console.WriteLine(" DeathSettlement-160-type:addexp/exp addcoin/coin: " + numeric.GetParent<Unit>().UnitType + ": " + addexp + "/" + numeric[NumericType.Exp] + "  " + addcoin + "/" + numeric[NumericType.Coin]);
                                    break;
                                case UnitType.Npc:

                                    break;
                            }
                        }
                        targetAttack.attackers.Clear();
                    }
                    self.isSettlement = true;

                    Console.WriteLine(" DeathSettlement-171-isSettlement: " + self.isSettlement);

                    SendUnitIdToRemoveUnit(selfUnit);

                }
            }
        }

        static void SendUnitIdToRemoveUnit(Unit self )
        {
            MapSessionHelper.Session().Send(new M2M_RemoveUnit() { UnitId = self.Id, UnitType = (int)self.UnitType });

            Console.WriteLine(" SendUnitIdToRemoveUnit-183-id/UnitType: " + self.Id + " / " + self.UnitType);
        }
        #endregion



    }
}