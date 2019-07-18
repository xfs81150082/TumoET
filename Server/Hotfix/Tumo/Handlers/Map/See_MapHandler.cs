using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class See_MapHandler : AMActorLocationHandler<Unit, See_Map>
    {
        protected override void Run(Unit entity, See_Map message)
        {
            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            entity.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();
        }

    }
}