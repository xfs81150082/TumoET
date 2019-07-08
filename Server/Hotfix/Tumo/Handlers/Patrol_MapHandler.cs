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
            //Unit unit = Game.Scene.GetComponent<EnemyUnitComponent>().Get(message.Id);
            //long[] playerIds = Game.Scene.GetComponent<AoiGridComponent>().GetPlayerIds(unit.GetComponent<AoiUnitComponent>().NineGridIds.ToArray());
            //if (!playerIds.Contains(entity.Id)) return;

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            entity.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();

            Console.WriteLine(" Patrol_MapHandler-xyz: (" + message.X + ", " + message.Y + ", " + message.Z + ") ");
        }

    }
}