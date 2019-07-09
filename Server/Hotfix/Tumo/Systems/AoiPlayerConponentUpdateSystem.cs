using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiPlayerConponentUpdateSystem : UpdateSystem<AoiPlayerComponent>
    {
        public override void Update(AoiPlayerComponent self)
        {
            if (!self.startNull)
            {
                self.startTime = TimeHelper.ClientNowSeconds();
                self.startNull = true;
            }

            long nowtime = TimeHelper.ClientNowSeconds();

            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();

            if ((nowtime - self.startTime) > self.resTime)
            {
                Unit[] enters = Game.Scene.GetComponent<EnemyUnitComponent>().GetAllByIds(aoiUnit.enemyIds.Enters.ToArray());
                if (enters.Length > 0)
                {
                    //UpdateEnterUnits(enters, self.GetParent<Unit>().Id);
                    Console.WriteLine(" AoiPlayerConponentUpdateSystem-30-enters: " + enters.Length);
                }

                Unit[] leaves = Game.Scene.GetComponent<EnemyUnitComponent>().GetAllByIds(aoiUnit.enemyIds.Leaves.ToArray());
                if (leaves.Length > 0)
                {
                    //UpdateRemoveUnits(leaves, self.GetParent<Unit>().Id);
                    Console.WriteLine(" AoiPlayerConponentUpdateSystem-37-leaves: " + leaves.Length);
                }

            }       
        }

        #region
        void UpdateEnterUnits(Unit[] units, long playerUnitId)
        {
            if (units.Length == 0) return;
            /// 广播创建的unit
            M2C_GetEnemyUnits createUnits = new M2C_GetEnemyUnits();
            foreach (Unit u in units)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.X = u.Position.x;
                unitInfo.Y = u.Position.y;
                unitInfo.Z = u.Position.z;
                unitInfo.UnitId = u.Id;
                createUnits.Units.Add(unitInfo);
            }
            MessageHelper.Broadcast(createUnits, playerUnitId);
        }

        void UpdateRemoveUnits(Unit[] units, long playerUnitId)
        {
            if (units.Length == 0) return;
            /// 广播创建的unit
            M2C_RemoveEnemyUnits removeUnits = new M2C_RemoveEnemyUnits();
            foreach (Unit u in units)
            {
                UnitInfo unitInfo = new UnitInfo();
                //unitInfo.X = u.Position.x;
                //unitInfo.Y = u.Position.y;
                //unitInfo.Z = u.Position.z;
                unitInfo.UnitId = u.Id;
                removeUnits.Units.Add(unitInfo);
            }
            MessageHelper.Broadcast(removeUnits, playerUnitId);
        }


        #endregion


    }
}
