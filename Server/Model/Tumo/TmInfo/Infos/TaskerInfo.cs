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

        }
        void GetSkillItems()
        {

        }

    }
}
