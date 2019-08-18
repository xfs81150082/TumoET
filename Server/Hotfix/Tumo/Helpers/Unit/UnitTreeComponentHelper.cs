using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class UnitTreeComponentHelper
    {
        public static bool CheckDeath(this UnitTreeComponent self)
        {
            return self.GetParent<Unit>().GetComponent<AttackComponent>().CheckDeath();
        }

        public static void RemoveUnit(this UnitTreeComponent self)
        {
            self.GetParent<Unit>().GetComponent<AttackComponent>().RemoveUnit();
        }

        public static void GetExpAndCoin(this UnitTreeComponent self)
        {
            self.GetParent<Unit>().GetComponent<AttackComponent>().GetExpAndCoin();
        }

        public static void RecoverHp(this UnitTreeComponent self)
        {
            self.GetParent<Unit>().GetComponent<RecoverComponent>().RecoverHp();
        }

        public static void CheckSeekTarget(this UnitTreeComponent self)
        {
            //SqrDistanceComponent sqrDistance = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>();
            //sqrDistance.SqrDistance();

            self.GetParent<Unit>().GetComponent<SeekComponent>().CheckSeekTarget();
        }

        public static bool CheckAttckTarget(this UnitTreeComponent self)
        {
            return self.GetParent<Unit>().GetComponent<AttackComponent>().CheckAttackTarget();
        }

        public static void SeekTarget(this UnitTreeComponent self)
        {
            self.GetParent<Unit>().GetComponent<SeekComponent>().SeekTarget();
        }

        public static bool CheckBattling(this UnitTreeComponent self)
        {
            SqrDistanceComponent sqrDistance = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>();

            sqrDistance.SqrDistance();

            return self.GetParent<Unit>().GetComponent<AttackComponent>().CheckIsBattling();
        }

        public static void AttackTarget(this UnitTreeComponent self)
        {
            self.GetParent<Unit>().GetComponent<AttackComponent>().AttackTarget();
        }

        public static void Patrol(this UnitTreeComponent self)
        {
            self.GetParent<Unit>().GetComponent<PatrolComponent>().UpdatePatrol();
        }

    }
}
