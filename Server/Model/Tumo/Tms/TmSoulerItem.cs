using System;
using System.Collections.Generic;

namespace ETModel
{
    public class TmSoulerItem : Entity
    {
        public void TmAwake()
        {
            AddComponent(new TmSouler());
            AddComponent(new TmName());
            AddComponent(new TmChangeType());         
        }
        public TmSoulerItem() { }                        ///构造函数 
        public TmSoulerItem(TmSoulerDB itemDB)
        {          
            TmSouler souler = null;
            //TmObjects.Soulers.TryGetValue(itemDB.SoulerId, out souler);
            //if (souler != null)
            //{
            //    if (this.GetComponent<TmSouler>() != null)
            //    {
            //        this.RemoveComponent<TmSouler>();
            //    }
            //    this.AddComponent(souler);
            //    if (GetComponent<TmCoolDown>() != null)
            //    {
            //        this.GetComponent<TmCoolDown>().CdTime = itemDB.CdTime;
            //        this.GetComponent<TmCoolDown>().MaxCdTime = souler.MaxColdTime;
            //    }
            //}
            this.GetComponent<TmName>().Id = itemDB.Id;
            this.GetComponent<TmName>().Name = itemDB.Name;
            this.GetComponent<TmName>().ParentId = itemDB.UserId;
            this.GetComponent<TmChangeType>().Exp = itemDB.Exp;
            this.GetComponent<TmChangeType>().Level = itemDB.Level;
            this.GetComponent<TmChangeType>().Coin = itemDB.Coin;
            this.GetComponent<TmChangeType>().Diamond = itemDB.Diamond;

        }
    }
}