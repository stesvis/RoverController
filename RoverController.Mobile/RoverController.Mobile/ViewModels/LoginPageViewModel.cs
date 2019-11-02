using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using System;
using Xamarin.Essentials;

namespace RoverController.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand, CanExecuteSignInCommand));

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
            return true;
        }

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

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        #endregion Properties

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, dialogService, appService)
        {
            AppName = AppInfo.Name;
            AppVersion = $"v{AppInfo.VersionString}.{AppInfo.BuildString}";
            Username = "admin";
            Password = "Abc123!!!";
        }
    }
}