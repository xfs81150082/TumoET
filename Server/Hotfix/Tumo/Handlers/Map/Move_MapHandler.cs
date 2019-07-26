using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Move_MapHandler : AMActorLocationHandler<Unit, Move_Map>
    {
        protected override void Run(Unit player, Move_Map message)
        {
            switch (message.KeyType)
            {
                case 0:
                    UnitClickMove(player, message);
                    break;
                case 1:
                    ServerUnitKeyCodeMove(player, message);
                    break;
            }
        }

        void UnitClickMove(Unit unit, Move_Map message)
        {
            Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.X + " / " + message.Y + " / " + message.Z);
            //unit.Position = new Vector3(message.X, message.Y, message.Z);

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            unit.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();
        }

        void ServerUnitKeyCodeMove(Unit unit, Move_Map message)
        {
            unit.GetComponent<ServerMoveComponent>().v = message.V;
            unit.GetComponent<ServerMoveComponent>().h = message.H;


            if (Math.Abs(message.V) > 0.05f || Math.Abs(message.H) > 0.05f)
            {
                unit.GetComponent<AoiUnitComponent>().MoveStateBroadcastToClient();

                Console.WriteLine(" Move_MapHandler-45-vh: " + KeyType.KeyCode + " / " + unit.Id + " : ( " + unit.Position.x + ", " + unit.Position.y + ", " + unit.Position.z+") ");
            }
        }


    }
}
