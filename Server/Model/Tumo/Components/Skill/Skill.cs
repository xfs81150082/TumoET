using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class SkillAwakeSystem : AwakeSystem<Skill>
    {
        public override void Awake(Skill self)
        {
            self.Awake();
        }
    }

    public class Skill : Entity
    {
        public void Awake()
        {
            this.AddComponent<NumericComponent>();
        }


    }
}
