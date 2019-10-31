using AutoMapper;
using RoverController.Lib;
using RoverController.Web.DTOs;
using RoverController.Web.Mapper.Users;
using RoverController.Web.Models;
using RoverController.Web.ViewModels;

namespace RoverController.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDTO>().MaxDepth(1)
                    .ForMember(
                        dest => dest.Roles,
                        opt => opt.MapFrom<CustomRoleNameResolver>())
                    .ForMember(
                        dest => dest.UserRoles,
                        opt => opt.MapFrom<CustomRolesResolver>())
                    .Ignore(m => m.Password)
                    .Ignore(m => m.DeletedDate);
                cfg.CreateMap<ApplicationRole, RoleDTO>()
                    .Ignore(m => m.IsSelected)
                    .MaxDepth(1);
                cfg.CreateMap<UserViewModel, ApplicationUser>()
                    .Ignore(m => m.CreatedDate)
                    .Ignore(m => m.EmailConfirmed)
                    .Ignore(m => m.PasswordHash)
                    .Ignore(m => m.SecurityStamp)
                    .Ignore(m => m.PhoneNumberConfirmed)
                    .Ignore(m => m.TwoFactorEnabled)
                    .Ignore(m => m.LockoutEndDateUtc)
                    .Ignore(m => m.LockoutEnabled)
                    .Ignore(m => m.AccessFailedCount)
                    .Ignore(m => m.Claims)
                    .Ignore(m => m.Logins)
                    .MaxDepth(1);
                cfg.CreateMap<UserViewModel, UserDTO>()
                    .Ignore(m => m.CreatedDate)
                    .Ignore(m => m.DeletedDate)
                    .MaxDepth(1);

                cfg.CreateMap<ApplicationUser, EditUserViewModel>().MaxDepth(1)
                    .ForMember(
                        dest => dest.Id,
                        opt => opt.Condition(src => src.Id.IsEmpty() == false))
                    .Ignore(m => m.OldPassword)
                    .Ignore(m => m.Password)
                    .Ignore(m => m.ConfirmPassword)
                    .Ignore(m => m.RoleId)
                    .Ignore(m => m.ClientGuid)
                    .Ignore(m => m.RoleName)
                    .Ignore(m => m.PrimaryClientId)
                    .Ignore(m => m.ClientId)
                    .Ignore(m => m.RoleCheckboxes)
                    .Ignore(m => m.UserRoles)
                    .Ignore(m => m.Errors)
                    .Ignore(m => m.UserHighestRole);

                cfg.CreateMap<UserDTO, EditUserViewModel>().MaxDepth(1)
                    .Ignore(m => m.OldPassword)
                    .Ignore(m => m.ConfirmPassword)
                    .Ignore(m => m.RoleId)
                    .Ignore(m => m.ClientGuid)
                    .Ignore(m => m.ClientId)
                    .Ignore(m => m.RoleCheckboxes)
                    .Ignore(m => m.Errors)
                    .Ignore(m => m.UserHighestRole);
            });

            // Configuration validation must be done outside initialization
            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}