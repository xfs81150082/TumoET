using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TreeMonsterComponentUpdateSystem : UpdateSystem<TreeMonsterComponent>
    {
        public override void Update(TreeMonsterComponent self)
        {
            self.root.Tick();
        }
    }
}
