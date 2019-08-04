using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    public class KeyboardSkillComponentUpdateSystem : UpdateSystem<KeyboardSkillComponent>
    {
        public override void Update(KeyboardSkillComponent self)
        {
            self.Update();
        }
    }

    public class KeyboardSkillComponent : Component
    {
        public string key;

        public Dictionary<string, long> skillIds = new Dictionary<string, long>();

        public void Update()
        {
            if (key != null)
            {
                CurSkillId(key);
            }
        }
        
        public long CurSkillId(string keyname)
        {
            skillIds.TryGetValue(keyname, out long tem);
            return tem;
        }

        public void Add(string keyname,long skillId)
        {
            skillIds.Add(keyname, skillId);
        }

        public void Remove(string keyname)
        {
            skillIds.Remove(keyname);
        }

        public int Count { get { return skillIds.Count; } }

        public override void Dispose()
        {
            base.Dispose();
            skillIds.Clear();
        }

    }
}
