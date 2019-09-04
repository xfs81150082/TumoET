using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class Playerdb : ComponentWithId
    {
        public long userid = 0L;
        public string name = "图末";
        public List<double> spawnVec = new List<double>();
        public int level = 10;
        public int exp = 0;
        public int hp = 40;
        public string createdate;
    }
}
