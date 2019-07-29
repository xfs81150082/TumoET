using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_ServerPathResultHandler : AMHandler<M2C_ServerPathResult>
    {
        protected override void Run(ETModel.Session session, M2C_ServerPathResult message)
        {
            Debug.Log(" ServerUnitPathComponent-13: " + message.Id );

            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);

            if (unit == null) return;

            unit.GetComponent<AnimatorComponent>().SetFloatValue("Speed", 5f);
            ServerUnitPathComponent unitPathComponent = unit.GetComponent<ServerUnitPathComponent>();
            unitPathComponent.StartMT(message);

            GizmosDebug.Instance.Path.Clear();
            GizmosDebug.Instance.Path.Add(new Vector3(message.X, message.Y, message.Z));
            for (int i = 0; i < message.Xs.Count; ++i)
            {
                GizmosDebug.Instance.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
            }

        }


    }
}
