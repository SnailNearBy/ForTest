﻿using System;

namespace ET
{
    public static class SceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene zoneScene, string sceneName, long sceneInstanceId)
        {
            zoneScene.RemoveComponent<AIComponent>();
            
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = SceneFactory.CreateCurrentScene(sceneInstanceId, zoneScene.Zone, sceneName, currentScenesComponent);
            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();
         
            // 可以订阅这个事件中创建Loading界面
            Game.EventSystem.Publish(new EventType.SceneChangeStart() {ZoneScene = zoneScene});

            // 等待CreateMyUnit的消息
            WaitType.Wait_CreateMyUnit waitCreateMyUnit = await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_CreateMyUnit>();
            M2C_CreateMyUnit m2CCreateMyUnit = waitCreateMyUnit.Message;
            Unit unit = UnitFactory.Create(currentScene, m2CCreateMyUnit.Unit);
            unitComponent.Add(unit);
            
            zoneScene.RemoveComponent<AIComponent>();
            
            Game.EventSystem.PublishAsync(new EventType.SceneChangeFinish() {ZoneScene = zoneScene, CurrentScene = currentScene}).Coroutine();

            //以下为示例代码
            // try
            // {
            //     Session session = zoneScene.GetComponent<SessionComponent>().Session;
            //        var  m2CTestActorLocationResponse =(M2C_TestActorLocationResponse)await session.Call(new C2M_TestActorLocationRequest() { Content = "11111111" });
            //        Log.Warning(m2CTestActorLocationResponse.Content);
            //        
            //        session.Send(new C2M_TestActorLocationMessage(){Content = "222222222"});
            // }
            // catch (Exception e)
            // {
            //     Log.Error(e.ToString());
            // }
            
            
            //end
            
            // 通知等待场景切换的协程
            zoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_SceneChangeFinish());
        }
    }
}