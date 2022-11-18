namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        public const int ERR_NetWorkError                      = 200002; // 网络错误
        public const int ERR_LoginInfoIsNull                   = 200003; // 登录信息错误
        public const int ERR_AccountNameFormError   = 200004; //登陆账号格式错误
        public const int ERR_PasswordFormError           = 200005; //登陆密码格式错误
        public const int ERR_AccountInBlackListError  = 200006; //账号处于黑名单
        public const int ERR_LoginPasswordError          = 200007; //登陆密码错误
        public const int ERR_RequestRepeatedly           = 200008; //登陆重复请求
        public const int ERR_TokenError                          = 200009;//令牌Token错误
        
        public const int ERR_RoleNameIsNull                  = 200010; //游戏角色名称为空
        public const int ERR_RoleNameSame                  = 200011; //游戏角色名同名
        public const int ERR_RoleNotExist                       = 200012; //游戏角色不存在

        public const int ERR_ConnectGateKeyError       = 200013;//连接Gate的令牌错误

        public const int ERR_RequestSceneTypeError   = 200014;//请求的Scene错误

        public const int ERR_OtherAccountLogin            = 200015;//其他账号登陆

        public const int ERR_SessionPlayerError             = 200016; //玩家连接不存在

        public const int ERR_NonePlayerError                 = 200017; //玩家存在

        public const int ERR_PlayerSessionError             = 200018;//玩家连接错误

        public const int ERR_SessionStateError              = 200019;//连线状态错误

        public const int ERR_EnterGameError                  = 200020;//进入游戏错误

        public const int ERR_ReEnterGameError             = 200021;//再次进入游戏错误
        
        public const int ERR_ReEnterGameError2            = 200022;//再次进入游戏错误2
    }
}