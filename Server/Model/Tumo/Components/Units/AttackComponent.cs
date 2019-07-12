using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class AttackComponent : Component
    {
        public bool isAttacking = false;                                 //表示是否战斗状态
        //public bool isDeath = false;                                     //表示是否死亡状态
        //public float enterAttackDistance = 225.0f;
        //public bool isSettlement = false;

        public bool deathNull = false;
        public long deathTime = 0;
        public long lifeCdTime = 40;

        public bool startNull = false;
        public long startTime = 0;
        public long attcdTime = 2;

        public float attackDistance = float.PositiveInfinity;
        public float cdDistance = 25.0f;

        //public object curSkill;
        public Unit target { get; set; }
        public HashSet<long> attackers = new HashSet<long>();
        public HashSet<long> targeters = new HashSet<long>();

        //public Dictionary<long, Unit> Attackers = new Dictionary<long, Unit>();
        //public List<Unit> targets = new List<Unit>();

    }
}