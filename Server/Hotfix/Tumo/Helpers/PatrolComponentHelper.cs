using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class PatrolComponentHelper 
    {
        /// <summary>
        /// 发送巡逻目标点坐标 消息
        /// </summary>
        /// <param name="self"></param>
        public static void SendPatrolMap(PatrolComponent self)
        {
            /// 休息时间到 开始发送巡逻目标点坐标 消息
            self.patrolMap = self.GetPatrolMap();
            self.goalPoint = self.patrolPoint;
            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.GetParent<Unit>().Id);
            actorLocationSender.Send(self.patrolMap);
        }

        static Patrol_Map GetPatrolMap(this PatrolComponent self)
        {
            self.patrolPoint = TargetPositon(self);
            Patrol_Map patrol_Map = new Patrol_Map() { Id = self.GetParent<Unit>().Id, X = self.patrolPoint.x, Y = self.patrolPoint.y, Z = self.patrolPoint.z };
            return patrol_Map;
        }

        static Vector3 TargetPositon(this PatrolComponent self)
        {
            Random ran = new Random(self.coreRan);
            self.coreRan += 1;
            if (self.coreRan > (self.coreDis * 2 - 1))
            {
                self.coreRan = 0;
            }
            int h = ran.Next(0, self.coreDis * 2); 
            int v = ran.Next(0, self.coreDis * 2); 
            Vector3 offset = new Vector3(h - self.coreDis, 0, v - self.coreDis);
            offset.y = self.spawnPosition.y;
            Vector3 endVec = offset + self.spawnPosition;
            int limitXZ = Game.Scene.GetComponent<AoiGridComponent>().mapWide / 2 - 5;
            if (endVec.x < -limitXZ)
            {
                endVec.x = -limitXZ;
            }
            if (endVec.x > limitXZ)
            {
                endVec.x = limitXZ;
            }
            if (endVec.z < -limitXZ)
            {
                endVec.z = -limitXZ;
            }
            if (endVec.z > limitXZ)
            {
                endVec.z = limitXZ;
            }
            return endVec;
        }

        /// <summary>
        /// 回到出生地
        /// </summary>
        /// <param name="self"></param>
        public static void SendPatrolSpawnMap(this PatrolComponent self)
        {
            /// 休息时间到 开始发送巡逻目标点坐标 消息
            self.patrolMap = GetPatrolSpawnMap(self);
            self.goalPoint = self.patrolPoint;
            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.GetParent<Unit>().Id);
            actorLocationSender.Send(self.patrolMap);
        }

        static Patrol_Map GetPatrolSpawnMap(this PatrolComponent self)
        {
            self.patrolPoint = self.spawnPosition;
            Patrol_Map patrol_Map = new Patrol_Map() { Id = self.GetParent<Unit>().Id, X = self.patrolPoint.x, Y = self.patrolPoint.y, Z = self.patrolPoint.z };
            return patrol_Map;
        }

        static void IsDeath(PatrolComponent self)
        {
            float dis = SqrDistanceHelper.Distance(self.GetParent<Unit>().Position, self.spawnPosition);
            if (dis < 0.01f)
            {
                self.GetParent<Unit>().GetComponent<AttackComponent>().isDeath = true;
            }
        }

    }
}