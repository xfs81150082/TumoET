using System;

namespace ETModel
{
    public class TaskerDB : Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RolerId { get; set; }
        public int TaskId { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public TaskState TaskState { get; set; }
        public string UpdateTime { get; set; }
    }
}
