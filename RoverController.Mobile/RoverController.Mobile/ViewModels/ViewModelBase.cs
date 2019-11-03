using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RoverController.Lib;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using System.Threading.Tasks;

namespace RoverController.Mobile.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }

        protected IModalNavigationService ModalNavigationService { get; private set; }

        protected IPageDialogService DialogService { get; }
        public IAppService AppService { get; }

        #region Commands

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand));

        private DelegateCommand<string> _navigateModalCommand;
        public DelegateCommand<string> NavigateModalCommand =>
            _navigateModalCommand ?? (_navigateModalCommand = new DelegateCommand<string>(ExecuteNavigateModalCommand));

        private DelegateCommand _popModalCommand;
        public DelegateCommand PopModalCommand =>
            _popModalCommand ?? (_popModalCommand = new DelegateCommand(ExecutePopModalCommand));

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(ExecuteGoBackCommand));

        #endregion Commands

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy))); }
        }

        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        #endregion Properties

        public ViewModelBase(
            INavigationService navigationService,
            IModalNavigationService modalNavigationService,
            IPageDialogService dialogService,
            IAppService appService)
        {
            NavigationService = navigationService;
            ModalNavigationService = modalNavigationService;
            DialogService = dialogService;
            AppService = appService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }

        #region Dialogs

        public async void DisplayExceptionMessage(Exception ex)
        {
            await DialogService.DisplayAlertAsync("Oops!", ex.GetFullMessage(), "OK");
        }

        public async void DisplayErrorMessage(string message = "We're sorry, but an error occurred.")
        {
            await DialogService.DisplayAlertAsync("Oops!", message, "OK");
        }

        public void DisplayNoConnectionMessage()
        {
            Helper.Toast("No Internet", ToastType.Warning);
        }

        #endregion Dialogs

        #region Navigate Command

        protected async void ExecuteNavigateCommand(string path)
        {
            try
            {
                await NavigationService.NavigateAsync(path, null, false);
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        protected async void ExecuteNavigateModalCommand(string path)
        {
            try
            {
                using (Helper.Loading())
                {
                    await Task.Delay(500); // just to show the spinning wheel
                    await NavigationService.NavigateAsync(path, null, true, true);
                }
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        protected async void ExecutePopModalCommand()
        {
            try
            {
                await ModalNavigationService.PopModalAsync(true);
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        protected async void ExecuteGoBackCommand()
        {
            try
            {
                await NavigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                DisplayExceptionMessage(ex);
            }
        }

        #endregion Navigate Command
    }
}