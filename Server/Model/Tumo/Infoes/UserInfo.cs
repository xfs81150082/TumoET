using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class UserInfo : Entity
    {
        public UserInfo()
        {
            GetEnemyFromBD();
        }
        public Dictionary<long, User> users = new Dictionary<long, User>();

        public Dictionary<long, Player> players = new Dictionary<long, Player>();

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        /// <param name="self"></param>
        void GetEnemyFromBD()
        {
            try
            {
                /// 先向 BD 服务器 初始化小怪数据
                //IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                //Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                //mapSession.Send(new M2M_CreateEnemyUnit() { Count = 4 });


                ///生产Users数据Info///生产Player数据Info                
                GetUsers();

                //Console.WriteLine(" UserInfo-36-users: " + users.Count);
                //Console.WriteLine(" UserInfo-37-players: " + players.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void GetUsers()
        {
            User user1 = ComponentFactory.CreateWithId<User>(101);
            user1.Count = 2;
            users.Add(user1.Id, user1);

            User user2 = ComponentFactory.CreateWithId<User>(102);
            user2.Count = 2;
            user2.Account = "tumo";
            user2.Password = "123456";
            users.Add(user2.Id, user2);

            GetPlayersByUsers(users.Values.ToArray());

        }

        void GetPlayersByUsers(User[] users)
        {
            foreach(User tem in users)
            {
                GetPlayers(tem);
            }
        }

        void GetPlayers(User user)
        {
            for (int i = 0; i < user.Count; i++)
            {
                Player player = ComponentFactory.CreateWithId<Player>(IdGenerater.GenerateId());
                player.UserId = user.Id;
                player.Account = user.Account;

                player.AddComponent<NumericComponent>();
                player.AddComponent<LifeCDComponent>();

                player.GetComponent<NumericComponent>().Set(NumericType.MaxHpAdd, 20);
                player.GetComponent<LifeCDComponent>().lifeCD = 2;
                player.GetComponent<LifeCDComponent>().unitType = UnitType.Player;
                player.spawnPosition = new Vector3(-40, 0, -10);

                players.Add(player.Id, player);
            }
        }

    }
}