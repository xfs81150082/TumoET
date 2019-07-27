using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class ServerMoveComponentHelper
    {
        public static void Move(this ServerMoveComponent self, Move_Map move_Map)
        {
            if (Math.Abs(move_Map.V) > 0.05f)
            {
                if (!self.isStart1)
                {
                    self.startTime1 = TimeHelper.Now();
                    self.isStart1 = true;
                }

                self.offsetTime1 = TimeHelper.Now() - self.startTime1;

                if (self.offsetTime1 > self.resTime)
                {
                    float dx = (float)Math.Cos(self.GetParent<Unit>().eulerAngles.y) * move_Map.V * self.moveSpeed /** self.resTime / 1000*/;
                    float dz = (float)Math.Sin(self.GetParent<Unit>().eulerAngles.y) * move_Map.V * self.moveSpeed /** self.resTime / 1000*/;

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

                    Console.WriteLine(" ServerMoveComponentHelper-51-px/pz: " + self.GetParent<Unit>().Id + " : ( " + dx + " , " + dz + ")" + " / ( " + px + " , " + 0 + " , " + pz + ")");
                    Console.WriteLine(" ServerMoveComponentHelper-52-posx/posz: " + self.GetParent<Unit>().Id + " : ( " + dx + " , " + dz + ")" + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");

                    self.GetParent<Unit>().GetComponent<UnitPathComponent>().MoveTo(target).Coroutine();

                    //self.MoveToAsync(target).Coroutine();

                    self.isStart1 = false;

                    Console.WriteLine(" ServerMoveComponentHelper-59-posx/posz: " + self.GetParent<Unit>().Id + " : ( " + dx + " , " + dz + ")" + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
                }

            }
            else
            {
                if (self.isStart1)
                {
                    self.isStart1 = false;

                    Console.WriteLine(" ServerMoveComponent-92: " + self.isStart1 + " / " + self.startTime1);
                }
            }
        }

        public static void Trun(this ServerMoveComponent self, Move_Map move_Map)
        {
            if (Math.Abs(move_Map.H) > 0.05f)
            {
                if (!self.isStart2)
                {
                    self.startTime2 = TimeHelper.Now();
                    self.isStart2 = true;
                }

                self.offsetTime2 = TimeHelper.Now() - self.startTime2;

                if (self.offsetTime2 > self.resTime)
                {
                    float ay = self.GetParent<Unit>().eulerAngles.y + move_Map.H * self.roteSpeed;

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
        /// <summary>
        /// 异步 移动到 目标点(一个点)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static async ETTask MoveToAsync(this ServerMoveComponent self, Vector3 target)
        {
            /// 发送 目标点坐标 给客户端
            //self.BroadcastPath(target);

            Console.WriteLine(" ServerMoveComponentHelper-130-posx/posz: " + self.GetParent<Unit>().Id + " : ( "  + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");

            ///等待 服务器里 Unit 移动到 目标点坐标
            //await self.Entity.GetComponent<MoveComponent>().MoveToAsync(target, self.CancellationTokenSource.Token);
            await self.GetParent<Unit>().GetComponent<MoveComponent>().MoveToAsync(target, self.CancellationTokenSource.Token);

            Console.WriteLine(" ServerMoveComponentHelper-135-posx/posz: " + self.GetParent<Unit>().Id + " : ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
        }

        ///广播 一个坐标点
        public static void BroadcastPath(this ServerMoveComponent self, Vector3 target)
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
