using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    class UserComponentStartSystem : StartSystem<UserComponent>
    {
        public override void Start(UserComponent self)
        {
            GetUserFromBD();
        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        void GetUserFromBD()
        {
            try
            {
                /// 先向 BD 服务器 读取数据 初始化小怪数据
                DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();
                List<long> ids = new List<long>() { 13655123801, 17751875107 };

                QueryMonsterdb(ids).Coroutine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async ETVoid QueryMonsterdb(List<long> ids)
        {
            DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

            List<ComponentWithId> testOnes = await dBProxy.Query<Userdb>(ids);

            foreach (Userdb tem in testOnes)
            {
                User user = tem.ToUser();

                /// 然后向 Gate 服务器 小怪数据放入字典                
                Game.Scene.GetComponent<UserComponent>().Add(user);
            }

            Console.WriteLine(" UserComponentStartSystem-59: " + testOnes.Count);
            Console.WriteLine(" UserComponentStartSystem-60: " + " BD服务器，User数量： " + Game.Scene.GetComponent<UserComponent>().Count);
        }

    }

    public static class UserHelper
    {

        public static User ToUser(this Userdb self)
        {
            User user = ComponentFactory.CreateWithId<User>(self.Id);
            user.Account = self.account;
            user.Password = self.password;
            user.phonenumber = self.phonenumber;
            user.playerids = self.playerids;
            user.createdate = self.createdate;
            return user;
        }

        public static Userdb ToUserdb(this User self)
        {
            Userdb userdb = ComponentFactory.CreateWithId<Userdb>(self.Id);
            userdb.account = self.Account;
            userdb.password = self.Password;
            userdb.phonenumber = self.phonenumber;
            userdb.playerids = self.playerids;
            userdb.createdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            return userdb;
        }

   
    }


}
