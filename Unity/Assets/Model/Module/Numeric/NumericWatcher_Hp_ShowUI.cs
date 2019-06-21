using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 监视hp数值变化，改变血条值
    /// </summary>
    [NumericWatcher(NumericType.Hp)]
    public class NumericWatcher_Hp_ShowUI : INumericWatcher
    {
        public void Run(long id, int value)
        {
            ///20190621
            //Log.Info("小骷髅ID为" + id + "血量变化了，变化之后的值为：" + value);
            Debug.Log("小骷髅ID为" + id + "血量变化了，变化之后的值为：" + value);

            Unit unit = UnitComponent.Instance.Get(id);
            if (unit != null && unit.GetComponent<NumericComponent>() != null)
            {
                Dictionary<int, int> dict = unit.GetComponent<NumericComponent>().NumericDic;
                if (dict.Count > 0)
                {
                    Debug.Log("小骷髅NumericDic-Count: " + dict.Count);
                    foreach (int tem in dict.Keys)
                    {
                        Debug.Log(" dict.Keys: " + tem + " dict.Values: " + dict[tem]);
                    }
                    foreach (int tem in dict.Values)
                    {
                        Debug.Log(" dict.Values: " + tem);
                    }
                }

            }

        }
    }
}
