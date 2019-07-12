using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_CreateEnemyUnitHandler : AMHandler<M2M_CreateUnit>
    {     
        protected override void Run(Session session, M2M_CreateUnit message)
        {
            CreateMonster(session, message).Coroutine();
        }     

        protected async ETVoid CreateMonster(Session session, M2M_CreateUnit message)
        {
            try
            {
                if (message.UnitId == 0)
                {
                    message.UnitId = IdGenerater.GenerateId();
                }
                Monster enemy = Game.Scene.GetComponent<MonsterComponent>().Get(message.RolerId);
                if (enemy == null) return;
                Unit unit = ComponentFactory.CreateWithId<Unit>(message.UnitId);
                unit.AddComponent<MoveComponent>();
                unit.AddComponent<UnitPathComponent>();
                unit.Position = new Vector3(enemy.spawnPosition.x, 0, enemy.spawnPosition.z);
                unit.RolerId = enemy.Id;

                await unit.AddComponent<MailBoxComponent>().AddLocation();
                Game.Scene.GetComponent<MonsterUnitComponent>().Add(unit);

                ///20190702
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Monster);
                unit.AddComponent<AoiUnitComponent>();
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<LifeComponent>();
                unit.AddComponent<PatrolComponent>();
                unit.AddComponent<SeeComponent>();

                SetNumeric(unit, enemy);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void SetNumeric(Unit unit, Monster enemy)
        {
            if (unit.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = unit.GetComponent<NumericComponent>();
            NumericComponent numEnemy = enemy.GetComponent<NumericComponent>();

            num[NumericType.MaxHpAdd] = numEnemy[NumericType.MaxHpAdd];

            ///小怪当前速度
            unit.GetComponent<MoveComponent>().Speed = 2.0f;

        }


    }
}