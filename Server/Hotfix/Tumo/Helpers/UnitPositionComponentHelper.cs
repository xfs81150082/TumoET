using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETHotfix
{
    public static class UnitPositionComponentHelper
    {
        #region
        public static async ETVoid MoveTo(this UnitPositionComponent self, Vector3 target)
        {
            if ((self.TargetPosition - target).magnitude < 0.1f)
            {
                return;
            }

            self.TargetPosition = target;

            //Unit unit = self.GetParent<Unit>();
            //PathfindingComponent pathfindingComponent = Game.Scene.GetComponent<PathfindingComponent>();
            //self.ABPath = ComponentFactory.Create<ABPathWrap, Vector3, Vector3>(unit.Position, new Vector3(target.x, target.y, target.z));
            //pathfindingComponent.Search(self.ABPath);
            //Log.Debug($"find result: {self.ABPath.Result.ListToString()}");

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();
            //await self.MoveAsync(self.ABPath.Result);

            await self.MoveAsync();

            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        public static async ETTask MoveAsync(this UnitPositionComponent self)
        {
            if (self.TargetPosition == null)
            {
                return;
            }
            // 第一个点是unit的当前位置，所以不用发送
            // 每移动3个点发送下3个点给客户端

            Vector3 v3 = self.TargetPosition;

            self.BroadcastPath(v3);

            await self.Entity.GetComponent<MoveComponent>().MoveToAsync(v3, self.CancellationTokenSource.Token);

        }

        // 从index找接下来3个点，广播
        public static void BroadcastPath(this UnitPositionComponent self, Vector3 targetPos)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;
            M2C_PathfindingResult m2CPathfindingResult = new M2C_PathfindingResult();
            m2CPathfindingResult.X = unitPos.x;
            m2CPathfindingResult.Y = unitPos.y;
            m2CPathfindingResult.Z = unitPos.z;
            m2CPathfindingResult.Id = unit.Id;

            Vector3 v = self.TargetPosition;
            m2CPathfindingResult.Xs.Add(v.x);
            m2CPathfindingResult.Ys.Add(v.y);
            m2CPathfindingResult.Zs.Add(v.z);

            MessageHelper.Broadcast(m2CPathfindingResult);
        }
        #endregion




    }
}
