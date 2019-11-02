using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RoverController.Lib;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using System;

namespace RoverController.Mobile.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; }
        protected IPageDialogService DialogService { get; }
        public IAppService AppService { get; }

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
            IPageDialogService dialogService,
            IAppService appService)
        {
            NavigationService = navigationService;
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
    }
}