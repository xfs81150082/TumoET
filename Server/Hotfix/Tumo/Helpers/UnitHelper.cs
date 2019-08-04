using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{  
    public static class UnitHelper
    {
        public static void SaveVector3(this Unit unit)
        {
            Player player = Game.Scene.GetComponent<PlayerComponent>().GetByUnitId(unit.Id);
            Vector3 unitVec = unit.Position;
            if (player != null)
            {
                player.spawnPosition = unitVec;

                Console.WriteLine(" UnitHelper-19-playerVec: " + player.Id + " / " + player.UnitId + " : " + player.spawnPosition.ToString());
            }
        }


    }
}
