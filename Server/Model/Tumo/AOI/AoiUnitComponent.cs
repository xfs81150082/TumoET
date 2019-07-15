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

        public long changerGridId { get; set; } = -1;

        public AoiUintInfo playerIds;

        public AoiUintInfo enemyIds;

        public AoiUintInfo npcerIds;

        public AoiUnitComponent()
        {
            playerIds = new AoiUintInfo { MovesSet = new HashSet<long>(), OldMovesSet = new HashSet<long>(), Enters = new HashSet<long>(), Leaves = new HashSet<long>() };
            enemyIds = new AoiUintInfo { MovesSet = new HashSet<long>(), OldMovesSet = new HashSet<long>(), Enters = new HashSet<long>(), Leaves = new HashSet<long>() };
            npcerIds = new AoiUintInfo { MovesSet = new HashSet<long>(), OldMovesSet = new HashSet<long>(), Enters = new HashSet<long>(), Leaves = new HashSet<long>() };
        }      

        public override void Dispose()
        {
            if (this.IsDisposed) return;

            playerIds.Dispose();
            enemyIds.Dispose();
            npcerIds.Dispose();

            gridId = 0;

            base.Dispose();
        }

    }
}
