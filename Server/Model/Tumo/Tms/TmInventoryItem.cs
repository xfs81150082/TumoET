using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    public class TmInventoryItem : Entity
    {
        public TmInventoryItem()
        {
            AddComponent(new TmInventoryDB());
            AddComponent(new TmInventory());
            AddComponent(new TmName());
            AddComponent(new TmChangeType());
        }

        public TmInventoryItem(TmInventoryDB itemDB)
        {
            if (this.GetComponent<TmInventoryDB>() != null)
            {
                RemoveComponent<TmInventoryDB>();
            }
            AddComponent(itemDB);
            TmInventory inventory = null;
            //TmObjects.Inventorys.TryGetValue(itemDB.InventoryId, out inventory);
            //if (inventory != null)
            //{
            //    if (this.GetComponent<TmInventory>() != null)
            //    {
            //        this.RemoveComponent<TmInventory>();
            //    }
            //    this.AddComponent(inventory);               
            //    if (GetComponent<TmCoolDown>() != null)
            //    {
            //        this.GetComponent<TmCoolDown>().CdTime = 0;
            //        this.GetComponent<TmCoolDown>().MaxCdTime = 14;
            //    }
            //}
            this.GetComponent<TmName>().Id = this.GetComponent<TmInventoryDB>().Id;
            this.GetComponent<TmName>().Name = this.GetComponent<TmInventoryDB>().Name;
            this.GetComponent<TmName>().ParentId = this.GetComponent<TmInventoryDB>().RolerId;
            this.GetComponent<TmChangeType>().Place = this.GetComponent<TmInventoryDB>().Place;
            this.GetComponent<TmChangeType>().Exp = this.GetComponent<TmInventoryDB>().Exp;
            this.GetComponent<TmChangeType>().Level = this.GetComponent<TmInventoryDB>().Level;
            this.GetComponent<TmChangeType>().Count = this.GetComponent<TmInventoryDB>().Count;
            this.GetComponent<TmChangeType>().Durability = this.GetComponent<TmInventoryDB>().Durability;
        }


    }
}
