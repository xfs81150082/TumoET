using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Turn_MapHandler : AMActorLocationHandler<Unit, Turn_Map>
    {
        protected override void Run(Unit entity, Turn_Map message)
        {
            ///ToTo 直接转发给所有 客户端
            Vector3 dirPos = new Vector3(message.X, message.Y, message.Z);

            Console.WriteLine(" Turn_MapHandler-20-dirPos: " + dirPos.ToString());

            M2C_KeyboardDirection m2C_KeyboardDirection = new M2C_KeyboardDirection();

            m2C_KeyboardDirection.X = dirPos.x;
            m2C_KeyboardDirection.Y = dirPos.y;
            m2C_KeyboardDirection.Z = dirPos.z;
            m2C_KeyboardDirection.Id = entity.Id;

            MessageHelper.Broadcast(m2C_KeyboardDirection, entity.GetComponent<AoiUnitComponent>().playerIds.MovesSet.ToArray());
        }


    }
}
