using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{  
    public class ServerMoveComponent : Component
    {
        public bool isCanControl = true;                   //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;
        public float moveSpeed = 4.0f;                      //移动速度
        public float roteSpeed = 5.0f;                      //旋转速度
        public float resTime = 100f;

        public bool isStart1 = false;
        public long startTime1 = 0;
        public long offsetTime1 = 0;

        public bool isStart2 = false;
        public long startTime2 = 0;
        public long offsetTime2 = 0;

        public CancellationTokenSource CancellationTokenSource;



    }
}
