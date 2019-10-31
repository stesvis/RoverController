using Microsoft.AspNet.Identity;
using RoverController.Web.DTOs;
using RoverController.Web.Models.Helpers;
using RoverController.Web.ViewModels;
using RoverController.Web.ViewModels.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoverController.Web.Services.Users
{
    public interface IUserService
    {
        IEnumerable<UserDTO> All();

        Task<UserResult> Create(RegisterUserViewModel model, string currentUserId);

        UserDTO Get(string userId);

        IEnumerable<RoleDTO> GetAllRoles(bool includeSuperAdmin = false);

        Task<IdentityResult> RemoveAllRoles(string userId);

        //IEnumerable<UserDTO> Filter(
        //    int clientId,
        //    bool includeExternalUsers,
        //    bool includeSuperAdmin,
        //    out int recordsTotal,
        //    int? takeFrom = null,
        //    int? takeCount = null,
        //    string searchValue = null,
        //    int? orderByIndex = (int)UserColumns.UserName,
        //    string orderDirection = "asc",
        //    bool includeDeleted = false);

        Task<UserResult> Update(EditUserViewModel model, string currentUserId, ApplicationSignInManager signInManager);
    }
}