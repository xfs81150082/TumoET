using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [NumericWatcher(NumericType.Mp)]
    public class NumericWatcher_MP_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
            ///20190621
            //Console.WriteLine(" UnitId: " + id + ". Mp 法量变化了，变化之后的值为：" + value + " NowTime: " + TimeHelper.ClientNowSeconds());
        }
    }
}
