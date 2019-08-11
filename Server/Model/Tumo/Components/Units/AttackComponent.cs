using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class AttackComponent : Entity
    {
        public bool isAttacking = false;                                 //表示是否战斗状态

        public bool deathNull = false;
        public long deathTime = 0;
        public long lifeCdTime = 40;

        public bool startNull = false;
        public long startTime = 0;
        public long attcdTime = 2;

        public float attackDistance = float.PositiveInfinity;
        public float cdDistance = 25.0f;

        public Unit attacker { get; set; }                                            //正在攻击我的敌人
        public Unit target { get; set; }                                                //我正在攻击的敌人
        public HashSet<long> attackers = new HashSet<long>();                          //已攻击到我的敌人
        public HashSet<long> targeters = new HashSet<long>();                         //已被我攻击到的敌人
        public Queue<SkillItem> TakeDamages = new Queue<SkillItem>();         //技能减伤列表集合
        public Queue<SkillItem> AddBuffs = new Queue<SkillItem>();         //技能减伤列表集合


        public string currentKey;
        public Dictionary<long, int> idSkillitemls = new Dictionary<long, int>();             // 我有那些技能，及其等级
        public Dictionary<string, long> keyIds = new Dictionary<string, long>();              // 我的技能与对应的按键  一般是3-4个技能
        public Dictionary<long, int> idBuffs = new Dictionary<long, int>();                   // 我身上挂的Buff,及其等级
        public SkillItem curSkillItem;                                                        // 我的当前技能（最近一次使用用的技能）


    }
}
