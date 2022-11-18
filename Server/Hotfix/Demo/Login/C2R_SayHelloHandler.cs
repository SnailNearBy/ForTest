namespace ET
{
    
    [MessageHandler]
    public class C2R_SayHelloHandler : AMHandler<C2R_SayHello>
    {
        protected override void Run(Session session, C2R_SayHello message)
        {
            Log.Debug(message.ToString());
            message.Hello = "hello1";
            session.Send(new R2C_GoodBye(){GoodBye = "GoodBye Client !"});
        }
    }
}