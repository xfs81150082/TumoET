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
        public void Awake()
        {
            SkillItemInfo  Info = new SkillItemInfo();
            foreach (Skill tem in  Info.idSkills.Values.ToArray())
            {
                this.idSkills.Add(tem.Id, tem);
            }
        } 

        #region SkillItem
        public void Add(Skill item)
        {
            this.idSkills.Add(item.Id, item);
        }     

        public Skill Get(long id)
        {
            this.idSkills.TryGetValue(id, out Skill item);
            return item;
        }

        public void Remove(long id)
        {
            Skill unit;
            this.idSkills.TryGetValue(id, out unit);
            this.idSkills.Remove(id);
            unit?.Dispose();
        }

        public int Count
        {
            get
            {
                return this.idSkills.Count;
            }
        }

        public long[] GetIdsAll()
        {
            return this.idSkills.Keys.ToArray();
        }

        public Skill[] GetAll()
        {
            return this.idSkills.Values.ToArray();
        }
        
        #endregion

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();        

            foreach (Skill unit in this.idSkills.Values)
            {
                unit.Dispose();
            }
            this.idSkills.Clear();
        }


    }
}
