using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
        public string map { get; set; }

        public long UnitId { get; set; }

        public Vector3 spawnPosition;
        
        public Enemy() { }
        
        public Enemy(string map)
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