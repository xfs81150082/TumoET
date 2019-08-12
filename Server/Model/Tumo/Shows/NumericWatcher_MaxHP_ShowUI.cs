using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [NumericWatcher(NumericType.MaxValuation)]
    public class NumericWatcher_MaxHP_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
            ///20190621
            Unit unit = Game.Scene.GetComponent<MonsterUnitComponent>().Get(id);
            if (unit != null)
            {
                NumericComponent num = unit.GetComponent<NumericComponent>();
                int mhb = num[NumericType.MaxValuationBase];
                int mha = num[NumericType.MaxValuationAdd];

                Console.WriteLine(" type/MaxHp/Maxhb/Maxha: " + unit.UnitType + " ：" + value + " / " + mhb + " / " + mha);
            }
        }
    }
}
