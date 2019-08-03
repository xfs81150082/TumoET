using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class UnitDirComponent : Component
    {
        public Vector3 dirPos;       //向量坐标

        public Vector3 TargetPosition;  //目标坐标

        public CancellationTokenSource CancellationTokenSource;

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();
        }
    }
}
