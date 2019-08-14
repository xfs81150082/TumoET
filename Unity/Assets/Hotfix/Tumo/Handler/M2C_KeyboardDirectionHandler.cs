using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_KeyboardDirectionHandler : AMHandler<M2C_KeyboardDirection>
    {
        protected override void Run(ETModel.Session session, M2C_KeyboardDirection message)
        {
            Debug.Log(" M2C_KeyboardDirectionHandler-13-angles: " + new Vector3(message.X, message.Y, message.Z).ToString());

            Vector3 dirPos = new Vector3(message.X, 0, message.Z);

            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            if (message.Id == ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId)
            {
                return;
            }
            if (unit == null) return;

            Vector3 self = unit.Position;
            Vector3 targetPos = self + dirPos;
            unit.GameObject.transform.LookAt(targetPos);
        }


    }
}
