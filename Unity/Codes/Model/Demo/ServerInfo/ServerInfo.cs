namespace ET
{

    public enum ServerStatus
    {
        Normal  = 0,
        Stop       = 1,
    }
    
    public class ServerInfo : Entity , IAwake
    {
        public int Status; // 游戏服务器状态
        public string ServerName; 
    }
}