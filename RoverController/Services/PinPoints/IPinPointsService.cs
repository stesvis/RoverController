using RoverController.Web.DTOs;
using System.Collections.Generic;

namespace RoverController.Web.Services.PinPoints
{
    public interface IPinPointsService
    {
        IEnumerable<PinPointDTO> All();

        PinPointDTO Get(int id);

        PinPointDTO Create(PinPointDTO pinPointDTO, string currentUserId);
    }
}