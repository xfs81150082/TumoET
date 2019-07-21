using System;
using System.Collections.Generic;
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
            UnitTrun(player, message);
        }

        void UnitClickMove(Unit unit, Move_Map message)
        {
            Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.X + " / " + message.Y + " / " + message.Z);

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            unit.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();

        }
        void UnitKeyCodeMove(Unit unit, Move_Map message)
        {
            Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.WS + " / " + message.AD);

        }
        void UnitTrun(Unit unit, Move_Map message)
        {

        }


    }


}
