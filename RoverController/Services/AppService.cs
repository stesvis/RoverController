using Microsoft.AspNet.Identity;
using NLog;
using RoverController.Web.DTOs;
using RoverController.Web.Services.Base;
using RoverController.Web.Services.Missions;
using RoverController.Web.Services.PinPoints;
using RoverController.Web.Services.Users;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoverController.Web.Services
{
    public class AppService : BaseService, IAppService
    {
        #region Services

        public IMissionsService Missions { get; private set; }
        public IPinPointsService PinPoints { get; private set; }
        public IUserService Users { get; private set; }

        #endregion Services

        #region Properties

        /// <summary>
        /// Returns the current logged in ApplicationUser
        /// </summary>
        public UserDTO CurrentUser
        {
            get
            {
                var userDTO = (UserDTO)HttpContext.Current.Session["CurrentUser"];
                if (userDTO == null)
                {
                    userDTO = Users.Get(CurrentUserId);

                    HttpContext.Current.Session["CurrentUser"] = userDTO;
                }

                MappedDiagnosticsContext.Set("currentUserId", userDTO?.Id);
                return userDTO;
            }

            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
                MappedDiagnosticsContext.Set("currentUserId", value?.Id);
            }
        }

        public string CurrentUserId
        {
            get
            {
                //throw new Exception("This is a custom exception");
                MappedDiagnosticsContext.Set("currentUserId", HttpContext.Current.User.Identity.GetUserId());
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        #endregion Properties

        public AppService(
            IMissionsService positionsService,
            IPinPointsService pinPointsService,
            IUserService userService)
            : base()
        {
            Users = userService;
            Missions = positionsService;
            PinPoints = pinPointsService;
        }

        #region Dropdowns

        /// <summary>
        /// Returns the list of available user roles
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetRoles()
        {
            var dictionary = new Dictionary<string, string>();

            using (var roleManager = RoleManager)
            {
                foreach (var role in roleManager.Roles.OrderBy(r => r.Order))
                {
                    dictionary.Add(role.Name, role.Name);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Returns a keypair list users (Id, ShortName) by role
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        //public Dictionary<string, string> GetUsers(int clientId, string role)
        //{
        //    var dictionary = new Dictionary<string, string>();

        // var users = Users.Filter(clientId, true, UserIsAtLeast(CurrentUserId,
        // UserRoles.SuperAdmin), out int recordsTotal) .Where(u => u.Roles.Contains(role));

        // foreach (var user in users) { dictionary.Add(user.Id, user.FullName); }

        //    return dictionary;
        //}

        #endregion Dropdowns

        /// <summary>
        /// Returns the user Id of the Superadmin user
        /// </summary>
        /// <returns></returns>
        public string GetAdminUserId()
        {
            using (var userManager = UserManager)
            {
                var admin = userManager.FindByName("admin");

                return admin?.Id;
            }
        }
    }
}