using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{  
    public sealed class Enemy : Entity
    {
        public long UnitId { get; set; }

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
