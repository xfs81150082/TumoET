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
        public static void KeyboardVH(this KeyboardPathComponent self, float V, float H)
        {
            if (Math.Abs(V) > 0.05f)
            {
                self.v = V;
                self.vCount = 4;
            }
            if (Math.Abs(H) > 0.05f)
            {
                self.h = H;
                self.hCount = 4;
            }

            Console.WriteLine(" KeyboardPathComponentHelper-26-vh: " + (KeyType)self.GetParent<Unit>().Id + " : ( " + V + "/" + H + ") ");
        }

        public static void KeyboardMoveTurn(this KeyboardPathComponent self)
        {
            if (self.offsetTimeMove > self.resTime)
            {
                self.offsetTimeMove = 0;
            }

            if (Math.Abs(self.v) > 0.3f || (Math.Abs(self.h) > 0.3f))
            {
                if (self.offsetTimeMove == 0)
                {
                    self.startTimeMove = TimeHelper.Now();

                    Vector3 targetEulerAngles = self.GetTargetEulerAngles(self.h);
                    Vector3 targetPosition = self.GetTargetPosition(self.v);

                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().TurnTo(targetEulerAngles).Coroutine();
                    self.GetParent<Unit>().GetComponent<UnitPathComponent>().MoveTo(targetPosition).Coroutine();

                    Console.WriteLine(" KeyboardPathComponentHelper-48-H: " + self.GetParent<Unit>().UnitType + " / ( " + 0 + " , " + targetEulerAngles.y + " , " + 0 + ")");
                    Console.WriteLine(" KeyboardPathComponentHelper-49-V: " + self.GetParent<Unit>().UnitType + " / ( " + targetPosition.x + " , " + 0 + " , " + targetPosition.z + ")");
                }
            }
            else
            {
                if (self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource = null;

                    Console.WriteLine(" KeyboardPathComponentHelper-60-H: CancellationTokenSource is Cancel.");
                }

                if (self.GetParent<Unit>().GetComponent<UnitPathComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<UnitPathComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<UnitPathComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<UnitPathComponent>().CancellationTokenSource = null;

                    Console.WriteLine(" KeyboardPathComponentHelper-69-V: CancellationTokenSource is Cancel.");
                }
            }

            self.offsetTimeMove = TimeHelper.Now() - self.startTimeMove + 1;
        }

        #region v h

        /// <summary>
        /// 得到 新目标点
        /// </summary>
        /// <param name="self"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        static Vector3 GetTargetPosition(this KeyboardPathComponent self , float v)
        {
            Vector3 dv = new Vector3((float)Math.Cos(self.GetParent<Unit>().eulerAngles.y) * v, 0, (float)Math.Sin(self.GetParent<Unit>().eulerAngles.y) * v).normalized;
            dv = dv * self.moveSpeed;

            Vector3 se = self.GetParent<Unit>().Position;
            Vector3 pv = se + dv;

            return pv;
        }

        static Vector3 GetTargetEulerAngles(this KeyboardPathComponent self, float h)
        {
            Vector3 dav = new Vector3(0, h, 0).normalized;
            dav = dav * self.roteSpeed;

            Vector3 sea = self.GetParent<Unit>().eulerAngles;
            Vector3 aav = sea + dav;

            return aav;
        }

        public static void VHToZero(this KeyboardPathComponent self, long speed = 40L)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            if (Math.Abs(self.v) != 0)
            {
                timerComponent.WaitAsync(speed);
                self.vCount -= 1;
                if (self.vCount <= 0)
                {
                    self.v = 0;
                    self.vCount = 4;
                }
            }
            if (Math.Abs(self.h) != 0)
            {
                timerComponent.WaitAsync(speed);
                self.hCount -= 1;
                if (self.hCount <= 0)
                {
                    self.h = 0;
                    self.hCount = 4;
                }
            }
        }
        #endregion


    }
}
