using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class Recruitment
    {
        public int id { get; set; }
        public int sid { get; set; }
        public decimal percenatge { get; set; }
        public int coursecategoryid { get; set; }
        public int educationTypeID { get; set; }
        public int CasteCategory { get; set; }
        public int sessionid { get; set; }
        public bool is_affiliated { get; set; }
        public int collegeid { get; set; }
        public int StreamCategoryID { get; set; }
        public DateTime adddate { get; set; }
        public int choicetable_id { get; set; }
        public int till_reaming_seat { get; set; }
        public bool ishandicapped { get; set; }
        public int counsellingno { get; set; }
        public int flag { get; set; }
        public string StudentName { get; set; }
        public string coursecategotyName { get; set; }
        public string CasteCategoryName { get; set; }
        public string StreamCategoryName { get; set; }
        public string CollegeName { get; set; }
        public string Session { get; set; }
        public string ApplicationNo { get; set; }
        public string StudentCasteCategoryName { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
        public int carryseat_casteid { get; set; }
        public string seatType { get; set; }
        public string waitingno { get; set; }
        public bool IsApplied { get; set; }

        public string EncriptedID { get; set; }
        public string EncriptedIDcourseyearid { get; set; }
        public int IsDocVerify { get; set; }// 0 pending,1 accecpt or verify,2 reject
        public string BloodGroup { get; set; }
        public string CurrentAddress { get; set; }
        public string PA_Address { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string rejectreason { get; set; }
        public string rejectdate { get; set; }
        public string IsAppliedDate { get; set; }
        public string IsDocVerifyDate { get; set; }
        public bool IsAdmissionFee { get; set; }
        public string IsfeesubmitDate { get; set; }
        public string FatherName { get; set; }
        public string IsDocVerifyDatenew { get; set; }
        public string Faculty { get; set; }
        public int RollNoId { get; set; }
        public string RollNo { get; set; }
        public string CurrentYear { get; set; }
        public string Semster { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string banktrxid { get; set; }
        public string Fees { get; set; }
        public string YearName { get; set; }
        //public string Enrollmentno { get; set; }
        public string incomecertificate { get; set; }
        public string migrationcertificate { get; set; }
        public string Subsidiary1_subject { get; set; }
        public string Subsidiary2_subject { get; set; }
        public string Compulsory1_subject { get; set; }
        public string Compulsory2_subject { get; set; }

        public string ReservationType { get; set; }
        public string Name { get; set; }
        public string BackSubject { get; set; }
        public string ExamStatus { get; set; }
        public string EnrollmentNo { get; set; }
        public string HounorsSubject { get; set; }
        public string Subsidiary1 { get; set; }
        public string Subsidiary2 { get; set; }
        public string Compulsory1 { get; set; }
        public string Compulsory2 { get; set; }
        public string Row { get; set; }
        public int IsBack { get; set; }
        public int Result { get; set; }
        public string rownumber { get; set; }
        public bool IsRegistrationFee { get; set; }

        //-- New fields added on 05/12/2019 by neeraj request by shahrukh
        public string FatherQualification { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string MotherName { get; set; }
        public string MotherQualification { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherEmail { get; set; }
        public string Paper1 { get; set; }
        public string Paper2 { get; set; }
        public string Paper3 { get; set; }
        public string Paper4 { get; set; }
        public string Paper5 { get; set; }
        public string Paper6 { get; set; }
        public string Paper7 { get; set; }
        public string Paper8 { get; set; }
        public string Paper9 { get; set; }
        public string Paper10 { get; set; }
        public string DOB { get; set; }
        public string application_feesubmit { get; set; }

        public int currentcourseyear_id { get; set; }
        public string ResultName { get; set; }
        public string honscenter { get; set; }
        public string subsidiarycenter { get; set; }
        public int courseyearid { get; set; }
        public string electivesubjectname { get; set; }
        public string electivesubjectname_2 { get; set; }
        public int educationtype { get; set; }
        public int studentyear { get; set; }
        public string CounsellingNo1 { get; set; }
        public string educationtype1 { get; set; }
        public BackStudentList BackStudentList(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string CourseCategoryID = "", string CourseYearID = "", string EducationTypeID = "", string Enrollmentno = "")
        {
            BackStudentList list = new BackStudentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (CollegeID == null && CollegeID == "")
                {
                    CollegeID = "0";
                }
                if (EducationTypeID == "11")
                {
                    var obj = conn.QueryMultiple("[sp_GetBackStudentListUG]", new { @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @SessionID = session, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @Enrollmentno = Enrollmentno }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                }
                else if (EducationTypeID == Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB).ToString())
                {
                    var obj = conn.QueryMultiple("[sp_GetBackStudentListLLB1]", new { @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @SessionID = session, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @Enrollmentno = Enrollmentno }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                }
                else if (EducationTypeID == Convert.ToInt32(CommonSetting.Commonid.EducationtypeBED).ToString())
                {
                    var obj = conn.QueryMultiple("[sp_GetBackStudentListBed]", new { @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @SessionID = session, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @Enrollmentno = Enrollmentno }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                }
                else if (EducationTypeID == "13")
                {
                    var obj = conn.QueryMultiple("[sp_GetBackStudentListBCA]", new { @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @SessionID = session, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @Enrollmentno = Enrollmentno }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                }
                else if (EducationTypeID == Convert.ToInt32(CommonSetting.Commonid.EducationtypePG).ToString())
                {
                    var obj = conn.QueryMultiple("[sp_GetBackStudentListPG]", new { @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @SessionID = session, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @Enrollmentno = Enrollmentno }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                }
            }
            return list;
        }

        //public string enrollmentno { get; set; }
        public RecruitmentList RecruitmentwaitingList(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string addmissionCategoryid = "")
        {

            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport]", new { Action = "waitinglistreport", PageIndex = pageIndex1, pageSize = pageSize1, @coursetype = coursetype, @subject = subject, @session = session, @AddmissionCategoryid = addmissionCategoryid }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public List<Recruitment> RecruitmentwaitinglistforPrint(string coursetype = "", string session = "")
        {

            List<Recruitment> list = new List<Recruitment>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                list = conn.Query<Recruitment>("[Sp_RecruitmentReport]", new { Action = "Printwaitinglist", @coursetype = coursetype, @session = session }, commandType: CommandType.StoredProcedure).ToList();

            }
            return list;
        }
        //public RecruitmentList studentdetailList(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "",string application="", string ApplicationStatus = "")
        //{

        //     RecruitmentList list = new RecruitmentList();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College]", new { Action = "report", PageIndex = pageIndex1, pageSize = pageSize1, @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType, @application = application, @ApplicationStatus = ApplicationStatus }, commandType: CommandType.StoredProcedure);
        //        list.qlist = obj.Read<Recruitment>().ToList();
        //        list.totalCount = obj.Read<string>().FirstOrDefault();
        //    }
        //    return list;
        //}
        public List<Recruitment> RecruitmentdetailforPrint(string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "")
        {
            List<Recruitment> list = new List<Recruitment>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                list = conn.Query<Recruitment>("[Sp_RecruitmentReport]", new { Action = "Print", @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType }, commandType: CommandType.StoredProcedure).ToList();
            }
            return list;
        }
        public AcademicSession GetSessionBYID(int session)
        {
            AcademicSession objdata = new AcademicSession();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<AcademicSession>("Sp_RecruitmentReport", new { Action = "Session", @session = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }
        public Recruitment RecruitmentStartMerit(int sessionid = 0, int CasteCategory = 0, int coursecategory = 0, int AddmissionCategoryid = 0, int counsellingno = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Recruitment>("sp_Recruitment_merit ", new
                {
                    @sessionid = sessionid,
                    @CasteCategory = CasteCategory,
                    @coursecategory = coursecategory,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @counsellingno = counsellingno
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Recruitment RecruitmentStartothercast(int sessionid = 0, int CasteCategory = 0, int coursecategory = 0, int AddmissionCategoryid = 0, int counsellingno = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objcheck = conn.Query<Recruitment>("[sp_recruitment_cutoff]", new
                {
                    @action = "Checkabovecutofflistdone",
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategory,
                    @CasteCategory = CasteCategory,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objcheck.status == false)
                {
                    return objcheck;
                }
                var obj = conn.Query<Recruitment>("sp_Recruitment_other ", new
                {
                    @sessionid = sessionid,
                    @CasteCategory = CasteCategory,
                    @coursecategory = coursecategory,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @counsellingno = counsellingno
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Recruitment RecruitmentStartotherReservation(int sessionid = 0, int CasteCategory = 0, int coursecategory = 0, int AddmissionCategoryid = 0, int counsellingno = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objcheck = conn.Query<Recruitment>("[sp_recruitment_cutoff]", new
                {
                    @action = "Checkabovecutofflistdone",
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategory,
                    @CasteCategory = CasteCategory,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objcheck.status == false)
                {
                    return objcheck;
                }
                var obj = conn.Query<Recruitment>("sp_Recruitment_Quota", new
                {
                    @sessionid = sessionid,
                    @CasteCategory = CasteCategory,
                    @coursecategory = coursecategory,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @counsellingno = counsellingno
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public RecruitmentList Currentcousenllingno(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "")
        {

            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport]", new { Action = "report", PageIndex = pageIndex1, pageSize = pageSize1, @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Recruitment RecruitmentRollback(int sessionid = 0, int coursecategory = 0, int AddmissionCategoryid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Recruitment>("sp_rollback_recruitment ", new
                {
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategory,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Recruitment Recruitmentwaitinglist(int sessionid = 0, int coursecategory = 0, int AddmissionCategoryid = 0, int counsellingno = 0, decimal waitingpercentage = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Recruitment>("sp_Recruitment_Waitinglist", new
                {
                    @sessionid = sessionid,
                    @coursecategory = coursecategory,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @counsellingno = counsellingno,
                    @waitingpercentage = waitingpercentage
                }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public RecruitmentList studentdetailList(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "", string application = "", string ApplicationStatus = "", string CounsellingNo = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College]", new { Action = "report", PageIndex = pageIndex1, pageSize = pageSize1, @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType, @application = application, @ApplicationStatus = ApplicationStatus, @CounsellingNo = CounsellingNo }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public List<Recruitment> RecruitmentdetailforPrintCollege(string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "", string educationTypeID = "", string CounsellingNo1 = "")
        {

            List<Recruitment> list = new List<Recruitment>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                list = conn.Query<Recruitment>("[Sp_RecruitmentReport_College]", new { Action = "Printcollege", @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType, @educationTypeID = educationTypeID, @CounsellingNo = CounsellingNo1 }, commandType: CommandType.StoredProcedure).ToList();

            }
            return list;
        }
        public RecruitmentList RecruitmentdetailList(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "", string Applicationno = "", string CounsellingNo = "", string EducationTypeID = "")
        {

            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport]", new { Action = "reportcollege", PageIndex = pageIndex1, pageSize = pageSize1, @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType, @Applicationno = Applicationno, @CounsellingNo = CounsellingNo, @educationTypeID = EducationTypeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList studentdetailListForVerification(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "", string application = "", string ApplicationStatus = "", string CounsellingNo = "", string FeeStatus = "", string IncomeStatus = "", string varEducationtype = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College]", new
                {
                    Action = "VerificationReport",
                    PageIndex = pageIndex1,
                    pageSize = pageSize1,
                    @coursetype = coursetype,
                    @subject = subject,
                    @session = session,
                    @collegeID = collegeID,
                    @cast = cast,
                    @seatType = seatType,
                    @application = application,
                    @ApplicationStatus = ApplicationStatus,
                    @CounsellingNo = CounsellingNo,
                    @FeeStatus = FeeStatus,
                    @IncomeStatus = IncomeStatus,
                    @EducationType = varEducationtype
                }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();

                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList feesubmittedstudentdetailList(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "", string application = "", /*string ApplicationStatus = "",*/ string CounsellingNo = "", string paymentStatus = "", string CourseYearID = "", string EducationType = "", string Registration = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College_new]", new
                {
                    Action = "FeeSubmitstudentReport",
                    PageIndex = pageIndex1,
                    pageSize = pageSize1,
                    @coursetype = coursetype,
                    @subject = subject,
                    @session = session,
                    @collegeID = collegeID,
                    @cast = cast,
                    @seatType = seatType,
                    @application = application, /*@ApplicationStatus = ApplicationStatus,*/
                    @CounsellingNo = CounsellingNo,
                    @paymentStatus = paymentStatus,
                    @CourseYearID = CourseYearID,
                    @educationtype = EducationType,
                    @registrationno = Registration
                },

                    commandTimeout: 120545, commandType: CommandType.StoredProcedure);

                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList feesubmittedstudentdetailListExport(int pageIndex1 = 1, int pageSize1 = 25, string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "", string application = "", /*string ApplicationStatus = "",*/ string CounsellingNo = "", string paymentStatus = "", string CourseYearID = "", string EducationType = "", string Registration = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College]", new { Action = "FeeSubmitstudentReport", PageIndex = pageIndex1, pageSize = pageSize1, @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType, @application = application, /*@ApplicationStatus = ApplicationStatus,*/ @CounsellingNo = CounsellingNo, @paymentStatus = paymentStatus, @CourseYearID = CourseYearID, @EducationType = EducationType, @registrationno = Registration }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailList(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeList", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }

        public RecruitmentList ExamFeeStudentdetailList_LLB(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListLLB", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailList_BED(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListBED", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailList_BCA(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListBCA", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailList_PG(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListPG", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }

        public RecruitmentList ExamFeeStudentdetailListexport(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListExcel", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();

            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailListexport_LLB(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListExcelLLB", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();

            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailListexport_BCA(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListExcelBCA", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();

            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailListexport_Bed(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListExcelBED", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();

            }
            return list;
        }
        public RecruitmentList ExamFeeStudentdetailListexport_PG(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", int IsBackStudent = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "ExamFeeListExcelPG", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = IsBackStudent }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();

            }
            return list;
        }


        public RecruitmentList CollegeStudentdetailList(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_CollegeStudentdetail]", new { @Action = "ExamFeeList", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        // Amit kr Yadav
        #region Student 
        public List<SelectListItem> GetStudentList(int CourseCategoryID, int CollegeID, int SessionID, int SubjectID)
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                if (CourseCategoryID > 0 && SessionID > 0 && SubjectID > 0)
                {
                    RecruitmentList list = new RecruitmentList();
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College]", new { Action = "FeeSubmitstudentReport", PageIndex = 1, pageSize = 50000, @coursetype = CourseCategoryID, @subject = SubjectID, @session = SessionID, @collegeID = 0, @cast = 0, @seatType = 2, @application = "", @ApplicationStatus = 0, @CounsellingNo = 0 }, commandType: CommandType.StoredProcedure);
                        list.qlist = obj.Read<Recruitment>().ToList();
                        list.totalCount = obj.Read<string>().FirstOrDefault();
                    }

                    if (list.qlist != null)
                    {
                        foreach (var p in list.qlist)
                        {
                            items.Add(new SelectListItem { Value = p.sid.ToString(), Text = p.StudentName.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        public List<Recruitment> GetAllStudentList()
        {
            List<Recruitment> list = new List<Recruitment>();
            try
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[Sp_College_AllStudent]", new { Action = "AllStudent" }, commandType: CommandType.StoredProcedure);
                    list = obj.Read<Recruitment>().ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public StudentAttendanceAdd InsertStudentRollNo(string Faculty, int Sid, string No)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                var obj = conn.Query<StudentAttendanceAdd>("[sp_Student_RollNo]", new
                {
                    @Action = "Insert",
                    @Sid = Sid,
                    @No = No,
                    @Faculty = Faculty
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<Recruitment> GetStudentRollNoList()
        {
            List<Recruitment> list = new List<Recruitment>();
            try
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[sp_Student_RollNo]", new { Action = "GetDetail" }, commandType: CommandType.StoredProcedure);
                    list = obj.Read<Recruitment>().ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<SelectListItem> GetStudentBySubjectList(int CollegeID, int SessionID, int SubjectID)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            List<Recruitment> list = new List<Recruitment>();
            try
            {
                if (CollegeID > 0 && SessionID > 0 && SubjectID > 0)
                {
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("[sp_Student_ChoiceSubjectnew]", new { Action = "GetStudentBySubject", @CollegeId = CollegeID, @SessionId = SessionID, @SubjectId = SubjectID }, commandType: CommandType.StoredProcedure);
                        list = obj.Read<Recruitment>().ToList();
                    }
                    if (list != null)
                    {
                        foreach (var p in list)
                        {
                            items.Add(new SelectListItem { Value = p.sid.ToString(), Text = p.StudentName.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        #endregion
        public List<Recruitment> CourseYear(int id)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<Recruitment>("sp_CourseYear", new { @CourseID = id }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }
        }
        public RecruitmentList viewfeestudentdetailList(int sid = 0)
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_RecruitmentReport_College_new]", new { Action = "FeeSubmitstudentReport_viewone", @sid = sid }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
            }
            return list;
        }
        public RecruitmentList Collegewise_CenterList(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_college]", new { @Action = "ExamFeeList", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = BackStatus }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public RecruitmentList Collegewise_CenterList2(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "", string CenterType = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_college2]", new { @Action = "ExamFeeList", @pageIndex = pageIndex1, @pageSize = pageSize1, @CollegeID = CollegeID, @session = session, @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @Subject = Subject, @CourseYearID = CourseYearID, @Application = Application, @ApplicationStatus = ApplicationStatus, @paymentStatus = paymentStatus, @IsBackStudent = BackStatus, @CenterType = CenterType }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Recruitment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
                for (int i = 0; i < list.qlist.Count; i++)
                {
                    if (list.qlist[i].courseyearid == 8)
                    {
                        list.qlist[i].Subsidiary1 = "Money and Banking";
                        list.qlist[i].Subsidiary2 = "Economics Development of India";
                    }
                }



            }
            return list;
        }
        public RecruitmentList sp_Collegewise_CenterList_rollnowise(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "", string CenterType = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string newaction = string.Empty;
                if (BackStatus == "0")//EducationType == "11" &&
                {
                    newaction = "ExamFeeList";
                }
                else if (EducationType == "40" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "41" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "12" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "13" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "11" && BackStatus == "1")
                {
                    newaction = "BackExamUG";
                }
                if (EducationType == "40")//b.ed
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_bed]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else if (EducationType == "12")// P.G..
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_PG]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else if (EducationType == "41")//l.l.b.
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_llb]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else if (EducationType == "13")// BCA
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_BCA]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

               commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                    for (int i = 0; i < list.qlist.Count; i++)
                    {
                        if (list.qlist[i].courseyearid == 8)
                        {
                            list.qlist[i].Subsidiary1 = "Money and Banking";
                            list.qlist[i].Subsidiary2 = "Economics Development of India";
                        }
                    }
                }





            }
            return list;
        }
        public RecruitmentList sp_Collegewise_CenterList_rollnowise_new(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "", string CenterType = "")
        {
            RecruitmentList list = new RecruitmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string newaction = string.Empty;
                if (BackStatus == "0")//EducationType == "11" &&
                {
                    newaction = "ExamFeeList";
                }
                else if (EducationType == "40" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "41" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "12" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "13" && BackStatus == "1")
                {
                    newaction = "BackExam";
                }
                else if (EducationType == "11" && BackStatus == "1")
                {
                    newaction = "BackExamUG";
                }
                if (EducationType == "40")//b.ed
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_bed]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else if (EducationType == "41")//l.l.b.
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_llb]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else if (EducationType == "12")// P.G..
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_PG]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else if (EducationType == "13")// BCA
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise_BCA]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

                   commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();


                }
                else
                {
                    var obj = conn.QueryMultiple("[sp_Collegewise_CenterList_rollnowise]", new
                    {
                        @Action = newaction,
                        @pageIndex = pageIndex1,
                        @pageSize = pageSize1,
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EducationType,
                        @CourseCategoryID = CourseCategoryID,
                        @Subject = Subject,
                        @CourseYearID = CourseYearID,
                        @Application = Application,
                        @ApplicationStatus = ApplicationStatus,
                        @paymentStatus = paymentStatus,
                        @IsBackStudent = BackStatus,
                        @CenterType = CenterType
                    },

               commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    list.qlist = obj.Read<Recruitment>().ToList();
                    list.totalCount = obj.Read<string>().FirstOrDefault();
                    for (int i = 0; i < list.qlist.Count; i++)
                    {
                        if (list.qlist[i].courseyearid == 8)
                        {
                            list.qlist[i].Subsidiary1 = "Money and Banking";
                            list.qlist[i].Subsidiary2 = "Economics Development of India";
                        }
                    }
                }





            }
            return list;
        }
        public List<ExamForm> sp_Collegewise_CenterList_subejctlist(int pageIndex1 = 1, int pageSize1 = 25, string CollegeID = "", string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "", string CenterType = "")
        {
            List<ExamForm> list = new List<ExamForm>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string newaction = string.Empty;
                var obj = conn.QueryMultiple("[sp_checklist_timetable]", new
                {
                    @Action = newaction,

                    @CollegeID = CollegeID,
                    @session = session,
                    @EducationType = EducationType,
                    @CourseCategoryID = CourseCategoryID,
                    @Subject = Subject,
                    @CourseYearID = CourseYearID,
                    @Application = Application,
                    @ApplicationStatus = ApplicationStatus,
                    @paymentStatus = paymentStatus,
                    @IsBackStudent = BackStatus,
                    @CenterType = CenterType
                },

                commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                list = obj.Read<ExamForm>().ToList();


            }
            return list;
        }

    }
    public class RecruitmentList
    {
        public List<Recruitment> qlist { get; set; }
        //public List<Login> qlist_new { get; set; }
        public string totalCount { get; set; }
    }
    public class BackStudentList
    {
        public List<Recruitment> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class tbl_recruitment_cutoff
    {
        public int ID { get; set; }
        public int CounsellingNo { get; set; }
        public string adddate { get; set; }
        public string coursecategoryid { get; set; }
        public string sessionid { get; set; }
        public string caste { get; set; }
        public string totalCount { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
        public tbl_recruitment_cutoff Currentcousenllingno(int sessionid, int AddmissionCategoryid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<tbl_recruitment_cutoff>("[sp_recruitment_cutoff]", new
                {
                    @action = "getcurrentCounsellingNo",
                    @sessionid = sessionid,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public tbl_recruitment_cutoff waitinglistCurrentcousenllingno(int sessionid, int AddmissionCategoryid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<tbl_recruitment_cutoff>("[sp_recruitment_cutoff]", new
                {
                    @action = "waitinglistgetcurrentCounsellingNo",
                    @sessionid = sessionid,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public int CheckexitCounsellingNo(int sessionid, int coursecategoryid, int CasteCategory, int AddmissionCategoryid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<int>("[sp_recruitment_cutoff]", new
                {
                    @action = "CheckexitCounsellingNo",
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategoryid,
                    @CasteCategory = CasteCategory,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public bool Checkcutoff_listdone(int sessionid, int coursecategoryid, int CasteCategory, int AddmissionCategoryid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objcheck = conn.Query<tbl_recruitment_cutoff>("[sp_recruitment_cutoff]", new
                {
                    @action = "Checkabovecutofflistdone",
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategoryid,
                    @CasteCategory = CasteCategory,
                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return objcheck.status;
            }

        }
        public bool Checkcutoff_finallist_buttonshow(int sessionid, int coursecategoryid, int AddmissionCategoryid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objcheck = conn.Query<tbl_recruitment_cutoff>("[sp_recruitment_cutoff]", new
                {
                    @action = "finallist_buttonshow",
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategoryid,

                    @AddmissionCategoryid = AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return objcheck.status;
            }

        }
        public bool Checkcutoff_waitinglist_buttonshow(int sessionid, int coursecategoryid, int AddmissionCategoryid, int CounsellingNo)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objcheck = conn.Query<tbl_recruitment_cutoff>("[sp_recruitment_cutoff_waiting]", new
                {
                    @action = "checkwaitingdone",
                    @sessionid = sessionid,
                    @coursecategoryid = coursecategoryid,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @CounsellingNo = CounsellingNo
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return objcheck.status;
            }

        }
        public List<tbl_recruitment_cutoff> GetCounsellingNo()
        {
            int session = new AcademicSession().GetAcademiccurrentSession().ID;
            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
            List<tbl_recruitment_cutoff> obj = new List<tbl_recruitment_cutoff>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<tbl_recruitment_cutoff>("[Sp_RecruitmentReport_College]", new { Action = "CounsellingNoBind", @session = session, @AddmissionCategoryid = AddmissionCategoryid }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
    }
}



