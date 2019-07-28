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
            switch (message.UnitType)
            {
                case 0:
                    CreatePlayerRunAsync(session, message, reply).Coroutine();
                    break;
                case 1:
                    CreateMonsterRunAsync(session, message, reply).Coroutine();
                    break;
            }
        }

        #region
        /// <summary>
        /// 在玩家登录游戏时，接受登录Handler发来的消息，在map服务器上创建战斗Unit
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        protected async ETVoid CreatePlayerRunAsync(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
		{
			M2G_CreateUnit response = new M2G_CreateUnit();
            try
            {
                if (message.UnitId == 0)
                {
                    message.UnitId = IdGenerater.GenerateId();
                }
                Player player = Game.Scene.GetComponent<PlayerComponent>().Get(message.RolerId);
                Unit unit = ComponentFactory.CreateWithId<Unit>(message.UnitId);
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
                unit.AddComponent<AoiUnitComponent>();
                unit.AddComponent<AoiPlayerComponent>();    //玩家独有
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<RayUnitComponent>();
                unit.AddComponent<KeyboardPathComponent>();

                SetNumeric(unit ,player);

                ///给客户端 添加 玩家和小怪 单元实例
                //AddPlayers(Game.Scene.GetComponent<UnitComponent>().GetIdsAll(), new long[1] { unit.Id});
                //AddMonsters(Game.Scene.GetComponent<MonsterUnitComponent>().GetIdsAll(), new long[1] { unit.Id });

                Console.WriteLine(" G2M_CreateUnitHandler-75-UnitComponent: " + Game.Scene.GetComponent<UnitComponent>().Count);
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

            unit.GetComponent<MoveComponent>().moveSpeed = 4.0f;

            Console.WriteLine(" M2M_CreateEnemyUnitHandler-Id-57: " + unit.Id + " MaxHp: " + num[NumericType.MaxHp] + " MaxHpBase: " + num[NumericType.MaxHpBase] + " MaxHpAdd: " + num[NumericType.MaxHpAdd]);
        }

        /// <summary>
        /// 给客户端 添加 玩家 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        void AddPlayers(long[] unitIds, long[] playerUnitIds)
        {        
            /// 广播创建的unit
            M2M_AddUnits m2M_AddUnits = new M2M_AddUnits() { UnitType = (int)UnitType.Player, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            SessionHelper.MapSession().Send(m2M_AddUnits);
        }

        /// <summary>
        /// 给客户端 添加 小怪 单元实例
        /// </summary>
        /// <param name="unitIds"></param>
        /// <param name="unit"></param>
        void AddMonsters(long[] unitIds , long[] playerUnitIds)
        {
            /// 广播创建的unit
            M2M_AddUnits m2M_AddUnits = new M2M_AddUnits() { UnitType = (int)UnitType.Monster, UnitIds = unitIds.ToHashSet(), PlayerUnitIds = playerUnitIds.ToHashSet() };
            SessionHelper.MapSession().Send(m2M_AddUnits);
        }

        #endregion

        #region
        protected async ETVoid CreateMonsterRunAsync(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
        {
            M2G_CreateUnit response = new M2G_CreateUnit();
            try
            {
                if (message.UnitId == 0)
                {
                    message.UnitId = IdGenerater.GenerateId();
                }
                Monster monster = Game.Scene.GetComponent<MonsterComponent>().Get(message.RolerId);
                if (monster == null) return;
                Unit unit = ComponentFactory.CreateWithId<Unit>(message.UnitId);
                unit.AddComponent<MoveComponent>();
                unit.AddComponent<UnitPathComponent>();
                unit.Position = new Vector3(monster.spawnPosition.x, 0, monster.spawnPosition.z);

                await unit.AddComponent<MailBoxComponent>().AddLocation();
                Game.Scene.GetComponent<MonsterUnitComponent>().Add(unit);

                response.UnitId = unit.Id;
                reply(response);

                ///20190702
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Monster);
                unit.AddComponent<AoiUnitComponent>();
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<PatrolComponent>();
                unit.AddComponent<SeeComponent>();

                SetNumeric(unit, monster);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void SetNumeric(Unit unit, Monster enemy)
        {
            if (unit.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = unit.GetComponent<NumericComponent>();
            NumericComponent numEnemy = enemy.GetComponent<NumericComponent>();

            num[NumericType.MaxHpAdd] = numEnemy[NumericType.MaxHpAdd];

            ///小怪当前速度
            unit.GetComponent<MoveComponent>().moveSpeed = 2.0f;

        }
               
        #endregion

    }
}
