using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class TaskerInfo : Component
    {
        public Dictionary<long, Tasker> idTaskers = new Dictionary<long, Tasker>();
        public Dictionary<long, TaskerItem> idTaskerItems = new Dictionary<long, TaskerItem>();

        public TaskerInfo()
        {
            GetSkills();
            GetSkillItems();
        }

        void GetSkills()
        {
            Tasker inv11101 = ComponentFactory.CreateWithId<Tasker>(11101);
            Tasker inv21101 = ComponentFactory.CreateWithId<Tasker>(21101);
            Tasker inv31101 = ComponentFactory.CreateWithId<Tasker>(31101);
            Tasker inv41101 = ComponentFactory.CreateWithId<Tasker>(41101);

            inv11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            inv11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            idTaskers.Add(inv11101.Id, inv11101);
            idTaskers.Add(inv21101.Id, inv21101);
            idTaskers.Add(inv31101.Id, inv31101);
            idTaskers.Add(inv41101.Id, inv41101);
        }

        void GetSkillItems()
        {
            TaskerItem item11101 = ComponentFactory.CreateWithId<TaskerItem>(IdGenerater.GenerateId());
            TaskerItem item21101 = ComponentFactory.CreateWithId<TaskerItem>(IdGenerater.GenerateId());
            TaskerItem item31101 = ComponentFactory.CreateWithId<TaskerItem>(IdGenerater.GenerateId());
            TaskerItem item41101 = ComponentFactory.CreateWithId<TaskerItem>(IdGenerater.GenerateId());

            item11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            item11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            item31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            item41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            idTaskerItems.Add(item11101.Id, item11101);
            idTaskerItems.Add(item21101.Id, item21101);
            idTaskerItems.Add(item31101.Id, item31101);
            idTaskerItems.Add(item41101.Id, item41101);
        }


    }
}
