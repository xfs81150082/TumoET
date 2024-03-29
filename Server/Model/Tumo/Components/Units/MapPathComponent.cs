﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class MapPathComponent : Component
    {
        public float moveSpeed = 5.0f;

        public Vector3 Target;

        private ABPathWrap abPath;

        public List<Vector3> Path;

        public CancellationTokenSource CancellationTokenSource;

        public ABPathWrap ABPath
        {
            get
            {
                return this.abPath;
            }
            set
            {
                this.abPath?.Dispose();
                this.abPath = value;
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            this.abPath?.Dispose();
        }

    }
}
