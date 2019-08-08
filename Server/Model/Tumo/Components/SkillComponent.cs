using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class SkillComponentAwakeSystem : AwakeSystem<SkillComponent>
    {
        public override void Awake(SkillComponent self)
        {
            self.Awake();
        }
    }

    public class SkillComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, Skill> idSkills = new Dictionary<long, Skill>();

        public readonly Dictionary<long, SkillItem> idSkillItems = new Dictionary<long, SkillItem>();

        public void Awake()
        {
            SkillItemInfo  Info = new SkillItemInfo();
            foreach (Skill tem in  Info.idSkills.Values.ToArray())
            {
                this.idSkills.Add(tem.Id, tem);
            }
        } 

        #region SkillItem
        public void Add(SkillItem item)
        {
            this.idSkillItems.Add(item.Id, item);
        }     

        public SkillItem Get(long id)
        {
            this.idSkillItems.TryGetValue(id, out SkillItem item);
            return item;
        }

        public void Remove(long id)
        {
            SkillItem unit;
            this.idSkillItems.TryGetValue(id, out unit);
            this.idSkillItems.Remove(id);
            unit?.Dispose();
        }

        public int Count
        {
            get
            {
                return this.idSkillItems.Count;
            }
        }

        public long[] GetSkillItemIdsAll()
        {
            return this.idSkillItems.Keys.ToArray();
        }

        public SkillItem[] GetSkillItemAll()
        {
            return this.idSkillItems.Values.ToArray();
        }
        
        #endregion

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (SkillItem unit in this.idSkillItems.Values)
            {
                unit.Dispose();
            }
            this.idSkillItems.Clear();

            foreach (Skill unit in this.idSkills.Values)
            {
                unit.Dispose();
            }
            this.idSkills.Clear();
        }


    }
}
