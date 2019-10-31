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
            SeedSuperAdmin(context);

            var superadminId = string.Empty;
            using (var userManager = UserManager)
            {
                superadminId = userManager.FindByName("superadmin").Id;
            }
            //--------------------------------------------//
            //var client = SeedClient(context, "Levitica", "Magrath", superadminId);
            //SeedClientUsers(context, client, superadminId, superadminId);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            try
            {
                using (var roleManager = RoleManager)
                {
                    if (!roleManager.RoleExists(UserRoles.SuperAdmin))
                    {
                        roleManager.Create(new ApplicationRole(UserRoles.SuperAdmin, (int)UserRolesOrder.SuperAdmin));
                    }
                    else
                    {
                        var role = roleManager.FindByName(UserRoles.SuperAdmin);
                        role.Order = (int)UserRolesOrder.SuperAdmin;
                        context.Set<ApplicationRole>().AddOrUpdate(role);
                        //context.Set<ApplicationRole>().AddOrUpdate(new ApplicationRole(UserRoles.SuperAdmin, (int)UserRolesOrder.SuperAdmin));
                    }

                    if (!roleManager.RoleExists(UserRoles.Admin))
                    {
                        roleManager.Create(new ApplicationRole(UserRoles.Admin, (int)UserRolesOrder.Admin));
                    }
                    else
                    {
                        var role = roleManager.FindByName(UserRoles.Admin);
                        role.Order = (int)UserRolesOrder.Admin;
                        context.Set<ApplicationRole>().AddOrUpdate(role);
                        //context.Set<ApplicationRole>().AddOrUpdate(new ApplicationRole(UserRoles.Admin, (int)UserRolesOrder.Admin));
                    }

                    if (!roleManager.RoleExists(UserRoles.User))
                    {
                        roleManager.Create(new ApplicationRole(UserRoles.User, (int)UserRolesOrder.User));
                    }
                    else
                    {
                        var role = roleManager.FindByName(UserRoles.User);
                        role.Order = (int)UserRolesOrder.User;
                        context.Set<ApplicationRole>().AddOrUpdate(role);
                        //context.Set<ApplicationRole>().AddOrUpdate(new ApplicationRole(UserRoles.Driver, (int)UserRolesOrder.Driver));
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw new Exception("Error in SeedRoles", ex);
            }
        }

        private void SeedSuperAdmin(ApplicationDbContext context)
        {
            try
            {
                using (var userManager = UserManager)
                {
                    string password = "Abc123!!!";
                    //----------------- Superadmin -----------------//
                    var user = userManager.FindByName("superadmin");

                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            UserName = "superadmin",
                            Email = "stesvis@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Cristian",
                            LastName = "Merli",
                            CreatedDate = DateTime.Now,
                        };

                        IdentityResult userResult = userManager.Create(user, password);

                        if (userResult.Succeeded)
                        {
                            var result = userManager.AddToRole(user.Id, UserRoles.SuperAdmin);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw new Exception("Error in SeedSuperAdmin", ex);
            }
        }
    }
}