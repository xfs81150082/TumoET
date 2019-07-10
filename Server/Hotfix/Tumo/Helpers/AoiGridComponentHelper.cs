using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class AoiGridComponentHelper
    {
        #region
        /// <summary>
        /// 根据坐标 即时 向地图固定格子 里注册 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        public static void AddUnitId(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            AoiGrid aoiGrid = self.Get(aoiUnit.gridId);
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
        public static void RemoveUnitId(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            AoiGrid aoiGrid = self.Get(aoiUnit.gridId);
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
        public static void UpdateAoiUnitInfo(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            // 把新的AOI节点转移到旧的节点里           
            aoiUnit.playerIds.OldMovesSet = aoiUnit.playerIds.MovesSet.Select(d => d).ToHashSet();
            aoiUnit.enemyIds.OldMovesSet = aoiUnit.enemyIds.MovesSet.Select(d => d).ToHashSet();
            aoiUnit.npcerIds.OldMovesSet = aoiUnit.npcerIds.MovesSet.Select(d => d).ToHashSet();

            //// 移动到新的位置
            self.MoveToChangeAoiGrid(aoiUnit);

            // 查找 周围 可见九宫格内单元 unitId 
            self.FindAoi(aoiUnit);

            // 差集计算
            aoiUnit.playerIds.Enters = aoiUnit.playerIds.MovesSet.Except(aoiUnit.playerIds.OldMovesSet).ToHashSet();
            aoiUnit.playerIds.Leaves = aoiUnit.playerIds.OldMovesSet.Except(aoiUnit.playerIds.MovesSet).ToHashSet();
            aoiUnit.enemyIds.Enters = aoiUnit.enemyIds.MovesSet.Except(aoiUnit.enemyIds.OldMovesSet).ToHashSet();
            aoiUnit.enemyIds.Leaves = aoiUnit.enemyIds.OldMovesSet.Except(aoiUnit.enemyIds.MovesSet).ToHashSet();
            aoiUnit.npcerIds.Enters = aoiUnit.npcerIds.MovesSet.Except(aoiUnit.npcerIds.OldMovesSet).ToHashSet();
            aoiUnit.npcerIds.Leaves = aoiUnit.npcerIds.OldMovesSet.Except(aoiUnit.npcerIds.MovesSet).ToHashSet();

            ///将进入自己视野的人 加进来；将自己加入别的人视野
            self.UpdateEnters(aoiUnit);

            ///将离开自己视野的人 删除掉；同时将自己从别人的视野里删除
            self.UpdateLeaves(aoiUnit);
        }

        /// <summary>
        /// 根据坐标 更换地图固定格子 注册注销 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        public static void MoveToChangeAoiGrid(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            long oldid = aoiUnit.gridId;
            self.RemoveUnitId(aoiUnit);
            aoiUnit.gridId = self.GetGridId(aoiUnit);
            self.AddUnitId(aoiUnit);

            Console.WriteLine(" AoiGridComponentHelper-119-Id: " + aoiUnit.GetParent<Unit>().Id + " " + aoiUnit.GetParent<Unit>().UnitType + " " + oldid + " => " + aoiUnit.gridId);
        }
        public static long GetGridId(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            float x = aoiUnit.GetParent<Unit>().Position.x;
            float y = aoiUnit.GetParent<Unit>().Position.z;
            long id = self.GetDridX(x) + self.GetDridY(y) * self.rcCount;
            return id;
        }
        static int GetDridX(this AoiGridComponent self, float x)
        {
            int gX = (int)Math.Floor((x + self.mapWide / 2) / self.gridWide);
            return gX;
        }
        static int GetDridY(this AoiGridComponent self, float y)
        {
            int gY = (int)Math.Floor((y + self.mapWide / 2) / self.gridWide);
            return gY;
        }

        /// <summary>
        /// 得到本人的 九宫格内 最新的 所有单元的 unitId
        /// </summary>
        /// <param name="aoiUnit"></param>
        static void FindAoi(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            aoiUnit.playerIds.MovesSet.Clear();
            aoiUnit.enemyIds.MovesSet.Clear();
            aoiUnit.npcerIds.MovesSet.Clear();

            AoiGrid mygrid = self.Get(aoiUnit.gridId);
            foreach (long gid in mygrid.seeGrids)
            {
                AoiGrid nineGrid = self.Get(gid);
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

        /// <summary>
        /// 将进入自己视野的人 加进来；将自己加入别的人视野
        /// </summary>
        static void UpdateEnters(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    foreach(long tem in aoiUnit.enemyIds.Enters)
                    {
                        ///通知 刚进入本人视野的小怪（多个） 加入本人的Id（1个） 
                        Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().playerIds.MovesSet.Add(aoiUnit.GetParent<Unit>().Id);

                        ///ToTo 通知 本人的客户端(1个)  加入这些小怪（多个）


                        Console.WriteLine(" 小怪-Id：" + tem + " 进入 PlayerId：" + aoiUnit.GetParent<Unit>().Id + "的视野。");
                        Console.WriteLine(" 小怪-Id：" + tem + " 他能看到玩家数量：" + Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().playerIds.MovesSet.Count);
                    }
                    break;
                case UnitType.Monster:
                    foreach (long tem in aoiUnit.playerIds.Enters)
                    {
                        ///通知 刚进入小怪视野的玩家（多个） 加入这个小怪的Id（1个） 
                        Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().enemyIds.MovesSet.Add(aoiUnit.GetParent<Unit>().Id);

                        ///ToTo 通知tem客户端(多个)  加入此小怪（1个）


                        Console.WriteLine(" 小怪+Id：" + aoiUnit.GetParent<Unit>().Id + " 进入 PlayerId：" + tem + "的视野。");
                        Console.WriteLine(" 小怪+Id：" + aoiUnit.GetParent<Unit>().Id + " 他能看到玩家数量：" + aoiUnit.GetParent<Unit>().GetComponent<AoiUnitComponent>().playerIds.MovesSet.Count);
                    }
                    break;
                case UnitType.Npc:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 将离开自己视野的人 删除掉；同时将自己从别人的视野里删除
        /// </summary>
        static void UpdateLeaves(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    foreach (long tem in aoiUnit.enemyIds.Enters)
                    {
                        Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().playerIds.MovesSet.Remove(aoiUnit.GetParent<Unit>().Id);
                        ///ToTo 通知aoiunit客户端(1个)  删除 这些小怪（多个）
                        
                        Console.WriteLine(" 小怪-Id：" + tem + " 离开 PlayerId：" + aoiUnit.GetParent<Unit>().Id + "的视野。");
                    }
                    break;
                case UnitType.Monster:
                    foreach (long tem in aoiUnit.playerIds.Leaves)
                    {
                        Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().enemyIds.MovesSet.Remove(aoiUnit.GetParent<Unit>().Id);
                        ///ToTo 通知tem客户端(多个)  删除 此小怪（1个）
                        
                        Console.WriteLine(" 小怪+Id：" + aoiUnit.GetParent<Unit>().Id + " 离开 PlayerId：" + tem + "的视野。");
                    }
                    break;
                case UnitType.Npc:

                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
