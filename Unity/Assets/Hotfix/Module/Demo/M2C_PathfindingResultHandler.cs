using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_PathfindingResultHandler : AMHandler<M2C_PathfindingResult>
    {
        protected override void Run(ETModel.Session session, M2C_PathfindingResult message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<MonsterUnitComponent>().Get(message.Id);

            ///20190630
            if (unit == null)
            {
                unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            }

            if (unit == null) return;

            //unit.GetComponent<AnimatorComponent>().SetFloatValue("Speed", 5f);

            ///20190803
            unit.GetComponent<AnimatorComponent>().AnimSet(1.0f);

            UnitPathComponent unitPathComponent = unit.GetComponent<UnitPathComponent>();
            unitPathComponent.StartMove(message).Coroutine();

            GizmosDebug.Instance.Path.Clear();
            GizmosDebug.Instance.Path.Add(new Vector3(message.X, message.Y, message.Z));
            for (int i = 0; i < message.Xs.Count; ++i)
            {
                GizmosDebug.Instance.Path.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
            }
        }


    }
}
