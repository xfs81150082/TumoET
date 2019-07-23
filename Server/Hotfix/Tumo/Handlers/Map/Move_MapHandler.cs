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
                    UnitKeyCodeMove(player, message);
                    break;
            }
        }

        void UnitClickMove(Unit unit, Move_Map message)
        {
            Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.X + " / " + message.Y + " / " + message.Z);

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            unit.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();
        }

        void UnitKeyCodeMove(Unit unit, Move_Map message)
        {
            if (unit.GetComponent<AoiUnitComponent>() != null)
            {
                AoiUnitComponent aoiUnit = unit.GetComponent<AoiUnitComponent>();

                float offsetZ = message.V;
                Vector3 unitPos = unit.Position;
                unit.Position = new Vector3(unitPos.x, unitPos.y, unitPos.z + offsetZ);

                Move_KeyCodeMap move_KeyCodeMap = new Move_KeyCodeMap();
                move_KeyCodeMap.Id = unit.Id;
                move_KeyCodeMap.KeyType = message.KeyType;
                move_KeyCodeMap.X = message.X;
                move_KeyCodeMap.Y = message.Y;
                move_KeyCodeMap.Z = message.Z;
                move_KeyCodeMap.V = message.V;
                move_KeyCodeMap.H = message.H;

                MessageHelper.Broadcast(move_KeyCodeMap, aoiUnit.playerIds.MovesSet.ToArray());

                Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.V + " / " + message.H );
            }
        }

        void UnitTrun(Unit unit, Move_Map message)
        {
            //Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.WS + " / " + message.AD);

        }


    }


}
