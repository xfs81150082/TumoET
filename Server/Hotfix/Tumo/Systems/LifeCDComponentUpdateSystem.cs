using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETHotfix
{
    public class LifeCDComponentUpdateSystem : UpdateSystem<LifeCDComponent>
    {
        public override void Update(LifeCDComponent self)
        {
            SpawnUnitByLifeCD(self);
        }

        private void SpawnUnitByLifeCD(LifeCDComponent self)
        {
            if (!self.isDeath) return;

            if (!self.startNull)
            {
                self.startTime = TimeHelper.ClientNowSeconds();
                self.startNull = true;
            }

            long nowtime = TimeHelper.ClientNowSeconds();

            self.remainingLifeCD = self.lifeCD - (nowtime - self.startTime);

            if (self.remainingLifeCD <= 0)
            {
                switch (self.unitType)
                {
                    case UnitType.Player:
                        //SpawnByPlayer(self.GetParent<Player>());
                        break;
                    case UnitType.Monster:
                        //SpawnByEnemy(self.GetParent<Enemy>());
                        break;
                }
                self.isDeath = false;
                self.startNull = false;
            }
        }

     
    }



}
