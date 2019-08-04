using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    public class SkillComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, SkillItem> idSkillItems = new Dictionary<long, SkillItem>();

        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, Skill> idSkills = new Dictionary<long, Skill>();

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

        public void Add(SkillItem unit)
        {
            this.idSkillItems.Add(unit.Id, unit);
        }

        public SkillItem Get(long id)
        {
            this.idSkillItems.TryGetValue(id, out SkillItem unit);
            return unit;
        }

        public void Remove(long id)
        {
            SkillItem unit;
            this.idSkillItems.TryGetValue(id, out unit);
            this.idSkillItems.Remove(id);
            unit?.Dispose();
        }

        public void RemoveNoDispose(long id)
        {
            this.idSkillItems.Remove(id);
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


    }
}
