﻿using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_EnterMapHandler : AMRpcHandler<C2G_EnterMap, G2C_EnterMap>
	{
		protected override void Run(Session session, C2G_EnterMap message, Action<G2C_EnterMap> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}
		
		protected async ETVoid RunAsync(Session session, C2G_EnterMap message, Action<G2C_EnterMap> reply)
		{
			G2C_EnterMap response = new G2C_EnterMap();
			try
			{
                Player player = session.GetComponent<SessionPlayerComponent>().Player;
                Player player22 = Game.Scene.GetComponent<PlayerComponent>().Get(message.PlayerId);

                ///20190714///验证过相同
                Console.WriteLine(" MyPlayer.Id: " + player.Id + " / " + player22.Id);

                // 在map服务器上创建战斗Unit
                M2G_CreateUnit createResponse = (M2G_CreateUnit)await MapSessionHelper.Session().Call(new G2M_CreateUnit() {UnitType = (int)UnitType.Player, RolerId = player.Id, GateSessionId = session.InstanceId });
                player.UnitId = createResponse.UnitId;

                response.UnitId = createResponse.UnitId;
                reply(response);

                Console.WriteLine(" C2G_EnterMapHandler-33-player.UnitId: " + player.UnitId);
                foreach(Unit tem in Game.Scene.GetComponent<UnitComponent>().GetAll())
                {
                    Console.WriteLine(" C2G_EnterMapHandler-36-UnitComponent: " + tem.Id);
                    Console.WriteLine(" C2G_EnterMapHandler-37-UnitComponent.count: " + Game.Scene.GetComponent<UnitComponent>().GetAll().Length);
                }

            }
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}