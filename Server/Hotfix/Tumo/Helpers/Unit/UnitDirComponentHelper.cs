using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETHotfix
{
    public static class UnitDirComponentHelper
    {
        public static async ETVoid Move(this UnitDirComponent self, Vector3 clientPos, Vector3 dir)
        {
            if (dir.magnitude < 0.01f)
            {
                return;
            }

            //如果 clientPos与serverPos相关较大，则视客户端异常，进行较正坐标；
            //如果 clientPos与serverPos相关较小，用视客户端坐标，更新服务端坐标；
            //TOTO

            Unit unit = self.GetParent<Unit>();
            self.TargetPosition = clientPos + dir;

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();

            await self.Entity.GetComponent<MovePositionComponent>().MoveToAsync(self.TargetPosition, self.CancellationTokenSource.Token);

            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

    }
}
