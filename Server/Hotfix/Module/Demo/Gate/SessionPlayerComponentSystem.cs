using ETModel;

namespace ETHotfix
{
	[ObjectSystem]
	public class SessionPlayerComponentDestroySystem : DestroySystem<SessionPlayerComponent>
	{
		public override void Destroy(SessionPlayerComponent self)
		{
			// 发送断线消息
			ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(self.Player.UnitId);
			actorLocationSender.Send(new G2M_SessionDisconnect());
           
            ///20190621  下线或断线后，自动删除unit
            Game.Scene.GetComponent<UnitComponent>()?.Remove(self.Player.UnitId);

            ///20190804  下线或断线后，不自动删除Player
            //Game.Scene.GetComponent<PlayerComponent>()?.Remove(self.Player.Id);

        }
	}
}