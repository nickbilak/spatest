using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace Vacations.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    [ExcludeFromCodeCoverage]
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Logger field
        /// </summary>
        private static ILog _logger;


        /// <summary>
        /// Handler for start event
        /// </summary>
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            LoggerConfig.RegisterParams();

            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Info(string.Format("{0} starts...", Assembly.GetExecutingAssembly().GetName().Name));
        }



        /// <summary>
        /// Handler for error event
        /// </summary>
        protected void Application_Error()
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var error = Server.GetLastError();

            _logger.Error(error.Message, error);

            HttpContext.Current.Response.ContentType = "text/html; charset=UTF-8";
        }


        /// <summary>
        /// Handler for begin request event
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="e">The event</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }
        }



        protected void Application_EndRequest()
        {
            // removing excessive headers. We don't need to see this.
            Response.Headers.Remove("Server");
        }
    }
}