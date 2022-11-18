using System;

namespace ET
{
    [ActorMessageHandler]
    public  class A2L_LoginAccountRequestHandler : AMActorRpcHandler<Scene,A2L_LoginAccountRequest,L2A_LoginAccountResponse>
    {
        protected override async ETTask Run(Scene scene, A2L_LoginAccountRequest request, L2A_LoginAccountResponse response, Action reply)
        {
            long accountId = request.AccountId;

            //使用协程锁
            using (await  CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock,accountId.GetHashCode()))
            {
                if (!scene.GetComponent<LoginInfoRecordComponent>().IsExist(accountId))
                {
                    reply();
                    return;
                }
                // zone 为当前区服 
                int zone = scene.GetComponent<LoginInfoRecordComponent>().Get(accountId);
              // Log.Debug(zone + "!!!!!");
                // 获取当前的 gate 网关配置信息 
                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGateExtend(zone,accountId);

                var g2LDisconnectGateUnit= (G2L_DisconnectGateUnit) await MessageHelper.CallActor(gateConfig.InstanceId, new L2G_DisconnectGateUnit() { AccountId = accountId });

                response.Error = g2LDisconnectGateUnit.Error;
                reply();
            }
     
        }
    }
}