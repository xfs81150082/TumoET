using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class KeyboardSkillComponentUpdateSystem : UpdateSystem<KeyboardSkillComponent>
    {
        public override void Update(KeyboardSkillComponent self)
        {
            self.SkillKeyboardToMapServer();
        }
    }

    public class KeyboardSkillComponent : Component
    {
        public float cdTime = 2.0f;
        public float cd = 2.0f;
        public KeyCode currentKey;
        public bool isSend = false;

        //public readonly C2M_KeyboardSkillResult c2M_KeyboardSkillResult = new C2M_KeyboardSkillResult();
        public readonly C2M_KeyboardSkillRequest c2M_KeyboardSkillRequest = new C2M_KeyboardSkillRequest();
        public readonly M2C_KeyboardSkillResponse m2C_KeyboardSkillResponse = new M2C_KeyboardSkillResponse();

        public override void Dispose()
        {
            base.Dispose();
        }


    }
}
