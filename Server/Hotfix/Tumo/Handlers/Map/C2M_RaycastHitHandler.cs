using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_RaycastHitHandler : AMActorLocationRpcHandler<Unit, C2M_RaycastHitActorRequest, M2C_RaycastHitActorResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_RaycastHitActorRequest message, Action<M2C_RaycastHitActorResponse> reply)
        {
            Unit mesUnit = Game.Scene.GetComponent<MonsterUnitComponent>().Get(long.Parse(message.Info));
            if(mesUnit == null)
            {
                mesUnit = Game.Scene.GetComponent<UnitComponent>().Get(long.Parse(message.Info));
            }
            unit.GetComponent<RayUnitComponent>().target = mesUnit;
            
            reply(new M2C_RaycastHitActorResponse() { Info = " Server Response：选择目标成功" });
            await ETTask.CompletedTask;
        }
    }
}