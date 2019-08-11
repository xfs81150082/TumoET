using ETModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Death_MapHandler : AMActorLocationHandler<Unit, Death_Map>
    {
        protected override void Run(Unit entity, Death_Map message)
        {           
            ///删除 客户端 死亡的单元实例或更新玩家位置
            entity.GetComponent<AoiUnitComponent>().DeathRemove();

            ///结算经验和金币
            entity.GetComponent<RecoverComponent>().GetExpAndCoin();

            //Console.WriteLine(" Death_MapHandler-24-unittype/unitid: " + entity.UnitType + " / " + entity.Id);

            ///删除 Unit
            switch (entity.UnitType)
            {
                case UnitType.Monster:
                    Game.Scene.GetComponent<MonsterUnitComponent>().Remove(entity.Id);
                    ///CD刷新时间到后，重新生成
                    
                    //Console.WriteLine(" Death_MapHandler-33-unittype/unitid: " + entity.UnitType + " / " + entity.Id);                    
                    break;
                case UnitType.Player:
                    ///重新生产Unit，先取得参数，后删除，再发送生产指令
                    T2M_CreateUnit t2M_Create = entity.T2M_CreateUnit();

                    Console.WriteLine(" Death_MapHandler-41-playerid: " + t2M_Create.RolerId + " /unitid: " + t2M_Create.UnitId + " /GateSessionId: " + t2M_Create.GateSessionId+" /Count:" + Game.Scene.GetComponent<UnitComponent>().Count);

                    //后删除
                    Game.Scene.GetComponent<UnitComponent>().Remove(entity.Id);

                    //再发送生产指令
                    CreatePlayerAsync(t2M_Create).Coroutine();

                    Console.WriteLine(" Death_MapHandler-51-players: " + Game.Scene.GetComponent<UnitComponent>().Count);
                    break;
            }
            ///通知 播放 死亡录像
            ///TOTO
        }

        protected async ETVoid CreatePlayerAsync(T2M_CreateUnit message)
        {
            // 在map服务器上创建战斗Unit
            M2T_CreateUnit m2T_Response = (M2T_CreateUnit)await SessionHelper.MapSession().Call(message);
        }


    }
}
