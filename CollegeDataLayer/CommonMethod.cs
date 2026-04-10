using DataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Dapper;
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
namespace DataLayer
{
    public interface IAddressRepository
    {
        IList<Country> GetAllCountries();
        IList<State> GetStatesByCountryId(int countryid);
    }
    public class board12
    {
        public int boardid { get; set; }
        public string boardname { get; set; }
    }
    
    public class CommonMethod
    {
        public enum SubSubjectType
        {
            CoreSubject = 1,
            Electivesubject = 2
        }
        public static List<board12> Boradtype()
        {
            List<board12> list = new List<board12>
            {
                new board12 { boardid = 1, boardname = "Demo State Bord"},
                new board12 { boardid =2, boardname = "Other Board" },
            };
            return list;
        }
        //public static string IPAddress = "192.185.11.49";
        //public static string varFrom_mail_Address = "nitingarg2016@demo.com";
        //public static string varFrom_mail_password = "ne1uN68#";
        public static string IPAddress = "";
        public static string varFrom_mail_Address = "";
        public static string varFrom_mail_password = "";
        public static string applicationIDAndroid = "AIzaSyCMa1r7MI9dmiJPpWq7pDv0pAg4fzQi5f0";// this is e911 md "AIzaSyBvQ54XWc4FEZlzqBPMh7a6QQcrpRb5Y_Q";
        DataSet ds;
        SqlDataAdapter da;
        public static SqlConnection connect()
        {
            //Reading the connection string from web.config    
            string Name = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
            //Passing the string in sqlconnection.    
            SqlConnection con = new SqlConnection(Name);
            //Check wheather the connection is close or not if open close it else open it    
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            else
            {

                con.Open();
            }
            return con;

        }
        public static void PrintLog(Exception ex,string url="",string action="",string id="")
        {
            try
            {
                string dir = @"C:\Error.txt";  // folder location
                if (!Directory.Exists(dir))
                {
                    using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/Error.txt"), true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("URl : " + url);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Action : " + action);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("ID : " + id);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine();
                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);
                            ex = ex.InnerException;
                        }
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }

        //public static void WritetoNotepad(Exception ex, string Url, string Remarks = "", string id = "")
        //{
        //    string strErrorContent = string.Empty;
        //    string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
        //    string strPreContent = string.Empty;


        //    strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "CollegeLogFile/" + "Error_" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

        //    if (!File.Exists(strErrorFile.Trim()))
        //    {
        //        FileStream fs1 = new FileStream(strErrorFile,
        //            FileMode.OpenOrCreate, FileAccess.Write);
        //        StreamWriter writer = new StreamWriter(fs1);
        //        writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
        //        //  writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
        //        writer.WriteLine("**ID >>>>=" + id);
        //        writer.WriteLine("**Path >>>>=" + Url);
        //        writer.WriteLine("**Remarks >>>>=" + Remarks);
        //        writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
        //        writer.WriteLine("==================================================================");
        //        while (ex != null)
        //        {
        //            writer.WriteLine(ex.GetType().FullName);
        //            writer.WriteLine("**Message : >>>>" + ex.Message);
        //            writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
        //            ex = ex.InnerException;
        //        }
        //        writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
        //        writer.Close();

        //    }
        //    else
        //    {
        //        FileStream fs1 = new FileStream(strErrorFile,
        //        FileMode.Append, FileAccess.Write);
        //        StreamWriter writer = new StreamWriter(fs1);
        //        writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
        //        //writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
        //        writer.WriteLine("**ID >>>>=" + id);
        //        writer.WriteLine("**Path >>>>=" + Url);
        //        writer.WriteLine("**Remarks >>>>=" + Remarks);
        //        writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.AddHours(5.5).ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
        //        writer.WriteLine("=================================================================");
        //        while (ex != null)
        //        {
        //            writer.WriteLine(ex.GetType().FullName);
        //            writer.WriteLine("**Message : >>>>" + ex.Message);
        //            writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
        //            ex = ex.InnerException;
        //        }
        //        writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
        //        writer.Close();

        //    }




        //    //----------------------
        //}
        public static void WritetoNotepad(Exception ex, string Url, string Remarks = "", string id = "")
        {
            string strErrorContent = string.Empty;
            string strErrorFile = string.Empty;//System.AppDomain.CurrentDomain.BaseDirectory + "ApplicationErrors.txt";
            string strPreContent = string.Empty;


            strErrorFile = System.AppDomain.CurrentDomain.BaseDirectory + "CollegeLogFile/" + "Error_" + System.DateTime.Now.ToString("dd_MM_yyyy") + ".txt";

            if (!File.Exists(strErrorFile.Trim()))
            {
                FileStream fs1 = new FileStream(strErrorFile,
                    FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //  writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                //writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                writer.WriteLine("==================================================================");
                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("**Message : >>>>" + ex.Message);
                    writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
                    ex = ex.InnerException;
                }
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();

            }
            else
            {
                FileStream fs1 = new FileStream(strErrorFile,
                FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.WriteLine("***************************Start Block***************************" + Environment.NewLine);
                //writer.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
                writer.WriteLine("**ID >>>>=" + id);
                writer.WriteLine("**Path >>>>=" + Url);
                writer.WriteLine("**Remarks >>>>=" + Remarks);
                // writer.WriteLine("**DateTime >>>>" + DateTime.UtcNow.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                // writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));

                writer.WriteLine("**DateTime >>>>" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat));
                writer.WriteLine("=================================================================");
                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("**Message : >>>>" + ex.Message);
                    writer.WriteLine("**StackTrace :>>>> " + ex.StackTrace);
                    ex = ex.InnerException;
                }
                writer.WriteLine("*************************** END BLOCK***************************" + Environment.NewLine);
                writer.Close();

            }

        }

        public static string RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max).ToString();
        }


        public static string GetIPAddress()
        {
            //String strHostName = string.Empty;
            //strHostName = Dns.GetHostName();
            //string ipaddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //Console.WriteLine("Local Machine's Host Name: " + strHostName);
            //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            //IPAddress[] addr = ipEntry.AddressList;
            //return addr[1].ToString();
            string ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
         
            return ipaddress.ToString();
        }

        //Creating a method which accept any type of query from controller to execute and give result.    
        //result kept in datatable and send back to the controller.    
        public DataTable MyMethod(string Query)
        {
            ds = new DataSet();
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(Query, CommonMethod.connect());

            da.Fill(dt);
            List<SelectListItem> list = new List<SelectListItem>();
            return dt;

        }


        //public DataSet Get_Country()

        //{

        //    SqlCommand com = new SqlCommand("Select * from tbl_Country", CommonMethod.connect());

        //    SqlDataAdapter da = new SqlDataAdapter(com);

        //    DataSet ds = new DataSet();

        //    da.Fill(ds);

        //    return ds;

        //}

        //public List<City> GetCityListByStateId(string state_id)
        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        return conn.Query<City>("Select ID,CityName from tbl_Cities where stateID=" + state_id).ToList();

        //    }
        //}

        ////Get all State

        //public List<State> Get_State(string country_id)

        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {

        //       var result=conn.Query<State> ("Select * from tbl_StatesName where Countryid=" + country_id).ToList();

        //        return result;

        //    }




        //}

        //public DataSet Get_state1(string state_id)

        //{

        //    SqlCommand com = new SqlCommand("Select * from tbl_StatesName where Countryid=" + state_id, CommonMethod.connect());

        //    SqlDataAdapter da = new SqlDataAdapter(com);

        //    DataSet ds = new DataSet();

        //    da.Fill(ds);

        //    return ds;

        //}

        ////Get all City

        //public DataSet Get_City(string state_id)

        //{

        //    SqlCommand com = new SqlCommand("Select * from tbl_Cities where stateID=" + state_id,CommonMethod.connect());

        //    SqlDataAdapter da = new SqlDataAdapter(com);

        //    DataSet ds = new DataSet();

        //    da.Fill(ds);

        //    return ds;

        //}
        //public List<City> Get_City1(string state_id)

        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {

        //        var result = conn.Query<City>("Select * from tbl_Cities where stateID=" + state_id).ToList();
        //        return result;

        //    }




        //}

        //public DataSet Get_Branch()

        //{

        //    SqlCommand com = new SqlCommand("proc_fnBranch 'viewAll',0", CommonMethod.connect());

        //    SqlDataAdapter da = new SqlDataAdapter(com);

        //    DataSet ds = new DataSet();

        //    da.Fill(ds);

        //    return ds;

        //}

        //public DataSet Get_Customer()

        //{

        //    SqlCommand com = new SqlCommand("proc_MemberMasterList 'viewlist',0", CommonMethod.connect());

        //    SqlDataAdapter da = new SqlDataAdapter(com);

        //    DataSet ds = new DataSet();

        //    da.Fill(ds);

        //    return ds;

        //}

        public static void PrintLogNeeraj(UserLogin obj)
        {
            try
            {
                string dir = @"C:\Error.txt";  // folder location
                if (!Directory.Exists(dir))
                {
                    using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/Error.txt"), true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Email : " + obj.Email);
                        writer.WriteLine();
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("password : " + obj.Password);
                        writer.WriteLine();
                       
                       
                    }
                }
            }
            catch (Exception ex1)
            {

            }
        }

    }
}
