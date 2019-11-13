using System;
using NLog;

namespace MetroOil.LoyaltyOps
{
    public class LoyaltyLogger
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LoyaltyLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        //public CardTrendNLogLogger(string serviceName)
        //{
        //    _logger = LogManager.GetLogger(serviceName);
        //}
        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);
        }

        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Error(Exception x)
        {
            Error(BuildExceptionMessage(x));
        }

        public static void Error(string message, Exception x)
        {
            _logger.ErrorException(message, x);
        }

        public static void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public static void Fatal(Exception x)
        {
            Fatal(BuildExceptionMessage(x));
        }

        private static string BuildExceptionMessage(Exception x)
        {
            Exception logException = x;
            if (x.InnerException != null)
                logException = x.InnerException;

            //string strErrorMsg = Environment.NewLine + "Error in Path :" + System.Web.HttpContext.Current.Request.Path;

            //// Get the QueryString along with the Virtual Path
            //strErrorMsg += Environment.NewLine + "Raw Url :" + System.Web.HttpContext.Current.Request.RawUrl;

            // Get the error message
            string strErrorMsg = Environment.NewLine + "Message :" + logException.Message;

            // Source of the message
            strErrorMsg += Environment.NewLine + "Source :" + logException.Source;

            // Stack Trace of the error

            strErrorMsg += Environment.NewLine + "Stack Trace :" + logException.StackTrace;

            // Method where the error occurred
            strErrorMsg += Environment.NewLine + "TargetSite :" + logException.TargetSite;
            return strErrorMsg;
        }
    }
}