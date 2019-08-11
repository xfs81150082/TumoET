using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
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
            Skill skill = ComponentFactory.CreateWithId<Skill>(this.Id);
            this.AddComponent(skill);
            this.AddComponent<ChangeType>();
            this.AddComponent<NumericComponent>();
        }

    }
}
