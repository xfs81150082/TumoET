using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class PatrolComponentUpdateSystem : UpdateSystem<PatrolComponent>
    {
        public override void Update(PatrolComponent self)
        {
            if (!self.isPatrol) return;
           self.UpdatePatrol();
        }
        
        
    }
}
