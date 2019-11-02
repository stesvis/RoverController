using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoverController.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, dialogService, appService)
        {
        }
    }
}