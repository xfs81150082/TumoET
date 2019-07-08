using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class SeeComponentHelper
    { 
        /// 追击敌人
        public static See_Map GetSeeMap (this SeeComponent self)
        {
            if(self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit == null)
            {
                return null;
            }
            self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
            self.seePoint = self.target.Position;
            See_Map see_Map = new See_Map() { Id = self.GetParent<Unit>().Id, X = self.seePoint.x, Y = self.seePoint.y, Z = self.seePoint.z };
            return see_Map;
        }
        

    }
}