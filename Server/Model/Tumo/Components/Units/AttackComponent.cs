using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class AttackComponent : Component
    {
        public bool isBattling = false;                                    //表示是否战斗状态
        public bool isDeath = false;
        public bool isSettlement = false;

        public bool deathNull = false;
        public long deathTime = 0;
        public long lifeCdTime = 40;

        public bool startNull = false;
        public long startTime = 0;
        public long attcdTime = 2;


        public float targetDistance = float.PositiveInfinity;
        public float attackDis = 25.0f;
        public float battlingDis = 225.0f;

        public Unit attacker { get; set; }                                  //正在攻击我的敌人
        public Unit target { get; set; }                                    //我正在攻击的敌人
        public HashSet<long> attackers = new HashSet<long>();               //已攻击到我的敌人
        public HashSet<long> targeters = new HashSet<long>();               //已被我攻击到的敌人
        public Queue<SkillItem> TakeDamages = new Queue<SkillItem>();       //技能减伤列表集合
        public Queue<SkillItem> AddBuffs = new Queue<SkillItem>();          //技能减伤列表集合




    }
}
