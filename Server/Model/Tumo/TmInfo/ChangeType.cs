using System;

namespace ETModel
{
    public class ChangeType : Component
    {
        public int Exp { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int Coin { get; set; } = 0;
        public int Diamond { get; set; } = 0;
        public int Place { get; set; } = 0;
        public int Count { get ; set ; }
        public int Durability { get; set; }
        public int changeCount = -1;
        public TaskState TaskState { get; set; }
    }
}
