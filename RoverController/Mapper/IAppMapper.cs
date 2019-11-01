using RoverController.Web.DTOs;
using RoverController.Web.Models;
using System.Collections.Generic;

namespace RoverController.Web.Mapper
{
    public interface IAppMapper
    {
        UserDTO MapUser(ApplicationUser source);

        List<string> MapRoles(UserDTO userDTO);
    }
}