using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;

namespace RoverController.Mobile.ViewModels
{
    public class DebugPageViewModel : ViewModelBase
    {
        public DebugPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
        }
    }
}