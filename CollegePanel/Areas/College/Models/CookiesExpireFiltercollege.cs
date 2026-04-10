using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using DataLayer;
using Website.Models;
namespace Website.Areas.college.Models
{
    public class CookiesExpireFilterCollegeAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            
            if (HttpContext.Current.Request.Cookies["ENNBCollegeCode"] == null && HttpContext.Current.Request.Cookies["ENNBUID"] == null)
            {
                 filterContext.Result = new RedirectResult("~/Login");
                 return;
            }
            else
            {
                if (HttpContext.Current.Request.Cookies["Currenttime"] == null )
                {
                    UserLogin.ExpireAllCookies();
                    filterContext.Result = new RedirectResult("~/Login");
                    return;
                }
                DateTime lastdatecheck = Convert.ToDateTime(EncriptDecript.Decrypt(DataLayer.ClsLanguage.GetCookies("Currenttime")));
                lastdatecheck = lastdatecheck.AddHours(2);
                if (lastdatecheck < DateTime.Now)
                {
                    UserLogin.ExpireAllCookies();
                    filterContext.Result = new RedirectResult("~/Login");
                    return;
                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}