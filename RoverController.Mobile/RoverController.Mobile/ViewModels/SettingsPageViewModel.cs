using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Misc;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using Xamarin.Essentials;

namespace RoverController.Mobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand)
            .ObservesProperty(() => IsBusy)
            .ObservesProperty(() => MaxX)
            .ObservesProperty(() => MaxY));

        #region Properties

        private int? _maxX;
        public int? MaxX
        {
            get { return _maxX; }
            set { SetProperty(ref _maxX, value); }
        }

        private int? _maxY;
        public int? MaxY
        {
            get { return _maxY; }
            set { SetProperty(ref _maxY, value); }
        }

        #endregion Properties

        public SettingsPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
            MaxX = Preferences.Get(Settings.GridMaxX, 0);
            MaxY = Preferences.Get(Settings.GridMaxY, 0);
        }

        #region Save Command

        private async void ExecuteSaveCommand()
        {
            try
            {
                IsBusy = true;

                Preferences.Set(Settings.GridMaxX, MaxX.Value);
                Preferences.Set(Settings.GridMaxY, MaxY.Value);

                await ModalNavigationService.PopModalAsync(true);
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

        private bool CanExecuteSaveCommand()
        {
            return IsNotBusy &&
                MaxX.HasValue && MaxX.Value > 0 &&
                MaxY.HasValue && MaxY.Value > 0;
        }

        #endregion Save Command
    }
}