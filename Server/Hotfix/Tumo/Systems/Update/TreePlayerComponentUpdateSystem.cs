using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TreePlayerComponentUpdateSystem : UpdateSystem<TreePlayerComponent>
    {
        public override void Update(TreePlayerComponent self)
        {
            self.root.Tick();
        }
    }


}
