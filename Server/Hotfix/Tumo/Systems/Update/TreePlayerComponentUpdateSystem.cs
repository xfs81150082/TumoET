using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TreePlayerComponentUpdateSystem : UpdateSystem<PlayerBehaviorTreeComponent>
    {
        public override void Update(PlayerBehaviorTreeComponent self)
        {
            self.root.Tick();
        }
    }


}
