using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ETModel;
using System.Linq;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_GetEnemyUnitsHandler : AMHandler<M2C_GetEnemyUnits>
    {
        protected override void Run(ETModel.Session session, M2C_GetEnemyUnits message)
        {
            EnemyUnitComponent enemyunitComponent = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>();
            foreach (UnitInfo unitInfo in message.Units)
            {
                if (enemyunitComponent.Get(unitInfo.UnitId) != null)
                {
                    continue;
                }
                Unit unit = EnemyUnitFactory.Create(unitInfo.UnitId);
                unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);

                Debug.Log(" M2C_GetEnemyUnitHandler-Id-25: " + unitInfo.UnitId + "/" + unit.Id);
            }
        }



        #region 作废
        Dictionary<long, UnitInfo> infoes = new Dictionary<long, UnitInfo>();
        void SetInfoes(M2C_GetEnemyUnits message)
        {
            foreach (UnitInfo unitInfo in message.Units)
            {
                infoes.Add(unitInfo.UnitId, unitInfo);
            }

            //long[] old1 = ETModel.Game.Scene.GetComponent<EnemyComponent>().GetIds();

            long[] old2 = ETModel.Game.Scene.GetComponent<EnemyUnitComponent>().GetIds();
            long[] nine = infoes.Keys.ToArray();

            long[] enters = nine.Except(old2).ToArray();
            long[] leaves = old2.Except(nine).ToArray();

            SpawnEnemys(enters);

            RemoveEnemys(leaves);
        }
        void SpawnEnemys(long[] enters)
        {
            foreach (long tem in enters)
            {
                Enemy enemy = EnemyFactory.Create(tem);
                Unit unit = EnemyUnitFactory.Create(tem);
                unit.Position = new Vector3(infoes[tem].X, infoes[tem].Y, infoes[tem].Z);
            }
        }
        void RemoveEnemys(long[] leaves)
        {
            foreach(long tem in leaves)
            {
                ETModel.Game.Scene.GetComponent<EnemyComponent>().Remove(tem);
                ETModel.Game.Scene.GetComponent<EnemyUnitComponent>().Remove(tem);
            }
        }
        #endregion



    }
}