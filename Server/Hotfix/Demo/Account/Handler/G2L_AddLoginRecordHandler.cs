using System;

namespace ET
{
    [ActorMessageHandler]
    public class G2L_AddLoginRecordHandler : AMActorRpcHandler<Scene,G2L_AddLoginRecord,L2G_AddLoginRecord>
    {
        protected override async ETTask Run(Scene scene, G2L_AddLoginRecord request, L2G_AddLoginRecord response, Action reply)
        {
            long accoutId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock,accoutId.GetHashCode()))
            {
                 scene.GetComponent<LoginInfoRecordComponent>().Remove(accoutId);
                 scene.GetComponent<LoginInfoRecordComponent>().Add(accoutId,request.ServerId);
            }

            reply();
        }
    }
}