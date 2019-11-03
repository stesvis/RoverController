using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;

namespace RoverController.Mobile.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        private DelegateCommand _signOutCommand;
        public DelegateCommand SignOutCommand =>
            _signOutCommand ?? (_signOutCommand = new DelegateCommand(ExecuteSignOutCommand, CanExecuteSignOutCommand));

        public MainMasterDetailPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
        }

        #region LogOut Command

        private async void ExecuteSignOutCommand()
        {
            try
            {
                IsBusy = true;

                using (Helper.Loading("Signing out"))
                {
                    await AppService.Api.SignOut();
                    //await NavigationService.GoBackToRootAsync();
                    await NavigationService.NavigateAsync("/Navigation/Login", null, true, true);
                }
            }
            catch (Exception ex)
            {
                base.DisplayExceptionMessage(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecuteSignOutCommand()
        {
            return true;
        }

        #endregion LogOut Command
    }
}