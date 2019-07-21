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
    public class M2C_AddUnitHandler : AMHandler<M2C_AddUnits>
    {
        protected override void Run(ETModel.Session session, M2C_AddUnits message)
        {
            int unittype = message.UnitType;
            switch (unittype)
            {
                case 0:
                    UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
                    int oldcount0 = unitComponent.Count;
                    foreach (UnitInfo unitInfo in message.Units)
                    {
                        if (unitComponent.Get(unitInfo.UnitId) != null)
                        {
                            continue;
                        }
                        Unit unit0 = UnitFactory.Create(unitInfo.UnitId);
                        unit0.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);

                        if (unitInfo.UnitId == PlayerComponent.Instance.MyPlayer.UnitId)
                        {
                            ///20190621//将参数unit 传给组件CameraComponent awake方法
                            ETModel.Game.EventSystem.Awake<Unit>(ETModel.Game.Scene.GetComponent<CameraComponent>(), unit0);
                            ///20190703
                            Game.Scene.AddComponent<RaycastHitComponent>();
                            Game.Scene.AddComponent<TranslateMoveComponent>();

                            Debug.Log(" M2C_AddUnitHandler-39: " + unit0.Id);
                        }
                    }
                    Debug.Log(" M2C_AddUnitHandler-42-Player: " + unittype + " : " + oldcount0 + " + " + message.Units.Count + " = " + unitComponent.Count);
                    break;
                case 1:
                    MonsterUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<MonsterUnitComponent>();
                    int oldcount1 = enemyunitComponent.Count;
                    foreach (UnitInfo unitInfo in message.Units)
                    {
                        if (enemyunitComponent.Get(unitInfo.UnitId) != null)
                        {
                            continue;
                        }
                        Unit unit1 = MonsterUnitFactory.Create(unitInfo.UnitId);
                        unit1.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
                    }
                    Debug.Log(" M2C_AddUnitHandler-53-Monster: " + unittype + " : " + oldcount1 + " + " + message.Units.Count + " = " + enemyunitComponent.Count);
                    break;
                case 2:
                    break;
            }

        }


    }
}
