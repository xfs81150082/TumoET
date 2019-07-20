using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class MonsterComponentAwakeSystem : AwakeSystem<MonsterComponent>
    {
        public override void Awake(MonsterComponent self)
        {
            self.Awake();
        }
    }

    public class MonsterComponent : Component
    {
        public static MonsterComponent Instance { get; private set; }

        private Monster booker;
        public Monster Enemy
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
        private readonly Dictionary<long, Monster> IdBookers = new Dictionary<long, Monster>();

        public void Awake()
        {
            Instance = this;
        }

   

        public void Add(Monster booker)
        {
            this.IdBookers.Add(booker.Id, booker);
            booker.Parent = this;
        }

        public Monster Get(long id)
        {
            Monster booker;
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

        public Monster[] GetAll()
        {
            return this.IdBookers.Values.ToArray();
        }

        public long[] GetIds()
        {
            return this.IdBookers.Keys.ToArray();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (Monster booker in this.IdBookers.Values)
            {
                booker.Dispose();
            }

            this.IdBookers.Clear();

            Instance = null;
        }


    }
}
