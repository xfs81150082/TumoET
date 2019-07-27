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
            unit.GetComponent<ServerMoveComponent>().Trun(message);
            unit.GetComponent<ServerMoveComponent>().Move(message);

            //unit.Position = new Vector3(message.X, 0, message.Z);
            //unit.eulerAngles = new Vector3(0, message.AY, 0);
            //unit.GetComponent<AoiUnitComponent>().MoveStateBroadcastToClient(message);

            Console.WriteLine(" Move_MapHandler-42-xyz: " + KeyType.KeyCode + " / " + unit.Id + " : ( " + message.V + " / " + message.H + ") ");
     
            //Console.WriteLine(" Move_MapHandler-42-xyz: " + KeyType.KeyCode + " / " + unit.Id + " : ( " + unit.Position.x + ", " + unit.Position.y + ", " + unit.Position.z + ") ");
            //Console.WriteLine(" Move_MapHandler-43-ay: " + KeyType.KeyCode + " / " + unit.Id + " : ( " + unit.eulerAngles.x + ", " + unit.eulerAngles.y + ", " + unit.eulerAngles.z + ") ");
        }


    }
}
