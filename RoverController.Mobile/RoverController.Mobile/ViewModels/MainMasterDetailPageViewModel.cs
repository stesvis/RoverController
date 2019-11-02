using Prism.Navigation;
using Prism.Services;

namespace RoverController.Mobile.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        public MainMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}