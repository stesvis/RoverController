using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using RoverController.Mobile.Services;
using RoverController.Mobile.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoverController.Mobile.ViewModels
{
    public class MissionDetailsPageViewModel : ViewModelBase
    {
        public MissionDetailsPageViewModel(INavigationService navigationService, IModalNavigationService modalNavigationService, IPageDialogService dialogService, IAppService appService)
            : base(navigationService, modalNavigationService, dialogService, appService)
        {
        }
    }
}