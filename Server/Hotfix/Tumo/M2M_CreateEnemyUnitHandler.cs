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

            Console.WriteLine(" M2M_CreateEnemyUnitHandler-17-Count: " + Game.Scene.GetComponent<EnemyUnitComponent>().Count);
        }

        async ETVoid RunAsync(Session session, M2M_CreateEnemyUnit message)
        {
            try
            {
                for (int i = 0; i < message.Count; i++)
                {
                    Unit unit = ComponentFactory.CreateWithId<Unit>(IdGenerater.GenerateId());
                    unit.AddComponent<MoveComponent>();
                    unit.AddComponent<UnitPathComponent>();
                    unit.Position = new Vector3(-10, 0, 4 * i);

                    await unit.AddComponent<MailBoxComponent>().AddLocation();
                    Game.Scene.GetComponent<EnemyUnitComponent>().Add(unit);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }




    }
}
