using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{  
    public static class UnitHelper
    {
        public static void SaveVector3(this Unit unit)
        {
            Player player = Game.Scene.GetComponent<PlayerComponent>().GetByUnitId(unit.Id);
            Vector3 unitVec = unit.Position;
            if (player != null)
            {
                player.spawnPosition = unitVec;
            }
        }

        public static T2M_CreateUnit T2M_CreateUnit(this Unit unit)
        {
            //重置生产Player单元Unit
            Player player = Game.Scene.GetComponent<PlayerComponent>().GetByUnitId(unit.Id);
            T2M_CreateUnit t2M_CreateUnit = new T2M_CreateUnit() { UnitType = (int)UnitType.Player, RolerId = player.Id, GateSessionId = unit.GetComponent<UnitGateComponent>().GateSessionActorId, UnitId = unit.Id };
            return t2M_CreateUnit;
        }

        public static void ToZero(this Unit unit)
        {
            unit.GetComponent<SqrDistanceComponent>().neastUnit = null;
            unit.GetComponent<SqrDistanceComponent>().neastDistance = float.PositiveInfinity;
            unit.GetComponent<AttackComponent>().target = null;
            unit.GetComponent<AttackComponent>().isBattling = false;

            if (unit.GetComponent<PatrolComponent>() != null)
            {
                unit.GetComponent<PatrolComponent>().isPatrol = true;
            }
            if (unit.GetComponent<SeekComponent>() != null)
            {
                unit.GetComponent<SeekComponent>().target = null;
            }
        }


        public static void UpdateLevel(this Unit unit)
        {
            ///unit 根据角色等级，初始化属性
            ///unit 根据装备，第二次初始化属性
            ///unit 根据技能Buff，第三次初始化属性
            ///unit 根据施放的当前技能，第四次初始化属性
            ///unit 得到最终属性
        }

        public static void UpdateEquip(this Unit unit)
        {
            ///unit 根据角色等级，初始化属性
            ///unit 根据装备，第二次初始化属性
            ///unit 根据技能Buff，第三次初始化属性
            ///unit 根据施放的当前技能，第四次初始化属性
            ///unit 得到最终属性
        }

        public static void UpdateBuffs(this Unit unit)
        {
            ///unit 根据角色等级，初始化属性
            ///unit 根据装备，第二次初始化属性
            ///unit 根据技能Buff，第三次初始化属性
            ///unit 根据施放的当前技能，第四次初始化属性
            ///unit 得到最终属性           
        }

        public static void UpdateSkillItem(this Unit unit)
        {
            ///unit 根据角色等级，初始化属性
            ///unit 根据装备，第二次初始化属性
            ///unit 根据技能Buff，第三次初始化属性
            ///unit 根据施放的当前技能，第四次初始化属性
            ///unit 得到最终属性
        }


    }
}
