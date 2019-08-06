using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class SkillItemAwakeSystem : AwakeSystem<SkillItem>
    {
        public override void Awake(SkillItem self)
        {
            self.Awake();
        }
    }

    public class SkillItem : Entity
    {
        public void Awake()
        {
            this.AddComponent<Skill>();
            this.AddComponent<NumericComponent>();
        }


    }
}
