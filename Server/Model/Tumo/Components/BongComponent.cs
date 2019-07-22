using System;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    [ObjectSystem]
    public class BongComponentAwakeSystem : AwakeSystem<BongComponent, long, long, Action<long>>
    {
        public override void Awake(BongComponent self, long waitTime, long overtime, Action<long> action)
        {
            self.Awake(waitTime, overtime, action);
        }
    }
    
    public class BongComponent : Component
    {
        private readonly Dictionary<long, long> _sessionTimes = new Dictionary<long, long>();

        public async void Awake(long waitTime, long overtime, Action<long> action)
        {
            var timerComponent = Game.Scene.GetComponent<TimerComponent>();

            while (true)
            {
                try
                {                   
                    Console.WriteLine("在线人数 ：" + _sessionTimes.Count.ToString() + " / " + Game.Scene.GetComponent<NetOuterComponent>().Count);

                    await timerComponent.WaitAsync(waitTime);

                    // 检查所有Session，如果有时间超过指定的间隔就执行action

                    for (int i = 0; i < _sessionTimes.Count; i++)
                    {
                        if ((TimeHelper.ClientNowSeconds() - _sessionTimes.ElementAt(i).Value) > overtime)
                        {
                            action?.Invoke(_sessionTimes.ElementAt(i).Key);

                            _sessionTimes.Remove(_sessionTimes.ElementAt(i).Key);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
        }

        public void AddSession(long id)
        {
            _sessionTimes.Add(id,TimeHelper.ClientNowSeconds());
        }

        public void UndateSession(long id)
        {
            Console.WriteLine(" id: " + id + " / " + TimeHelper.ClientNowSeconds());
            Console.WriteLine(" id: " + id + " vaule: " + _sessionTimes[id] + " / " + TimeHelper.ClientNowSeconds());
            if (_sessionTimes.ContainsKey(id)) _sessionTimes[id] = TimeHelper.ClientNowSeconds();
        }
    }
}