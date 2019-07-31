using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class UnitPositionComponent : Component
    {
        public float moveSpeed = 4.0f;

        public Vector3 TargetPosition;

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
