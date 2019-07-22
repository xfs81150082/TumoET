using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class StateMoveComponent : Component
    {
        public float wsForword = 0.0f;
        public float adTrun = 0.0f;
        public float moveSpeed = 5.0f;
        public float roteSpeed = 5.0f;

        public Vector3 ClickPoint;

        private readonly Move_Map move_Map = new Move_Map();

    }
}
