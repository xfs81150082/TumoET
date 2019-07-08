using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class SqrDistanceComponentUpdateSystem : UpdateSystem<SqrDistanceComponent>
    {
        public override void Update(SqrDistanceComponent self)
        {
            UnitType unitType = self.GetParent<Unit>().UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    Unit[] units = Game.Scene.GetComponent<AoiGridComponent>().GetMonsterUnits(self.GetParent<AoiUnitComponent>().NineGridIds.ToArray());
                    self.SqrDistance(units);
                    break;
                case UnitType.Monster:
                    Unit[] units2 = Game.Scene.GetComponent<AoiGridComponent>().GetPlayerUnits(self.GetParent<AoiUnitComponent>().NineGridIds.ToArray());
                    Unit[] units3 = Game.Scene.GetComponent<AoiGridComponent>().GetNpcerUnits(self.GetParent<AoiUnitComponent>().NineGridIds.ToArray());
                    self.SqrDistance(units2.Union(units3).ToArray());
                    break;
                case UnitType.Npc:
                    Unit[] units6 = Game.Scene.GetComponent<AoiGridComponent>().GetMonsterUnits(self.GetParent<AoiUnitComponent>().NineGridIds.ToArray());
                    self.SqrDistance(units6);
                    break;
                default:
                    break;
            }

            if (self.neastDistance > self.seeDistance)
            {
                SetIsAttacking(self.GetParent<Unit>(), false);
                SetIsPatrol(self.GetParent<Unit>(), true);
            }
            else
            {
                SetIsAttacking(self.GetParent<Unit>(), true);
                SetIsPatrol(self.GetParent<Unit>(), false);
            }
        }

        void SetIsAttacking(Unit unit, bool boo)
        {
            if (unit.GetComponent<AttackComponent>() != null)
            {
                unit.GetComponent<AttackComponent>().isAttacking = boo;
            }
        }
        void SetIsPatrol(Unit unit , bool boo)
        {
            if (unit.GetComponent<PatrolComponent>() != null)
            {
                unit.GetComponent<PatrolComponent>().isPatrol = boo;
            }
        }
        void SetIsSeeing(Unit unit, bool boo)
        {
            if (unit.GetComponent<SeeComponent>() != null)
            {
                unit.GetComponent<SeeComponent>().isSee = boo;
            }
        }

    }
}
