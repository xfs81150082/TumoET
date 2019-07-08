using ETModel;

namespace ETHotfix
{
	public static class MessageHelper
	{
		public static void Broadcast(IActorMessage message)
		{
			Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
			ActorMessageSenderComponent actorLocationSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
			foreach (Unit unit in units)
			{
				UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
				if (unitGateComponent.IsDisconnect)
				{
					continue;
				}

				ActorMessageSender actorMessageSender = actorLocationSenderComponent.Get(unitGateComponent.GateSessionActorId);
				actorMessageSender.Send(message);
			}
		}

        /// <summary>
        /// 后来增加
        /// </summary>
        /// <param name="message"></param>
        /// <param name="playerUnitId"></param>
        public static void Broadcast(IActorMessage message, long playerUnitId)
        {
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(playerUnitId);
            ActorMessageSenderComponent actorLocationSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();

            UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
            if (unitGateComponent.IsDisconnect)
            {
                return;
            }
            ActorMessageSender actorMessageSender = actorLocationSenderComponent.Get(unitGateComponent.GateSessionActorId);
            actorMessageSender.Send(message);
        }
    }
}