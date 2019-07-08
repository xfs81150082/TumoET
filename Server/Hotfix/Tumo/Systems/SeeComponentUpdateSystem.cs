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
                if (self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastDistance < self.canSeeDistance)
                {
                    self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
                }
            }
            else
            {
                self.targetDistance = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);

                if (self.targetDistance > self.canSeeDistance)
                {
                    self.target = null;
                }

                self.seeTimer += 1;
                if (self.seeTimer > self.resTime)
                {
                    //如果追到目标后，离目标距离小于2米，不追击
                    if (self.targetDistance < 4f) return;

                    // 每间隔 40 MS 发送一次目标点坐标 消息
                    self.seeMap = self.GetSeeMap();
                    if (self.seeMap != null)
                    {
                        ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get((self.Parent as Unit).Id);
                        actorLocationSender.Send(self.seeMap);
                        self.seeTimer = 0;
                    }
                }

            }
        }       

    }
}