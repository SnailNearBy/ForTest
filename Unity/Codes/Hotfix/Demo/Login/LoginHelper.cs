using System;
using System.Security.Cryptography;

namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    [FriendClass(typeof(RoleInfosComponent))]
    [FriendClass(typeof(ServerInfosComponent))]
    [FriendClass(typeof(AccountInfoComponent))]
    public static class LoginHelper
    {
        // public static async ETTask Login(Scene zoneScene, string address, string account, string password)
        // {
        //     try
        //     {
        //         // 创建一个ETModel层的Session
        //         R2C_Login r2CLogin;
        //         Session session = null;
        //         try
        //         {
        //             session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
        //             {
        //                 r2CLogin = (R2C_Login) await session.Call(new C2R_Login() { Account = account, Password = password });
        //             }
        //         }
        //         finally
        //         {
        //             session?.Dispose();
        //         }
        //
        //         // 创建一个gate Session,并且保存到SessionComponent中
        //         Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLogin.Address));
        //         gateSession.AddComponent<PingComponent>();
        //         zoneScene.AddComponent<SessionComponent>().Session = gateSession;
				    //
        //         G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
        //             new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});
        //
        //         Log.Debug("登陆gate成功!");
        //
        //         Game.EventSystem.PublishAsync(new EventType.LoginFinish() {ZoneScene = zoneScene}).Coroutine();
        //     }
        //     catch (Exception e)
        //     {
        //         Log.Error(e);
        //     }
        // } 

        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2CLoginAccount = null;

            Session accountSession = null;

            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address)); 
                password = MD5Helper.StringMD5(password);
                a2CLoginAccount  = (A2C_LoginAccount)await accountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
            }
            catch (Exception e)
            {
               accountSession?.Dispose();  //抛出异常断开连接
               Log.Error(e.ToString());
               return ErrorCode.ERR_NetWorkError;
            }

            if (a2CLoginAccount.Error != ErrorCode.ERR_Success)
            {
                accountSession?.Dispose();
                return a2CLoginAccount.Error;
            }

            zoneScene.AddComponent<SessionComponent>().Session = accountSession;
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();
            
            zoneScene.GetComponent<AccountInfoComponent>().Token = a2CLoginAccount.Token;
            zoneScene.GetComponent<AccountInfoComponent>().AccountId = a2CLoginAccount.AccountId;

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> EnterGame(Scene zoneScene)
        {
            string realmAdress = zoneScene.GetComponent<AccountInfoComponent>().RealmAddress;
            
            Log.Error(realmAdress + "!!!!");
            // 1. 连接Realm ，获取分配的 Gate
            R2C_LoginRealm r2CLoginRealm;

            Session session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(realmAdress));
            try
            {

                r2CLoginRealm = (R2C_LoginRealm) await session.Call(new C2R_LoginRealm()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    RealmTokenKey = zoneScene.GetComponent<AccountInfoComponent>().RealmKey,
                });
            }
            catch (Exception e)
            {
              Log.Error(e.ToString());
              session?.Dispose();
              return ErrorCode.ERR_NetWorkError;
            }
            
            session?.Dispose();

            if (r2CLoginRealm.Error != ErrorCode.ERR_Success)
            {
                return r2CLoginRealm.Error;
            }
            
            Log.Warning($"GateAddress：{r2CLoginRealm.GateAddress}");
            Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLoginRealm.GateAddress));
            gateSession.AddComponent<PingComponent>();
            zoneScene.GetComponent<SessionComponent>().Session = gateSession;
            
            // 2. 开始连接Gate
            long currentRoleId = zoneScene.GetComponent<RoleInfosComponent>().CurrentRoleId;
            G2C_LoginGameGate g2CLoginGameGate = null;

            try
            {
                long accountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId;
               g2CLoginGameGate = (G2C_LoginGameGate) await  gateSession.Call(new C2G_LoginGameGate()
                {
                    Key = r2CLoginRealm.GateSessionKey,
                    Account = accountId,
                    RoleId = currentRoleId,
                });
              
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }

            if (g2CLoginGameGate.Error != ErrorCode.ERR_Success)
            {
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return g2CLoginGameGate.Error;
            }
            Log.Debug("登陆gate成功！");
            
            // 3. 角色正式请求进入游戏逻辑服
            G2C_EnterGame g2CEnterGame = null;
            try
            {
                g2CEnterGame =  (G2C_EnterGame) await gateSession.Call(new C2G_EnterGame() { });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }

            if (g2CEnterGame.Error != ErrorCode.ERR_Success)
            {
                Log.Error(g2CEnterGame.Error.ToString());
                return g2CEnterGame.Error;
            }
            Log.Error("角色进入游戏成功 ！");
            
            return ErrorCode.ERR_Success;
        }
        public static async ETTask<int> GetRelamKey(Scene zoneScene)
        {
            A2C_GetRealmKey a2CGetRealmKey = null; 

            try
            {
               a2CGetRealmKey = (A2C_GetRealmKey) await zoneScene.DomainScene().GetComponent<SessionComponent>().Session.Call(new C2A_GetRealmKey()
                {
                    Token = zoneScene.DomainScene().GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.DomainScene().GetComponent<AccountInfoComponent>().AccountId,
                    ServerId = zoneScene.DomainScene().GetComponent<ServerInfosComponent>().CurrentServerId,
                });
            }
            catch (Exception e)
            {
               Log.Error(e.ToString());
               return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetRealmKey.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CGetRealmKey.Error.ToString());
                return a2CGetRealmKey.Error;
            }

            zoneScene.GetComponent<AccountInfoComponent>().RealmKey = a2CGetRealmKey.RealmKey;
            zoneScene.GetComponent<AccountInfoComponent>().RealmAddress = a2CGetRealmKey.RealmAddress;
            zoneScene.GetComponent<SessionComponent>().Session.Dispose();
            return ErrorCode.ERR_Success;   
        }
        public static async ETTask<int> GetServerInfo(Scene zoneScene)
        {
            A2C_GetServerInfos a2CGetServerInfos = null;
            try
            {
               a2CGetServerInfos = (A2C_GetServerInfos) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfos()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token =  zoneScene.GetComponent<AccountInfoComponent>().Token,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetServerInfos.Error != ErrorCode.ERR_Success)
            {
                return a2CGetServerInfos.Error;
            }

            foreach (var serverInfoProto in a2CGetServerInfos.ServerInfosList)
            {
                ServerInfo serverInfo = zoneScene.GetComponent<ServerInfosComponent>().AddChild<ServerInfo>();
                serverInfo.FromMessage(serverInfoProto);
                zoneScene.GetComponent<ServerInfosComponent>().Add(serverInfo);
            }
            
            
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRole(Scene zoneScene, string name)
        {
            A2C_CreateRole a2CCreateRole = null;

            try
            { 
                a2CCreateRole = (A2C_CreateRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRole()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    Name = name,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId,
                });
            }
            catch (Exception e)   
            {
               Log.Error(e.ToString());
               return ErrorCode.ERR_NetWorkError;
            }

            if (a2CCreateRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CCreateRole.Error.ToString());
                return a2CCreateRole.Error;
            }

            RoleInfo newRoleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
            newRoleInfo.FromMessage(a2CCreateRole.RoleInfo);
            
            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Add(newRoleInfo);
         
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoles(Scene zoneScene)
        {
            A2C_GetRoles a2CGetRoles = null;

            try
            {
              a2CGetRoles = (A2C_GetRoles) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRoles()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Debug(a2CGetRoles.Error.ToString());
                return a2CGetRoles.Error;
            }

            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Clear();
            foreach (var roleInfoProto in a2CGetRoles.RoleInfo)
            {
                RoleInfo roleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
                roleInfo.FromMessage(roleInfoProto);
                if (roleInfo.State != (int)RoleInfoState.Freeze)
                {
                    zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Add(roleInfo);
                }
            }
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> DeleteRole(Scene zoneScene)
        {
            A2C_DeleteRole a2CDeleteRole = null;

            try
            {
                 a2CDeleteRole = (A2C_DeleteRole) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_DeleteRole()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    RoleInfoId = zoneScene.GetComponent<RoleInfosComponent>().CurrentRoleId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurrentServerId,
                });
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2CDeleteRole.Error.ToString());
                return a2CDeleteRole.Error;
            }

            int index = zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.FindIndex((info) =>
            {
                return info.Id == a2CDeleteRole.DeletedRoleInfoId;
            });
            
            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.RemoveAt(index);
            
            return ErrorCode.ERR_Success;
        }
        
        
        public static async ETTask LoginTest(Scene zoneScene,string address)
        {
            try
            {
                Session session = null;
                R2C_LoginTest r2CLoginTest = null;
                try
                {
                    session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                    {
                        r2CLoginTest= (R2C_LoginTest)await session.Call(new C2R_LoginTest() { Account = "", Password = "" });
                       Log.Warning(r2CLoginTest.ToString());
                       
                       session.Send(new C2R_SayHello() {Hello = "Hello Server !"});
                    }
                }
                finally
                {
                    session?.Dispose();
                }
            }
            catch (Exception e)
            {
               Log.Error(e.ToString());
            }
        }
    }
}