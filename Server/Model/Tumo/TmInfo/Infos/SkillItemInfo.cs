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
            Skill inv11101 = ComponentFactory.CreateWithId<Skill>(11101);
            Skill inv21101 = ComponentFactory.CreateWithId<Skill>(21101);
            Skill inv31101 = ComponentFactory.CreateWithId<Skill>(31101);
            Skill inv41101 = ComponentFactory.CreateWithId<Skill>(41101);

            inv11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            inv11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            skills.Add(inv11101.Id, inv11101);
            skills.Add(inv21101.Id, inv21101);
            skills.Add(inv31101.Id, inv31101);
            skills.Add(inv41101.Id, inv41101);
        }

        void GetSkillItems()
        {
            SkillItem item11101 = ComponentFactory.CreateWithId<SkillItem>(IdGenerater.GenerateId());
            SkillItem item21101 = ComponentFactory.CreateWithId<SkillItem>(IdGenerater.GenerateId());
            SkillItem item31101 = ComponentFactory.CreateWithId<SkillItem>(IdGenerater.GenerateId());
            SkillItem item41101 = ComponentFactory.CreateWithId<SkillItem>(IdGenerater.GenerateId());

            item11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            item11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            item31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            item41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            skillItems.Add(item11101.Id, item11101);
            skillItems.Add(item21101.Id, item21101);
            skillItems.Add(item31101.Id, item31101);
            skillItems.Add(item41101.Id, item41101);
        }


    }
}
