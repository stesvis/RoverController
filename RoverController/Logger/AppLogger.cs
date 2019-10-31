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
    }
}