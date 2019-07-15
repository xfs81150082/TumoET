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
        /// <summary>
        /// 给客户端 添加 玩家 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        public static void AddPlayers(this AoiPlayerComponent self, long[] unitIds, long[] playerUnitIds)
        {
            /// 广播创建的unit
            M2M_AddUnits m2M_AddUnits = new M2M_AddUnits() { UnitType = (int)UnitType.Player, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            MapSessionHelper.Session().Send(m2M_AddUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-23-playerId/us/ps: "+self.GetParent<Unit>().Id+" : " + unitIds.Length + " / " + playerUnitIds.Length);
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
            MapSessionHelper.Session().Send(m2M_AddUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-23-playerId/us/ps: " + self.GetParent<Unit>().Id + " : " + unitIds.Length + " / " + playerUnitIds.Length);
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
            MapSessionHelper.Session().Send(m2M_AddUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-23-playerId/us/ps: " + self.GetParent<Unit>().Id + " : " + unitIds.Length + " / " + playerUnitIds.Length);
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
            MapSessionHelper.Session().Send(m2M_RemoveUnits);

            Console.WriteLine(" AoiPlayerComponentHelper-23-playerId/us/ps: " + self.GetParent<Unit>().Id + " : " + unitIds.Length + " / " + playerUnitIds.Length);
        }


        #endregion




    }
}
