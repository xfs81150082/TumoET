using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class KeyboardPathComponentUpdateSystem : UpdateSystem<KeyboardPathComponent>
    {
        public override void Update(KeyboardPathComponent self)
        {
            if (Math.Abs(self.v) > 0.03f || (Math.Abs(self.h) > 0.03f))
            {
                self.KeyboardMoveTurn();

                self.VHToZero();
            }
        }
    }
}
