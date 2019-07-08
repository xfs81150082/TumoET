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
            AttackSkillHelper.DeathSettlement(self.GetParent<Unit>());

            if (self.GetParent<Unit>().GetComponent<AttackComponent>() != null && self.GetParent<Unit>().GetComponent<AttackComponent>().isDeath) return;

            RecoverHp(self);
            RecoverMp(self);
        }

        /// <summary>
        /// 更新血量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverHp(RecoverComponent self)
        {
            NumericComponent num = self.GetParent<Unit>().GetComponent<NumericComponent>();
            AttackComponent attack = self.GetParent<Unit>().GetComponent<AttackComponent>();

            if (num[NumericType.Hp] == num[NumericType.MaxHp]) return;

            if (num[NumericType.Hp] > num[NumericType.MaxHp])
            {
                num[NumericType.Hp] = num[NumericType.MaxHp];
                Console.WriteLine(" Hp: " + num[NumericType.Hp] + " MaxHp: " + num[NumericType.MaxHp]);
                return;
            }

            if (num[NumericType.Hp] < num[NumericType.MaxHp])
            {
                if (!attack.isAttacking && !attack.isDeath)
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
        }

        /// <summary>
        /// 更新法量
        /// </summary>
        /// <param name="self"></param>
        static void RecoverMp(RecoverComponent self)
        {
            NumericComponent num = self.GetParent<Unit>().GetComponent<NumericComponent>();
            AttackComponent attack = self.GetParent<Unit>().GetComponent<AttackComponent>();

            if (num[NumericType.Mp] == num[NumericType.MaxMp]) return;

            if (num[NumericType.Mp] > num[NumericType.MaxMp])
            {
                num[NumericType.Mp] = num[NumericType.MaxMp];
                Console.WriteLine(" Mp: " + num[NumericType.Mp] + " MaxMp: " + num[NumericType.MaxMp]);
                return;
            }

            if (num[NumericType.Mp] < num[NumericType.MaxMp])
            {
                if (!attack.isDeath)
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

        }

        ///// <summary>
        ///// 小怪回到出生地，重生
        ///// </summary>
        ///// <param name="self"></param>
        //static void IsDeath(Unit unit)
        //{
        //    if (unit.GetComponent<PatrolComponent>() != null)
        //    {
        //        float dis = SqrDistanceHelper.Distance(unit.Position, unit.GetComponent<PatrolComponent>().spawnPosition);
        //        if (dis < 0.01f)
        //        {
        //            if (unit.GetComponent<AttackComponent>().isDeath)
        //            {
        //                unit.GetComponent<AttackComponent>().isDeath = false;
        //            }
        //        }
        //    }
        //}


    }
}