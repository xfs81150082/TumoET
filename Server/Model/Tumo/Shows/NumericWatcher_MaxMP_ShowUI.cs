using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [NumericWatcher(NumericType.MaxMp)]
    public class NumericWatcher_MaxMP_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
            ///20190621
            //Console.WriteLine(" UnitId: " + id + ". MaxMp 最大法量变化了，变化之后的值为：" + value);
        }
    }
}
