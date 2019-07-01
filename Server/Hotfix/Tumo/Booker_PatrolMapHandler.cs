using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Booker_PatrolMapHandler : AMActorLocationHandler<Unit, Booker_PatrolMap>
    {
        protected override void Run(Unit entity, Booker_PatrolMap message)
        {
            Console.WriteLine(" Booker_PatrolMapHandler-Id-14: " + entity.Id + " :Id/InstanceId: " + entity.InstanceId);

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            entity.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();

            Console.WriteLine(" Booker_PatrolMapHandler-ActorId-18: " + message.Id + " :Id/ActorId: " + message.ActorId);
            Console.WriteLine(" Booker_PatrolMapHandler-xyz-19: " + message.X + ", " + message.Y + ", " + message.Z);
        }
    }
}