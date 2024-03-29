﻿using System;
using System.Threading;
using ETModel;
using NLog;

namespace App
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			// 异步方法全部会回掉到主线程
			SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
			
			try
			{			
				Game.EventSystem.Add(DLLType.Model, typeof(Game).Assembly);
				Game.EventSystem.Add(DLLType.Hotfix, DllHelper.GetHotfixAssembly());

				Options options = Game.Scene.AddComponent<OptionComponent, string[]>(args).Options;
				StartConfig startConfig = Game.Scene.AddComponent<StartConfigComponent, string, int>(options.Config, options.AppId).StartConfig;

				if (!options.AppType.Is(startConfig.AppType))
				{
					Log.Error("命令行参数apptype与配置不一致");
					return;
				}

				IdGenerater.AppId = options.AppId;

				LogManager.Configuration.Variables["appType"] = $"{startConfig.AppType}";
				LogManager.Configuration.Variables["appId"] = $"{startConfig.AppId}";
				LogManager.Configuration.Variables["appTypeFormat"] = $"{startConfig.AppType, -8}";
				LogManager.Configuration.Variables["appIdFormat"] = $"{startConfig.AppId:0000}";

				Log.Info($"server start........................ {startConfig.AppId} {startConfig.AppType}");

				Game.Scene.AddComponent<TimerComponent>();
				Game.Scene.AddComponent<OpcodeTypeComponent>();
				Game.Scene.AddComponent<MessageDispatcherComponent>();

				// 根据不同的AppType添加不同的组件
				OuterConfig outerConfig = startConfig.GetComponent<OuterConfig>();
				InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();
				ClientConfig clientConfig = startConfig.GetComponent<ClientConfig>();
				
				switch (startConfig.AppType)
				{
					case AppType.Manager:
						Game.Scene.AddComponent<AppManagerComponent>();
						Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
						Game.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);
						break;
					case AppType.Realm:
						Game.Scene.AddComponent<MailboxDispatcherComponent>();
						Game.Scene.AddComponent<ActorMessageDispatcherComponent>();
						Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
						Game.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);
						Game.Scene.AddComponent<LocationProxyComponent>();
						Game.Scene.AddComponent<RealmGateAddressComponent>();
						break;
					case AppType.Gate:
						Game.Scene.AddComponent<PlayerComponent>();
						Game.Scene.AddComponent<MailboxDispatcherComponent>();
						Game.Scene.AddComponent<ActorMessageDispatcherComponent>();
						Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
						Game.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);
						Game.Scene.AddComponent<LocationProxyComponent>();
						Game.Scene.AddComponent<ActorMessageSenderComponent>();
						Game.Scene.AddComponent<ActorLocationSenderComponent>();
						Game.Scene.AddComponent<GateSessionKeyComponent>();
						break;
					case AppType.Location:
						Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
						Game.Scene.AddComponent<LocationComponent>();
						break;
					case AppType.Map:
						Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
						Game.Scene.AddComponent<UnitComponent>();
						Game.Scene.AddComponent<LocationProxyComponent>();
						Game.Scene.AddComponent<ActorMessageSenderComponent>();
						Game.Scene.AddComponent<ActorLocationSenderComponent>();
						Game.Scene.AddComponent<MailboxDispatcherComponent>();
						Game.Scene.AddComponent<ActorMessageDispatcherComponent>();
						Game.Scene.AddComponent<PathfindingComponent>();
						break;
					case AppType.AllServer:
						// 发送普通actor消息
						Game.Scene.AddComponent<ActorMessageSenderComponent>();
						
						// 发送location actor消息
						Game.Scene.AddComponent<ActorLocationSenderComponent>();

                        Game.Scene.AddComponent<DBComponent>();
                        Game.Scene.AddComponent<DBProxyComponent>();

                        // location server需要的组件
                        Game.Scene.AddComponent<LocationComponent>();
						
						// 访问location server的组件
						Game.Scene.AddComponent<LocationProxyComponent>();
						
						// 这两个组件是处理actor消息使用的
						Game.Scene.AddComponent<MailboxDispatcherComponent>();
						Game.Scene.AddComponent<ActorMessageDispatcherComponent>();
						
						// 内网消息组件
						Game.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
						
						// 外网消息组件
						Game.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);
						
						// manager server组件，用来管理其它进程使用
						Game.Scene.AddComponent<AppManagerComponent>();
						Game.Scene.AddComponent<RealmGateAddressComponent>();
						Game.Scene.AddComponent<GateSessionKeyComponent>();                        

                        // 配置管理
                        Game.Scene.AddComponent<ConfigComponent>();

                        #region ///20190621
                        // 调用方法
                        //Game.Scene.AddComponent<PingComponent, long, long, Action<long>>(5000, 6, OnExit);
                        // 调用匿名方法
                        Game.Scene.AddComponent<BongComponent, long, long, Action<long>>(5000, 10, sessionId => {
                            Game.Scene.GetComponent<NetOuterComponent>().Remove(sessionId);
                            Game.Scene.GetComponent<NetInnerComponent>().Remove(sessionId);
                        });

                        Game.Scene.AddComponent<NumericWatcherComponent>();     //创建数值组件NumericWatcherComponent
                        Game.Scene.AddComponent<AoiGridComponent>();            //创建 AOI 组件
                        Game.Scene.AddComponent<InventoryComponent>();          //创建 Inventory 组件
                        Game.Scene.AddComponent<SkillComponent>();              //创建 Skill 组件


                        #endregion


                        // recast寻路组件
                        Game.Scene.AddComponent<PathfindingComponent>();
						Game.Scene.AddComponent<PlayerComponent>();
						Game.Scene.AddComponent<UnitComponent>();

                        Game.Scene.AddComponent<ConsoleComponent>();
                        // Game.Scene.AddComponent<HttpComponent>();

                        Console.WriteLine(" 内网： " + innerConfig.Address + " 外网： " + outerConfig.Address);


                        #region ///20190613
                        Game.Scene.AddComponent<MonsterUnitComponent>();
                        Game.Scene.AddComponent<MonsterComponent>();
                        Game.Scene.AddComponent<UserComponent>();

                        Game.Scene.AddComponent<TestComponent>();
                        Game.Scene.AddComponent<SpawnComponent>();


                        #endregion

                        Console.WriteLine(" 服务器配置完成： " + AppType.AllServer + "  " + DateTime.Now.ToString("yyyy MM dd HH:mm:ss"));

						break;
					case AppType.Benchmark:
						Game.Scene.AddComponent<NetOuterComponent>();
						Game.Scene.AddComponent<BenchmarkComponent, string>(clientConfig.Address);
						break;
					case AppType.BenchmarkWebsocketServer:
						Game.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);
						break;
					case AppType.BenchmarkWebsocketClient:
						Game.Scene.AddComponent<NetOuterComponent>();
						Game.Scene.AddComponent<WebSocketBenchmarkComponent, string>(clientConfig.Address);
						break;
					default:
						throw new Exception($"命令行参数没有设置正确的AppType: {startConfig.AppType}");
				}
				
				while (true)
				{
					try
					{
						Thread.Sleep(1);
						OneThreadSynchronizationContext.Instance.Update();
						Game.EventSystem.Update();
					}
					catch (Exception e)
					{
						Log.Error(e);
					}
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}
	}
}
