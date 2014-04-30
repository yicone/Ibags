using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Ibags.API.App_Start
{
    public class Logger
    {
        private static ILog instance;
        public static ILog Instance()
        {
            if (instance == null)
            {
                instance = log4net.LogManager.GetLogger(
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
                );
            }
            instance.Info("Logger has a new instance. ");
            return instance;
        }

        private Logger() { }
    }
}