using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiGridComponentAwakeSystem : AwakeSystem<AoiGridComponent>
    {
        public override void Awake(AoiGridComponent self)
        {
            CreateAoiGridDict(self);

            GetNineAoiGridIds(self);
        }

        /// <summary>
        /// 创建 格子集合（元素是固定不变的。变化的是元素里面的参数）
        /// </summary>
        /// <param name="self"></param>
        void CreateAoiGridDict(AoiGridComponent self)
        {
            for (int y = 0; y < self.rcCount; y++)
            {
                for (int x = 0; x < self.rcCount; x++)
                {
                    AoiGrid grid = new AoiGrid((x + y * self.rcCount), x, y);
                    grid.minX = x * self.gridWide;
                    grid.maxX = (x + 1) * self.gridWide;
                    grid.minY = y * self.gridWide;
                    grid.maxY = (y + 1) * self.gridWide;
                    self.Add(grid);

                    grid.Parent = self;
                }
            }
            Console.WriteLine(" AoiGridComponentAwakeSystem-40-grids: " + self.Count);
        }

        /// <summary>
        /// 找到每个格子，四周可见的格子（九宫格）。
        /// </summary>
        /// <param name="self"></param>
        void GetNineAoiGridIds(AoiGridComponent self)
        {
            AoiGrid[] aoiGrids = self.GetAll();
            foreach (AoiGrid tem in aoiGrids)
            {
                tem.seeGrids = new HashSet<long>();
                long nine;
                AoiGrid nineGrid;
                ///左上
                nine = tem.gridId + self.rcCount - 1;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X < tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///上
                nine = tem.gridId + self.rcCount;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X == tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///右上
                nine = tem.gridId + self.rcCount + 1;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X > tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///左
                nine = tem.gridId - 1;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X < tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///中
                tem.seeGrids.Add(tem.gridId);
                ///右
                nine = tem.gridId + 1;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X > tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///左下
                nine = tem.gridId - self.rcCount - 1;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X < tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///下
                nine = tem.gridId - self.rcCount;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X == tem.X)
                {
                    tem.seeGrids.Add(nine);
                }
                ///右下
                nine = tem.gridId - self.rcCount + 1;
                nineGrid = self.Get(nine);
                if (nineGrid != null && nineGrid.X > tem.X)
                {
                    tem.seeGrids.Add(nine);
                }

                Console.WriteLine(" AoiGridComponentAwakeSystem-122-seeGrids: " + tem.gridId + " / " + tem.seeGrids.Count);
            }
        }


    }
}