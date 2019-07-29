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
            Console.WriteLine(" C2M_KeyboardPathHandler: " + (KeyType)message.KeyType + " / " + message.Id + " / " + message.X + " / " + message.Y + " / " + message.Z);
            //unit.Position = new Vector3(message.X, message.Y, message.Z);

            Vector3 target = new Vector3(message.X, message.Y, message.Z);
            unit.GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();
        }

        void ServerUnitKeyCodeMove(Unit unit, C2M_KeyboardPathResult message)
        {
            unit.GetComponent<KeyboardPathComponent>().KeyboardVH(message.V, message.H);

            //unit.GetComponent<KeyboardPathComponent>().KeyboardVMove(message.V);
            //unit.GetComponent<KeyboardPathComponent>().KeyboardHTurn(message.H);

            Console.WriteLine(" C2M_KeyboardPathHandler-39-xyz: " + (KeyType)message.KeyType + " / " + unit.Id + " : ( " + message.V + " / " + message.H + ") ");     
        }


    }
}
