using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_KeyboardPathHandler : AMActorLocationHandler<Unit, C2M_KeyboardPathResult>
    {
        protected override void Run(Unit player, C2M_KeyboardPathResult message)
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

        void UnitClickMove(Unit unit, C2M_KeyboardPathResult message)
        {
            Console.WriteLine(" Move_MapHandler: " + (KeyType)message.KeyType + " / " + unit.Id + " / " + message.Id + " / " + message.X + " / " + message.Y + " / " + message.Z);
            //unit.Position = new Vector3(message.X, message.Y, message.Z);

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            unit.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();
        }

        void ServerUnitKeyCodeMove(Unit unit, C2M_KeyboardPathResult message)
        {
            unit.GetComponent<KeyboardPathComponent>().KeyboardVHMove(message.V, message.H);

            Console.WriteLine(" Move_MapHandler-42-xyz: " + (KeyType)message.KeyType + " / " + unit.Id + " : ( " + message.V + " / " + message.H + ") ");     
        }


    }
}
