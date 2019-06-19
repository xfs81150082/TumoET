using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class EnemyAwakeSystem : AwakeSystem<Enemy, string>
    {
        public override void Awake(Enemy self, string a)
        {
            self.Awake(a);
        }
    }

    public sealed class Enemy : Entity
    {
        public string Account { get; private set; }

        public long UnitId { get; set; }
       
        public void Awake(string account)
        {
            this.Account = account;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
        }


    }
}
