using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AttackComponentUpdateSystem : UpdateSystem<AttackComponent>
    {
        public override void Update(AttackComponent self)
        {
            self.TakeAttack();
        }


    }
}