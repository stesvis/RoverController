using RoverController.Lib;
using RoverController.Mobile.DTOs;
using RoverController.Mobile.Misc;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RoverController.Mobile.Services.APIs
{
    public class ApiService : IApiService
    {
        public async Task<Tuple<UserDTO, string>> SignIn(string username, string password)
        {
            var response = await GetAuthenticationTokenAsync(username, password);

            // Check the result token
            if (response != null && response.Item1 != null)
            {
                // Log in successful
                await SecureStorage.SetAsync(SecureStorageProperties.AccessToken, response.Item1.AccessToken);
                //await SecureStorage.SetAsync(SecureStorageProperties.AccessToken_Expires, response.Item1.Expires.Value);
                Preferences.Set(PrivateSettings.AccessTokenExpiryDate, response.Item1.Expires.Value);

                // Get the current user
                var userResponse = await GetUserInfoAsync();
                var userDTO = userResponse.Item1;

                if (userResponse != null && userDTO != null)
                {
                    Preferences.Set(PrivateSettings.IsLoggedIn, true);

                    return userResponse;
                }

                return Tuple.Create<UserDTO, string>(null, userResponse.Item2);
            }

            // If we couldn't even get the token, return a null User and the error message
            return Tuple.Create<UserDTO, string>(null, response?.Item2);
        }

        private async Task<Tuple<SignInResponseDTO, string>> GetAuthenticationTokenAsync(string username, string password)
        {
            if (Helper.IsInternetAvailable())
            {
                var request = new SignInRequestDTO
                {
                    Username = username,
                    Password = password,
                    GrantType = "password",
                };
                var result = await ApiWrapper<SignInResponseDTO>.Post(Lib.Api.Account.Token, request, false);

                return result;
            }

            return null;
        }

        private async Task<Tuple<UserDTO, string>> GetUserInfoAsync()
        {
            if (Helper.IsInternetAvailable())
            {
                var response = await ApiWrapper<UserDTO>.Get(Lib.Api.Account.UserInfo);

                if (response != null && response.Item1 != null && !response.Item1.Id.IsEmpty())
                {
                    var userDTO = response.Item1;

                    return Tuple.Create(userDTO, response.Item2);
                }

                return response;
            }

            return null;
        }
    }
}