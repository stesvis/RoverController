using Prism.Navigation;
using Prism.Services;
using RoverController.Lib;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RoverController.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsBusy = true;

                if (VersionTracking.IsFirstLaunchForCurrentVersion ||
                    Preferences.Get(Misc.Settings.GridMaxX, null).IsEmpty() ||
                    Preferences.Get(Misc.Settings.GridMaxY, null).IsEmpty())
                {
                    using (Helper.Loading("Redirecting to Settings"))
                    {
                        await Task.Delay(2000);
                        await NavigationService.NavigateAsync("Navigation/Settings", null, true);
                    }
                }
            }
            catch (Exception ex)
            {
                base.DisplayExceptionMessage(ex);
            }
            finally
            {
                IsBusy = false;
                base.Initialize(parameters);
            }
        }
    }
}