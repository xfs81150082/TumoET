using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 监视hp数值变化，改变血条值
    /// </summary>
    [NumericWatcher(NumericType.Valuation)]
    public class NumericWatcher_Hp_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
             ///20190621
            Unit unit = Game.Scene.GetComponent<MonsterUnitComponent>().Get(id);
            if (unit != null)
            {
                NumericComponent num = unit.GetComponent<NumericComponent>();
                int hb = num[NumericType.ValuationBase];
                int ha = num[NumericType.ValuationAdd];

                Console.WriteLine(" type/Hp/hb/ha: " + unit.UnitType + " ：" + value + " / " + hb + " / " + ha);
           }
        }
    }
}