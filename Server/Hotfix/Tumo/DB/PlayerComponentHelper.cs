using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public static class PlayerComponentHelper
    {
        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        public static async ETTask<List<Player>> GetPlayerByIds(this PlayerComponent self, List<long> ids)
        {
            DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

            List<ComponentWithId> playerdbs = await dBProxy.Query<Playerdb>(ids);

            foreach (Playerdb tem in playerdbs)
            {
                Player player = tem.ToPlayer();

                /// 然后向 DB 服务器 小怪数据放入字典                
                self.Add(player);
            }
            Console.WriteLine(" PlayerComponentHelper-28: " + " BD服务器，Player数量： " + Game.Scene.GetComponent<PlayerComponent>().Count);

            return self.GetAll().ToList();
        }


        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        //public static async ETVoid SavePlayer(this PlayerComponent self, List<long> ids)
        //{
        //    DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

        //    List<ComponentWithId> playerdbs = await dBProxy.Query<Playerdb>(ids);

        //    foreach (Playerdb tem in playerdbs)
        //    {
        //        Player player = tem.ToPlayer();

        //        /// 然后向 DB 服务器 小怪数据放入字典                
        //        self.Add(player);
        //    }

        //    Console.WriteLine(" PlayerComponentHelper-28: " + " BD服务器，Player数量： " + Game.Scene.GetComponent<PlayerComponent>().Count);
        //}

    }

    public static class PlayerHelper
    {
        public static Player ToPlayer(this Playerdb self)
        {
            Player player = ComponentFactory.CreateWithId<Player>(self.Id);
            player.UserId = self.userid;
            player.createdate = self.createdate;
            player.exp = self.exp;
            player.level = self.level;
            player.hp = self.hp;
            player.spawnPosition = new Vector3((float)self.spawnVec[0], (float)self.spawnVec[1], (float)self.spawnVec[2]);
            return player;
        }

        public static Playerdb ToPlayerdb(this Player self)
        {
            Playerdb playerdb = ComponentFactory.CreateWithId<Playerdb>(self.Id);
            playerdb.userid = self.UserId;
            playerdb.exp = self.exp;
            playerdb.level = self.level;
            playerdb.hp = self.hp;
            playerdb.spawnVec.Add(self.spawnPosition.x);
            playerdb.spawnVec.Add(self.spawnPosition.y);
            playerdb.spawnVec.Add(self.spawnPosition.z);
            playerdb.createdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            return playerdb;
        }

    }
}
