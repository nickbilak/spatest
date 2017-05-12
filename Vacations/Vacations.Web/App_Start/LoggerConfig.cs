using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Vacations.Web
{
    [ExcludeFromCodeCoverage]
    public class LoggerConfig
    {
        /// <summary>
        /// Registers logger parameters
        /// </summary>
        public static void RegisterParams()
        {
            var moduleName = Assembly.GetExecutingAssembly().GetName();
            var loggerPath = ConfigurationManager.AppSettings[@"log4net.config"];
            var env = ConfigurationManager.AppSettings["environment"];

            GlobalContext.Properties["ApplicationName"] = moduleName.Name;
            GlobalContext.Properties["ApplicationVersion"] = moduleName.Version.ToString();
            GlobalContext.Properties["Environment"] = env;

            XmlConfigurator.ConfigureAndWatch(new FileInfo(loggerPath));
        }
    }
}