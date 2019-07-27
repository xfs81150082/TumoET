using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETHotfix
{
    public static class KeyboardComponentHelper
    {
        public static void KeyboardVMove(this KeyboardPathComponent self, float V)
        {
            if (Math.Abs(V) > 0.05f)
            {
                if (!self.isStart1)
                {
                    self.startTime1 = TimeHelper.Now();
                    self.isStart1 = true;
                }

                self.offsetTime1 = TimeHelper.Now() - self.startTime1;

                if (self.offsetTime1 > self.resTime)
                {
                    float dx = (float)Math.Cos(self.GetParent<Unit>().eulerAngles.y) * V * self.moveSpeed * self.resTime / 1000;
                    float dz = (float)Math.Sin(self.GetParent<Unit>().eulerAngles.y) * V * self.moveSpeed * self.resTime / 1000;

                    float px = self.GetParent<Unit>().Position.x + dx;
                    float pz = self.GetParent<Unit>().Position.z + dz;
                    float mapWide = Game.Scene.GetComponent<AoiGridComponent>().mapWide;

                    if (px > mapWide / 2)
                    {
                        px = mapWide / 2;
                    }
                    if (px < -mapWide / 2)
                    {
                        px = -mapWide / 2;
                    }
                    if (pz > mapWide / 2)
                    {
                        pz = mapWide / 2;
                    }
                    if (pz < -mapWide / 2)
                    {
                        pz = -mapWide / 2;
                    }

                    Vector3 target = new Vector3(px, 0, pz);

                    self.MoveTo(target).Coroutine();

                    self.isStart1 = false;

                    Console.WriteLine(" KeyboardComponent-56: " + self.GetParent<Unit>().UnitType + " : ( " + dx + " , " + dz + ")" + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
                }

            }
            else
            {
                if (self.isStart1)
                {
                    self.isStart1 = false;

                    Console.WriteLine(" KeyboardComponent-92: " + self.isStart1 + " / " + self.startTime1);
                }
            }
        }

        public static void KeyboardHTrun(this KeyboardPathComponent self, float H)
        {
            if (Math.Abs(H) > 0.05f)
            {
                if (!self.isStart2)
                {
                    self.startTime2 = TimeHelper.Now();
                    self.isStart2 = true;
                }

                self.offsetTime2 = TimeHelper.Now() - self.startTime2;

                if (self.offsetTime2 > self.resTime)
                {
                    float ay = self.GetParent<Unit>().eulerAngles.y + H * self.roteSpeed * self.resTime / 1000;

                    if (ay > 360)
                    {
                        ay -= 360;
                    }
                    if (ay < 0)
                    {
                        ay += 360;
                    }

                    self.GetParent<Unit>().eulerAngles = new Vector3(0, ay, 0);

                    self.isStart2 = false;

                    Console.WriteLine(" ServerMoveComponentHelper-99: " + self.GetParent<Unit>().UnitType + " / " + self.GetParent<Unit>().Id + " : ( " + 0 + " , " + ay + " , " + 0 + ")");
                }

            }
            else
            {
                if (self.isStart2)
                {
                    self.isStart2 = false;

                    Console.WriteLine(" ServerMoveComponentHelper-109: " + self.isStart2 + " / " + self.startTime2);
                }
            }

        }

        #region
        public static async ETVoid MoveTo(this KeyboardPathComponent self, Vector3 target)
        {
            if ((self.Target - target).magnitude < 0.1f)
            {
                return;
            }

            self.Target = target;

            self.CancellationTokenSource?.Cancel();
            self.CancellationTokenSource = new CancellationTokenSource();
            await self.MoveToAsync(self.Target);
            self.CancellationTokenSource.Dispose();
            self.CancellationTokenSource = null;
        }

        /// <summary>
        /// 异步 移动到 目标点(一个点)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static async ETTask MoveToAsync(this KeyboardPathComponent self, Vector3 target)
        {
            /// 发送 目标点坐标 给客户端
            self.BroadcastPath(target);

            ///等待 服务器里 Unit 移动到 目标点坐标
            await self.Entity.GetComponent<MoveComponent>().MoveToAsync(target, self.CancellationTokenSource.Token);
        }

        ///广播 一个坐标点
        public static void BroadcastPath(this KeyboardPathComponent self, Vector3 target)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;
            M2C_PathfindingResult m2CPathfindingResult = new M2C_PathfindingResult();
            m2CPathfindingResult.X = unitPos.x;
            m2CPathfindingResult.Y = unitPos.y;
            m2CPathfindingResult.Z = unitPos.z;
            m2CPathfindingResult.Id = unit.Id;

            m2CPathfindingResult.Xs.Add(target.x);
            m2CPathfindingResult.Ys.Add(target.y);
            m2CPathfindingResult.Zs.Add(target.z);

            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();

            MessageHelper.Broadcast(m2CPathfindingResult, aoiUnit.playerIds.MovesSet.ToArray());
        }
#endregion


    }
}
