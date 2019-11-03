using RoverController.Mobile.DTOs;
using System;
using System.Threading.Tasks;

namespace RoverController.Mobile.Services.APIs
{
    public interface IApiService
    {
        Task<Tuple<UserDTO, string>> SignIn(string username, string password);

        Task SignOut();
    }
}