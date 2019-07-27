using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{  
    public class KeyboardPathComponent : Component
    {
        public bool isCanControl = true;                      //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;
        public float moveSpeed = 40.0f;                       //移动速度
        public float roteSpeed = 140.0f;                      //旋转速度

        public Vector3 Target;
        public CancellationTokenSource CancellationTokenSource;

        public long resTime = 100L;
        public bool isStart1 = false;
        public bool isStart2 = false;

        public long startTime1 = 0L;
        public long offsetTime1 = 0L;
        public long startTime2 = 0L;
        public long offsetTime2 = 0L;




    }
}
