using Xamarin.Forms;

namespace RoverController.Mobile.Misc
{
    public static class Properties
    {
        public static Thickness iOSPagePadding
        {
            get { return new Thickness(0, App.StatusBarHeight, 0, 0); }
        }
    }
}