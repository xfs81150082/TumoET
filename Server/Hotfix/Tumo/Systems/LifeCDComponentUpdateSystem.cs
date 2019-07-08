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
                        SpawnByEnemy(self.GetParent<Enemy>());
                        break;
                }
                self.isDeath = false;
                self.startNull = false;
            }
        }

        private void SpawnByEnemy(Enemy enemy)
        {
            /// 再向 Map 服务器 初始化小怪实例
            IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
            Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
            mapSession.Send(new M2M_CreateEnemyUnit() { EnemyId = enemy.Id });

            Console.WriteLine(" EnemyComponentStartSystem-86: " + " 发给消息给 map服务器 初始化小怪");

        }
        //private void SpawnByPlayer(Player player)
        //{
        //    /// 再向 Map 服务器 初始化小怪实例
        //    IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
        //    Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
        //    mapSession.Send(new G2M_CreateUnit() { PlayerId = player.Id });

        //    Console.WriteLine(" EnemyComponentStartSystem-86: " + " 发给消息给 map服务器 初始化小怪");

        //}
    }



}
