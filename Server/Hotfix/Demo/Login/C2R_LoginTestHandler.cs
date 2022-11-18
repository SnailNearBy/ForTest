using System;
using System.Security;

namespace ET
{
    //C2R_LoginTest消息处理
    
    [MessageHandler]
    public class C2R_LoginTestHandler : AMRpcHandler<C2R_LoginTest,R2C_LoginTest>
    {
        protected override  async ETTask Run(Session session, C2R_LoginTest request, R2C_LoginTest response, Action reply)
        {
            Log.Debug(request.ToString());
            //response.Message = "6666666";
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}