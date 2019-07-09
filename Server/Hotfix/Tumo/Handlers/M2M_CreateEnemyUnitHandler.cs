using System;
using System.Collections.Generic;
using System.Text;
using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_CreateEnemyUnitHandler : AMHandler<M2M_CreateEnemyUnit>
    {
        protected override void Run(Session session, M2M_CreateEnemyUnit message)
        {
            RunAsync(session, message).Coroutine();

            //Console.WriteLine(" M2M_CreateEnemyUnitHandler-17-Count: " + Game.Scene.GetComponent<EnemyUnitComponent>().Count);
        }

        async ETVoid RunAsync(Session session, M2M_CreateEnemyUnit message)
        {
            try
            {
                Enemy enemy = Game.Scene.GetComponent<EnemyComponent>().Get(message.EnemyId);
                if (enemy == null) return;

                Unit unit = ComponentFactory.CreateWithId<Unit>(enemy.Id);
                unit.AddComponent<MoveComponent>();
                unit.AddComponent<UnitPathComponent>();
                unit.Position = new Vector3(enemy.spawnPosition.x, 0, enemy.spawnPosition.z);

                await unit.AddComponent<MailBoxComponent>().AddLocation();
                Game.Scene.GetComponent<EnemyUnitComponent>().Add(unit);

                ///20190702
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Monster);
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<RecoverComponent>();
                unit.AddComponent<PatrolComponent>();
                unit.AddComponent<SeeComponent>();
                unit.AddComponent<AoiUnitComponent>();

                SetNumeric(unit, enemy);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void SetNumeric(Unit unit , Enemy enemy)
        {
            if (unit.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = unit.GetComponent<NumericComponent>();
            NumericComponent numEnemy = enemy.GetComponent<NumericComponent>();

            num[NumericType.MaxHpAdd] = numEnemy[NumericType.MaxHpAdd];

            ///小怪当前速度
            unit.AddComponent<MoveComponent>().Speed = 2.0f;

        }

    }
}