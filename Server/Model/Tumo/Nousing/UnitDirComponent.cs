using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class UnitDirComponent : Component
    {
        //public Vector3 clientPos;    //客户端坐标
        //public Vector3 dirPos;       //向量坐标

        public Vector3 Target;    //结果坐标

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
