using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataLayer;
using System.IO;
using System.Web.Mvc;

namespace DataLayer
{
    public class BL_CollegeLogin
    {
        public int ID { get; set; }
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public string Createdate { get; set; }
        public string UpdateDate { get; set; }
        public int InsertBy { get; set; }
        public string Ipaddress { get; set; }
        public bool Isactive { get; set; }
        public int Title { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public int Gender { get; set; }
        public string Photo { get; set; }
        public int NoOfSeats { get; set; }
        public int NoOfRooms { get; set; }
        public string NodalOfficerName { get; set; }
        public string NodalOfficerEmail { get; set; }
        public string NodalOfficerMobile { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalEmail { get; set; }
        public string PrincipalMobile { get; set; }
        public string Password { get; set; }
        public int IsLogin { get; set; }
        public bool Status { get; set; }
        public bool rememberMe { get; set; }
        public string Message { get; set; }
        public string txtCaptcha { get; set; }
        public string UserID { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }//1 for Collegeadmin and 2 for College Employee
        public string UserName { get; set; }
        public int UID { get; set; }
        public byte[] U_password { get; set; }
        public string LastLoginsvrDt { get; set; }
        public string OTP { get; set; }
        public string employeeMobileNo { get; set; }
        public DateTime Currenttime { get; set; }
        public BL_CollegeLogin Login(BL_CollegeLogin objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                objLogin = conn.Query<BL_CollegeLogin>("[Proc_CollegeLogin]", new { @action= "login", @otp= objLogin.OTP, @CollegeCode = objLogin.CollegeCode, @Password = objLogin.Password, @IPAddress = ip, @U_password= objLogin.U_password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objLogin.ID <= 0)
                {
                    objLogin.Status = false;
                }
                return objLogin;
            }
        }
        public BL_CollegeLogin verifyotpLogin(BL_CollegeLogin objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                objLogin = conn.Query<BL_CollegeLogin>("[Proc_CollegeLogin]", new { @action = "verifyotplogin", @otp = objLogin.OTP, @CollegeCode = objLogin.CollegeCode, @Password = objLogin.Password, @IPAddress = ip, @U_password = objLogin.U_password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objLogin.ID <= 0)
                {
                    objLogin.Status = false;

                }
                return objLogin;
            }
        }
        public ChangeCollegePassword ChangePassword(ChangeCollegePassword obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<ChangeCollegePassword>("sp_ChangeCollegePassword", new { @CollegeCode = obj.CollegeID, CurrentPassword = obj.CurrentPassword, NewPassword = obj.NewPassword, Status = "ChangePassword", @U_password =obj.U_password  , @U_passwordNew=obj.U_passwordNew }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
         
        }
        public static bool logout(string id = "0")
        {
            ExpireAllCookies();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Login>("sp_ChangeCollegePassword", new { @CollegeCode  = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return true;
            }

        }
        public static void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        if (cookie.Name != "rcpc")
                        {
                            var expiredCookie = new HttpCookie(cookie.Name)
                            {

                                Expires = DateTime.Now.AddDays(-1),
                                Domain = cookie.Domain

                            };
                            HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                        }

                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }

    }
    public class ChangeCollegePassword
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }
        [StringLength(20, ErrorMessage = "Password must be at least {5} characters long.", MinimumLength = 5)]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        public string ConfirmPassword { get; set; }
        public int CollegeID { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public byte[] U_password { get; set; }
        public byte[] U_passwordNew { get; set; }
    }
    public class CollegeLoginHistory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string IPAddress { get; set; }
        public string Date { get; set; }
        public CollegeLoginHistoryList UserLogindetailList(int pageIndex = 1, int pageSize = 25,string collegeID="")
        {


            CollegeLoginHistoryList list = new CollegeLoginHistoryList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_CollegeLoginHistory]", new { @Action = "view", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID = collegeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeLoginHistory>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    //CollegeLoginHistory
    public class CollegeLoginHistoryList
    {
        public List<CollegeLoginHistory> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
