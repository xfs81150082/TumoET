using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{ 
    public class UserComponent : Component
    {
        public Dictionary<long, User> idUsers = new Dictionary<long, User>();

        public Dictionary<string, User> accountUsers = new Dictionary<string, User>();

        public User GetByAccount(string account)
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

        public void Add(User user)
        {
            if (!this.idUsers.Keys.Contains(user.Id))
            {
                this.idUsers.Add(user.Id, user);
            }
            if (!this.accountUsers.Keys.Contains(user.Account))
            {
                this.accountUsers.Add(user.Account, user);
            }
        }

        public User Get(long id)
        {
            this.idUsers.TryGetValue(id, out User user);
            return user;
        }

        public User Get(string account)
        {
            this.accountUsers.TryGetValue(account, out User user);
            return user;
        }
        public User[] GetAll()
        {
            return accountUsers.Values.ToArray();
        }

        public int Count
        {
            get { return idUsers.Count; }
        }


    }
}
