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
        }

        /// <summary>
        /// 更新 回复血量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverHp(this RecoverComponent self)
        {
            if (!self.isDeath && !self.GetParent<Unit>().GetComponent<AttackComponent>().isAttacking)
            {

                NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();
                if (numC[NumericType.Valuation] == numC[NumericType.MaxValuation]) return;
                if (numC[NumericType.Valuation] > numC[NumericType.MaxValuation])
                {
                    numC[NumericType.Valuation] = numC[NumericType.MaxValuation];
                    Console.WriteLine(" Hp/MaxHp: " + numC[NumericType.Valuation] + " / " + numC[NumericType.MaxValuation]);
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
        }

        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        public static void DeathSettlement(this RecoverComponent self)
        {
            if (self.isSettlement) return;
            NumericComponent numC = self.GetParent<Unit>().GetComponent<NumericComponent>();
            if (numC[NumericType.Valuation] <= 0)
            {
                ///Hp小于0时，标记死亡状态
                self.isDeath = true;

                ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get((self.Parent as Unit).Id);
                actorLocationSender.Send(new Death_Map());

                self.isSettlement = true;
            }
        }
 
        /// <summary>
        /// 发放工资 经验和金币
        /// </summary>
        /// <param name="self"></param>
        public static void GetExpAndCoin(this RecoverComponent self)
        {
            Unit selfunit = self.GetParent<Unit>();
            NumericComponent numC = selfunit.GetComponent<NumericComponent>();
            AttackComponent targetAttack = selfunit.GetComponent<AttackComponent>();
            if (targetAttack != null)
            {
                int addexp = numC[NumericType.Level] * numC[NumericType.Level] + 1;
                int addcoin = numC[NumericType.Level] + 1;
                NumericComponent numeric = null;
                ///我的类型，我敌人的类型是什么呢
                switch (selfunit.UnitType)
                {
                    case UnitType.Player:
                        if (targetAttack.attackers.Count > 0)
                        {
                            foreach (long tem in targetAttack.attackers.ToArray())
                            {
                                numeric = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<NumericComponent>();
                                numeric[NumericType.ExpAdd] += addexp;
                                numeric[NumericType.CoinAdd] += addcoin;
                            }
                            targetAttack.attackers.Clear();
                        }
                        Console.WriteLine(" DeathSettlement-101-type(得到经验和金币): " + numeric.GetParent<Unit>().UnitType + " addexp/exp: " + addexp + "/" + numeric[NumericType.Exp] + "  addcoin/coin: " + addcoin + "/" + numeric[NumericType.Coin]);
                        break;
                    case UnitType.Monster:
                        if (targetAttack.attackers.Count > 0)
                        {
                            foreach (long tem in targetAttack.attackers.ToArray())
                            {
                                numeric = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<NumericComponent>();
                                numeric[NumericType.ExpAdd] += addexp;
                                numeric[NumericType.CoinAdd] += addcoin;
                            }
                            targetAttack.attackers.Clear();
                        }
                        Console.WriteLine(" DeathSettlement-112-type(得到经验和金币): " + numeric.GetParent<Unit>().UnitType + " addexp/exp: " + addexp + "/" + numeric[NumericType.Exp] + "  addcoin/coin: " + addcoin + "/" + numeric[NumericType.Coin]);
                        break;
                }
            }
        }


    }
}
