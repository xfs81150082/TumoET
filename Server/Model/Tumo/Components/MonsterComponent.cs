﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private readonly Dictionary<long, Monster> IdEnemys = new Dictionary<long, Monster>();

        public void Awake()
        {
            Instance = this;
        }

        public void AddAll(Monster[] enemys)
        {
            foreach (Monster tem in enemys)
            {
                if (IdEnemys.Keys.Contains(tem.Id)) return;
                this.IdEnemys.Add(tem.Id, tem);
                tem.Parent = this;
            }
        }
        public void Add(Monster booker)
        {
            this.IdEnemys.Add(booker.Id, booker);
            booker.Parent = this;
        }

        public Monster Get(long id)
        {
            this.IdEnemys.TryGetValue(id, out Monster booker);
            return booker;
        }
        public Monster GetByUnitId(long id)
        {
            foreach(Monster tem in GetAll())
            {
                if (tem.UnitId == id)
                {
                    return tem;
                }
            }
            return null;
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

        public Monster[] GetAll()
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

            foreach (Monster booker in this.IdEnemys.Values)
            {
                booker.Dispose();
            }

            this.IdEnemys.Clear();

            Instance = null;
        }

    }
}