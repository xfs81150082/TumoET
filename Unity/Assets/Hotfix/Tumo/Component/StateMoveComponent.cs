using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class StateMoveComponentAwakeSystem : AwakeSystem<StateMoveComponent>
    {
        public override void Awake(StateMoveComponent self)
        {
            self.Awake();
        }
    }

    public class StateMoveComponent : Component
    {
        public float wsForword = 0.0f;
        public float adTrun = 0.0f;
        public float moveSpeed = 5.0f;
        public float roteSpeed = 5.0f;

        public Vector3 ClickPoint;
        public int mapMask;

        private readonly Move_Map move_Map = new Move_Map();

        public void Awake()
        {
            this.mapMask = LayerMask.GetMask("Map");
        }


    }
}
