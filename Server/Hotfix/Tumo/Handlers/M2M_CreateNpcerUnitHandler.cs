using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_CreateNpcerUnitHandler : AMHandler<M2M_CreateNpcerUnit>
    {
        protected override void Run(Session session, M2M_CreateNpcerUnit message)
        {
            RunAsync(session, message).Coroutine();

            //Console.WriteLine(" M2M_CreateNpcerUnitHandler-17-Count: " + Game.Scene.GetComponent<NpcerUnitComponent>().Count);
        }

        async ETVoid RunAsync(Session session, M2M_CreateNpcerUnit message)
        {
            try
            {
                Npcer npcer = Game.Scene.GetComponent<NpcerComponent>().Get(message.NpcerId);
                if (npcer == null) return;

                Unit unit = ComponentFactory.CreateWithId<Unit>(npcer.Id);
                unit.AddComponent<MoveComponent>();
                unit.AddComponent<UnitPathComponent>();
                unit.Position = new Vector3(npcer.spawnPosition.x, 0, npcer.spawnPosition.z);

                await unit.AddComponent<MailBoxComponent>().AddLocation();
                Game.Scene.GetComponent<NpcerUnitComponent>().Add(unit);

                ///20190702
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Npc);
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<LifeComponent>();
                unit.AddComponent<PatrolComponent>();
                unit.AddComponent<SeeComponent>();
                unit.AddComponent<AoiUnitComponent>();

                SetNumeric(unit, npcer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void SetNumeric(Unit unit, Npcer enemy)
        {
            if (unit.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = unit.GetComponent<NumericComponent>();
            NumericComponent numEnemy = enemy.GetComponent<NumericComponent>();

            num[NumericType.MaxHpAdd] = numEnemy[NumericType.MaxHpAdd];

        }

    }
}
