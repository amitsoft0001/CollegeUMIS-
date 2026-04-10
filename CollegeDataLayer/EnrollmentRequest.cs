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
   public class EnrollmentRequest
    {
        public int ID { get; set; }
        public string EnrollmentNo { get; set; }
        public string EnrollmentReqDate { get; set; }
        public string ApplicationNo { get; set; }
        public int SID { get; set; }
        public int Ftitle { get; set; }
        public int EnrollmentStatus { get; set; }
        public string EnrollmentGrantDate { get; set; }
        public int EnrollmentGrantBy { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string CourseApplied { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string InsertedBy { get; set; }
        public string EncriptedID { get; set; }

        public string collegeName { get; set; }
        public string sessionname { get; set; }
        public string FatherName { get; set; }
        public string photo { get; set; }
        public string streamCategory { get; set; }
        public string MotherName { get; set; }
        public string bloodgroup { get; set; }
        public string DOI { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Prisign { get; set; }
        public string stsign { get; set; }
        public int is_affiliated { get; set; }
        public string RollNo { get; set; }
        public string CollegeCode { get; set; }
        public EnrollmentRequest ApplyforEnrollment(EnrollmentRequest ob)
        {
            ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
            var IP = CommonMethod.GetIPAddress();
            using (System.Data.IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EnrollmentRequest>("[sp_EnrollmentRequest]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,               
                    @ApplicationNo = ob.ApplicationNo,                   
                    @IPaddress = IP,
                    @SID = ob.SID,
                
                    
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public EnrollmentRequest DetailsByApplication(string ApplicationNo)
        {
            EnrollmentRequest obj = new EnrollmentRequest();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<EnrollmentRequest>("[sp_EnrollmentRequest]", new { @Action = "GetByID", @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }

        public EnrollmentRequestList EnrollmentDetailList( int pageIndex, int pageSize,string ApplicationNo = "", string Name = "",string EnrollmentStatus="")
        {

            EnrollmentRequestList list = new EnrollmentRequestList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_EnrollmentRequest]", new {@Action="ViewList", @pageIndex = pageIndex, @pageSize = pageSize, @ApplicationNo = ApplicationNo, @Name = Name, @EnrollmentStatus = EnrollmentStatus }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EnrollmentRequest>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EnrollmentRequestList EnrollmentDetailList1(int pageIndex = 1, int pageSize = 25, string collegeID = "", string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RegistrationNo = "", string session = "")
        {
            //AcademicSession ac = new AcademicSession();
            //int session = ac.GetAcademiccurrentSession().ID;
            EnrollmentRequestList list = new EnrollmentRequestList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_EnrollmentRequest]", new { @Action = "ViewList", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID = collegeID, @EducationTypeID = EducationTypeID, @CourseCategory = CourseCategoryID, @Name = Name, @enrollmentNo = RegistrationNo, @Session = session }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EnrollmentRequest>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EnrollmentRequest ApproveEnrollmentRequest(int id=0,int insertId=0)
        {
            
            EnrollmentRequest obj = new EnrollmentRequest();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<EnrollmentRequest>("[sp_EnrollmentRequest]", new { @Action = "ApproveRequest", @ID = id, @insertId = insertId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<EnrollmentRequest> AllStudentForIDCard(int Collegeid = 0,int CourseCategory=0,string Name = "", string Enrollment="")
        {
            AcademicSession ac = new AcademicSession();
            var Session = ac.GetAcademiccurrentSession().ID;
            List<EnrollmentRequest> obj = new List<EnrollmentRequest>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<EnrollmentRequest>("[sp_EnrollmentRequest]", new { @Action = "ViewenrollList", @Collegeid = Collegeid , @CourseCategory = CourseCategory, @session=Session, @Name = Name, @enrollmentNo = Enrollment }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<Commn_master> getcourseMaster(int Id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Commn_master>("[sp_EnrollmentRequest]", new { @Action = "CourseList", @Id = Id }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

    }
    public class EnrollmentRequestList
    {
        public List<EnrollmentRequest> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
