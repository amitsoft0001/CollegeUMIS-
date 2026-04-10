using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace Website.Models
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute    
    {
       
        public string BasicRealm { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public BasicAuthenticationAttribute()
        {
           
        }
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            Username = ClsLanguage.GetCookies("ENNBUserID");
            Password = ClsLanguage.GetCookies("ENNBPassword");           
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name == Username && user.Pass == Password) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel"));
            /// thanks to eismanpat for this line: http://www.ryadel.com/en/http-basic-authentication-asp-net-mvc-using-custom-actionfilter/#comment-2507605761
            // filterContext.Result = new HttpUnauthorizedResult(statusDescription:"xdcscdbh");
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No session tr tlkjhet8ruyhtricket specified");

        }
    }

   
   

}
