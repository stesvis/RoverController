namespace RoverController.Mobile.Misc
{
    public enum ToastType
    {
        Success,
        Error,
        Warning,
        Info
    }

    public static class Settings
    {
        public const string GridMaxX = "GridMaxX";
        public const string GridMaxY = "GridMaxY";
    }

    public static class PrivateSettings
    {
        public const string IsLoggedIn = "is_logged_in";
        public const string AccessTokenExpiryDate = "access_token_expiry_date";
        public const string CurrentUserJSON = "CurrentUserJSON";
    }

    public static class SecureStorageProperties
    {
        public const string AccessToken = "access_token";
        public const string AccessToken_Expires = "access_token_expires";
        public const string Username = "username";
        public const string Password = "password";
    }
}