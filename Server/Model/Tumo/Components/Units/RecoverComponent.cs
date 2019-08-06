using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class RecoverComponentAwakeSystem : AwakeSystem<RecoverComponent>
    {
        public override void Awake(RecoverComponent self)
        {
            self.Awake();
        }
    }

    public class RecoverComponent : Component
    {
        public bool isDeath = false;
        public bool isWarring = false;
        public bool isSettlement = false;
        public float enterWarringSqr = 225.0f;

        public bool hpNull = false;
        public long hptimer = 0;
        public long reshpTime = 4;
        public float reshp = 0.15f;

        public bool mpNull = false;
        public long mptimer = 0;
        public long resmpTime = 4;
        public float resmp = 0.15f;

        public void Awake()
        {
            NumericComponent numC = this.GetParent<Unit>().GetComponent<NumericComponent>();
            // 这里初始化base值，给各个数值进行赋值
            ///20190621
            //注意，这两个语句都将触发数值改变组件，只是没有写Max的处理函数，所以会没有反应
            numC.Set(NumericType.Max, 981150082);

            numC.Set(NumericType.SpeedBase, 4);
            numC.Set(NumericType.HpBase, 40);
            numC.Set(NumericType.MaxHpBase, 140);
            numC.Set(NumericType.MpBase, 40);
            numC.Set(NumericType.MaxMpBase, 100);
            numC.Set(NumericType.AttackBase, 14);
            numC.Set(NumericType.SkillBase, 40);

            numC.Set(NumericType.Exp, 1);
            numC.Set(NumericType.Level, 1);
            numC.Set(NumericType.Coin, 1);
        }


    }
}
