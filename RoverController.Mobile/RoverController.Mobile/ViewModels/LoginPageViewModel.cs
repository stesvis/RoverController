using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;

namespace RoverController.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand, CanExecuteSignInCommand)
            .ObservesProperty(() => IsBusy));

        #endregion Commands

        #region Properties

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        #endregion Properties

        public LoginPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
            Username = "admin";
            Password = "Abc123!!!";
        }

        #region SignIn Command

        private async void ExecuteSignInCommand()
        {
            try
            {
                IsBusy = true;

                var username = Username.Trim();
                var password = Password.Trim();

                using (Helper.Loading("Signing in"))
                {
                    var userResponse = await AppService.Api.SignIn(username, password);
                    if (userResponse == null)
                    {
                        base.DisplayNoConnectionMessage();
                        return;
                    }

                    if (userResponse?.Item2 != null)
                    {
                        base.DisplayErrorMessage(userResponse.Item2);
                    }

                    if (userResponse?.Item1 == null)
                    {
                        base.DisplayErrorMessage("You could not be authenticated.");
                        return;
                    }

                    // SUCCESS!
                    await NavigationService.NavigateAsync("MasterDetail/Navigation/Main");
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

        private bool CanExecuteSignInCommand()
        {
            return IsNotBusy;
        }

        #endregion SignIn Command
    }
}