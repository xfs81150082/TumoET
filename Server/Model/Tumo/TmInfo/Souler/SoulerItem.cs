using System;
using System.Collections.Generic;

namespace ETModel
{
    [ObjectSystem]
    public class SoulerItemAwakeSystem : AwakeSystem<SoulerItem>
    {
        public override void Awake(SoulerItem self)
        {
            self.Awake();
        }
    }

    public class SoulerItem : Entity
    {
        public void Awake()
        {
            this.AddComponent<Namer>();
            this.AddComponent<ChangeType>();
            this.AddComponent<NumericComponent>();
            this.AddComponent<Souler>();
            this.AddComponent<SoulerDB>();
        }
        public SoulerItem() { }                        ///构造函数 
        public SoulerItem(SoulerDB itemDB)
        {
            this.AddComponent<SoulerDB>();

            this.GetComponent<Namer>().Id = itemDB.Id;
            this.GetComponent<Namer>().Name = itemDB.Name;
            this.GetComponent<Namer>().ParentId = itemDB.UserId;
            this.GetComponent<ChangeType>().Exp = itemDB.Exp;
            this.GetComponent<ChangeType>().Level = itemDB.Level;
            this.GetComponent<ChangeType>().Coin = itemDB.Coin;
            this.GetComponent<ChangeType>().Diamond = itemDB.Diamond;

        }

        public SoulerDB CreateSoulerDB()
        {
            this.GetComponent<SoulerDB>().Name = this.GetComponent<Namer>().Name;
            this.GetComponent<SoulerDB>().Id = this.GetComponent<Namer>().Id;
            this.GetComponent<SoulerDB>().UserId = this.GetComponent<Namer>().ParentId;
            this.GetComponent<SoulerDB>().Exp = this.GetComponent<ChangeType>().Exp;
            this.GetComponent<SoulerDB>().Level = this.GetComponent<ChangeType>().Level;
            return this.GetComponent<SoulerDB>();
        }


    }
}
