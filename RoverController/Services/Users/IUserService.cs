using Microsoft.AspNet.Identity;
using RoverController.Web.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoverController.Web.Services.Users
{
    public interface IUserService
    {
        IEnumerable<UserDTO> All();

        UserDTO Get(string userId);

        IEnumerable<RoleDTO> GetAllRoles();

        Task<IdentityResult> RemoveAllRoles(string userId);
    }
}