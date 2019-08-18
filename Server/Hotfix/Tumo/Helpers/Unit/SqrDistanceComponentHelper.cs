using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class SqrDistanceComponentHelper
    {
        #region 行为树模式
        public static void SqrDistance(this SqrDistanceComponent self)
        {
            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();
            UnitType unitType = self.GetParent<Unit>().UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    if (aoiUnit.enemyIds.MovesSet.Count > 0)
                    {
                        Unit[] units1 = Game.Scene.GetComponent<MonsterUnitComponent>().GetAllByIds(aoiUnit.enemyIds.MovesSet.ToArray());

                        //Console.WriteLine(" SqrDistance-19-Player:" + aoiUnit.enemyIds.MovesSet.Count + " / " + units1.Length);

                        self.SqrDistance(units1);
                    }
                    break;
                case UnitType.Monster:
                    if (aoiUnit.playerIds.MovesSet.Count > 0)
                    {
                        Unit[] units2 = Game.Scene.GetComponent<UnitComponent>().GetAllByIds(aoiUnit.playerIds.MovesSet.ToArray());

                        //Console.WriteLine(" SqrDistance-25-Monster:" + aoiUnit.playerIds.MovesSet.Count + " / " + units2.Length);

                        self.SqrDistance(units2);
                    }
                    break;
                default:
                    break;
            }
        }

        static void SqrDistance(this SqrDistanceComponent self, Unit[] units)
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
            //Console.WriteLine(" SqrDistanceHelper-62-Id:Type/neastId/neastDis: " + self.GetParent<Unit>().Id + " : " + self.GetParent<Unit>().UnitType + " / " + self.neastUnit.Id + " / " + self.neastDistance);
        }

        public static Unit NeastUnit(Unit unit, Unit[] units)
        {
            Unit obj = null;
            if (!unit.GetComponent<AttackComponent>().isDeath)
            {
                float dis = float.PositiveInfinity;
                foreach (Unit tem in units)
                {
                    if (!tem.GetComponent<AttackComponent>().isDeath)
                    {
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
                    else
                    {
                        //Console.WriteLine(" SqrDistance-77:" + tem.UnitType + " / " + tem.GetComponent<RecoverComponent>().isDeath);
                    }
                }
            }
            else
            {
                //Console.WriteLine(" SqrDistance-79:" + unit.UnitType + " / " + unit.GetComponent<RecoverComponent>().isDeath);
            }
            return obj;
        }

        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            Vector3 vec0 = vec1;
            vec0.y = vec2.y;
            Vector3 offsect = vec2 - vec0;
            return offsect.sqrMagnitude;
        }

        #endregion

        //public static void UpdateIsAttacking(this SqrDistanceComponent self)
        //{
        //    if (self.neastDistance < self.enterAttackSqr)
        //    {
        //        self.GetParent<Unit>().GetComponent<AttackComponent>().isBattling = true;

        //        if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
        //        {
        //            self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol = false;
        //        }

        //        if (!self.isShow)
        //        {
        //            Console.WriteLine(" SqrDistanceHelper-117- type: " + self.GetParent<Unit>().UnitType + " 进入 战斗。" + self.GetParent<Unit>().GetComponent<AttackComponent>().isBattling);
        //            if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
        //            {
        //                Console.WriteLine(" SqrDistanceHelper-120- type: " + self.GetParent<Unit>().UnitType + " 进入 追击。" + self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol);
        //            }
        //            self.isShow = true;
        //        }
        //    }
        //    else
        //    {
        //        self.GetParent<Unit>().GetComponent<AttackComponent>().isBattling = false;

        //        if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
        //        {
        //            self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol = true;
        //        }

        //        if (self.isShow)
        //        {
        //            Console.WriteLine(" SqrDistanceHelper-136- type: " + self.GetParent<Unit>().UnitType + " 离开 战斗。" + self.GetParent<Unit>().GetComponent<AttackComponent>().isBattling);
        //            if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
        //            {
        //                Console.WriteLine(" SqrDistanceHelper-139- type: " + self.GetParent<Unit>().UnitType + " 离开 追击。" + self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol);
        //            }
        //            self.isShow = false;
        //        }

        //    }
        //}


    }
}
