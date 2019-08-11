using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class SpawnComponentAwakeSystem : AwakeSystem<SpawnComponent>
    {
        public override void Awake(SpawnComponent self)
        {
            UpdateSpawnAsync().Coroutine();
        }
        /// <summary>
        /// 向 map 服务器 实例小怪单元
        /// </summary>
        /// <returns></returns>
        async ETVoid UpdateSpawnAsync()
        {
            TimerComponent timer = Game.Scene.GetComponent<TimerComponent>();
            while (true)
            {
                try
                {
                    await timer.WaitAsync(4000);

                    foreach (Monster monster in Game.Scene.GetComponent<MonsterComponent>().GetAll())
                    {
                        Unit unit = Game.Scene.GetComponent<MonsterUnitComponent>().Get(monster.UnitId);
                        if (unit != null)
                        {
                            continue;
                        }

                        SpawnUnit(monster).Coroutine();

                        Console.WriteLine(" SpawnComponentAwakeSystem-35-生产小怪id：" + monster.Id);
                    }

                    await timer.WaitAsync(10000);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);

                }
            }
        }

        async ETVoid SpawnUnit(Monster monster)
        {
            M2T_CreateUnit response = (M2T_CreateUnit)await SessionHelper.MapSession().Call(new T2M_CreateUnit() { UnitType = (int)UnitType.Monster, RolerId = monster.Id, UnitId = 0 });
            monster.UnitId = response.UnitId;
        }

    }
}
