using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

public class EmailHelper
{
    public static void SendOtp(string toEmail, string otp)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var host = ConfigurationManager.AppSettings["SmtpHost"];
            var port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var user = ConfigurationManager.AppSettings["SmtpUser"];
            var pass = ConfigurationManager.AppSettings["SmtpPass"];
            var from = ConfigurationManager.AppSettings["FromEmail"];

            SmtpClient smtp = new SmtpClient(host, port);
            smtp.Credentials = new NetworkCredential(user, pass);
            smtp.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(toEmail);
            mail.Subject = "Your OTP Code";
            mail.Body = "<h3>Your OTP is: " + otp + "</h3>";
            mail.IsBodyHtml = true;

            smtp.Send(mail);
        }
        catch(Exception ex)
        { 
        

        }
    }
}