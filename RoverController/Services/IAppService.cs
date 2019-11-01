using RoverController.Web.DTOs;
using RoverController.Web.Services.Base;
using RoverController.Web.Services.Missions;
using RoverController.Web.Services.PinPoints;
using RoverController.Web.Services.Users;
using System.Collections.Generic;

namespace RoverController.Web.Services
{
    public interface IAppService : IBaseService
    {
        #region Services

        IMissionsService Missions { get; }
        IPinPointsService PinPoints { get; }

        IUserService Users { get; }

        #endregion Services

        UserDTO CurrentUser { get; set; }
        string CurrentUserId { get; }

        string GetAdminUserId();

        #region Dropdowns

        Dictionary<string, string> GetRoles();

        //Dictionary<string, string> GetUsers(int clientId, string role);

        #endregion Dropdowns
    }
}