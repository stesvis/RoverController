using System.Collections.Generic;
using AutoMapper;
using RoverController.Web.DTOs;
using RoverController.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RoverController.Web.Mapper.Users
{
    public class CustomRolesResolver : IValueResolver<ApplicationUser, UserDTO, IList<RoleDTO>>
    {
        public RoleManager<ApplicationRole> RoleManager
        {
            get { return new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext())); }
        }

        IList<RoleDTO> IValueResolver<ApplicationUser, UserDTO, IList<RoleDTO>>.Resolve(ApplicationUser source, UserDTO destination, IList<RoleDTO> destMember, ResolutionContext context)
        {
            destination.UserRoles = new List<RoleDTO>();

            using (var roleManager = RoleManager)
            {
                foreach (var role in source.Roles)
                {
                    var fullRole = roleManager.FindById(role.RoleId);
                    destination.UserRoles.Add(new RoleDTO
                    {
                        Id = fullRole.Id,
                        Name = fullRole.Name,
                        Order = fullRole.Order
                    });
                }
            }

            return destination.UserRoles;
        }
    }
}