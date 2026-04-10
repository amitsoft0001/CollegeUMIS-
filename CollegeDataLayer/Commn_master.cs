using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
namespace DataLayer
{
   public  class Commn_master
    {      
        public int CommonId { get; set; }
        public int CommonCode { get; set; }
        public string Session { get; set; }
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Titlecode { get; set; }
        public string CreateDate { get; set; }
        public string Type { get; set; }
        public int CourseCategoryID { get; set; }
        public int EducationTypeID { get; set; }
        public string CourseCategory { get; set; }
        public int StreamCategoryID { get; set; }
        public string streamCategory { get; set; }
        public int bloodgrouid { get; set; }
        public string BloodGroup { get; set; }
        public decimal commonvalue { get; set; }
        public int ID { get; set; }
        public string YearName { get; set; }
        public string Name { get; set; }
        public bool isopendate { get; set; }
        public bool isclosedate { get; set; }
        public string opendate { get; set; }
        public string closedate { get; set; }

        public List<string> GetAllowedRoutes(string module, string env)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                return conn.Query<string>(
                    "sp_GetAllowedRoutes",
                    new { @Module = module, @Environment = env },
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }
        public bool Backcheck_ExamFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "BackExamFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
               // return true;
            }
        }
        public bool Backcheck_ExamFeeSubmit_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "BackExamFeeenddate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
               // return true;
            }
        }
        public List<Commn_master> getcommonMaster(string type,  int Id= 0,int quaid =0 )
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_Common_master]", new { @type = type, @Id= Id , @quaid = quaid }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<Commn_master> getCourseMaster( int Id,int CollegeID, int Session)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_Student_RollNo]", new { @Action = "GetCourse", @Id = Id , @CollegeID = CollegeID, @Session=Session }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<Commn_master> Getbloodgroup(string Action, string BloodGroup="", int Id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_BloodGroup]", new { @Id = Id, @BloodGroup = BloodGroup, @Action = Action }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<Commn_master> GetSession(string Action)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_GetSession]", new {   @Action = Action }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

        public bool check_admission_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_admissionopen]", new { @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public bool check_ExamFeeSubmit_open(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ExamFeestartDate", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }
        public bool check_ExamFeeSubmit_close(int seessionid = 0, int educationtype = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<bool>("[sp_check_ExamFeeopen]", new { @Action = "ExamFee", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }
        public Commn_master check_ExamFeeSubmit_check(int seessionid = 0, int educationtype = 0)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                //obj.isopendate = true;
                //obj.isclosedate = true;
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new { @Action = "ExamFeecheck", @seessionid = seessionid, @educationtype = educationtype }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
                //return true;
            }
        }
        public Commn_master check_BAckExamFeeSubmit_check(int seessionid = 0, int educationtype = 0,int courseyearid=1)
        {
            Commn_master obj = new Commn_master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Commn_master>("[sp_check_ExamFeeopen]", new { @Action = "BAckExamFeecheck", @seessionid = seessionid, @educationtype = educationtype ,@courseyearid=courseyearid}, commandType: CommandType.StoredProcedure).FirstOrDefault();
                // obj.isopendate = true;
                //  obj.isclosedate = true;
                return obj;

                //return true;
            }
        }
        public bool CheckStudentAddmisionExtendDate(int SessionID = 0, int educationtype = 0)
        {
             using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process]", new
                {
                    @Action = "CheckExtendDate",
                    @SessionID = SessionID,
                    @AddmissionCategoryid = 1,
                    @educationtype = educationtype
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj.Status;
            }
        }
        public bool CheckStudentAddmisionStartDate(int SessionID = 0, int educationtype = 0)
        {
           
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process]", new
                {
                    @Action = "CheckStartDate",
                    @SessionID = SessionID,
                    @AddmissionCategoryid = 1,
                    @educationtype = educationtype
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj.Status;
            }
        }

    }
}
