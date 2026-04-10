

using DataLayer;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class PageAuthorize : AuthorizeAttribute
{
    //protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
    //{
    //    var routeData = httpContext.Request.RequestContext.RouteData;

    //    string controller = routeData.Values["controller"].ToString();
    //    string action = routeData.Values["action"].ToString();
    //    string method = httpContext.Request.HttpMethod;

    //    // ❌ Area hata diya
    //    string currentPage = $"{controller}-{action}";

    //    // 🔐 Sirf ye GET pages allowed
    //    var allowedGetPages = new[]
    //    {
    //    "HomeUGCBCS-Index",
    //    "HomeUGCBCS-BasicDetail",
    //    "HomeUGCBCS-AdmissionFeeSubmit",
    //    "Login-Index",
    //    "Login-Logout",
    //    "Login-OldLogin"
    //};

    //    // ✅ POST sab allow
    //    if (method == "POST")
    //    {
    //        return true;
    //    }

    //    // 🔥 GET me sirf allowed pages
    //    if (method == "GET")
    //    {
    //        return allowedGetPages.Any(x =>
    //            x.Equals(currentPage, StringComparison.OrdinalIgnoreCase));
    //    }

    //    return false;
    //}
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        filterContext.Result = new RedirectResult("NotAllowed");
    }


    //protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
    //{
    //    string url = httpContext.Request.RawUrl.ToLower();

    //    // ✅ POST allow
    //    if (httpContext.Request.HttpMethod == "POST")
    //    {
    //        return true;
    //    }

    //    // 🔥 DB nahi — cache use
    //    var allowedRoutes = RouteCache.AllowedRoutes;

    //    return allowedRoutes.Any(x => url.StartsWith(x.ToLower()));
    //}

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        string url = httpContext.Request.RawUrl.ToLower();

        // ✅ POST allow
        if (httpContext.Request.HttpMethod == "POST")
        {
            return true;
        }

        // 🔥 CACHE EMPTY HAI TO LOAD KARO (FIRST TIME)
        if (RouteCache.AllowedRoutes == null || RouteCache.AllowedRoutes.Count == 0)
        {
            string env = AppEnvironment.GetEnvironment();
            string module = AppEnvironment.GetModule();

            RouteCache.AllowedRoutes = new Commn_master().GetAllowedRoutes(module, env);
        }

        var allowedRoutes = RouteCache.AllowedRoutes;

        return allowedRoutes.Any(x => url.StartsWith(x.ToLower()));
    }



}