using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class MapHelper
    {
        public static async ETVoid EnterMapAsync()
        {
            try
            {
                // 加载Unit资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                await resourcesComponent.LoadBundleAsync($"unit.unity3d");

                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("map.unity3d");
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.Map);
                }

                Player player = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer;
                G2C_EnterMap g2CEnterMap = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap() { PlayerId = player.Id }) as G2C_EnterMap;
                //PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;
                player.UnitId = g2CEnterMap.UnitId;

                Debug.Log(" MapHelper-player.Id/player.UnitId: " + player.Id + " / " + player.UnitId);

                ///20190721 取消合并到Move里了
                //Game.Scene.AddComponent<OperaComponent>();
				
                Game.EventSystem.Run(EventIdType.EnterMapFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}