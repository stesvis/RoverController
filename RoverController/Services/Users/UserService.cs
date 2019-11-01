using Microsoft.AspNet.Identity;
using RoverController.Web.DTOs;
using RoverController.Web.Mapper;
using RoverController.Web.Models;
using RoverController.Web.Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoverController.Web.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        protected IAppMapper AppMapper { get; }

        public UserService(IAppMapper appMapper)
            : base()
        {
            AppMapper = appMapper;
        }

        /// <summary>
        /// Returns a list of all the users without their list of clients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDTO> All()
        {
            using (var userManager = UserManager)
            {
                var userDTOs = new List<UserDTO>();
                foreach (var user in userManager.Users)
                {
                    //var userDTO = Mapper.MapUser(user);
                    var userDTO = AutoMapper.Mapper.Map<UserDTO>(user);
                    userDTOs.Add(userDTO);
                }

                return userDTOs;
            }
        }

        /// <summary>
        /// Returns the ApplicationUser by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDTO Get(string userId)
        {
            var applicationUser = new ApplicationUser();
            using (var userManager = UserManager)
            {
                applicationUser = userManager.FindById(userId);
            }

            var userDTO = AutoMapper.Mapper.Map<UserDTO>(applicationUser);

            return userDTO;
        }

        public IEnumerable<RoleDTO> GetAllRoles()
        {
            using (var roleManager = RoleManager)
            {
                var roles = roleManager.Roles.OrderBy(r => r.Order);
                var roleDTOs = AutoMapper.Mapper.Map<IEnumerable<RoleDTO>>(roles);

                return roleDTOs;
            }
        }

        public async Task<IdentityResult> RemoveAllRoles(string userId)
        {
            using (var userManager = UserManager)
            {
                var roles = await userManager.GetRolesAsync(userId);
                var result = await userManager.RemoveFromRolesAsync(userId, roles.ToArray());

                return result;
            }
        }
    }
}