using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    public class TmSkillItem : Entity
    {
        public void TmAwake()
        {
            AddComponent(new TmName());
            AddComponent(new TmSkill());
            AddComponent(new TmSkillDB());
            AddComponent(new TmChangeType());
        }

        public TmSkillItem() { }

        public TmSkillItem(TmSkillDB itemDB)
        {
            if (this.GetComponent<TmSkillDB>() != null)
            {
                this.RemoveComponent<TmSkillDB>();
            }
            this.AddComponent(itemDB);
            this.GetComponent<TmName>().Name = this.GetComponent<TmSkillDB>().Name;
            this.GetComponent<TmName>().Id = this.GetComponent<TmSkillDB>().Id;
            this.GetComponent<TmName>().ParentId = this.GetComponent<TmSkillDB>().RolerId;
            this.GetComponent<TmChangeType>().Exp = this.GetComponent<TmSkillDB>().Exp;
            this.GetComponent<TmChangeType>().Level = this.GetComponent<TmSkillDB>().Level;
        }
    }
}
