using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class SkillItemAwakeSystem : AwakeSystem<SkillItem>
    {
        public override void Awake(SkillItem self)
        {
            self.Awake();
        }
    }

    public class SkillItem : Entity
    {
        public void Awake()
        {
            this.AddComponent<Namer>();
            this.AddComponent<ChangeType>();
            this.AddComponent<NumericComponent>();
            this.AddComponent<Skill>();
            this.AddComponent<SkillDB>();
        }      

        public SkillItem() { }

        public SkillItem(SkillDB itemDB)
        {
            if (this.GetComponent<SkillDB>() != null)
            {
                this.RemoveComponent<SkillDB>();
            }
            this.AddComponent(itemDB);
            this.GetComponent<Namer>().Name = this.GetComponent<SkillDB>().Name;
            this.GetComponent<Namer>().Id = this.GetComponent<SkillDB>().Id;
            this.GetComponent<Namer>().ParentId = this.GetComponent<SkillDB>().RolerId;
            this.GetComponent<ChangeType>().Exp = this.GetComponent<SkillDB>().Exp;
            this.GetComponent<ChangeType>().Level = this.GetComponent<SkillDB>().Level;
        }

        public SkillDB CreateSkillDB()
        {
            this.GetComponent<SkillDB>().Name = this.GetComponent<Namer>().Name;
            this.GetComponent<SkillDB>().Id = this.GetComponent<Namer>().Id;
            this.GetComponent<SkillDB>().RolerId = this.GetComponent<Namer>().ParentId;
            this.GetComponent<SkillDB>().Exp = this.GetComponent<ChangeType>().Exp;
            this.GetComponent<SkillDB>().Level = this.GetComponent<ChangeType>().Level;
            return this.GetComponent<SkillDB>();
        }

    }
}
