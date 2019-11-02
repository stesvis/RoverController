using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Services;

namespace RoverController.Mobile.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        public MainMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, dialogService, appService)
        {
        }
    }
}