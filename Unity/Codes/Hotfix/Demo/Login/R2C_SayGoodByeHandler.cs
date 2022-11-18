namespace ET
{
    [MessageHandler]
    public class R2C_SayGoodByeHandler : AMHandler<R2C_GoodBye>
    {
        protected override void Run(Session session, R2C_GoodBye message)
        {
            Log.Debug(message.ToString());
        }
    }
}