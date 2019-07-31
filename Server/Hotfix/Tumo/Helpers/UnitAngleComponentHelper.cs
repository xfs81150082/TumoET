using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETHotfix
{
    public static class UnitAngleComponentHelper
    {
        #region
        public static async ETVoid TurnTo(this UnitAnglesComponent self, Vector3 targetEulerAngles)
        {
            if ((self.EulerAnglesTarget - targetEulerAngles).magnitude < 0.1f)
            {
                return;
            }
            self.EulerAnglesTarget = targetEulerAngles;

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();

            await self.TurnAsync(self.EulerAnglesTarget);

            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        public static async ETTask TurnAsync(this UnitAnglesComponent self, Vector3 targetEulerAngles)
        {
            //self.BroadcastPath();

            await self.Entity.GetComponent<TurnComponent>().TurnToAsync(targetEulerAngles, self.CancellationTokenSource.Token);
        }

        // 从index找接下来3个点，广播
        public static void BroadcastPath(this UnitAnglesComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;
            M2C_PathfindingResult m2CPathfindingResult = new M2C_PathfindingResult();
            m2CPathfindingResult.X = unitPos.x;
            m2CPathfindingResult.Y = unitPos.y;
            m2CPathfindingResult.Z = unitPos.z;
            m2CPathfindingResult.Id = unit.Id;

                Vector3 v = self.EulerAnglesTarget;
                m2CPathfindingResult.Xs.Add(v.x);
                m2CPathfindingResult.Ys.Add(v.y);
                m2CPathfindingResult.Zs.Add(v.z);

            MessageHelper.Broadcast(m2CPathfindingResult);
        }
        #endregion


    }
}
