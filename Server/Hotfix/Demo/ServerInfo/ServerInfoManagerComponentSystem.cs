namespace ET
{
    public class ServerInfoManagerComponentAwakeSystem : AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }
    
    public class ServerInfoManagerComponentDestroySystem : DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            foreach (var serverInfo in self.ServerInfos)
            {
                serverInfo?.Dispose();
            }
            self.ServerInfos.Clear();
        }
    }

    public class ServerInfoManagerComponentLoadSystem : LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    [FriendClass(typeof(ServerInfo))]
    [FriendClass(typeof(ServerInfoManagerComponent))]
    public static class ServerInfoManagerComponentSystem
    {
        
        // 因为Awake 中会操作数据库 ，所以在这里使用 ETTask 进行等待
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            var serverInfoList =await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(d => true);

            if (serverInfoList == null || serverInfoList.Count <= 0)
            {
                Log.Error("serverInfo count is zero");
                self.ServerInfos.Clear();
                var serverInfoConfigs =ServerInfoConfigCategory.Instance.GetAll();

                foreach (var info in serverInfoConfigs.Values)
                {
                    ServerInfo newServerinfo     = self.AddChildWithId<ServerInfo>(info.Id);
                    newServerinfo.ServerName = info.ServerName;
                    newServerinfo.Status            = (int) ServerStatus.Normal;
                    self.ServerInfos.Add(newServerinfo);
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(newServerinfo);
                }
                
                return;
            }
            self.ServerInfos.Clear();

            foreach (var serverInfo in serverInfoList)
            {
                self.AddChild(serverInfo);
                self.ServerInfos.Add(serverInfo);
            }
            
            await ETTask.CompletedTask;
        }
    }
}