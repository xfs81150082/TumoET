using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class MapPathComponentHelper
    {
        public static void MoveMap(this MapPathComponent self, Move_Map move_Map)
        {
            self.clientPos = new Vector3(move_Map.X, move_Map.Y, move_Map.Z);
            self.dirPos = new Vector3(move_Map.AX, move_Map.AY, move_Map.AZ);

            if (move_Map.V == 0f)
            {
                if (self.GetParent<Unit>().GetComponent<UnitDirComponent>().CancellationTokenSource != null)
                {
                    self.GetParent<Unit>().GetComponent<UnitDirComponent>().CancellationTokenSource?.Cancel();
                    self.GetParent<Unit>().GetComponent<UnitDirComponent>().CancellationTokenSource?.Dispose();
                    self.GetParent<Unit>().GetComponent<UnitDirComponent>().CancellationTokenSource = null;

                    self.GetParent<Unit>().Position = self.clientPos;
                    self.GetParent<Unit>().SaveVector3();

                    Console.WriteLine(" MapPathComponentHelper-26-ServerPos: CancellationTokenSource is Cancel." + self.GetParent<Unit>().Position.ToString());
                }
            }
            else
            {
                self.GetParent<Unit>().GetComponent<UnitDirComponent>().Move(self.clientPos, self.dirPos).Coroutine();
            }
        }


    }
}
