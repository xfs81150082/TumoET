using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class PatrolComponentUpdateSystem : UpdateSystem<PatrolComponent>
    {
        public override void Update(PatrolComponent self)
        {
            if (!self.isPatrol) return;
            Patrol(self);
        }

        void Patrol(PatrolComponent self)
        {
            if (self.isIdle)
            {
                if (!self.startNull)
                {
                    self.startTime = TimeHelper.ClientNowSeconds();
                    self.startNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                ///休息时间到 开始发送巡逻目标点坐标 消息
                if ((timeNow - self.startTime) > self.idleResTime)
                {
                    PatrolComponentHelper.SendPatrolPosition(self);
                    self.startNull = false;
                    self.isIdle = false;
                }                
            }
            else
            {
                ///如果卡住在地图到达不了目标点 此计时40秒后 重置巡逻目标点
                if (!self.patolNull)
                {
                    self.patolTimer = TimeHelper.ClientNowSeconds();
                    self.patolNull = true;
                }

                long timeNow = TimeHelper.ClientNowSeconds();

                if ((timeNow - self.patolTimer) > self.lifeCdTime)
                {
                    self.patolNull = false;
                    self.isIdle = true;
                }
            
                ///如果到达目标点,开始休息,并计时4秒
                float sqr = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.goalPoint);

                if(sqr < 0.01f)
                {
                    self.patolNull = false;
                    self.isIdle = true;
                }
            }
        }
        
        
    }
}