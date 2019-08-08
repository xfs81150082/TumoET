using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Linq;

namespace ETModel
{
    public class SouerComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, Souler> idSoulers = new Dictionary<long, Souler>();

        public readonly Dictionary<long, SoulerItem> idSoulerItems = new Dictionary<long, SoulerItem>();

        public void Awake()
        {
            /// idSkillItems idSkills
            SoulerInfo skillItemInfo = new SoulerInfo();

            foreach (Souler tem in skillItemInfo.Soulers.Values)
            {
                this.idSoulers.Add(tem.Id, tem);
            }

            foreach (SoulerItem tem in skillItemInfo.SoulerItems.Values)
            {
                this.idSoulerItems.Add(tem.Id, tem);
            }

        }

        #region SkillItem
        public void Add(SoulerItem item)
        {
            this.idSoulerItems.Add(item.Id, item);
        }

        public SoulerItem Get(long id)
        {
            this.idSoulerItems.TryGetValue(id, out SoulerItem item);
            return item;
        }

        public void Remove(long id)
        {
            SoulerItem unit;
            this.idSoulerItems.TryGetValue(id, out unit);
            this.idSoulerItems.Remove(id);
            unit?.Dispose();
        }

        public int Count
        {
            get
            {
                return this.idSoulerItems.Count;
            }
        }

        public long[] GetSkillItemIdsAll()
        {
            return this.idSoulerItems.Keys.ToArray();
        }

        public SoulerItem[] GetSkillItemAll()
        {
            return this.idSoulerItems.Values.ToArray();
        }

        #endregion

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (SoulerItem unit in this.idSoulerItems.Values)
            {
                unit.Dispose();
            }
            this.idSoulerItems.Clear();

            foreach (Souler unit in this.idSoulers.Values)
            {
                unit.Dispose();
            }
            this.idSoulers.Clear();
        }

    }
}
