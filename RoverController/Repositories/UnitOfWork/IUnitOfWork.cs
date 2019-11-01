using RoverController.Web.Repositories.MissionAttachments;
using RoverController.Web.Repositories.Missions;
using RoverController.Web.Repositories.PinPoints;
using System;

namespace RoverController.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMissionsRepository Missions { get; }
        IMissionAttachmentsRepository MissionAttachments { get; }

        IPinPointsRepository PinPoints { get; }

        int SaveChanges();
    }
}