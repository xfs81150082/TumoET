using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class SkillItemInfo : Component
    {
        public Dictionary<long, Skill> skills = new Dictionary<long, Skill>();
        public Dictionary<long, SkillItem> skillItems = new Dictionary<long, SkillItem>();

        public SkillItemInfo()
        {
            GetSkills();
            GetSkillItems();
        }

        void GetSkills()
        {

        }
        void GetSkillItems()
        {

        }


    }
}
