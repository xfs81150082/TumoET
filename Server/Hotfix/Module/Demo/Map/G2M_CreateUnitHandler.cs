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
                unit.Position = new Vector3(player.spawnPosition.x, player.spawnPosition.y, player.spawnPosition.z);

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
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<RecoverComponent>();
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<UnitSkillComponent>();
                unit.AddComponent<RayUnitComponent>();

                //unit.AddComponent<KeyboardPathComponent>();
                //unit.AddComponent<TurnAnglesComponent>();
                //unit.AddComponent<UnitAnglesComponent>();
                //unit.AddComponent<UnitPositionComponent>();

                unit.AddComponent<MovePositionComponent>();
                unit.AddComponent<UnitDirComponent>();
                unit.AddComponent<MapPathComponent>();

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
            NumericComponent numPlayer = player.GetComponent<NumericComponent>();

            num[NumericType.MaxHpAdd] = numPlayer[NumericType.MaxHpAdd]; // MaxHpAdd 数值,进行赋值
            num[NumericType.HpAdd] = numPlayer[NumericType.HpAdd]; // HpAdd 数值,进行赋值

            unit.GetComponent<MoveComponent>().moveSpeed = 4.0f;

            Console.WriteLine(" M2M_CreateEnemyUnitHandler-Id-101: " + unit.Id + " MaxHp: " + num[NumericType.MaxHp] + " MaxHpBase: " + num[NumericType.MaxHpBase] + " MaxHpAdd: " + num[NumericType.MaxHpAdd]);
        }
        
        #endregion

        #region
        protected async ETVoid CreateMonsterRunAsync(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
        {
            M2G_CreateUnit response = new M2G_CreateUnit();
            try
            {
                Monster monster = Game.Scene.GetComponent<MonsterComponent>().Get(message.RolerId);
                if (monster == null) return;
                Unit unit0 = Game.Scene.GetComponent<MonsterUnitComponent>().Get(monster.UnitId);
                if (unit0 != null)
                {
                    return;
                }
                Unit unit = ComponentFactory.CreateWithId<Unit>(IdGenerater.GenerateId());
                unit.AddComponent<MoveComponent>();
                unit.AddComponent<UnitPathComponent>();
                unit.Position = new Vector3(monster.spawnPosition.x, monster.spawnPosition.y, monster.spawnPosition.z);

                await unit.AddComponent<MailBoxComponent>().AddLocation();
                Game.Scene.GetComponent<MonsterUnitComponent>().Add(unit);

                response.UnitId = unit.Id;
                reply(response);

                ///20190702
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Monster);
                unit.AddComponent<AoiUnitComponent>();
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<RecoverComponent>();
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<UnitSkillComponent>();
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
            num[NumericType.HpAdd] = numEnemy[NumericType.HpAdd];

            ///小怪当前速度
            unit.GetComponent<MoveComponent>().moveSpeed = 2.0f;

            Console.WriteLine(" M2M_CreateEnemyUnitHandler-Id-158: " + unit.Id + " MaxHp: " + num[NumericType.MaxHp] + " MaxHpBase: " + num[NumericType.MaxHpBase] + " MaxHpAdd: " + num[NumericType.MaxHpAdd]);
        }

        #endregion

    }
}
