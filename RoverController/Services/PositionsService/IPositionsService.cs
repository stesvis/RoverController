using RoverController.DTOs;
using System.Collections.Generic;

namespace RoverController.Web.Services.PositionsService
{
    public interface IPositionsService
    {
        IEnumerable<PositionDTO> All();
    }
}