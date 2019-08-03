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
        #region v
        public static void KeyboardVW(this KeyboardPathComponent self, float V, float W)
        {
            if (Math.Abs(V) > 0.05f)
            {
                self.v = V;
                self.w = W;
                self.vCount = 4;
            }
            //Console.WriteLine(" KeyboardPathComponentHelper-22-vw: " + (KeyType)self.GetParent<Unit>().Id + " :  " + V + "=>" + W);
        }

        public static void KeyboardMove(this KeyboardPathComponent self)
        { 
            if (self.offsetTimeMove > self.resTime)
            {
                self.offsetTimeMove = 0;
            }

            if (Math.Abs(self.v) > 0.3f)
            {
                if (self.offsetTimeMove == 0)
                {
                    self.startTimeMove = TimeHelper.Now();

                    self.GetTargetPosition();

                    Console.WriteLine(" KeyboardPathComponentHelper-40-vw: " + (KeyType)self.GetParent<Unit>().Id + " :  " + self.v + "=>" + self.w);

                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().MoveTo(self.result).Coroutine();
                }
            }
            else
            {
                if (self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<UnitPositionComponent>().CancellationTokenSource = null;

                    Console.WriteLine(" KeyboardPathComponentHelper-51-V: CancellationTokenSource is Cancel.");
                }
            }

            self.offsetTimeMove = TimeHelper.Now() - self.startTimeMove + 1;
        }

        static void GetTargetPosition(this KeyboardPathComponent self)
        {            
            self.GetParent<Unit>().EulerAngles = new Vector3(0, self.w, 0);
            self.offsetPos = new Vector3((float)Math.Cos(self.w) * self.v, 0, (float)Math.Sin(self.w) * self.v);
            self.offsetPos = self.offsetPos * self.moveSpeed;
            self.result = self.GetParent<Unit>().Position + self.offsetPos;
        }

        public static void VToZero(this KeyboardPathComponent self, long speed = 40L)
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
        }
        #endregion

        #region h
        public static void KeyboardH(this KeyboardPathComponent self, float H)
        {  
            if (Math.Abs(H) > 0.05f)
            {
                self.h = H;
                self.hCount = 4;
            }

            Console.WriteLine(" KeyboardPathComponentHelper-28-vh: " + (KeyType)self.GetParent<Unit>().Id + " : "+ H ) ;
        }
        public static void KeyboardTurn(this KeyboardPathComponent self)
        {
            if (self.offsetTimeTurn > self.resTime)
            {
                self.offsetTimeTurn = 0;
            }          
            if (Math.Abs(self.h) > 0.3f)
            {
                if (self.offsetTimeTurn == 0)
                {
                    self.startTimeTurn = TimeHelper.Now();

                    Vector3 targetEulerAngles = self.GetTargetEulerAngles(self.h);

                    self.GetParent<Unit>().GetComponent<UnitAnglesComponent>().TurnTo(targetEulerAngles).Coroutine();
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

            self.offsetTimeTurn = TimeHelper.Now() - self.startTimeTurn + 1;
        }
        static Vector3 GetTargetEulerAngles(this KeyboardPathComponent self, float h)
        {
            Vector3 dav = new Vector3(0, h, 0).normalized;
            dav = dav * self.roteSpeed;

            Vector3 sea = self.GetParent<Unit>().EulerAngles;
            Vector3 aav = sea + dav;

            return aav;
        }
        public static void HToZero(this KeyboardPathComponent self, long speed = 40L)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();          
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
