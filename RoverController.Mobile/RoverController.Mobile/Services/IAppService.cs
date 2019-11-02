using System;
using System.Collections.Generic;
using System.Text;
using RoverController.Mobile.Services.APIs;

namespace RoverController.Mobile.Services
{
    public interface IAppService
    {
        IApiService Api { get; }
    }
}