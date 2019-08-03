using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{ 
    [ObjectSystem]
    public class KeyboardPathComponentUpdateSystem : UpdateSystem<KeyboardPathComponent>
    {
        public override void Update(KeyboardPathComponent self)
        {
            self.FramevhToMapServer();
        }
    }

    public class KeyboardPathComponent : Component
    {
        public float h = 0;
        public float v = 0;
        public float w = 0;

        public bool isZero = true;

        public readonly C2M_KeyboardPathResult c2M_PathKeyboardResult = new C2M_KeyboardPathResult();    

    }
}
