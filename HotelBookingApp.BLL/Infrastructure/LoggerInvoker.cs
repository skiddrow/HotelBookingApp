using NLog;

namespace HotelBookingApp.BLL.Infrastructure
{
    public static class LoggerInvoker
    {
        public static Logger Logger
        {
            get
            {
                return LogManager.GetCurrentClassLogger();
            }
        }
    }
}
