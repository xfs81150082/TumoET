using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_UnitRemoveHandler : AMHandler<M2C_RemoveUnits>
    {
        protected override void Run(ETModel.Session session, M2C_RemoveUnits message)
        {
            int unittype = message.UnitType;
            switch (unittype)
            {
                case 0:
                    UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
                    int oldcount0 = unitComponent.Count;
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
                            if (unitInfo.UnitId == ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId)
                            {
                                unit0.Position = new Vector3(-40, 0, -20);
                            }
                            else
                            {
                                unitComponent.Remove(unit0.Id);
                                GameObject.Destroy(unitGo0, 1.0f);
                            }
                        }
                    }
                    Debug.Log(" M2C_AddUnitHandler-35-Player: " + unittype + " : " + oldcount0 + " - " + message.Units.Count + " = " + unitComponent.Count);
                    break;
                case 1:
                    MonsterUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<MonsterUnitComponent>();
                    int oldcount1 = enemyunitComponent.Count;
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
                        }
                    }
                    Debug.Log(" M2C_AddUnitHandler-56-Monster: " + unittype + " : " + oldcount1 + " - " + message.Units.Count + " = " + enemyunitComponent.Count);
                    break;
            }
        }


    }
}
