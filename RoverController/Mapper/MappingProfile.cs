using AutoMapper;
using RoverController.Models;
using RoverController.Web.DTOs;
using RoverController.Web.Mapper.Users;
using RoverController.Web.Models;

namespace RoverController.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                //----------------------- Users
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

                //----------------------- Missions
                cfg.CreateMap<Mission, MissionDTO>().MaxDepth(1)
                    .ForMember(
                        dest => dest.CreatedByName,
                        opt => opt.MapFrom(src => src.CreatedByUser.UserName))
                    .Ignore(m => m.Input)
                    .Ignore(m => m.Output)
                    .Ignore(m => m.CreatedDateFormatted);
                cfg.CreateMap<MissionDTO, Mission>().MaxDepth(1)
                    .Ignore(m => m.Id)
                    .Ignore(m => m.CreatedByUser)
                    .Ignore(m => m.CreatedByUserId)
                    .Ignore(m => m.PinPoints);

                //----------------------- PinPoints
                cfg.CreateMap<PinPoint, PinPointDTO>().MaxDepth(1)
                    .Ignore(m => m.CreatedByName)
                    .Ignore(m => m.CreatedDateFormatted);
                cfg.CreateMap<PinPointDTO, PinPoint>().MaxDepth(1)
                    .Ignore(m => m.Id)
                    .Ignore(m => m.CreatedByUser)
                    .Ignore(m => m.CreatedByUserId)
                    .Ignore(m => m.Mission);
            });

            // Configuration validation must be done outside initialization
            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}