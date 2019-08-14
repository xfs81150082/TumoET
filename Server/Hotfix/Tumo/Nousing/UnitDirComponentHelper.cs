using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class UnitDirComponentHelper
    {
        public static void MoveMap(this UnitDirComponent self, Move_Map message)
        {
            Vector3 target = new Vector3(message.X, message.Y, message.Z);

            if (message.V == 0f)
            {
                if (self.GetParent<Unit>().GetComponent<MapPathComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<MapPathComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<MapPathComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<MapPathComponent>().CancellationTokenSource = null;

                    self.GetParent<Unit>().Position = target;
                    self.GetParent<Unit>().SaveVector3();

                    self.GetParent<Unit>().GetComponent<MapPathComponent>().MoveTo(target).Coroutine();

                    Console.WriteLine(" MapPathComponentHelper-26-ServerPos: CancellationTokenSource is Cancel." + self.GetParent<Unit>().Position.ToString());
                }
            }
            else
            {
                self.GetParent<Unit>().GetComponent<MapPathComponent>().MoveTo(target).Coroutine();
            }
            
        }


    }
}
