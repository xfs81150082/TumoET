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
        #region v h
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

            Console.WriteLine(" KeyboardPathComponentHelper-28-vh: " + (KeyType)self.GetParent<Unit>().Id + " : ( " + V + " / " + H + ") ");
        }

        public static void KeyboardMoveTurn(this KeyboardPathComponent self)
        {
            if (self.offsetTimeTurn > self.resTime)
            {
                self.offsetTimeTurn = 0;
            }

            if (self.offsetTimeMove > self.resTime)
            {
                self.offsetTimeMove = 0;
            }

            if (Math.Abs(self.h) > 0.3f)
            {
                if (self.offsetTimeTurn == 0)
                {
                    self.startTimeTurn = TimeHelper.Now();

                    Vector3 targetEulerAngles = self.GetTargetEulerAngles(self.h);

                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().TurnTo(targetEulerAngles).Coroutine();

                    //Console.WriteLine(" KeyboardPathComponentHelper-53-H: " + self.GetParent<Unit>().UnitType + " / ( " + 0 + " , " + targetEulerAngles.y + " , " + 0 + ")");
                    //Console.WriteLine(" KeyboardPathComponentHelper-54-unitH: " + self.GetParent<Unit>().UnitType + " / ( " + 0 + " , " + self.GetParent<Unit>().EulerAngles.y + " , " + 0 + ")");
                    //Console.WriteLine(" KeyboardPathComponentHelper-55-unitV: " + self.GetParent<Unit>().UnitType + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
                }
            }
            else
            {
                if (self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().CancellationTokenSource = null;

                    Console.WriteLine(" KeyboardPathComponentHelper-66-H: CancellationTokenSource is Cancel.");
                }
            }

            if (Math.Abs(self.v) > 0.3f)
            {
                if (self.offsetTimeMove == 0)
                {
                    self.startTimeMove = TimeHelper.Now();

                    Vector3 targetPosition = self.GetTargetPosition(self.v);

                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().MoveTo(targetPosition).Coroutine();

                    //Console.WriteLine(" KeyboardPathComponentHelper-80-targetV: " + self.GetParent<Unit>().UnitType + " / ( " + targetPosition.x + " , " + 0 + " , " + targetPosition.z + ")");
                    //Console.WriteLine(" KeyboardPathComponentHelper-81-unitV: " + self.GetParent<Unit>().UnitType + " / ( " + self.GetParent<Unit>().Position.x + " , " + 0 + " , " + self.GetParent<Unit>().Position.z + ")");
                    //Console.WriteLine(" KeyboardPathComponentHelper-82-unitH: " + self.GetParent<Unit>().UnitType + " / ( " + 0 + " , " + self.GetParent<Unit>().EulerAngles.y + " , " + 0 + ")");
                }
            }
            else
            {
                if (self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource = null;

                    Console.WriteLine(" KeyboardPathComponentHelper-93-V: CancellationTokenSource is Cancel.");
                }
            }

            self.offsetTimeTurn = TimeHelper.Now() - self.startTimeTurn + 1;

            self.offsetTimeMove = TimeHelper.Now() - self.startTimeMove + 1;
        }

        static Vector3 GetTargetPosition(this KeyboardPathComponent self , float v)
        {
            Vector3 dv = new Vector3((float)Math.Cos(self.GetParent<Unit>().EulerAngles.y) * v, 0, (float)Math.Sin(self.GetParent<Unit>().EulerAngles.y) * v).normalized;
            dv = dv * self.moveSpeed;

            Vector3 se = self.GetParent<Unit>().Position;
            Vector3 pv = se + dv;

            return pv;
        }

        static Vector3 GetTargetEulerAngles(this KeyboardPathComponent self, float h)
        {
            Vector3 dav = new Vector3(0, h, 0).normalized;
            dav = dav * self.roteSpeed;

            Vector3 sea = self.GetParent<Unit>().EulerAngles;
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
