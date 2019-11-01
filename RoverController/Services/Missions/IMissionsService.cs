using RoverController.DTOs;
using System.Collections.Generic;

namespace RoverController.Web.Services.Missions
{
    public interface IMissionsService
    {
        IEnumerable<MissionDTO> All();

        MissionDTO Get(int id);
    }
}