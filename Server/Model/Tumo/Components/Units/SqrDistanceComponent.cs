using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{  
    public class SqrDistanceComponent : Component
    {
        public float neastDistance { get; set; } = float.PositiveInfinity;
        public Unit neastUnit { get; set; }                                    //最近的敌人
        
    }
}