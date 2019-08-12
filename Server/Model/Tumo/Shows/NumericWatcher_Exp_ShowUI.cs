using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    /// <summary>
    /// 监视hp数值变化，改变血条值
    /// </summary>
    [NumericWatcher(NumericType.Exp)]
    public class NumericWatcher_Exp_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
            ///20190621
            //Console.WriteLine(" UnitId: " + id + ". Exp经验值变化了，变化之后的值为：" + value + " NowTime: " + TimeHelper.ClientNowSeconds());
        }
    }
}