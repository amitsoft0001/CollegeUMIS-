using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Configuration;
using System.Threading;
using System.Net.Mail;

namespace DataLayer
{
    public class Email
    {
        public static string FromEmailAddress_Gmail_cracex = "demo@gmail.com";
        public static string FromEmailPassword_GmailPassword_cracex = "";
        public static string MailIpAddress_gmail_cracex = "smtp.gmail.com";

        public static void sendmailthread(string body, string subject, string email)
        {
            using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            {

                mailMessage.From = new MailAddress(FromEmailAddress_Gmail_cracex, "no-reply");
                mailMessage.Subject = subject;
                mailMessage.Body = body.Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
                mailMessage.IsBodyHtml = true;
                //mailMessage.Body = "helo  this is testing mail";
                //mailMessage.IsBodyHtml = false;
                mailMessage.To.Add(new MailAddress(email));
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

            }
            return;
            //mailbysendgrid mailsendgrid = new mailbysendgrid();
            //mailsendgrid.EmailTemplateFileName = body;
            //mailsendgrid.EmailTemplateSubject = subject;
            //mailsendgrid.Emailsend = email;
            //mailsendgrid.mailsend();
        }
        public static void SendEmailForcl_otp(string email, string otp, string name, string username)
        {
            string msgBody = "";
            string body = string.Empty;
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            filePath = filePath + "emailtemplate/otp.html";
            using (StreamReader reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("@name@", username); //replacing the required things  
            body = body.Replace("@otp@", otp);
            body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                return;
            sendmailthread(body, "OTP", email);
            //Thread backgroundThread = new Thread(() => sendmailthread(body, "OTP", email));
            //backgroundThread.IsBackground = true;
            //backgroundThread.Start();
        }

        public static void SendStudentLoginDetails(string email, string name, string username, string password)
        {
            if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
                return;

            string body = $@"
    Dear {name},

    Your login details are as follows:

    User ID: {username}
    Password: {password}

    Please keep your credentials safe and do not share with anyone.

    Regards,
    Munger University
    ";

            sendmailthread(body, "Student Login Details", email);
        }


        public static void SendEmailForSt_signup(string email, string password, string name,string applicaionno)
        {
            //string msgBody = "";
            //string body = string.Empty;
            //string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //filePath = filePath + "emailtemplate/signup.html";
            //using (StreamReader reader = new StreamReader(filePath))
            //{
            //    body = reader.ReadToEnd();
            //}
            //body = body.Replace("@name@", name); //replacing the required things  
            //body = body.Replace("@loginid@", applicaionno);
            //body = body.Replace("@password@", password);
            //body = body.Replace("@year@", DateTime.Now.Year.ToString());
            //body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
            //body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
            //body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
            //body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
            //body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
            //body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            //body = body.Replace("@loginurl@", DataLayer.CommonSetting.EmailSt_loginurl);

            //  DataLayer.CommonSetting.SendMail(email, "Student Registration", body);
            //if (WebConfigurationManager.AppSettings["EnableEmail"].ToString() != "true")
            //    return;
            ////sendmailthread(body, "Student Registration", email);
            //Thread backgroundThread = new Thread(() => sendmailthread(body, "Student Registration", email));
            //backgroundThread.IsBackground = true;
            //backgroundThread.Start();
        }
  
       
        public static void SendEmailForCollege_signup(string email, string password, string name, string collegeCode, string college, string UserID)
        {
            //string msgBody = "";
            //string body = string.Empty;
            //string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //filePath = filePath + "emailtemplate/register_confirmation.html";
            //using (StreamReader reader = new StreamReader(filePath))
            //{
            //    body = reader.ReadToEnd();
            //}
            //body = body.Replace("@name@", name); //replacing the required things  
            //body = body.Replace("@loginid@", UserID);
            //body = body.Replace("@password@", password);
            //body = body.Replace("@college@", college);
            //body = body.Replace("@year@", DateTime.Now.Year.ToString());
            //body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
            //body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
            //body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
            //body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
            //body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
            //body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            //DataLayer.CommonSetting.SendMail(email, "College Registration", body);
            //mailbysendgrid mailsendgrid = new mailbysendgrid();
            //mailsendgrid.EmailTemplateFileName = body;
            //mailsendgrid.EmailTemplateSubject = "College Registration";
            //mailsendgrid.Emailsend = email;
            //mailsendgrid.mailsend();
        }

        public static void SendEmailForResetPassword(string email, string name, string Link)
        {
            //string msgBody = "";
            //string body = string.Empty;
            //string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //filePath = filePath + "emailtemplate/ResetPassword.html";
            //using (StreamReader reader = new StreamReader(filePath))
            //{
            //    body = reader.ReadToEnd();
            //}
            //body = body.Replace("@name@", name); //replacing the required things  
            //body = body.Replace("@link@", Link);
            //body = body.Replace("@year@", DateTime.Now.Year.ToString());
            //body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
            //body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
            //body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
            //body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
            //body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
            //body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            // DataLayer.CommonSetting.SendMail(email, "Reset Password", body);
            //mailbysendgrid mailsendgrid = new mailbysendgrid();
            //mailsendgrid.EmailTemplateFileName = body;
            //mailsendgrid.EmailTemplateSubject = "Reset Password";
            //mailsendgrid.Emailsend = email;
            //mailsendgrid.mailsend();
        }
        public static void SendEmailForUser_SignUp(string email, string password, string name,string empID="")
        {
            //string msgBody = "";
            //string body = string.Empty;
            //string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //filePath = filePath + "emailtemplate/register_confirmation.html";
            //using (StreamReader reader = new StreamReader(filePath))
            //{
            //    body = reader.ReadToEnd();
            //}
            //body = body.Replace("@name@", name); //replacing the required things  
            //body = body.Replace("@loginid@", name);
            //body = body.Replace("@password@", password);
            //body = body.Replace("@empID@", empID);
            //body = body.Replace("@year@", DateTime.Now.Year.ToString());
            //body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
            //body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
            //body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
            //body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
            //body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
            //body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            // DataLayer.CommonSetting.SendMail(email, "College Registration", body);
            //mailbysendgrid mailsendgrid = new mailbysendgrid();
            //mailsendgrid.EmailTemplateFileName = body;
            //mailsendgrid.EmailTemplateSubject = "College Registration";
            //mailsendgrid.Emailsend = email;
            //mailsendgrid.mailsend();
        }
        public static void SendEmailForLeaveStatus(string email, string name, string Status,string CollegeName)
        {
            //string msgBody = "";
            //string body = string.Empty;
            //string filePath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //filePath = filePath + "emailtemplate/EmailTamplate.html";
            //using (StreamReader reader = new StreamReader(filePath))
            //{
            //    body = reader.ReadToEnd();
            //}
            //msgBody = "<table width='100%' border='0' cellspacing='0'><tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr><tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr><tr><td colspan = '2' align = 'left' valign = 'top' style = 'font-size:14px; color:#006DF0;' > " + CollegeName + "</td></tr><tr><td align = 'left' colspan = '2' valign = 'top' height = '15px' ></td></tr>";
            //msgBody += "<tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr><tr><td width='50px' align='left' valign='top' style='font-size:16px;'></td><td align='left' valign='top' style='font-size:16px;'></td></tr>";
            //msgBody += "<tr><td width = '50px' align = 'left' valign = 'top' style = 'font-size:16px;' ><strong > Dear : </strong ></td><td align = 'left' valign = 'top' style = 'font-size:16px;'><strong> "+name+" </strong ></td ></tr > ";
            //msgBody += "<tr ><td align = 'left' colspan = '2' valign = 'top' height = '2px' ></td></tr><tr><table width='100%' border='0' cellspacing='0'>";
          
            //msgBody += "</td></tr>";
           
            //   msgBody += "<td colspan='2' align='left' valign='top' style='font-size:14px;'></td></tr><tr><td align='left' colspan='2' valign='top' height='10px'></td></tr> <tr><td colspan='2' align='left' valign='top' style='font-size:14px;'><p style='margin:0; padding:0;'>Your Leave Application has been "+Status +".</p></td></tr><tr><td align='left' colspan='2' valign='top' height='10px'></td></tr><tr><td align='left' colspan='2' valign='top' height='10px'></td></tr></table>";
                                                                            
            //  body = body.Replace("@msgBody@", msgBody); //replacing the required things  
            
            //body = body.Replace("@year@", DateTime.Now.Year.ToString());
            //body = body.Replace("@bgurl@", DataLayer.CommonSetting.Emailbgimgurl);
            //body = body.Replace("@logourl@", DataLayer.CommonSetting.Emaillogo);
            //body = body.Replace("@fburl@", DataLayer.CommonSetting.Emailfblogo);
            //body = body.Replace("@twittterurl@", DataLayer.CommonSetting.Emailtelogo);
            //body = body.Replace("@gooleourl@", DataLayer.CommonSetting.Emailgooglelogo);
            //body = body.Replace("@@projectname@", DataLayer.CommonSetting.ProjectName);
            // DataLayer.CommonSetting.SendMail(email, "Leave Status", body);
            //mailbysendgrid mailsendgrid = new mailbysendgrid();
            //mailsendgrid.EmailTemplateFileName = body;
            //mailsendgrid.EmailTemplateSubject = "Leave Status";
            //mailsendgrid.Emailsend = email;
            //mailsendgrid.mailsend();
        }
    }
}
