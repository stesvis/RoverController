using System.Collections.Generic;
using RoverController.Web.DTOs;
using RoverController.Web.Models;
using RoverController.Web.ViewModels;

namespace RoverController.Web.Mapper
{
    public interface IAppMapper
    {
        UserDTO MapUser(ApplicationUser source);

        ApplicationUser MapUser(EditUserViewModel source, ref ApplicationUser dest);

        EditUserViewModel MapUser(ApplicationUser source, ref EditUserViewModel dest);

        List<string> MapRoles(UserDTO userDTO);
    }
}