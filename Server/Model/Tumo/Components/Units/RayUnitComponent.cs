using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class RayUnitComponentAwakeSystem : AwakeSystem<RayUnitComponent>
    {
        public override void Awake(RayUnitComponent self)
        {
            self.Awake();
        }
    }

    public class RayUnitComponent : Component
    {
        public static RayUnitComponent Instance { get; set; }

        public void Awake()
        {
            Instance = this;
        }

        public Unit target { get; set; }

    }
}
