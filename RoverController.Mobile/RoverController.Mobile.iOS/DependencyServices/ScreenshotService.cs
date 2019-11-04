using Foundation;
using RoverController.Mobile.iOS.DependencyServices;
using RoverController.Mobile.Services.DependencyServices;
using System;
using System.Runtime.InteropServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenshotService))]

namespace RoverController.Mobile.iOS.DependencyServices
{
    public class ScreenshotService : IScreenshotService
    {
        public byte[] Capture()
        {
            var capture = UIScreen.MainScreen.Capture();
            using (NSData data = capture.AsPNG())
            {
                var bytes = new byte[data.Length];
                Marshal.Copy(data.Bytes, bytes, 0, Convert.ToInt32(data.Length));

                return bytes;
            }
        }
    }
}