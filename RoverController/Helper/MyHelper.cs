using System;
using System.Net.Http;
using System.Web;
using RoverController.Lib;
using RoverController.Logger;

namespace RoverController.Web
{
    public class MyHelper
    {
        private static HttpClient _myHttpClient;
        public static HttpClient MyHttpClient
        {
            get
            {
                if (_myHttpClient == null)
                {
                    _myHttpClient = new HttpClient();
                }

                return _myHttpClient;
            }
        }

        #region Public Methods

        public static string GetFullExceptionMessage(Exception ex)
        {
            string exMessage = ex.Message;

            if (ex.TargetSite != null)
            {
                exMessage = "\"" + exMessage + "\" in " + ex.TargetSite.ReflectedType.Name + "." + ex.TargetSite.Name + "()";
            }

            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    exMessage = exMessage + " ---> " + ex.InnerException.Message;
                }

                if (ex.InnerException.InnerException != null)
                {
                    if (!string.IsNullOrEmpty(ex.InnerException.InnerException.Message))
                    {
                        exMessage = exMessage + " --->" + ex.InnerException.InnerException.Message;
                    }
                }
            }

            return exMessage;
        }

        public static bool UserIsAtLeast(string minimumRole)
        {
            bool retval = false;

            try
            {
                var currentUser = HttpContext.Current.User;

                switch (minimumRole)
                {
                    case UserRoles.SuperAdmin:
                        if (currentUser.IsInRole(UserRoles.SuperAdmin))
                        {
                            retval = true;
                        }

                        break;

                    case UserRoles.Admin:
                        if (
                            currentUser.IsInRole(UserRoles.SuperAdmin) ||
                            currentUser.IsInRole(UserRoles.Admin))
                        {
                            retval = true;
                        }

                        break;

                    case UserRoles.User:
                        if (
                            currentUser.IsInRole(UserRoles.SuperAdmin) ||
                            currentUser.IsInRole(UserRoles.Admin) ||
                            currentUser.IsInRole(UserRoles.User))
                        {
                            retval = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                throw;
            }

            return retval;
        }

        public static string Truncate(string myStr, int THRESHOLD)
        {
            if (myStr.Length > THRESHOLD)
                return myStr.Substring(0, THRESHOLD) + "...";
            return myStr;
        }

        #endregion Public Methods
    }
}