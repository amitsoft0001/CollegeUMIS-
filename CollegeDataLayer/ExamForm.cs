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
    public class ExamForm
    {
        public int Currentyear_courseyarid { get; set; }
        public string Registrationyear { get; set; }
        public int electivesubjectid { get; set; }
        public string StudentName { get; set; }
        public string StudentNameHindi { get; set; }        
        public string CollegeName { get; set; }
        public string SessionName { get; set; }
        public string DOB { get; set; }
        public int Substreamcategoryid { get; set; }
        public string MobileNo { get; set; }
        public string CurrentAddress { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string sign { get; set; }
        public string photo { get; set; }
        public string streamCategory { get; set; }
        public string CourseCategory { get; set; }
        public string RegistrationNo { get; set; }
        public string YearName { get; set; }
        public string HonoursSubject { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string Compulsory2Subject { get; set; }
        public bool IsExamFee { get; set; }
        public string City { get; set; }
        public string RollNo { get; set; }
        public string ExamCenter { get; set; }
        public int sid { get; set; }
        public int CertificateID { get; set; }
        public bool Status { get; set; }
        public string Division { get; set; }
        public string Title { get; set; }
        public string PTitle { get; set; }
        public string SerialNo { get; set; }
        public string Msg { get; set; }
        public string Email { get; set; }       
        public int coursecategoryid { get; set; }
        public int sessionid { get; set; }
        public int collegeid { get; set; }
        public int StreamCategoryID { get; set; }
        public int courseyearid { get; set; }
        public string adddate { get; set; }
        public bool isappearedearlierfail { get; set; }
        public bool IsApplied { get; set; }
        public string IsAppliedDate { get; set; }
        public int IsDocVerify { get; set; }
        public string IsDocVerifyDate { get; set; }
        public int IsDocVerifyBy { get; set; }
        public string IsDocVerifyIP { get; set; }
        public int IsExamfeesubmit { get; set; }
        public string IsExamfeesubmitdate { get; set; }
        public string rejectreason { get; set; }
        public string rejectdate { get; set; }
        public string EnrollmentNo { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public int ftitle { get; set; }
        public string castecategoryname { get; set; }
        public string permanentAddress { get; set; }
        public string formno { get; set; }
        public string currentyear { get; set; }
        public int isfeesubmitregistration { get; set; }
        public int castecategoryid { get; set; }
        public int EducationType { get; set; }
        public string electivesubjectname { get; set; }
        public string electivesubjectname_2 { get; set; }
        public int electivesubjectid_2 { get; set; }
        public string ApplicationNo { get; set; }
        public string subexamcenter { get; set; }
        public string ExamStartDate { get; set; }
        public string Examyear{ get; set; }
        public string paper { get; set; }
        public string setting { get; set; }
        public string settingTime { get; set; }
        public string Type { get; set; }
        public int Subsidiary1 { get; set; }
        public int Subsidiary2 { get; set; }
        public int Compulsory1 { get; set; }
        public int Compulsory2 { get; set; }
        public string CollegeCode { get; set; }
        public string HONSCENTER { get; set; }
        public string SUBS1CENTER { get; set; }
        public string SUBS2CENTER { get; set; }
        public string COM1CENTER { get; set; }
        public string SubjectName { get; set; }
        public string IsBackSubjectStr { get; set; }
        public int SubjectCodeID { get; set; }
        public string subjectcode { get; set; }
        public int subjecttype { get; set; }
        public int allotid { get; set; }
        public string allotdate { get; set; }
        public string allotno { get; set; }
        public string remarks { get; set; }
        public string Name { get; set; }
        public string certificateType { get; set; }

        public string CourseApplied { get; set; }
        public string sessionname { get; set; }
     
        public string EncriptedID { get; set; }
        public ExamForm StudentDetailForclc(int StID, int Sission)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_Certificate_college]", new { @Action = "StudentDetailforclc", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ExamForm StudentDetailFortc(int StID, int Sission)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_Certificate_college]", new { @Action = "StudentDetailfortc", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ExamForm Savecertificateclc(int sid, int insertby, string CertificateType, int CertificateTypeID, string SerialNo,string Remarks)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_Certificate_college]", new
                {
                    @Action = "PrintCertificate",
                    @sid = sid,
                    @IPAddress = ip,
                    @InsertedBy = insertby,
                    @CertificateType = CertificateType,
                    @CertificateTypeID = CertificateTypeID,
                    @SerialNo = SerialNo,
                    @remarks=Remarks
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ExamForm_Listlist clcReportList(int pageIndex = 1, int pageSize = 25, string collegeID = "", string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RegistrationNo = "", string session = "")
        {
            ExamForm_Listlist objdata = new ExamForm_Listlist();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_Certificate_college]", new { @Action = "AllotViewList", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID = collegeID, @EducationTypeID = EducationTypeID, @CourseCategory = CourseCategoryID, @Name = Name, @enrollmentNo = RegistrationNo, @Session = session }, commandType: CommandType.StoredProcedure);

                if (obj != null)
                {
                    objdata.qlist = obj.Read<ExamForm>().ToList();
                    objdata.totalCount = obj.Read<string>().FirstOrDefault();

                }
            }
            return objdata;
        }
        public ExamForm_Listlist clcAllotList(int pageIndex = 1, int pageSize = 25, string collegeID = "", string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RegistrationNo = "", string session = "")
        {

            ExamForm_Listlist list = new ExamForm_Listlist();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_student_Certificate_college]", new { @Action = "SearchViewList", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID = collegeID, @EducationTypeID = EducationTypeID, @CourseCategory = CourseCategoryID, @Name = Name, @enrollmentNo = RegistrationNo, @Session = session }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<ExamForm>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public ExamForm StudentDetail()
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID")!=null? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")):0);              
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0); 
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom]", new { @Action = "StudentDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
              
                return Obj;
            }
        }
        public ExamForm StudentDetailForAdmitCard()
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
                int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom]", new { @Action = "StudentDetailForAdmitCard", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public int check_examfeebefore_admissionfee(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                EaxmFeesSubmit obj = new EaxmFeesSubmit();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<int>("[sp_check_examfeebefore_admissionfee]", new { @Action = "check_examfeebefore_admissionfee", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @sessionid = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ObjFees;
            }
        }
        //public PrintExamForm GetAppLicationDataForExamFee(int id = 0)
        //{
        //    int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
        //    int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
        //    PrintExamForm objdata = new PrintExamForm();
        //    FeesSubmit stlogin = new FeesSubmit();
        //    BL_PrintRecipt PritFee = new BL_PrintRecipt();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var obj = conn.QueryMultiple("sp_student_ExamFrom", new { @Action = "StudentDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure);
        //        if (obj != null)
        //        {
        //            objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();                   
        //            objdata.objfeesubmit = stlogin.ExamFeesDetail();
        //            objdata.objPrintRecipt = PritFee.GetPaymentReciptExamFee();
        //        }
        //        return objdata;
        //    }
        //}
        public ExamForm StudentDetailForAdmitCardCollege(string RegistrationNo = "",string session="")
        {
            
             int Sission = (session != "" ? Convert.ToInt32(session) : 0);
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom_College]", new { @Action = "StudentDetailForAdmitCard", @RegistrationNo = RegistrationNo, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        //public PrintExamForm_admicard StudentDetailForAdmitCardCollege_mainexam(string EducationTypeID = "", string session = "", string CourseCategoryID = "", string CourseYearID = "", string Examtype = "",string collegeid="",string streamcategoryid="")
        //{
        //    PrintExamForm_admicard objdata = new PrintExamForm_admicard();
        //    int Sission = (session != "" ? Convert.ToInt32(session) : 0);
            
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        CommonMethod cmn = new CommonMethod();
        //        string ip = CommonMethod.GetIPAddress();
        //        if (EducationTypeID == "11")// UG
        //        {
        //              var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard",
        //                   new
        //                   {
        //                       @Action = "StudentDetailForAdmitCardUG",
        //                       @courseCategoryid = CourseCategoryID,@courseyearid = CourseYearID,@session = session,
        //                       @collegeid = collegeid,@streamcategoryid = streamcategoryid
        //                   }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
        //                    if (obj != null)
        //                    {
        //                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
        //                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
        //                    }
        //        }
        //        if (EducationTypeID == "41")// LLB
        //        {
        //            var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_LLB",
        //                  new
        //                  {
        //                      @Action = "StudentDetailForAdmitCard",
        //                      @courseCategoryid = CourseCategoryID,
        //                      @courseyearid = CourseYearID,
        //                      @session = session,
        //                      @collegeid = collegeid,
        //                      @streamcategoryid = streamcategoryid
        //                  }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
        //            if (obj != null)
        //            {
        //                objdata.Studentlist = obj.Read<ExamForm>().ToList();
        //                objdata.subjectlist = obj.Read<ExamForm>().ToList();
        //            }
        //        }
        //        if (EducationTypeID == "13")// BCA
        //        {
        //            var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BCA",
        //                  new
        //                  {
        //                      @Action = "StudentDetailForAdmitCard",
        //                      @courseCategoryid = CourseCategoryID,
        //                      @courseyearid = CourseYearID,
        //                      @session = session,
        //                      @collegeid = collegeid,
        //                      @streamcategoryid = streamcategoryid
        //                  }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
        //            if (obj != null)
        //            {
        //                objdata.Studentlist = obj.Read<ExamForm>().ToList();
        //                objdata.subjectlist = obj.Read<ExamForm>().ToList();
        //            }
        //        }
        //        if (EducationTypeID == "12")// PG
        //        {
        //            var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_PG",
        //                  new
        //                  {
        //                      @Action = "StudentDetailForAdmitCard",
        //                      @courseCategoryid = CourseCategoryID,
        //                      @courseyearid = CourseYearID,
        //                      @session = session,
        //                      @collegeid = collegeid,
        //                      @streamcategoryid = streamcategoryid
        //                  }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
        //            if (obj != null)
        //            {
        //                objdata.Studentlist = obj.Read<ExamForm>().ToList();
        //                objdata.subjectlist = obj.Read<ExamForm>().ToList();
        //            }
        //        }
        //        if (EducationTypeID == "40")// B.Ed
        //        {
        //            var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BED",
        //                  new
        //                  {
        //                      @Action = "StudentDetailForAdmitCard",
        //                      @courseCategoryid = CourseCategoryID,
        //                      @courseyearid = CourseYearID,
        //                      @session = session,
        //                      @collegeid = collegeid,
        //                      @streamcategoryid = streamcategoryid
        //                  }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
        //            if (obj != null)
        //            {
        //                objdata.Studentlist = obj.Read<ExamForm>().ToList();
        //                objdata.subjectlist = obj.Read<ExamForm>().ToList();
        //            }
        //        }
        //        return objdata;
        //    }
        //}

        public PrintExamForm_admicard StudentDetailForAdmitCardCollege_mainexam(string EducationTypeID = "", string session = "", string CourseCategoryID = "", string CourseYearID = "", string Examtype = "", string collegeid = "", string streamcategoryid = "")
        {
            PrintExamForm_admicard objdata = new PrintExamForm_admicard();
            int Sission = (session != "" ? Convert.ToInt32(session) : 0);

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                if (EducationTypeID == "11")// UG
                {

                    if (CourseYearID == "9" || CourseYearID == "6" || CourseYearID == "3")
                    {

                        var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_Comman",
                              new
                              {
                                  @Action = "StudentDetailForAdmitCard",
                                  @courseCategoryid = CourseCategoryID,
                                  @courseyearid = CourseYearID,
                                  @session = session,
                                  @collegeid = collegeid,
                                  @streamcategoryid = streamcategoryid
                              }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                        if (obj != null)
                        {
                            objdata.Studentlist = obj.Read<ExamForm>().ToList();
                            objdata.subjectlist = obj.Read<ExamForm>().ToList();
                        }

                    }
                    else
                    {

                        var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard",
                             new
                             {
                                 @Action = "StudentDetailForAdmitCardUG",
                                 @courseCategoryid = CourseCategoryID,
                                 @courseyearid = CourseYearID,
                                 @session = session,
                                 @collegeid = collegeid,
                                 @streamcategoryid = streamcategoryid
                             }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                        if (obj != null)
                        {
                            objdata.Studentlist = obj.Read<ExamForm>().ToList();
                            objdata.subjectlist = obj.Read<ExamForm>().ToList();
                        }


                    }
                }
                if (EducationTypeID == "41")// LLB
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_LLB",
                          new
                          {
                              @Action = "StudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "13")// BCA
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BCA",
                          new
                          {
                              @Action = "StudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "12")// PG
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_PG",
                          new
                          {
                              @Action = "StudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "40")// B.Ed
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BED",
                          new
                          {
                              @Action = "StudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "42")// BPharma
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BED",
                          new
                          {
                              @Action = "StudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }


                return objdata;
            }
        }




        public PrintExamForm_admicard StudentDetailForAdmitCardCollege_backexam(string EducationTypeID = "", string session = "", string CourseCategoryID = "", string CourseYearID = "", string Examtype = "", string collegeid = "",string streamcategoryid="")
        {

            int Sission = (session != "" ? Convert.ToInt32(session) : 0);
            PrintExamForm_admicard objdata = new PrintExamForm_admicard();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                if (EducationTypeID == "11")//UG
                {
                    if (CourseYearID == "9" || CourseYearID == "6" || CourseYearID == "3")
                    {

                    }

                    else
                    {                
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCardUG",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid,
                              @currentyear = System.DateTime.Now.Year
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                        if (obj != null)
                        {
                            objdata.Studentlist = obj.Read<ExamForm>().ToList();
                            objdata.subjectlist = obj.Read<ExamForm>().ToList();
                        }
                    }

                  
                 }
                if (EducationTypeID == "41")//LLB
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_LLB",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid,
                              @currentyear = System.DateTime.Now.Year
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "13")//BCA
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BCA",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid,
                              @currentyear = System.DateTime.Now.Year
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "12")//PG
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_PG",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid,
                              @currentyear = System.DateTime.Now.Year
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "40")//BEd
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BED",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCard",
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @session = session,
                              @collegeid = collegeid,
                              @streamcategoryid = streamcategoryid,
                              @currentyear = System.DateTime.Now.Year
                          }, commandTimeout: 2222222, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                return objdata;
            }
        }
        public PrintExamForm_admicard StudentDetailForAdmitCardCollege_mainexam_byapplicationo(string EducationTypeID = "", string session = "",string collegeid = "",  string RegistrationNo = "",string CourseCategoryID="",string CourseYearID="")
        {

            int Sission = (session != "" ? Convert.ToInt32(session) : 0);
            PrintExamForm_admicard objdata = new PrintExamForm_admicard();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                if (EducationTypeID == "11")//UG
                {

                    if (CourseYearID=="3" || CourseYearID=="6"|| CourseYearID=="9")
                    {

                        var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_Comman",
                               new
                               {
                                   @Action = "StudentDetailForAdmitCardUGbyapplicationo",
                                   @session = session,
                                   @collegeid = collegeid,
                                   @courseCategoryid = CourseCategoryID,
                                   @courseyearid = CourseYearID,
                                   @RegistrationNo = RegistrationNo
                               }, commandType: CommandType.StoredProcedure);
                        if (obj != null)
                        {
                            objdata.Studentlist = obj.Read<ExamForm>().ToList();
                            objdata.subjectlist = obj.Read<ExamForm>().ToList();


                        }

                    }

                    else
                        { 

                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard",
                           new
                           {
                               @Action = "StudentDetailForAdmitCardUGbyapplicationo",
                               @session = session,
                               @collegeid = collegeid,
                               @courseCategoryid = CourseCategoryID,
                               @courseyearid = CourseYearID,
                               @RegistrationNo = RegistrationNo
                           }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();


                    }
                    }
                }
                if (EducationTypeID == "41")//LLB
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_LLB",
                           new
                           {
                               @Action = "StudentDetailForAdmitCardUGbyapplicationo",
                               @session = session,
                               @collegeid = collegeid,
                               @courseCategoryid = CourseCategoryID,
                               @courseyearid = CourseYearID,
                               @RegistrationNo = RegistrationNo
                           }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();


                    }
                }
                if (EducationTypeID == "13")//BCA
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BCA",
                           new
                           {
                               @Action = "StudentDetailForAdmitCardUGbyapplicationo",
                               @session = session,
                               @collegeid = collegeid,
                               @courseCategoryid = CourseCategoryID,
                               @courseyearid = CourseYearID,
                               @RegistrationNo = RegistrationNo
                           }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();


                    }
                }
                if (EducationTypeID == "12")//PG
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_PG",
                           new
                           {
                               @Action = "StudentDetailForAdmitCardUGbyapplicationo",
                               @session = session,
                               @collegeid = collegeid,
                               @courseCategoryid = CourseCategoryID,
                               @courseyearid = CourseYearID,
                               @RegistrationNo = RegistrationNo
                           }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();


                    }
                }
                if (EducationTypeID == "40")//BED
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BED",
                           new
                           {
                               @Action = "StudentDetailForAdmitCardUGbyapplicationo",
                               @session = session,
                               @collegeid = collegeid,
                               @courseCategoryid = CourseCategoryID,
                               @courseyearid = CourseYearID,
                               @RegistrationNo = RegistrationNo
                           }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();


                    }
                }
                return objdata;
            }
        }
        public PrintExamForm_admicard StudentDetailForAdmitCardCollege_backexam_byapplicationo(string EducationTypeID = "", string session = "", string collegeid = "", string RegistrationNo = "", string CourseCategoryID = "", string CourseYearID = "")
        {

            int Sission = (session != "" ? Convert.ToInt32(session) : 0);
            PrintExamForm_admicard objdata = new PrintExamForm_admicard();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                if (EducationTypeID == "11")//UG
                {
                    if (CourseYearID == "3" || CourseYearID == "6" || CourseYearID == "9")
                    {
                    }

                    else { 
                        var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCardUGbyapplicationo",
                              @session = session,
                              @collegeid = collegeid,
                              @RegistrationNo = RegistrationNo,
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @currentyear = System.DateTime.Now.Year

                          }, commandType: CommandType.StoredProcedure);
                        if (obj != null)
                        {
                            objdata.Studentlist = obj.Read<ExamForm>().ToList();
                            objdata.subjectlist = obj.Read<ExamForm>().ToList();
                        }
                    }
                }
                if (EducationTypeID == "41")//LLB
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_LLB",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCardUGbyapplicationo",
                              @session = session,
                              @collegeid = collegeid,
                              @RegistrationNo = RegistrationNo,
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @currentyear = System.DateTime.Now.Year

                          }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "13")//BCA
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BCA",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCardUGbyapplicationo",
                              @session = session,
                              @collegeid = collegeid,
                              @RegistrationNo = RegistrationNo,
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @currentyear = System.DateTime.Now.Year

                          }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "12")//PG
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_PG",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCardUGbyapplicationo",
                              @session = session,
                              @collegeid = collegeid,
                              @RegistrationNo = RegistrationNo,
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @currentyear = System.DateTime.Now.Year

                          }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                if (EducationTypeID == "40")//BED
                {
                    var obj = conn.QueryMultiple("sp_student_ExamFrom_College_Admitcard_BED",
                          new
                          {
                              @Action = "BAckStudentDetailForAdmitCardUGbyapplicationo",
                              @session = session,
                              @collegeid = collegeid,
                              @RegistrationNo = RegistrationNo,
                              @courseCategoryid = CourseCategoryID,
                              @courseyearid = CourseYearID,
                              @currentyear = System.DateTime.Now.Year

                          }, commandType: CommandType.StoredProcedure);
                    if (obj != null)
                    {
                        objdata.Studentlist = obj.Read<ExamForm>().ToList();
                        objdata.subjectlist = obj.Read<ExamForm>().ToList();
                    }
                }
                return objdata;
            }
        }

        public Login BasicDetail()
        {
            int StID = (ClsLanguage.GetCookies("NBStID") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBStID")) : 0);
            int Sission = (ClsLanguage.GetCookies("NBSission") != null ? Convert.ToInt32(ClsLanguage.GetCookies("NBSission")) : 0);
            Login obj = new Login();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<Login>("[sp_student_ExamFrom]", new { @Action = "StudentFeesDetail", @SID = StID, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public FeesSubmit FeessubStudenttest()
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
                //if (ObjFees.Id <= 0)
                //{
                //    ObjFees.Status = false;

                //}
                return ObjFees;
            }
        }
        public ExamForm StudentDetailForCertificate(string RegistrationNo = "",int Sission=0)
        {           
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {  
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_ExamFrom_College]", new { @Action = "StudentDetail", @RegistrationNo = RegistrationNo, @session = Sission, @CertificateTypeID=1 }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_UG(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.ba) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bsc) || courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bcomm))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findUG_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "StudentExamStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public ExamForm StudentDetailForProvisional(string RegistrationNo = "", int Sission = 0)
        {
            //AcademicSession ac = new AcademicSession();
            //int Sission = ac.GetAcademiccurrentSession().ID ;
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                // move to admin panel
                //Obj = conn.Query<ExamForm>("[sp_student_ExamFrom_College]", new { @Action = "StudentDetailForProvisional", @RegistrationNo = RegistrationNo, @session = Sission }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }
        public ExamForm StudentDetailForPrintCertificate(int sid = 0, int Sission = 0,int insertby=0 ,string Type="")
        {
            //AcademicSession ac = new AcademicSession();
            //int Sission = ac.GetAcademiccurrentSession().ID ;
            var CertificateType = (Type == "Migration"? "Migration": "Provisional");
            var CertificateTypeID = (Type == "Migration" ? 1 : 2);

            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                // move to admin panel
                //Obj = conn.Query<ExamForm>("[sp_student_ExamFrom_College]", new {
                //    @Action = "PrintCertificate",
                //    @sid = sid,
                //    @session = Sission ,
                //    @IPAddress = ip,
                //    @InsertedBy = insertby,
                //    @CertificateType= "Migration",
                //    @CertificateTypeID= CertificateTypeID,
                //}, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }

        public ExamForm student_examform_apply(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", string CollegeUserId="")
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply]", new
                {
                    @Action = "isapply_bycollege",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid,
                    @type = type,
                    @fileupload = fileupload,                    
                    @FromVerifyBy = CollegeUserId,
                     
                @IPAddress = ip
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }
        public ExamForm student_examform_applyBack(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", string CollegeUserId = "")
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply_back]", new
                {
                    @Action = "isapply_bycollege",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid,
                    @type = type,
                    @fileupload = fileupload,
                    @FromVerifyBy = CollegeUserId,
                    @currentyear = System.DateTime.Now.Year,
                    @IPAddress = ip
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }
        public ExamForm student_examform_applyBack1(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", string CollegeUserId = "")
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply_back]", new
                {
                    @Action = "isapply_bycollege1",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid,
                    @type = type,
                    @fileupload = fileupload,
                    @FromVerifyBy = CollegeUserId,
                    @currentyear = System.DateTime.Now.Year,
                    @IPAddress = ip
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }

        public ExamForm student_examform_apply_back(int sid, int Session, int coursecategoryid, int collegeid, int StreamCategoryID, int courseyearid, int isappearedearlierfail, int electivesubjectid, string type = "", string fileupload = "", int currentyear = 0)
        {
            ExamForm Obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<ExamForm>("[sp_student_examform_apply_back]", new
                {
                    @Action = "isapply",
                    @SID = sid,
                    @sessionid = Session,
                    @coursecategoryid = @coursecategoryid,
                    @collegeid = collegeid,
                    @StreamCategoryID = StreamCategoryID,
                    @courseyearid = courseyearid,
                    @isappearedearlierfail = isappearedearlierfail,
                    @electivesubjectid = electivesubjectid
                    ,
                    @type = type,
                    @fileupload = fileupload,
                    @currentyear = currentyear
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }


        public PrintExamForm GetAppLicationDataForExamFee(int id = 0)
        {  
            PrintExamForm objdata = new PrintExamForm();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "StudentDetail", @SID = id }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();                    
                }
                return objdata;
            }
        }
        public PrintExamForm GetAppLicationDataForExamFeeBack(int id ,int CourseYearID)
        {
            PrintExamForm objdata = new PrintExamForm();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "StudentDetailback", @SID = id, @CourseYearID= CourseYearID }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                }
                return objdata;
            }
        }
        public PrintExamForm GetAppLicationDataForExamFeeBackbca(int id = 0,int courseyearid=0)
        {
            PrintExamForm objdata = new PrintExamForm();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_ExamFormFee_CollegeNew]", new { @Action = "StudentDetailbackbca", @SID = id, @courseyearid= courseyearid }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                }
                return objdata;
            }
        }

        public PrintExamForm GetAppLicationDataForCollegeFee(int id = 0)
        {
            PrintExamForm objdata = new PrintExamForm();
            FeesSubmit stlogin = new FeesSubmit();
            BL_PrintRecipt PritFee = new BL_PrintRecipt();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_CollegeStudentdetail]", new { @Action = "StudentDetail", @SID = id }, commandType: CommandType.StoredProcedure);
                if (obj != null)
                {
                    objdata.objExamFrom = obj.Read<ExamForm>().FirstOrDefault();
                }
                return objdata;
            }
        }

        public List<EaxmFeesSubmit> FeesDetailsstructure(int collegeid, int courseCategoryid, int sessionid, int castecategory, int streamCategoryid, int courseyearid, int isconsession = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "StudentExamStuctureFeesDetail", @collegeid = collegeid, @session = sessionid, @courseCategoryid = courseCategoryid, @castecategory = castecategory, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @isconsession = isconsession }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public ExamForm ExamFromVerify(int SID = 0, int CourseYearID = 0, int CollegeId = 0, int SessionID = 0)
        {
            ExamForm obj = new ExamForm();
            string ip = CommonMethod.GetIPAddress();

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<ExamForm>("[Sp_ExamFormFee_College]", new
                {
                    @Action = "VerifyStudent",
                    @SID = SID,
                    @session = SessionID,
                    @FromVerifyBy = CollegeId,
                    @CourseYearID = CourseYearID,
                    @IPAddress = ip
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public ExamForm ExamFromReject(int SID = 0, int CourseYearID = 0, int CollegeId = 0, string reason = "", int SessionID = 0)
        {
            string ip = CommonMethod.GetIPAddress();
            ExamForm obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<ExamForm>("[Sp_ExamFormFee_College]", new
                {
                    @Action = "RejectVerifyStudent",
                    @SID = SID,
                    @session = SessionID,
                    @FromVerifyBy = CollegeId,
                    @IPAddress = ip,
                    @CourseYearID= CourseYearID,
                    @reason = reason
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public ExamForm ExamFormFor_updateelective1subject(int SID = 0, int CourseYearID = 0, int CollegeId = 0, string reason = "", int SessionID = 0,int coursecategortid=0,int electivesubjectid=0)
        {
            string ip = CommonMethod.GetIPAddress();
            ExamForm obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<ExamForm>("[Sp_ExamFormFee_College]", new
                {
                    @Action = "update_elective1",
                    @SID = SID,
                    @session = SessionID,
                    @FromVerifyBy = CollegeId,
                    @IPAddress = ip,
                    @CourseYearID = CourseYearID,
                    @reason = reason,
                    @coursecategoryid= coursecategortid,
                    @electivesubjectid= electivesubjectid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public ExamForm ExamFormFor_updateelective1subject_2(int SID = 0, int CourseYearID = 0, int CollegeId = 0, string reason = "", int SessionID = 0, int coursecategortid = 0, int electivesubjectid = 0)
        {
            string ip = CommonMethod.GetIPAddress();
            ExamForm obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<ExamForm>("[Sp_ExamFormFee_College]", new
                {
                    @Action = "update_elective2",
                    @SID = SID,
                    @session = SessionID,
                    @FromVerifyBy = CollegeId,
                    @IPAddress = ip,
                    @CourseYearID = CourseYearID,
                    @reason = reason,
                    @coursecategoryid = coursecategortid,
                    @electivesubjectid_2 = electivesubjectid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public ExamForm CollegeFromVerify(int SID = 0, int CourseYearID = 0, int CollegeId = 0, int SessionID = 0,int coursecategoryid=0,int collegeid1=0,int StreamCategoryID=0)
        {
            ExamForm obj = new ExamForm();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<ExamForm>("[Sp_CollegeStudentdetail]", new
                {
                    @Action = "VerifyStudent",
                    @SID = SID,
                    @session = SessionID,
                    @FromVerifyBy = CollegeId,
                    @CourseYearID = CourseYearID,
                    @IPAddress = ip,

                    @coursecategoryid= coursecategoryid,
                    @collegeid = collegeid1,
                    @StreamCategoryID = StreamCategoryID,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public ExamForm CollegeFromReject(int SID = 0, int CourseYearID = 0, int CollegeId = 0, string reason = "", int SessionID = 0)
        {
            string ip = CommonMethod.GetIPAddress();
            ExamForm obj = new ExamForm();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<ExamForm>("[Sp_CollegeStudentdetail]", new
                {
                    @Action = "RejectVerifyStudent",
                    @SID = SID,
                    @session = SessionID,
                    @FromVerifyBy = CollegeId,
                    @IPAddress = ip,
                    @CourseYearID = CourseYearID,
                    @reason = reason
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_bca(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                if (courseCategoryid == Convert.ToInt32(CommonSetting.coursecategory.bca))
                {
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findBCA_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = 1124, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                }
                return obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_LLB(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();
                
                    var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findLLB_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                    obj = ObjFees;
                
                return obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_BED(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findBed_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                obj = ObjFees;

                return obj;
            }
        }
        public List<EaxmFeesSubmit> backFeesDetailSubjectlist_PG(int courseCategoryid, int streamCategoryid, int courseyearid, int session, int collegeid, int SID)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                List<EaxmFeesSubmit> obj = new List<EaxmFeesSubmit>();
                string ip = CommonMethod.GetIPAddress();

                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom_back]", new { @Action = "findPG_backpaper", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @session = session, @collegeid = @collegeid, @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                obj = ObjFees;

                return obj;
            }
        }
        public List<EaxmFeesSubmit> SubjectDetailList(int courseCategoryid, int streamCategoryid, int courseyearid)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[Sp_ExamFormFee_College]", new { @Action = "LLBSubject",  @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_bed_C11(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c11", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
    public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_bed_c7b(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c7b", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_Comman_C11(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType, string Action ="")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = Action, @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }

       

        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_bed_c7a(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "ElectivesubjectlistBed_c7a", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
        public List<EaxmFeesSubmit> ElectiveFeesDetailSubjectlist_pg(int courseCategoryid, int streamCategoryid, int courseyearid, int SubjectType)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                var ObjFees = conn.Query<EaxmFeesSubmit>("[sp_student_ExamFrom]", new { @Action = "Electivesubjectlist", @courseCategoryid = courseCategoryid, @streamCategoryid = streamCategoryid, @courseyearid = courseyearid, @SubjectType = SubjectType }, commandType: CommandType.StoredProcedure).ToList();
                return ObjFees;
            }
        }
    }
    public class PrintExamForm
    {

        public EaxmFeesSubmit Examobjfeesubmit { get; set; }     
        public List<EaxmFeesSubmit> subjectlist { get; set; }
        public FeesSubmit objfeesubmit { get; set; }
        public ExamForm objExamFrom { get; set; }
        public BL_PrintRecipt objPrintRecipt { get; set; }
        public ExamForm Currentyear_courseyarid { get; set; }
        

    }
    public class PrintExamForm_admicard
    {

       
        public List<ExamForm> Studentlist { get; set; }
        public List<ExamForm> subjectlist { get; set; }


    }
    public class EaxmFeesSubmit
    {
        
       
        public string HonoursSubject { get; set; }
        public string Subsidiary1Subject { get; set; }
        public string Subsidiary2Subject { get; set; }
        public string Compulsory1Subject { get; set; }
        public string Compulsory2Subject { get; set; }


        public int Substreamcategoryid2 { get; set; }
        public int Substreamcategoryid { get; set; }


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
        public string yearname { get; set; }
        public bool IsAdmissionFee2 { get; set; }
        public string FeeStatus2 { get; set; }
        public int studentyear { get; set; }
        public int incomecertificate_iseligible { get; set; }
        public int late_fee { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
    }
    public class ExamForm_Listlist
    {
        public List<ExamForm> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
