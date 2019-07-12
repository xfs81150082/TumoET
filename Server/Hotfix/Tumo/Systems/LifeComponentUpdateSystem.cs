using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class LifeComponentUpdateSystem : UpdateSystem<LifeComponent>
    {
        public override void Update(LifeComponent self)
        {
            self.UpdateProperty();
        }

    }
}
