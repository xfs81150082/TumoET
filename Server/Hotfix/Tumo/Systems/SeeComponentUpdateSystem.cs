using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class SeeComponentUpdateSystem : UpdateSystem<SeeComponent>
    {
        public override void Update(SeeComponent self)
        {
            self.UpdateSee();
        }


    }
}
