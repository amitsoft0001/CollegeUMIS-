using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BL_StudentDetailByID
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string CastCategory { get; set; }
        public string BloodGroup { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string CA_PinCode { get; set; }
        public string CA_Country { get; set; }
        public string CA_State { get; set; }
        public string CA_City { get; set; }
        public string PA_Address { get; set; }
        public string PA_PinCode { get; set; }
        public string PA_Country { get; set; }
        public string PA_State { get; set; }
        public string PA_City { get; set; }
        public string FatherName { get; set; }
        public string FatherQualification { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string MotherName { get; set; }
        public string MotherQualification { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherEmail { get; set; }
        public string Session { get; set; }
        public string AdmisitionCategory { get; set; }
        public string EducationType { get; set; }
        public string CourseCategory { get; set; }
        public string IsQualifying { get; set; }
        public string IsLogin { get; set; }
        public string IsFeeSubmit { get; set; }
        public string Password { get; set; }
        public string MotherMobile { get; set; }
        public string stphoto { get; set; }
        public string stsign { get; set; }
        public string adddate { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }

        public BL_StudentDetailByID GetStudentdt(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<BL_StudentDetailByID>("sp_StudentRegistration", new { @Action = "GetStudentDetailByID", @Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }
   
    public class BL_StudentDetailList
    {
        public List<Bl_SeatMater> qlist { get; set; }       
    }
}
