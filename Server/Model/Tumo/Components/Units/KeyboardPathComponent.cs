using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class KeyboardPathComponent : Component
    { 
        public bool isControl = false;                  //是否键盘控制
        public float h = 0;                             // “X” 水平轴 要能自动 递减
        public float v = 0;                             // “Z” 水平轴 要能自动 递减
        public int hCount = 4;
        public int vCount = 4;
        public float moveSpeed = 4.0f;                  //虚拟 移动速度
        public float roteSpeed = 14.0f;                 //虚拟 旋转速度

        public long startTimeMove = 0L;
        public long offsetTimeMove = 0L;
        public long startTimeTurn = 0L;
        public long offsetTimeTurn = 0L;
        public long resTime = 400L;


    }
}
