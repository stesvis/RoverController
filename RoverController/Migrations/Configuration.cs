namespace RoverController.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RoverController.Lib;
    using RoverController.Logger;
    using RoverController.Web.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RoverController.Web.Models.ApplicationDbContext>
    {
        public UserManager<ApplicationUser> UserManager
        {
            get { return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())); }
        }

        public RoleManager<ApplicationRole> RoleManager
        {
            get { return new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext())); }
        }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RoverController.Web.Models.ApplicationDbContext context)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Launch();

            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method to avoid creating
            // duplicate seed data.

            //--------------------------------------------//
            SeedRoles(context);
            //--------------------------------------------//
            SeedUsers(context);

            var superadminId = string.Empty;
            using (var userManager = UserManager)
            {
                superadminId = userManager.FindByName("superadmin").Id;
            }
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            try
            {
                using (var roleManager = RoleManager)
                {
                    if (!roleManager.RoleExists(UserRoles.Admin))
                    {
                        roleManager.Create(new ApplicationRole(UserRoles.Admin, (int)UserRolesOrder.Admin));
                    }
                    else
                    {
                        var role = roleManager.FindByName(UserRoles.Admin);
                        role.Order = (int)UserRolesOrder.Admin;
                        context.Set<ApplicationRole>().AddOrUpdate(role);
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw new Exception("Error in SeedRoles", ex);
            }
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            try
            {
                using (var userManager = UserManager)
                {
                    string password = "Abc123!!!";
                    //----------------- Superadmin -----------------//
                    var user = userManager.FindByName("admin");

                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            UserName = "admin",
                            Email = "admin@test.com",
                            EmailConfirmed = true,
                            FirstName = "Cristian",
                            LastName = "Merli",
                            CreatedDate = DateTime.Now,
                        };

                        IdentityResult userResult = userManager.Create(user, password);

                        if (userResult.Succeeded)
                        {
                            var result = userManager.AddToRole(user.Id, UserRoles.Admin);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw new Exception("Error in SeedUsers", ex);
            }
        }
    }
}