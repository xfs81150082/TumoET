using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class EnemyComponentAwakeSystem : AwakeSystem<EnemyComponent>
    {
        public override void Awake(EnemyComponent self)
        {
            self.Awake();
        }
    }

    public class EnemyComponentStartSystem : StartSystem<EnemyComponent>
    {
        public override void Start(EnemyComponent self)
        {
            self.Start();
        }
    }

    public class EnemyComponent : Component
    {
        public static EnemyComponent Instance { get; private set; }

        public Enemy Enemy;
       
        private readonly Dictionary<long, Enemy> IdEnemys = new Dictionary<long, Enemy>();

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {

        }               

        public void Add(Enemy booker)
        {
            this.IdEnemys.Add(booker.Id, booker);
            booker.Parent = this;
        }

        public Enemy Get(long id)
        {
            this.IdEnemys.TryGetValue(id, out Enemy booker);
            return booker;
        }

        public void Remove(long id)
        {
            this.IdEnemys.Remove(id);
        }

        public int Count
        {
            get
            {
                return this.IdEnemys.Count;
            }
        }

        public Enemy[] GetAll()
        {
            return this.IdEnemys.Values.ToArray();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (Enemy booker in this.IdEnemys.Values)
            {
                booker.Dispose();
            }

            this.IdEnemys.Clear();

            Instance = null;
        }

    }
}