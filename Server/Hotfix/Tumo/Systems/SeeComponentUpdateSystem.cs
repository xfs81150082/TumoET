using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class SeeComponentUpdateSystem : UpdateSystem<SeeComponent>
    {
        public override void Update(SeeComponent self)
        {
            if (self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol) return;

            if (self.target == null)
            {
                if (self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastDistance < self.GetParent<Unit>().GetComponent<NumericComponent>().enterWarringSqr)
                {
                    self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
                }
            }
            else
            {
                self.targetDistance = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);

                if (self.targetDistance > self.GetParent<Unit>().GetComponent<NumericComponent>().enterWarringSqr)
                {
                    self.target = null;
                }

                ///如果卡住在地图到达不了目标点 此计时40秒后 重置巡逻目标点
                if (!self.startNull)
                {
                    self.seeTimer = TimeHelper.ClientNow();
                    self.startNull = true;
                }

                ///精确到毫秒
                long timeNow = TimeHelper.ClientNow();

                if ((timeNow - self.seeTimer) > self.resTime)
                {
                    //如果追到目标后，离目标距离小于2米，不追击
                    if (self.targetDistance < 4f) return;

                    // 每间隔 resTime（100） MS 发送一次目标点坐标 消息
                    self.SendSeePosition();

                    self.startNull = false;
                }
            }
        }


    }
}
