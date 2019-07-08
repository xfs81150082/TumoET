using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class SkillComponent
    {
        public Dictionary<int, object> Buffs { get; set; } = new Dictionary<int, object>();
        public Dictionary<int, object> Abilitys { get; set; } = new Dictionary<int, object>();
        public Dictionary<int, object> Inborns { get; set; } = new Dictionary<int, object>();
    }
}
