using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_KeyboardPositionHandler : AMHandler<M2C_KeyboardPosition>
    {
        protected override void Run(ETModel.Session session, M2C_KeyboardPosition message)
        {
            Debug.Log(" M2C_KeyboardPositionHandler-13: " + message.Id);

            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);

            if (unit == null) return;


            UnitPositionComponent unitPositionComponent = unit.GetComponent<UnitPositionComponent>();
            unitPositionComponent.StartMove(message).Coroutine();

            GizmosDebug.Instance.Path.Clear();
            GizmosDebug.Instance.Path.Add(new Vector3(message.X, message.Y, message.Z));
            for (int i = 0; i < message.Xs.Count; ++i)
            {
                GizmosDebug.Instance.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
            }

        }


    }

}
