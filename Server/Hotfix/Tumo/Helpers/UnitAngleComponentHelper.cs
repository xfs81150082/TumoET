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
            self.BroadcastPath();

            await self.Entity.GetComponent<TurnAnglesComponent>().TurnToAsync(targetEulerAngles, self.CancellationTokenSource.Token);
        }

        // 从index找接下来3个点，广播
        public static void BroadcastPath(this UnitAnglesComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitEul = unit.EulerAngles;
            M2C_KeyboardEulerAnglers m2C_KeyboardEulerAnglers = new M2C_KeyboardEulerAnglers();
            m2C_KeyboardEulerAnglers.X = unitEul.x;
            m2C_KeyboardEulerAnglers.Y = unitEul.y;
            m2C_KeyboardEulerAnglers.Z = unitEul.z;
            m2C_KeyboardEulerAnglers.Id = unit.Id;

            Vector3 v = self.EulerAnglesTarget;
            m2C_KeyboardEulerAnglers.Xs.Add(v.x);
            m2C_KeyboardEulerAnglers.Ys.Add(v.y);
            m2C_KeyboardEulerAnglers.Zs.Add(v.z);

            MessageHelper.Broadcast(m2C_KeyboardEulerAnglers);
        }
        #endregion


    }
}
