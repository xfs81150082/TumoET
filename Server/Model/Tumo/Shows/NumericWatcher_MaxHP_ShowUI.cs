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
            //Console.WriteLine(" UnitId: " + id + ". MaxHp 最大血量变化了，变化之后的值为：" + value);
        }
    }
}
