using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_RemoveEnemyUnitsHandler : AMHandler<M2C_RemoveEnemyUnits>
    {
        protected override void Run(ETModel.Session session, M2C_RemoveEnemyUnits message)
        {
            EnemyUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>();
            foreach (UnitInfo unitInfo in message.Units)
            {
                if (enemyunitComponent.Get(unitInfo.UnitId) != null)
                {
                    enemyunitComponent.Remove(unitInfo.UnitId);

                    Debug.Log(" M2C_RemoveEnemyUnitsHandler-25-Id: " + unitInfo.UnitId);
                }
            }
        }

    }
}
