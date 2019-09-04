using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class Monsterdb : ComponentWithId
    {
        public string name = "计量山道人";
        public List<double> spawnVec = new List<double>();
        public int level = 10;
        public int exp = 0;
        public int hp = 40;
    }
}
