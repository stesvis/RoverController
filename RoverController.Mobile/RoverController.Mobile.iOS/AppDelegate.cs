using Foundation;
using iAd;
using ObjCRuntime;
using Plugin.FileUploader;
using Prism;
using Prism.Ioc;
using RoverController.Mobile.iOS.DependencyServices;
using System;
using UIKit;
using Xamarin.Forms;

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

#if DEBUG
            XAMLator.Server.PreviewServer.Run();
#endif

            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// Save the completion-handler we get when the app opens from the background. This method
        /// informs iOS that the app has finished all internal processing and can sleep again.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="sessionIdentifier"></param>
        /// <param name="completionHandler"></param>
        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, [BlockProxy(typeof(AdAction))] Action completionHandler)
        {
            FileUploadManager.UrlSessionCompletion = completionHandler;
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