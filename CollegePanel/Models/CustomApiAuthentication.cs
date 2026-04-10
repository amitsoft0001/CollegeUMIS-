using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Website.Models
{
    public class CustomApiAuthentication : AuthorizationFilterAttribute
    {
      
            public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {
                    // Gets header parameters  
                    string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                    string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                    // Gets username and password  
                    string usrename = originalString.Split(':')[0];
                    string password = originalString.Split(':')[1];
                string enusrename = EncriptDecript.Decrypt(usrename);
                string enpassword = EncriptDecript.Decrypt(password);
                // Validate username and password  
                if (!ApiSecurity.VaidateUser(enusrename, enpassword))
                    {
                        // returns unauthorized error  
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }

                base.OnAuthorization(actionContext);
            }
        }
    }
