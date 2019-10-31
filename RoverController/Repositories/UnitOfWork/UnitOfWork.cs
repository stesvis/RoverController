using RoverController.Logger;
using RoverController.Web.Models;
using RoverController.Web.Repositories.Positions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace RoverController.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private readonly ApplicationDbContext _context;
        public ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }

        #endregion Private Fields

        #region Public Properties

        public IPositionsRepository Positions { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
            Initialize();
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Initialize();
        }

        #endregion Public Constructors

        #region Public Methods

        public int SaveChanges()
        {
            bool saveFailed;
            int attempts = 0;
            do
            {
                saveFailed = false;
                try
                {
                    return _context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    List<string> messages = new List<string>();
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            messages.Add(message);
                        }
                    }
                    throw new DbEntityValidationException(string.Join(",", messages));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    AppLogger.Logger.Error(ex);

                    // Update original values from the database
                    var entry = ex.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    attempts++;
                }
            } while (saveFailed && attempts < 3);

            return 0;
        }

#if false
        public void Dispose()
        {
            _context.Dispose();
        }
#else
        // Trying to follow MSDN practices https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-unit-of-work-class

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#endif

        #endregion Public Methods

        private void Initialize()
        {
            Positions = new PositionsRepository(_context);
        }
    }
}