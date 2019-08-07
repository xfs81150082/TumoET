using System;

namespace ETModel
{
    [ObjectSystem]
    public class TaskerItemAwakeSystem : AwakeSystem<TaskerItem>
    {
        public override void Awake(TaskerItem self)
        {
            self.Awake();
        }
    }

    public class TaskerItem : Entity
    {
        public void Awake()
        {
            this.AddComponent<Namer>();
            this.AddComponent<ChangeType>();
            this.AddComponent<NumericComponent>();
            this.AddComponent<Tasker>();
            this.AddComponent<TaskerDB>();
        }

        public TaskerItem() {   }

        public TaskerItem(TaskerDB itemDB)
        {
            if (this.GetComponent<TaskerDB>() != null)
            {
                this.RemoveComponent<TaskerDB>();
            }
            this.AddComponent(itemDB);
            this.GetComponent<Namer>().Name = this.GetComponent<TaskerDB>().Name;
            this.GetComponent<Namer>().Id = this.GetComponent<TaskerDB>().Id;
            this.GetComponent<Namer>().ParentId = this.GetComponent<TaskerDB>().RolerId;
            this.GetComponent<ChangeType>().Exp = this.GetComponent<TaskerDB>().Exp;
            this.GetComponent<ChangeType>().Level = this.GetComponent<TaskerDB>().Level;
            this.GetComponent<ChangeType>().TaskState = this.GetComponent<TaskerDB>().TaskState;
        }

        public TaskerDB CreateSkillDB()
        {
            this.GetComponent<TaskerDB>().Name = this.GetComponent<Namer>().Name;
            this.GetComponent<TaskerDB>().Id = this.GetComponent<Namer>().Id;
            this.GetComponent<TaskerDB>().RolerId = this.GetComponent<Namer>().ParentId;
            this.GetComponent<TaskerDB>().Exp = this.GetComponent<ChangeType>().Exp;
            this.GetComponent<TaskerDB>().Level = this.GetComponent<ChangeType>().Level;
            this.GetComponent<TaskerDB>().TaskState = this.GetComponent<ChangeType>().TaskState;
            return this.GetComponent<TaskerDB>();
        }


    }
}
