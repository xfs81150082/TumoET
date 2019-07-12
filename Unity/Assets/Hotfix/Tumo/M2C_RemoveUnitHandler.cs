using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_RemoveUnitHandler : AMHandler<M2C_RemoveUnits>
    {
        protected override void Run(ETModel.Session session, M2C_RemoveUnits message)
        {
            Debug.Log(" M2C_RemoveUnitHandler-13-Id/unittype:  " + message.UnitType + " / " + message.Units.count);

            int unittype = message.UnitType;
            switch (unittype)
            {
                case 0:
                    UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
                    foreach (UnitInfo unitInfo in message.Units)
                    {
                        if (unitComponent.Get(unitInfo.UnitId) == null)
                        {
                            continue;
                        }

                        Unit unit0 = unitComponent.Get(unitInfo.UnitId);
                        GameObject unitGo0 = unit0.GameObject;

                        if (unit0 != null && unit0.GameObject != null)
                        {
                            unitComponent.Remove(unit0.Id);
                            GameObject.Destroy(unitGo0, 1.0f);

                            Debug.Log(" M2C_RemoveUnitHandler-35-Id/unittype:  " + unit0.Id + " / " + unittype);
                        }
                    }
                    break;
                case 1:
                    EnemyUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>();
                    foreach (UnitInfo unitInfo in message.Units)
                    {
                        if (enemyunitComponent.Get(unitInfo.UnitId) == null)
                        {
                            continue;
                        }

                        Unit unit1 = enemyunitComponent.Get(unitInfo.UnitId);
                        GameObject unitGo1 = unit1.GameObject;

                        if (unit1 != null && unit1.GameObject != null)
                        {
                            enemyunitComponent.Remove(unit1.Id);
                            GameObject.Destroy(unitGo1, 1.0f);

                            Debug.Log(" M2C_RemoveUnitHandler-56-Id/unittype:  " + unit1.Id + " / " + unittype);
                        }
                    }
                    break;
                case 2:
                    break;
            }
        }


    }
}
