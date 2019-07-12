using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class MonsterAwakeSystem : AwakeSystem<Monster, string>
    {
        public override void Awake(Monster self, string a)
        {
            self.Awake(a);
        }
    }

    public sealed class Monster : Entity
    {
        public string map { get; set; }

        public long UnitId { get; set; }

        public Vector3 spawnPosition;
        
        public Monster() { }
        
        public Monster(string map)
        {
            this.map = map;
        }
       
        public void Awake(string map)
        {
            this.map = map;
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