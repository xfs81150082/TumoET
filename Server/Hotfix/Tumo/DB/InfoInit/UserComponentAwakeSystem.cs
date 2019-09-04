using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class UserComponentAwakeSystem : AwakeSystem<UserComponent>
    {
        public override void Awake(UserComponent self)
        {
            //InfoInit();

            //InfoSave();
        }

        public void InfoInit()
        {
            UserInfo userInfo = ComponentFactory.Create<UserInfo>();

            //GetIdUser(userInfo.users);

            //GetPlayers(userInfo.players);

            //Console.WriteLine(" UserComponent-30-idUsers/Players: " + idUsers.Count + " / " + Game.Scene.GetComponent<PlayerComponent>().Count);
        }

        void GetIdUser(HashSet<User> users)
        {
            foreach (User tem in users)
            {
                if (Game.Scene.GetComponent<UserComponent>() != null)
                {
                    Game.Scene.GetComponent<UserComponent>().Add(tem);
                }
            }
        }

        void GetPlayers(HashSet<Player> Players)
        {
            foreach (Player tem in Players)
            {
                if (Game.Scene.GetComponent<PlayerComponent>() != null)
                {
                    Game.Scene.GetComponent<PlayerComponent>().Add(tem);
                }
            }
        }

        void InfoSave()
        {
            SaveUser(Game.Scene.GetComponent<UserComponent>().GetAll());

            SavePlayer(Game.Scene.GetComponent<PlayerComponent>().GetAll());
        }

        void SaveUser(User[] users)
        {
            DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

            foreach (User tem in users)
            {
                Userdb userdb = ComponentFactory.CreateWithId<Userdb>(tem.Id);
                userdb.account = tem.Account;
                userdb.password = tem.Password;
                userdb.phonenumber = tem.phonenumber;
                userdb.createdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

                dBProxy.Save(userdb).Coroutine();
            }
        }

        void SavePlayer(Player[] players)
        {
            DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

            foreach (Player tem in players)
            {
                Playerdb playerdb = ComponentFactory.CreateWithId<Playerdb>(tem.Id);
                playerdb.level = 10;
                playerdb.exp = 0;
                playerdb.hp = 40;
                playerdb.spawnVec.Add(tem.spawnPosition.x);
                playerdb.spawnVec.Add(tem.spawnPosition.y);
                playerdb.spawnVec.Add(tem.spawnPosition.z);

                dBProxy.Save(playerdb).Coroutine();
            }
        }


    }
}
