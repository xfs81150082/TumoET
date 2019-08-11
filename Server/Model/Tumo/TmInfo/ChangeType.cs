using System;
using System.Collections.Generic;

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
        public long CastId = -1;
        public HashSet<long> TargetIds = new HashSet<long>();
        public int changeCount = -1;
        public TaskState TaskState { get; set; }
    }
}
