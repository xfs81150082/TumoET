using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class UserInfo : Component
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
                ///生产Users数据Info///生产Player数据Info                
                GetUsers();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void GetUsers()
        {
            User user1 = ComponentFactory.CreateWithId<User>(101);
            user1.Count = 1;
            users.Add(user1.Id, user1);

            User user2 = ComponentFactory.CreateWithId<User>(102);
            user2.Count = 1;
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
                player.spawnPosition = new Vector3(-40, 0, -20);

                player.AddComponent<NumericComponent>();

                player.GetComponent<NumericComponent>().Set(NumericType.MaxValuationAdd, 140);   // MaxHpAdd 数值,进行赋值
                player.GetComponent<NumericComponent>().Set(NumericType.ValuationAdd, 140);      // HpAdd 数值,进行赋值

                players.Add(player.Id, player);
            }
        }

    }
}