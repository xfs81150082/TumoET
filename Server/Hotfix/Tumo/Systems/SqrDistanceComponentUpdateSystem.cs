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

            SetIsAttacking(self);
        }

        void SetIsAttacking(SqrDistanceComponent self)
        {
            if (self.neastDistance < self.GetParent<Unit>().GetComponent<AttackComponent>().enterAttackDistance)
            {
                self.GetParent<Unit>().GetComponent<AttackComponent>().isAttacking = true;

                if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
                {
                    self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol = false;
                }
            }
            else
            {
                self.GetParent<Unit>().GetComponent<AttackComponent>().isAttacking = false;

                if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
                {
                    self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol = true;
                }
            }
        }

    }
}