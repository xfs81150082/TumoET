using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{  
    public class RecoverComponent : Component
    {
        public bool isDeath = false;
        public bool isWarring = false;
        public bool isSettlement = false;

        public bool hpNull = false;
        public long hptimer = 0;
        public long reshpTime = 4;
        public float reshp = 0.15f;

        public bool mpNull = false;
        public long mptimer = 0;
        public long resmpTime = 4;
        public float resmp = 0.15f;

    }
}
