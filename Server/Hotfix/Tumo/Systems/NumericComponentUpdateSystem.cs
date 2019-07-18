using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class NumericComponentUpdateSystem : UpdateSystem<NumericComponent>
    {
        public override void Update(NumericComponent self)
        {
            self.UpdateProperty();
        }


    }
}
