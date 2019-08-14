using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Move_MapHandler : AMActorLocationHandler<Unit, Move_Map>
    {
        protected override void Run(Unit entity, Move_Map message)
        {
            Vector3 target = new Vector3(message.X, message.Y, message.Z);

            if (message.V == 0f)
            {
                if (entity.GetComponent<MapPathComponent>().CancellationTokenSource != null)
                {
                    entity.GetComponent<MapPathComponent>().CancellationTokenSource?.Cancel();
                    entity.GetComponent<MapPathComponent>().CancellationTokenSource?.Dispose();
                    entity.GetComponent<MapPathComponent>().CancellationTokenSource = null;

                    entity.Position = target;
                    entity.SaveVector3();

                    Console.WriteLine(" Move_MapHandler-26-ServerPos: CancellationTokenSource is Cancel." + entity.Position.ToString());
                }
            }
            else
            {
                entity.GetComponent<MapPathComponent>().MoveTo(target).Coroutine();

                Console.WriteLine(" Move_MapHandler-38-target:" + target.ToString());
            }

        }
    }
}
