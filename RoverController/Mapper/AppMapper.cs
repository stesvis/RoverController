using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoverController.Lib;
using RoverController.Web.DTOs;
using RoverController.Web.Models;
using RoverController.Web.ViewModels;

namespace RoverController.Web.Mapper
{
    public class AppMapper : IAppMapper
    {
        protected UserManager<ApplicationUser> UserManager
        {
            get { return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())); }
        }

        public RoleManager<ApplicationRole> RoleManager
        {
            get { return new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext())); }
        }

        #region User

        /// <summary>
        /// Maps an ApplicationUser to a UserDTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public UserDTO MapUser(ApplicationUser source)
        {
            using (var roleManager = RoleManager)
            {
                var dest = AutoMapper.Mapper.Map<UserDTO>(source);
                //dest.UserRoles = new List<RoleDTO>();

                foreach (var role in source.Roles)
                {
                    var fullRole = roleManager.FindById(role.RoleId);
                    dest.UserRoles.Add(new RoleDTO
                    {
                        Id = role.RoleId,
                        Name = fullRole.Name,
                        Order = fullRole.Order
                    });
                }

                return dest;
            }
        }

        public List<string> MapRoles(UserDTO userDTO)
        {
            using (var userManager = UserManager)
            {
                var user = userManager.FindById(userDTO.Id);
                var rolesList = new List<string>();
                using (var roleManager = RoleManager)
                {
                    foreach (var role in user.Roles)
                    {
                        var fullRole = roleManager.FindById(role.RoleId);
                        rolesList.Add(fullRole.Name);
                    }
                }
                return rolesList;
            }
        }

        public ApplicationUser MapUser(EditUserViewModel source, ref ApplicationUser dest)
        {
            if (!source.Id.IsEmpty())
            {
                dest.Id = source.Id;
            }

            dest.FirstName = source.FirstName;
            dest.LastName = source.LastName;
            dest.Email = source.Email;
            dest.PhoneNumber = source.PhoneNumber;
            dest.UserName = source.UserName;

            return dest;
        }

        public EditUserViewModel MapUser(ApplicationUser source, ref EditUserViewModel dest)
        {
            using (var roleManager = RoleManager)
            {
                if (!source.Id.IsEmpty())
                {
                    dest.Id = source.Id;
                }

                dest.Email = source.Email;
                dest.FirstName = source.FirstName;
                dest.LastName = source.LastName;
                dest.PhoneNumber = source.PhoneNumber;
                dest.UserName = source.UserName;

                dest.Roles = new List<string>();

                foreach (var role in source.Roles)
                {
                    var roleName = roleManager.FindById(role.RoleId).Name;
                    dest.Roles.Add(roleName);
                }

                dest.RoleName = dest.Roles.FirstOrDefault();

                return dest;
            }
        }

        #endregion User
    }
}