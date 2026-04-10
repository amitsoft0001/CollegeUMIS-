using DataLayer;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Website.Models;

namespace Website.Controllers
{
    public class LoginController : Controller
    {
        public static string ReCaptcha_Key = "";
        public static string ReCaptcha_Secret = "";

        public ActionResult Index()
        {
            ReCaptcha_Key = "6Lek4JwUAAAAABWxgpAF18nsiR1nuikeWHzSmi3v";
            ReCaptcha_Secret = "6Lek4JwUAAAAAEbvGDeQOTni9dHBJj6TMIsG61-a";
            if (HttpContext.Request.Cookies["ENNBCollegeCode"] != null && HttpContext.Request.Cookies["ENNBUID"] != null)
            {
                return RedirectToAction("Index", "Home", new { area = "College" });
            }
            return View();

        }
        //public ActionResult DEvlogin()
        //{
        //    ReCaptcha_Key = "6Lek4JwUAAAAABWxgpAF18nsiR1nuikeWHzSmi3v";
        //    ReCaptcha_Secret = "6Lek4JwUAAAAAEbvGDeQOTni9dHBJj6TMIsG61-a";
        //    if (HttpContext.Request.Cookies["ENNBCollegeCode"] != null && HttpContext.Request.Cookies["ENNBUID"] != null)
        //    {
        //        return RedirectToAction("Index", "Home", new { area = "College" });
        //    }
        //    return View();

        //}

        public ActionResult SendOtp(string email)
        {
            string otp = new Random().Next(100000, 999999).ToString();

            EmailHelper.SendOtp(email, otp);

            return Json(new { success = true, message = "OTP sent", otp = otp }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult test()
        {
            //ReCaptcha_Key = "6Lek4JwUAAAAABWxgpAF18nsiR1nuikeWHzSmi3v";
            //ReCaptcha_Secret = "6Lek4JwUAAAAAEbvGDeQOTni9dHBJj6TMIsG61-a";
            //if (HttpContext.Request.Cookies["ENNBCollegeCode"] != null && HttpContext.Request.Cookies["ENNBUID"] != null)
            //{
            //    return RedirectToAction("Index", "Home", new { area = "College" });
            //}
            //using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            //{
            //    string msgBody = "";
            //    string body = string.Empty;
            //    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //    filePath = filePath + "emailtemplate/otp.html";
            //    using (StreamReader reader = new StreamReader(filePath))
            //    {
            //        body = reader.ReadToEnd();
            //    }
            //    body = body.Replace("@name@", "hf kjsdhf"); //replacing the required things  
            //    body = body.Replace("@otp@", "2222");
            //    body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            //    mailMessage.From = new MailAddress("demo@gmail.com", "MDS University");
            //    mailMessage.Subject = "Send OTP";
            //    mailMessage.Body = body.Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
            //    mailMessage.IsBodyHtml = true;
            //    mailMessage.To.Add(new MailAddress("preetam.kumar@demo.com"));
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Host = "smtp.gmail.com";
            //    smtp.EnableSsl = Convert.ToBoolean(true);
            //    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            //    NetworkCred.UserName = "demo@gmail.com";
            //    NetworkCred.Password = "Brsoft@123";
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = NetworkCred;
            //    smtp.Port = 587;
            //    smtp.Send(mailMessage);
            //}
            //SendMail("preetam.kumar@demo.com", "preetam", "OTP", "test", "");
            return View();

        }
        public static string FromEmailAddress_Gmail_cracex = "no-reply@crakex.in";
        public static string FromEmailPassword_GmailPassword_cracex = "";  // "!@#$%^";
        public static string MailIpAddress_gmail_cracex = "mail.crakex.in";

        public string SendMail(string ToEmail, string Name, string Subject, string Body, string _SenderEmail = "")
        {





            string StrReturn = "";
            string CRLF = "\r\n";
            string[] Receivers = ToEmail.Split(',');
            using (MailMessage mailMessage = new MailMessage())
            {
                //try
                //{
                //String MessageID = "Message-ID: <" + Guid.NewGuid().ToString() + DateTime.Now.Ticks + "@demo.com>" + CRLF;                  

                mailMessage.From = new MailAddress(FromEmailAddress_Gmail_cracex, Name);
                mailMessage.Subject = Subject;
                mailMessage.Body = Body.Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
                mailMessage.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(_SenderEmail))
                {
                    mailMessage.ReplyTo = new MailAddress(_SenderEmail);
                }


                //mailMessage.Headers.Add("Message-ID", MessageID);
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                for (int i = 0; i < Receivers.Length; i++)
                {
                    mailMessage.To.Add(new MailAddress(Receivers[i]));
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = MailIpAddress_gmail_cracex;
                smtp.EnableSsl = Convert.ToBoolean(true);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = FromEmailAddress_Gmail_cracex;
                NetworkCred.Password = FromEmailPassword_GmailPassword_cracex;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
                StrReturn = "True";
                //}
                //catch (Exception ex)
                //{
                //    StrReturn = ex.Message;
                //}

            }
            return StrReturn;
        }
        [HttpPost]
        public ActionResult test(BL_CollegeLogin lo)
        {
            SMS sms = new SMS();
            SMSFUN sms1 = new SMSFUN();
            //SMS.Sendget(lo.PrincipalMobile, "send otp msg");
            SMSFUN.Send_OTPverifymobile(lo.PrincipalMobile, "4454");
            TempData["errorMsg"] = "Send Successfully !!";
            return View();

        }
        //public JsonResult ResetPass
        public string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }
        [HttpPost]
        public JsonResult IndexNew(BL_CollegeLogin objlogin)
        {
            BL_CollegeLogin objCollege = new BL_CollegeLogin();
            try
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(objlogin.Password));
                objlogin.U_password = hashedBytes;
                objlogin.OTP = CommonMethod.RandomNumber(1000, 9999);
                var obj = objCollege.Login(objlogin);
                if (obj.Status)
                {
                    // Send OTP
                    SMSFUN.sms_loginotp(obj.employeeMobileNo, obj.OTP);
                    //Email.SendEmailForcl_otp(obj.Email, obj.OTP,obj.CollegeName,obj.UserName);
                    //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.CollegeCode.ToString(), DateTime.Now, expiryDate, true, obj.CollegeCode + "$#$" + obj.CollegeName + "$#$" + obj.ID + "$#$" + obj.Photo + "$#$" + obj.Password + "$#$" + obj.UserID + "$#$" + obj.UserName + "$#$" + obj.UserPassword + "$#$" + obj.UserType+"$#$"+obj.UID + "$#$" + obj.Name + "$#$" + obj.LastLoginsvrDt);
                    //string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    ////create a new authentication cookie - and set its expiration date
                    //HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    ////--- new code
                    //string[] userdata = ticket.UserData.Split(new[] { "$#$" }, StringSplitOptions.None);
                    ////ClsLanguage.SetCookies(userdata[0], "NBCollegeCode");
                    //ClsLanguage.SetCookies(userdata[1], "NBCollegeName");
                    //ClsLanguage.SetCookies(userdata[3], "NBCPhoto");
                    //ClsLanguage.SetCookies(userdata[6], "NBUserName");
                    //ClsLanguage.SetCookies(userdata[10], "NBUserssName");

                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[0]), "ENNBCollegeCode");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENNBCollegeName");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[3]), "ENNBCPhoto");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[2]), "ENNBCLID");

                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[6]), "ENNBUserID");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[7]), "ENNBPassword");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[8]), "ENNBUserType");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[9]), "ENNBUID");
                    //ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[11]), "ENNBLastLogin");

                    ////--
                    //if (obj.rememberMe)
                    //{
                    //    HttpCookie remCookie = new HttpCookie("Crcpc");
                    //    remCookie.Expires = DateTime.Now.AddHours(CommonSetting.Cookieshoursset);
                    //    Response.Cookies.Add(remCookie);
                    //    Response.Cookies["Crcpc"]["li"] = EncriptDecript.Encrypt(obj.CollegeCode);
                    //    Response.Cookies["Crcpc"]["p"] = EncriptDecript.Encrypt(obj.Password);
                    //    Response.Cookies["Crcpc"]["r"] = obj.rememberMe.ToString();
                    //    authenticationCookie.Expires = ticket.Expiration;
                    //}
                    //else
                    //{
                    //    authenticationCookie.Expires = DateTime.Now.AddHours(CommonSetting.Cookieshoursset);
                    //    if (Request.Cookies["Crcpc"] != null)
                    //    {
                    //        Response.Cookies["Crcpc"].Expires = DateTime.Now.AddDays(-101);
                    //    }
                    //}
                    //Response.Cookies.Add(authenticationCookie);
                    //obj.CollegeCode + "$#$" + obj.CollegeName + "$#$" + obj.ID + "$#$" + obj.Photo + "$#$" + obj.Password + "$#$" + obj.UserID
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = obj.Status;
                    newobj.Message = obj.Message;// + obj.OTP.ToString();
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = obj.Status;
                    newobj.Message = obj.Message;
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Login Panel", "");

            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Index(UserLogin objlogin)
        {
            try
            {
                //var obj = objlogin.Login(objlogin);
                //if (obj.status)
                //{
                //    if (!obj.IsActive)
                //    {

                //        TempData["errorMsg"] = "Your account has been deactivated";
                //        return View(obj);
                //    }
                //    //TempData["AdminName"] = obj.UserName;
                //    College_MenuMaster menu = new College_MenuMaster();
                //    List<College_MenuMaster> _menus = menu.getAdminMenuList("AdminViewAll").ToList();
                //    if (obj.menustr == null)
                //    {
                //        obj.menustr = ",";
                //    }
                //    string[] values = obj.menustr.Split(',');
                //    List<College_MenuMaster> i = _menus.Where(m => values.Contains(m.MenuID.ToString())).ToList();



                //    DateTime expiryDate = DateTime.Now.AddMinutes(20);
                //    expiryDate = DateTime.Now.AddDays(7);

                //    //ormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.UserID.ToString(), DateTime.Now, expiryDate, true, obj.UserID.ToString() + "$#$" + obj.UserName + "$#$" + obj.UserType.ToString() + "$#$" + obj.menustr);
                //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.UserID.ToString(), DateTime.Now, expiryDate, true, obj.UserID.ToString() + "$#$" + obj.UserName + "$#$" + obj.UserType.ToString() + "$#$" + obj.menustr + "$#$" + obj.Permission.ToString() + "$#$" + obj.UserType.ToString());


                //    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                //    //create a new authentication cookie - and set its expiration date
                //    HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                //    //--- new code
                //    string[] userdata = ticket.UserData.Split(new[] { "$#$" }, StringSplitOptions.None);
                //    ClsLanguage.SetCookies(userdata[0], "NUserId");
                //    ClsLanguage.SetCookies(obj.UserName, "NUserName");
                //    ClsLanguage.SetCookies(userdata[4], "NPermission");
                //    ClsLanguage.SetCookies(userdata[5], "NUsertype");

                //    //--
                //    if (obj.rememberMe)
                //    {
                //        HttpCookie remCookie = new HttpCookie("rcpc");
                //        remCookie.Expires = DateTime.Now.AddDays(1);
                //        Response.Cookies.Add(remCookie);
                //        Response.Cookies["rcpc"]["li"] = EncriptDecript.Encrypt(obj.Email);
                //        Response.Cookies["rcpc"]["p"] = EncriptDecript.Encrypt(obj.Password);
                //        Response.Cookies["rcpc"]["r"] = obj.rememberMe.ToString();
                //        authenticationCookie.Expires = ticket.Expiration;
                //    }
                //    else
                //    {
                //        authenticationCookie.Expires = DateTime.Now.AddDays(1);
                //        if (Request.Cookies["rcpc"] != null)
                //        {
                //            Response.Cookies["rcpc"].Expires = DateTime.Now.AddDays(-101);
                //        }
                //    }
                //    Response.Cookies.Add(authenticationCookie);
                //    if (obj.UserType == (int)CommonSetting.UserType.Admin)
                //    { }
                //    TempData["errorMsg"] = "";
                //    //return RedirectToAction("Index", "Home/dashboard");
                //    return Json(obj, JsonRequestBehavior.AllowGet);


                //}
                //else
                //{
                //    TempData["errorMsg"] = "Invalid LoginId & Password";
                //    return Json(obj, JsonRequestBehavior.AllowGet);
                //}
                // return View(obj);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Login Panel", "");

            }
            return View();
        }
        [HttpPost]
        public JsonResult VerifyOTP(BL_CollegeLogin objlogin)
        {
            BL_CollegeLogin objCollege = new BL_CollegeLogin();
            try
            {


                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(objlogin.Password));
                objlogin.U_password = hashedBytes;
                if (objlogin.OTP == null || objlogin.OTP == "")
                {
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = false;
                    newobj.Message = "Please enter OTP !!";
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
                var obj = objCollege.verifyotpLogin(objlogin);
                if (obj.Status)
                {
                    if (obj.Message != null)
                    {
                        TempData["errorMsg"] = obj.Message;
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    if (obj.LastLoginsvrDt == "01 Jan 1900 00:00:00")
                    {
                        obj.LastLoginsvrDt = System.DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt");

                    }
                    TempData["CollegeName"] = obj.CollegeName;
                    DateTime expiryDate = DateTime.Now.AddMinutes(20);
                    expiryDate = DateTime.Now.AddDays(7);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.CollegeCode.ToString(), DateTime.Now, expiryDate, true, obj.CollegeCode + "$#$" + obj.CollegeName + "$#$" + obj.ID + "$#$" + obj.Photo + "$#$" + obj.Password + "$#$" + obj.UserID + "$#$" + obj.UserName + "$#$" + obj.UserPassword + "$#$" + obj.UserType + "$#$" + obj.UID + "$#$" + obj.Name + "$#$" + obj.LastLoginsvrDt);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //create a new authentication cookie - and set its expiration date
                    HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //--- new code
                    string[] userdata = ticket.UserData.Split(new[] { "$#$" }, StringSplitOptions.None);
                    //ClsLanguage.SetCookies(userdata[0], "NBCollegeCode");
                    ClsLanguage.SetCookies(userdata[1], "NBCollegeName");
                    ClsLanguage.SetCookies(userdata[3], "NBCPhoto");
                    ClsLanguage.SetCookies(userdata[6], "NBUserName");
                    ClsLanguage.SetCookies(userdata[10], "NBUserssName");

                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[0]), "ENNBCollegeCode");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENNBCollegeName");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[3]), "ENNBCPhoto");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[2]), "ENNBCLID");

                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[6]), "ENNBUserID");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[7]), "ENNBPassword");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[8]), "ENNBUserType");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[9]), "ENNBUID");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[11]), "ENNBLastLogin");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(obj.Currenttime.ToString()), "Currenttime");


                    //--
                    if (obj.rememberMe)
                    {
                        HttpCookie remCookie = new HttpCookie("Crcpc");
                        remCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(remCookie);
                        Response.Cookies["Crcpc"]["li"] = EncriptDecript.Encrypt(obj.CollegeCode);
                        Response.Cookies["Crcpc"]["p"] = EncriptDecript.Encrypt(obj.Password);
                        Response.Cookies["Crcpc"]["r"] = obj.rememberMe.ToString();
                        authenticationCookie.Expires = ticket.Expiration;
                    }
                    else
                    {
                        authenticationCookie.Expires = DateTime.Now.AddDays(1);
                        if (Request.Cookies["Crcpc"] != null)
                        {
                            Response.Cookies["Crcpc"].Expires = DateTime.Now.AddDays(-101);
                        }
                    }
                    Response.Cookies.Add(authenticationCookie);
                    //obj.CollegeCode + "$#$" + obj.CollegeName + "$#$" + obj.ID + "$#$" + obj.Photo + "$#$" + obj.Password + "$#$" + obj.UserID
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = obj.Status;
                    newobj.Message = obj.Message;
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = obj.Status;
                    newobj.Message = obj.Message;
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Login Panel", "");

            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult devIndexNew(BL_CollegeLogin objlogin)
        {
            BL_CollegeLogin objCollege = new BL_CollegeLogin();
            try
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(objlogin.Password));
                objlogin.U_password = hashedBytes;
                objlogin.OTP = CommonMethod.RandomNumber(1000, 9999);
                var obj = objCollege.Login(objlogin);
                if (obj.Status)
                {
                    //SMSFUN.sms_loginotp("8963057650", obj.OTP);
                    //Email.SendEmailForcl_otp("preetam.kumar@demo.com", obj.OTP, obj.CollegeName, obj.UserName);
                    if (obj.LastLoginsvrDt == "01 Jan 1900 00:00:00")
                    {
                        obj.LastLoginsvrDt = System.DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt");

                    }
                    TempData["CollegeName"] = obj.CollegeName;
                    DateTime expiryDate = DateTime.Now.AddMinutes(20);
                    expiryDate = DateTime.Now.AddDays(7);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, obj.CollegeCode.ToString(), DateTime.Now, expiryDate, true, obj.CollegeCode + "$#$" + obj.CollegeName + "$#$" + obj.ID + "$#$" + obj.Photo + "$#$" + obj.Password + "$#$" + obj.UserID + "$#$" + obj.UserName + "$#$" + obj.UserPassword + "$#$" + obj.UserType + "$#$" + obj.UID + "$#$" + obj.Name + "$#$" + obj.LastLoginsvrDt);
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    //create a new authentication cookie - and set its expiration date
                    HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    //--- new code
                    string[] userdata = ticket.UserData.Split(new[] { "$#$" }, StringSplitOptions.None);
                    //ClsLanguage.SetCookies(userdata[0], "NBCollegeCode");
                    ClsLanguage.SetCookies(userdata[1], "NBCollegeName");
                    ClsLanguage.SetCookies(userdata[3], "NBCPhoto");
                    ClsLanguage.SetCookies(userdata[6], "NBUserName");
                    ClsLanguage.SetCookies(userdata[10], "NBUserssName");

                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[0]), "ENNBCollegeCode");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[1]), "ENNBCollegeName");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[3]), "ENNBCPhoto");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[2]), "ENNBCLID");

                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[6]), "ENNBUserID");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[7]), "ENNBPassword");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[8]), "ENNBUserType");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[9]), "ENNBUID");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[11]), "ENNBLastLogin");
                    ClsLanguage.SetCookies(EncriptDecript.Encrypt(obj.Currenttime.ToString()), "Currenttime");


                    //--
                    if (obj.rememberMe)
                    {
                        HttpCookie remCookie = new HttpCookie("Crcpc");
                        remCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(remCookie);
                        Response.Cookies["Crcpc"]["li"] = EncriptDecript.Encrypt(obj.CollegeCode);
                        Response.Cookies["Crcpc"]["p"] = EncriptDecript.Encrypt(obj.Password);
                        Response.Cookies["Crcpc"]["r"] = obj.rememberMe.ToString();
                        authenticationCookie.Expires = ticket.Expiration;
                    }
                    else
                    {
                        authenticationCookie.Expires = DateTime.Now.AddDays(1);
                        if (Request.Cookies["Crcpc"] != null)
                        {
                            Response.Cookies["Crcpc"].Expires = DateTime.Now.AddDays(-101);
                        }
                    }
                    Response.Cookies.Add(authenticationCookie);
                    //obj.CollegeCode + "$#$" + obj.CollegeName + "$#$" + obj.ID + "$#$" + obj.Photo + "$#$" + obj.Password + "$#$" + obj.UserID
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = obj.Status;
                    newobj.Message = obj.Message;
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BL_CollegeLogin newobj = new BL_CollegeLogin();
                    newobj.Status = obj.Status;
                    newobj.Message = obj.Message;
                    return Json(newobj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Login Panel", "");

            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            UserLogin.ExpireAllCookies();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ForgotPassword(string id)
        {
            string AdminID = EncriptDecript.Decrypt(id);
            BL_ForgotPass obj = new BL_ForgotPass();

            obj.AdminID = Convert.ToInt32(AdminID);
            ViewBag.ID = Convert.ToInt32(AdminID);
            return View();
        }
        [HttpPost]
        public JsonResult ForgotPasswords(string Password, int id)
        {
            BL_ForgotPass forgot = new BL_ForgotPass();
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password));
            // objlogin.U_password = hashedBytes;

            var obj = forgot.ForgotPasswordCollege(id, Password, hashedBytes);
            return Json(new { data = obj, success = true });
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ResetPass(string EmailID)
        {
            BL_ForgotPass obj = new BL_ForgotPass();
            var sendreset = obj.ResetPassClg(EmailID);
            int AdminRegID = sendreset.CollegeID;
            string ID = EncriptDecript.Encrypt(AdminRegID.ToString());
            string MyName = sendreset.CollegeName;
            string url = ConfigurationManager.AppSettings["siteUrl"];
            string PasswordResetLink = url + "Login/ForgotPassword?Id=" + ID;
            if (AdminRegID > 0)
            {
                Email.SendEmailForResetPassword(EmailID, MyName, PasswordResetLink);
                obj.Msg = "Reset Password Link sent to your registered Email ID..!!";
                obj.Status = true;
                TempData["errorMsg"] = obj.Msg;
            }
            else { obj.Msg = "Invalid email address..!!"; }

            return Json(new { data = obj, success = true });
        }
        //  Admin  Forgot Password  start

        public ActionResult captchaa()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}