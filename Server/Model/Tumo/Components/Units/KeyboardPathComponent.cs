using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{  
    public class KeyboardPathComponent : Component
    {
        //public bool isCanControl = true;                                   //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;
        public float moveSpeed = 4.0f;                                     //虚拟 移动速度
        public float roteSpeed = 140.0f;                                   //虚拟 旋转速度

        public Vector3 targetPosition;                                     //实际 移动目标点
        public CancellationTokenSource moveCancellationTokenSource;        //委托 当结束或中途中断时进行移动插值

        public Vector3 targetEulerAngles;                                  //实际 旋转角色
        public CancellationTokenSource turnCancellationTokenSource;        //委托 当结束或中途中断时进行移动插值

        public long resTime = 1000L;

        public bool isStartMove = false;
        public bool isStartOffset = false;
        public long startTimeMove = 0L;
        public long offsetTimeMove = 0L;

        public bool isStartTurn = false;
        public long startTimeTurn = 0L;
        public long offsetTimeTurn = 0L;

    }
}
