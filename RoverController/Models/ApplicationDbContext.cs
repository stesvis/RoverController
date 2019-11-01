using Microsoft.AspNet.Identity.EntityFramework;
using RoverController.Logger;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RoverController.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Mission> Missions { get; set; }

        public DbSet<MissionAttachment> MissionAttachments { get; set; }

        public DbSet<PinPoint> PinPoints { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            Database.CommandTimeout = 0;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            var connection = this.Database.Connection;
            base.Dispose(disposing);
            connection.Dispose();
        }
    }
}