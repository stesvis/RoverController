using Foundation;
using Prism;
using Prism.Ioc;
using RoverController.Mobile.iOS.DependencyServices;
using System;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace RoverController.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            App.StatusBarHeight = (int)UIApplication.SharedApplication.StatusBarFrame.Height;

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            // Syncfusion components
            Syncfusion.SfNumericUpDown.XForms.iOS.SfNumericUpDownRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication app)
        {
            try
            {
                #region StatusBar

                if (double.TryParse(DeviceInfo.VersionString, out double version))
                {
                    if (version >= 13)
                    {
                        UIView statusBar = new UIView(UIApplication.SharedApplication.Delegate.GetWindow().WindowScene.StatusBarManager.StatusBarFrame)
                        {
                            BackgroundColor = ((Color)Xamarin.Forms.Application.Current.Resources["primaryColorDark"]).ToUIColor(),
                            TintColor = UIColor.White
                        };
                        UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                    }
                    else
                    {
                        UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                        if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                        {
                            statusBar.BackgroundColor = ((Color)Xamarin.Forms.Application.Current.Resources["primaryColorDark"]).ToUIColor();
                            statusBar.TintColor = UIColor.White;
                        }
                    }
                }
                //app.StatusBarStyle = UIStatusBarStyle.BlackOpaque;

                #endregion StatusBar
            }
            catch (Exception ex)
            {
                // Do nothing
            }
            finally
            {
                base.OnActivated(app);
            }
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            DependencyService.Register<ScreenshotService>();
        }
    }
}