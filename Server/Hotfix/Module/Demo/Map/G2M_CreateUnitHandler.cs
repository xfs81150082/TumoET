using System;
using System.Linq;
using System.Net;
using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
	[MessageHandler(AppType.Map)]
	public class G2M_CreateUnitHandler : AMRpcHandler<G2M_CreateUnit, M2G_CreateUnit>
	{
        protected override void Run(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
        {
            Console.WriteLine(" G2M_CreateUnitHandler-15: " + session.InstanceId + " / " + message.GateSessionId);

            CreatePlayerAsync(session, message, reply).Coroutine();
        }
        protected async ETVoid CreatePlayerAsync(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
        {
            M2G_CreateUnit response = new M2G_CreateUnit();

            // 在map服务器上创建战斗Unit
            M2T_CreateUnit m2T_Response = (M2T_CreateUnit)await SessionHelper.MapSession().Call(new T2M_CreateUnit() { UnitType = (int)UnitType.Player, RolerId = message.RolerId, GateSessionId = message.GateSessionId, UnitId = 0 });

            response.UnitId = m2T_Response.UnitId;

            reply(response);
        }

    }
}
