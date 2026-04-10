using DataLayer;
using System;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {

            string module = ConfigurationManager.AppSettings["Module"];
            string env = AppEnvironment.GetEnvironment();
            RouteCache.AllowedRoutes = new Commn_master().GetAllowedRoutes(module, env);
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            // Manually installed WebAPI 2.2 after making an MVC project.
            GlobalConfiguration.Configure(Website.WebApiConfig.Register); // NEW way
            //WebApiConfig.Register(GlobalConfiguration.Configuration); // DEPRECATED
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {


            if (HttpContext.Current.User != null)
            {

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {

                    if (HttpContext.Current.User.Identity

                        is FormsIdentity)
                    {

                        FormsIdentity id = (FormsIdentity)

                        HttpContext.Current.User.Identity;

                        FormsAuthenticationTicket ticket = id.Ticket;

                        string userInfo = ticket.UserData;

                        string[] roles = userInfo.Split(new[] { "$#$" }, StringSplitOptions.None);

                        HttpContext.Current.User =

                                     new GenericPrincipal(id, roles);

                    }

                }

            }

        }
    }
}
