using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace DataLayer
{
    public class Notifications
    {

        public string SendNotification(string[] ArrPlayerIds)
        {

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic YmUwZmQ3MTUtMjg4ZC00ZTZiLWI3ZTUtOTZjNGM5ZDJmNDUy");

            string PlayerIds = JsonConvert.SerializeObject(ArrPlayerIds);
            //MDEwYmFkYzAtMDI5ZC00OTIwLTkyMmYtNTg2MDZiYmU4MDM5
            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"749bb129-e5ff-4278-9838-e2e2b3edcb5f\","
                                                    + "\"contents\": {\"en\": \"English Message\"},"
                                                    + "\"include_player_ids\": [\"33ce12fe-a89f-4040-912c-e5f547f0b7fa\"]}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
            return "";

        }
        public string SendNotification(string[] ArrPlayerIds, string Msg)
        {

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic YmUwZmQ3MTUtMjg4ZC00ZTZiLWI3ZTUtOTZjNGM5ZDJmNDUy");

            string PlayerIds = JsonConvert.SerializeObject(ArrPlayerIds);





            //MDEwYmFkYzAtMDI5ZC00OTIwLTkyMmYtNTg2MDZiYmU4MDM5
            //byte[] byteArray = Encoding.UTF8.GetBytes("{"
            //                                        + "\"app_id\": \"749bb129-e5ff-4278-9838-e2e2b3edcb5f\","
            //                                        + "\"contents\": {\"en\": \"English Message\"},"
            //                                        + "\"include_player_ids\": [\"33ce12fe-a89f-4040-912c-e5f547f0b7fa\"]}");


            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                   + "\"app_id\": \"749bb129-e5ff-4278-9838-e2e2b3edcb5f\","
                                                   + "\"contents\": {\"en\": \"" + Msg + "\"},"
                                                   + "\"include_player_ids\": " + PlayerIds + "}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
            return "";

        }


        public string SendNotification(string[] ArrPlayerIds, string Msg, string dataKey, string DataKeyValue)
        {

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic YmUwZmQ3MTUtMjg4ZC00ZTZiLWI3ZTUtOTZjNGM5ZDJmNDUy");

            string PlayerIds = JsonConvert.SerializeObject(ArrPlayerIds);





            //MDEwYmFkYzAtMDI5ZC00OTIwLTkyMmYtNTg2MDZiYmU4MDM5
            //byte[] byteArray = Encoding.UTF8.GetBytes("{"
            //                                        + "\"app_id\": \"749bb129-e5ff-4278-9838-e2e2b3edcb5f\","
            //                                        + "\"contents\": {\"en\": \"English Message\"},"
            //                                        + "\"include_player_ids\": [\"33ce12fe-a89f-4040-912c-e5f547f0b7fa\"]}");


            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                   + "\"app_id\": \"749bb129-e5ff-4278-9838-e2e2b3edcb5f\","
                                                   + "\"contents\": {\"en\": \"" + Msg + "\"},"
                                                   + "\"data\": {\"" + dataKey + "\": \"" + DataKeyValue + "\"},"
                                                   + "\"include_player_ids\": " + PlayerIds + "}");

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
            return "";

        }

        /// <summary>
        /// Send notification by group
        /// </summary>
        /// <param name="UserGroup">Value Must be In( All & Active Users) </param>
        /// <param name="Msg"></param>
        /// <returns></returns>

        public string SendNotificationByGroup(string UserGroup, string Msg)
        {

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic YmUwZmQ3MTUtMjg4ZC00ZTZiLWI3ZTUtOTZjNGM5ZDJmNDUy");

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = "749bb129-e5ff-4278-9838-e2e2b3edcb5f",
                contents = new { en = Msg },
                included_segments = new string[] { UserGroup }
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
            return "";
        }
    }


}
