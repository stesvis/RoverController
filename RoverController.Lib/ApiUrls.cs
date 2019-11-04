namespace RoverController.Lib
{
    public static class Api
    {
        public static string ApiBaseUrl = @"https://rovercontroller.levitica.ca";

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
            public const string Get = "/api/Missions/{id}";
            public const string Create = "/api/Missions";
            public const string Upload = "/api/Missions/{id}/upload";
            public const string Move = "/api/Missions/{id}/move";
        }

        public struct PinPoints
        {
            public const string All = "/api/PinPoints";
            public const string Create = "/api/PinPoints";
        }
    }
}