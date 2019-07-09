using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class AoiUnitComponentUpdateSystem : UpdateSystem<AoiUnitComponent>
    {
        public override void Update(AoiUnitComponent self)
        {
            UpdateUnitAoiGrid(self);
        }

        void UpdateUnitAoiGrid(AoiUnitComponent self)
        {
            if (Game.Scene.GetComponent<AoiGridComponent>().GetGridId(self) != self.gridId)
            {
                //if (self.GetParent<Unit>().GetComponent<AttackComponent>() != null && self.GetParent<Unit>().GetComponent<AttackComponent>().isDeath)
                //{
                //    return;
                //}

                ///移动后 根据坐标 更换地图固定格子 注册注销 unitId
                Game.Scene.GetComponent<AoiGridComponent>().UpdateAoiUnitInfo(self);

            }
        }
        

    }
}
