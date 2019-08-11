using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_CreateUnitHandler : AMRpcHandler<T2M_CreateUnit, M2T_CreateUnit>
    {
        protected override void Run(Session session, T2M_CreateUnit message, Action<M2T_CreateUnit> reply)
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

        #region 在玩家登录游戏时，接受登录Handler发来的消息，在map服务器上创建战斗Unit-Player
        /// <summary>
        /// 在玩家登录游戏时，接受登录Handler发来的消息，在map服务器上创建战斗Unit
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        protected async ETVoid CreatePlayerRunAsync(Session session, T2M_CreateUnit message, Action<M2T_CreateUnit> reply)
        {
            Console.WriteLine(" M2M_CreateUnitHandler-35: " + session.InstanceId + " / " + message.GateSessionId);

            M2T_CreateUnit response = new M2T_CreateUnit();
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
                unit.Position = new Vector3(-40, 0, -20);
                unit.Position = new Vector3(player.spawnPosition.x, player.spawnPosition.y, player.spawnPosition.z);

                await unit.AddComponent<MailBoxComponent>().AddLocation();
                unit.AddComponent<UnitGateComponent, long>(message.GateSessionId);
                Game.Scene.GetComponent<UnitComponent>().Add(unit);

                if (message.UnitId == 0) { }

                response.UnitId = unit.Id;
                reply(response);

                ///20190702 玩家
                Game.EventSystem.Awake<UnitType>(unit, UnitType.Player);
                unit.AddComponent<AoiUnitComponent>();
                unit.AddComponent<AoiPlayerComponent>();    //玩家独有
                unit.AddComponent<NumericComponent>();
                unit.AddComponent<RecoverComponent>().isDeath = false;
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<RayUnitComponent>();

                unit.AddComponent<MovePositionComponent>();
                unit.AddComponent<UnitDirComponent>();
                unit.AddComponent<MapPathComponent>();

                unit.GetComponent<NumericComponent>().PlayerNumericInit();
                unit.GetComponent<MoveComponent>().moveSpeed = 4.0f;

                Console.WriteLine(" M2M_CreateUnitHandler-78-gateActorid: " + unit.GetComponent<UnitGateComponent>().GateSessionActorId);
                Console.WriteLine(" M2M_CreateUnitHandler-79-Players: " + Game.Scene.GetComponent<UnitComponent>().Count);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
        #endregion

        #region 在服务器启动时，在map服务器上创建战斗Unit-Monster
        protected async ETVoid CreateMonsterRunAsync(Session session, T2M_CreateUnit message, Action<M2T_CreateUnit> reply)
        {
            M2T_CreateUnit response = new M2T_CreateUnit();
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
                unit.AddComponent<RecoverComponent>().isDeath = false;
                unit.AddComponent<SqrDistanceComponent>();
                unit.AddComponent<AttackComponent>();
                unit.AddComponent<PatrolComponent>();
                unit.AddComponent<SeeComponent>();

                unit.GetComponent<NumericComponent>().MonsterNumericInit();
                unit.GetComponent<MoveComponent>().moveSpeed = 2.0f;

                Console.WriteLine(" M2M_CreateUnitHandler-125-Monsters: " + Game.Scene.GetComponent<MonsterUnitComponent>().Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion


    }
}
