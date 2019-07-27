using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
    public static class UnitPathComponentHelper
    {
        #region
        public static async ETVoid MoveTo(this UnitPathComponent self, Vector3 target)
        {
            if ((self.Target - target).magnitude < 0.1f)
            {
                return;
            }

            self.Target = target;
            Unit unit = self.GetParent<Unit>();

            PathfindingComponent pathfindingComponent = Game.Scene.GetComponent<PathfindingComponent>();
            self.ABPath = ComponentFactory.Create<ABPathWrap, Vector3, Vector3>(unit.Position, new Vector3(target.x, target.y, target.z));
            pathfindingComponent.Search(self.ABPath);
            Log.Debug($"find result: {self.ABPath.Result.ListToString()}");

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();
            await self.MoveAsync(self.ABPath.Result);
            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        public static async ETTask MoveAsync(this UnitPathComponent self, List<Vector3> path)
        {
            if (path.Count == 0)
            {
                return;
            }
            // 第一个点是unit的当前位置，所以不用发送
            for (int i = 1; i < path.Count; ++i)
            {
                // 每移动3个点发送下3个点给客户端
                if (i % 3 == 1)
                {
                    self.BroadcastPath(path, i, 3);
                }
                Vector3 v3 = path[i];
                await self.Entity.GetComponent<MoveComponent>().MoveToAsync(v3, self.CancellationTokenSource.Token);
            }
        }

        // 从index找接下来3个点，广播
        public static void BroadcastPath(this UnitPathComponent self, List<Vector3> path, int index, int offset)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;
            M2C_PathfindingResult m2CPathfindingResult = new M2C_PathfindingResult();
            m2CPathfindingResult.X = unitPos.x;
            m2CPathfindingResult.Y = unitPos.y;
            m2CPathfindingResult.Z = unitPos.z;
            m2CPathfindingResult.Id = unit.Id;

            for (int i = 0; i < offset; ++i)
            {
                if (index + i >= self.ABPath.Result.Count)
                {
                    break;
                }
                Vector3 v = self.ABPath.Result[index + i];
                m2CPathfindingResult.Xs.Add(v.x);
                m2CPathfindingResult.Ys.Add(v.y);
                m2CPathfindingResult.Zs.Add(v.z);
            }
            MessageHelper.Broadcast(m2CPathfindingResult);
        }
        #endregion


        #region   20190726 新加  
        //public static async ETVoid MoveTo(this UnitPathComponent self, Move_Map move_Map)
        //{
        //    var timerComponent = Game.Scene.GetComponent<TimerComponent>();

        //    while (true)
        //    {
        //        if (Math.Abs(move_Map.V) > 0.05f || Math.Abs(move_Map.H) > 0.05f)
        //        {
        //            //float dx = (float)Math.Cos(self.GetParent<Unit>().eulerAngles.y) * move_Map.V * self.moveSpeed * 1000f / 1000;
        //            //float dz = (float)Math.Sin(self.GetParent<Unit>().eulerAngles.y) * move_Map.V * self.moveSpeed * 1000f / 1000;

        //            float dx = (float) move_Map.H * self.moveSpeed * 1000f / 1000;
        //            float dz = (float) move_Map.V * self.moveSpeed * 1000f / 1000;

        //            float px = self.GetParent<Unit>().Position.x + dx;
        //            float pz = self.GetParent<Unit>().Position.z + dz;
        //            float hmWide = 50.0f /*Game.Scene.GetComponent<AoiGridComponent>().mapWide / 2*/;

        //            if (px > hmWide) { px = hmWide; }
        //            if (px < -hmWide) { px = -hmWide; }
        //            if (pz > hmWide) { pz = hmWide; }
        //            if (pz < -hmWide) { pz = -hmWide; }

        //            Vector3 target = new Vector3(px, 0, pz);
        //            self.MoveToAsync(target).Coroutine();

        //            await timerComponent.WaitAsync(1000);

        //            Console.WriteLine(" UnitPathComponentHelper-106-position-px/pz: " + self.GetParent<Unit>().Id + " : ( " + dx + " , " + dz + ")" + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
        //            Console.WriteLine(" UnitPathComponentHelper-107-target-px/pz: " + self.GetParent<Unit>().Id + " : ( " + dx + " , " + dz + ")" + " / ( " + target.x + " , " + 0 + " , " + target.z + ")");
        //        }
        //        else
        //        {
        //            break;
        //        }

        //    }
        //}

        /// <summary>
        /// 异步 移动到 目标点(一个点)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        //public static async ETTask MoveToAsync(this UnitPathComponent self, Vector3 target)
        //{
        //    /// 发送 目标点坐标 给客户端
        //    self.BroadcastPath(target);

        //    ///等待 服务器里 Unit 移动到 目标点坐标
        //    await self.Entity.GetComponent<MoveComponent>().MoveToAsync(target, self.CancellationTokenSource.Token);
        //}
       
        /////广播 一个坐标点
        //public static void BroadcastPath(this UnitPathComponent self, Vector3 target)
        //{
        //    Unit unit = self.GetParent<Unit>();
        //    Vector3 unitPos = unit.Position;
        //    M2C_PathfindingResult m2CPathfindingResult = new M2C_PathfindingResult();
        //    m2CPathfindingResult.X = unitPos.x;
        //    m2CPathfindingResult.Y = unitPos.y;
        //    m2CPathfindingResult.Z = unitPos.z;
        //    m2CPathfindingResult.Id = unit.Id;

        //    m2CPathfindingResult.Xs.Add(target.x);
        //    m2CPathfindingResult.Ys.Add(target.y);
        //    m2CPathfindingResult.Zs.Add(target.z);

        //    AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();

        //    MessageHelper.Broadcast(m2CPathfindingResult, aoiUnit.playerIds.MovesSet.ToArray());
        //}
        #endregion


    }
}
