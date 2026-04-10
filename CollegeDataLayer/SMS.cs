using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Configuration;

/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{
    public SMS()
    {
        //
        // TODO: Add constructor logic here
        //


    }

    public static void Send(string Mobile, string smsMessage)
    {
        try
        {
            Sendget(Mobile, smsMessage);      
        }
        catch (Exception ex)
        {

        }
    }

    public static void Sendpost(string Mobile, string smsMessage)
    {
        //Your authentication key
        string authKey = "276024AqshVwSJ5cd52d77";
        //Multiple mobiles numbers separated by comma
        string mobileNumber = Mobile;
        //Sender ID,While using route4 sender id should be 6 characters long.
        string senderId = "MGUNIV";
        //Your message to send, Add URL encoding here.
        string message = HttpUtility.UrlEncode(smsMessage);

        //Prepare you post parameters
        StringBuilder sbPostData = new StringBuilder();
        sbPostData.AppendFormat("authkey={0}", authKey);
        sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
        sbPostData.AppendFormat("&message={0}", message);
        sbPostData.AppendFormat("&sender={0}", senderId);
        sbPostData.AppendFormat("&route={0}", "4");

        try
        {
            //Call Send SMS API
            string sendSMSUri = "http://msg.sparkhost.com/api/sendhttp.php";
            //Create HTTPWebrequest
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //Prepare and Add URL Encoded data
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {

                stream.Write(data, 0, data.Length);
            }
            //Get the response
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            //Close the response
            reader.Close();
            response.Close();
        }
        catch (SystemException ex)
        {

        }
    }
    public static void Sendget(string Mobile, string smsMessage)
    {
        if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
            return;
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        WebClient client = new WebClient();
        // string baseurl = "http://msg.sparkhost.com/api/sendhttp.php?authkey=276024AqshVwSJ5cd52d77&mobiles=" + Mobile + "&message=" + smsMessage + "&sender=MGUNIV&route=4";
      //  string baseurl = "https://sms.hitechsms.com/app/smsapi/index.php?key=45DE60A5A188EC&campaign=0&routeid=13&type=text&contacts=" + Mobile + "&senderid=HITECH&msg=" + smsMessage + "";
        string baseurl = "http://103.129.97.36/index.php/smsapi/httpapi/?uname=mungeruniversity1&password=123456&sender=MGUNIV&receiver=" + Mobile + "&route=TA&msgtype=1&sms=" + smsMessage + "";

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        Stream data = client.OpenRead(baseurl);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();
    }
    public static void SendgetDLT(string Mobile, string smsMessage,string tempid)
    {
        if (WebConfigurationManager.AppSettings["EnableSMS"].ToString() != "true")
            return;
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        WebClient client = new WebClient();
       //  string baseurl = "http://103.129.97.36/index.php/smsapi/httpapi/?uname=mungeruniversity1&password=123456&sender=MGUNIV&receiver=" + Mobile + "&route=TA&msgtype=1&sms=" + smsMessage + "";
        //string baseurl = "http://43.252.88.230/index.php/smsapi/httpapi/?uname=U1226&password=JqE1FIFU&sender=MGUEDU&receiver="+ Mobile + "&route=TA&tempid="+ tempid + "&msgtype=1&sms="+ smsMessage + "";
        string baseurl = "http://sms.bulkssms.com/submitsms.jsp?user=AEIPLPAT&key=27456eec13XX&mobile="+ Mobile + "&message="+ smsMessage + "&senderid=OTPSSS&accusage=1&entityid=1201159543060917386&tempid=1207161729866691748";
        
        
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        Stream data = client.OpenRead(baseurl);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();
    }

}