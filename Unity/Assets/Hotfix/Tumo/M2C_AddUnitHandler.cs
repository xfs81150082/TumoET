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
            Debug.Log(" M2C_AddUnitHandler-16-Id/unittype/Units:  " + session.Id + " / " + message.UnitType + " / " + message.Units.count);

            int unittype =message.UnitType;
            switch (unittype)
            {
                case 0:
                    UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
                    foreach (UnitInfo unitInfo in message.Units)
                    {
                        if (unitComponent.Get(unitInfo.UnitId) != null)
                        {
                            continue;
                        }
                        Unit unit0 = UnitFactory.Create(unitInfo.UnitId);
                        unit0.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);

                        ///20190621//将参数unit 传给组件CameraComponent awake方法
                        ETModel.Game.EventSystem.Awake<Unit>(ETModel.Game.Scene.GetComponent<CameraComponent>(), unit0);

                        ///20190619
                        Debug.Log(" M2C_AddUnitHandler-Id-32: " + unitInfo.UnitId + "/" + unit0.Id);
                        Debug.Log(" M2C_AddUnitHandler-PlayerComponent.Instance.MyPlayer.UnitId: " + PlayerComponent.Instance.MyPlayer.UnitId);
                    }
                    ///20190703
                    Game.Scene.AddComponent<RaycastHitComponent>();

                    break;
                case 1:
                    EnemyUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>();
                    foreach (UnitInfo unitInfo in message.Units)
                    {
                        if (enemyunitComponent.Get(unitInfo.UnitId) != null)
                        {
                            continue;
                        }
                        Unit unit1 = EnemyUnitFactory.Create(unitInfo.UnitId);
                        unit1.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);

                        Debug.Log(" M2C_AddUnitHandler-Id-46: " + unitInfo.UnitId + "/" + unit1.Id);
                    }
                    break;
                case 2:
                    break;
            }
        }


    }
}
