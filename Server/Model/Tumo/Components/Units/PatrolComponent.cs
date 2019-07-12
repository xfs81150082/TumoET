using System;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class PatrolComponentAwakeSystem : AwakeSystem<PatrolComponent>
    {
        public override void Awake(PatrolComponent self)
        {
            self.Awake();
        }
    } 

    public class PatrolComponent : Component
    {
        public bool isPatrol = true;                                   //表示是否在巡逻
        public bool isIdle = true;                                     //表示在休息？

        public bool startNull = false;
        public long startTime = 0;
        public long idleResTime = 4;

        public bool patolNull = false;
        public long patolTimer = 0;
        public long lifeCdTime = 400;

        public int coreDis = 20;
        public int coreRan = 0;
        public Vector3 spawnPosition { get; set; } = new Vector3(0, 0, 0);
        public Vector3 goalPoint;

        public Vector3 patrolPoint;
        public Patrol_Map patrolMap { get; set; }
        public long speed = 1;

        public void Awake()
        {
            this.spawnPosition = new Vector3(GetParent<Unit>().Position.x, GetParent<Unit>().Position.y, GetParent<Unit>().Position.z);
            coreRan = Convert.ToInt32(GetParent<Unit>().Id % 10);
        }        
    }
}