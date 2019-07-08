using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class RecoverComponentUpdateSystem : UpdateSystem<RecoverComponent>
    {
        public override void Update(RecoverComponent self)
        {
            self.UpdateProperty();
        }

    }
}
