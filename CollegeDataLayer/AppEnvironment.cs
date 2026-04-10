using System.Web;
using System.Configuration;

public static class AppEnvironment
{
    public static string GetEnvironment()
    {
        try
        {
            var context = HttpContext.Current;

            if (context == null || context.Request == null)
                return "Live"; // fallback

            string host = context.Request.Url.Host.ToLower();

            if (host.Contains("localhost") || host.Contains("127.0.0.1"))
                return "Local";

            return "Live";
        }
        catch
        {
            return "Live"; // safest fallback
        }
    }

    public static string GetModule()
    {
        return ConfigurationManager.AppSettings["Module"];
    }
}