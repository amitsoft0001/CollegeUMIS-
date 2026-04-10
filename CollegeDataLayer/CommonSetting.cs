using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace DataLayer
{
    public static class CommonSetting
    {
        public enum UserType
        {
            //Admin=1,
            //other=2//tbl_designationMaster RoleType Column
            Admin=1,
            Lecturer=2,
            Accountant=2,
            Cashier=2,
            Librarian=2

        }
        public enum Commonid
        {
            Educationtype = 11,
            EducationtypePG = 12,
            EducationtypeVoc=13,
            EducationtypeLLB=41,
            EducationtypeBED = 40,

            Femalegender = 9,
            General = 4,
            SC = 5,
            ST = 6,
            BC1 = 7,
            BC2 = 22,
            WBC = 23,
            Sports = 35,
            NCCCandidate = 36,
            Exserviceman = 37,
            UniversityStaff = 38,
            RegularAdmissionType = 1,
            Mr=16

        }
        public enum Streamtype
        {
            Art12 = 1,
            Science12 = 2,
            Comm12 = 3,

        }
        public enum Religion
        {
            OtherReligion=28
        }
        public enum coursecategory
        {
            ba = 1,
            bsc=2,
            bcomm=3,
            bca = 26,
            bba = 27,
            llb=29,
            bed=28,
            boitech=30
        }
        public enum CollegeID
        {
            LLBC = 34,
            BEdK = 35,
            BEdJ = 36,
            BEdM = 37,
            BEdR = 38,
            BEdS = 39
        }
        public static string ProjectNamecapital = "MUNGER UNIVERSITY";
        public enum commQualification
        {
            Ten = 1,
            Art12 = 2,
            Science12 = 3,
            Comm12 = 4,
            ArtUG = 5,
            ScienceUG = 7,
            CommUG = 9, others=12,
            diploma=11
        }
        public enum CourseYearID
        {
            BA1st = 1,
            BA2nd = 2,
            BA3rd = 3,
            Bsc1st = 4,
            Bsc2nd = 5,
            Bsc3rd = 6,
            Bcom1st = 7,
            Bcom2nd = 8,
            Bcom3rd = 9,
            CHEMISTRYID = 1049,
            Semester1BCA = 10,
            Semester2BCA = 11,
            Semester3BCA = 12,
            Semester4BCA = 13,
            Semester5BCA = 14,
            Semester6BCA = 15,
            Semester1BBA = 18,
            Semester2BBA = 19,
            Semester3BBA = 10,
            Semester4BBA = 21,
            Semester5BBA = 22,
            Semester6BBA = 23,
            MA1st = 16,
            MA2nd = 17,
            MSC1st = 24,
            MSC2nd = 25,
            MCOM1st = 27,
            MCOM2nd = 26,
        }

        public static string ProjectName = WebConfigurationManager.AppSettings["ProjectName"];
        public static string Usernmae = ClsLanguage.GetCookies("NUsername");
        public static string LoginID = ClsLanguage.GetCookies("NLoginID");
        public static int Cookieshoursset = 2;



        public static string Email = "demo@gmail.com";
        public static string EmailPassword = "G00gl3@123@#$";
        public static string EmailIp = "";
        public static string EmailHost = "smtp.gmail.com";
        public static int EmailPort = 587;
        public static string Emailbgimgurl = WebConfigurationManager.AppSettings["siteUrl"] +"emailtemplate/bg.png";
        public static string Emaillogo = WebConfigurationManager.AppSettings["EmailLogopath"] + "";
        public static string Emailfblogo = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/fb.png";
        public static string Emailtelogo = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/twitter.png";
        public static string Emailgooglelogo = WebConfigurationManager.AppSettings["siteUrl"] + "emailtemplate/google.png";
        public static string EmailSt_loginurl = WebConfigurationManager.AppSettings["EmailSt_loginurl"] ;
        //public static string Patient { get { return ""; } }
        //public static string Doctor { get { return ""; } }
        // public static string SiteUrl { get { return WebConfigurationManager.AppSettings["siteUrl"]; } }
        //public static string ProfilePhoto { get { return SiteUrl + "/uploads/profile/"; } }
        //public static string CountryFlag { get { return SiteUrl + "/uploads/flag/"; } }
        //public static string SpecialityPhoto { get { return SiteUrl + "/uploads/speciality/"; } }
        //public static string StaticPage { get { return SiteUrl + "/uploads/staticpage/"; } }
        //public static double HoursDiff { get { return 5.5; } }
        public static string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        public static string PhotonConstr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string RemoveSpecialChars(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { ",", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "<", ">", "?", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", "-", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\",", "]", "^", "_", "`", "{", "|", "}", "~" };
            //Iterate the number of times based on the String array length.
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }

            return str;
        }
        public static string RemoveSpecialCharsemail(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { ",", "/", "!", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "<", ">", "?", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", "-", "/", ":", ";", "<", "=", ">", "?", "[", "\",", "]", "^", "_", "`", "{", "|", "}", "~" };   //Iterate the number of times based on the String array length.
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }

            return str;
        }
        public static string RemoveSpecialCharsaddress(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "<", ">", "?", "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", ":", ";", "<", "=", ">", "?", "@", "[", "\",", "]", "^", "_", "`", "{", "|", "}", "~" };    //Iterate the number of times based on the String array length.
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }

            return str;
        }
        public static string Removenumber(string str)
        {
            // Create  a string array and add the special characters you want to remove
            //You can include / exclude more special characters based on your needs
            string[] chars = new string[] { "0", "2", "3", "4", "5", "6", "7", "8", "9", "1" };
            if (str == null)
            {
                return str;
            }
            for (int i = 0; i < chars.Length; i++)
            {

                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "0");
                }
            }

            return str;
        }

        public static bool SendMail(string TO, string Sub, string msg, string[] CC=null)
        {
            bool status = false;
            try
            {
                if (WebConfigurationManager.AppSettings["EnableEmail"] == "true")
                {

                    string StrBody = "";
                    MailMessage mail = new MailMessage();
                    mail.Priority = MailPriority.High;
                    mail.To.Add(TO);
                    if (CC != null)
                    {
                        foreach (var strEmail in CC)
                        {
                            mail.CC.Add(strEmail);
                        }
                    }
                    mail.From = new MailAddress(Email, ProjectName);
                    mail.Subject = Sub;
                    mail.IsBodyHtml = true;
                    StrBody = msg;
                    mail.Body = StrBody;
                    SmtpClient smtp = new SmtpClient(EmailIp);
                    NetworkCredential Credentials = new NetworkCredential(Email, EmailPassword);
                    smtp.Credentials = Credentials;
                    smtp.EnableSsl = true;
                    smtp.Host = EmailHost;
                    smtp.Port = EmailPort;
                    smtp.Send(mail);
                    status = true;
                }
            }
            catch (Exception ex)
            {
                CommonMethod.PrintLog(ex);
                status = false;

            }
            return status;

        }

    }
}
