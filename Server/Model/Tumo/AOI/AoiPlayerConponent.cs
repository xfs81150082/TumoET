using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class AoiPlayerComponentAwakeSystem : AwakeSystem<AoiPlayerComponent>
    {
        public override void Awake(AoiPlayerComponent self)
        {
            self.Awake();
        }
    }

    public class AoiPlayerComponent : Component
    {
        public long changeId = -1;
        public long changePlayers = -1;
        public long changeMonsters = -1;
        public long changeNpcers = -1;

        public bool startNull = false;
        public long startTime = 0;
        public long resTime = 1;


        public void Awake()
        {

            Console.WriteLine(" AoiPlayerComponent-21-unitId: " + this.GetParent<Unit>().Id);
        }


    }
}
