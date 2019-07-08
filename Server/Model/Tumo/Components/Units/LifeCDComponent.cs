using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class LifeCDComponent : Component
    {
        public bool isDeath = false;
        public bool isRemove = false;
        public UnitType unitType;

        public bool startNull = false;
        public long startTime = 0;
        public long remainingLifeCD = 0;
        public long lifeCD = 40;

    }
}