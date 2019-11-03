using RoverController.Mobile.DTOs;
using RoverController.Mobile.Services.APIs.Missions;
using System;
using System.Threading.Tasks;

namespace RoverController.Mobile.Services.APIs
{
    public interface IApiService
    {
        IMissionsApiService Missions { get; }

        Task<Tuple<UserDTO, string>> SignIn(string username, string password);

        Task SignOut();
    }
}