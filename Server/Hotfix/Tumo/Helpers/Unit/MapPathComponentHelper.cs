using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETHotfix
{
    public static class MapPathComponentHelper
    {
        public static async ETVoid MoveTo(this MapPathComponent self, Vector3 target)
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

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();

            //ToTo 同步到服务端 本身坐标和向量坐标
            await self.MoveAsync(self.ABPath.Result);

            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        public static async ETTask MoveAsync(this MapPathComponent self, List<Vector3> path)
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

        //广播 本身坐标和向量坐标
        public static void BroadcastPath(this MapPathComponent self, List<Vector3> path, int index, int offset)
        {

            M2C_KeyboardPosition m2C_KeyboardPosition = new M2C_KeyboardPosition();
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;

            m2C_KeyboardPosition.X = unitPos.x;
            m2C_KeyboardPosition.Y = unitPos.y;
            m2C_KeyboardPosition.Z = unitPos.z;
            m2C_KeyboardPosition.Id = unit.Id;

            for (int i = 0; i < offset; ++i)
            {
                if (index + i >= self.ABPath.Result.Count)
                {
                    break;
                }
                Vector3 v = self.ABPath.Result[index + i];
                m2C_KeyboardPosition.Xs.Add(v.x);
                m2C_KeyboardPosition.Ys.Add(v.y);
                m2C_KeyboardPosition.Zs.Add(v.z);
            }
            MessageHelper.Broadcast(m2C_KeyboardPosition, unit.GetComponent<AoiUnitComponent>().playerIds.MovesSet.ToArray());

            Console.WriteLine(" UnitDirComponentHelper-82-MoveToClient:  " + m2C_KeyboardPosition.Id + " ( " + m2C_KeyboardPosition.X + ", " + m2C_KeyboardPosition.Y + ", " + m2C_KeyboardPosition.Z + ")");
        }

    }
}
