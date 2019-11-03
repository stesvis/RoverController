using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;

namespace RoverController.Mobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
        }

        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsBusy = true;
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