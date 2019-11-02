using Acr.UserDialogs;
using System;
using Xamarin.Essentials;

namespace RoverController.Mobile.Misc
{
    public static class Helper
    {
        /// <summary>
        /// Checks if an internet connection is available
        /// </summary>
        /// <returns></returns>
        /// <see cref="https://jamesmontemagno.github.io/ConnectivityPlugin/"/>
        /// <example>https://jamesmontemagno.github.io/ConnectivityPlugin/CheckingConnectivity.html</example>
        public static bool IsInternetAvailable()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public static IProgressDialog Loading(string message = null)
        {
            //Device.OnPlatform(iOS: () => retval = UserDialogs.Instance.Loading(message), Android: () => retval = UserDialogs.Instance.Loading(message));
            var dialog = UserDialogs.Instance.Loading(message);

            return dialog;
        }

        public static IDisposable Toast(string message, ToastType type)
        {
            var toastConfig = new ToastConfig(message);
            toastConfig.SetDuration(3000);

            switch (type)
            {
                case ToastType.Success:
                    toastConfig.SetBackgroundColor(System.Drawing.Color.ForestGreen);
                    toastConfig.SetMessageTextColor(System.Drawing.Color.White);
                    break;

                case ToastType.Error:
                    toastConfig.SetBackgroundColor(System.Drawing.Color.DarkRed);
                    toastConfig.SetMessageTextColor(System.Drawing.Color.White);
                    break;

                case ToastType.Warning:
                    //toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(1, 216, 143, 16));
                    toastConfig.SetBackgroundColor(System.Drawing.Color.Orange);
                    toastConfig.SetMessageTextColor(System.Drawing.Color.White);
                    break;

                case ToastType.Info:
                    toastConfig.SetBackgroundColor(System.Drawing.Color.LightSkyBlue);
                    toastConfig.SetMessageTextColor(System.Drawing.Color.Black);
                    break;

                default:
                    break;
            }
            toastConfig.SetMessageTextColor(System.Drawing.Color.White);

            var toast = UserDialogs.Instance.Toast(toastConfig);

            return toast;
        }
    }
}