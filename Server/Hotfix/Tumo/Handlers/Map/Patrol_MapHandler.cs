using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Patrol_MapHandler : AMActorLocationHandler<Unit, Patrol_Map>
    {
        protected override void Run(Unit entity, Patrol_Map message)
        {
            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            entity.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();

            Console.WriteLine(" Patrol_MapHandler-xyz: (" + message.X + ", " + message.Y + ", " + message.Z + ") ");
        }

    }
}