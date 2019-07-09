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

        public int rcCount = 5;

        public int gridWide = 20;

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

        #region
        /// <summary>
        /// 根据坐标 即时 向地图固定格子 里注册 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        public void AddUnitId(AoiUnitComponent aoiUnit)
        {
            grids.TryGetValue(aoiUnit.gridId, out AoiGrid aoiGrid);
            if (aoiGrid != null)
            {
                UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        if (!aoiGrid.players.Contains(aoiUnit.GetParent<Unit>().Id))
                        {
                            aoiGrid.players.Add(aoiUnit.GetParent<Unit>().Id);
                        }
                        break;
                    case UnitType.Monster:
                        if (!aoiGrid.enemys.Contains(aoiUnit.GetParent<Unit>().Id))
                        {
                            aoiGrid.enemys.Add(aoiUnit.GetParent<Unit>().Id);
                        }
                        break;
                    case UnitType.Npc:
                        if (!aoiGrid.npcers.Contains(aoiUnit.GetParent<Unit>().Id))
                        {
                            aoiGrid.npcers.Add(aoiUnit.GetParent<Unit>().Id);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 根据坐标 即时 向地图固定格子 里删除 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        public void RemoveUnitId(AoiUnitComponent aoiUnit)
        {
            grids.TryGetValue(aoiUnit.gridId, out AoiGrid aoiGrid);
            if (aoiGrid != null)
            {
                UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        if (aoiGrid.players.Contains(aoiUnit.GetParent<Unit>().Id))
                        {
                            aoiGrid.players.Remove(aoiUnit.GetParent<Unit>().Id);
                        }
                        break;
                    case UnitType.Monster:
                        if (aoiGrid.enemys.Contains(aoiUnit.GetParent<Unit>().Id))
                        {
                            aoiGrid.enemys.Remove(aoiUnit.GetParent<Unit>().Id);
                        }
                        break;
                    case UnitType.Npc:
                        if (aoiGrid.npcers.Contains(aoiUnit.GetParent<Unit>().Id))
                        {
                            aoiGrid.npcers.Remove(aoiUnit.GetParent<Unit>().Id);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 更新 AoiUnitInfo ，当换格时
        /// </summary>
        /// <param name="aoiUnit"></param>
        public void UpdateAoiUnitInfo(AoiUnitComponent aoiUnit)
        {
            // 把新的AOI节点转移到旧的节点里           
            aoiUnit.playerIds.OldMovesSet = aoiUnit.playerIds.MovesSet.Select(d => d).ToHashSet();
            aoiUnit.enemyIds.OldMovesSet = aoiUnit.enemyIds.MovesSet.Select(d => d).ToHashSet();
            aoiUnit.npcerIds.OldMovesSet = aoiUnit.npcerIds.MovesSet.Select(d => d).ToHashSet();

            //// 移动到新的位置
            //MoveToChangeAoiGrid(aoiUnit);

            // 查找 周围 可见九宫格内单元 unitId 
            FindAoi(aoiUnit);

            // 差集计算
            aoiUnit.playerIds.Enters = aoiUnit.playerIds.MovesSet.Except(aoiUnit.playerIds.OldMovesSet).ToHashSet();
            aoiUnit.playerIds.Leaves = aoiUnit.playerIds.OldMovesSet.Except(aoiUnit.playerIds.MovesSet).ToHashSet();

            aoiUnit.enemyIds.Enters = aoiUnit.enemyIds.MovesSet.Except(aoiUnit.enemyIds.OldMovesSet).ToHashSet();
            aoiUnit.enemyIds.Leaves = aoiUnit.enemyIds.OldMovesSet.Except(aoiUnit.enemyIds.MovesSet).ToHashSet();

            aoiUnit.npcerIds.Enters = aoiUnit.npcerIds.MovesSet.Except(aoiUnit.npcerIds.OldMovesSet).ToHashSet();
            aoiUnit.npcerIds.Leaves = aoiUnit.npcerIds.OldMovesSet.Except(aoiUnit.npcerIds.MovesSet).ToHashSet();
        }

        /// <summary>
        /// 根据坐标 更换地图固定格子 注册注销 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        public void MoveToChangeAoiGrid(AoiUnitComponent aoiUnit)
        {
            long oldid = aoiUnit.gridId;
            RemoveUnitId(aoiUnit);
            aoiUnit.gridId = GetGridId(aoiUnit);
            AddUnitId(aoiUnit);

            Console.WriteLine(" AoiGridComponent-169-oldid/gridId: " + oldid + " => " + aoiUnit.gridId);
        }
        public long GetGridId(AoiUnitComponent aoiUnit)
        {
            float x = aoiUnit.GetParent<Unit>().Position.x;
            float y = aoiUnit.GetParent<Unit>().Position.z;
            long id = GetDridX(x) + GetDridY(y) * rcCount;
            return id;
        }
        int GetDridX(float x)
        {
            int gX = (int)Math.Floor((x + mapWide / 2) / gridWide);
            return gX;
        }
        int GetDridY(float y)
        {
            int gY = (int)Math.Floor((y + mapWide / 2) / gridWide);
            return gY;
        }

        /// <summary>
        /// 得到本人的 九宫格内 最新的 所有单元的 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        void FindAoi(AoiUnitComponent aoiUnit)
        {
            aoiUnit.playerIds.MovesSet.Clear();
            aoiUnit.enemyIds.MovesSet.Clear();
            aoiUnit.npcerIds.MovesSet.Clear();

            AoiGrid mygrid = Get(aoiUnit.gridId);
            foreach (long gid in mygrid.seeGrids)
            {
                AoiGrid nineGrid = Get(gid);
                if (nineGrid.players.Count > 0)
                {
                    foreach (long unitId1 in nineGrid.players)
                    {
                        aoiUnit.playerIds.MovesSet.Add(unitId1);
                    }
                }
                if (nineGrid.enemys.Count > 0)
                {
                    foreach (long unitId2 in nineGrid.enemys)
                    {
                        aoiUnit.enemyIds.MovesSet.Add(unitId2);
                    }
                }
                if (nineGrid.npcers.Count > 0)
                {
                    foreach (long unitId3 in nineGrid.npcers)
                    {
                        aoiUnit.npcerIds.MovesSet.Add(unitId3);
                    }
                }
            }
        }
        #endregion
        
    }
}