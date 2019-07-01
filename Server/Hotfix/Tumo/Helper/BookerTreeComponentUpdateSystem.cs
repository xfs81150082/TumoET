using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class BookerTreeComponentUpdateSystem : UpdateSystem<BookerTreeComponent>
    {
        public override void Update(BookerTreeComponent self)
        {
            //Console.WriteLine(" BookerTreeComponentUpdateSystem-idleTimer: " + self.idleTimer);

            if (!self.isPatrol) return;

            if (self.isIdle)
            {
                self.idleTimer += 1;
                if (self.idleTimer > self.idleRestime)
                {
                    Booker_PatrolMap patrolMap = self.PatrolMap();

                    // 发送消息
                    ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.booker.Id);
                    actorLocationSender.Send(patrolMap);

                    Console.WriteLine(" BookerTreeComponentUpdateSystem-xyz-19: " + patrolMap.X + ", " + patrolMap.Y + ", " + patrolMap.Z);


                    self.idleTimer = 0;
                    self.isIdle = false;
                }
                //Console.WriteLine(" idleTimer: " + idleTimer);
            }
            else
            {
                self.patolTimer += 1;
                if (self.patolTimer > self.resTime)
                {
                    self.isIdle = true;
                    self.patolTimer = 0;
                }
                //Console.WriteLine(" patolTimer: " + patolTimer);
            }


        }

        #region 正在巡逻

        // public void RolerPotrol(this BookerTreeComponent self )
        // {
        //     if (!self.isPatrol) return;

        //     if (self.isIdle)
        //     {
        //         self.idleTimer += 1;
        //         if (self.idleTimer > self.idleRestime)
        //         {
        //             SetPatrolPoint(self);
        //             self.idleTimer = 0;
        //             self.isIdle = false;
        //         }
        //         //Console.WriteLine(" idleTimer: " + idleTimer);
        //     }
        //     else
        //     {
        //         self.patolTimer += 1;
        //         if (self.patolTimer > self.resTime)
        //         {
        //             self.isIdle = true;
        //             self.patolTimer = 0;
        //         }
        //         //Console.WriteLine(" patolTimer: " + patolTimer);
        //     }
        // }

        //public void SetPatrolPoint(this BookerTreeComponent self)
        // {
        //     try
        //     {
        //         self.patrolPoint = TargetPositon(self);
        //         Console.WriteLine(" patrolPoint: " + self.patrolPoint.z);


        //         Booker_PatrolMap booker_PatrolMap = new Booker_PatrolMap() { Id = self.booker.Id };
        //         booker_PatrolMap.X = self.patrolPoint.x;
        //         booker_PatrolMap.Y = self.patrolPoint.y;
        //         booker_PatrolMap.Z = self.patrolPoint.z;

        //         Console.WriteLine(" booker_PatrolMap-xyz: " + booker_PatrolMap.X + ", " + booker_PatrolMap.Y + ", " + booker_PatrolMap.Z);

        //         IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
        //         Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
        //         //mapSession.Send(booker_PatrolMap);

        //         //ActorMessageSenderComponent actorLocationSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
        //         //ActorMessageSender actorMessageSender = actorLocationSenderComponent.Get(mapSession.Id);
        //         //actorMessageSender.Send(booker_PatrolMap);

        //         //MessageHelper.Broadcast(booker_PatrolMap);

        //         Console.WriteLine(booker_PatrolMap.Id + " :Id/ActorId: " + booker_PatrolMap.ActorId);
        //     }
        //     catch (Exception e)
        //     {
        //         Log.Error(e);
        //     }
        // }

        // //public void Broadcast(IActorMessage message)
        // //{
        // //    Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
        // //    ActorMessageSenderComponent actorLocationSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
        // //    foreach (Unit unit in units)
        // //    {
        // //        UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
        // //        if (unitGateComponent.IsDisconnect)
        // //        {
        // //            continue;
        // //        }

        // //        ActorMessageSender actorMessageSender = actorLocationSenderComponent.Get(unitGateComponent.GateSessionActorId);
        // //        actorMessageSender.Send(message);
        // //    }
        // //}

        // private Vector3 TargetPositon(this BookerTreeComponent self)
        // {
        //     Random ran = new Random(self.coreRan);
        //     self.coreRan += 1;
        //     if (self.coreRan > 39)
        //     {
        //         self.coreRan = 0;
        //     }
        //     int h = ran.Next(0, 40); //17
        //     int v = ran.Next(0, 40); //35

        //     Console.WriteLine(" hv: " + h + " / " + v + " coreRan: " + self.coreRan);

        //     Vector3 offset = new Vector3(h - 20, 0, v - 20);
        //     offset.y = self.spawnPosition.y;
        //     return (offset + self.spawnPosition);
        // }



        #endregion


    }
}
