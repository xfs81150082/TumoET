using System;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class PingComponentAwakeSystem : AwakeSystem<PingComponent, long, Action>
    {      
        public override void Awake(PingComponent self, long waitTime, Action action)
        {
            self.Awake(waitTime, action);
        }
    }
    
    /// <summary>
    /// 心跳组件+PING组件
    /// </summary>
    public class PingComponent : Component
    {
        #region 成员变量

        /// <summary>
        /// 发送时间
        /// </summary>
        private long _sendTimer;
        
        /// <summary>
        /// 接收时间
        /// </summary>
        private long _receiveTimer;

        /// <summary>
        /// 延时
        /// </summary>
        public long Ping = 0;
        
        /// <summary>
        /// 心跳协议包
        /// </summary>
        private readonly C2G_PingRequest _request = new C2G_PingRequest();

        #endregion

        #region Awake

        public async void Awake(long waitTime, Action action)
        {
            var timerComponent = Game.Scene.GetComponent<TimerComponent>();

            var session = this.GetParent<Session>();

            while (true)
            {
                try
                {
                    _sendTimer = TimeHelper.ClientNowSeconds();
                    
                    await session.Call(_request);
                    
                    _receiveTimer = TimeHelper.ClientNowSeconds();
                    
                    // 计算延时

                    Ping = ((_receiveTimer - _sendTimer) / 2) < 0 ? 0 : (_receiveTimer - _sendTimer) / 2;

                    //Debug.Log(" 计算延时-rpcid: " + _request.RpcId + " / " + Ping);
                }
                catch (Exception e)
                {
                    // 执行断线后的操作
                    
                    action?.Invoke();

                    Debug.Log(" 断线了: " + e.Message);
                }
                
                await timerComponent.WaitAsync(waitTime);
            }
        }

        #endregion

        public override void Dispose()
        {
            if (this.IsFromPool) return;
			
            base.Dispose();
        }
    }
}