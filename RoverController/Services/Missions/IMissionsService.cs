using RoverController.Web.DTOs;
using System.Collections.Generic;

namespace RoverController.Web.Services.Missions
{
    public interface IMissionsService
    {
        IEnumerable<MissionDTO> All();

        MissionDTO Get(int id);

        MissionDTO Create(MissionDTO missionDTO, string currentUserId);

        MissionAttachmentDTO Attach(int id, MissionAttachmentDTO attachmentDTO, string currentUserId);
    }
}