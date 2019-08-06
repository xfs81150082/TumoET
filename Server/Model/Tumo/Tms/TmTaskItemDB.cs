using System;

namespace ETModel
{
    public class TmTaskItemDB : Component
    {
        public int Id { get; set; }
        public int RolerId { get; set; }
        public int TmTaskId { get; set; }
        public TaskState TaskState { get; set; }
        public string UpdateTime { get; set; }
    }
}
