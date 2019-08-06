using System;
using System.Collections.Generic;

namespace ETModel
{
    public class TmTask : Component
    {
        public int Stamina { get; set; } = 10;            //耐力，案例，每升1级加1，影响血量
        public int Brains { get; set; } = 10;             //智力，计价，每升1级加1，影响魔法攻击强度，魔法防御
        public int Power { get; set; } = 10;              //力量，计量，每升1级加1，影响物理攻击强度，物理防御
        public int Agility { get; set; } = 10;            //敏捷，管理，每升1级加1，影响暴击率，命中率，闪避率 
        public int Hp { get; set; } = 10;                 //当前血量
        public int Mp { get; set; } = 0;                 //当前血量
        public int MaxHp { get; set; } = 100;            //HP=10 * 耐力 + 装备 + 法术；暂定10倍；
        public int MaxMp { get; set; } = 10;             //MP=10 * Level;
        public int Bp { get; set; } = 0;                 //Bp与智力成正比；暂定1倍；
        public int Ap { get; set; } = 0;                 //Ap与力量成正比；暂定1倍；
    }
}
