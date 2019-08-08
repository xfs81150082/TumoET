using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Linq;

namespace ETModel
{
    public class TaskerComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public readonly Dictionary<long, Tasker> idTaskers = new Dictionary<long, Tasker>();

        public readonly Dictionary<long, TaskerItem> idTaskerItems = new Dictionary<long, TaskerItem>();

        public void Awake()
        {
            /// idSkillItems idSkills
            TaskerInfo skillItemInfo = new TaskerInfo();

            foreach (Tasker tem in skillItemInfo.idTaskers.Values.ToArray())
            {
                this.idTaskers.Add(tem.Id, tem);
            }

            foreach (TaskerItem tem in skillItemInfo.idTaskerItems.Values.ToArray())
            {
                this.idTaskerItems.Add(tem.Id, tem);
            }

        }

        #region SkillItem
        public void Add(TaskerItem item)
        {
            this.idTaskerItems.Add(item.Id, item);
        }

        public TaskerItem Get(long id)
        {
            this.idTaskerItems.TryGetValue(id, out TaskerItem item);
            return item;
        }

        public void Remove(long id)
        {
            TaskerItem unit;
            this.idTaskerItems.TryGetValue(id, out unit);
            this.idTaskerItems.Remove(id);
            unit?.Dispose();
        }

        public int Count
        {
            get
            {
                return this.idTaskerItems.Count;
            }
        }

        public long[] GetSkillItemIdsAll()
        {
            return this.idTaskerItems.Keys.ToArray();
        }

        public TaskerItem[] GetSkillItemAll()
        {
            return this.idTaskerItems.Values.ToArray();
        }

        #endregion

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (TaskerItem unit in this.idTaskerItems.Values.ToArray())
            {
                unit.Dispose();
            }
            this.idTaskerItems.Clear();

            foreach (Tasker unit in this.idTaskers.Values.ToArray())
            {
                unit.Dispose();
            }
            this.idTaskers.Clear();
        }

    }
}
