using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            ////routes.MapRoute("ForgotPassword", "forgot-password", new { controller = "Home", action = "ForgotPassword" });
            ////routes.MapRoute("DoctorRegister", "register-your-practice", new { controller = "Home", action = "DoctorRegister" });
            routes.MapRoute("studentdata", "StudentList", new {  controller = "Home", action = "StudentList" }, new String[] { "Website.Areas.Administrator.Controllers" }).DataTokens = new RouteValueDictionary(new { area = "Administrator" }); ;
            routes.MapRoute("printpdf", "PDF", new { controller = "Home", action = "printpdf" }, new String[] { "Website.Areas.Administrator.Controllers" }).DataTokens = new RouteValueDictionary(new { area = "Administrator" }); ;
            routes.MapRoute("printpdf1", "PDF1", new { controller = "Home", action = "printpdfwaitinglist" }, new String[] { "Website.Areas.Administrator.Controllers" }).DataTokens = new RouteValueDictionary(new { area = "Administrator" }); ;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           // routes.MapRoute(
           //    name: "Default",
           //    url: "{controller}/{action}/{Id}",
           //    defaults: new { controller = "Home", action = "Index", Id = UrlParameter.Optional }
           //);
            routes.MapRoute(
                        "Default",
                        "{controller}/{action}/{id}",
                        new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                        namespaces: new[] { "Website.Controllers" }
                    );

            routes.MapRoute("Adminindex", "{controller}/{action}", new { area = "Administrator", controller = "Home", action = "index" }, new String[] { "Website.Areas.Administrator.Controllers" });
            routes.MapRoute("Studentindex", "{controller}/{action}", new { area = "Student", controller = "Home", action = "index" }, new String[] { "Website.Areas.Student.Controllers" });
            routes.MapRoute("Collegeindex", "{controller}/{action}", new { area = "College", controller = "Home", action = "index" }, new String[] { "Website.Areas.College.Controllers" });


          
         

           //       var route = routes.MapRoute(
           //name: "Default",
           //url: "{controller}/{action}/{id}",
           //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           //).DataTokens = new RouteValueDictionary(new { area = "MyArea" });
        }
    }
}
