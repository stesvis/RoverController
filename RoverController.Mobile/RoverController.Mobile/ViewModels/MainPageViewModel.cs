using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
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
        private DelegateCommand _newMissionCommand;
        public DelegateCommand NewMissionCommand =>
            _newMissionCommand ?? (_newMissionCommand = new DelegateCommand(ExecuteNewMissionCommand, CanExecuteNewMissionCommand));

        private async void ExecuteNewMissionCommand()
        {
            await CheckBasicSettings();
        }

        private bool CanExecuteNewMissionCommand()
        {
            return true;
        }

        public MainPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsBusy = true;
                await CheckBasicSettings();
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

        private async Task CheckBasicSettings()
        {
            if (Preferences.Get(Misc.Settings.GridMaxX, 0) == 0 ||
                Preferences.Get(Misc.Settings.GridMaxY, 0) == 0)
            {
                await DialogService.DisplayAlertAsync("First Setup", "Before you can continue you need to set the Grid size.", "OK");
                using (Helper.Loading("Redirecting to Settings"))
                {
                    await Task.Delay(2000);
                    await NavigationService.NavigateAsync("Navigation/Settings", null, true);
                }
            }
        }
    }
}