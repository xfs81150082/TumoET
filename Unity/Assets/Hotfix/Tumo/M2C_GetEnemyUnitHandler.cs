using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_GetEnemyUnitHandler : AMHandler<M2C_GetEnemyUnits>
    {
        protected override void Run(ETModel.Session session, M2C_GetEnemyUnits message)
        {
            EnemyUnitComponent unitComponent = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>();

            foreach (UnitInfo unitInfo in message.Units)
            {
                if (unitComponent.Get(unitInfo.UnitId) != null)
                {
                    continue;
                }
                ////Unit unit = UnitFactory.Create(unitInfo.UnitId);
                Unit unit = EnemyUnitFactory.Create(unitInfo.UnitId);
                unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);

                //Debug.Log(" M2C_CreateEnemyUnitHandler-23: " + unitInfo.Z);
            }
        }



    }
}