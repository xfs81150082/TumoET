using ETModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class W2M_DeathHandler : AMActorLocationRpcHandler<Unit, W2M_DeathActorRequest, M2W_DeathActorResponse>
    {
        protected override async ETTask Run(Unit unit, W2M_DeathActorRequest message, Action<M2W_DeathActorResponse> reply)
        {
            M2W_DeathActorResponse response = new M2W_DeathActorResponse();
            try
            {
                unit.GetComponent<AoiUnitComponent>().DeathRemove();

                ///结算经验和金币
                unit.GetComponent<RecoverComponent>().GetExpAndCoin();

                ///删除Unit
                switch (unit.UnitType)
                {
                    case UnitType.Player:
                        //Game.Scene.GetComponent<UnitComponent>().Remove(unit.Id);
                        ///仅重新给个坐标点，但不删除

                        break;
                    case UnitType.Monster:
                        Game.Scene.GetComponent<MonsterUnitComponent>().Remove(unit.Id);
                        break;
                    case UnitType.Npcer:
                        //Game.Scene.GetComponent<NpcerUnitComponent>().Remove(unit.Id);
                        break;
                }
                ///通知 播放 死亡录像
                ///TOTO

                long timer = TimeHelper.ClientNowSeconds();

                response.Message = " Retrun-20190718-W2M_DeathHandler-15" + timer;

                Console.WriteLine(" W2M_DeathHandler-message/unitid/ClientNowSeconds: " + message.Info + " / " + unit.Id + " / " + timer);

                reply(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" W2M_DeathHandler-50-message/id/ex: " + message.Info + " / " + unit.Id + " / " + ex.Message);
            }
            await ETTask.CompletedTask;
        }
        

    }
}
