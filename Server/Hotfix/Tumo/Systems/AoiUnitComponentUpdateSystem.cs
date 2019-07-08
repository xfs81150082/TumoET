using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiUnitComponentUpdateSystem : UpdateSystem<AoiUnitComponent>
    {
        public override void Update(AoiUnitComponent self)
        {
            UpdateUnitAoiGrid(self);

        }

        void UpdateUnitAoiGrid(AoiUnitComponent self)
        {
            if (self.GetParent<Unit>().GetComponent<AttackComponent>() != null && !self.GetParent<Unit>().GetComponent<AttackComponent>().isDeath)
            {
                if (GetGridId(self.GetParent<Unit>().Position.x, self.GetParent<Unit>().Position.z) != self.gridId)
                {
                    UpdateRegisterAoiGridId(self);
                    UpdateNineAoiGridIds(self);
                    UpdateEnters(self);
                    UpdateLeaves(self);
                }
            }
        }

        #region
        /// <summary>
        /// 更新 注册在网格上的 位置
        /// </summary>
        /// <param name="self"></param>
        void UpdateRegisterAoiGridId(AoiUnitComponent self)
        {
            if (Game.Scene.GetComponent<AoiGridComponent>().Count > 0)
            {
                long oldid = self.gridId;
                if (Game.Scene.GetComponent<AoiGridComponent>() != null)
                {
                    Game.Scene.GetComponent<AoiGridComponent>().Remove(self);
                    self.gridId = GetGridId(self.GetParent<Unit>().Position.x, self.GetParent<Unit>().Position.z);
                    Game.Scene.GetComponent<AoiGridComponent>().Add(self);
                }
                self.aoiGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(self.gridId);

                Console.WriteLine(" AoiUnitComponentUpdateSystem-53-grideId/players/Count: " + self.gridId + " / " + self.GetParent<Unit>().UnitType + " / " + Game.Scene.GetComponent<AoiGridComponent>().Get(self.gridId).players.Count);
                Console.WriteLine(" AoiUnitComponentUpdateSystem-54-grideId/enemys/Count: " + self.gridId + " / " + self.GetParent<Unit>().UnitType + " / " + Game.Scene.GetComponent<AoiGridComponent>().Get(self.gridId).enemys.Count);
            }
        }
        long GetGridId(float x, float y)
        {
            long id = GetDridX(x) + GetDridY(y) * Game.Scene.GetComponent<AoiGridComponent>().rcCount;
            return id;
        }
        int GetDridX(float x)
        {
            int gX = (int)Math.Floor((x + Game.Scene.GetComponent<AoiGridComponent>().mapWide / 2) / Game.Scene.GetComponent<AoiGridComponent>().gridWide);
            return gX;
        }
        int GetDridY(float y)
        {
            int gY = (int)Math.Floor((y + Game.Scene.GetComponent<AoiGridComponent>().mapWide / 2) / Game.Scene.GetComponent<AoiGridComponent>().gridWide);
            return gY;
        }
      
        /// <summary>
        /// 更新 自已的九宫格 集合
        /// </summary>
        /// <param name="self"></param>
        void UpdateNineAoiGridIds(AoiUnitComponent self)
        {
            self.OldNineGridIds = self.NineGridIds.Select(d => d).ToHashSet();
            self.NineGridIds.Clear();

            long nine;
            AoiGrid nineGrid;

            ///左上
            nine = self.gridId + Game.Scene.GetComponent<AoiGridComponent>().rcCount - 1;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X < self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///上
            nine = self.gridId + Game.Scene.GetComponent<AoiGridComponent>().rcCount;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X == self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///右上
            nine = self.gridId + Game.Scene.GetComponent<AoiGridComponent>().rcCount + 1;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X > self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///左
            nine = self.gridId - 1;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X < self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///中
            self.NineGridIds.Add(self.gridId);

            ///右
            nine = self.gridId + 1;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X > self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///左下
            nine = self.gridId - Game.Scene.GetComponent<AoiGridComponent>().rcCount - 1;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X < self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///下
            nine = self.gridId - Game.Scene.GetComponent<AoiGridComponent>().rcCount;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X == self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }

            ///右下
            nine = self.gridId - Game.Scene.GetComponent<AoiGridComponent>().rcCount + 1;
            nineGrid = Game.Scene.GetComponent<AoiGridComponent>().Get(nine);
            if (nineGrid != null && nineGrid.X > self.aoiGrid.X)
            {
                self.NineGridIds.Add(nine);
            }
        }
        /// <summary>
        /// 本次 加入 九宫格的 格子Id
        /// </summary>
        /// <param name="self"></param>
        void UpdateEnters(AoiUnitComponent self)
        {
            self.Enters = self.NineGridIds.Except(self.OldNineGridIds).ToHashSet();
        }
        /// <summary>
        ///  本次 离开 九宫格的 格子Id
        /// </summary>
        /// <param name="self"></param>
        void UpdateLeaves(AoiUnitComponent self)
        {
            self.Leaves = self.OldNineGridIds.Except(self.NineGridIds).ToHashSet();
        }


        #endregion

    }
}
