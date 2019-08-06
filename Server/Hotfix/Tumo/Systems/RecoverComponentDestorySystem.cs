using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class RecoverComponentDestorySystem : DestroySystem<RecoverComponent>
    {
        public override void Destroy(RecoverComponent self)
        {
            //DeathSpawn(self).Coroutine();
        }

        /// <summary>
        /// 自己被销毁时 将要做的事
        /// </summary>
        /// <param name="self"></param>
        async ETVoid DeathSpawn(RecoverComponent self)
        {
            Console.WriteLine(" LifeComponentDestorySystem-22: " + self.GetParent<Unit>().Id);

            TimerComponent timer = Game.Scene.GetComponent<TimerComponent>();
            UnitType unitType = self.GetParent<Unit>().UnitType;
            long unitId = self.GetParent<Unit>().Id;

            switch (unitType)
            {
                case UnitType.Player:
                    Player player = Game.Scene.GetComponent<PlayerComponent>().GetByUnitId(unitId);
                    await timer.WaitAsync(5000);
                    //await player.SpawnUnit();
                    break;
                case UnitType.Monster:
                    Monster monster = Game.Scene.GetComponent<MonsterComponent>().GetByUnitId(unitId);
                    await timer.WaitAsync(5000);
                    //await monster.SpawnUnit();
                    break;
                case UnitType.Npcer:
                    break;
            }

            Console.WriteLine(" LifeComponentDestorySystem-44: " + self.GetParent<Unit>().Id);
        }


    }
}
