using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class UnitInventoryComponentAwakeSystem : AwakeSystem<UnitInventoryComponent>
    {
        public override void Awake(UnitInventoryComponent self)
        {
            self.Awake();
        }
    }

    public class UnitInventoryComponent : Entity
    {
        public Dictionary<long, int> idInventoryLevels = new Dictionary<long, int>();         // 我有那些衣服，及其等级；背包
        public Dictionary<long, int> idDressedLevels = new Dictionary<long, int>();           // 我身上穿着的衣服,及其等级
        public InventoryItem curItem;                                                         // 我的当前技能（最近一次使用用的技能）

        public void Awake()
        {
            this.AddComponent<NumericComponent>();
        }

    }
}
