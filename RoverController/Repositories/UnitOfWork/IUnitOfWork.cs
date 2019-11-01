using RoverController.Web.Repositories.Missions;
using RoverController.Web.Repositories.PinPoints;
using System;

namespace RoverController.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMissionsRepository Missions { get; }

        IPinPointsRepository PinPoints { get; }

        int SaveChanges();
    }
}