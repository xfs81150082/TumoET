using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class SqrDistanceHelper
    {
        public static void SqrDistance(this SqrDistanceComponent self)
        {
            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();

            UnitType unitType = self.GetParent<Unit>().UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    if (aoiUnit.enemyIds.MovesSet.Count == 0) return;
                    Unit[] units1 = Game.Scene.GetComponent<EnemyUnitComponent>().GetAllByIds(aoiUnit.enemyIds.MovesSet.ToArray());
                    self.SqrDistance(units1);

                    //Console.WriteLine(" SqrDistanceHelper-24-enemyIds.MovesSet: ids/units: " + aoiUnit.enemyIds.MovesSet.Count + " / " + units1.Length);
                    break;
                case UnitType.Monster:
                    if (aoiUnit.playerIds.MovesSet.Count == 0) return;
                    Unit[] units2 = Game.Scene.GetComponent<UnitComponent>().GetAllByIds(aoiUnit.playerIds.MovesSet.ToArray());
                    Unit[] units3 = Game.Scene.GetComponent<NpcerUnitComponent>().GetAllByIds(aoiUnit.npcerIds.MovesSet.ToArray());
                    self.SqrDistance(units2);

                    //Console.WriteLine(" SqrDistanceHelper-31-playerIds.MovesSet: ids/units: " + aoiUnit.playerIds.MovesSet.Count + " / " + units2.Length);
                    break;
                case UnitType.Npc:
                    if (aoiUnit.playerIds.MovesSet.Count == 0) return;
                    Unit[] units4 = Game.Scene.GetComponent<EnemyUnitComponent>().GetAllByIds(aoiUnit.enemyIds.MovesSet.ToArray());
                    self.SqrDistance(units4);

                    //Console.WriteLine(" SqrDistanceHelper-40-enemyIds.MovesSet: ids/units: " + aoiUnit.enemyIds.MovesSet.Count + " / " + units4.Length);
                    break;
                default:
                    break;
            }
        }

        public static void SqrDistance(this SqrDistanceComponent self, Unit[] units)
        {
            if (units.Length == 0)
            {
                self.neastDistance = float.PositiveInfinity;
                self.neastUnit = null;
                return;
            }
            self.neastUnit = NeastUnit(self.GetParent<Unit>(), units);
            if (self.neastUnit != null)
            {
                self.neastDistance = Distance(self.GetParent<Unit>().Position, self.neastUnit.Position);
            }
            else
            {
                self.neastDistance = float.PositiveInfinity;
            }
        }      

        public static Unit NeastUnit(Unit unit, Unit[] units)
        {
            if (unit.GetComponent<AttackComponent>() != null && unit.GetComponent<AttackComponent>().isDeath) return null;

            Unit obj = null;
            float dis = float.PositiveInfinity;
            foreach (Unit tem in units)
            {
                if (tem.GetComponent<AttackComponent>() != null && tem.GetComponent<AttackComponent>().isDeath) break;
               
                if (tem != null)
                {
                    float sqr = Distance(unit.Position, tem.Position);
                    if (sqr < dis)
                    {
                        dis = sqr;
                        obj = tem;
                    }
                }
            }
            return obj;
        }

        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            Vector3 vec0 = vec1;
            vec0.y =vec2.y ;
            Vector3 offsect = vec2 - vec0;
            return offsect.sqrMagnitude;
        }

    }
}