using RoverController.Web.Repositories.Positions;
using System;

namespace RoverController.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPositionsRepository Positions { get; }

        int SaveChanges();
    }
}