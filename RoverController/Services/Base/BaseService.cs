using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using RoverController.Lib;
using RoverController.Logger;
using RoverController.Web.Models;
using System;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace RoverController.Web.Services.Base
{
    public class BaseService : IBaseService
    {
        protected UserManager<ApplicationUser> UserManager
        {
            get
            {
                var provider = new DpapiDataProtectionProvider("RoverController");
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))
                {
                    UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("ChangePassword"))
                };

                return userManager;
            }
        }

        public RoleManager<ApplicationRole> RoleManager
        {
            get { return new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext())); }
        }

        public string BaseUrl
        {
            get
            {
                // variables
                string Authority = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).TrimStart('/').TrimEnd('/');
                string ApplicationPath = HttpContext.Current.Request.ApplicationPath.TrimStart('/').TrimEnd('/');

                // add trailing slashes if necessary
                if (Authority.Length > 0)
                {
                    Authority += "/";
                }

                if (ApplicationPath.Length > 0)
                {
                    ApplicationPath += "/";
                }

                // return
                return string.Format("{0}{1}", Authority, ApplicationPath);
            }
        }

        public BaseService()
        {
        }

        public bool IsConnectedToProd()
        {
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            builder.ConnectionString = connectionString;

            var server = builder["Data Source"] as string;
            var database = builder["Initial Catalog"] as string;

            if (server == "35.182.98.83" && database == "RoverController")
            {
                return true;
            }

            return false;
        }

        public bool UserIsAtLeast(string userId, string minimumRole)
        {
            bool retval = false;

            try
            {
                using (var userManager = UserManager)
                {
                    switch (minimumRole)
                    {
                        case UserRoles.Admin:
                            retval = UserIsInRoles(userId, UserRoles.Admin);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw;
            }

            return retval;
        }

        public bool UserIsInRoles(string userId, params string[] roles)
        {
            using (var userManager = UserManager)
            {
                return roles.ToList().Any(role => userManager.IsInRole(userId, role));
            }
        }
    }
}