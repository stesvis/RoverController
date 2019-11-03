using RoverController.Mobile.DTOs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RoverController.Mobile.Misc
{
    public static class Properties
    {
        /// <summary>
        /// Current user
        /// </summary>
        public static UserDTO CurrentUser { get; set; }

        public static string CurrentUsername { get { return CurrentUser?.UserName; } }

        public static string AppName
        {
            get { return AppInfo.Name; }
        }

        public static string AppVersion
        {
            get { return $"v{AppInfo.VersionString}.{AppInfo.BuildString}"; }
        }

        public static Thickness iOSPagePadding
        {
            get { return new Thickness(0, App.StatusBarHeight, 0, 0); }
        }
    }
}