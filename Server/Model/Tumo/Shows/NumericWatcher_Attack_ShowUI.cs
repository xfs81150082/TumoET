using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [NumericWatcher(NumericType.Attack)]
    public class NumericWatcher_Attack_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
            ///20190703
            //Console.WriteLine(" UnitId: " + id + ". Attack 攻击力变化了，变化之后的值为：" + value);
        }
    }
}
