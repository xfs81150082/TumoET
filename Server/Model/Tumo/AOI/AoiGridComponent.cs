using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{ 
    /// <summary>
    /// 挂在 Sense场景上，Update更新AOI格子和单元内容
    /// </summary>
    public class AoiGridComponent : Component
    {
        private readonly Dictionary<long, AoiGrid> grids = new Dictionary<long, AoiGrid>();

        public int rcCount = 10;

        public int gridWide = 10;

        public int mapWide = 100;

        #region
        public void Add(AoiGrid aoiGrid)
        {
            grids.Add(aoiGrid.gridId, aoiGrid);
        }

        public AoiGrid Get(long gridId)
        {
            return grids.TryGetValue(gridId, out var aoiGrid) ? aoiGrid : null;
        }

        public AoiGrid[] GetAll()
        {
            return this.grids.Values.ToArray();
        }

        public int Count
        {
            get
            {
                return this.grids.Count;
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (AoiGrid aoiGrid in this.grids.Values)
            {
                aoiGrid.Dispose();
            }
            this.grids.Clear();
        }
        #endregion
        
    }
}