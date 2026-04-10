using SendGrid.SmtpApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for mailbysendgrid
/// </summary>
public class mailbysendgrid
{
    private string _TemplateDoc;
    private string _EmailTemplateSubject;
    private static bool _mailSent;
    private string[] _ArrValues;
    private string _Email;
    public string[] ValueArray
    {
        //ARRAY OF VALUES TO REPLACE VARS IN TEMPLATE 
        set { _ArrValues = value; }
    }
    public string EmailTemplateFileName
    {
        //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
        set { _TemplateDoc = value; }
    }
    public string EmailTemplateSubject
    {
        //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
        set { _EmailTemplateSubject = value; }
    }
    public string Emailsend
    {
        //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
        set { _Email = value; }
    }
    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        var token = (string)e.UserState;

        if (e.Cancelled)
        {
            Console.WriteLine("[{0}] Send canceled.", token);
        }
        if (e.Error != null)
        {
            Console.WriteLine("[{0}] {1}", token, e.Error);
        }
        else
        {
            Console.WriteLine("Message sent.");
        }
        _mailSent = true;
    }

    private string XsmtpapiHeaderAsJson()
    {
        var header = new Header();

        var uniqueArgs = new Dictionary<string, string>
            {
                {
                    "foo",
                    "bar"
                },
                {
                    "chunky",
                    "bacon"
                },
                {
                    // UTF8 encoding test
                    Encoding.UTF8.GetString(Encoding.Default.GetBytes("dead")),
                    Encoding.UTF8.GetString(Encoding.Default.GetBytes("beef"))
                }
            };
        header.AddUniqueArgs(uniqueArgs);

        var subs = new List<String> { "Hello" };
        header.AddSubstitution("%tag%", subs);

        return header.JsonString();
    }
    public void mailsend()
    {
        try
        {
            string xmstpapiJson = XsmtpapiHeaderAsJson();
            var client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.sendgrid.net",
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true
            };

            string username = "Mungeruniversity";
            string password = "Munger@123";

            //string username = "mightyyield";
            //string password = "hjfbmdsgk651@68416";
            client.Credentials = new NetworkCredential(username, password);



            MailMessage mailMsg = new MailMessage();

            // To
            //mailMsg.To.Add(new MailAddress("to@example.com", "To Name"));

            // From
           // mailMsg.From = new MailAddress("info@mightyyield.com", "mightyield.com");

            mailMsg.From = new MailAddress("admission-no-reply@mungeruniversity.ac.in", "Munger University");
            mailMsg.Subject = _EmailTemplateSubject;
           // string text = "Text Body";

            // string html = mailbody(name, Loginname, activationkey);
            string html = _TemplateDoc;
            //string html = "fdsfsdf";
           // mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // add the custom header that we built above
            mailMsg.Headers.Add("X-SMTPAPI", xmstpapiJson);
            mailMsg.BodyEncoding = Encoding.UTF8;
            //async event handler
            client.SendCompleted += SendCompletedCallback;
            const string state = "test1";
            string email = _Email;
            //string name = membername;
            if (email != null)
            {
                mailMsg.To.Add(new MailAddress(email));
                client.Send(mailMsg);
            }

            mailMsg.Dispose();
        }
        catch (Exception ex)
        {

        }
    }

    #region GetHtml
    public string GetHtml(string argTemplateDocument)
    {
        int i;
        StreamReader filePtr;
        string fileData = "";
        //var host = HttpContext.Current.Request.Url.Scheme +"//:" + HttpContext.Current.Request.Url.Authority;
        //var data = host + "\\EmailTemplates" + "\\" + argTemplateDocument;
        //System.Web.HttpContext.Current.Server.MapPath("~/SignatureImages/");
        //filePtr = File.OpenText(HttpContext.Current.Server.MapPath("~/EmailTemplates/activationmail.html"));
        //("~\\EmailTemplates") + "\\" + argTemplateDocument);
        //  filePtr = File.OpenText(host+"\\EmailTemplates"+ "\\" + argTemplateDocument);
        var data = HttpContext.Current.Server.MapPath("\\EmailTemplates") + "\\" + argTemplateDocument;
        filePtr = File.OpenText(data);
        fileData = filePtr.ReadToEnd();

        if ((_ArrValues == null))
        {
            return fileData;
        }
        else
        {
            for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
            {
                fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
            }
            filePtr.Close();
            return fileData;

        }

        filePtr.Close();
        filePtr = null;
    }
    #endregion
}








