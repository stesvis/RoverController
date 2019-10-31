using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoverController.Web.DTOs;
using RoverController.Web.Models;

namespace RoverController.Web.Mapper.Users
{
    public class CustomRoleNameResolver : IValueResolver<ApplicationUser, UserDTO, IList<string>>
    {
        public RoleManager<ApplicationRole> RoleManager
        {
            get { return new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext())); }
        }

        IList<string> IValueResolver<ApplicationUser, UserDTO, IList<string>>.Resolve(ApplicationUser source, UserDTO destination, IList<string> destMember, ResolutionContext context)
        {
            destination.Roles = new List<string>();

            using (var roleManager = RoleManager)
            {
                foreach (var role in source.Roles)
                {
                    var roleName = roleManager.FindById(role.RoleId).Name;
                    destination.Roles.Add(roleName);
                }
            }

            return destination.Roles;
        }
    }
}