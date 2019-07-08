using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{ 
    /// <summary>
    /// 挂在 Unit 个体上
    /// </summary>
    public class AoiUnitComponent : Component
    {
        public long gridId { get; set; } = -1;

        public AoiGrid aoiGrid { get; set; }

        public HashSet<long> NineGridIds { get; set; } = new HashSet<long>();

        public HashSet<long> OldNineGridIds { get; set; } = new HashSet<long>();

        public HashSet<long> Enters { get; set; } = new HashSet<long>();

        public HashSet<long> Leaves { get; set; } = new HashSet<long>();
    }
}