using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class UnitSkillComponentHelper
    {
        public static void Cast(this UnitSkillComponent self, string keycode)
        {
            RayUnitComponent ray = self.GetParent<Unit>().GetComponent<RayUnitComponent>();
            AttackComponent attack = self.GetParent<Unit>().GetComponent<AttackComponent>();

            self.currentKey = keycode;
            self.keycodeIds.TryGetValue(self.currentKey, out long skid);
            if (skid == 0)
            {
                skid = 41101;
            }
            Skill skill = Game.Scene.GetComponent<SkillComponent>().Get(skid);
            self.curSkillItem = ComponentFactory.CreateWithId<SkillItem>(skid);
            self.curSkillItem.UpdateLevel(10);
            
            if (ray.target != null)
            {
                attack.target = ray.target;
            }

            if (attack.target != null)
            {
                attack.target.GetComponent<AttackComponent>().TakeDamage(self.curSkillItem);
            }

        }



    }
}
