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

namespace DataLayer
{
    public class BL_ForgotPass
    {

        [StringLength(20, ErrorMessage = "Password must be at least {5} characters long.", MinimumLength = 5)]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        public string ApplicationID { get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int UserType { get; set; }
        public int AdminID { get; set; }
        public int SID { get; set; }
      
        public string Name { get; set; }
        //college start
        public string CollegeName { get; set; }
        public int CollegeID { get; set; }
        //college end

        public BL_ForgotPass ForgotPassword(int ID, string NewPassword)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPassword", new { @ID = ID, @Role = "ResetAdminPass", @NewPassword= NewPassword }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }
        public BL_ForgotPass ResetPass(string EmailID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPassword", new { @Email = EmailID, @Role = "SendAdmin" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }

        public BL_ForgotPass ResetPassClg(string EmailID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPassword", new { @Email = EmailID, @Role = "SendCollege" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }
        public BL_ForgotPass ForgotPasswordCollege(int ID, string NewPassword,byte[] U_password)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<BL_ForgotPass>("sp_ForgotPassword", new { @ID = ID, @Role = "ResetCollegePass", @NewPassword = NewPassword, @U_password= U_password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ob;
            }
        }

    }
}
