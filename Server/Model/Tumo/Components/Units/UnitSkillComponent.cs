using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class UnitSkillComponent : Component
    {
        public string currentKey;
        public Dictionary<string, long> keycodeIds = new Dictionary<string, long>();
        public Dictionary<long, int> idLevels = new Dictionary<long, int>();
        public SkillItem curSkillItem;
        public Root root;

    }
}
