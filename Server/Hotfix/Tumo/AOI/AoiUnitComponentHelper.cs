using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    public static class AoiUnitComponentHelper
    {
        /// <summary>
        /// 将进入自己视野的人 加进来；将自己加入别的人视野
        /// </summary>
        public static void UpdateEnters(this AoiUnitComponent self)
        {
            foreach (long tem in self.playerIds.Enters)
            {
                ///通知 刚进入小怪视野的玩家（多个） 加入这个小怪的Id（1个） 
                AoiUnitComponent temAoiUnit1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                AoiPlayerComponent temAoiPlayer1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiPlayerComponent>();
                UnitType unitType = self.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit1.playerIds.Enters.Add(self.GetParent<Unit>().Id);
                        temAoiUnit1.playerIds.MovesSet.Add(self.GetParent<Unit>().Id);

                        //ToTo 通知tem客户端(多个)  加入此玩家（1个）
                        temAoiPlayer1.AddPlayers(new long[1] { self.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiUnit-31-玩家-Id：" + self.GetParent<Unit>().Id + "(" + self.gridId + ") 进入 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiUnit-32-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到玩家：" + temAoiUnit1.playerIds.MovesSet.Count);
                        break;
                    case UnitType.Monster:
                        temAoiUnit1.enemyIds.Enters.Add(self.GetParent<Unit>().Id);
                        temAoiUnit1.enemyIds.MovesSet.Add(self.GetParent<Unit>().Id);

                        //ToTo 通知tem客户端(多个)  加入此小怪（1个）
                        temAoiPlayer1.AddMonsters(new long[1] { self.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiUnit-41-小怪-Id：" + self.GetParent<Unit>().Id + "(" + self.gridId + ") 进入 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiUnit-42-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到小怪：" + temAoiUnit1.enemyIds.MovesSet.Count);
                        break;
                }
            }

            foreach (long tem in self.enemyIds.Enters)
            {
                ///通知 刚进入本人视野的小怪（多个） 加入本人的Id（1个） 
                AoiUnitComponent temAoiUnit0 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = self.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit0.playerIds.Enters.Add(self.GetParent<Unit>().Id);
                        temAoiUnit0.playerIds.MovesSet.Add(self.GetParent<Unit>().Id);
                        break;
                    case UnitType.Monster:
                        temAoiUnit0.enemyIds.Enters.Add(self.GetParent<Unit>().Id);
                        temAoiUnit0.enemyIds.MovesSet.Add(self.GetParent<Unit>().Id);
                        break;
                }
            }
        }

        /// <summary>
        /// 将离开自己视野的人 删除掉；同时将自己从别人的视野里删除
        /// </summary>
        public static void UpdateLeaves(this AoiUnitComponent self)
        {
            foreach (long tem in self.playerIds.Leaves)
            {
                ///通知 刚进入小怪视野的玩家（多个） 加入这个小怪的Id（1个） 
                AoiPlayerComponent temAoiPlayer1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiPlayerComponent>();
                AoiUnitComponent temAoiUnit1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = self.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit1.playerIds.MovesSet.Remove(self.GetParent<Unit>().Id);
                        temAoiUnit1.playerIds.Leaves.Add(self.GetParent<Unit>().Id);

                        ///ToTo 通知tem客户端(多个) 删除此玩家（1个）
                        temAoiPlayer1.RemovePlayers(new long[1] { self.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiUnit-86-玩家-Id：" + self.GetParent<Unit>().Id + " 离开 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiUnit-87-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到玩家：" + temAoiUnit1.playerIds.MovesSet.Count);
                        break;
                    case UnitType.Monster:
                        temAoiUnit1.enemyIds.MovesSet.Remove(self.GetParent<Unit>().Id);
                        temAoiUnit1.enemyIds.Leaves.Add(self.GetParent<Unit>().Id);

                        ///ToTo 通知tem客户端(多个) 删除此小怪（1个）
                        temAoiPlayer1.RemoveMonsters(new long[1] { self.GetParent<Unit>().Id }, new long[1] { tem });

                        Console.WriteLine(" AoiUnit-96-小怪-Id：" + self.GetParent<Unit>().Id + " 离开 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                        Console.WriteLine(" AoiUnit-97-玩家-Id：" + tem + "(" + temAoiUnit1.gridId + ") 看到小怪：" + temAoiUnit1.enemyIds.MovesSet.Count);
                        break;
                }
            }

            foreach (long tem in self.enemyIds.Enters)
            {
                ///通知 刚进入本人视野的小怪（多个） 加入本人的Id（1个） 
                AoiUnitComponent temAoiUnit0 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                UnitType unitType = self.GetParent<Unit>().UnitType;
                switch (unitType)
                {
                    case UnitType.Player:
                        temAoiUnit0.playerIds.MovesSet.Remove(self.GetParent<Unit>().Id);
                        temAoiUnit0.playerIds.Leaves.Add(self.GetParent<Unit>().Id);
                        break;
                    case UnitType.Monster:
                        temAoiUnit0.enemyIds.MovesSet.Remove(self.GetParent<Unit>().Id);
                        temAoiUnit0.enemyIds.Leaves.Add(self.GetParent<Unit>().Id);
                        break;
                }
            }
        }

        /// <summary>
        /// 自己死亡后 将自己从别人的视野里删除
        /// </summary>
        public static void DeathRemove(this AoiUnitComponent self)
        {
            long unitId = self.GetParent<Unit>().Id;
            UnitType unitType = self.GetParent<Unit>().UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    foreach (long tem in self.playerIds.MovesSet)
                    {
                        AoiPlayerComponent temAoiPlayer1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiPlayerComponent>();
                        AoiUnitComponent temAoiUnit1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();

                        ///将自己从别人玩家的视野里删除
                        if (tem != unitId)
                        {
                            temAoiUnit1.playerIds.MovesSet.Remove(unitId);
                        }

                        ///通知tem客户端(多个) 删除此玩家（1个）实例，如果是本客户端则重置此玩家自己实例位置
                        temAoiPlayer1.RemovePlayers(new long[1] { unitId }, new long[1] { tem });                        

                        Console.WriteLine(" AoiUnit-145-玩家死亡-Id：" + unitId + " 离开 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                    }

                    foreach (long tem in self.enemyIds.MovesSet)
                    {
                        ///将自己 从小怪（多个）的视野里删除
                        AoiUnitComponent temAoiUnit2 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                        temAoiUnit2.playerIds.MovesSet.Remove(unitId);

                        Console.WriteLine(" AoiUnit-151-玩家死亡-Id：" + unitId + " 离开 MonsterId：" + tem + "(" + temAoiUnit2.gridId + ") 的视野。");
                    }

                    break;
                case UnitType.Monster:
                    foreach (long tem in self.playerIds.MovesSet)
                    {
                        AoiPlayerComponent temAoiPlayer1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiPlayerComponent>();
                        AoiUnitComponent temAoiUnit1 = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();

                        ///将死亡的小怪 从玩家的视野里删除
                        temAoiUnit1.enemyIds.MovesSet.Remove(unitId);

                        ///ToTo 通知tem客户端(多个) 删除此小怪（1个）实例
                        temAoiPlayer1.RemoveMonsters(new long[1] { unitId }, new long[1] { tem });

                        Console.WriteLine(" AoiUnit-165-小怪死亡-Id：" + unitId + " 离开 PlayerId：" + tem + "(" + temAoiUnit1.gridId + ") 的视野。");
                    }

                    //foreach (long tem in self.enemyIds.MovesSet)
                    //{
                    //    ///将死亡小怪 从其他小怪（多个）的视野里删除
                    //    AoiUnitComponent temAoiUnit2 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>();
                    //    temAoiUnit2.enemyIds.MovesSet.Remove(unitId);
                    //}

                    break;
            }
        }


    }
}
