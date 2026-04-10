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
    public class StudentLogin
    {
        public Login Login(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                objLogin = conn.Query<Login>("ProcST_StudentLogin", new { @ApplicationNo = objLogin.ApplicationNo, @Password = objLogin.Password, @IPAddress = ip }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (objLogin.Id <= 0)
                {
                    objLogin.status = false;

                }
                return objLogin;
            }
        }
        public Login BasicDetail(string ApplicationNo)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<Login>("[sp_StudentRegistration]", new { @Action = "GetByID", @ApplicID = ApplicationNo }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }

        public ChangePassword ChangePassword(ChangePassword obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<ChangePassword>("sp_ChangeStudentPassword", new { ApplicationID = obj.ApplicationID, CurrentPassword = obj.CurrentPassword, NewPassword = obj.NewPassword, Status = "ChangePassword" }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return ob;
            }
        }
        public static bool logout(string id = "0")
        {
            ExpireAllCookies();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Login>("sp_ChangeStudentPassword", new { ApplicationID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
        public Login Student_registration(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_StudentRegistration]", new
                {
                    @Id = 0,
                    @FirstName = objLogin.FirstName,
                    @MiddleName = objLogin.MiddleName,
                    @LastName = objLogin.LastName,
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @CastCategory = objLogin.CastCategory,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = objLogin.MobileNo,
                    @Email = objLogin.Email,
                    @CurrentAddress = objLogin.CurrentAddress,
                    @CA_PinCode = objLogin.CA_PinCode,
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = objLogin.CA_City,
                    @PA_Address = objLogin.PA_Address,
                    @PA_PinCode = objLogin.PA_PinCode,
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = objLogin.PA_City,
                    @FatherName = objLogin.FatherName,
                    @FatherQualification = objLogin.FatherQualification,
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = objLogin.FatherMobile,
                    @FatherEmail = objLogin.FatherEmail,
                    @MotherName = objLogin.MotherName,
                    @MotherQualification = objLogin.MotherQualification,
                    @MotherOccupation = objLogin.MotherOccupation,
                    @MotherEmail = objLogin.MotherEmail,
                    @MotherMobile = objLogin.MotherMobile,
                    @session = objLogin.session,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsQualifying = 0,
                    @IsLogin = 0,
                    @Action = "Insert",
                    @ApplicID = 0,
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @aadharno = objLogin.aadharno,
                    @previous_qua_id = objLogin.prevoiustreamid,
                    @FirstNameInHindi = objLogin.FirstNameInHindi,
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = objLogin.ReligonOther,
                    @FatherNameInHindi = objLogin.FatherNameInHindi,// N"'+ objLogin.FatherNameInHindi+',
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @ModifyBy = objLogin.InsertedBy,
                    @IPAddress = ip,
                    @prevoiusboardid = objLogin.boardtype,
                    @is_GEW = objLogin.is_GEW,
                    @Password = objLogin.Password,
                    @U_password = objLogin.U_password


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login Student_DetailsUpdatemanually(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                if (objLogin.hid == 0)
                {
                    objLogin.status = false;
                    objLogin.Message = "Network Error !!";
                    return objLogin;
                }
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_StudentRegistration]", new
                {
                    @Id = objLogin.hid,
                    @FirstName = objLogin.FirstName,
                    @MiddleName = objLogin.MiddleName,
                    @LastName = objLogin.LastName,
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = objLogin.MobileNo,
                    @Email = objLogin.Email,
                    @CurrentAddress = objLogin.CurrentAddress,
                    @CA_PinCode = objLogin.CA_PinCode,
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = objLogin.CA_City,
                    @PA_Address = objLogin.PA_Address,
                    @PA_PinCode = objLogin.PA_PinCode,
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = objLogin.PA_City,
                    @FatherName = objLogin.FatherName,
                    @FatherQualification = objLogin.FatherQualification,
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = objLogin.FatherMobile,
                    @FatherEmail = objLogin.FatherEmail,
                    @MotherName = objLogin.MotherName,
                    @MotherQualification = objLogin.MotherQualification,
                    @MotherOccupation = objLogin.MotherOccupation,
                    @MotherEmail = objLogin.MotherEmail,
                    @MotherMobile = objLogin.MotherMobile,
                    @AdmisitionCategory = objLogin.AdmisitionCategory,
                    @CourseType = objLogin.CourseType,
                    @EducationType = objLogin.EducationType,
                    @CourseCategory = objLogin.CourseCategory,
                    @IsLogin = 0,
                    @Action = "Update",
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @aadharno = objLogin.aadharno,
                    @FirstNameInHindi = objLogin.FirstNameInHindi,
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = objLogin.ReligonOther,
                    @FatherNameInHindi = objLogin.FatherNameInHindi,
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @ModifyBy = objLogin.InsertedBy,
                    @IPAddress = ip,
                    @CastCategory = objLogin.CastCategory,
                    @prevoiusboardid = objLogin.boardtype,
                    @ishandicapped = objLogin.ishandicapped,
                    @isex_service_man = objLogin.isex_service_man,
                    @is_ncc_candidate = objLogin.is_ncc_candidate,
                    @IsSports = objLogin.IsSports,
                    @IsStaff = objLogin.IsStaff,
                    @is_GEW = objLogin.is_GEW,
                    @Session = objLogin.session,
                    @Value = objLogin.value,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login Student_DetailsUpdate(Login objLogin)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                if (objLogin.hid == 0)
                {
                    objLogin.status = false;
                    objLogin.Message = "Network Error !!";
                    return objLogin;
                }
                string ip = CommonMethod.GetIPAddress();
                if (objLogin.DOB == null)
                {
                    objLogin.DOB = "";
                }
                else
                {
                    objLogin.DOB = DateTime.ParseExact(objLogin.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<Login>("[sp_College_StudentDetailsUpdate]", new
                {
                    @Id = objLogin.hid,
                    @FirstName = objLogin.FirstName,
                    @MiddleName = objLogin.MiddleName,
                    @LastName = objLogin.LastName,
                    @Gender = objLogin.Gender,
                    @DOB = objLogin.DOB,
                    @BloodGroup = objLogin.BloodGroup,
                    @MobileNo = objLogin.MobileNo,
                    @Email = objLogin.Email,
                    @CurrentAddress = objLogin.CurrentAddress,
                    @CA_PinCode = objLogin.CA_PinCode,
                    @CA_Country = objLogin.CA_Country,
                    @CA_State = objLogin.CA_State,
                    @CA_City = objLogin.CA_City,
                    @PA_Address = objLogin.PA_Address,
                    @PA_PinCode = objLogin.PA_PinCode,
                    @PA_Country = objLogin.PA_Country,
                    @PA_State = objLogin.PA_State,
                    @PA_City = objLogin.PA_City,
                    @FatherName = objLogin.FatherName,
                    @FatherQualification = objLogin.FatherQualification,
                    @FatherOccupation = objLogin.FatherOccupation,
                    @FatherMobile = objLogin.FatherMobile,
                    @FatherEmail = objLogin.FatherEmail,
                    @MotherName = objLogin.MotherName,
                    @MotherQualification = objLogin.MotherQualification,
                    @MotherOccupation = objLogin.MotherOccupation,
                    @MotherEmail = objLogin.MotherEmail,
                    @MotherMobile = objLogin.MotherMobile,
                    //@AdmisitionCategory = objLogin.AdmisitionCategory,
                    //@CourseType = objLogin.CourseType,
                    //@EducationType = objLogin.EducationType,
                    //@CourseCategory = objLogin.CourseCategory,                   
                    @IsLogin = 0,
                    @Action = "Update",
                    @stphoto = objLogin.stphoto,
                    @stsign = objLogin.stsign,
                    @title = objLogin.title,
                    @ftile = objLogin.Ftitle,
                    @Nationality = objLogin.Nationality,
                    @Religion = objLogin.Religion,
                    @MotherTongue = objLogin.MotherTongue,
                    @aadharno = objLogin.aadharno,
                    @FirstNameInHindi = objLogin.FirstNameInHindi,
                    @MiddleNameInHindi = objLogin.MiddleNameInHindi,
                    @LastNameInHindi = objLogin.LastNameInHindi,
                    @ReligonOther = objLogin.ReligonOther,
                    @FatherNameInHindi = objLogin.FatherNameInHindi,
                    @MotherNameInHindi = objLogin.MotherNameInHindi,
                    @ModifyBy = objLogin.InsertedBy,
                    @IPAddress = ip,
                    @Session = objLogin.session,
                    @Value= objLogin.value,
                    //@is_GEW=objLogin.is_GEW,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;


            }
        }
        public Login BasicDetailByID(int id = 0)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Login>("[sp_CollegeEmployee_Registration]", new { @Action = "GetByStudentID", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Login ByIDgethounors_subjectid(int id = 0, int sessionid = 0)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Login>("[sp_CollegeEmployee_Registration]", new { @Action = "ManualSubjectChoiceGet", @sid = id, @sessionid = sessionid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Login BasicDetailwithrecruitment(string ApplicationNo)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Login>("[sp_StudentAdmissionManualy]", new { @Action = "GetByID", @ApplicID = ApplicationNo }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Login BasicDetailwithrecruitmentByID(int id = 0)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Login>("[sp_StudentAdmissionManualy]", new { @Action = "GetByStudentID", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Login CollegeSeatDetail(int Subject = 0, string CollegeID = "", string CastCategory = "", int coursecategory = 0, int AddmissionCategoryid = 0, string ishandicapped = "")
        {
            AcademicSession ac = new AcademicSession();
            int session = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Login>("[sp_student_ChangeCollege]", new { @Action = "RemainingSeat", @session = session, @Collegeid = CollegeID, @studentcategory = CastCategory, @StreamCategoryID = Subject, @coursecategory = coursecategory, @AddmissionCategoryid = AddmissionCategoryid, @ishandicapped = 0 }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Login IncomeCertificateDetailID(int id = 0, int sessionid = 0)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Login>("[Sp_IncomeCertification_Verify]", new { @Action = "StudentDetail", @ID = id, @SessionID = sessionid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Login migrationCertificateDetailID(int id = 0, int sessionid = 0)
        {
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Login>("[Sp_mirationCertification_Verify]", new { @Action = "StudentDetail", @ID = id, @SessionID = sessionid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }

    }


    public class Login
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "LoginId is ApplicationNo")]
        public string ApplicationNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FirstNameInHindi { get; set; }
        public string MiddleNameInHindi { get; set; }
        public string LastNameInHindi { get; set; }

        public int Gender { get; set; }
        public string DOB { get; set; }
        public int CastCategory { get; set; }
        public int BloodGroup { get; set; }


        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string CA_PinCode { get; set; }
        public int CA_Country { get; set; }
        public int CA_State { get; set; }
        public string CA_City { get; set; }
        public string PA_Address { get; set; }
        public string PA_PinCode { get; set; }
        public int PA_Country { get; set; }
        public int PA_State { get; set; }
        public string PA_City { get; set; }
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
        public string MotherMobile { get; set; }
        public string GargianName { get; set; }
        public string GargianContactNo { get; set; }
        public string GargianRelation { get; set; }
        public int session { get; set; }
        public int value { get; set; }
        public int Subject { get; set; }
        public int AdmisitionCategory { get; set; }
        public int CourseType { get; set; }
        public int EducationType { get; set; }
        public int CourseCategory { get; set; }

        public int Stream { get; set; }
        
        public int IsLogin { get; set; }
        public int IsFeeSubmit { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool status { get; set; }
        public string Message { get; set; }
        public bool rememberMe { get; set; }
        public string stphoto { get; set; }
        public string stsign { get; set; }
        public int Ftitle { get; set; }
        public int title { get; set; }

        public int Nationality { get; set; }
        public int Religion { get; set; }
        public int MotherTongue { get; set; }
        public bool is_ncc_candidate { get; set; }
        public bool ishandicapped { get; set; }
        public bool isex_service_man { get; set; }
        public string aadharno { get; set; }
        public int prevoiustreamid { get; set; }
        public int previous_qua_id { get; set; }
        public bool issame_stream { get; set; }
        public string ReligonOther { get; set; }
        public bool IsSports { get; set; }
        public bool IsStaff { get; set; }
        public int InsertedBy { get; set; }
        public string Ipaddress { get; set; }
        public string txtCaptcha { get; set; }
        public int hid { get; set; }
        public string Name { get; set; }
        public int boardtype { get; set; }
        public string CollegeName { get; set; }
        public string College { get; set; }
        public string alloatSubject { get; set; }
        public int prevoiusboardid { get; set; }
        public string EncriptedID { get; set; }
        public string Msg { get; set; }
        public bool is_GEW { get; set; }
        public bool Isadmissionfee { get; set; }
        public int hounors_subjectid { get; set; }
        public int countseat { get; set; }
        public byte[] U_password { get; set; }
        public string CourseName { get; set; }
        public string StreamName { get; set; }
        public bool IsAdmissionFee2 { get; set; }
        public int sid { get; set; }
        public int incomecertificate_iseligible { get; set; }
        public string Reason { get; set; }
        public string incomecertificate { get; set; }
        public string migrationcertificate { get; set; }
        public int migrationcertificate_iseligible { get; set; }
        //-- New fileds added on 04/12/2019 by neeraj 
        public string StudentName { get; set; }
        public string percenatge { get; set; }
        public int counsellingno { get; set; }
        public bool IsApplied { get; set; }
        public string IsAppliedDate { get; set; }
        public string StudentCasteCategoryName { get; set; }
        public string coursecategotyName { get; set; }
        public string StreamCategoryName { get; set; }
        public string CasteCategoryName { get; set; }
        public int IsDocVerify { get; set; }// 0 pending,1 accecpt or verify,2 reject
        //public bool IsAdmissionFee { get; set; }
        public string IsfeesubmitDate { get; set; }
        public string Enrollmentno { get; set; }
        public int CourseYearID { get; set; }
        public string BloodGroupName { get; set; }
        public string GenderName { get; set; }
        public string NationalityName { get; set; }
        public string ReligionName { get; set; }

    }


    public class CheckListClass
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "LoginId is ApplicationNo")]
        public string ApplicationNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FirstNameInHindi { get; set; }
        public string MiddleNameInHindi { get; set; }
        public string LastNameInHindi { get; set; }

        public int Gender { get; set; }
        public string DOB { get; set; }
        public int CastCategory { get; set; }
        public int BloodGroup { get; set; }
        public int SubjectType { get; set; }

        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string CA_PinCode { get; set; }
        public int CA_Country { get; set; }
        public int CA_State { get; set; }
        public string CA_City { get; set; }
        public string PA_Address { get; set; }
        public string PA_PinCode { get; set; }
        public int PA_Country { get; set; }
        public int PA_State { get; set; }
        public string PA_City { get; set; }
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
        public string MotherMobile { get; set; }
        public string GargianName { get; set; }
        public string GargianContactNo { get; set; }
        public string GargianRelation { get; set; }
        public int session { get; set; }
        public string sessionname { get; set; }

        public string YearName { get; set; }
        public int AdmisitionCategory { get; set; }
        public int CourseType { get; set; }
        public int EducationType { get; set; }
        public int CourseCategory { get; set; }
        public int Stream { get; set; }
        public int IsLogin { get; set; }
        public int IsFeeSubmit { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool status { get; set; }
        public string Message { get; set; }
        public bool rememberMe { get; set; }
        public string stphoto { get; set; }
        public string stsign { get; set; }
        public int Ftitle { get; set; }
        public int title { get; set; }

        public int Nationality { get; set; }
        public int Religion { get; set; }
        public int MotherTongue { get; set; }
        public bool is_ncc_candidate { get; set; }
        public bool ishandicapped { get; set; }
        public bool isex_service_man { get; set; }
        public string aadharno { get; set; }
        public int prevoiustreamid { get; set; }
        public int previous_qua_id { get; set; }
        public bool issame_stream { get; set; }
        public string ReligonOther { get; set; }
        public bool IsSports { get; set; }
        public bool IsStaff { get; set; }
        public int InsertedBy { get; set; }
        public string Ipaddress { get; set; }
        public string txtCaptcha { get; set; }
        public int hid { get; set; }
        public string Name { get; set; }
        public int boardtype { get; set; }
        public string CollegeName { get; set; }
        public string alloatSubject { get; set; }
        public int prevoiusboardid { get; set; }
        public string EncriptedID { get; set; }
        public string Msg { get; set; }
        public bool is_GEW { get; set; }
        public bool Isadmissionfee { get; set; }
        public int hounors_subjectid { get; set; }
        public int countseat { get; set; }
        public byte[] U_password { get; set; }
        public string CourseName { get; set; }
        public string StreamName { get; set; }
        public bool IsAdmissionFee2 { get; set; }
        public int sid { get; set; }
        public int incomecertificate_iseligible { get; set; }
        public string Reason { get; set; }
        public string incomecertificate { get; set; }
        //-- New fileds added on 04/12/2019 by neeraj 
        public string StudentName { get; set; }
        public string percenatge { get; set; }
        public int counsellingno { get; set; }
        public bool IsApplied { get; set; }
        public string IsAppliedDate { get; set; }
        public string StudentCasteCategoryName { get; set; }
        public string coursecategotyName { get; set; }
        public string StreamCategoryName { get; set; }
        public string CasteCategoryName { get; set; }
        public int IsDocVerify { get; set; }// 0 pending,1 accecpt or verify,2 reject
        public string ExamStatus { get; set; }
        public string IsfeesubmitDate { get; set; }
        public string Enrollmentno { get; set; }
        public int CourseYearID { get; set; }
        public string BloodGroupName { get; set; }
        public string GenderName { get; set; }
        public string NationalityName { get; set; }
        public string ReligionName { get; set; }
        public string RollNo { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string Compulsory2Subject { get; set; }
        public string electivesubjectname { get; set; }
        public string electivesubjectname_2 { get; set; }
        public string Paper_C1_back { get; set; }
        public string Paper_C2_back { get; set; }
        public string Paper_C3_back { get; set; }
        public string Paper_C4_back { get; set; }
        public string Paper_C5_back { get; set; }
        public string Paper_C6_back { get; set; }
        public string Paper_C7_back { get; set; }
        public string Paper_EPC1_back { get; set; }
        public string Paper_EPC2_back { get; set; }
        public string Paper_EPC3_back { get; set; }
        public int coursecategoryid { get; set; }
        public int StreamCategoryID { get; set; }
        public string Paper_C1_back_code { get; set; }
        public string Paper_C2_back_code { get; set; }
        public string Paper_C3_back_code { get; set; }
        public string Paper_C4_back_code { get; set; }
        public string Paper_C5_back_code { get; set; }
        public string Paper_C6_back_code { get; set; }
        public string Paper_C7_back_code { get; set; }

        //public List<CheckListClass> checkList_subject { get; set; }
        public List<CheckListClass> checkList { get; set; }
        public List<CheckListClass> ExamCheckList(int session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int Examtype = 0, int CollegeID = 0, int SubjectType = 0)
        {
            List<CheckListClass> obj = new List<CheckListClass>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<CheckListClass>("sp_Collegewise_checklist_rollnowise", new { @CollegeID = CollegeID, @session = session, @EducationType = EduTypeID, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @IsBackStudent = Examtype, @SubjectType = SubjectType }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();

                return obj;
            }
        }
        public List<CheckListClass> ExamCheckList_temp(int session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int Examtype = 0, int CollegeID = 0, int SubjectType = 0)
        {
            List<CheckListClass> obj = new List<CheckListClass>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<CheckListClass>("[sp_Collegewise_CenterCheckList_temp]", new { @CollegeID = CollegeID, @session = session, @EducationType = EduTypeID, @CourseCategoryID = CourseCategoryID, @CourseYearID = CourseYearID, @IsBackStudent = Examtype, @SubjectType = SubjectType }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();

                return obj;
            }
        }
        public List<CheckListClass> ExamCheckList_temp_rollnowise(int session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int Examtype = 0, int CollegeID = 0, int SubjectType = 0)
        {
            List<CheckListClass> obj = new List<CheckListClass>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                if (EduTypeID == 40)// B.Ed.
                {
                    obj = conn.Query<CheckListClass>("sp_Collegewise_checklist_rollnowise_BED", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                }
                else if (EduTypeID == 41)// l.l.b..
                {
                    obj = conn.Query<CheckListClass>("sp_Collegewise_checklist_rollnowise_LLB", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                }
                else if (EduTypeID == 12)// p.g.
                {
                    obj = conn.Query<CheckListClass>("sp_Collegewise_checklist_rollnowise_PG", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                }
                else
                {
                    obj = conn.Query<CheckListClass>("sp_Collegewise_checklist_rollnowise", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure).ToList();
                }

                return obj;
            }
        }

        public PrintExamForm_ehceklist ExamCheckList_temp_rollnowise_new(int session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int Examtype = 0, int CollegeID = 0, int SubjectType = 0)
        {
            //List<CheckListClass> obj = new List<CheckListClass>();
            PrintExamForm_ehceklist objdata = new PrintExamForm_ehceklist();

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                if (EduTypeID == 40)// B.Ed.
                {

                    var obj = conn.QueryMultiple("sp_Collegewise_checklist_rollnowise_BED", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<CheckListClass>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                else if (EduTypeID == 41)// l.l.b..
                {

                    var obj = conn.QueryMultiple("sp_Collegewise_checklist_rollnowise_LLB", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<CheckListClass>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                else if (EduTypeID == 12)// p.g..
                {

                    var obj = conn.QueryMultiple("sp_Collegewise_checklist_rollnowise_PG", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<CheckListClass>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                else if (EduTypeID == 11)// u.g.
                {

                    var obj = conn.QueryMultiple("sp_Collegewise_checklist_rollnowise", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<CheckListClass>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                else if (EduTypeID == 13)// b.c.a.
                {

                    var obj = conn.QueryMultiple("sp_Collegewise_checklist_rollnowise_BCA", new
                    {
                        @CollegeID = CollegeID,
                        @session = session,
                        @EducationType = EduTypeID,
                        @CourseCategoryID = CourseCategoryID,
                        @CourseYearID = CourseYearID,
                        @IsBackStudent = Examtype,
                        @SubjectType = SubjectType
                    }, commandTimeout: 120545, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<CheckListClass>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                else
                {
                    List<CheckListClass> listA = new List<CheckListClass>();
                    List<ExamForm> listB = new List<ExamForm>();
                   
                    objdata.Studentlist = listA;
                    objdata.subjectlist = listB;
                }

                return objdata;
            }
        }
    }
    public class PrintExamForm_ehceklist
    {
        public List<CheckListClass> Studentlist { get; set; }
        public List<ExamForm> subjectlist { get; set; }
    }
    public class ChangePassword
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }
        [StringLength(20, ErrorMessage = "Password must be at least {5} characters long.", MinimumLength = 5)]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        public string ApplicationID { get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int CollegeID { get; set; }
    }

    public class FeesSubmit
    {
        public string Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Title { get; set; }
        public string Session { get; set; }
        public string Message { get; set; }
        [Required(ErrorMessage = "Select Reservation Category First!")]
        public string CastCategory { get; set; }
        public string Fees { get; set; }
        public string Course { get; set; }
        public string IsFeeSubmit { get; set; }
        public string FeeStatus { get; set; }
        public bool Status { get; set; }
        public string Requestdata { get; set; }
        public string dRequestdata { get; set; }
        public string requesttime { get; set; }
        public string PGstatus { get; set; }
        [Required(ErrorMessage = "Enter Transaction ID  First!")]
        public string banktrxid { get; set; }
        public string clienttrxid { get; set; }
        public string amount { get; set; }
        public string feeamount { get; set; }
        public string gst { get; set; }
        public string commission { get; set; }
        [Required(ErrorMessage = "Select Payment Type First!")]
        public string paymode { get; set; }
        public string banktxndate { get; set; }
        public string Reason { get; set; }

        public string apitxnid { get; set; }
        public int SID { get; set; }
        public int collegeID { get; set; }
        public bool recstatus { get; set; }
        public int Insertedby { get; set; }
        public string CastCategort { get; set; }
        public string Msg { get; set; }
        public int TID { get; set; }
        public decimal Feesval { get; set; }
        public decimal AdmissionFees { get; set; }
        public string Admissionbanktrxid { get; set; }
        public string Admissionpaymode { get; set; }
        public string AdmisionTransactionId { get; set; }
        public bool isadmission { get; set; }
        public string SessionName { get; set; }
        public bool IsExamFee { get; set; }

        public FeesSubmit Feessub(int StID = 0)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                AcademicSession ac = new AcademicSession();
                int Sission = ac.GetAcademiccurrentSession().ID;
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "StudentFeesDetail", @Id = StID.ToString(), @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Convert.ToInt32(ObjFees.Id) <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubStudent()
        {
            FeesSubmit ObjFees = new FeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                string ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                CommonMethod cmn = new CommonMethod();
                ObjFees = conn.Query<FeesSubmit>("[sp_StudentRegistration]", new { @Action = "FeesSubmit", @ApplicID = ApplicationNo.ToString(), @Id = StID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (Convert.ToInt32(ObjFees.Id) <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        public FeesSubmit FeessubManualAd(FeesSubmit ob)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            string ip = CommonMethod.GetIPAddress();
            DateTime now = DateTime.Now;
            string Order_Number = "";
            string Order_NumberAdmission = "";
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            if (ob.paymode == "Cash")
            {
                ob.banktrxid = Order_Number;
            }
            Order_NumberAdmission = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            if (ob.Admissionpaymode == "Cash")
            {
                ob.Admissionbanktrxid = Order_NumberAdmission;
            }


            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (ob.SID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }

                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_ManualAdmission]", new
                {
                    @Action = "FeesSubmit",
                    @paystatus = "SUCCESS",
                    @banktrxid = ob.banktrxid,
                    @paymode = ob.paymode,
                    @CastCategory = ob.CastCategory,
                    @Collegeid = ob.collegeID,
                    @SID = ob.SID,
                    @session = ob.Session,
                    @Fees1 = 0,
                    @Insertedby = ob.Insertedby,
                    @IPAddress = ip,
                    @TransactionId = Order_Number,
                    @AdmissionFees = ob.AdmissionFees,
                    @Admissionbanktrxid = ob.Admissionbanktrxid,
                    @Admissionpaymode = ob.Admissionpaymode,
                    @AdmisionTransactionId = Order_NumberAdmission


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();


                //if (ObjFees.Status == false)
                //{
                //    goto loop;
                //}
                if (ObjFees.isadmission == true)
                {
                    goto loop;
                }

                return ObjFees;
            }
        }
        public FeesSubmit FeessubVocational(FeesSubmit ob)
        {
            FeesSubmit ObjFees = new FeesSubmit();
            string ip = CommonMethod.GetIPAddress();
            DateTime now = DateTime.Now;
            string Order_Number = "";
            loop:
            Order_Number = CommonMethod.RandomNumber(100000, 999999) + now.ToString("MdHHmmyyssfff");
            if (ob.paymode == "Cash")
            {
                ob.banktrxid = Order_Number;
            }

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (ob.SID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }

                ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_vocationalCourse]", new
                {
                    @Action = "FeesSubmit",
                    @paystatus = "SUCCESS",
                    @banktrxid = ob.banktrxid,
                    @paymode = ob.paymode,
                    @SID = ob.SID,
                    @session = ob.Session,
                    @Fees1 = ob.Feesval,
                    @Insertedby = ob.Insertedby,
                    @IPAddress = ip,
                    @TransactionId = Order_Number,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();


                if (ObjFees.TID == 1)
                {
                    goto loop;
                }
                return ObjFees;
            }
        }
        public int DocumentStatus(int StID = 0)
        {
            AcademicSession ac = new AcademicSession();
            int Sission = ac.GetAcademiccurrentSession().ID;

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ObjFees = conn.Query<int>("[sp_student_registrationfee_vocationalCourse]", new { @Action = "VerifyStatus", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
        public FeesSubmit FeeSubmitStatus(int StID = 0)
        {
            AcademicSession ac = new AcademicSession();
            int Sission = ac.GetAcademiccurrentSession().ID;

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ObjFees = conn.Query<FeesSubmit>("[sp_student_registrationfee_vocationalCourse]", new { @Action = "FeeDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
        public List<SelectListItem> GetPaymentType()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                items.Add(new SelectListItem { Value = "Cash", Text = "Cash" });
                items.Add(new SelectListItem { Value = "Cheque/DD", Text = "Cheque/DD" });
                items.Add(new SelectListItem { Value = "NEFT", Text = "NEFT" });

            }
            catch (Exception ex)
            {
            }
            return items;
        }
    }

    public class ElegibilityCreteria
    {
        public int ID { get; set; }
        public int EducationType { get; set; }
        public int CourseCategoryID { get; set; }
        public int QualificationTypeID { get; set; }
        public decimal Percentage { get; set; }
        public DateTime createdate { get; set; }
        public string IPAddress { get; set; }
        public int insertBy { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }

        public ElegibilityCreteria getdetail(string app = "", int qual = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<ElegibilityCreteria>("[sp_StudentRegistration]", new { @Action = "checkpercentage", @ApplicID = app, @qual = qual }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public ElegibilityCreteria getdetailofper(string app = "")
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<ElegibilityCreteria>("[sp_StudentRegistration]", new { @Action = "percentage", @ApplicID = app }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<ElegibilityCreteria> getdetailofper1(string app = "")
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                var obj = conn.Query<ElegibilityCreteria>("[sp_StudentRegistration]", new { @Action = "percentage", @ApplicID = app }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }





    }
    public class AdmissionFeesSubmit
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Title { get; set; }
        public string Session { get; set; }
        public string Message { get; set; }
        public string CastCategort { get; set; }
        public string Fees { get; set; }
        public string Course { get; set; }
        public string IsFeeSubmit { get; set; }
        public string FeeStatus { get; set; }
        public string FeeStatus1 { get; set; }
        public bool Status { get; set; }
        public string CollegeName { get; set; }
        public string Stream { get; set; }
        public string PaymentType { get; set; }
        public string PaymentHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Expires { get; set; }
        public string NuthNum { get; set; }
        public string mid { get; set; }
        public string mkey { get; set; }
        public string headname { get; set; }
        public decimal amount { get; set; }
        public int Collegeid { get; set; }
        public int coursecategoryid { get; set; }
        public int sessionid { get; set; }
        public int Examtype { get; set; }
        public string Requestdata { get; set; }
        public string dRequestdata { get; set; }
        public string requesttime { get; set; }
        public string PGstatus { get; set; }
        public string banktrxid { get; set; }
        public string clienttrxid { get; set; }
        public string feeamount { get; set; }
        public string gst { get; set; }
        public string commission { get; set; }
        public string paymode { get; set; }
        public string banktxndate { get; set; }
        public string Reason { get; set; }

        public string apitxnid { get; set; }

        public decimal surcharge { get; set; }
        public string email { get; set; }
        public string mobileno { get; set; }
        public string name { get; set; }
        public int CastCategory { get; set; }
        public int streamcategoryid { get; set; }




        public AdmissionFeesSubmit FeesDetails(int StID = 0)
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                if (StID == 0)
                {
                    ObjFees.Status = false;
                    ObjFees.Message = "Network Error !!";
                    return ObjFees;
                }
                //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
                int session = new AcademicSession().GetAcademiccurrentSession().ID;
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesDetail", @SID = StID, @SessionID = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees == null)
                {
                    AdmissionFeesSubmit dssadf = new AdmissionFeesSubmit();
                    ObjFees = dssadf;
                }
                else
                {
                    if (ObjFees.Id <= 0)
                    {
                        ObjFees.Status = false;

                    }
                }
                return ObjFees;
            }
        }
        public AdmissionFeesSubmit FeesDetailsapplicationno(string applicationno = "")
        {
            AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesDetailbyapplicationno", @Applicationno = applicationno }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (ObjFees.Id <= 0)
                {
                    ObjFees.Status = false;

                }
                return ObjFees;
            }
        }
        //public AdmissionFeesSubmit getmidviasid(int StID = 0)
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        // int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
        //        if (StID == 0)
        //        {
        //            ObjFees.Status = false;
        //            ObjFees.Message = "Network Error !!";
        //            return ObjFees;
        //        }
        //        //int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
        //        int session = new AcademicSession().GetAcademiccurrentSession().ID;
        //        CommonMethod cmn = new CommonMethod();
        //        string ip = CommonMethod.GetIPAddress();
        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "getmidviasid", @SID = StID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        if (ObjFees.Id <= 0)
        //        {
        //            ObjFees.Status = false;

        //        }
        //        return ObjFees;
        //    }
        //}
        public List<AdmissionFeesSubmit> FeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesstructure", @collegeid = collegeid, @SessionID = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        //public AdmissionFeesSubmit AdmissionFeessub(int StID = 0)
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        if (StID == 0)
        //        {
        //            ObjFees.Status = false;
        //            ObjFees.Message = "Network Error !!";
        //            return ObjFees;
        //        }

        //        int session = new AcademicSession().GetAcademiccurrentSession().ID;
        //        var AddmissionCategoryid = Convert.ToInt32(CommonSetting.Commonid.RegularAdmissionType);
        //        CommonMethod cmn = new CommonMethod();
        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_AdmissionFee]", new { @Action = "AdmissionFeesSubmit", @SID = StID, @SessionID = session, @AddmissionCategoryid = AddmissionCategoryid }, commandType: CommandType.StoredProcedure).FirstOrDefault();

        //        return ObjFees;
        //    }
        //}
        //public AdmissionFeesSubmit FeessubStudentbeforeadmission(AdmissionFeesSubmit obj123)
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_registrationfee_beforeadmission]", new
        //        {
        //            @applicationno = obj123.ApplicationNo,
        //            @Requestdata = obj123.Requestdata,
        //            @dRequestdata = obj123.dRequestdata,
        //            @status = obj123.PGstatus,
        //            @banktrxid = obj123.banktrxid,
        //            @clienttrxid = obj123.clienttrxid,
        //            @amount = obj123.amount,
        //            @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
        //            @gst = (obj123.gst == "" ? "0" : obj123.gst),
        //            @commission = (obj123.commission == "" ? "0" : obj123.commission),
        //            @paymode = obj123.paymode,
        //            @Reason = obj123.Reason,
        //            @banktxndate = obj123.banktxndate,
        //            @apitxnid = obj123.apitxnid,
        //            @responsevia = "browser",
        //            @collegeid = obj123.Collegeid,
        //            @Mid = obj123.mid

        //        }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        ObjFees.Status = ObjFees.Status;
        //        return ObjFees;
        //    }
        //}
        //public AdmissionFeesSubmit FeessubStudentadmission(AdmissionFeesSubmit obj123)
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        int Sission = Convert.ToInt32(ClsLanguage.GetCookies("NBSission"));
        //        CommonMethod cmn = new CommonMethod();
        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
        //        {
        //            @applicationno = obj123.ApplicationNo,
        //            @Requestdata = obj123.Requestdata,
        //            @dRequestdata = obj123.dRequestdata,
        //            @status = obj123.PGstatus,
        //            @banktrxid = obj123.banktrxid,
        //            @clienttrxid = obj123.clienttrxid,
        //            @amount = obj123.amount,
        //            @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
        //            @gst = (obj123.gst == "" ? "0" : obj123.gst),
        //            @commission = (obj123.commission == "" ? "0" : obj123.commission),
        //            @paymode = obj123.paymode,
        //            @Reason = obj123.Reason,
        //            @banktxndate = obj123.banktxndate,
        //            @apitxnid = obj123.apitxnid,
        //            @responsevia = "browser",
        //            @collegeid = obj123.Collegeid

        //        }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        if (ObjFees.Id <= 0)
        //        {
        //            ObjFees.Status = false;
        //        }
        //        return ObjFees;
        //    }
        //}
        //public AdmissionFeesSubmit FeessubStudentadmissionPushresponse(AdmissionFeesSubmit obj123)
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
        //        {
        //            @applicationno = obj123.ApplicationNo,
        //            @Requestdata = obj123.Requestdata,
        //            @dRequestdata = obj123.dRequestdata,
        //            @status = obj123.PGstatus,
        //            @banktrxid = obj123.banktrxid,
        //            @clienttrxid = obj123.clienttrxid,
        //            @amount = obj123.amount,
        //            @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
        //            @gst = (obj123.gst == "" ? "0" : obj123.gst),
        //            @commission = (obj123.commission == "" ? "0" : obj123.commission),
        //            @paymode = obj123.paymode,
        //            @Reason = obj123.Reason,
        //            @banktxndate = obj123.banktxndate,
        //            @apitxnid = obj123.apitxnid,
        //            @responsevia = "Pushresponse"

        //        }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        if (ObjFees.Id <= 0)
        //        {
        //            ObjFees.Status = false;

        //        }
        //        return ObjFees;
        //    }
        //}
        //public AdmissionFeesSubmit FeessubStudentadmissionDoubleverification(AdmissionFeesSubmit obj123)
        //{
        //    AdmissionFeesSubmit ObjFees = new AdmissionFeesSubmit();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {

        //        ObjFees = conn.Query<AdmissionFeesSubmit>("[sp_student_admissionfee]", new
        //        {
        //            @applicationno = obj123.ApplicationNo,
        //            @Requestdata = obj123.Requestdata,
        //            @dRequestdata = obj123.dRequestdata,
        //            @status = obj123.PGstatus,
        //            @banktrxid = obj123.banktrxid,
        //            @clienttrxid = obj123.clienttrxid,
        //            @amount = obj123.amount,
        //            @feeamount = (obj123.feeamount == "" ? "0" : obj123.feeamount),
        //            @gst = (obj123.gst == "" ? "0" : obj123.gst),
        //            @commission = (obj123.commission == "" ? "0" : obj123.commission),
        //            @paymode = obj123.paymode,
        //            @Reason = obj123.Reason,
        //            @banktxndate = obj123.banktxndate,
        //            @apitxnid = obj123.apitxnid,
        //            @responsevia = "DoubleVerificationURl"

        //        }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        if (ObjFees.Id <= 0)
        //        {
        //            ObjFees.Status = false;

        //        }
        //        return ObjFees;
        //    }
        //}
    }
}

