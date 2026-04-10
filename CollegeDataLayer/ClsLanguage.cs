using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;

namespace DataLayer
{
    public class ClsLanguage
    {

        public static void SetCookies(string value, string key)
        {
            //HttpCookie abc = new HttpCookie(key);
            //abc.Expires =  DateTime.Now.AddDays(-1); // make it expire yesterday
            //HttpContext.Current.Response.Cookies.Add(abc); // overwrite it

            HttpCookie loginCookie1 = HttpContext.Current.Response.Cookies[key];
            if (loginCookie1 == null)
            {
                loginCookie1 = new HttpCookie(key);

            }
            HttpContext.Current.Response.Cookies[key].Value = value; // <--- strange!!!!
           HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddHours(CommonSetting.Cookieshoursset);
           // HttpContext.Current.Response.Cookies[key].Expires = HttpContext.Current.Session.Timeout;
            HttpContext.Current.Response.Cookies.Add(loginCookie1);

        }

        public static string GetCookies(string key)
        {
            HttpCookie languageCookie = HttpContext.Current.Request.Cookies[key];
            if (languageCookie == null)
            {
                return "ok";
            }
            return languageCookie.Value;


        }
    }
}
