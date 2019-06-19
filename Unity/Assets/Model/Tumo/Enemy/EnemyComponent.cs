using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public class EnemyComponent : Component
    {
        public static EnemyComponent Instance { get; private set; }

        private Enemy booker;
        public Enemy Enemy
        {
            get
            {
                return this.booker;
            }
            set
            {
                this.booker = value;
                this.booker.Parent = this;
            }
        }
        private readonly Dictionary<long, Enemy> IdBookers = new Dictionary<long, Enemy>();

        public void Awake()
        {
            Instance = this;
        }

   

        public void Add(Enemy booker)
        {
            this.IdBookers.Add(booker.Id, booker);
            booker.Parent = this;
        }

        public Enemy Get(long id)
        {
            Enemy booker;
            this.IdBookers.TryGetValue(id, out booker);
            return booker;
        }

        public void Remove(long id)
        {
            this.IdBookers.Remove(id);
        }

        public void RemoveNoDispose(long id)
        {
            this.IdBookers.Remove(id);
        }

        public int Count
        {
            get
            {
                return this.IdBookers.Count;
            }
        }

        public Enemy[] GetAll()
        {
            return this.IdBookers.Values.ToArray();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (Enemy booker in this.IdBookers.Values)
            {
                booker.Dispose();
            }

            this.IdBookers.Clear();

            Instance = null;
        }


    }
}
