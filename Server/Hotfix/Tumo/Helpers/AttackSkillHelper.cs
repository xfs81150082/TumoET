using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class AttackSkillHelper
    {
        #region  死亡判断 后结算
        /// <summary>
        /// 死亡判断 后结算
        /// </summary>
        /// <param name="unit"></param>
        public static void DeathSettlement(Unit unit)
        {
            if (unit == null) return;
            if (unit.GetComponent<AttackComponent>() != null && unit.GetComponent<NumericComponent>() != null)
            {
                AttackComponent attack = unit.GetComponent<AttackComponent>();
                NumericComponent num = unit.GetComponent<NumericComponent>();

                if (num[NumericType.Hp] <= 0)
                {
                    attack.isDeath = true;

                    ///从格子 注销 因为已死亡
                    if (unit.GetComponent<AoiUnitComponent>() != null)
                    {
                        Game.Scene.GetComponent<AoiGridComponent>().Remove(unit.GetComponent<AoiUnitComponent>());
                    }

                    ///通知 播放 死亡录像
                    ///TOTO
                }
                else
                {
                    return;
                }

                if (attack.isDeath && attack.Attackers.Count > 0)
                {
                    foreach (Unit tem in attack.Attackers.Values.ToArray())
                    {
                        if (tem != null)
                        {
                            tem.GetComponent<NumericComponent>()[NumericType.Exp] += (tem.GetComponent<NumericComponent>()[NumericType.Level] * tem.GetComponent<NumericComponent>()[NumericType.Level] + 1);
                            tem.GetComponent<NumericComponent>()[NumericType.Coin] += 1;
                        }
                    }
                    attack.Attackers.Clear();
                    DestroyUnit(unit);
                    return;
                }
            }
            else
            {
                return;
            }
        }
        static void DestroyUnit(Unit unit)
        {
            switch (unit.UnitType)
            {
                case UnitType.Player:
                    unit.GetComponent<AttackComponent>().isDeath = true;
                    unit.GetComponent<AttackComponent>().isAttacking = false;
                    unit.GetComponent<NumericComponent>()[NumericType.HpAdd] = (int)(unit.GetComponent<NumericComponent>()[NumericType.MaxHp] * 0.10f);
                    unit.GetComponent<RayUnitComponent>().target = null;


                     //Game.Scene.GetComponent<UnitComponent>().Remove(unit.Id);
                   break;
                case UnitType.Monster:
                    unit.GetComponent<AttackComponent>().isDeath = true;
                    unit.GetComponent<AttackComponent>().isAttacking = false;
                    unit.GetComponent<SeeComponent>().target = null;

                    //unit.GetComponent<NumericComponent>()[NumericType.HpAdd] = (int)(unit.GetComponent<NumericComponent>()[NumericType.MaxHp] * 0.80f);
                    //unit.GetComponent<PatrolComponent>().isPatrol = true;
                    //PatrolComponentHelper.SendPatrolSpawnMap(unit.GetComponent<PatrolComponent>());

                    Game.Scene.GetComponent<EnemyUnitComponent>().Remove(unit.Id);
                    Game.Scene.GetComponent<EnemyComponent>().Get(unit.Id).GetComponent<LifeCDComponent>().isDeath = true;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ///暂时没有用
        public static void TakeDamage(this AttackComponent self)
        {
            //SetSkillBullet(this.Parent as Unit, Target);
            //this.GetComponent<TmAnimatorComponent>().attackState.Enqueue("skillbasic");
        }
        public static void SetSkillBullet(Unit self, Unit target)
        {
            //Unit prefab = Resources.Load("Skills/SkillBullet") as Unit;
            //GameObject go = GameObject.Instantiate(prefab) as GameObject;
            //if (go != null)
            //{
            //    go.transform.SetParent(TmSkillTransform.Instance.transform, true);
            //    go.GetComponent<Transform>().position = target.transform.position;
            //    go.GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
            //    go.GetComponent<TmSkillBullet>().TakeDamage(myself, target);
            //}
        }
        public static Unit[] GetTargets()
        {
            return null;
        }
        public static Unit GetTarget()
        {
            return null;
        }

        #endregion


    }
}