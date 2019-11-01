namespace RoverController.Lib
{
    public static class Api
    {
        public struct Account
        {
            public static readonly string Token = "/token";
            public static readonly string UserInfo = "/api/Account/UserInfo";
            public static readonly string LogOut = "/api/Account/LogOut";
            public static readonly string SendResetPasswordLink = "/api/Account/SendResetPasswordLink";
            public static readonly string ChangePassword = "/api/Account/ChangePassword";
        }

        public struct Missions
        {
            public const string All = "/api/Missions";
            public const string Create = "/api/Missions";
        }

        public struct PinPoints
        {
            public const string All = "/api/PinPoints";
            public const string Create = "/api/PinPoints";
        }
    }
}