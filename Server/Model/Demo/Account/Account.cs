using System.Runtime.Intrinsics.Arm;

namespace ET
{
    public enum AccountType
    {
        General = 0 ,  //普通账号类型
        
        BlackList = 1, //黑名单
    }
    
    public class Account : Entity , IAwake
    {
        public string AccountName; //账户名
         
        public string Password;  //账户密码
 
        public long CreateTime;  //创建时间

        public int AccountType;  //账号类型
    }
}