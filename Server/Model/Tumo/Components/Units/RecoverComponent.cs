using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    //[ObjectSystem]
    //public class RecoverComponentUpdateSystem : UpdateSystem<RecoverComponent>
    //{
    //    public override void Update(RecoverComponent self)
    //    {
    //        self.Update();
    //    }
    //}

    public class RecoverComponent : Component
    {
        public bool hpNull = false;
        public long hptimer = 0;
        public long reshpTime = 4;
        public float reshp = 0.15f;

        //public bool isShow = true;

        //public void Update()
        //{
        //    //RecoverHp();
        //}

        /// <summary>
        /// 更新 回复血量
        /// </summary>
        /// <param name="self"></param>
        public void RecoverHp()
        {
            NumericComponent numC = this.GetParent<Unit>().GetComponent<NumericComponent>();

            if (numC[NumericType.Valuation] == numC[NumericType.MaxValuation]) return;

            if (numC[NumericType.Valuation] > numC[NumericType.MaxValuation])
            {
                numC[NumericType.Valuation] = numC[NumericType.MaxValuation];

                Console.WriteLine(" type/Hp/hb/ha: " + numC.GetParent<Unit>().UnitType + " ：" + numC[NumericType.Valuation] + " / " + numC[NumericType.ValuationBase] + " / " + numC[NumericType.ValuationAdd]);

                return;
            }

            if (numC[NumericType.Valuation] < numC[NumericType.MaxValuation])
            {
                if (!this.hpNull)
                {
                    this.hptimer = TimeHelper.ClientNowSeconds();
                    this.hpNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - this.hptimer) > this.reshpTime)
                {
                    numC[NumericType.ValuationAdd] += (int)(numC[NumericType.MaxValuation] * this.reshp);
                    this.hpNull = false;
                }
            }
        }

        /// <summary>
        /// 更新 回复血量
        /// </summary>
        /// <param name="self"></param>
        //void RecoverHp()
        //{
        //    NumericComponent numC = this.GetParent<Unit>().GetComponent<NumericComponent>();
        //    if (numC[NumericType.Valuation] == numC[NumericType.MaxValuation]) return;

        //    AttackComponent attack = this.GetParent<Unit>().GetComponent<AttackComponent>();

        //    if (!attack.isDeath & !attack.isAttacking)
        //    {
        //        if (this.isShow)
        //        {
        //            Console.WriteLine(" RecoverComponent-45-进入 回血。" + attack.isDeath + " / " + attack.isAttacking);

        //            this.isShow = false;
        //        }

        //        if (numC[NumericType.Valuation] > numC[NumericType.MaxValuation])
        //        {
        //            numC[NumericType.Valuation] = numC[NumericType.MaxValuation];

        //            Console.WriteLine(" type/Hp/hb/ha: " + numC.GetParent<Unit>().UnitType + " ：" + numC[NumericType.Valuation] + " / " + numC[NumericType.ValuationBase] + " / " + numC[NumericType.ValuationAdd]);

        //            return;
        //        }

        //        if (numC[NumericType.Valuation] < numC[NumericType.MaxValuation])
        //        {
        //            if (!this.hpNull)
        //            {
        //                this.hptimer = TimeHelper.ClientNowSeconds();
        //                this.hpNull = true;
        //            }

        //            long timeNow = TimeHelper.ClientNowSeconds();

        //            if ((timeNow - this.hptimer) > this.reshpTime)
        //            {
        //                numC[NumericType.ValuationAdd] += (int)(numC[NumericType.MaxValuation] * this.reshp);
        //                this.hpNull = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (!this.isShow)
        //        {
        //            if (attack.isDeath)
        //            {
        //                Console.WriteLine(" RecoverComponent-82-进入 死亡。" + attack.isDeath);
        //            }
        //            if (attack.isAttacking)
        //            {
        //                Console.WriteLine(" RecoverComponent-86-进入 战斗。" + attack.isAttacking);
        //            }
        //            this.isShow = true;
        //        }
        //    }
        //}


    }
}
