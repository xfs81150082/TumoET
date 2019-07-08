using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{  
    public class SqrDistanceComponent : Component
    {
        public float neastDistance { get; set; } = 10000f;
        public Unit neastUnit { get; set; }                             //最近的敌人
        public float seeDistance { get; set; } = 400f;
        
    }
}