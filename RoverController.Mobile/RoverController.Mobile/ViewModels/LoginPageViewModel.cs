using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace RoverController.Mobile.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand, CanExecuteSignInCommand));

        private void ExecuteSignInCommand()
        {
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

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            AppName = AppInfo.Name;
            AppVersion = $"v{AppInfo.VersionString}.{AppInfo.BuildString}";
            Username = "admin@levitica.ca";
            Password = "Abc123!!!";
        }
    }
}