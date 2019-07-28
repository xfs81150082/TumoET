using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETHotfix
{
    public static class KeyboardPathComponentHelper
    {
        #region  KeyboardVHMove
        public static void KeyboardVHMove(this KeyboardPathComponent self, float V, float H)
        {
            if (self.offsetTimeMove > self.resTime)
            {
                self.offsetTimeMove = 0;
            }

            if (Math.Abs(V) > 0.05f)
            {
                if (self.offsetTimeMove == 0)
                {
                    self.startTimeMove = TimeHelper.Now();

                    Vector3 targetPosition = self.GetTargetPosition(V);
                    Vector3 targetEulerAngles = self.GetTargetEulerAngles(H);


                    self.MoveTo(targetPosition , targetEulerAngles).Coroutine();                 //服务器移动

                    self.MoveToClient(targetPosition);

                    Console.WriteLine(" KeyboardPathComponentHelper-56: " + self.GetParent<Unit>().UnitType + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
                }
            }
            else
            {
                if (self.moveCancellationTokenSource != null)
                {
                    self.moveCancellationTokenSource?.Cancel();
                    Console.WriteLine(" KeyboardPathComponentHelper-61: moveCancellationTokenSource is cancel. ");
                }

                ///通知client 停止 协程移动
                ///ToTo
                //self.MoveToClient(self.GetParent<Unit>().Position);

                Console.WriteLine(" KeyboardPathComponentHelper-65: moveCancellationTokenSource is cancel. ");

                if (self.isStartMove)
                {
                    self.isStartMove = false;

                    Console.WriteLine(" KeyboardPathComponentHelper-71: " + self.isStartMove + " / " + self.isStartMove);
                }
            }

            self.offsetTimeMove = TimeHelper.Now() - self.startTimeMove + 1;
        }

        /// <summary>
        /// 得到 新目标点
        /// </summary>
        /// <param name="self"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        static Vector3 GetTargetPosition(this KeyboardPathComponent self, float V)
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

            return new Vector3(px, 0, pz);
        }
        static Vector3 GetTargetEulerAngles(this KeyboardPathComponent self, float H)
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

            return new Vector3(0, ay, 0);
        }

        public static void MoveToClient(this KeyboardPathComponent self, Vector3 target)
        {
            //if ((self.moveTarget - target).magnitude < 0.1f)
            //{
            //    return;
            //}

            self.targetPosition = target;

            /// 发送 目标点坐标 给客户端
            self.BroadcastPath(target);
        }

        public static async ETVoid MoveTo(this KeyboardPathComponent self, Vector3 targetPosition, Vector3 targetEulerAngles)
        {
            if ((self.targetPosition - targetPosition).magnitude < 0.1f)
            {
                self.moveCancellationTokenSource?.Cancel();
                return;
            }

            self.targetPosition = targetPosition;
            self.targetEulerAngles = targetEulerAngles;

            self.moveCancellationTokenSource?.Cancel();
            self.moveCancellationTokenSource = new CancellationTokenSource();
            await self.MoveToAsync();
            self.moveCancellationTokenSource.Dispose();
            self.moveCancellationTokenSource = null;
        }

        /// <summary>
        /// 异步 移动到 目标点(一个点)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static async ETTask MoveToAsync(this KeyboardPathComponent self)
        {
            /// 发送 目标点坐标 给客户端
            //self.BroadcastPath(target);

            ///等待 服务器里 Unit 移动到 目标点坐标
            await self.Entity.GetComponent<MoveComponent>().MoveToAsync(self.targetPosition, self.moveCancellationTokenSource.Token);
        }

        ///广播 一个坐标点
        public static void BroadcastPath(this KeyboardPathComponent self, Vector3 target)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;
            M2C_ServerPathResult m2CPathfindingResult = new M2C_ServerPathResult();
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
