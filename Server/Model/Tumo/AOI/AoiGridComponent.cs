using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class AoiGridComponentAwakeSystem : AwakeSystem<AoiGridComponent>
    {
        public override void Awake(AoiGridComponent self)
        {
            self.Awake();

            Console.WriteLine(" AoiGridComponentAwakeSystem-15-count: " + self.Count);
        }
    }

    /// <summary>
    /// 挂在 Sense场景上 ， Update更新格子内容
    /// </summary>
    public class AoiGridComponent : Component
    {
        private readonly Dictionary<long, AoiGrid> grids = new Dictionary<long, AoiGrid>();

        public int rcCount = 5;

        public int gridWide = 20;

        public int mapWide = 100;

        public void Awake()
        {
            for (int y = 0; y < rcCount; y++)
            {
                for (int x = 0; x < rcCount; x++)
                {
                    AoiGrid grid = new AoiGrid((x + y * rcCount), x, y);
                    grid.minX = x * gridWide;
                    grid.maxX = (x + 1) * gridWide;
                    grid.minY = y * gridWide;
                    grid.maxY = (y + 1) * gridWide;
                    grids.Add(grid.gridId, grid);
                }
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

        public void Add(AoiUnitComponent aoiUnit)
        {
            bool yes = grids.TryGetValue(aoiUnit.gridId, out AoiGrid aoiGrid);
            if (yes)
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
        public void Remove(AoiUnitComponent aoiUnit)
        {
            bool yes = grids.TryGetValue(aoiUnit.gridId, out AoiGrid aoiGrid);
            if (yes)
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
        public int Count
        {
            get
            {
                return this.grids.Count;
            }
        }
        public AoiGrid Get(long gridId)
        {
            return grids.TryGetValue(gridId, out var aoiGrid) ? aoiGrid : null;
        }
        public AoiGrid[] GetAll()
        {
            return this.grids.Values.ToArray();
        }

        /// <summary>
        /// 得到 九宫格内 实例 Player Enemy Npcer
        /// </summary>
        /// <param name="aoiGrids"></param>
        /// <returns></returns>
        public Unit[] GetPlayerUnits(long[] aoiGridIds)
        {
            HashSet<Unit> units = new HashSet<Unit>();
            HashSet<long> unitIds = new HashSet<long>();
            foreach (long temid in aoiGridIds)
            {
                if (Get(temid).players.Count > 0)
                {
                    foreach (long temlong in Get(temid).players)
                    {
                        unitIds.Add(temlong);
                    }
                }
            }
            if (unitIds.Count > 0)
            {
                foreach (long id in unitIds)
                {
                    Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(id);
                    if (unit != null && unit.GetComponent<AttackComponent>() != null && !unit.GetComponent<AttackComponent>().isDeath)
                    {
                        units.Add(unit);
                    }
                }
            }
            return units.ToArray();
        }
        public Unit[] GetMonsterUnits(long[] aoiGrids)
        {
            HashSet<Unit> units = new HashSet<Unit>();
            HashSet<long> unitIds = new HashSet<long>();
            foreach (long temid in aoiGrids)
            {
                if (Get(temid).enemys.Count > 0)
                {
                    foreach (long temlong in Get(temid).enemys)
                    {
                        unitIds.Add(temlong);
                    }
                }
            }
            //Console.WriteLine(" AoiGridComponent-180-unitIds: " + unitIds.Count);
            if (unitIds.Count > 0)
            {
                foreach (long id in unitIds)
                {
                    Unit unit = Game.Scene.GetComponent<EnemyUnitComponent>().Get(id);
                    if (unit != null && unit.GetComponent<AttackComponent>() != null && !unit.GetComponent<AttackComponent>().isDeath)
                    {
                        units.Add(unit);
                    }
                }
                //Console.WriteLine(" AoiGridComponent-187-units: " + units.Count);
            }
            return units.ToArray();
        }
        public Unit[] GetNpcerUnits(long[] aoiGrids)
        {
            HashSet<Unit> units = new HashSet<Unit>();
            HashSet<long> unitIds = new HashSet<long>();
            foreach (long temid in aoiGrids)
            {
                if (Get(temid).npcers.Count > 0)
                {
                    foreach (long temlong in Get(temid).npcers)
                    {
                        unitIds.Add(temlong);
                    }
                }
            }
            if (unitIds.Count > 0)
            {
                foreach (long id in unitIds)
                {
                    Unit unit = Game.Scene.GetComponent<NpcerUnitComponent>().Get(id);
                    if (unit != null && unit.GetComponent<AttackComponent>() != null && !unit.GetComponent<AttackComponent>().isDeath)
                    {
                        units.Add(unit);
                    }
                }
            }
            return units.ToArray();
        }

        public long[] GetPlayerIds(long[] aoiGridIds)
        {
            HashSet<long> unitIds = new HashSet<long>();
            foreach (long temid in aoiGridIds)
            {
                unitIds.Union(Get(temid).players);
            }
            return unitIds.ToArray();
        }
        public long[] GetMonsterIds(long[] aoiGrids)
        {
            HashSet<long> unitIds = new HashSet<long>();
            foreach (long temid in aoiGrids)
            {
                unitIds.Union(Get(temid).enemys);
            }
            return unitIds.ToArray();
        }
        public long[] GetNpcerIds(long[] aoiGrids)
        {
            HashSet<long> unitIds = new HashSet<long>();
            foreach (long temid in aoiGrids)
            {
                unitIds.Union(Get(temid).npcers);
            }
            return unitIds.ToArray();
        }

    }
}