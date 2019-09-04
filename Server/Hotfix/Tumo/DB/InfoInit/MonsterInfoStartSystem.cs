using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class MonsterInfoStartSystem : StartSystem<MonsterInfo>
    {
        public override void Start(MonsterInfo self)
        {
            //SaveMonster(self.enemys);
        }

        void SaveMonster(Dictionary<long, Monster> enemys)
        {
            DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

            foreach (Monster tem in enemys.Values.ToArray())
            {
                Monsterdb monsterdb = ComponentFactory.CreateWithId<Monsterdb>(tem.Id);
                monsterdb.level = 10;
                monsterdb.exp = 0;
                monsterdb.hp = 40;
                monsterdb.spawnVec.Add(tem.spawnPosition.x);
                monsterdb.spawnVec.Add(tem.spawnPosition.y);
                monsterdb.spawnVec.Add(tem.spawnPosition.z);

                dBProxy.Save(monsterdb).Coroutine();
            }


        }
    }
}
