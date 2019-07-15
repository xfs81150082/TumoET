using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_AddUnitHandler : AMHandler<M2M_AddUnits>
    {
        protected override void Run(ETModel.Session session, M2M_AddUnits message)
        {
            Console.WriteLine(" M2C_AddUnitHandler-15-unittype/uis/pis:  " + (UnitType)message.UnitType + " / " + message.UnitIds.Count + " / " + message.PlayerUnitIds.Count);

            int unittype = message.UnitType;
            switch (unittype)
            {
                case 0:
                    M2C_AddUnits m2C_AddUnits0 = new M2C_AddUnits() { UnitType = message.UnitType };
                    foreach (long playerId in message.UnitIds)
                    {
                        Unit player = Game.Scene.GetComponent<UnitComponent>().Get(playerId);
                        if (player != null)
                        {
                            UnitInfo unitInfo = new UnitInfo();
                            unitInfo.UnitId = player.Id;
                            unitInfo.X = player.Position.x;
                            unitInfo.Y = player.Position.y;
                            unitInfo.Z = player.Position.z;
                            m2C_AddUnits0.Units.Add(unitInfo);
                        }
                    }
                    MessageHelper.Broadcast(m2C_AddUnits0, message.PlayerUnitIds.ToArray());

                    Console.WriteLine(" M2C_AddUnitHandler-Id-38: " + (int)message.UnitType);
                    break;
                case 1:
                    M2C_AddUnits m2C_AddUnits1 = new M2C_AddUnits() { UnitType = message.UnitType };
                    foreach (long monsterId in message.UnitIds)
                    {
                        Unit monster = Game.Scene.GetComponent<MonsterUnitComponent>().Get(monsterId);
                        if (monster != null)
                        {
                            UnitInfo unitInfo = new UnitInfo();
                            unitInfo.UnitId = monster.Id;
                            unitInfo.X = monster.Position.x;
                            unitInfo.Y = monster.Position.y;
                            unitInfo.Z = monster.Position.z;
                            m2C_AddUnits1.Units.Add(unitInfo);
                        }
                    }
                    MessageHelper.Broadcast(m2C_AddUnits1 ,message.PlayerUnitIds.ToArray());

                    Console.WriteLine(" M2C_AddUnitHandler-Id-58: " + (int)message.UnitType);
                    break;
                case 2:
                    break;
            }
        }



    }
}
