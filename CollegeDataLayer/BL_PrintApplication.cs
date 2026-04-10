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

    public class BL_PrintApplication
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
        public string FeeInINR { get; set; } 
        //-- new properties by neeraj previous year qualification details
        public string board_universityName { get; set; }
        public string Percentage { get; set; }
        public string passingyear { get; set; }
        public string ROllNo { get; set; }
        public string NewNationality { get; set; }
        public string NewReligion { get; set; }
        public bool IsStaff { get; set; }
        public bool IsSports { get; set; }
        public bool is_ncc_candidate { get; set; }
        public bool ishandicapped { get; set; }
        public bool isex_service_man { get; set; }
        public string admissionFee { get; set; }
        public string transactionID { get; set; }
        public string paymentBY { get; set; }
        public string intermediatstream { get; set; }
        public string CollegeName { get; set; }
        public string CourseName { get; set; }
        public string StreamName { get; set; }
        public string ExtendDate { get; set; }
        public string startdate { get; set; }
        public string Msg { get; set; }
        public string Reason { get; set; }
        public int DocumentStatus { get; set; }
        public string IsDocVerifyDate { get; set; }
        public bool is_GEW { get; set; }
        public bool IsAdmissionFee2 { get; set; }
        public int migrationcertificate_iseligible { get; set; }
        public string migrationVerifyByDate { get; set; }
        public string migrationRejectReaseon { get; set; }

        public BL_PrintAllRecord GetAppLicationData()
        {
            BL_PrintAllRecord objdata = new BL_PrintAllRecord();
            int id = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
            string SessionID = ClsLanguage.GetCookies("NBSission");
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_StudentRegistration", new { @Action = "PrintAddmissionByID", @Id = id, @session = SessionID }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.ObjApplication = obj.Read<BL_PrintApplication>().FirstOrDefault();
                   
                    objdata.ObjQualification = obj.Read<BL_PrntStudentQualification>().ToList();
                    objdata.ObjDocument = obj.Read<BL_PrintDocument>().ToList();
                    objdata.ObjIntermidataeSubjects = obj.Read<IntermidataeSubjects>().ToList();
                    objdata.objChoices = obj.Read<PrintChoices>().ToList();
                }
                return objdata;
            }
        }
        public BL_PrintAllRecord GetAppLicationDataAdmin(int id=0)
        {
            BL_PrintAllRecord objdata = new BL_PrintAllRecord();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_StudentRegistration", new { @Action = "PrintAddmissionByID", @Id = id, @session = 0 }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.ObjApplication = obj.Read<BL_PrintApplication>().FirstOrDefault();
                    objdata.ObjQualification = obj.Read<BL_PrntStudentQualification>().ToList();
                    objdata.ObjDocument = obj.Read<BL_PrintDocument>().ToList();
                    objdata.ObjIntermidataeSubjects = obj.Read<IntermidataeSubjects>().ToList();
                    objdata.objChoices = obj.Read<PrintChoices>().ToList();
                    objdata.objfeesubmit = stlogin.Feessub(id);
                    objdata.objPrintRecipt = PritFee.GetPaymentRecipt(id);
                }
                return objdata;
            }
        }
        public BL_PrintAllRecord AdmissionDetail(int SessionID = 0,int SID=0)
        {
            BL_PrintAllRecord objdata = new BL_PrintAllRecord();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

              var  obj = conn.QueryMultiple("[Sp_Admission_Process_document]", new
                {
                    @Action = "StudentDetail",
                    @SID = SID,
                    @SessionID = SessionID
                }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.ObjApplication = obj.Read<BL_PrintApplication>().FirstOrDefault();

                    objdata.ObjQualification = obj.Read<BL_PrntStudentQualification>().ToList();
                    objdata.ObjDocument = obj.Read<BL_PrintDocument>().ToList();
                    objdata.ObjIntermidataeSubjects = obj.Read<IntermidataeSubjects>().ToList();
                    //objdata.objChoices = obj.Read<PrintChoices>().ToList();
                }
                return objdata;
                
            }
        }
        public BL_PrintApplication CheckStudentAdmission(int SessionID=0, int SID = 0)
        {          
           
            var AddmissionCategoryid =Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
            BL_PrintApplication obj = new BL_PrintApplication();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process_document]", new
                {
                    @Action = "CheckStudent",
                    @SID = SID,
                    @SessionID = SessionID,
                    @AddmissionCategoryid= AddmissionCategoryid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public BL_PrintApplication CheckStudentAddmisionExtendDate(int SessionID = 0, int educationtype = 0, int SID = 0)
        {
           
            var AddmissionCategoryid =Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
            
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

               var obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process_document]", new
                {
                    @Action = "CheckExtendDate",                 
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid,
                   @educationtype= educationtype,
                   @SID = SID
               }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public BL_PrintApplication CheckStudentAddmisionStartDate(int SessionID = 0, int educationtype = 0, int SID = 0)
        {
                    
            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process_document]", new
                {
                    @Action = "CheckStartDate",
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @educationtype = educationtype,
                    @SID = SID,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public BL_PrintApplication UpdateAdmission( int SID = 0)
        {
            AcademicSession ac = new AcademicSession();
            int SessionID = ac.GetAcademiccurrentSession().ID;
           
            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
            //if (SID==id)
            //{
                BL_PrintApplication obj = new BL_PrintApplication();
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    CommonMethod cmn = new CommonMethod();

                    obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process_document]", new
                    {
                        @Action = "UpdateAdmission",
                        @SID = SID,
                        @SessionID = SessionID,
                        @AddmissionCategoryid= AddmissionCategoryid
                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return obj;
                }
           // }
            //else
            //{
            //    BL_PrintApplication obj = new BL_PrintApplication();
            //    obj.Status = false;
            //    obj.Msg = "Something Wrong Happen!!!";
            //    return obj;
            //}
                   }
        public BL_PrintApplication CheckStudentApplied(int SessionID = 0, int SID = 0)
        {
           
            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<BL_PrintApplication>("[Sp_Admission_Process_document]", new
                {

                    @Action = "CheckApplied",
                    @SID=SID,
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid,
                   
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public BL_PrintAllRecord GetAppLicationDataAdmincollege(int id = 0)
        {
            BL_PrintAllRecord objdata = new BL_PrintAllRecord();

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("cl_student_datashow", new { @Action = "PrintAddmissionByID", @Id = id, @session = 0 }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.ObjApplication = obj.Read<BL_PrintApplication>().FirstOrDefault();
                    objdata.ObjQualification = obj.Read<BL_PrntStudentQualification>().ToList();
                    objdata.ObjDocument = obj.Read<BL_PrintDocument>().ToList();
                    objdata.ObjIntermidataeSubjects = obj.Read<IntermidataeSubjects>().ToList();

                }
                return objdata;
            }
        }
        public BL_PrintApplication VerifyStudent( int SID = 0,int CollegeId=0)
        {
            AcademicSession ac = new AcademicSession();
            int SessionID = ac.GetAcademiccurrentSession().ID;

            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
           
            BL_PrintApplication obj = new BL_PrintApplication();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[cl_Admission_Process]", new
                {
                    @Action = "VerifyStudent",
                    @SID = SID,
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @IsDocVerifyBy= CollegeId,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
            
        }
        public BL_PrintApplication VerifyStudentreject(int SID = 0, int CollegeId = 0,string reason="")
        {
            AcademicSession ac = new AcademicSession();
            int SessionID = ac.GetAcademiccurrentSession().ID;
            
            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);

            BL_PrintApplication obj = new BL_PrintApplication();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[cl_Admission_Process]", new
                {
                    @Action = "RejectVerifyStudent",
                    @SID = SID,
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @IsDocVerifyBy = CollegeId,
                    @reason= reason
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public BL_PrintApplication IncomeVerifyStudent(int SID = 0, int CollegeId = 0,int SessionID=0)
        {
            BL_PrintApplication obj = new BL_PrintApplication();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[Sp_IncomeCertification_Verify]", new
                {
                    @Action = "VerifyStudent",
                    @ID = SID,
                    @SessionID = SessionID,
                    @incomeVerifyBy = CollegeId,
                    @IPAddress = ip
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public BL_PrintApplication IncomeVerifyStudentreject(int SID = 0, int CollegeId = 0, string reason = "", int SessionID = 0)
        {
            string ip = CommonMethod.GetIPAddress();
            BL_PrintApplication obj = new BL_PrintApplication();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[Sp_IncomeCertification_Verify]", new
                {
                    @Action = "RejectVerifyStudent",
                    @ID = SID,
                    @SessionID = SessionID,
                    @incomeVerifyBy = CollegeId,
                    @IPAddress = ip,
                    @reason = reason
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public BL_PrintApplication CheckStudentVerify(int SessionID = 0, int SID = 0)
        {

            var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<BL_PrintApplication>("[cl_Admission_Process]", new
                {

                    @Action = "CheckVerify",
                    @SID = SID,
                    @SessionID = SessionID,
                    @AddmissionCategoryid = AddmissionCategoryid,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public BL_PrintApplication migrationVerifyStudent(int SID = 0, int CollegeId = 0, int SessionID = 0)
        {
            BL_PrintApplication obj = new BL_PrintApplication();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[Sp_mirationCertification_Verify]", new
                {
                    @Action = "VerifyStudent",
                    @ID = SID,
                    @SessionID = SessionID,
                    @migrationVerifyBy = CollegeId,
                    @IPAddress = ip
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public BL_PrintApplication migrationVerifyStudentreject(int SID = 0, int CollegeId = 0, string reason = "", int SessionID = 0)
        {
            string ip = CommonMethod.GetIPAddress();
            BL_PrintApplication obj = new BL_PrintApplication();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<BL_PrintApplication>("[Sp_mirationCertification_Verify]", new
                {
                    @Action = "RejectVerifyStudent",
                    @ID = SID,
                    @SessionID = SessionID,
                    @migrationVerifyBy = CollegeId,
                    @IPAddress = ip,
                    @reason = reason
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

    }
    public struct IntermidataeSubjects
    {
        public string SubjectName { get; set; }
        public string SubjectPercentage { get; set; }
        public string TotalMarks { get; set; }
        public string MarksObtain { get; set; }
    }
    public class BL_PrintDocument
    {
        public string DocumentType { get; set; }
        public string DocStatus { get; set; }     
        public string IsUpload { get; set; }
        public string FileName { get; set; }
    }
    public class BL_PrntStudentQualification
    {
        public string Qualication { get; set; }
        public string Board { get; set; }
        public string RollNo { get; set; }
        public string Percentage { get; set; }
        public string PassingYear { get; set; }
        public string DocumentURl { get; set; }
        public string boardtype { get; set; }
    }

    public class PrintChoices
    {
        public string CollegeName { get; set; }
        public string hounors_subject { get; set; }
        public string Subsidiary1_subject { get; set; }
        public string Subsidiary2_subject { get; set; }
        public string Compulsory1_subject { get; set; }
        public string Compulsory2_subject { get; set; }
    }
    public class BL_PrintAllRecord
    {
        public List<BL_PrintDocument> ObjDocument { get; set; }
        public List<BL_PrntStudentQualification> ObjQualification { get; set; }
        public BL_PrintApplication ObjApplication { get; set; }
        public List<IntermidataeSubjects> ObjIntermidataeSubjects { get; set; }
        public List<PrintChoices> objChoices { get; set; }
        public FeesSubmit objfeesubmit { get; set; }
        public BL_PrintRecipt objPrintRecipt { get; set; }
    }

    
}
