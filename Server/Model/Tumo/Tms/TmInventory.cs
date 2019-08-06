using System;

namespace ETModel
{
    public class TmInventory : Component
    {
        public string Name { get; set; } = "tumo";
        public int Id { get; set; } = 100001;
        public int ParentId { get; set; } = 0;
        public string Icon { get; set; } = "headimagegirl";                 //图标
        public string AvatarName { get; set; } = "15101_AncientWarrior";    //预制体名称
        public string Chater { get; set; } = "111";                            //章节号 
        public int LevelUpLimit { get; set; } = 30;                         //等级上级 
        public string Does { get; set; } = "人族是最智慧的生灵之一";        //简介  

        public RoleType RoleType { get; set; } = RoleType.Engineer;         //种族和职业：造价师、监理师、建造师
        public EquipType EquipType { get; set; } = EquipType.Weapon;        //装备类型（帽子，衣，鞋子，武器，项链。。。等）  套装技能
        public InfoType InfoType { get; set; } = InfoType.Bp;               //作用类型，表示作用在那个属性之上
        public Quality Quality { get; set; } = Quality.Green;               //品质等级（白绿蓝紫橙）//怪的级别：普通-白、精英-蓝、首领-橙
        public int DamageDis { get; set; } = 10;
        public int Duration { get; set; } = 120;                            //持续时间
        public int MaxColdTime { get; set; } = 4;                              //冷却时间
        public bool Start { get; set; } = false;
        public bool End { get; set; } = false;

        public int Stamina { get; set; } = 0;            //耐力，案例，每升1级加1，影响血量
        public int Brains { get; set; } = 0;             //智力，计价，每升1级加1，影响魔法攻击强度，魔法防御
        public int Power { get; set; } = 0;              //力量，计量，每升1级加1，影响物理攻击强度，物理防御
        public int Agility { get; set; } = 0;            //敏捷，管理，每升1级加1，影响暴击率，命中率，闪避率 
        public int Hp { get; set; } = 0;                 //当前血量
        public int Mp { get; set; } = 0;                 //当前血量
        public int MaxHp { get; set; } = 0;              //HP=10 * 耐力 + 装备 + 法术；暂定10倍；
        public int MaxMp { get; set; } = 0;              //MP=10 * Level;
        public int Bp { get; set; } = 0;                 //Bp与智力成正比；暂定1倍；
        public int Ap { get; set; } = 0;                 //Ap与力量成正比；暂定1倍；
        public double Hr { get; set; } = 0.0f;           //hr命中率与敏捷成正比。
        public double Cr { get; set; } = 0.0f;           //cr暴击率与敏捷成正比。
        public double Sp { get; set; } = 0.0f;           //移动速度,与敏捷成正比。 
        public double StaminaRate { get; set; } = 0.0f;  //耐力，案例，每升1级加1，影响血量
        public double BrainsRate { get; set; } = 0.0f;   //智力，计价，每升1级加1，影响魔法攻击强度，魔法防御
        public double PowerRate { get; set; } = 0.0f;    //力量，计量，每升1级加1，影响物理攻击强度，物理防御
        public double AgilityRate { get; set; } = 0.0f;  //敏捷，管理，每升1级加1，影响暴击率，命中率，闪避率 
        public double HpRate { get; set; } = 0.0f;       //当前血量
        public double MpRate { get; set; } = 0.0f;       //当前血量
        public double MaxHpRate { get; set; } = 0.0f;    //HP=10 * 耐力 + 装备 + 法术；暂定10倍；
        public double MaxMpRate { get; set; } = 0.0f;    //MP=10 * Level;
        public double ApRate { get; set; } = 0.0f;       //Ap与力量成正比；暂定1倍；
        public double BpRate { get; set; } = 0.0f;       //Bp与智力成正比；暂定1倍；
        public double HrRate { get; set; } = 0.0f;       //hr命中率与敏捷成正比。
        public double CrRate { get; set; } = 0.0f;       //cr暴击率与敏捷成正比。
        public double SpRate { get; set; } = 0.0f;       //移动速度 
    }
}
