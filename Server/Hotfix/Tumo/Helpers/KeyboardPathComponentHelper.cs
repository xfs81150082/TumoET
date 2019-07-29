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
        public static void KeyboardVH(this KeyboardPathComponent self, float V,float H)
        {
            Console.WriteLine(" KeyboardVMove-16-v: " + (KeyType)self.GetParent<Unit>().Id + " : ( " + V + ") ");

            if (self.offsetTimeMove > self.resTime)
            {
                self.offsetTimeMove = 0;
            }

            if (Math.Abs(V) > 0.05f || (Math.Abs(H) > 0.05f))
            {
                if (self.offsetTimeMove == 0)
                {
                    self.startTimeMove = TimeHelper.Now();

                    Vector3 targetEulerAngles = self.GetTargetEulerAngles(H);
                    Vector3 targetPosition = self.GetTargetPosition(V);

                    self.MoveToEulerAngles(targetEulerAngles).Coroutine();                 ///等待//服务器里 Unit 旋转到 目标点角色
                    self.MoveToPosition(targetPosition).Coroutine();                       ///等待//服务器里 Unit 移动到 目标点坐标

                    self.MoveToClient(targetEulerAngles, targetPosition);                  ///通知client  协程移动

                    Console.WriteLine(" KeyboardVMove-36: " + self.GetParent<Unit>().UnitType + " / ( " + targetPosition.x + " , " + 0 + " , " + targetPosition.z + ")");
                }
            }
            else
            {
                if (self.turnCancellationTokenSource != null)
                {
                    self.turnCancellationTokenSource?.Cancel();
                    Console.WriteLine(" KeyboardHTurn-176: turnCancellationTokenSource is cancel. ");
                }

                if (self.moveCancellationTokenSource != null)
                {
                    self.moveCancellationTokenSource?.Cancel();
                    Console.WriteLine(" KeyboardVMove-45: moveCancellationTokenSource is cancel. ");
                }

                ///通知client 停止 协程移动
                ///ToTo
                //self.MoveToClient(self.GetParent<Unit>().Position);

                if (self.isStartMove)
                {
                    self.isStartMove = false;

                    Console.WriteLine(" KeyboardVMove-56: " + self.isStartMove + " / " + self.isStartMove);
                }
            }

            self.offsetTimeMove = TimeHelper.Now() - self.startTimeMove + 1;
        }

        #region  KeyboardVMove
        //public static void KeyboardVMove(this KeyboardPathComponent self, float V)
        //{
        //    Console.WriteLine(" KeyboardVMove-16-v: " + (KeyType)self.GetParent<Unit>().Id + " : ( " + V + ") ");

        //    if (self.offsetTimeMove > self.resTime)
        //    {
        //        self.offsetTimeMove = 0;
        //    }

        //    if (Math.Abs(V) > 0.05f)
        //    {
        //        if (self.offsetTimeMove == 0)
        //        {
        //            self.startTimeMove = TimeHelper.Now();

        //            Vector3 targetPosition = self.GetTargetPosition(V);

        //            self.MoveToPosition(targetPosition).Coroutine();                       ///等待//服务器里 Unit 移动到 目标点坐标

        //            //self.MoveToClient(targetPosition);

        //            Console.WriteLine(" KeyboardVMove-36: " + self.GetParent<Unit>().UnitType + " / ( " + targetPosition.x + " , " + 0 + " , " + targetPosition.z + ")");
        //        }
        //    }
        //    else
        //    {
        //        if (self.moveCancellationTokenSource != null)
        //        {
        //            self.moveCancellationTokenSource?.Cancel();
        //            Console.WriteLine(" KeyboardVMove-45: moveCancellationTokenSource is cancel. ");
        //        }

        //        ///通知client 停止 协程移动
        //        ///ToTo
        //        //self.MoveToClient(self.GetParent<Unit>().Position);

        //        if (self.isStartMove)
        //        {
        //            self.isStartMove = false;

        //            Console.WriteLine(" KeyboardVMove-56: " + self.isStartMove + " / " + self.isStartMove);
        //        }
        //    }

        //    self.offsetTimeMove = TimeHelper.Now() - self.startTimeMove + 1;
        //}

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

        static async ETVoid MoveToPosition(this KeyboardPathComponent self, Vector3 targetPosition)
        {

            if ((self.targetPosition - targetPosition).magnitude < 0.1f)
            {
                return;
            }

            self.targetPosition = targetPosition;

            self.moveCancellationTokenSource?.Cancel();
            self.moveCancellationTokenSource = new CancellationTokenSource();
        
            //await self.MoveToAsync();
            ///等待 服务器里 Unit 移动到 目标点坐标
            await self.Entity.GetComponent<MoveComponent>().MoveToAsync(self.targetPosition, self.moveCancellationTokenSource.Token);

            self.moveCancellationTokenSource.Dispose();
            self.moveCancellationTokenSource = null;
        }
        #endregion

        #region KeyboardHTurn
        //public static void KeyboardHTurn(this KeyboardPathComponent self, float H)
        //{
        //    Console.WriteLine(" KeyboardHTurn-148-h: " + (KeyType)self.GetParent<Unit>().Id + " : ( " + H + ") ");

        //    if (self.offsetTimeTurn > self.resTime)
        //    {
        //        self.offsetTimeTurn = 0;
        //    }

        //    if (Math.Abs(H) > 0.05f)
        //    {
        //        if (self.offsetTimeTurn == 0)
        //        {
        //            self.startTimeTurn = TimeHelper.Now();

        //            Vector3 targetEulerAngles = self.GetTargetEulerAngles(H);

        //            self.MoveToEulerAngles(targetEulerAngles).Coroutine();                 ///等待//服务器里 Unit 旋转到 目标点角色

        //            //self.MoveToClient(targetEulerAngles);

        //            Console.WriteLine(" KeyboardHTurn-167: " + self.GetParent<Unit>().UnitType + " / ( " + 0 + " , " + targetEulerAngles.y + " , " + 0 + ")");
        //        }
        //    }
        //    else
        //    {
        //        if (self.turnCancellationTokenSource != null)
        //        {
        //            self.turnCancellationTokenSource?.Cancel();
        //            Console.WriteLine(" KeyboardHTurn-176: turnCancellationTokenSource is cancel. ");
        //        }

        //        ///通知client 停止 协程移动
        //        ///ToTo
        //        //self.MoveToClient(self.GetParent<Unit>().Position);

        //        if (self.isStartTurn)
        //        {
        //            self.isStartTurn = false;

        //            Console.WriteLine(" KeyboardHTurn-186: " + self.isStartTurn + " / " + self.isStartTurn);
        //        }
        //    }

        //    self.offsetTimeTurn = TimeHelper.Now() - self.startTimeTurn + 1;
        //}

        static Vector3 GetTargetEulerAngles(this KeyboardPathComponent self, float H)
        {
            float dy = H * self.roteSpeed * self.resTime / 1000;

            float ay = self.GetParent<Unit>().eulerAngles.y + dy;           

            Console.WriteLine(" GetTargetEulerAngles-208-dy/ay/eul: " + self.GetParent<Unit>().UnitType + " :  " + dy + " / " + ay + " / " + self.GetParent<Unit>().eulerAngles.y + ")");

            return new Vector3(0, ay, 0);
        }

        static async ETVoid MoveToEulerAngles(this KeyboardPathComponent self, Vector3 targetEulerAngles)
        {
            if (Math.Abs(self.targetEulerAngles.y - targetEulerAngles.y) < 0.1f)
            {
                return;
            }

            self.targetEulerAngles = targetEulerAngles;

            self.turnCancellationTokenSource?.Cancel();
            self.turnCancellationTokenSource = new CancellationTokenSource();

            ///等待 服务器里 Unit 旋转到 目标点角色
            await self.Entity.GetComponent<TurnComponent>().MoveToAsync(self.targetEulerAngles, self.turnCancellationTokenSource.Token);

            self.turnCancellationTokenSource.Dispose();
            self.turnCancellationTokenSource = null;
        }
        #endregion

        #region
        public static void MoveToClient(this KeyboardPathComponent self, Vector3 targetEulrerAngles, Vector3 targetPosition)
        {
            /// 发送 目标点坐标 给客户端
            self.BroadcastPath(targetEulrerAngles, targetPosition);
        }
 
        ///广播 一个坐标点
        static void BroadcastPath(this KeyboardPathComponent self, Vector3 targetEulrerAngles, Vector3 targetPosition)
        {
            Unit unit = self.GetParent<Unit>();
            Vector3 unitPos = unit.Position;
            Vector3 unitEul = unit.eulerAngles;
            M2C_ServerPathResult m2CPathfindingResult = new M2C_ServerPathResult();
            m2CPathfindingResult.X = unitPos.x;
            m2CPathfindingResult.Y = unitPos.y;
            m2CPathfindingResult.Z = unitPos.z;
            m2CPathfindingResult.W = unitEul.y;
            m2CPathfindingResult.Id = unit.Id;

            m2CPathfindingResult.Xs.Add(targetPosition.x);
            m2CPathfindingResult.Ys.Add(targetPosition.y);
            m2CPathfindingResult.Zs.Add(targetPosition.z);
            m2CPathfindingResult.Ws.Add(targetEulrerAngles.y);

            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();

            MessageHelper.Broadcast(m2CPathfindingResult, aoiUnit.playerIds.MovesSet.ToArray());
        }
        #endregion

    }
}
