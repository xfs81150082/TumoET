using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class BookerTreeComponentAwakeSystem : AwakeSystem<BookerTreeComponent>
    {
        public override void Awake(BookerTreeComponent self)
        {
            self.Awake();
        }
    } 

    public class BookerTreeComponent : Component
    {
        public Unit booker { get; set; }

        public bool isPatrol = true;                                   //表示是否在巡逻
        public bool isIdle = true;                                     //表示在休息？
        public long idleTimer = 0;
        public long idleRestime = 400;
        public long patolTimer = 0;
        public long resTime = 4000;
        public int coreDis = 20;
        public int coreRan = 0;
        public Vector3 spawnPosition { get; set; } = new Vector3(0, 0, 0);

        public Vector3 patrolPoint;
        private readonly Booker_PatrolMap bookerPatrol = new Booker_PatrolMap();
        public long speed = 4;

        public void Awake()
        {
            this.booker = Parent as Unit;
            this.spawnPosition = new Vector3(booker.Position.x, booker.Position.y, booker.Position.z);
            coreRan = Convert.ToInt32(booker.Id % 10);

            Console.WriteLine(" spawnPosition:" + this.spawnPosition.x + ", " + this.spawnPosition.y + ", " + this.spawnPosition.z + " coreRan: " + coreRan);
        }

        #region 巡逻

        public Booker_PatrolMap PatrolMap()
        {
            patrolPoint = TargetPositon();
            Booker_PatrolMap bookerPatrol = new Booker_PatrolMap() { Id = booker.Id, X = patrolPoint.x, Y = patrolPoint.y, Z = patrolPoint.z };
            return bookerPatrol;
        }
        
        Vector3 TargetPositon()
        {
            Random ran = new Random(coreRan);
            coreRan += 1;
            if (coreRan > 39)
            {
                coreRan = 0;
            }
            int h = ran.Next(0, coreDis * 2); //17
            int v = ran.Next(0, coreDis * 2); //35

            Console.WriteLine(" hv: " + h + " / " + v + " coreRan: " + coreRan);

            Vector3 offset = new Vector3(h - coreDis, 0, v - coreDis);
            offset.y = spawnPosition.y;
            return (offset + spawnPosition);
        }
        #endregion

    }
}