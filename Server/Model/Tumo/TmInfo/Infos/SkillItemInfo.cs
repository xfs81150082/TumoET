using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    public class SkillItemInfo : Component
    {
        public readonly Dictionary<long, Skill> idSkills = new Dictionary<long, Skill>();

        public SkillItemInfo()
        {
            GetSkills();
        }

        void GetSkills()
        {
            Skill inv11101 = ComponentFactory.CreateWithId<Skill>(11101);
            Skill inv21101 = ComponentFactory.CreateWithId<Skill>(21101);
            Skill inv31101 = ComponentFactory.CreateWithId<Skill>(31101);
            Skill inv41101 = ComponentFactory.CreateWithId<Skill>(41101);

            //inv11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            inv11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            //inv31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            idSkills.Add(inv11101.Id, inv11101);
            idSkills.Add(inv21101.Id, inv21101);
            idSkills.Add(inv31101.Id, inv31101);
            idSkills.Add(inv41101.Id, inv41101);
        }

        public int Count
        {
            get { return idSkills.Count; }
        }

        public Skill[] GetAll()
        {
            return idSkills.Values.ToArray();
        }


    }
}
