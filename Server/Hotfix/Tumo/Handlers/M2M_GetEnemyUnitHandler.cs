﻿using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_GetEnemyUnitHandler : AMHandler<M2M_GetEnemyUnit>
    {
        protected override void Run(Session session, M2M_GetEnemyUnit message)
        {
            try
            {                
                /// 广播刷新小怪到客户端 unit
                Unit[] units = Game.Scene.GetComponent<MonsterUnitComponent>().GetAll();
                //SetEnemyUnits(units, message.playerUnitId);
                SetUnits(units, message.playerUnitId);

                Console.WriteLine(" M2M_GetEnemyUnitHandler-19-playerUnitId: " + message.playerUnitId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void SetUnits(Unit[] units, long playerUnitId)
        {
            /// 广播创建的unit
            M2C_AddUnits m2C_AddUnits = new M2C_AddUnits();

            m2C_AddUnits.UnitType = (int)UnitType.Monster;

            foreach (Unit u in units)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.X = u.Position.x;
                unitInfo.Y = u.Position.y;
                unitInfo.Z = u.Position.z;
                unitInfo.UnitId = u.Id;
                m2C_AddUnits.Units.Add(unitInfo);
            }
            MessageHelper.Broadcast(m2C_AddUnits, playerUnitId);
        }

        void SetEnemyUnits(Unit[] units, long playerUnitId)
        {
            /// 广播创建的unit
            M2C_GetEnemyUnits createUnits = new M2C_GetEnemyUnits();
            foreach (Unit u in units)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.X = u.Position.x;
                unitInfo.Y = u.Position.y;
                unitInfo.Z = u.Position.z;
                unitInfo.UnitId = u.Id;
                createUnits.Units.Add(unitInfo);
            }
            MessageHelper.Broadcast(createUnits ,playerUnitId);
        }
        
    }
}