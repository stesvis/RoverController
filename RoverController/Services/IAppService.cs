using RoverController.Web.DTOs;
using RoverController.Web.Services.Base;
using RoverController.Web.Services.PositionsService;
using RoverController.Web.Services.Users;
using System.Collections.Generic;

namespace RoverController.Web.Services
{
    public interface IAppService : IBaseService
    {
        #region Services

        IPositionsService Positions { get; }

        IUserService Users { get; }

        #endregion Services

        UserDTO CurrentUser { get; set; }
        string CurrentUserId { get; }

        string GetSuperadminUserId();

        #region Dropdowns

        Dictionary<string, string> GetRoles();

        //Dictionary<string, string> GetUsers(int clientId, string role);

        #endregion Dropdowns

        //string FormatProductsListForHtml(WorkOrderDTO workOrderDTO);

        //List<string> GetFormattedProductsList(WorkOrderDTO workOrderDTO);

        //string GetWorkOrderRowClass(WorkOrderDTO workOrderDTO);
    }
}