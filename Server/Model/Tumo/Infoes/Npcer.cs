using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class NpcerAwakeSystem : AwakeSystem<Npcer, string>
    {
        public override void Awake(Npcer self, string a)
        {
            self.Awake(a);
        }
    }

    public sealed class Npcer : Entity
    {
        public string map { get; set; }

        public Vector3 spawnPosition;

        public Npcer() { }

        public Npcer(string map)
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