using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class InventoryItemAwakeSystem : AwakeSystem<InventoryItem>
    {
        public override void Awake(InventoryItem self)
        {
            self.Awake();
        }
    }

    public class InventoryItem : Entity
    {
        public void Awake()
        {
            this.AddComponent<Namer>();
            this.AddComponent<ChangeType>();
            this.AddComponent<NumericComponent>();
            this.AddComponent<Inventory>();
            this.AddComponent<InventoryDB>();
        }
        public InventoryItem()  { }

        public InventoryItem(InventoryDB itemDB)
        {
            if (this.GetComponent<InventoryDB>() != null)
            {
                RemoveComponent<InventoryDB>();
            }
            AddComponent(itemDB);
            this.GetComponent<Namer>().Id = this.GetComponent<InventoryDB>().Id;
            this.GetComponent<Namer>().Name = this.GetComponent<InventoryDB>().Name;
            this.GetComponent<Namer>().ParentId = this.GetComponent<InventoryDB>().RolerId;
            this.GetComponent<ChangeType>().Place = this.GetComponent<InventoryDB>().Place;
            this.GetComponent<ChangeType>().Exp = this.GetComponent<InventoryDB>().Exp;
            this.GetComponent<ChangeType>().Level = this.GetComponent<InventoryDB>().Level;
            this.GetComponent<ChangeType>().Count = this.GetComponent<InventoryDB>().Count;
            this.GetComponent<ChangeType>().Durability = this.GetComponent<InventoryDB>().Durability;
        }
        public InventoryDB CreateInventoryDB()
        {
            this.GetComponent<InventoryDB>().Name = this.GetComponent<Namer>().Name;
            this.GetComponent<InventoryDB>().Id = this.GetComponent<Namer>().Id;
            this.GetComponent<InventoryDB>().RolerId = this.GetComponent<Namer>().ParentId;
            this.GetComponent<InventoryDB>().Exp = this.GetComponent<ChangeType>().Exp;
            this.GetComponent<InventoryDB>().Level = this.GetComponent<ChangeType>().Level;
            return this.GetComponent<InventoryDB>();
        }


    }
}
