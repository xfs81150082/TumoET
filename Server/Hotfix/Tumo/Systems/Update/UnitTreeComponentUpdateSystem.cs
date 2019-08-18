using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class UnitTreeComponentUpdateSystem : UpdateSystem<UnitTreeComponent>
    {
        public override void Update(UnitTreeComponent self)
        {
            self.root.Tick();
        }
    }


}
