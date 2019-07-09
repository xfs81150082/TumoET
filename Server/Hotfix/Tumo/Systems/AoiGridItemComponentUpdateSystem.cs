using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiGridItemComponentUpdateSystem : UpdateSystem<AoiGridItemComponent>
    {
        public override void Update(AoiGridItemComponent self)
        {
            ChangeUpdate(self.GetParent<AoiGrid>());
        }

        void ChangeUpdate(AoiGrid self)
        {
            if (self.players.Count != self.playerChangeCount)
            {
                foreach (long tem in self.players)
                {
                    Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().UpdateAoiUnitInfo();

                    AoiUintInfo uintInfo = Game.Scene.GetComponent<UnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().playerIds;
                    Console.WriteLine(" AoiGridItemComponentUpdateSystem-25 " + self.gridId + " - " + Game.Scene.GetComponent<UnitComponent>().Get(tem).Id + " : " + uintInfo.OldMovesSet.Count + " => " + uintInfo.MovesSet.Count + " :  " + uintInfo.Enters.Count + " -E/L- " + uintInfo.Leaves.Count);
                }
                self.playerChangeCount = self.players.Count;
            }

            if (self.enemys.Count != self.enemyChangeCount)
            {
                foreach (long tem in self.enemys)
                {
                    Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().UpdateAoiUnitInfo();

                    AoiUintInfo uintInfo = Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().enemyIds;
                    Console.WriteLine(" AoiGridItemComponentUpdateSystem-37 " + self.gridId + " - " + Game.Scene.GetComponent<EnemyUnitComponent>().Get(tem).Id + " : " + uintInfo.OldMovesSet.Count + " => " + uintInfo.MovesSet.Count + " :  " + uintInfo.Enters.Count + " -E/L- " + uintInfo.Leaves.Count);
                }
                self.enemyChangeCount = self.enemys.Count;
            }

            if (self.npcers.Count != self.npcerChangeCount)
            {
                foreach (long tem in self.npcers)
                {
                    Game.Scene.GetComponent<NpcerUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().UpdateAoiUnitInfo();

                    AoiUintInfo uintInfo = Game.Scene.GetComponent<NpcerUnitComponent>().Get(tem).GetComponent<AoiUnitComponent>().npcerIds;
                    Console.WriteLine(" AoiGridItemComponentUpdateSystem-49 " + self.gridId + " - " + Game.Scene.GetComponent<NpcerUnitComponent>().Get(tem).Id + " : " + uintInfo.OldMovesSet.Count + " => " + uintInfo.MovesSet.Count + " :  " + uintInfo.Enters.Count + " -E/L- " + uintInfo.Leaves.Count);
                }
                self.npcerChangeCount = self.npcers.Count;
            }
        }

    }
}
