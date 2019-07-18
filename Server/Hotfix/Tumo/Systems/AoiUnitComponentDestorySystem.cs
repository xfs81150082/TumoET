 using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiUnitComponentDestorySystem : DestroySystem<AoiUnitComponent>
    {
        public override void Destroy(AoiUnitComponent self)
        {
            /// 从格子 注销 因为已死亡
            Game.Scene.GetComponent<AoiGridComponent>().RemoveUnitId(self);
        }


    }
}
