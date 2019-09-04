using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class TreeMonsterComponentUpdateSystem : UpdateSystem<MonsterBehaviorTreeComponent>
    {
        public override void Update(MonsterBehaviorTreeComponent self)
        {
            self.root.Tick();
        }
    }
}
