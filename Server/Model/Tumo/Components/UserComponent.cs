using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class UserComponentAwakeSystem : AwakeSystem<UserComponent>
    {
        public override void Awake(UserComponent self)
        {
            self.Awake();
        }
    }

    public class UserComponent : Component
    {
        public Dictionary<long, User> idUsers = new Dictionary<long, User>();

        public void Awake()
        {
            UserInfo userInfo = ComponentFactory.Create<UserInfo>();

            GetIdUser(userInfo.users.Values.ToArray());

            GetPlayers(userInfo.players.Values.ToArray());

            Console.WriteLine(" UserComponent-30-idUsers/Players: " + idUsers.Count +" / "+ Game.Scene.GetComponent<PlayerComponent>().Count );
        }

        void GetIdUser(User[] users)
        {
            foreach (User tem in users)
            {
                if (!idUsers.Keys.Contains(tem.Id))
                {
                    idUsers.Add(tem.Id, tem);
                }
            }
        }
        void GetPlayers(Player[] Players)
        {
            foreach (Player tem in Players)
            {
                if (Game.Scene.GetComponent<PlayerComponent>()!=null)
                {
                    Game.Scene.GetComponent<PlayerComponent>().Add(tem);
                }
            }
        }
        public User Get(string account)
        {
            User user = null;
            foreach(User tem in this.idUsers.Values .ToArray())
            {
                if(tem.Account == account)
                {
                    user = tem;
                }
            }
            return user;
        }
        public User Get(long id)
        {
            this.idUsers.TryGetValue(id, out User user);
            return user;
        }

    }
}
