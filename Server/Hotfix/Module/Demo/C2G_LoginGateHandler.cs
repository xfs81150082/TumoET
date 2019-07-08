﻿using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override void Run(Session session, C2G_LoginGate message, Action<G2C_LoginGate> reply)
		{
			G2C_LoginGate response = new G2C_LoginGate();
            try
            {
                string account = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(message.Key);
                if (account == null)
                {
                    response.Error = ErrorCode.ERR_ConnectGateKeyError;
                    response.Message = "Gate key验证失败!";
                    reply(response);
                    return;
                }            

                User user = Game.Scene.GetComponent<UserComponent>().Get(account);
                Player[] players = Game.Scene.GetComponent<PlayerComponent>().GetByUserId(user.Id);
                Player player = players[0];

                session.AddComponent<SessionPlayerComponent>().Player = player;
                session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);               

                response.PlayerId = player.Id;
                reply(response);

                Console.WriteLine(" C2G_LoginGateHandler-34-playerId: " + player.Id);

                //session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
		}

    }
}