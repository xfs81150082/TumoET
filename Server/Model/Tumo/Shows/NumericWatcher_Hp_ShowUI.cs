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
            Console.WriteLine(" UnitId: " + id + ". Hp 血量变化了，变化之后的值为：" + value + " NowTime: " + TimeHelper.ClientNowSeconds());
        }
    }
}