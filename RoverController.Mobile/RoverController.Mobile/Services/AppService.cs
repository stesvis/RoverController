using System;
using System.Collections.Generic;
using System.Text;
using Prism.Services;
using RoverController.Mobile.Services.APIs;

namespace RoverController.Mobile.Services
{
    public class AppService : IAppService
    {
        public IApiService Api { get; private set; }

        public IPageDialogService DialogService { get; }

        public AppService(
            IApiService apiService,
            IPageDialogService dialogService)
        {
            Api = apiService;
            DialogService = dialogService;
        }
    }
}