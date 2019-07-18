using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class AoiPlayerComponentHelper
    {
        #region
        public static void UpdateAddRemove(this AoiPlayerComponent self)
        {
            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();
            if (aoiUnit.playerIds.Enters.Count > 0)
            {
                //ToTo 通知self客户端(1个)  加入这些玩家（多个）
                self.AddPlayers(aoiUnit.playerIds.Enters.ToArray(), new long[1] { self.GetParent<Unit>().Id });

                Console.WriteLine(" AoiPlayer-20-玩家：" + aoiUnit.playerIds.Enters.Count + " 个，" + " 进入 PlayerId：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 的视野。");
                Console.WriteLine(" AoiPlayer-21-玩家：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 看到玩家：" + aoiUnit.enemyIds.MovesSet.Count);
            }
            if (aoiUnit.enemyIds.Enters.Count > 0)
            {
                //ToTo 通知self客户端(1个)  加入这些小怪（多个）
                self.AddMonsters(aoiUnit.enemyIds.Enters.ToArray(), new long[1] { self.GetParent<Unit>().Id });

                Console.WriteLine(" AoiPlayer-28-小怪：" + aoiUnit.enemyIds.Enters.Count + " 个，" + " 进入 PlayerId：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 的视野。");
                Console.WriteLine(" AoiPlayer-29-玩家：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 看到小怪：" + aoiUnit.enemyIds.MovesSet.Count);
            }
            if (aoiUnit.playerIds.Leaves.Count > 0)
            {
                //ToTo 通知self客户端(1个)  删除这些玩家（多个）
                self.RemovePlayers(aoiUnit.playerIds.Leaves.ToArray(), new long[1] { self.GetParent<Unit>().Id });

                Console.WriteLine(" AoiPlayer-36-玩家：" + aoiUnit.playerIds.Leaves.Count + " 个，" + " 离开 PlayerId：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 的视野。");
                Console.WriteLine(" AoiPlayer-37-玩家：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 看到玩家：" + aoiUnit.playerIds.MovesSet.Count);
            }
            if (aoiUnit.enemyIds.Leaves.Count > 0)
            {
                //ToTo 通知self客户端(1个)  删除这些小怪（多个）
                self.RemoveMonsters(aoiUnit.enemyIds.Leaves.ToArray(), new long[1] { self.GetParent<Unit>().Id });

                Console.WriteLine(" AoiPlayer-44-小怪：" + aoiUnit.enemyIds.Leaves.Count + " 个，" + "离开 PlayerId：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 视野。");
                Console.WriteLine(" AoiPlayer-45-玩家：" + self.GetParent<Unit>().Id + "(" + aoiUnit.gridId + ") 看到小怪：" + aoiUnit.enemyIds.MovesSet.Count);
            }
        }        

        #endregion
        
        #region
        /// <summary>
        /// 给客户端 添加 玩家 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        public static void AddPlayers(this AoiPlayerComponent self, long[] unitIds, long[] playerUnitIds)
        {
            /// 广播创建的unit
            M2M_AddUnits m2M_AddUnits = new M2M_AddUnits() { UnitType = (int)UnitType.Player, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            SessionHelper.MapSession().Send(m2M_AddUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-55-playerId/us/ps: "+self.GetParent<Unit>().Id+" : " + unitIds.Length + " / " + playerUnitIds.Length);
        }

        /// <summary>
        /// 给客户端 添加 小怪 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        public static void AddMonsters(this AoiPlayerComponent self, long[] unitIds, long[] playerUnitIds)
        {
            /// 广播创建的unit
            M2M_AddUnits m2M_AddUnits = new M2M_AddUnits() { UnitType = (int)UnitType.Monster, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            SessionHelper.MapSession().Send(m2M_AddUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-69-playerId/us/ps: " + self.GetParent<Unit>().Id + " : " + unitIds.Length + " / " + playerUnitIds.Length);
        }

        #endregion

        #region
        /// <summary>
        /// 给客户端 删除 玩家 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        public static void RemovePlayers(this AoiPlayerComponent self, long[] unitIds, long[] playerUnitIds)
        {
            /// 广播创建的unit
            M2M_AddUnits m2M_AddUnits = new M2M_AddUnits() { UnitType = (int)UnitType.Player, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            SessionHelper.MapSession().Send(m2M_AddUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-86-playerId/us/ps: " + self.GetParent<Unit>().Id + " : " + unitIds.Length + " / " + playerUnitIds.Length);
        }

        /// <summary>
        /// 给客户端 删除 小怪 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        public static void RemoveMonsters(this AoiPlayerComponent self, long[] unitIds, long[] playerUnitIds)
        {
            /// 广播创建的unit
            M2M_RemoveUnits m2M_RemoveUnits = new M2M_RemoveUnits() { UnitType = (int)UnitType.Monster, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            SessionHelper.MapSession().Send(m2M_RemoveUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-100-playerId/us/ps: " + self.GetParent<Unit>().Id + " : " + unitIds.Length + " / " + playerUnitIds.Length);
        }

        #endregion


    }
}
