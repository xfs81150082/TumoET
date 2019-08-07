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

        async ETVoid UpdateSpawnAsync()
        {
            TimerComponent timer = Game.Scene.GetComponent<TimerComponent>();
            while (true)
            {
                try
                {
                    foreach (Monster monster in Game.Scene.GetComponent<MonsterComponent>().GetAll())
                    {
                        Unit unit = Game.Scene.GetComponent<MonsterUnitComponent>().Get(monster.UnitId);
                        if (unit != null)
                        {
                            continue;
                        }

                        SpawnUnit(monster).Coroutine();

                        Console.WriteLine(" SpawnComponentAwakeSystem-35-生产小怪：" + monster.Id);
                    }

                    await timer.WaitAsync(4000);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);

                }
            }
        }

        async ETVoid SpawnUnit(Monster monster)
        {
            M2G_CreateUnit response = (M2G_CreateUnit)await SessionHelper.MapSession().Call(new G2M_CreateUnit() { UnitType = (int)UnitType.Monster, RolerId = monster.Id });
            monster.UnitId = response.UnitId;
        }

    }
}
