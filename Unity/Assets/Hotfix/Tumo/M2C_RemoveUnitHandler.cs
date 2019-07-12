using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_RemoveUnitHandler : AMHandler<M2C_RemoveUnit>
    {
        protected override void Run(ETModel.Session session, M2C_RemoveUnit message)
        {
            Debug.Log(" M2C_RemoveUnitHandler-13-Id/unittype:  " + message.Request + " / " + message.Message);

            EnemyUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>();

            long unitId = long.Parse(message.Request);

            Unit unit = enemyunitComponent.Get(unitId);
            int unittype = int.Parse(message.Message);

            GameObject unitGo = unit.GameObject;

            if (unit != null && unit.GameObject != null)
            {
                enemyunitComponent.Remove(unitId);

                GameObject.Destroy(unitGo, 1.0f);

                Debug.Log(" M2C_RemoveUnitHandler-26-Id/unittype:  " + unitId +" / "+ unittype);
            }
        }


    }
}
