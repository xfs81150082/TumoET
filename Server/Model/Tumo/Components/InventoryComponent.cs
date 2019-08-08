using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    public class InventoryComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, Inventory> idInventorys = new Dictionary<long, Inventory>();

        public readonly Dictionary<long, InventoryItem> idInventoryItems = new Dictionary<long, InventoryItem>();

        public void Awake()
        {
            /// idSkillItems idSkills
            InventoryInfo skillItemInfo = new InventoryInfo();

            foreach (Inventory tem in skillItemInfo.Inventorys.Values)
            {
                this.idInventorys.Add(tem.Id, tem);
            }

            foreach (InventoryItem tem in skillItemInfo.InventoryItems.Values)
            {
                this.idInventoryItems.Add(tem.Id, tem);
            }

        }

        #region SkillItem
        public void Add(InventoryItem item)
        {
            this.idInventoryItems.Add(item.Id, item);
        }

        public InventoryItem Get(long id)
        {
            this.idInventoryItems.TryGetValue(id, out InventoryItem item);
            return item;
        }

        public void Remove(long id)
        {
            InventoryItem unit;
            this.idInventoryItems.TryGetValue(id, out unit);
            this.idInventoryItems.Remove(id);
            unit?.Dispose();
        }

        public int Count
        {
            get
            {
                return this.idInventoryItems.Count;
            }
        }

        public long[] GetSkillItemIdsAll()
        {
            return this.idInventoryItems.Keys.ToArray();
        }

        public InventoryItem[] GetSkillItemAll()
        {
            return this.idInventoryItems.Values.ToArray();
        }

        #endregion

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (InventoryItem unit in this.idInventoryItems.Values)
            {
                unit.Dispose();
            }
            this.idInventoryItems.Clear();

            foreach (Inventory unit in this.idInventorys.Values)
            {
                unit.Dispose();
            }
            this.idInventorys.Clear();
        }

    }
}
