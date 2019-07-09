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
            self.SqrDistance();           

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
