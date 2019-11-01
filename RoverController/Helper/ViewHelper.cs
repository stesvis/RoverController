using System.Web;

namespace RoverController.Web.Helper
{
    public class ViewHelper
    {
        public static bool IsAdmin
        {
            get { return (bool)HttpContext.Current.Session["IsAdmin"]; }
            set { HttpContext.Current.Session["IsAdmin"] = value; }
        }

        public static string CurrentUserId
        {
            get { return (string)HttpContext.Current.Session["CurrentUserId"]; }
            set { HttpContext.Current.Session["CurrentUserId"] = value; }
        }
    }
}