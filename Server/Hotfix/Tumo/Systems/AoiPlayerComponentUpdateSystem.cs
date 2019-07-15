using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiPlayerComponentUpdateSystem : UpdateSystem<AoiPlayerComponent>
    {
        public override void Update(AoiPlayerComponent self)
        {
            AoiUnitComponent aoiUnit = self.GetParent<Unit>().GetComponent<AoiUnitComponent>();
            if (aoiUnit.playerIds.Enters.Count != 0)
            {
                self.AddPlayers(aoiUnit.playerIds.Enters.ToArray(), new long[1] { self.GetParent<Unit>().Id });
                aoiUnit.playerIds.Enters.Clear();
            }
            if (aoiUnit.enemyIds.Enters.Count != 0)
            {
                self.AddMonsters(aoiUnit.enemyIds.Enters.ToArray(), new long[1] { self.GetParent<Unit>().Id });
                aoiUnit.enemyIds.Enters.Clear();
            }
            if (aoiUnit.playerIds.Leaves.Count != 0)
            {
                self.RemovePlayers(aoiUnit.playerIds.Leaves.ToArray(), new long[1] { self.GetParent<Unit>().Id });
                aoiUnit.playerIds.Leaves.Clear();
            }
            if (aoiUnit.enemyIds.Leaves.Count != 0)
            {
                self.RemoveMonsters(aoiUnit.enemyIds.Leaves.ToArray(), new long[1] { self.GetParent<Unit>().Id });
                aoiUnit.enemyIds.Leaves.Clear();
            }
        }

    }
}
