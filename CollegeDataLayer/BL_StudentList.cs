using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataLayer
{
    public class BL_StudentList
    {
        public List<NewStudentList> qlist { get; set; }
        public List<NewStudentListExport> qlistPrint { get; set; }
        public string totalCount { get; set; }
        public string totalCountPrint{ get; set; }
        public int EduTypeID { get; set; }
        public int CourseCategoryID { get; set; }


        public BL_StudentList StudentList(int pageIndex1, int pageSize1, string ApplicationNo = "", string session = "", int IsFeeSubmit = 0, string name = "", int CourseCategory = 0, int Subject = 0, string CollegeID = "", string fromdate = "", string todate = "", string EducationType = "")
        {
            BL_StudentList ObjStudentList = new BL_StudentList();
            int Edu = 0;
            if (EducationType != "")
            {
                Edu = Convert.ToInt32(EducationType);
            }
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                var obj = conn.QueryMultiple("[sp_StudentRegistration_admin]", new
                {
                    @Action = "ListStudent",
                    @ApplicID = ApplicationNo,
                    @session = session,
                    @IsFeeSubmit = IsFeeSubmit,
                    @CourseCategory = CourseCategory,
                    @FirstName = name,
                    @PageIndex = pageIndex1,
                    @PageSize = pageSize1,
                    @collegeid = CollegeID,
                    @SubjectID = Subject
                    ,
                    @fromdate = fromdate,
                    @todate = todate,
                    @EducationType = Edu

                }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    ObjStudentList.qlist = obj.Read<NewStudentList>().ToList();
                    ObjStudentList.totalCount = obj.Read<string>().FirstOrDefault();

                }
                return ObjStudentList;
            }
        }

        public BL_StudentList StudentListPrint(int pageIndex1, int pageSize1, string ApplicationNo = "", string session = "", int IsFeeSubmit = 0, string name = "", int CourseCategory = 0, int Subject = 0, string CollegeID = "", string fromdate = "", string todate = "", int EducationType = 0)
        {
            BL_StudentList ObjStudentListPrint = new BL_StudentList();
            int Edu = 0;
            if (EducationType != 0)
            {
                Edu = Convert.ToInt32(EducationType);
            }
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                var obj = conn.QueryMultiple("[sp_StudentRegistration_admin]", new
                {
                    @Action = "ListStudentAllRecord",
                    @ApplicID = ApplicationNo,
                    @session = session,
                    @IsFeeSubmit = IsFeeSubmit,
                    @CourseCategory = CourseCategory,
                    @FirstName = name,
                    @PageIndex = pageIndex1,
                    @PageSize = pageSize1,
                    @collegeid = CollegeID,
                    @SubjectID = Subject
                  ,
                    @fromdate = fromdate,
                    @todate = todate,
                    @EducationType = Edu
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    ObjStudentListPrint.qlistPrint = obj.Read<NewStudentListExport>().ToList();
                    ObjStudentListPrint.totalCountPrint = obj.Read<string>().FirstOrDefault();

                }
                return ObjStudentListPrint;
            }
        }

      



    }
    public class NewStudentList
    {      
        public int pageIndex1 { get; set; }
        public int pageSize1 { get; set; }
        public int CourseCategoryId { get; set; }
    
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Title { get; set; }
        public string EncriptedID { get; set; }
        public string Message { get; set; }
        public string CastCategort { get; set; }
        public string Fees { get; set; }

        public int IsFeeSubmit { get; set; }
        public string FeeStatus { get; set; }
        public bool Status { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Session { get; set; }
        public string Course { get; set; }
        public string Name { get; set; }
        public string adddate { get; set; }

    }

    public class NewStudentListExport
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Name { get; set; }
        public string NameInHindi { get; set; }
        public string DOB { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Session { get; set; }
        public string FeeStatus { get; set; }
        public string Course { get; set; }
        public string adddate { get; set; }
        public string BloodGroup { get; set; }
        public string CurrentAddress { get; set; }        
        public string PA_Address { get; set; }        
        public string FatherName { get; set; }
        public string FatherNameInHindi { get; set; }
        public string FatherQualification { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string MotherName { get; set; }
        public string MotherNameInHindi { get; set; }
        public string MotherQualification { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherEmail { get; set; }
        public string AdmisitionCategory { get; set; }
        public string EducationType { get; set; }
        public string CourseCategory { get; set; }
        public string Gender { get; set; }
        public string IsFeeSubmit { get; set; }
        public string MotherMobile { get; set; }     
       
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string ishandicapped { get; set; }
        public string is_ncc_candidate { get; set; }
                    
        public string previous_qua_id { get; set; }
     
        public string IsSports { get; set; }
        public string IsStaff { get; set; }
        public string studentcaste { get; set; }
    }
    public class OtherSubjectModel
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string HeadingName { get; set; }
        public string SubjectTypeID { get; set; }
    }

    public class StudentSubjectVM
    {
        public int SubjectID { get; set; }
        public int SubjectTypeID { get; set; }
    }
    public class BL_student_formcomplete
    {
        public bool isdoccomplete { get; set; }
        public bool isqua_complete { get; set; }
        public bool ischoice_complete { get; set; }
        public bool isfeesubmitt { get; set; }

        public bool IsAdmissionfee { get; set; }
        public bool IsApplied { get; set; }
        public int IsDocVerify { get; set; }
        public string IsAppliedDate { get; set; }
        public string IsDocVerifyDate { get; set; }
        public string IsfeesubmitDate { get; set; }
        public List<OtherSubjectModel> GetOtherSubjects(int courseId, int semesterId)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<OtherSubjectModel>(
                    "GetOtherSubjects",
                    new { @CourseID = courseId, @SemesterID = semesterId },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return obj;
            }
        }

        public List<OtherSubjectModel> GetSavedSubjects(int studentId, int semesterId)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<OtherSubjectModel>(
                    "sp_GetSavedSubjects",
                    new { @studentId = studentId, @SemesterID = semesterId },
                    commandType: CommandType.StoredProcedure
                ).ToList();

                return obj;
            }
        }
        public BL_student_formcomplete sp_st_check_details(string ApplicationNo = "", string session = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                var obj = conn.Query<BL_student_formcomplete>("[sp_st_check_details]", new { @applicationno = ApplicationNo, @sesssionid = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public BL_student_formcomplete CheckAdmission_details(int SessionID = 0)
        {
            int SID = 0;

            if (ClsLanguage.GetCookies("NBStID") != null)
            {
                SID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
            }

            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                var obj = conn.Query<BL_student_formcomplete>("[sp_st_check_Admission_details]", new
                {
                    @Action = "StudentDetail",
                    @SID = SID,
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
    }
}
