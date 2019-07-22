using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TestComponentUpdateSystem : UpdateSystem<TestComponent>
    {
        public override void Update(TestComponent self)
        {
            //ETVoidAsync().Coroutine();
            SendClient();
        }

        void SendClient()
        {
            //Console.WriteLine(" TestComponentUpdateSystem-20-unitIds: " + TimeHelper.ClientNow());

            //long[] unitIds = Game.Scene.GetComponent<MonsterUnitComponent>().GetIdsAll();

            MessageHelper.Broadcast(new Move_KeyCodeMap());
        }

        async ETVoid ETVoidAsync()
        {
            long[] unitIds = Game.Scene.GetComponent<MonsterUnitComponent>().GetIdsAll();

            Console.WriteLine(" TestComponentUpdateSystem-20-unitIds: " + unitIds);

            M2W_DeathActorResponse response = (M2W_DeathActorResponse)await ActorHelper.ActorLocation(unitIds[0]).Call(new W2M_DeathActorRequest() { Info = "TestComponentUpdateSystem-20" });

            Console.WriteLine(" TestComponentUpdateSystem-24-response: " + response.Message);

        }


    }
}
