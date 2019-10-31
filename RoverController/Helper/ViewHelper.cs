using System.Web;

namespace RoverController.Web.Helper
{
    public class ViewHelper
    {
        public static bool IsSuperadmin
        {
            get { return (bool)HttpContext.Current.Session["IsSuperadmin"]; }
            set { HttpContext.Current.Session["IsSuperadmin"] = value; }
        }

        public static bool IsAdmin
        {
            get { return (bool)HttpContext.Current.Session["IsAdmin"]; }
            set { HttpContext.Current.Session["IsAdmin"] = value; }
        }

        public static bool IsUser
        {
            get { return (bool)HttpContext.Current.Session["IsUser"]; }
            set { HttpContext.Current.Session["IsUser"] = value; }
        }

        public static string CurrentUserId
        {
            get { return (string)HttpContext.Current.Session["CurrentUserId"]; }
            set { HttpContext.Current.Session["CurrentUserId"] = value; }
        }
    }
}