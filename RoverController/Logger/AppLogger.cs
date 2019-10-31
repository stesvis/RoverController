using NLog;

namespace RoverController.Logger
{
    public class AppLogger
    {
        public static NLog.Logger Logger
        {
            get
            {
                return LogManager.GetLogger("fileLogger");
            }
        }

        public static NLog.Logger FatalLogger
        {
            get
            {
                return LogManager.GetLogger("fatalLogger");
            }
        }

        public static NLog.Logger DebugLogger
        {
            get
            {
                return LogManager.GetLogger("debugLogger");
            }
        }

        public static NLog.Logger RouteLogger
        {
            get
            {
                return LogManager.GetLogger("routeLogger");
            }
        }

        public static NLog.Logger TaskLogger
        {
            get
            {
                return LogManager.GetLogger("taskLogger");
            }
        }

        public static NLog.Logger SmsLogger
        {
            get
            {
                return LogManager.GetLogger("smsLogger");
            }
        }

        public static NLog.Logger NotificationsLogger
        {
            get
            {
                return LogManager.GetLogger("notificationsLogger");
            }
        }
    }
}