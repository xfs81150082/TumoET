using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_KeyboardEulerAnglersHandler : AMHandler<M2C_KeyboardEulerAnglers>
    {
        protected override void Run(ETModel.Session session, M2C_KeyboardEulerAnglers message)
        {
            Debug.Log(" M2C_KeyboardEulerAnglersHandler-13-angles: " + message.Y + " / " + message.Ys[0]);

            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);

            if (unit == null) return;

            UnitAnglersComponent unitAnglersComponent = unit.GetComponent<UnitAnglersComponent>();
            unitAnglersComponent.StartTurn(message).Coroutine();

            //unit.GetComponent<TmAnimatorComponent>().AnimSet(5f);
        }


    }
}
