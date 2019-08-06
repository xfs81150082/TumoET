using System;

namespace ETModel
{
    public class TmTaskItem : Entity
    {
        public TmTaskItem()
        {
            AddComponent(new TmName());
            AddComponent(new TmTask());
            AddComponent(new TmTaskItemDB());
        }
        public TmTaskItemDB TmTaskItemDB { get; set; }
        public int TmTaskItemId { get; set; }
        public int Rolerid { get; set; }
        public TmTask TmTask { get; set; }
        public TaskState TaskState { get; set; }
        public string UpdateTime { get; set; }      
    }
}
