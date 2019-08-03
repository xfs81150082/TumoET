using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class UnitAnglesComponent : Component
    {
        public float trunSpeed = 14.0f;

        public Vector3 EulerAnglesTarget;

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
