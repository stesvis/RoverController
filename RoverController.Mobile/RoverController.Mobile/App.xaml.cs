using Prism;
using Prism.Ioc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.APIs;
using RoverController.Mobile.Services.APIs.Missions;
using RoverController.Mobile.Services.Navigation;
using RoverController.Mobile.ViewModels;
using RoverController.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace RoverController.Mobile
{
    public partial class App
    {
        public static double StatusBarHeight { get; set; }

        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY1NzMyQDMxMzcyZTMzMmUzMFBxeFR6WXhVUHlqYTZkMmtQNlJtVVlMWThzSHNvNCtNNUdEbXFoV1cyeEU9"); // Community edition license key

            InitializeComponent();

            await NavigationService.NavigateAsync("Navigation/Login");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services Api
            containerRegistry.RegisterSingleton<IAppService, AppService>();
            containerRegistry.RegisterSingleton<IApiService, ApiService>();
            containerRegistry.RegisterSingleton<IMissionsApiService, MissionsApiService>();
            containerRegistry.RegisterSingleton<IModalNavigationService, ModalNavigationService>();

            containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("Main");
            containerRegistry.RegisterForNavigation<MainMasterDetailPage, MainMasterDetailPageViewModel>("MasterDetail");
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>("Login");
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>("Settings");
            containerRegistry.RegisterForNavigation<NewMissionPage, NewMissionPageViewModel>("NewMission");
            containerRegistry.RegisterForNavigation<MissionDetailsPage, MissionDetailsPageViewModel>("MissionDetails");
        }
    }
}