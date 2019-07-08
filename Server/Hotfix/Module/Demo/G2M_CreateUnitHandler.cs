using System;
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
			RunAsync(session, message, reply).Coroutine();
		}
		
		protected async ETVoid RunAsync(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
		{
			M2G_CreateUnit response = new M2G_CreateUnit();
            try
            {
                Player player = Game.Scene.GetComponent<PlayerComponent>().Get(message.PlayerId);
                Unit unit = ComponentFactory.CreateWithId<Unit>(player.Id);
                unit.AddComponent<MoveComponent>();
                unit.AddComponent<UnitPathComponent>();
                unit.Position = new Vector3(-40, 0, -10);
                unit.Position = new Vector3(player.spawnPosition.x, 0, player.spawnPosition.z);
                await unit.AddComponent<MailBoxComponent>().AddLocation();
                unit.AddComponent<UnitGateComponent, long>(message.GateSessionId);
                Game.Scene.GetComponent<UnitComponent>().Add(unit);

                response.UnitId = unit.Id;
                reply(response);

                Console.WriteLine(" G2M_CreateUnitHandler-38-myunitId: " + unit.Id);

                ///20190702 玩家
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Player);
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<RecoverComponent>();
                unit.AddComponent<RayUnitComponent>();
                unit.AddComponent<AoiUnitComponent>();
                unit.AddComponent<AoiPlayerComponent>();

                SetNumeric(unit ,player);

                ///生产玩家本人的单元实例
                SpawnMyPlayerUnit(unit);

                //BroadcastPlayerUnit();

                BroadcastEnemyUnit(unit);

            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
		}

        void SetNumeric(Unit unit, Player player)
        {
            if (unit.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = unit.GetComponent<NumericComponent>();

            num.Set(NumericType.HpAdd, 400);
            num.Set(NumericType.MaxHpAdd, 400);

            Console.WriteLine(" M2M_CreateEnemyUnitHandler-Id-57: " + unit.Id + " MaxHp: " + num[NumericType.MaxHp] + " MaxHpBase: " + num[NumericType.MaxHpBase] + " MaxHpAdd: " + num[NumericType.MaxHpAdd]);
        }

        void SpawnMyPlayerUnit(Unit unit)
        {
            /// 创建 本人的 unit
            M2C_CreateUnits createUnits = new M2C_CreateUnits();
            UnitInfo unitInfo = new UnitInfo();
            unitInfo.X = unit.Position.x;
            unitInfo.Y = unit.Position.y;
            unitInfo.Z = unit.Position.z;
            unitInfo.UnitId = unit.Id;
            createUnits.Units.Add(unitInfo);          
            MessageHelper.Broadcast(createUnits);
        }

        void BroadcastPlayerUnit()
        {
            /// 广播创建的unit
            M2C_CreateUnits createUnits = new M2C_CreateUnits();
            Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
            foreach (Unit u in units)
            {
                UnitInfo unitInfo = new UnitInfo();
                unitInfo.X = u.Position.x;
                unitInfo.Y = u.Position.y;
                unitInfo.Z = u.Position.z;
                unitInfo.UnitId = u.Id;
                createUnits.Units.Add(unitInfo);
            }
            MessageHelper.Broadcast(createUnits);
        }

        void BroadcastEnemyUnit(Unit unit)
        {
                ///20190613 通知map 广播刷新小怪 NPC Enemy unit                
                Console.WriteLine(" G2M_CreateUnitHandler-51: " + "通知 map服务器 向客户端 刷新小怪");
                IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                mapSession.Send(new M2M_GetEnemyUnit() { playerUnitId = unit.Id });

                Console.WriteLine(" G2M_CreateUnitHandler-51: " + "通知 map服务器 向客户端 刷新 NPC");
                ///TOTO......

        }
             

    }
}