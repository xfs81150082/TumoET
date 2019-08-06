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

            SetIsWarring(self);
        }

        void SetIsWarring(SqrDistanceComponent self)
        {
            if (self.neastDistance < self.GetParent<Unit>().GetComponent<RecoverComponent>().enterWarringSqr)
            {
                self.GetParent<Unit>().GetComponent<RecoverComponent>().isWarring = true;

                //self.GetParent<Unit>().GetComponent<AttackComponent>().isAttacking = true;

                if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
                {
                    self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol = false;
                }
            }
            else
            {
                self.GetParent<Unit>().GetComponent<RecoverComponent>().isWarring = false;

                //self.GetParent<Unit>().GetComponent<AttackComponent>().isAttacking = false;

                if (self.GetParent<Unit>().GetComponent<PatrolComponent>() != null)
                {
                    self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol = true;
                }
            }
        }

    }
}