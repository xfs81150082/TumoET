using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_KeyboardSkillHandler : AMActorLocationRpcHandler<Unit, C2M_KeyboardSkillRequest, M2C_KeyboardSkillResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_KeyboardSkillRequest message, Action<M2C_KeyboardSkillResponse> reply)
        {
            M2C_KeyboardSkillResponse response = new M2C_KeyboardSkillResponse();
            try
            {
                unit.GetComponent<AttackComponent>().currentKey = message.Info;
                ///通知 播放 死亡录像
                ///TOTO

                response.Info = message.Info;

                response.Message = " Retrun-C2M_KeyboardSkillHandler-20";

                Console.WriteLine(" C2M_KeyboardSkillHandler-unitid/message: " + unit.Id + " / " + message.Info);

                reply(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" C2M_KeyboardSkillHandler-unitid/message: " + unit.Id + " / " + message.Info + " / " + ex.Message);
            }
            await ETTask.CompletedTask;
        }


    }
}
