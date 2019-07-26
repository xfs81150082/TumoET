using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_MoveKeyCodeHandler : AMHandler<Move_KeyCodeMap>
    {
        protected override void Run(ETModel.Session session, Move_KeyCodeMap message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            if (unit != null)
            {
                unit.GetComponent<StateMoveComponent>().mypath.Add(message);
            }

            Debug.Log(" M2C_MoveKeyCodeHandler-13-x/y/z: " + TimeHelper.ClientNow() + " : " + (KeyType)message.KeyType + " / " + message.Id + " / " + message.X + " / " + message.Y + " / " + message.Z);
            
        }

    }


}
