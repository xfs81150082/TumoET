using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class SeekComponentHelper
    {
        #region 行为树模式
        /// 检查有无追击目标
        public static void CheckSeekTarget(this SeekComponent self)
        {
            if (self.target == null)
            {
                if (self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastDistance < self.enterSeeDis)
                {
                    self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
                }
            }
            else
            {
                self.targetDistance = SqrDistanceComponentHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);

                if (self.targetDistance > self.enterSeeDis)
                {
                    if (self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastDistance < self.enterSeeDis)
                    {
                        self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
                    }

                    self.target = null;
                }
            }
        }

        /// 追击目标
        public static void SeekTarget(this SeekComponent self)
        {
            ///如果卡住在地图到达不了目标点 此计时40秒后 重置巡逻目标点
            if (self.delTime == 0)
            {
                //如果追到目标后，离目标距离小于2米，不追击
                if (self.targetDistance < 4f)
                {
                    return;
                }

                // 每间隔 resTime（100） MS 发送一次目标点坐标 消息
                self.SendSeePosition();

                self.seeTimer = TimeHelper.ClientNow();
            }

            ///精确到毫秒
            self.delTime = TimeHelper.ClientNow() - self.seeTimer + 1;

            if (self.delTime > self.resTime)
            {
                self.delTime = 0;
            }
        }

        /// <summary>
        /// 发送追击目标坐标 消息
        /// </summary>
        /// <param name="self"></param>
        public static void SendSeePosition(this SeekComponent self)
        {                
            // 发送一次目标点坐标 消息
            self.seeMap = self.GetSeeMap();
            if (self.seeMap != null)
            {
                ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get((self.Parent as Unit).Id);
                actorLocationSender.Send(self.seeMap);
            }
        }

        /// 追击敌人
        static See_Map GetSeeMap (this SeekComponent self)
        {
            if(self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit == null)
            {
                return null;
            }
            self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
            self.seePoint = self.target.Position;
            See_Map see_Map = new See_Map() { Id = self.GetParent<Unit>().Id, X = self.seePoint.x, Y = self.seePoint.y, Z = self.seePoint.z };
            return see_Map;
        }

        #endregion

        //public static void UpdateSee(this SeekComponent self)
        //{
        //    if (self.GetParent<Unit>().GetComponent<PatrolComponent>().isPatrol)
        //    {
        //        self.delTime = 0;
        //        self.target = null;
        //        self.targetDistance = float.PositiveInfinity;
        //        return;
        //    }

        //    if (self.target == null)
        //    {
        //        if (self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastDistance < self.enterSeeDis)
        //        {
        //            self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
        //        }
        //    }
        //    else
        //    {
        //        self.targetDistance = SqrDistanceComponentHelper.Distance(self.GetParent<Unit>().Position, self.target.Position);

        //        if (self.targetDistance > self.enterSeeDis)
        //        {
        //            self.target = null;
        //        }

        //        ///如果卡住在地图到达不了目标点 此计时40秒后 重置巡逻目标点

        //        if (self.delTime == 0)
        //        {
        //            //如果追到目标后，离目标距离小于2米，不追击
        //            if (self.targetDistance < 4f) return;

        //            // 每间隔 resTime（100） MS 发送一次目标点坐标 消息
        //            self.SendSeePosition();

        //            self.seeTimer = TimeHelper.ClientNow();
        //        }

        //        ///精确到毫秒
        //        self.delTime = TimeHelper.ClientNow() - self.seeTimer + 1;

        //        if (self.delTime > self.resTime)
        //        {
        //            self.delTime = 0;
        //        }

        //    }
        //}

        
    }
}
