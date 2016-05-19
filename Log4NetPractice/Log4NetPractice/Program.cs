using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Log4NetPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Error("logger root");


            var logA = log4net.LogManager.GetLogger("loggerA");
            logA.Error("logger a");

            var logB = log4net.LogManager.GetLogger("loggerB");
            logB.Error("logger b");
        }
    }
}
