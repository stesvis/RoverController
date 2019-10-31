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

        public struct Clients
        {
            public const string Create = "/api/Clients";
            public const string Edit = "/api/Clients/{id}";
            public const string Delete = "/api/Clients/{id}";

            public const string Filter = "/api/Clients";
            public const string AutoRenew = "/api/Clients/ToggleAutoRenew/{id}";
            public const string RenewOneMonth = "/api/Clients/Renew/{id}";
        }

        public struct ClientUsers
        {
            public const string Delete = "/api/clientusers/{id}";
        }

        public struct Home
        {
            public const string AppVersion = "/api/Home/AppVersion/{platform}";
            public const string Contact = "/api/Home/Contact";
            public const string Summary = "/api/Home/Summary?clientId={clientId}";
        }

        public struct Settings
        {
            public const string Get = "/api/Settings/{id}";
            public const string Create = "/api/Settings";
            public const string Edit = "/api/Settings/{id}";
            public const string Delete = "/api/Settings/{id}";

            public const string MySettings = "/api/Settings/MySettings";
            public const string CreateUserSettings = "/api/Settings/CreateUserSettings";
            public const string UpdateUserSettings = "/api/Settings/UpdateUserSettings/{id}";

            public const string GetClientSettings = "/api/Settings/Client/{clientId}";
        }

        public struct Tasks
        {
            public const string Provinces = "/api/Tasks/Provinces";
            public const string States = "/api/Tasks/States";
        }

        public struct Users
        {
            public const string Get = "/api/Users/{id}";
            public const string Create = "/api/Users";
            public const string Edit = "/api/Users/{id}";
            public const string Delete = "/api/Users/{id}?clientId={clientId}";

            public const string Filter = "/api/Users?clientId={clientId}";
            public const string GetByRole = "/api/Users?clientId={clientId}&role={role}";
            public const string AddDevice = "/api/Users/AddDevice";
            public const string RemoveDevice = "/api/Users/RemoveDevice";
            public const string GetLastKnownLocation = "/api/Users/{id}/LastKnownLocation";
        }
    }
}