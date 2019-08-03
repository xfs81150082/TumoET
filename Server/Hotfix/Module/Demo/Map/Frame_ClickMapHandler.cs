using ETModel;
using PF;
using System;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Frame_ClickMapHandler : AMActorLocationHandler<Unit, Frame_ClickMap>
    {
        protected override void Run(Unit entity, Frame_ClickMap message)
        {
            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            entity.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();

            Console.WriteLine(" Frame_ClickMapHandler-16-ClickPos: " + entity.Id + " " + target.ToString());
        }
    }
}
