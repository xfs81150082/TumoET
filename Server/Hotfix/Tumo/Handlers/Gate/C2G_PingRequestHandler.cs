using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_PingRequestHandler : AMRpcHandler<C2G_PingRequest, G2C_PingResponse>
    {
        protected override void Run(Session session, C2G_PingRequest message, Action<G2C_PingResponse> reply)
        {
            var response = new G2C_PingResponse();

            try
            {
                Console.WriteLine(" 收到心跑包： " + message.RpcId);

                Game.Scene.GetComponent<BongComponent>().UndateSession(session.Id);
                
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }


    }
}
