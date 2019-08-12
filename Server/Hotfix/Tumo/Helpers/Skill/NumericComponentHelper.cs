using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class NumericComponentHelper
    {
        public static void PlayerNumericInit(this NumericComponent self)
        {
            ///20190621
            // 这里初始化base值，给各个数值进行赋值           
            // 注意，这两个语句都将触发数值改变组件，只是没有写Max的处理函数，所以会没有反应
            self.Set(NumericType.Max, 9981150082);
            //self.Set(NumericType.ManageBase, 10);
            //self.Set(NumericType.MaxManageBase, 100);
            self.Set(NumericType.ValuationBase, 12);
            self.Set(NumericType.MaxValuationBase, 120);
            //self.Set(NumericType.MeasureBase, 10);
            //self.Set(NumericType.MaxMeasureBase, 100);
            self.Set(NumericType.CaseBase, 14);
            self.Set(NumericType.MaxCaseBase, 140);

            self.Set(NumericType.LevelBase, 1);
            self.Set(NumericType.ExpBase, 1);
            self.Set(NumericType.CoinBase, 1);

            self.Set(NumericType.ValuationAdd, 260);               // HpAdd 数值,进行赋值
            self.Set(NumericType.MaxValuationAdd, 260);            // MaxHpAdd 数值,进行赋值
        }

        public static void MonsterNumericInit(this NumericComponent self)
        {
            ///20190621
            // 这里初始化base值，给各个数值进行赋值           
            // 注意，这两个语句都将触发数值改变组件，只是没有写Max的处理函数，所以会没有反应
            self.Set(NumericType.Max, 9981150082);
            //self.Set(NumericType.ManageBase, 10);
            //self.Set(NumericType.MaxManageBase, 100);
            self.Set(NumericType.ValuationBase, 12);
            self.Set(NumericType.MaxValuationBase, 120);
            //self.Set(NumericType.MeasureBase, 10);
            //self.Set(NumericType.MaxMeasureBase, 100);
            self.Set(NumericType.CaseBase, 14);
            self.Set(NumericType.MaxCaseBase, 140);

            self.Set(NumericType.LevelBase, 1);
            self.Set(NumericType.ExpBase, 1);
            self.Set(NumericType.CoinBase, 1);

            self.Set(NumericType.ValuationAdd, 140);               // HpAdd 数值,进行赋值
            self.Set(NumericType.MaxValuationAdd, 140);            // MaxHpAdd 数值,进行赋值
        }


    }
}
