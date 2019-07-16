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
                    case UnitType.Npcer:
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
                    case UnitType.Npcer:
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
  
            ///20190715
            AoiPlayerComponent aoiPlayer = aoiUnit.GetParent<Unit>().GetComponent<AoiPlayerComponent>();
            if (aoiPlayer != null)
            {
                aoiPlayer.UpdateAddRemove();
            } 
            
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
            foreach (long tem in aoiUnit.playerIds.Enters)
            {
                ///通知 刚进入小怪视野的玩家（多个） 加入这个小怪的Id（1个） 
                AoiPlayerComponent temAoiPlayer1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiPlayerComponent>();
                AoiUnitComponent temAoiUnit1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit1.playerIds.Enters.Add(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit1.playerIds.MovesSet.Add(aoiUnit.GetParent<Unit>().Id);

                        //ToTo 通知tem客户端(多个)  加入此玩家（1个）
                        temAoiPlayer1.AddPlayers(new long[1] { aoiUnit.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiGrid-208-玩家-Id：" + aoiUnit.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 进入 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiGrid-209-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到玩家：" + temAoiUnit1.playerIds.MovesSet.Count);
                        break;
                    case UnitType.Monster:
                        temAoiUnit1.enemyIds.Enters.Add(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit1.enemyIds.MovesSet.Add(aoiUnit.GetParent<Unit>().Id);
                        
                        //ToTo 通知tem客户端(多个)  加入此小怪（1个）
                        temAoiPlayer1.AddMonsters(new long[1] { aoiUnit.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiGrid-218-小怪-Id：" + aoiUnit.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 进入 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiGrid-219-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到小怪：" + temAoiUnit1.enemyIds.MovesSet.Count);
                        break;
                }
            }

            foreach (long tem in aoiUnit.enemyIds.Enters)
            {
                ///通知 刚进入本人视野的小怪（多个） 加入本人的Id（1个） 
                AoiUnitComponent temAoiUnit0 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit0.playerIds.Enters.Add(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit0.playerIds.MovesSet.Add(aoiUnit.GetParent<Unit>().Id);
                        break;
                    case UnitType.Monster:
                        temAoiUnit0.enemyIds.Enters.Add(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit0.enemyIds.MovesSet.Add(aoiUnit.GetParent<Unit>().Id);
                        break;
                }
            }        
                   
        }

        /// <summary>
        /// 将离开自己视野的人 删除掉；同时将自己从别人的视野里删除
        /// </summary>
        static void UpdateLeaves(this AoiGridComponent self, AoiUnitComponent aoiUnit)
        {
            foreach (long tem in aoiUnit.playerIds.Leaves)
            {
                ///通知 刚进入小怪视野的玩家（多个） 加入这个小怪的Id（1个） 
                AoiPlayerComponent temAoiPlayer1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiPlayerComponent>();
                AoiUnitComponent temAoiUnit1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit1.playerIds.MovesSet.Remove(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit1.playerIds.Leaves.Add(aoiUnit.GetParent<Unit>().Id);

                        ///ToTo 通知tem客户端(多个) 删除此玩家（1个）
                        temAoiPlayer1.RemovePlayers(new long[1] { aoiUnit.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiGrid-264-玩家-Id：" + aoiUnit.GetParent<Unit>().Id + " 离开 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiGrid-265-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到玩家：" + temAoiUnit1.playerIds.MovesSet.Count);
                        break;
                    case UnitType.Monster:
                        temAoiUnit1.enemyIds.MovesSet.Remove(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit1.enemyIds.Leaves.Add(aoiUnit.GetParent<Unit>().Id);

                        ///ToTo 通知tem客户端(多个) 删除此小怪（1个）
                        temAoiPlayer1.RemoveMonsters(new long[1] { aoiUnit.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiGrid-274-小怪-Id：" + aoiUnit.GetParent<Unit>().Id + " 离开 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiGrid-275-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到小怪：" + temAoiUnit1.enemyIds.MovesSet.Count);
                        break;
                }
            }

            foreach (long tem in aoiUnit.enemyIds.Enters)
            {
                ///通知 刚进入本人视野的小怪（多个） 加入本人的Id（1个） 
                AoiUnitComponent temAoiUnit0 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = aoiUnit.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit0.playerIds.MovesSet.Remove(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit0.playerIds.Leaves.Add(aoiUnit.GetParent<Unit>().Id);
                        break;
                    case UnitType.Monster:
                        temAoiUnit0.enemyIds.MovesSet.Remove(aoiUnit.GetParent<Unit>().Id);
                        temAoiUnit0.enemyIds.Leaves.Add(aoiUnit.GetParent<Unit>().Id);
                        break;
                }
            }
            
        }

        #endregion


    }
}
