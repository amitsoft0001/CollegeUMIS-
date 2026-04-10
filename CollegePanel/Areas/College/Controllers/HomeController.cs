using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;
using Website.Areas.college.Models;
using Website.Models;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Web.UI.WebControls;
using System.Web.UI;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.OleDb;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Collections;
using static DataLayer.Student_Admission_Choicesubject;

namespace Website.Areas.college.Controllers
{
    [CookiesExpireFilterCollegeAttribute]
    public class HomeController : Controller
    {
        #region
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult Index()
        {
            AcademicSession ad = new DataLayer.AcademicSession();
            int sessionID = ad.GetAcademiccurrentSession().ID;
            ViewBag.sessionid = sessionID;
            ViewBag.sessionlist = ad.GetSession();
            BL_DasbordAdmin obj = new BL_DasbordAdmin();
            BL_DasbordAdminList sub = new BL_DasbordAdminList();
            string collegeID = "";
            int CID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            CID = Convert.ToInt32(collegeID);
            sub = obj.CollegeAdmissionDetails(CID, sessionID);
            return View(sub);
        }
        [HttpPost]
        public ActionResult Index(int id = 0, int Sessionid = 0)
        {
            AcademicSession ad = new DataLayer.AcademicSession();
            ViewBag.sessionid = Sessionid;
            ViewBag.sessionlist = ad.GetSession();
            if (ViewBag.sessionid == 0)
            {
                ViewBag.sessionid = ad.GetAcademiccurrentSession().ID;
            }
            BL_DasbordAdmin obj = new BL_DasbordAdmin();
            BL_DasbordAdminList sub = new BL_DasbordAdminList();
            string collegeID = "";
            int CID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            CID = Convert.ToInt32(collegeID);
            sub = obj.CollegeAdmissionDetails(CID, Sessionid);

            return View(sub);
        }



        [VerifyUrlFilterCollegeAttribute]
        public ActionResult AdmissionRepoprt()
        {
            AcademicSession ad = new DataLayer.AcademicSession();
            int sessionID = ad.GetAcademiccurrentSession().ID;
            ViewBag.sessionid = sessionID;
            ViewBag.sessionlist = ad.GetSession();
            BL_DasbordAdmin obj = new BL_DasbordAdmin();
            BL_DasbordAdminList sub = new BL_DasbordAdminList();
            string collegeID = "";
            int CID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            CID = Convert.ToInt32(collegeID);
            sub = obj.CollegeAdmissionDetails(CID, sessionID);
            return View(sub);
        }
        [HttpPost]
        public ActionResult AdmissionReport(int id = 0, int Sessionid = 0)
        {
            AcademicSession ad = new DataLayer.AcademicSession();
            ViewBag.sessionid = Sessionid;
            ViewBag.sessionlist = ad.GetSession();
            if (ViewBag.sessionid == 0)
            {
                ViewBag.sessionid = ad.GetAcademiccurrentSession().ID;
            }
            BL_DasbordAdmin obj = new BL_DasbordAdmin();
            BL_DasbordAdminList sub = new BL_DasbordAdminList();
            string collegeID = "";
            int CID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            CID = Convert.ToInt32(collegeID);
            sub = obj.CollegeAdmissionDetails(CID, Sessionid);

            return View(sub);
        }







        [VerifyUrlFilterCollegeAttribute]
        public ActionResult UpdateProfile(string id = "")
        {
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                ViewBag.Usertype = usertype;
            }
            else
            {
                ViewBag.Usertype = "";
            }
            if (usertype == 2)
            {
                return RedirectToAction("Index");
            }
            BL_CollegeMaster objmaster = new BL_CollegeMaster();
            Commn_master com = new Commn_master();
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.stitle = com.getcommonMaster("Title");
            State st = new State();
            ViewBag.CAState = st.GetStateListByCountryId(80.ToString());
            //ViewBag.CollegeData = objmaster.GetCollegeData(2);
            string enCollegeID = ClsLanguage.GetCookies("ENNBCLID");

            string enID = "";
            int eID = 0;
            if (enCollegeID != "0" && enCollegeID.Length > 0)
            {

                enID = EncriptDecript.Decrypt(enCollegeID);
                if (enID != "")
                {
                    eID = Convert.ToInt32(enID);
                }
            }
            objmaster = objmaster.GetCollegeDataBYID(3, eID);
            if (objmaster != null)
            {
                objmaster.ID = objmaster.ID;
                objmaster.CollegeCode = objmaster.CollegeCode;
                objmaster.CollegeName = objmaster.CollegeName;
                ViewBag.tit = objmaster.NameTitle;
                ViewBag.gen = objmaster.Gender;

            }

            return View(objmaster);

        }
        public JsonResult AddNewCollage()
        {
            if (Request.Form.Count > 0)
            {
                BL_CollegeMaster stvalue = new BL_CollegeMaster();
                //stvalue.CollegeCode = Request.Form["code"];
                //stvalue.CollegeName = Request.Form["Cname"];
                //stvalue.NameTitle = Convert.ToInt32(Request.Form["title"]);
                // stvalue.Name = Request.Form["Name"];
                //stvalue.Gender = Convert.ToInt32(Request.Form["Gender"]);
                stvalue.ContactNo = Request.Form["ContactNo"];
                stvalue.Email = Request.Form["email"];
                stvalue.Address = Request.Form["Address"];
                stvalue.City = Request.Form["City"];
                stvalue.State = Convert.ToInt32(Request.Form["State"]);
                stvalue.PinCode = Request.Form["pincode"];
                stvalue.ID = Convert.ToInt32(Request.Form["ID"]);
                stvalue.NoOfRooms = Convert.ToInt32(Request.Form["NoOfRooms"]);
                stvalue.NoOfSeats = Convert.ToInt32(Request.Form["NoOfSeats"]);
                stvalue.NodalOfficerName = Request.Form["NodalOfficerName"];
                stvalue.NodalOfficerEmail = Request.Form["NodalOfficerEmail"];
                stvalue.NodalOfficerMobile = Request.Form["NodalOfficerMobile"];
                stvalue.PrincipalName = Request.Form["PrincipalName"];
                stvalue.PrincipalMobile = Request.Form["PrincipalMobile"];
                stvalue.PrincipalEmail = Request.Form["PrincipalEmail"];


                try
                {
                    BL_CollegeMaster ob = new BL_CollegeMaster();
                    if (ClsLanguage.GetCookies("ENNBCLID") != null)
                    {
                        stvalue.InsertBy = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
                    }
                    var result = ob.SaveCollegeDataajax(stvalue);
                    //if (result.status == true)
                    //{
                    //    Email.SendEmailForCollege_signup(result.PrincipalEmail, result.Password, result.PrincipalName, result.CollegeCode, result.CollegeName);
                    //}
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Update College Profile", stvalue.ID.ToString());
                    BL_CollegeMaster ob = new BL_CollegeMaster();
                    ob.Msg = "Error occurred. Error details: " + ex.Message;
                    return Json(ob, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                BL_CollegeMaster ob = new BL_CollegeMaster();
                ob.Msg = "Please Again Fill From : ";
                return Json(ob, JsonRequestBehavior.AllowGet);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult Changepassword()
        {
            return View();
        }
        public JsonResult Updatepassword(ChangeCollegePassword ob)
        {
            BL_CollegeLogin st = new BL_CollegeLogin();
            var cid = ClsLanguage.GetCookies("ENNBUID");
            string enID = "";
            int eID = 0;
            if (cid != "0" && cid.Length > 0)
            {
                enID = EncriptDecript.Decrypt(cid);
                if (enID != "")
                {
                    eID = Convert.ToInt32(enID);
                }
            }
            ob.CollegeID = eID;
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            Byte[] hashedBytesPass;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(ob.CurrentPassword));
            hashedBytesPass = md5Hasher.ComputeHash(encoder.GetBytes(ob.NewPassword));
            ob.U_password = hashedBytes;
            ob.U_passwordNew = hashedBytesPass;
            var obj = st.ChangePassword(ob);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        // [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeStudentList()
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            return View();
        }
        //[VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult CollegeStudentList(int id = 0)
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            string subject = Request.Form["Subject"];
            string session1 = Request.Form["session"];
            string seatType = Request.Form["SeatType"];
            string cast = Request.Form["CastCategory"];
            string coursetype = Request.Form["Coursetype"];
            //string collegeID = Request.Form["CollegeID"];
            string CounsellingNo = Request.Form["CounsellingNo"];
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            string application = Request.Form["application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            sub = obj.studentdetailList(1, 1000000, coursetype, subject, session1, collegeID, cast, seatType, application, ApplicationStatus, CounsellingNo);
            var result = sub.qlist.Select(x => new { ApplicationNo = x.ApplicationNo, StudentName = x.StudentName, FatherName = x.FatherName, PercentageInIntermediate = x.percenatge, CasteCategory = x.StudentCasteCategoryName, Course = x.coursecategotyName, HonoursSubject = x.StreamCategoryName, Session = x.Session, CollegeName = x.CollegeName, ReservationType = (x.ishandicapped == true ? "Handicapped" : "Normal"), Counsellingno = x.counsellingno, ReservationCategory = x.CasteCategoryName, AdmissionStatus = (x.IsApplied == true ? "Applied" : "Pending"), ApplyDate = (x.IsApplied == true ? x.IsAppliedDate : ""), Gender = x.Gender, BloodGroup = x.BloodGroup, Religion = x.Religion, Nationality = x.Nationality, CurrentAddress = x.CurrentAddress }).ToList();

            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=SelectedStudentList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [BasicAuthentication]
        public JsonResult getcourse(int id)
        {
            Commn_master com = new Commn_master();
            var obj = com.getcommonMaster("Course", id);
            return Json(new { data = obj, success = true });
        }
        [BasicAuthentication]
        public JsonResult getcourseAvailable(int id,int session)
        {
            Commn_master com = new Commn_master();
            int collegeid = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "0");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeid = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            var obj = com.getCourseMaster(id, collegeid,session);
            return Json(new { data = obj, success = true });
        }
        [BasicAuthentication]
        public JsonResult selectStreamCategory(string varid)
        {
            int id = !string.IsNullOrEmpty(varid) ? Convert.ToInt16(varid) : 0;
            SelectStreamCategory objstreem = new SelectStreamCategory();
            var obj = objstreem.GetStreamCategory(id);
            return Json(new { data = obj, success = true });
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ForgotPassword(string id)
        {
            try
            {
                string AdminID = EncriptDecript.Decrypt(id);
                BL_ForgotPass obj = new BL_ForgotPass();
                obj.AdminID = Convert.ToInt32(AdminID);
                ViewBag.ID = Convert.ToInt32(AdminID);
                return View();
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ForgotPassword", id);
                return View();
            }
        }
        [HttpPost]
        [VerifyUrlFilterCollegeAttribute]
        public JsonResult ForgotPasswords(string Password, int id)
        {
            try
            {
                BL_ForgotPass forgot = new BL_ForgotPass();
                var obj = forgot.ForgotPassword(id, Password);
                return Json(new { data = obj, success = true });
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "ForgotPassword paost Method", id.ToString());
                return Json(new { data = new BL_ForgotPass(), success = true });
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ResetPass(string ApplicationNo)
        {

            BL_ForgotPass obj = new BL_ForgotPass();
            try
            {
                var sendreset = obj.ResetPass(ApplicationNo);
                int StudentID = sendreset.SID;
                string ID = EncriptDecript.Encrypt(StudentID.ToString());
                string MyName = sendreset.Name;
                string SEmail = sendreset.Email;
                string url = ConfigurationManager.AppSettings["siteUrl"];
                string PasswordResetLink = url + "Home/ForgotPassword?Id=" + ID;
                if (StudentID > 0)
                {
                    Email.SendEmailForResetPassword(SEmail, MyName, PasswordResetLink);
                    obj.Msg = "Reset Password Link sent to your registered Email ID..!!";
                    TempData["errorMsg"] = obj.Msg;
                }
                else
                {
                    obj.Msg = "Invalid Application ID..!!";
                    //TempData["errorMsg"] = obj.Msg;
                }
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Reset Password", ApplicationNo);
            }
            return Json(new { data = obj, success = true });
        }
        [HttpPost]
        public ActionResult PrintApplicationCall(int id)
        {
            BL_PrintApplication PritApp = new BL_PrintApplication();

            var obj = PritApp.GetAppLicationDataAdmincollege(id);
            return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [BasicAuthentication]
        public JsonResult getsubjtectbycousrescollege(int id = 0)
        {
            int collegeid = 0;
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeid = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            BL_StreamMaster com = new BL_StreamMaster();
            var obj = com.getsubjectbycourse(18, id, collegeid);
            return Json(new { data = obj, success = true });
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult DocumentVerifyList()
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  // objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult DocumentVerifyList(int id = 0)
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();

            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();

            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            string subject = Request.Form["Subject"];
            string session1 = Request.Form["session"];
            string seatType = Request.Form["SeatType"];
            string cast = Request.Form["CastCategory"];
            string coursetype = Request.Form["Coursetype"];
            string CounsellingNo = Request.Form["CounsellingNo"];


            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            string application = Request.Form["application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string FeeStatus = Request.Form["FeeStatus"];
            string IncomeStatus = Request.Form["IncomeStatus"];

            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            sub = obj.studentdetailListForVerification(1, 1000000, coursetype, subject, session1, collegeID, cast, seatType, application, ApplicationStatus, CounsellingNo, FeeStatus, IncomeStatus);

            var result = sub.qlist.Select(x => new
            {
                ApplicationNo = x.ApplicationNo,
                StudentName = x.StudentName,
                FatherName = x.FatherName,
                DOB = x.DOB,
                Email = x.Email,
                MobileNumber = x.Mobile,
                PercentageInIntermediate = x.percenatge,
                CasteCategory = x.StudentCasteCategoryName,
                Course = x.coursecategotyName,
                Session = x.Session,
                CollegeName = x.CollegeName,
                ReservationType = (x.ishandicapped == true ? "Handicapped" : "Normal"),
                Counsellingno = x.counsellingno,
                ReservationCategory = x.CasteCategoryName,
                ApplyDate = (x.IsApplied == true ? x.IsAppliedDate : ""),
                DocumentVerification = (x.IsDocVerify == 1 ? "Verified" : (x.IsDocVerify == 2 ? "Rejected" : "Pending")),
                //DocumentVerificationDate = (x.IsDocVerify == 1 ? x.IsDocVerifyDate : (x.IsDocVerify == 2 ? x.IsDocVerifyDate : "")),
                FeeSubmit = (x.IsAdmissionFee == true ? "Paid" : "Pending"),
                FeeSubmissionDate = (x.IsAdmissionFee == true ? x.IsfeesubmitDate : ""),
                Gender = x.Gender,
                BloodGroup = x.BloodGroup,
                Religion = x.Religion,
                Nationality = x.Nationality,
                HonoursSubject = x.StreamCategoryName,
                CurrentAddress = x.CurrentAddress,
                PermanentAddress = x.PA_Address,
                Subsidarysubject1 = x.Subsidiary1Subject,
                Subsidarysubject2 = x.Subsidiary2Subject,
                Compublsorysubject1 = x.Compulsory1Subject,
                Compublsorysubject2 = x.Compulsory2Subject,
                FatherQualification = x.FatherQualification,
                FatherOccupation = x.FatherOccupation,
                FatherMobile = x.FatherMobile,
                FatherEmail = x.FatherEmail,
                MotherName = x.MotherName,
                MotherQualification = x.MotherQualification,
                MotherOccupation = x.MotherOccupation,
                MotherEmail = x.MotherEmail


            }).ToList();




            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=VerifiedStudentList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [BasicAuthentication]
        public JsonResult getcastseatidseatscollege(int id)
        {
            Bl_SeatMater com = new Bl_SeatMater();
            var obj = com.getdateseatcollge(0, "courseseatbyidseat", 0, id, 0);
            return Json(new { data = obj, success = true });
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeSeatList()
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.EducationType = objmaster.GetEduType(6, Convert.ToInt32(collegCode));

            //objmaster.GetEducationType(Convert.ToInt32(collegCode));

            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            //ViewBag.Educationtype = com.getcommonMaster("EducationType");

            ViewBag.Session = ac.GetSession();
            ViewBag.CurrentSession = ac.GetAcademiccurrentSession().Session;
            ViewBag.CurrentSessionid = ac.GetAcademiccurrentSession().ID;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            return View();



        }
        [HttpPost]
        public ActionResult CollegeSeatList(int id = 0)
        {
            Commn_master com = new Commn_master();
            AcademicSession ac = new AcademicSession();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            ViewBag.CurrentSession = ac.GetAcademiccurrentSession().Session;
            ViewBag.CurrentSessionid = ac.GetAcademiccurrentSession().ID;
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollege();
            ViewBag.Session = ac.GetSession();
            string courseid = Request.Form["CourseCategoryID"];
            string adminissionid = Request.Form["Admissiontype"];
            string Educationtype = Request.Form["Educationtype"];
            string sessionid = Request.Form["session"];
            string collegeID = Request.Form["CollegeID"];
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            Bl_SeatMater obj = new Bl_SeatMater();
            Bl_SeatList sub = new Bl_SeatList();
            sub = obj.GetseatListCollege("Couserseatview", 1, 1000000000, 0, courseid, Educationtype, sessionid, adminissionid, CollegeID);


            var result = sub.qlist.Select(x => new { Pogramme = x.EducationTypename, Course = x.CourseCategory, CollegeName = x.CollegeName, Subject = x.streamCategory, TotalSeat = x.SeatsAvaible, Add_ManualSeat = x.add_manualseat, ConsumeSeat = x.consume_seat, RemainingSeats = x.remainingseat }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=CollegeSeatList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CounsellingSheetReporCollegeWiset()
        {
            Commn_master com = new Commn_master();
            AcademicSession session = new AcademicSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            return View();
        }
        [BasicAuthentication]
        [HttpPost]
        public JsonResult CounsellingSheetReporCollegeWiset(string EducationType = "", string ddlsession = "", string Coursetype = "")
        {
            BL_CounsellingSheetReport PritApp = new BL_CounsellingSheetReport();
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = PritApp.GetCounsellingSheetReportCollegeWise(EducationType, ddlsession, Coursetype, CollegeID);
            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CounsellingSheetReport()
        {
            Commn_master com = new Commn_master();
            AcademicSession session = new AcademicSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollegeData(2);
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            return View();
        }
        [BasicAuthentication]
        [HttpPost]
        public JsonResult CounsellingSheetReport(string EducationType = "", string ddlsession = "", string Coursetype = "", string Subject = "")
        {
            BL_CounsellingSheetReport PritApp = new BL_CounsellingSheetReport();
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = PritApp.GetCounsellingSheetReport(EducationType, ddlsession, Coursetype, Subject, CollegeID);
            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult DocumentVerify(string id = "")
        {
            //string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                StudentLogin tblST = new StudentLogin();
                BL_PrintApplication ob = new BL_PrintApplication();
                AcademicSession ad = new AcademicSession();
                int sessionid = ad.GetAcademiccurrentSession().ID;
                StudentLogin STu = new StudentLogin();
                var obj1 = tblST.BasicDetailByID(sid);
                if (obj1 != null)
                {
                    int educationtype = obj1.EducationType;
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype, sid);
                    ViewBag.addmissionExtenddate = dateextend.Status;
                    ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype, sid);
                    ViewBag.addmissionStartdate = datestart.Status;
                    ViewBag.addmissionStartdateValue = datestart.startdate;
                    ViewBag.IsApplied = ob.CheckStudentApplied(obj1.session, sid).Status;
                    ViewBag.IsVerify = ob.CheckStudentVerify(obj1.session, sid).Status;

                }

                var objstu = ob.CheckStudentAdmission(obj1.session, sid);
                if (objstu.Status == true)
                {
                    ViewBag.Status = objstu.Status;
                    ViewBag.Course = objstu.CourseName;
                    ViewBag.College = objstu.CollegeName;
                    ViewBag.Stream = objstu.StreamName;
                }
                else
                {
                    ViewBag.Status = false;
                    ViewBag.Course = "";
                    ViewBag.College = "";
                }
                BL_PrintAllRecord result = ob.AdmissionDetail(obj1.session, sid);
                return View(result);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Document Verify get action", stdID);
                return RedirectToAction("DocumentVerifyList/");
            }


        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult IncomeCertificateVerify(string id = "")
        {
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                StudentLogin tblST = new StudentLogin();
                DataLayer.Login objstu = new DataLayer.Login();
                var obj1 = tblST.BasicDetailByID(sid);
                if (obj1 != null)
                {
                    int sessionid = obj1.session;
                    objstu = tblST.IncomeCertificateDetailID(sid, sessionid);
                    if (objstu != null)
                    {
                        ViewBag.IsAdmissionFee2 = objstu.IsAdmissionFee2;
                        ViewBag.incomecertificate_iseligible = objstu.incomecertificate_iseligible;
                        ViewBag.Reason = objstu.Reason;
                    }
                }

                return View(objstu);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Income Document Verify get action", stdID);
                return RedirectToAction("DocumentVerifyList/");
            }


        }
        public JsonResult IncomeDocumentVerifyForStudent(string Id = "")
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            int sid = 0;
            try
            {
                if (Id != "")
                {
                    sid = Convert.ToInt32(Id);
                }
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(sid);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.IncomeVerifyStudent(sid, eID, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Income Document Verify get action", Id);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something hdhsajd Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult IncomeDocumentVerifyForStudentrejects(string Id = "", string reason = "")
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            int sid = 0;
            try
            {
                if (Id != "")
                {
                    sid = Convert.ToInt32(Id);
                }
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(sid);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.IncomeVerifyStudentreject(sid, eID, reason, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Income Document Verify get action", Id);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DocumentVerifyForStudent(string Id = "")
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            if (Id != "")
            {
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");

                string enID = "";
                int eID = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                }
                var result = ob.VerifyStudent(Convert.ToInt32(Id), eID);
                if (result.Status)
                {
                    SMSFUN.sms_StudentDocumentVerification(result.MobileNo);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DocumentVerifyForStudentrejects(string Id = "", string reason = "")
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            if (Id != "")
            {
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");

                string enID = "";
                int eID = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                }
                var result = ob.VerifyStudentreject(Convert.ToInt32(Id), eID, reason);
                if (result.Status)
                {
                    SMSFUN.sms_StudentDocumentReject(result.MobileNo);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult FinalStudentList()
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();

            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();

            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = "";
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            ViewBag.CourseYear = "";
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult FinalStudentList(int id = 0)
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            ViewBag.CourseYear = "";
            string subject = Request.Form["Subject"];
            string session1 = Request.Form["session"];
            string seatType = Request.Form["SeatType"];
            string cast = Request.Form["CastCategory"];
            string coursetype = Request.Form["Coursetype"];
            string CounsellingNo = Request.Form["CounsellingNo"];
            string Registration = Request.Form["Registration"];
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            string application = Request.Form["application"];
            // string ApplicationStatus = Request.Form["ApplicationStatus"];
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            sub = obj.feesubmittedstudentdetailListExport(1, 1000000, coursetype, subject, session1, collegeID, cast, seatType, application, /*ApplicationStatus,*/ CounsellingNo, "", "", "", Registration);
            //var result = sub.qlist.Select(x => new { ApplicationNo = x.ApplicationNo, RollNo = x.RollNo, StudentName = x.StudentName, MobileNo = x.Mobile, Email = x.Email, FatherName = x.FatherName, PercentageInIntermediate = x.percenatge, CasteCategory = x.StudentCasteCategoryName, Course = x.coursecategotyName, HonoursSubject = x.StreamCategoryName, Subsidiary1Subject = x.Subsidiary1Subject, Subsidiary2Subject = x.Subsidiary2Subject, Compulsory1Subject = x.Compulsory1Subject, Compulsory2Subject = x.Compulsory2Subject, Session = x.Session, CollegeName = x.CollegeName, ReservationType = (x.ishandicapped == true ? "Handicapped" : "Normal"), Counsellingno = x.counsellingno, ReservationCategory = x.CasteCategoryName, ApplyDate = (x.IsApplied == true ? x.IsAppliedDate : ""), DocumentVerification = (x.IsDocVerify == 1 ? "Verified" : (x.IsDocVerify == 2 ? "Rejected" : "Pending")), DocumentVerificationDate = (x.IsDocVerify == 1 ? x.IsDocVerifyDate : (x.IsDocVerify == 2 ? x.IsDocVerifyDate : "")), AdmissionFee = x.Fees, FeeStatus = (x.IsAdmissionFee == true ? "Paid" : "Pending"), FeeSubmissionDate = (x.IsAdmissionFee == true ? x.IsfeesubmitDate : ""), TransactionID = x.banktrxid, Gender = x.Gender, BloodGroup = x.BloodGroup, Religion = x.Religion, Nationality = x.Nationality, CurrentAddress = x.CurrentAddress }).ToList();



            var result = sub.qlist.Select(x => new
            {
                SrNo = x.rownumber,
                ApplicationNo = x.ApplicationNo,
                StudentName = x.StudentName,
                Gender = x.Gender,
                StudentCasteCategory = x.StudentCasteCategoryName,
                RollNo = x.RollNo,
                RegistrationNo = x.EnrollmentNo,
                CollegName = x.CollegeName,
                Percenatge = x.percenatge,
                ReservationCategory = x.CasteCategoryName,
                Course = x.coursecategotyName,
                Session = x.Session,
                ScrutinyNo = x.counsellingno,
                DocumentVerified = (x.IsDocVerify == 1 ? "Verified" : "Pending"),
                DocumentVerificationDate = x.IsDocVerifyDatenew,
                FeeAmount = x.Fees,
                FeesStatus = (x.IsAdmissionFee == true ? "Paid" : "UnPaid"),
                FeesSubmissionDate = x.IsfeesubmitDate,
                HonoursSubject = x.StreamCategoryName,
                Subsidiary1Subject = x.Subsidiary1Subject,
                Subsidiary2Subject = x.Subsidiary2Subject,
                Compulsory1Subject = x.Compulsory1Subject,
                Compulsory2Subject = x.Compulsory2Subject,
            }).ToList();


            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=FinalStudentList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [HttpPost]
        public JsonResult view1studentDetail(int sid)
        {
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            sub = obj.viewfeestudentdetailList(sid);
            return Json(new { data = sub.qlist, success = true });
        }
        public JsonResult CourseYear(int id)
        {
            Recruitment com = new Recruitment();
            var obj = com.CourseYear(id);
            return Json(new { data = obj, success = true });
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CreateUser(string id = "")
        {
            UserLogin ob = new UserLogin();
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                ViewBag.Usertype = usertype;
            }
            else
            {
                ViewBag.Usertype = "";
            }
            if (usertype == 2)
            {
                return RedirectToAction("Index");

            }

            if (id != "0" && id.Length > 0)
            {
                string enID = "";
                int eID = 0;
                try
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (eID > 0)
                    {

                        UserLogin res = ob.userdata(eID);
                        ViewBag.role = res.UserType;
                        return View(res);
                    }
                    else
                    {


                        return View(ob);

                    }
                }
                catch (Exception ex)
                {
                    //CommonMethod.PrintLog(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit User", eID.ToString() );
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit New User", eID.ToString());
                    return RedirectToAction("CreateUser/");
                }
            }
            return View(ob);


        }
        public JsonResult Addnewuser(UserLogin ob)
        {
            UserLogin st = new UserLogin();
            UserLogin obj = new UserLogin();
            string enid = "";
            var Id = 0;
            var Usertype = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                Id = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            }
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                Usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            }

            try
            {

                if (ob.EncriptedID != null && ob.EncriptedID != "0" && ob.EncriptedID.Length > 0)
                {
                    ob.ID = Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID));
                    obj = st.Addnewusertype(ob, Usertype, Id);
                    if (ob.ID == 0)
                    {
                        if (obj.status == true)
                        {
                            Email.SendEmailForUser_SignUp(obj.Email, obj.Password, obj.UserName);
                        }
                    }
                }
                else
                {
                    obj = st.Addnewusertype(ob, Usertype, Id);
                }
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit New User", obj.ID.ToString());
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Checkuser(string name = "")
        {
            UserLogin st = new UserLogin();
            var obj = st.Checkuserbyname(name);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ManageUser()
        {
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                ViewBag.Usertype = usertype;
            }
            else
            {
                ViewBag.Usertype = "";
            }
            if (usertype == 2)
            {
                return RedirectToAction("Index");

            }
            else
            {
                return View();

            }
        }
        public JsonResult ActiveDeactiveRecord(string id, string type)
        {
            var result = false;
            var Id = Int32.Parse(id);

            result = EmployeeRegisteration.activedeactiveuser(Id);
            return Json(result);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult UserRolePermission()
        {
            UserLogin obj = new UserLogin();
            UserList objUser = new UserList();
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            var collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            if (usertype != 0 && collegeId != 0)
            {
                objUser = obj.GetUsermenuList(Convert.ToInt32(usertype), Convert.ToInt32(collegeId));
                ViewBag.UserList = objUser.qlist.ToList();
                College_MenuMaster menu = new College_MenuMaster();
                List<College_MenuMaster> _menus = menu.getmenuadminlist(collegeId).ToList();
                if (usertype == 2)
                {
                    //var ID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
                    //_menus = menu.getempmenulist(collegeId, ID).ToList();                    
                    return RedirectToAction("Index");

                }
                ViewBag.menuList = _menus;
            }
            else
            {
                ViewBag.UserList = "";
                ViewBag.menuList = "";
            }

            return View();

        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult UserRolePermission(string id = "", string selectedItems = "")
        {
            UserLogin obj = new UserLogin();
            UserList objUser = new UserList();
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            var collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            ViewBag.Usertype = usertype;
            objUser = obj.GetUsermenuList(Convert.ToInt32(usertype), Convert.ToInt32(collegeId));
            ViewBag.UserList = objUser.qlist.ToList();
            College_MenuMaster menu = new College_MenuMaster();
            List<College_MenuMaster> _menus = menu.getmenuadminlist(collegeId).ToList();
            if (usertype == 2)
            {
                return RedirectToAction("Index");
            }
            ViewBag.menuList = _menus;
            if (id == "")
            {
                return View();
            }
            var userid = obj.GetUserList(Convert.ToInt32(id));
            var usermenustr = ",";
            if (userid != null)
            {
                if (userid.menustr != null)
                {
                    usermenustr = userid.menustr;
                }
            }
            string[] userdatamenustr = usermenustr.Split(new[] { "," }, StringSplitOptions.None);
            foreach (var item in _menus.ToList())
            {
                //_menus.Where(m => obj.menustr.Contains(m.MenuID.ToString())).ToList();
                if (userdatamenustr.Contains(item.MenuID.ToString()))
                {
                    item.checkchecked = true;
                }
                else
                {
                    item.checkchecked = false;
                }
            }
            if (selectedItems == "")
            {
                TempData["Msg"] = null;
                return View();
            }
            else
            {
                try
                {
                    var result = obj.GetUserchagemenustr(Convert.ToInt32(id), selectedItems);
                    if (result == true)
                    {
                        userdatamenustr = selectedItems.Split(new[] { "," }, StringSplitOptions.None);
                        foreach (var item in _menus.ToList())
                        {
                            item.checkchecked = false;
                            if (userdatamenustr.Contains(item.MenuID.ToString()))
                            {
                                item.checkchecked = true;
                            }
                            else
                            {
                                item.checkchecked = false;
                            }
                        }
                        TempData["Msg"] = " Saved Successfully !!";
                        return RedirectToAction("UserRolePermission");
                    }
                    else
                    {
                        TempData["Msg"] = "Failed to Saved !!";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    // CommonMethod.PrintLog(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit User Role Permission", id.ToString());
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit Employee Role Permission", " " + id);
                    return View();
                }
            }
            return View();

        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult UpdateUserProfile(string id = "")
        {
            Commn_master com = new Commn_master();
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            EmployeeRegisteration ob = new EmployeeRegisteration();

            State st = new State();
            ViewBag.CAState = st.GetStateListByCountryId(80.ToString());
            ViewBag.PAState = st.GetStateListByCountryId(80.ToString());

            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                ViewBag.Usertype = usertype;
            }
            else
            {
                ViewBag.Usertype = "";
            }
            var result = new EmployeeRegisteration();
            string enID = "";
            int eID = 0;
            try
            {

                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    result = ob.getdetailsForUpdate(eID);
                    ViewBag.bloodgroupid = result.BloodGroup;
                    ViewBag.castid = result.CastCategory;
                    ViewBag.genderid = result.Gender;

                    ViewBag.titileid = result.title;
                    ViewBag.ftitileid = result.Ftitle;
                    ViewBag.Nationalityid = result.Nationality;
                    ViewBag.Religionid = result.Religion;
                    ViewBag.MotherTongueid = result.MotherTongue;
                    //ViewBag.FacultyTypeid = result.facultyType;
                    ViewBag.Designationid = result.Designation;
                }
                result.EID = id;
                return View(result);
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Edit Employee Profile", eID.ToString());
                return RedirectToAction("UpdateUserProfile/");
            }

        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult UpdateUserProfile(EmployeeRegisteration emp, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {
            EmployeeRegisteration st = new EmployeeRegisteration();
            Commn_master com = new Commn_master();
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            EmployeeRegisteration ob = new EmployeeRegisteration();

            State sta = new State();
            ViewBag.CAState = sta.GetStateListByCountryId(80.ToString());
            ViewBag.PAState = sta.GetStateListByCountryId(80.ToString());
            ViewBag.bloodgroupid = emp.BloodGroup;
            ViewBag.castid = emp.CastCategory;
            ViewBag.genderid = emp.Gender;
            ViewBag.titileid = emp.title;
            ViewBag.ftitileid = emp.Ftitle;
            ViewBag.Nationalityid = emp.Nationality;
            ViewBag.Religionid = emp.Religion;
            ViewBag.MotherTongueid = emp.MotherTongue;

            emp.Password = CommonSetting.CreatePassword(6);
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(emp.Password));
            emp.U_password = hashedBytes;
            string jsonstring = JsonConvert.SerializeObject(emp);
            try
            {
                if (photo != null)
                {
                    Stream st1 = photo.InputStream;
                    string name = Path.GetFileName(photo.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "College";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeePhoto_" + emp.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        emp.stphoto = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
                    }
                }
                if (sign != null)
                {
                    Stream st1 = sign.InputStream;
                    string name = Path.GetFileName(sign.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "College";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeeSign_" + emp.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        emp.stsign = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
                    }
                }
                var collegeid = ClsLanguage.GetCookies("ENNBCLID");
                if (collegeid != "")
                {
                    emp.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    emp.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                emp.Id = emp.hid;
                var result = st.Employee_registration(emp);
                if (result.status == true)
                {
                    TempData["StMessage"] = result.Message;
                    // return RedirectToAction("UpdateUserProfile");
                    return RedirectToAction("ViewProfile/" + emp.EID);
                }
                else { TempData["StMessage"] = result.Message; }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
            }
            return View(emp);
        }
        //public ActionResult UpdateUserProfile(string id = "")
        //{
        //    UserLogin ob = new UserLogin();
        //    var usertype = 0;
        //    if (ClsLanguage.GetCookies("ENNBUserType") != null)
        //    {
        //        usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
        //        ViewBag.Usertype = usertype;
        //    }
        //    else
        //    {
        //        ViewBag.Usertype = "";
        //    }


        //    if (id != null && id != "0" && id.Length > 0)
        //    {
        //        string enID = "";
        //        int eID = 0;
        //        try
        //        {

        //            enID = EncriptDecript.Decrypt(id);
        //            if (enID != "")
        //            {
        //                eID = Convert.ToInt32(enID);
        //            }
        //            if (eID > 0)
        //            {

        //                UserLogin res = ob.userdata(eID);
        //                ViewBag.role = res.UserType;
        //                return View(res);
        //            }
        //            else
        //            {


        //                return View(ob);

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //CommonMethod.PrintLog(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit User", eID.ToString() );
        //            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Edit Employee Profile", eID.ToString());
        //            return RedirectToAction("UpdateUserProfile/");
        //        }
        //    }
        //    return View(ob);


        //}
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ViewProfile(string id = "")
        {
            EmployeeRegisteration ob = new EmployeeRegisteration();

            if (id != null && id != "0" && id.Length > 0)
            {
                string enID = "";
                int eID = 0;
                try
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (eID > 0)
                    {

                        EmployeeRegisteration res = ob.getdetailsForViewDetail(eID);
                        ViewBag.role = res.Designation;
                        return View(res);
                    }
                    else
                    {
                        return View(ob);
                    }
                }
                catch (Exception ex)
                {

                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "View Profile Employee Profile", eID.ToString());
                    return RedirectToAction("ViewProfile/");
                }
            }
            else
            {
                return View(ob);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult GenerateIDCard()
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            //  ViewBag.EducationType = objmaster.GetEducationType();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            ViewBag.application = "";
            ViewBag.Education = "";
            ViewBag.Enrollment = "";
            return View();
        }
        // [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult GenerateIDCard(string program = "", string CoursetypeID = "", string Name = "", string Enrollment = "")
        {
            int eID = 0;
            BL_courseMaster objmaster = new BL_courseMaster();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.Education = program;
            ViewBag.Coursetype = "";
            ViewBag.CoursetypeID = CoursetypeID;
            ViewBag.application = Name;
            ViewBag.Enrollment = Enrollment;
            try
            {
                string enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
                int CourseCategory = 0;
                if (CoursetypeID != "")
                {
                    CourseCategory = Convert.ToInt32(CoursetypeID);

                }
                string enID = "";

                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                }
                EnrollmentRequest ob = new EnrollmentRequest();
                int edid = 0;
                if (program != "")
                {
                    edid = Convert.ToInt32(program);
                    ViewBag.Coursetype = ob.getcourseMaster(edid);
                    if (CoursetypeID == "")
                    {
                        return View();
                    }
                }
                List<EnrollmentRequest> list = new List<EnrollmentRequest>();
                list = ob.AllStudentForIDCard(eID, CourseCategory, Name, Enrollment);
                if (list.Count == 0)
                {
                    return View();
                }

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    string content = "";
                    content += @"
                              <div style=""page-break-after: always;"">
                            <div class="" minheight1"" id=""dvContents"">
                                <div style=""width:1050px; margin:auto; font-family:Arial, Helvetica, sans-serif;  padding:20px; margin-top:40px;border:0px solid #eee; font-size:10px; clear:both;"">
                                    <div style=""float:left; width:475px; padding:15px; margin:0 10px; border:1px solid #ccc; height: 260px;"">
                                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                            <tr>
                                                <td width=""100%"" valign=""top"">
                                                    <table width=""98%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                                        <tr>
                                                            <td colspan=""2"">
                                                                <div style=""text-align:center; font-size:12px;"">
                                                                    <div style=""height: 28px;text-align: center;font-weight: bold;font-size:18px;"">" + list[i].collegeName + @" </div>";
                    if (list[i].is_affiliated == 0)
                    {
                        content += @"<p style=""margin-bottom:10px; margin-top:10px;"">(A Constitutent  Unit of Munger University, Munger (Bihar).)</p> ";

                    }
                    else
                    {
                        content += @" <p style=""margin-bottom:10px; margin-top:10px;"">(An Affiliated Unit of Munger University, Munger (Bihar).)</p>";
                    }
                    content += @"    <p><strong>Session: " + list[i].sessionname + @"</strong></p>";
                    content += @"</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width=""116"" valign=""top"">
                                                                <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                                                    <tr>
                                                                        <td width=""100"" valign=""top"" style=""border:3px solid #333;"">
                                                                            <img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + list[i].photo + @""" width=""100"" onerror="" this.src='../../images/noimage.png';"">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width=""431"" valign=""top"">
                                                                <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""font-size:12px;"">
                                                                    <tr>
                                                                        <td width=""21"" style=""padding:5px;"">&nbsp;</td>
                                                                        <td width=""141"" style=""padding:5px 5px 5px 0""><strong>Name</strong></td>
                                                                        <td width=""14"" style=""padding:5px;"">:</td>
                                                                        <td width=""218"" style=""padding:5px"">" + list[i].Name + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width=""21"" style=""padding:5px;"">&nbsp;</td>
                                                                        <td style=""padding:5px 5px 5px 0""><strong>Roll No</strong></td>
                                                                        <td style=""padding:5px;"">:</td>
                                                                        <td style=""padding:5px"">" + list[i].RollNo + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px;"">&nbsp;</td>
                                                                        <td style=""padding:5px 5px 5px 0""><strong>";

                    content += ((list[i].Ftitle == 20 ? "Father's  Name" : (list[i].Ftitle == 21 ? " Husband's  Name" : " Father's Name")));


                    content += @" </strong></td>
                                                                        <td style=""padding:5px;"">:</td>
                                                                        <td style=""padding:5px"">" + list[i].FatherName + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px;"">&nbsp;</td>
                                                                        <td style=""padding:5px 5px 5px 0""><strong>Mother’s Name</strong></td>
                                                                        <td style=""padding:5px;"">:</td>
                                                                        <td style=""padding:5px"">" + list[i].MotherName + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px;"">&nbsp;</td>
                                                                        <td style=""padding:5px 5px 5px 0""><strong>Blood Group</strong></td>
                                                                        <td style=""padding:5px;"">:</td>
                                                                        <td style=""padding:5px"">" + list[i].bloodgroup + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px;"">&nbsp;</td>
                                                                        <td style=""padding:5px 5px 5px 0""><strong>Date of Birth</strong></td>
                                                                        <td style=""padding:5px;"">:</td>
                                                                        <td style=""padding:5px"">" + list[i].DOB + @"</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style=""float:left; width:460px; padding:15px; margin:0 10px; border:1px solid #ccc; height: 260px;"">
                                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                            <tr>
                                                <td width=""100%"" valign=""top"">
                                                    <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""  style=""font-size:12px;"">
                                                        <tr>
                                                            <td width=""600"" valign=""top"">
                                                                <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""font-size:12px;"">
                                                                    <tr>
                                                                        <td style=""padding:5px 5px 5px 0"" width=""40%""><strong>Date of Admission</strong></td>
                                                                        <td style=""padding:5px;"" width=""5%"">:</td>
                                                                        <td style=""padding:5px"" width=""55%"">" + list[i].EnrollmentGrantDate + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px 5px 5px 0"" width=""40%""><strong>Course Name</strong></td>
                                                                        <td style=""padding:5px;"" width=""5%"">:</td>
                                                                        <td style=""padding:5px"" width=""55%""><span>" + list[i].CourseApplied + @"</span>(" + list[i].streamCategory + @")<span></span></td>
                                                                    </tr>
                                                                  
                                                                    <tr>
                                                                        <td style=""padding:5px 5px 5px 0"" width=""40%""><strong>Mob.</strong></td>
                                                                        <td style=""padding:5px;"" width=""5%"">:</td>
                                                                        <td style=""padding:5px;"" width=""55%"">" + list[i].Mobile + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px 5px 5px 0"" width=""40%""><strong>Email</strong></td>
                                                                        <td style=""padding:5px;"" width=""5%"">:</td>
                                                                        <td style=""padding:5px;"" width=""55%"">" + list[i].Email + @"</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px 5px 5px 0"" width=""40%"" valign=""top""><strong>Address</strong></td>
                                                                        <td style=""padding:5px;"" width=""5%"" valign=""top"">:</td>
                                                                        <td style=""padding:5px"" width=""55%"" valign=""top""><div style=""min-height:50px"">" + list[i].Address + @"</div></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width=""40"" valign=""top"">
                                                                            <img src=""http://collegeportal.mungeruniversity.ac.in/images/PrincipalSignature/" + list[i].CollegeCode + @".jpg"" class=""imgstyle"" width=""180"" height=""40"" onerror=""this.src = '../../../images/white_img.png';"">
                                                                        </td>
                                                                        <td style=""padding:5px;"">&nbsp;</td>
                                                                        <td width=""40"" align=""right"" valign=""top"">
                                                                            <img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + list[i].stsign + @""" class=""imgstyle"" width=""180"" height=""40"" onerror=""this.src = '../../images/white_img.png';"">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style=""padding:5px 5px 5px 0"">Principal&rsquo;s  Signature </td>
                                                                        <td style=""padding:5px;"">&nbsp;</td>
                                                                        <td style=""padding:5px"" align=""right"">Student’s Signature</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            </div>
                            ";
                    builder.Append(content);
                }
                // DownloadAdmitcard(builder.ToString());
                string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content=\"text/html;charset=utf-8\" /></head><body><div style='font-size: 25px;'><center></center></div><br />" + builder.ToString() + "</body></html>";
                //Div Content will be fetched from form data.                                                    
                string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
                string dataname = "GenerateIDCard";  // here we declare for filename for download
                if (string.IsNullOrEmpty(InpuData))
                    InpuData = "Some Error occured.Content not found.Please try again.";
                string appPath = HttpContext.Request.PhysicalApplicationPath;

                var htmlContent = InpuData.Replace("AppPath", appPath);
                var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

                if (string.IsNullOrEmpty(PageType))
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
                else
                {
                    if (PageType == "Landscape")
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                    else
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                }
                pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                pageMargins.Bottom = 05;
                pageMargins.Left = 05;
                pageMargins.Right = 05;
                pageMargins.Top = 05;
                pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
                var pdfBytes = pdfDoc.GeneratePdf(htmlContent);
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
                HttpContext.ApplicationInstance.CompleteRequest();
                // TempData["Admitcard"] = "Download Successfully !!!!";
                return RedirectToAction("GenerateIDCard");
                return View();


            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Generate ID Card method", eID.ToString());
                return RedirectToAction("GenerateIDCard");
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeRegistration(string id = "0")
        {
            Commn_master com = new Commn_master();
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            EmployeeRegisteration ob = new EmployeeRegisteration();
            var collegeid = 0;
            int usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                collegeid = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            }
            ViewBag.Designation = ob.getDesignationMaster(collegeid);
            State st = new State();
            ViewBag.CAState = st.GetStateListByCountryId(80.ToString());
            ViewBag.PAState = st.GetStateListByCountryId(80.ToString());

            ViewBag.FacultyType = ob.FacultyTypeList;
            var result = new EmployeeRegisteration();
            string enID = "";
            int eID = 0;
            try
            {

                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    result = ob.getdetailsByID(eID);
                    ViewBag.bloodgroupid = result.BloodGroup;
                    ViewBag.castid = result.CastCategory;
                    ViewBag.genderid = result.Gender;

                    ViewBag.titileid = result.title;
                    ViewBag.ftitileid = result.Ftitle;
                    ViewBag.Nationalityid = result.Nationality;
                    ViewBag.Religionid = result.Religion;
                    ViewBag.MotherTongueid = result.MotherTongue;
                    ViewBag.FacultyTypeid = result.facultyType;
                    ViewBag.Designationid = result.Designation;
                    ViewBag.updatebutton = true;
                    ViewBag.RoleType = result.RoleType;
                }
                else
                {
                    ViewBag.updatebutton = false;
                }
                return View(result);
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail get Method", eID.ToString());
            }



            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult EmployeeRegistration(EmployeeRegisteration emp, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {

            EmployeeRegisteration st = new EmployeeRegisteration();

            Commn_master com = new Commn_master();
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            EmployeeRegisteration ob = new EmployeeRegisteration();
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            int CID = 0;
            if (collegeid != "")
            {
                CID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            int usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            ViewBag.Designation = ob.getDesignationMaster(CID);
            State sta = new State();
            ViewBag.CAState = sta.GetStateListByCountryId(80.ToString());
            ViewBag.PAState = sta.GetStateListByCountryId(80.ToString());
            ViewBag.bloodgroupid = emp.BloodGroup;
            ViewBag.castid = emp.CastCategory;
            ViewBag.genderid = emp.Gender;
            ViewBag.titileid = emp.title;
            ViewBag.ftitileid = emp.Ftitle;
            ViewBag.Nationalityid = emp.Nationality;
            ViewBag.Religionid = emp.Religion;
            ViewBag.MotherTongueid = emp.MotherTongue;
            ViewBag.FacultyType = emp.FacultyTypeList;
            emp.Password = CommonSetting.CreatePassword(6);
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(emp.Password));
            emp.U_password = hashedBytes;
            string jsonstring = JsonConvert.SerializeObject(emp);
            try
            {

                if (photo != null)
                {

                    Stream st1 = photo.InputStream;
                    string name = Path.GetFileName(photo.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "College";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeePhoto_" + emp.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        emp.stphoto = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
                    }
                }
                if (sign != null)
                {

                    Stream st1 = sign.InputStream;
                    string name = Path.GetFileName(sign.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "College";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeeSign_" + emp.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        emp.stsign = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
                    }
                }

                if (collegeid != "")
                {
                    emp.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    emp.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                emp.Id = emp.hid;
                var result = st.Employee_registration(emp);

                if (result.status == true)
                {
                    TempData["StMessage"] = result.Message;
                    if (emp.Id == 0)
                    {
                        Email.SendEmailForUser_SignUp(result.Email, result.Password, result.FirstName, result.UserName);
                    }
                    return RedirectToAction("EmployeeRegistration");
                }

                else { TempData["StMessage"] = result.Message; }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "College Employee Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);

            }
            return View(emp);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeList()
        {
            EmployeeRegisteration ob = new EmployeeRegisteration();
            var collegeId = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            }
            ViewBag.Designation = ob.getDesignationMaster(collegeId);
            ViewBag.FacultyType = ob.FacultyTypeList;
            return View();
        }
        public JsonResult State_Bind(string id)
        {
            State ob = new State();
            List<State> ds = ob.GetStateListByCountryId(id);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (State dr in ds)
            {
                statelist.Add(new SelectListItem { Text = dr.StateName, Value = dr.stateID.ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Bind_gender(int title)
        {
            Commn_master com = new Commn_master();
            if (title == Convert.ToInt32(CommonSetting.Commonid.Mr))
            {
                var aa = com.getcommonMaster("malegender");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var aa = com.getcommonMaster("femalegender");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult Bind_ftitle(int title)
        {
            Commn_master com = new Commn_master();
            if (title == Convert.ToInt32(CommonSetting.Commonid.Mr))
            {
                var aa = com.getcommonMaster("maleftile");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var aa = com.getcommonMaster("femaleftitle");
                return Json(aa, JsonRequestBehavior.AllowGet);
            }

        }

        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeDocumentUpload(string id = "", string Uid = "", string edit = "")
        {
            Employee_DocumentUpload ob = new Employee_DocumentUpload();
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            var collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            ViewBag.UserList = ob.GetEmpmenuList(collegeId);
            ViewBag.DocumentType1 = ob.GetDocumentList("DocListType1");
            ViewBag.DocumentType2 = ob.GetDocumentList("DocListType2");
            ViewBag.DocumentType = ob.GetDocumentList("DocumentList");
            ViewBag.dropdown = false;
            int eID = 0;
            List<Employee_DocumentUpload> obyear = new List<Employee_DocumentUpload>();
            for (int i = System.DateTime.Now.Year; i >= 1980; i--)
            {
                obyear.Add(new Employee_DocumentUpload { ID = i, year = i.ToString() });
            }
            ViewBag.year = obyear;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    eID = Convert.ToInt32(EncriptDecript.Decrypt(id));
                    EmployeeRegisteration emp = new EmployeeRegisteration();
                    var result = emp.getdetailsByEID(eID);
                    ViewBag.EID = result.EID;
                    ViewBag.ID = id;
                    ViewBag.Fullname = result.FirstName;
                    ViewBag.dropdown = true;
                    ViewBag.Isuploaded = emp.getdetailsofDocument(eID);
                    ViewBag.DocumentType1 = ob.GetDocumentList("DocListforType1", Convert.ToInt32(result.EID));
                    ViewBag.DocumentType2 = ob.GetDocumentList("DocListforType2", Convert.ToInt32(result.EID));
                    ViewBag.yearnameID = '0';
                    if (Uid != "0" && Uid.Length > 0)
                    {
                        ViewBag.dropdown = false;
                        ViewBag.ID = id;
                        eID = Convert.ToInt32(EncriptDecript.Decrypt(Uid));
                        Employee_DocumentUpload obj = ob.GetByID(eID);
                        ViewBag.EID = obj.EID;
                        ViewBag.Fullname = obj.Fullname;
                        ViewBag.yearnameID = obj.PassingYear;
                        return View(obj);
                    }
                    if (ViewBag.Isuploaded)
                    {
                        if (edit == "")
                        {
                            return RedirectToAction("DocumentListEmployee/" + id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "EmployeeDocumentUpload get Method Edit /Add", eID.ToString());
            }
            return View();
        }

        public ActionResult DeleteEmployeeDocument(string Uid = "", string id = "")
        {
            Employee_DocumentUpload ob = new Employee_DocumentUpload();

            int eID = 0;
            try
            {
                if (Uid != "0" && Uid.Length > 0)
                {
                    ViewBag.dropdown = false;
                    eID = Convert.ToInt32(EncriptDecript.Decrypt(Uid));
                    Employee_DocumentUpload emp = ob.DeleteDocumentByID(eID);
                    TempData["Msg"] = emp.Message;
                    return RedirectToAction("DocumentListEmployee/" + id);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "EmployeeDocument  Delete Method ", eID.ToString());
                return RedirectToAction("DocumentListEmployee/" + id);
            }
            return View();
        }
        public JsonResult DocumentID_bindDynamic(string res = "", int id = 0)
        {
            Employee_DocumentUpload obj = new Employee_DocumentUpload();
            List<Employee_Document_Master> list = new List<Employee_Document_Master>();
            list = obj.GetDocumentBYID(res, 1, id);
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (Employee_Document_Master dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.DocTitle, Value = dr.DocID.ToString() });
            }
            ViewBag.DocumentType1 = slist;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DocumentID_bind()
        {
            Employee_DocumentUpload obj = new Employee_DocumentUpload();
            List<Employee_Document_Master> list = new List<Employee_Document_Master>();
            list = obj.GetDocumentList("DocListType1");
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (Employee_Document_Master dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.DocTitle, Value = dr.DocID.ToString() });
            }
            ViewBag.DocumentType1 = slist;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DocumentID_bindDynamictype2(string res = "", int id = 0)
        {
            Employee_DocumentUpload obj = new Employee_DocumentUpload();
            List<Employee_Document_Master> list = new List<Employee_Document_Master>();
            list = obj.GetDocumentBYID(res, 2, id);
            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (Employee_Document_Master dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.DocTitle, Value = dr.DocID.ToString() });
            }
            ViewBag.DocumentType2 = slist;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult save_Documentstype1()
        {
            if (Request.Form.Count > 0)
            {
                Employee_DocumentUpload doc = new Employee_DocumentUpload();
                doc.narrationList = Request.Form["narrationList"];
                doc.Documentidlist = Request.Form["Documentidlist"];
                doc.filevalueslist = Request.Form["filevalueslist"];
                doc.EID = Convert.ToInt32(Request.Form["UserID"] == "" ? "0" : Request.Form["UserID"]);
                var docidlist = doc.Documentidlist.Split(',');
                var filelist = doc.filevalueslist.Split(',');
                var narrlist = doc.narrationList.Split(',');
                Employee_DocumentUpload result = new Employee_DocumentUpload();
                string jsonstring = JsonConvert.SerializeObject(doc);

                if (Request.Files.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            Employee_DocumentUpload ob = new Employee_DocumentUpload();
                            if (docidlist[i] == "")
                            {
                                break;
                            }
                            ob.DocType = Convert.ToInt32(docidlist[i]); ;
                            ob.Narration = narrlist[i];
                            ob.EID = doc.EID;

                            if (Request.Files.GetKey(i) == "photo")
                            {
                                HttpPostedFileBase fileUpload = Request.Files.Get(i);
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                }
                                Stream st1 = fileUpload.InputStream;
                                string name = Path.GetFileName(fileUpload.FileName);
                                try
                                {
                                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                    string s3DirectoryName = "College";
                                    string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeeDocument_" + ob.DocType + '_' + doc.EID + @name;
                                    s3FileName = s3FileName.Replace(" ", "");
                                    ob.DocumentURl = s3FileName;
                                    bool a;
                                    AmazonUploader myUploader = new AmazonUploader();
                                    a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                                }
                                catch (Exception ex)
                                {
                                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
                                }
                                var collegeid = ClsLanguage.GetCookies("ENNBCLID");
                                if (collegeid != "")
                                {
                                    ob.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
                                }
                                var insertby = ClsLanguage.GetCookies("ENNBUID");
                                if (insertby != "")
                                {
                                    ob.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                                }
                                result = doc.SaveDocumentType1(ob);
                                if (result.Status)
                                {
                                    continue;
                                }



                            }
                            else
                            {
                                Employee_DocumentUpload ob1 = new Employee_DocumentUpload();
                                ob1.Message = "Error occurred. Error details: ";
                                return Json(ob1, JsonRequestBehavior.AllowGet);
                            }


                        }
                        return Json(result, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
                        Employee_DocumentUpload ob3 = new Employee_DocumentUpload();
                        ob3.Message = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Employee_DocumentUpload ob4 = new Employee_DocumentUpload();
                    ob4.Message = "Error occurred. Error details: ";
                    return Json(ob4, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Employee_DocumentUpload logmsg = new Employee_DocumentUpload();
                logmsg.Message = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }






        }
        [HttpPost]
        public JsonResult save_Documentstype2()
        {
            if (Request.Form.Count > 0)
            {
                Employee_DocumentUpload doc = new Employee_DocumentUpload();
                doc.narrationList = Request.Form["narrationList"];
                doc.Documentidlist = Request.Form["Documentidlist"];
                doc.filevalueslist = Request.Form["filevalueslist"];
                doc.YearvaluesList = Request.Form["YearvaluesList"];
                doc.GradevaluesList = Request.Form["GradevaluesList"];
                doc.EID = Convert.ToInt32(Request.Form["UserID"] == "" ? "0" : Request.Form["UserID"]);
                var docidlist = doc.Documentidlist.Split(',');
                var filelist = doc.filevalueslist.Split(',');
                var YearList = doc.YearvaluesList.Split(',');
                var GradeList = doc.GradevaluesList.Split(',');
                var narrlist = doc.narrationList.Split(',');
                Employee_DocumentUpload result = new Employee_DocumentUpload();
                string jsonstring = JsonConvert.SerializeObject(doc);

                if (Request.Files.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            Employee_DocumentUpload ob = new Employee_DocumentUpload();
                            if (docidlist[i] == "")
                            {
                                break;
                            }
                            ob.DocType = Convert.ToInt32(docidlist[i]); ;
                            ob.Narration = narrlist[i];
                            ob.EID = doc.EID;
                            ob.Grade = GradeList[i];
                            ob.PassingYear = YearList[i];
                            if (Request.Files.GetKey(i) == "filetype")
                            {
                                HttpPostedFileBase fileUpload = Request.Files.Get(i);
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                }
                                Stream st1 = fileUpload.InputStream;
                                string name = Path.GetFileName(fileUpload.FileName);
                                try
                                {
                                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                    string s3DirectoryName = "College";
                                    string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeeDocument_" + ob.DocType + '_' + doc.EID + @name;
                                    s3FileName = s3FileName.Replace(" ", "");
                                    ob.DocumentURl = s3FileName;
                                    bool a;
                                    AmazonUploader myUploader = new AmazonUploader();
                                    a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                                }
                                catch (Exception ex)
                                {
                                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType2" + jsonstring);
                                }
                                var collegeid = ClsLanguage.GetCookies("ENNBCLID");
                                if (collegeid != "")
                                {
                                    ob.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
                                }
                                var insertby = ClsLanguage.GetCookies("ENNBUID");
                                if (insertby != "")
                                {
                                    ob.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                                }
                                result = doc.SaveDocumentType1(ob);
                                if (result.Status)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                Employee_DocumentUpload ob1 = new Employee_DocumentUpload();
                                ob1.Message = "Error occurred. Error details: ";
                                return Json(ob1, JsonRequestBehavior.AllowGet);
                            }


                        }
                        return Json(result, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType2" + jsonstring);
                        Employee_DocumentUpload ob3 = new Employee_DocumentUpload();
                        ob3.Message = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    Employee_DocumentUpload ob4 = new Employee_DocumentUpload();
                    ob4.Message = "Error occurred. Error details: ";
                    return Json(ob4, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Employee_DocumentUpload logmsg = new Employee_DocumentUpload();
                logmsg.Message = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }

        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult DocumentListEmployee(string id = "")
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            Employee_DocumentUpload ob = new Employee_DocumentUpload();
            ViewBag.DocumentType1 = ob.GetDocumentList("DocumentList");
            string EmployeeName = "";
            if (id != "0" && id.Length > 0)
            {
                ViewBag.oneID = EncriptDecript.Encrypt("1");
                ViewBag.ID = id;
                EmployeeName = EncriptDecript.Decrypt(id);
            }
            else
            {
                return View();
            }

            Employee_DocumentUpload obj = new Employee_DocumentUpload();
            Employee_DocumentUploadList sub = new Employee_DocumentUploadList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.EmployeedocListByUser(EmployeeName, collegeID);

            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return View(sub.qlist);
        }
        public JsonResult Update_documentUpload()
        {

            if (Request.Form.Count > 0)
            {
                Employee_DocumentUpload doc = new Employee_DocumentUpload();
                doc.DocType = Convert.ToInt32(Request.Form["DocType"]);
                doc.Narration = CommonSetting.RemoveSpecialChars(Request.Form["Narration"]);
                doc.DocumentURl = Request.Form["editfile"];
                doc.EID = Convert.ToInt32(Request.Form["UserID"] == "" ? "0" : Request.Form["UserID"]);
                doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.Grade = Request.Form["Grade"];
                doc.PassingYear = Request.Form["Year"];
                Employee_DocumentUpload result = new Employee_DocumentUpload();
                string jsonstring = JsonConvert.SerializeObject(doc);
                try
                {


                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                if (Request.Files.GetKey(i) == "photo")
                                {
                                    HttpPostedFileBase fileUpload = Request.Files.Get(i);
                                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                    {
                                        string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                    }
                                    Stream st1 = fileUpload.InputStream;
                                    string name = Path.GetFileName(fileUpload.FileName);
                                    try
                                    {
                                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                        string s3DirectoryName = "College";
                                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EmployeeDocument_" + doc.DocType + '_' + doc.EID + @name;
                                        s3FileName = s3FileName.Replace(" ", "");
                                        doc.DocumentURl = s3FileName;
                                        bool a;
                                        AmazonUploader myUploader = new AmazonUploader();
                                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                                    }
                                    catch (Exception ex)
                                    {
                                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload Edit", jsonstring);
                                    }

                                }
                            }
                        }

                        catch (Exception ex)
                        {

                            CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload Edit", jsonstring);
                            Employee_DocumentUpload ob3 = new Employee_DocumentUpload();
                            ob3.Message = "Error occurred. Error details: " + ex.Message;
                            return Json(ob3, JsonRequestBehavior.AllowGet);
                        }
                    }
                    var collegeid = ClsLanguage.GetCookies("ENNBCLID");
                    if (collegeid != "")
                    {
                        doc.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
                    }
                    var insertby = ClsLanguage.GetCookies("ENNBUID");
                    if (insertby != "")
                    {
                        doc.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                    }
                    result = doc.EditDocumentupload(doc);
                    return Json(result, JsonRequestBehavior.AllowGet);

                    //else
                    //{
                    //    Employee_DocumentUpload logmsg = new Employee_DocumentUpload();
                    //    logmsg.Message = "Error occurred. Error details: ";
                    //    return Json(logmsg, JsonRequestBehavior.AllowGet);
                    //}
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload Edit", jsonstring);
                    Employee_DocumentUpload logmsg = new Employee_DocumentUpload();
                    logmsg.Message = "Error occurred. Error details: ";
                    return Json(logmsg, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                Employee_DocumentUpload logmsg = new Employee_DocumentUpload();
                logmsg.Message = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }





        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ApplyLeave()
        {
            Employee_DocumentUpload ob = new Employee_DocumentUpload();
            var usertype = 0;
            var collegeId = 0;
            var UID = 0;
            EmployeeLeave_Management emp = new EmployeeLeave_Management();
            ViewBag.LeaveType = emp.getleaveTypeList();
            try
            {

                if (ClsLanguage.GetCookies("ENNBUserType") != null)
                {
                    usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));

                }
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));

                }
                if (ClsLanguage.GetCookies("ENNBUID") != null)
                {
                    UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

                }
                ViewBag.usertype = (usertype == Convert.ToInt32(CommonSetting.UserType.Admin) ? true : false);
                ViewBag.UserList = ob.GetEmpmenuListAdmin(collegeId);
                ViewBag.UserListEmp = ob.GetEmpmenuListUser(collegeId, UID);
                var result = emp.GetByEmployeeID(UID);
                emp.Name = result.Name;
                emp.EmployeeID = UID;
                emp.halftime = 1;
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : Apply Leave get Method", UID.ToString());
                return View();
            }

            return View(emp);
        }
        public JsonResult AddNewLeave(EmployeeLeave_Management ob)
        {
            Employee_DocumentUpload emp = new Employee_DocumentUpload();
            EmployeeLeave_Management obj = new EmployeeLeave_Management();
            var Id = 0;
            string jsonstring = "";
            var usertype = 0;
            var collegeId = 0;
            var UID = 0;

            if (ob.Day == "0.5")
            {
                if (ob.Fromdate != ob.Todate)
                {
                    ob.Msg = "from date and to date should be equal";
                    return Json(ob, JsonRequestBehavior.AllowGet);
                }
            }
            ViewBag.LeaveType = obj.getleaveTypeList();
            try
            {

                if (ClsLanguage.GetCookies("ENNBUserType") != null)
                {
                    usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));

                }
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));

                }
                if (ClsLanguage.GetCookies("ENNBUID") != null)
                {
                    UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

                }
                ViewBag.usertype = (usertype == Convert.ToInt32(CommonSetting.UserType.Admin) ? true : false);
                ViewBag.UserList = emp.GetEmpmenuListAdmin(collegeId);
                ViewBag.UserListEmp = emp.GetEmpmenuListUser(collegeId, UID);
                ob.CollegeID = collegeId;
                ob.InsertedBy = UID;
                ob.LeaveStatus = (usertype == Convert.ToInt32(CommonSetting.UserType.Admin) ? 1 : 0);
                jsonstring = JsonConvert.SerializeObject(ob);
                var result = obj.AddLeave(ob);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add/Edit New Leave", jsonstring);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }




        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CheckLeaveStatus()
        {
            List<SelectListItem> obj = new List<SelectListItem>();
            obj.Add(new SelectListItem { Text = "--Select Year--", Value = "1951" });
            for (int i = 2019; i >= 1995; i--)
            {
                obj.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.Year = obj;
            List<SelectListItem> ob = new List<SelectListItem>();
            ob.Add(new SelectListItem { Value = "13", Text = "--Select Month---" });
            ob.Add(new SelectListItem { Value = "1", Text = "Janurary" });
            ob.Add(new SelectListItem { Value = "2", Text = "Feburary" });
            ob.Add(new SelectListItem { Value = "3", Text = "March" });
            ob.Add(new SelectListItem { Value = "4", Text = "April" });
            ob.Add(new SelectListItem { Value = "5", Text = "May" });
            ob.Add(new SelectListItem { Value = "6", Text = "June" });
            ob.Add(new SelectListItem { Value = "7", Text = "July" });
            ob.Add(new SelectListItem { Value = "8", Text = "August" });
            ob.Add(new SelectListItem { Value = "9", Text = "September" });
            ob.Add(new SelectListItem { Value = "10", Text = "October" });
            ob.Add(new SelectListItem { Value = "11", Text = "November" });
            ob.Add(new SelectListItem { Value = "12", Text = "December" });
            ViewBag.Month = ob;
            EmployeeLeave_Management emp = new EmployeeLeave_Management();
            int UID = 0;
            if (ClsLanguage.GetCookies("ENNBUID") != null)
            {
                UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

            }
            var result = emp.GetByEmployeeID(UID);
            ViewBag.Name = result.Name;
            ViewBag.CuYear = DateTime.Now.Year.ToString();
            ViewBag.CuMonth = DateTime.Now.Month.ToString();
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult VerifyLeave()
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            List<SelectListItem> obj = new List<SelectListItem>();
            obj.Add(new SelectListItem { Text = "--Select Year--", Value = "" });
            for (int i = 2019; i >= 1995; i--)
            {
                obj.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.Year = obj;
            List<SelectListItem> ob = new List<SelectListItem>();
            ob.Add(new SelectListItem { Value = "", Text = "--Select Month---" });
            ob.Add(new SelectListItem { Value = "1", Text = "Janurary" });
            ob.Add(new SelectListItem { Value = "2", Text = "Feburary" });
            ob.Add(new SelectListItem { Value = "3", Text = "March" });
            ob.Add(new SelectListItem { Value = "4", Text = "April" });
            ob.Add(new SelectListItem { Value = "5", Text = "May" });
            ob.Add(new SelectListItem { Value = "6", Text = "June" });
            ob.Add(new SelectListItem { Value = "7", Text = "July" });
            ob.Add(new SelectListItem { Value = "8", Text = "August" });
            ob.Add(new SelectListItem { Value = "9", Text = "September" });
            ob.Add(new SelectListItem { Value = "10", Text = "October" });
            ob.Add(new SelectListItem { Value = "11", Text = "November" });
            ob.Add(new SelectListItem { Value = "12", Text = "December" });
            ViewBag.Month = ob;

            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult PendingLeaveDetail(string id = "")
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string enID = "";
            int eID = 0;
            var collegeId = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                }
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));

                }
                Employee_DocumentUpload ob = new Employee_DocumentUpload();
                EmployeeLeave_Management emp = new EmployeeLeave_Management();
                ViewBag.LeaveType = emp.getleaveTypeList();
                ViewBag.UserList = ob.GetEmpmenuListAdmin(collegeId);

                var result = emp.getLeaveByID(eID);
                ViewBag.Leave = result.LeaveType;
                return View(result);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Pending LeaveDetail Get Method", eID.ToString());
                return View("PendingLeaveDetail/");
            }
            return View();
        }
        [HttpPost]
        public JsonResult ApproveLeave(string id = "")
        {
            EmployeeLeave_Management ob = new EmployeeLeave_Management();
            string CollegeName = ClsLanguage.GetCookies("NBCollegeName");
            string enID = "";
            int eID = 0;
            int UID = 0;
            try
            {
                if (id != "")
                {

                    if (id != "0" && id.Length > 0)
                    {

                        enID = EncriptDecript.Decrypt(id);
                        if (enID != "")
                        {
                            eID = Convert.ToInt32(enID);
                        }
                    }
                    if (ClsLanguage.GetCookies("ENNBUID") != null)
                    {
                        UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

                    }
                    var result = ob.Verifyleave(eID, UID);
                    if (result.Status)
                    {

                        Email.SendEmailForLeaveStatus(result.Email, result.Name, "Approved", CollegeName);
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var obj = new EmployeeLeave_Management();
                        obj.Status = false;
                        obj.Msg = "Something Wrong Happen!!!";

                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var obj = new EmployeeLeave_Management();
                    obj.Status = false;
                    obj.Msg = "Something Wrong Happen!!!";

                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Approveleave", enID);
                return Json(ob, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult RejectLeaveEmp(string id = "")
        {
            EmployeeLeave_Management ob = new EmployeeLeave_Management();
            string CollegeName = ClsLanguage.GetCookies("NBCollegeName");
            string enID = "";
            int eID = 0;
            int UID = 0;
            try
            {
                if (id != "")
                {

                    if (id != "0" && id.Length > 0)
                    {

                        enID = EncriptDecript.Decrypt(id);
                        if (enID != "")
                        {
                            eID = Convert.ToInt32(enID);
                        }
                    }
                    if (ClsLanguage.GetCookies("ENNBUID") != null)
                    {
                        UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

                    }
                    var result = ob.Rejectleave(eID, UID);

                    if (result.Status)
                    {

                        Email.SendEmailForLeaveStatus(result.Email, result.Name, "Rejected", CollegeName);
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var obj = new EmployeeLeave_Management();
                        obj.Status = false;

                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var obj = new EmployeeLeave_Management();
                    obj.Status = false;
                    obj.Msg = "Something Wrong Happen!!!";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Rejectleave", enID);
                ob.Msg = "Something Wrong Happen!!!";
                return Json(ob, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult CheckuserMobile(string mobile = "")
        {
            UserLogin st = new UserLogin();
            var obj = st.Checkuserbymobile(mobile);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckuserEmail(string email = "")
        {
            UserLogin st = new UserLogin();
            var obj = st.Checkuserbyemail(email);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingLeaveDelete(string id = "")
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string enID = "";
            int eID = 0;
            var collegeId = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                }

                EmployeeLeave_Management emp = new EmployeeLeave_Management();


                var result = emp.deleteLeaveByID(eID);
                TempData["Msg"] = result.Msg;
                ViewBag.Leave = result.LeaveType;
                return RedirectToAction("CheckLeaveStatus");
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Pending Leave delete Method", eID.ToString());
                return View("CheckLeaveStatus/");
            }

        }
        public ActionResult StudentDetailIndex(string id = "")
        {
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    ViewBag.ID = id;
                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    //TempData["ID"] = eID;

                    //TempData.Keep("ID");
                }
                return View();
            }

            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Details Index page Get Method", eID.ToString());
                return RedirectToAction("FinalStudentList");
            }
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentDetailsEdit(string id = "", string test = "")
        {
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    ViewBag.ID = id;
                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }

                    Commn_master com = new Commn_master();
                    ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
                    ViewBag.Educationtype = com.getcommonMaster("EducationType");
                    // ViewBag.Gender = com.getcommonMaster("Gender");
                    // ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
                    ViewBag.bloodgroup = com.Getbloodgroup("Select");
                    Country cont = new Country();
                    ViewBag.country = cont.GetAllCountries();
                    ViewBag.Pcountry = cont.GetAllCountries();
                    ViewBag.stitle = com.getcommonMaster("Title");
                    //ViewBag.ftitle = com.getcommonMaster("TitleM");
                    ViewBag.Nationality = com.getcommonMaster("Nationality");
                    ViewBag.Religion = com.getcommonMaster("Religion");

                    StudentLogin tblST = new StudentLogin();
                    var obj = tblST.BasicDetailByID(eID);
                    if (obj.Gender == Convert.ToInt32(CommonSetting.Commonid.Femalegender))
                    {
                        ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");

                    }
                    else
                    {
                        ViewBag.CasteCategory = com.getcommonMaster("CastwithoutWBC");
                    }
                    if (obj.title == Convert.ToInt32(CommonSetting.Commonid.Mr))
                    {
                        ViewBag.ftitle = com.getcommonMaster("maleftile");
                        ViewBag.Gender = com.getcommonMaster("malegender");
                    }
                    else
                    {
                        ViewBag.ftitle = com.getcommonMaster("femaleftitle");
                        ViewBag.Gender = com.getcommonMaster("femalegender");
                    }
                    ViewBag.App = obj.ApplicationNo;
                    ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
                    ViewBag.Course = com.getcommonMaster("Course", obj.EducationType);
                    ViewBag.Stream = com.getcommonMaster("Stream", obj.CourseCategory);
                    State st = new State();
                    ViewBag.CAState = st.GetStateListByCountryId(obj.CA_Country.ToString());
                    ViewBag.PAState = st.GetStateListByCountryId(obj.PA_Country.ToString());
                    ViewBag.bloodgroupid = obj.BloodGroup;
                    ViewBag.castid = obj.CastCategory;
                    ViewBag.genderid = obj.Gender;
                    ViewBag.eduid = obj.EducationType;
                    ViewBag.titileid = obj.title;
                    ViewBag.ftitileid = obj.Ftitle;
                    ViewBag.Nationalityid = obj.Nationality;
                    ViewBag.Religionid = obj.Religion;
                    ViewBag.MotherTongueid = obj.MotherTongue;
                    ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

                    return View(obj);
                }
                else
                {
                    // TempData["StMessage"] = "Invaild Access";
                    return RedirectToAction("FinalStudentList/");
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Edit Student Details Get Method", eID.ToString());
                return RedirectToAction("FinalStudentList/");
            }

        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult StudentDetailsEdit(DataLayer.Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo)
        {
            CommonMethod.WritetoNotepad(null, HttpContext.Request.Url.AbsolutePath, "first Student Details Edit post Method");
            StudentLogin st = new StudentLogin();
            Commn_master com = new Commn_master();
            string jsonstring = JsonConvert.SerializeObject(objlogin);
            try
            {
                if (objlogin.Gender == Convert.ToInt32(CommonSetting.Commonid.Femalegender))
                {
                    ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");

                }
                else
                {
                    ViewBag.CasteCategory = com.getcommonMaster("CastwithoutWBC");
                }
                if (objlogin.title == Convert.ToInt32(CommonSetting.Commonid.Mr))
                {
                    ViewBag.ftitle = com.getcommonMaster("maleftile");
                    ViewBag.Gender = com.getcommonMaster("malegender");
                }
                else
                {
                    ViewBag.ftitle = com.getcommonMaster("femaleftitle");
                    ViewBag.Gender = com.getcommonMaster("femalegender");
                }
                if (objlogin.Gender == 0)
                {
                    return View(objlogin);
                }
                //if (objlogin.CastCategory == 0)
                //{
                //    return View(objlogin);
                //}
                if (objlogin.Ftitle == 0)
                {
                    return View(objlogin);
                }
                if (objlogin.PA_State == 0)
                {
                    return View(objlogin);
                }
                if (photo != null)
                {

                    Stream st1 = photo.InputStream;
                    string name = Path.GetFileName(photo.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Photoandsign";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentPhoto_" + objlogin.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        objlogin.stphoto = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("ENNBUID") + "   " + jsonstring);
                    }
                }
                if (sign != null)
                {

                    Stream st1 = sign.InputStream;
                    string name = Path.GetFileName(sign.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Photoandsign";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentSign_" + objlogin.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        objlogin.stsign = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {
                        // CommonMethod.PrintLog(ex);
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Detail Edit post Method", ClsLanguage.GetCookies("ENNBUID") + "   " + jsonstring);
                    }
                }
                int UID = 0;
                if (ClsLanguage.GetCookies("ENNBUID") != null)
                {
                    UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

                }
                objlogin.InsertedBy = UID;

                var result = st.Student_DetailsUpdate(objlogin);
                if (result.status == true)
                {
                    TempData["StMessage"] = result.Message;
                    return RedirectToAction("StudentDetailsEdit");
                }

                else { TempData["StMessage"] = result.Message; }
            }
            catch (Exception ex)
            {
                //TempData["StMessage"] = ex.Message;
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Details Edit post Method", ClsLanguage.GetCookies("ENNBUID") + "   " + jsonstring);

            }
            return View(objlogin);
        }
        public ActionResult StudentSubjectsEdit(string id = "")
        {
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    ViewBag.ID = id;
                    Student_Admission_Choicesubject ob = new Student_Admission_Choicesubject();
                    Student_Admission_Choicesubject result = ob.getSubjectChoiceDetail(eID);
                    StudentLogin tblST = new StudentLogin();
                    var obj = tblST.BasicDetailByID(eID);
                    ViewBag.Name = obj.Name;
                    BL_StreamMaster objStream = new BL_StreamMaster();
                    if (result.sessionid == 39)
                    {
                        ViewBag.subsidiary1 = objStream.getcollegesubjects_old_for_session2018(result.SID, 11, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);
                        ViewBag.subcomposition1 = objStream.getcollegesubjects_old_for_session2018(result.SID, 13, result.CollegeID, 0, 0, 0, result.sessionid);
                        ViewBag.subsidiary2 = objStream.getcollegesubjects_old_for_session2018(result.SID, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);
                        ViewBag.subcomposition2 = objStream.getcollegesubjects_old_for_session2018(result.SID, 14, result.CollegeID, 0, 0, result.Compulsory1_subjectid, result.sessionid);
                    }
                    else
                    {
                        //ViewBag.subsidiary2 = objStream.getcollegesubjects(result.SID, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid);
                        //ViewBag.subsidiary1 = objStream.getcollegesubjects(result.SID, 11, result.CollegeID, result.SubjectID);
                        ViewBag.stream = objStream.getcollegesubjectscopy(result.SID, 141, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);
                        //ViewBag.MDC = stream.Where(x => x.SubjectCategory == "MDC").Select(y => new { StreamCategoryID = y.Substreamcategoryid, streamCategory = y.SubjectName});
                        //ViewBag.AEC = stream.Where(x => x.SubjectCategory == "AEC").Select(y => new { StreamCategoryID = y.Substreamcategoryid, streamCategory = y.SubjectName });
                        //ViewBag.SEC = stream.Where(x => x.SubjectCategory == "SEC").Select(y => new { StreamCategoryID = y.Substreamcategoryid, streamCategory = y.SubjectName });
                        //ViewBag.VAC = stream.Where(x => x.SubjectCategory == "VAC").Select(y => new { StreamCategoryID = y.Substreamcategoryid, streamCategory = y.SubjectName });
                        if (obj.CourseCategory == 3)
                        {
                            //result = objStream.getcollegesubjects(obj.Id, 20, id, 0, 0, 0, obj.CourseCategory);//bcom honours
                            ViewBag.Minor = objStream.getcollegesubjects(obj.Id, 145, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bcom
                            //ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bcom
                        }
                        else if (obj.CourseCategory == 2)
                        {
                            // result = objStream.getcollegesubjects(obj.Id, 24, id, 0, 0, 0, obj.CourseCategory);//bsc honours
                            ViewBag.Minor = objStream.getcollegesubjects(obj.Id, 145, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bsc
                            //ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 27, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bsc
                        }
                        else
                        {
                            // result = objStream.getcollegesubjects(obj.Id, 10, id, 0, 0, 0, obj.CourseCategory);//ba honours
                            //ViewBag.Minor = stream.Where(x => x.SubjectCategory == "Minor").Select(y => new { StreamCategoryID = y.Substreamcategoryid, streamCategory = y.SubjectName });
                            //ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 29, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//ba
                        }
                    }

                    ViewBag.SubjectIds = result.SubjectIds.Split(',').ToList();
                    ViewBag.Minor1 = result.MinorId;
                    ViewBag.MDC1 = result.MDCId;
                    ViewBag.AEC1 = result.AECId;
                    ViewBag.SEC1 = result.SECId;
                    ViewBag.VAC1 = result.VACId;
                    return View(result);
                }
                else
                {
                    ViewBag.subsidiary1 = "";
                    ViewBag.subcomposition1 = "";
                    ViewBag.subsidiary2 = "";
                    ViewBag.subcomposition2 = "";
                    return View();
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Edit Student Details Get Method", eID.ToString());
                return RedirectToAction("FinalStudentList/");
            }
        }
        [HttpPost]
        public ActionResult StudentSubjectsEdit(ChooseSubjectCopy sub)
        {
            string enID = "";
            int eID = 0;
            try
            {


                enID = EncriptDecript.Decrypt(sub.SID);
                if (enID != "")
                {
                    eID = Convert.ToInt32(enID);
                }
                Student_Admission_Choicesubject ob = new Student_Admission_Choicesubject();
                Student_Admission_Choicesubject result = ob.getSubjectChoiceDetail(eID);
                sub.MajorID = result.SubjectID;
                sub.StudentId = eID;
                StudentLogin tblST = new StudentLogin();
                var obj = tblST.BasicDetailByID(eID);
                ViewBag.Name = obj.Name;

                //sub.SID = eID;

                BL_StreamMaster objStream = new BL_StreamMaster();

                //if (result.sessionid == 39)
                //{
                //    ViewBag.subsidiary1 = objStream.getcollegesubjects_old_for_session2018(result.SID, 11, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);
                //    ViewBag.subcomposition1 = objStream.getcollegesubjects_old_for_session2018(result.SID, 13, result.CollegeID, 0, 0, 0, result.sessionid);
                //    ViewBag.subsidiary2 = objStream.getcollegesubjects_old_for_session2018(result.SID, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);
                //    ViewBag.subcomposition2 = objStream.getcollegesubjects_old_for_session2018(result.SID, 14, result.CollegeID, 0, 0, result.Compulsory1_subjectid, result.sessionid);
                //}
                //else
                //{
                //ViewBag.MDC = objStream.getcollegesubjects(result.SID, 141, result.CollegeID, 0, 0, 0, result.sessionid);
                //ViewBag.AEC = objStream.getcollegesubjects(result.SID, 142, result.CollegeID, 0, 0, result.Compulsory1_subjectid, result.sessionid);
                //ViewBag.SEC = objStream.getcollegesubjects(obj.Id, 143, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bcom
                //ViewBag.VAC = objStream.getcollegesubjects(obj.Id, 144, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bcom
                //if (obj.CourseCategory == 3)
                //{
                //    //result = objStream.getcollegesubjects(obj.Id, 20, id, 0, 0, 0, obj.CourseCategory);//bcom honours
                //    ViewBag.Minor = objStream.getcollegesubjects(obj.Id, 145, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bcom
                //    //ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bcom
                //}
                //else if (obj.CourseCategory == 2)
                //{
                //    // result = objStream.getcollegesubjects(obj.Id, 24, id, 0, 0, 0, obj.CourseCategory);//bsc honours
                //    ViewBag.Minor = objStream.getcollegesubjects(obj.Id, 145, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bsc
                //    //ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 27, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bsc
                //}
                //else
                //{
                //    // result = objStream.getcollegesubjects(obj.Id, 10, id, 0, 0, 0, obj.CourseCategory);//ba honours
                //    ViewBag.Minor = objStream.getcollegesubjects(obj.Id, 145, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//ba
                //    //ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 29, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//ba
                //}
                //}
                //sub.sessionid = result.sessionid;
                //if (sub.AECId == 0)
                //{
                //    TempData["StMessage"] = "Please Select AEC first";
                //    return View(result);
                //}
                //if (sub.MDCId == 0)
                //{
                //    TempData["StMessage"] = "Please Select MDC Second";
                //    return View(result);
                //}
                //if (sub.SECId == 0)
                //{
                //    TempData["StMessage"] = "Please Select SEC first";
                //    return View(result);
                //}
                //if (sub.MinorId == 0)
                //{
                //    TempData["StMessage"] = "Please Select Minor first";
                //    return View(result);
                //}
                //if (sub.VACId == 0)
                //{
                //    TempData["StMessage"] = "Please Select VAC first";
                //    return View(result);
                //}
                var res = ob.updateSubjectChoiceDetailsCopy(sub);
                //TempData["StMessage"] = res.Msg;
                //sub.CollegeName = result.CollegeName;
                //sub.HonSubName = result.HonSubName;
                //if (res.Status)
                //{
                //    return RedirectToAction("", "Student/Home/Index", new { area = "" });

                //}
                //else
                //{
                //    return View(sub);
                //}
                return Json(new { data = res, success = true });

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Edit Student Details Get Method", eID.ToString());
                return RedirectToAction("FinalStudentList/");
            }


        }

        public JsonResult getcollegesubsidiary2(int collegeid, int subjectid, int subsidiary1, string id)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin objs = new StudentLogin();
                    var objl = objs.BasicDetailByID(eID);
                    BL_StreamMaster objStream = new BL_StreamMaster();
                    if (objl.session == 39)
                    {
                        var result = objStream.getcollegesubjects_old_for_session2018(objl.Id, 12, collegeid, subjectid, subsidiary1,0, objl.session);
                        return Json(new { data = result, success = true });
                    }
                    else
                    {
                        var result = new List<BL_StreamMaster>();
                        //var result = objStream.getcollegesubjects(objl.Id, 12, collegeid, subjectid, subsidiary1);
                        if (objl.CourseCategory == 3)
                        {
                            result = objStream.getcollegesubjects(objl.Id, 12, collegeid, subjectid, subsidiary1, 0, objl.session);//bcom
                        }
                        else if (objl.CourseCategory == 2)
                        {
                            result = objStream.getcollegesubjects(objl.Id, 27, collegeid, subjectid, subsidiary1, 0, objl.session);//bsc
                        }
                        else
                        {
                            result = objStream.getcollegesubjects(objl.Id, 29, collegeid, subjectid, subsidiary1, 0, objl.session);//ba
                        }
                        return Json(new { data = result, success = true });
                    }
                    //var result = objStream.getcollegesubjects(objl.Id, 12, collegeid, subjectid, subsidiary1);
                    //return Json(new { data = result, success = true });
                }
                else
                {
                    return Json(new { data = obj, success = true });
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "getcollegesubsidiary2 Get Method", eID.ToString());
                // return RedirectToAction("StudentSubjectsEdit/");
                return Json(new { data = obj, success = true });
            }

        }
        public JsonResult getcollegecompulsory2(int collegeid, int compulsory1, string id)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            StudentLogin objs = new StudentLogin();
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    var objl = objs.BasicDetailByID(eID);
                    BL_StreamMaster objStream = new BL_StreamMaster();
                    if (objl.session == 39)
                    {
                        var result = objStream.getcollegesubjects_old_for_session2018(objl.Id, 14, collegeid, 0, 0, compulsory1, objl.session);
                        return Json(new { data = result, success = true });
                    }
                    else
                    {
                        var result = objStream.getcollegesubjects(objl.Id, 14, collegeid, 0, 0, compulsory1, objl.session);
                        return Json(new { data = result, success = true });
                    }

                }
                else
                {
                    return Json(new { data = obj, success = true });
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "getcollegecompulsory2 Get Method", eID.ToString());
                // return RedirectToAction("StudentSubjectsEdit/");
                return Json(new { data = obj, success = true });
            }
        }
        //[VerifyUrlFilterCollegeAttribute]
        //[HttpPost]
        //public ActionResult StudentSubjectsEdit(Student_Admission_Choicesubject sub)
        //{
        //    string enID = "";
        //    int eID = 0;
        //    try
        //    {
        //        if (sub.ID != "0" && sub.ID.Length > 0)
        //        {

        //            enID = EncriptDecript.Decrypt(sub.ID);
        //            if (enID != "")
        //            {
        //                eID = Convert.ToInt32(enID);
        //            }
        //            Student_Admission_Choicesubject ob = new Student_Admission_Choicesubject();
        //            Student_Admission_Choicesubject result = ob.getSubjectChoiceDetail(eID);
        //            StudentLogin tblST = new StudentLogin();
        //            var obj = tblST.BasicDetailByID(eID);
        //            ViewBag.Name = obj.Name;
        //            int UID = 0;
        //            if (ClsLanguage.GetCookies("ENNBUID") != null)
        //            {
        //                UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

        //            }
        //            sub.SID = eID;
        //            sub.ModifyBy = UID;

        //            BL_StreamMaster objStream = new BL_StreamMaster();

        //            if (result.sessionid == 39)
        //            {
        //                ViewBag.subsidiary1 = objStream.getcollegesubjects_old_for_session2018(result.SID, 11, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);
        //                ViewBag.subcomposition1 = objStream.getcollegesubjects_old_for_session2018(result.SID, 13, result.CollegeID, 0, 0, 0, result.sessionid);
        //                ViewBag.subsidiary2 = objStream.getcollegesubjects_old_for_session2018(result.SID, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);
        //                ViewBag.subcomposition2 = objStream.getcollegesubjects_old_for_session2018(result.SID, 14, result.CollegeID, 0, 0, result.Compulsory1_subjectid, result.sessionid);
        //            }
        //            else
        //            {
        //                // ViewBag.subsidiary1 = objStream.getcollegesubjects(result.SID, 11, result.CollegeID, result.SubjectID);
        //                ViewBag.subcomposition1 = objStream.getcollegesubjects(result.SID, 13, result.CollegeID,0,0,00,result.sessionid);
        //                //ViewBag.subsidiary2 = objStream.getcollegesubjects(result.SID, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid);
        //                ViewBag.subcomposition2 = objStream.getcollegesubjects(result.SID, 14, result.CollegeID, 0, 0, result.Compulsory1_subjectid,result.sessionid);
        //                if (obj.CourseCategory == 3)
        //                {
        //                    //result = objStream.getcollegesubjects(obj.Id, 20, id, 0, 0, 0, obj.CourseCategory);//bcom honours
        //                    ViewBag.subsidiary1 = objStream.getcollegesubjects(obj.Id, 25, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bcom
        //                    ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 12, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bcom
        //                }
        //                else if (obj.CourseCategory == 2)
        //                {
        //                    // result = objStream.getcollegesubjects(obj.Id, 24, id, 0, 0, 0, obj.CourseCategory);//bsc honours
        //                    ViewBag.subsidiary1 = objStream.getcollegesubjects(obj.Id, 26, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//bsc
        //                    ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 27, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//bsc
        //                }
        //                else
        //                {
        //                    // result = objStream.getcollegesubjects(obj.Id, 10, id, 0, 0, 0, obj.CourseCategory);//ba honours
        //                    ViewBag.subsidiary1 = objStream.getcollegesubjects(obj.Id, 28, result.CollegeID, result.SubjectID, 0, 0, result.sessionid);//ba
        //                    ViewBag.subsidiary2 = objStream.getcollegesubjects(obj.Id, 29, result.CollegeID, result.SubjectID, result.Subsidiary1_subjectid, 0, result.sessionid);//ba
        //                }
        //            }
        //            sub.sessionid = result.sessionid;
        //            if (sub.Subsidiary1_subjectid == 0)
        //            {
        //                TempData["StMessage"] = "Please Select Subsidiary Subject first";
        //                return View(result);
        //            }
        //            if (sub.Subsidiary2_subjectid == 0)
        //            {
        //                TempData["StMessage"] = "Please Select Subsidiary Subject Second";
        //                return View(result);
        //            }
        //            if (sub.Compulsory1_subjectid == 0)
        //            {
        //                TempData["StMessage"] = "Please Select Composition Subject first";
        //                return View(result);
        //            }
        //            if (sub.Compulsory1_subjectid == 1063 || sub.Compulsory1_subjectid == 1066 || sub.Compulsory1_subjectid == 1067 || sub.Compulsory1_subjectid == 1068 || sub.Compulsory1_subjectid == 1069 || sub.Compulsory1_subjectid == 1070)
        //            {

        //            }
        //            else
        //            {
        //                if (sub.Compulsory2_subjectid == 0)
        //                {
        //                    TempData["StMessage"] = "Please Select Composition Subject Second";
        //                    return View(result);
        //                }
        //            }
        //            var res = ob.updateSubjectChoiceDetails(sub);
        //            TempData["StMessage"] = res.Msg;
        //            sub.CollegeName = result.CollegeName;
        //            sub.HonSubName = result.HonSubName;
        //            return View(sub);
        //        }
        //        else
        //        {
        //            ViewBag.subsidiary1 = "";
        //            ViewBag.subcomposition1 = "";
        //            ViewBag.subsidiary2 = "";
        //            ViewBag.subcomposition2 = "";
        //            return View();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Edit Student Details Get Method", eID.ToString());
        //        return RedirectToAction("FinalStudentList/");
        //    }


        //}
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult PaymentDetail()
        {
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult PaymentDetail(int id = 0)
        {

            BL_PrintRecipt obj = new BL_PrintRecipt();

            BL_PrintReciptList sub = new BL_PrintReciptList();

            string Status = Request.Form["Status"];
            string fromdate = Request.Form["fromdate"];
            string todate = Request.Form["todate"];
            string ApplicationNo = Request.Form["ApplicationNo"];
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.paymentdetail(10000000, 1, fromdate, todate, ApplicationNo, Status, collegeID);


            var result = sub.qlist.Select(x => new { ApplicationNo = x.ApplicationNo, Name = x.Name, Amount = x.Amount, TransactionID = x.Clienttrxid, BankReferenceId = x.banktrxid, BankTransactionTime = x.banktxndate, RequestTime = x.requesttime1, Status = x.status, Reason = x.Reason,Year_Semester=x.yearname }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Paymentdetails" + System.DateTime.Now.ToString() + ".xls");

            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();
        }
        //[VerifyUrlFilterCollegeAttribute]
        //public ActionResult FeeStructureReport()
        //{
        //    BL_courseMaster objmaster = new BL_courseMaster();
        //    ViewBag.EducationType = objmaster.GetEducationType();
        //    ViewBag.Coursetype = "";
        //    Commn_master com = new Commn_master(); 
        //    ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
        //    AcademicSession session = new AcademicSession();
        //    ViewBag.Session = session.GetSession();
        //    List<FeeStructure> res = new List<FeeStructure>();
        //    return View(res);
        //}
        //[HttpPost]
        //[VerifyUrlFilterCollegeAttribute]
        //public ActionResult FeeStructureReport(string program = "", string CoursetypeID = "", string session = "",string CastCategory="")
        //{
        //    BL_courseMaster objmaster = new BL_courseMaster();
        //    ViewBag.EducationType = objmaster.GetEducationType();
        //    ViewBag.Coursetype = "";
        //    ViewBag.Education = program;
        //    Commn_master com = new Commn_master();
        //    ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
        //    ViewBag.CastCategory = CastCategory;
        //    ViewBag.CoursetypeID = CoursetypeID;
        //    ViewBag.sessionval = session;
        //    AcademicSession sess = new AcademicSession();
        //    ViewBag.Session = sess.GetSession();
        //    FeeStructure ob = new FeeStructure();
        //    int edid = 0;
        //    EnrollmentRequest obj = new EnrollmentRequest();
        //    if (program != "")
        //    {
        //        edid = Convert.ToInt32(program);
        //        ViewBag.Coursetype = obj.getcourseMaster(edid);
        //    }

        //    string enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
        //    string enID = "";
        //    int eID = 0;
        //    if (enCollegeID != "0" && enCollegeID.Length > 0)
        //    {

        //        enID = EncriptDecript.Decrypt(enCollegeID);
        //        if (enID != "")
        //        {
        //            eID = Convert.ToInt32(enID);
        //        }


        //    }
        //    List<FeeStructure> res = ob.getfeedetail(CoursetypeID, session, eID, CastCategory);
        //    return View(res);
        //}
        #endregion

        #region Student Attendance
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult AddStudentAttendance()
        {
            ViewBag.CourseYear = "";
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));



            var CollegeID = 0; var EmployeeID = 0;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    model.EduTypeList = objEmp.GetEmployeeDropdown("EducationType", 0, 0, CollegeID, EmployeeID).Distinct().ToList();
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                }
                else if (usertype == 1)
                {
                    model.EduTypeList = objCourse.GetEducationTypeList();
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                    model.EmployeeList = objEmp.GetEmployeeBySubject(0, 0);
                    //model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                }
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddStudentAttendance(StudentAttendanceAdd model, HttpPostedFileBase ExcelPath)
        {
            try
            {
            }
            catch (Exception ex)
            {

            }
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));


            StudentAttendanceAdd st = new StudentAttendanceAdd();

            string filePath = string.Empty;
            if (ExcelPath != null)
            {
                string path = Server.MapPath("~/Excel/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(ExcelPath.FileName);
                string extension = Path.GetExtension(ExcelPath.FileName);
                ExcelPath.SaveAs(filePath);

                string conString = string.Empty;
                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                foreach (DataRow row in dt.Rows)
                {
                    StudentAttendanceAdd sta = new StudentAttendanceAdd();
                    //BL_StreamMaster objSub = new BL_StreamMaster();
                    // Recruitment objStu = new Recruitment();
                    var SubjectCode = Convert.ToString(row["SubjectCode"]);
                    //var SubjectID = objSub.AllSubjectList(model.CourseCategoryID, model.CollegeID).FirstOrDefault(x => x.SubjectCode == SubjectCode).StreamCategoryID;

                    //var StuRollNo = Convert.ToString(row["StudentRollNo"]);
                    //var StudentID = objStu.GetStudentRollNoList().FirstOrDefault(x => x.RollNo == StuRollNo).Sid;

                    //var stuCollegeID = objStu.GetAllStudentList().FirstOrDefault(x => x.sid == StudentID).collegeid;

                    //var list = objStu.GetStudentBySubjectList(model.CollegeID, model.SessionID, model.SubjectID).FirstOrDefault(x => x.Value == StudentID.ToString());
                    if (model.SubjectCode.ToLower() == SubjectCode.ToLower())
                    {
                        sta.CollegeID = Convert.ToInt32(model.CollegeID);
                        sta.EmployeeID = Convert.ToInt32(model.EmployeeID);
                        sta.SessionID = Convert.ToInt32(model.SessionID);
                        sta.StudentYear = Convert.ToInt32(model.StudentYear);
                        sta.SubjectCode = Convert.ToString(row["SubjectCode"]);
                        sta.StudentRollNo = Convert.ToString(row["StudentRollNo"]);
                        sta.AttendanceDate = Convert.ToDateTime(row["AttendanceDate"]);
                        sta.AttendanceType = Convert.ToString(row["AttendanceType"]);

                        var result = st.StudentAttendance_Add(sta);

                        if (result.status == true)
                        {
                            TempData["StMessage"] = result.Message;
                        }
                        else { TempData["StError"] = result.Message; }
                    }
                }
            }


            return RedirectToAction("AddStudentAttendance");
        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }
        public JsonResult PopulateCourseCategoryDropdown(int EduTypeID = 0, int CollegeID = 0, int EmployeeID = 0)
        {
            //var eid = ClsLanguage.GetCookies("ENNBUID");
            //if (eid != "")
            //{
            //    EmployeeID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
            //}
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            List<SelectListItem> list = new List<SelectListItem>();
            if (usertype == 2)
            {
                UserLogin objEmp = new UserLogin();
                list = objEmp.GetEmployeeDropdown("", EduTypeID, 0, CollegeID, EmployeeID);
            }
            else if (usertype == 1)
            {
                BL_courseMaster objCourse = new BL_courseMaster();
                list = objCourse.GetCourseCategoryList(EduTypeID);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PopulateSubjectDropdown(int CourseCategoryID = 0, int CollegeID = 0, int EmployeeID = 0)
        {
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            st.CourseCategoryID = CourseCategoryID;
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            List<SelectListItem> list = new List<SelectListItem>();
            if (usertype == 2)
            {
                UserLogin objEmp = new UserLogin();
                list = objEmp.GetEmployeeDropdown("", 0, CourseCategoryID, CollegeID, EmployeeID);
            }
            else if (usertype == 1)
            {
                BL_StreamMaster objSubject = new BL_StreamMaster();
                list = objSubject.GetSubjectCodeList(CourseCategoryID, CollegeID);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PopulateSubjectDetailPageDropdown(int CourseCategoryID = 0, int CollegeID = 0, int EmployeeID = 0)
        {
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            List<SelectListItem> list = new List<SelectListItem>();
            if (usertype == 2)
            {
                UserLogin objEmp = new UserLogin();
                list = objEmp.GetEmployeeDropdown("Detail", 0, CourseCategoryID, CollegeID, EmployeeID);
            }
            else if (usertype == 1)
            {
                BL_StreamMaster objSubject = new BL_StreamMaster();
                list = objSubject.GetSubjectList(CourseCategoryID, CollegeID);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PopulateStudentDropdown(int CourseCategoryID = 0, int CollegeID = 0, int SessionID = 0, int SubjectID = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Recruitment objStudent = new Recruitment();
            list = objStudent.GetStudentBySubjectList(CollegeID, SessionID, SubjectID);
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PopulateEmployeeDropdown(int CollegeID = 0, int SubjectID = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            UserLogin objEmp = new UserLogin();
            if (usertype == 2)
            {
                list = objEmp.GetEmployeeList(usertype, CollegeID);
            }
            else if (usertype == 1)
            {
                list = objEmp.GetEmployeeBySubject(CollegeID, SubjectID);
            }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulateLecturerDropdown(int CollegeID = 0, string SubjectCode = "", int CourseCategoryID = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            UserLogin objEmp = new UserLogin();
            try
            {
                if (usertype == 1)
                {
                    BL_StreamMaster objSub = new BL_StreamMaster();
                    var SubId = objSub.AllSubjectList(CourseCategoryID, CollegeID).FirstOrDefault(x => x.SubjectCode == SubjectCode.ToString()).StreamCategoryID;
                    list = objEmp.GetEmployeeBySubject(CollegeID, SubId);
                }
            }
            catch { }
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentAttendanceDetail(string Month = "", string Year = "", int SubjectID = 0, int StudentID = 0, int EmpID = 0, int EduTypeID = 0, int CourseCategoryID = 0, int Session = 0, int StudentYear = 0)
        {
            TempData["StError"] = "";

            ViewBag.CourseYear = "";
            ViewBag.Session = "";
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));

            var CollegeID = 0; var EmployeeID = 0; ;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    model.EduTypeList = objEmp.GetEmployeeDropdown("EducationType", 0, 0, CollegeID, EmployeeID).Distinct().ToList();
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {
                    model.EduTypeList = objCourse.GetEducationTypeList();
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }
                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;

                if (Request.IsAjaxRequest())
                {
                    model.AttendanceList = st.StudentAttendanceDetailList(CollegeID, EmpID).AttendanceList;
                }
                else
                {
                    model.AttendanceList = AttList;
                }
                //if (Month != "")
                //{
                //    model.AttendanceList = st.StudentAttendanceDetailList_FilterDate(CollegeID, EmpID, Month, Year).AttendanceList;
                //}
                if (SubjectID > 0)
                {
                    model.AttendanceList = model.AttendanceList.Where(x => x.SubjectID == SubjectID).ToList();
                }
                if (StudentID > 0)
                {
                    model.AttendanceList = model.AttendanceList.Where(x => x.StudentID == StudentID).ToList();
                }
                if (StudentYear > 0)
                {
                    model.AttendanceList = model.AttendanceList.Where(x => x.StudentYear == StudentYear).ToList();
                }
                if (CourseCategoryID > 0)
                {
                    model.AttendanceList = model.AttendanceList.Where(x => x.CourseCategoryID == CourseCategoryID).ToList();
                }
                if (Session > 0)
                {
                    model.AttendanceList = model.AttendanceList.Where(x => x.SSession == Session).ToList();
                }
                if (Convert.ToInt32(Month) > 0)
                {
                    model.AttendanceList = model.AttendanceList.Where(x => x.Month == Month).ToList();
                }



            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }

            var AllStudent = objStudent.GetAllStudentList();
            //var stuList = AllStudent.OrderBy(x=>x.collegeid).GroupBy(x => x.collegeid);
            //int i = 1;
            //foreach (var item in stuList)
            //{
            //    var facList = item.OrderBy(x => x.Faculty).GroupBy(x => x.Faculty);
            //    foreach (var item1 in facList)
            //    {
            //        var stNameList = item1.OrderBy(x => x.StudentName);

            //        foreach (var item3 in stNameList)
            //        {
            //            var faculty = item1.Key;
            //            var No = String.Format("{0:0000}", i);

            //            int sid = Convert.ToInt32(item3.sid);

            //          var result = objStudent.InsertStudentRollNo(faculty, sid, No);

            //            i++;
            //        }
            //    }                                  
            //}

            return View(model);
        }


        #endregion

        #region Employee Subject Assign
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeSubjectAssign(int CollegeId = 0, int CourseCategoryID = 0, int EmployeeId = 0)
        {
            var CollegeID = 0;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));

            UserLogin model = new UserLogin();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession session = new AcademicSession();
            List<UserLogin> lst = new List<UserLogin>();
            model.EduTypeList = objCourse.GetEducationTypeList();
            model.CourseCategoryList = objCourse.GetCourseCategoryList(0);

            model.AssignSubjectList = lst;
            model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
            model.CollegeID = CollegeID;
            model.SessionId = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
            List<UserLogin> aslst = new List<UserLogin>();
            model.AssignSubjectList = aslst;
            if (Request.IsAjaxRequest() && EmployeeId > 0)
            {
                model.AssignSubjectList = objEmp.GetEmployeeAssignedSubject(CollegeId, EmployeeId, CourseCategoryID, 0);
            }
            return View(model);
        }

        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult EmployeeSubjectAssign(UserLogin model)
        {
            UserLogin user = new UserLogin();
            foreach (var item in model.AssignSubjectList)
            {
                UserLogin ul = new UserLogin();
                ul.CollegeID = Convert.ToInt32(model.CollegeID);
                ul.EmployeeID = Convert.ToInt32(model.EmployeeID);
                ul.SubjectId = Convert.ToInt32(item.SubjectId);
                ul.SessionId = Convert.ToInt32(model.SessionId);
                ul.EducationTypeId = Convert.ToInt32(model.EducationTypeId);
                ul.CourseCategoryId = Convert.ToInt32(model.CourseCategoryId);
                ul.IsActive = item.IsActive;

                var result = user.EmployeeSubjectAssign_Add(ul);

                if (result.status == true)
                {
                    TempData["StMessage"] = result.Message;
                }
                else { TempData["StError"] = result.Message; }
            }
            return RedirectToAction("EmployeeSubjectAssign");
        }
        #endregion
        public ActionResult LoginHistory()
        {
            return View();
        }
        public ActionResult BasicDetailManualAd(string id = "", string eid = "")
        {
            if (id != "")
            {
                if (EncriptDecript.Decrypt(id) == "1")
                {
                    ViewBag.exist = true;
                    ViewBag.ID = id;
                }
            }
            //ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            DataLayer.Login obj = new DataLayer.Login();
            Commn_master com = new Commn_master();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            //ViewBag.Educationtype = com.getcommonMaster("selectbycommonid", Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.Educationtype = objmaster.GetEducationType();
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            ViewBag.Session = com.GetSession("Session");
            ViewBag.Course = com.getcommonMaster("Course", obj.EducationType);
            ViewBag.Stream = com.getcommonMaster("Stream", obj.CourseCategory);

            StudentAdmissionQualification com1 = new StudentAdmissionQualification();
            ViewBag.previousqua = com1.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.boardtype = CommonMethod.Boradtype();
            ViewBag.adu = "1";
            ViewBag.Course = com.getcommonMaster("coursesfordrop");
            State ob = new State();
            List<State> ds = ob.GetStateListByCountryId("80");
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (State dr in ds)
            {
                statelist.Add(new SelectListItem { Text = dr.StateName, Value = dr.stateID.ToString() });
            }
            ViewBag.State = statelist;
            ViewBag.PState = statelist;
            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            string stid = "";
            if (eid != "0" && eid.Length > 0)
            {
                stid = EncriptDecript.Decrypt(eid);
                int finalID = (stid != "" ? Convert.ToInt32(stid) : 0);
                objl = objs.BasicDetailwithrecruitmentByID(finalID);
                ViewBag.bloodgroupid = objl.BloodGroup;
                ViewBag.castid = objl.CastCategory;
                ViewBag.genderid = objl.Gender;
                ViewBag.eduid = objl.EducationType;
                ViewBag.titileid = objl.title;
                ViewBag.ftitileid = objl.Ftitle;
                ViewBag.Nationalityid = objl.Nationality;
                ViewBag.Religionid = objl.Religion;
                ViewBag.MotherTongueid = objl.MotherTongue;
                ViewBag.boardid = objl.boardtype;
                ViewBag.prevoiustreamid = objl.previous_qua_id;
                ViewBag.Studentid = objl.Id;
                ViewBag.exist = false;
                //ViewBag.existrec = true;
                ViewBag.ID = eid;
               
                return View(objl);
            }

            return View(objl);
        }
        [HttpPost]
        public ActionResult BasicDetailManualAd(DataLayer.Login objlogin, HttpPostedFileBase sign, HttpPostedFileBase photo, string id = "", string eid = "")
        {

            StudentLogin st = new StudentLogin();

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            DataLayer.Login obj = new DataLayer.Login();
            Commn_master com = new Commn_master();
            ViewBag.Admissiontype = com.getcommonMaster("AdmissionType");
            //ViewBag.Educationtype = com.getcommonMaster("selectbycommonid", Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.Educationtype = objmaster.GetEducationType();
            ViewBag.Course = "";
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.Gender = com.getcommonMaster("Gender");
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.bloodgroup = com.Getbloodgroup("Select");
            ViewBag.Session = com.GetSession("Session");
            ViewBag.stitle = com.getcommonMaster("Title");
            ViewBag.ftitle = com.getcommonMaster("TitleM");
            ViewBag.Nationality = com.getcommonMaster("Nationality");
            ViewBag.Religion = com.getcommonMaster("Religion");
            ViewBag.MotherTongue = com.getcommonMaster("MotherTongue");
            ViewBag.Course = com.getcommonMaster("coursesfordrop");
            StudentAdmissionQualification com1 = new StudentAdmissionQualification();
            ViewBag.previousqua = com1.getqualificationst(Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            Country cont = new Country();
            ViewBag.country = cont.GetAllCountries();
            ViewBag.Pcountry = cont.GetAllCountries();
            ViewBag.boardtype = CommonMethod.Boradtype();
            ViewBag.adu = "1";
            ViewBag.ID = id;
            State ob = new State();
            List<State> ds = ob.GetStateListByCountryId("80");
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (State dr in ds)
            {
                statelist.Add(new SelectListItem { Text = dr.StateName, Value = dr.stateID.ToString() });
            }

            ViewBag.State = statelist;
            ViewBag.PState = statelist;

            string jsonstring = JsonConvert.SerializeObject(objlogin);
            try
            {
                if (photo != null)
                {

                    Stream st1 = photo.InputStream;
                    string name = Path.GetFileName(photo.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Photoandsign";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentPhoto_" + objlogin.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        objlogin.stphoto = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Basic Detail post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
                    }
                }
                if (sign != null)
                {

                    Stream st1 = sign.InputStream;
                    string name = Path.GetFileName(sign.FileName);
                    try
                    {
                        string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                        string s3DirectoryName = "Student/Photoandsign";
                        string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentSign_" + objlogin.FirstName + @name;
                        s3FileName = s3FileName.Replace(" ", "");
                        objlogin.stsign = s3FileName;
                        bool a;
                        AmazonUploader myUploader = new AmazonUploader();
                        a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                    }
                    catch (Exception ex)
                    {
                        // CommonMethod.PrintLog(ex);
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Detail Edit post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);
                    }
                }
                int UID = 0;
                if (ClsLanguage.GetCookies("ENNBUID") != null)
                {
                    UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));

                }
                objlogin.Password = CommonSetting.CreatePassword(6);
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(objlogin.Password));
                objlogin.U_password = hashedBytes;
                objlogin.InsertedBy = UID;
                AcademicSession ac = new AcademicSession();
                objlogin.session = ac.GetAcademiccurrentSession().ID;
                if (id != "0" && id != "")
                {
                    if (EncriptDecript.Decrypt(id) == "1")
                    {
                        var result = st.Student_DetailsUpdatemanually(objlogin);
                        if (result.status == true)
                        {
                            TempData["StMessage"] = result.Message;
                            ViewBag.enID = EncriptDecript.Encrypt("1");
                            objlogin.EncriptedID = EncriptDecript.Encrypt(result.Id.ToString());
                            ViewBag.ID = objlogin.EncriptedID;
                            //return RedirectToAction("BasicDetailManualAd/?eid="+objlogin.EncriptedID);
                            return RedirectToAction("BasicDetailManualAd/", new { id = id, eid = objlogin.EncriptedID });
                        }

                        else { TempData["StMessage"] = result.Message; }
                    }


                }
                else
                {
                    var result = st.Student_registration(objlogin);
                    if (result.status == true)
                    {
                        TempData["StMessage"] = result.Message;
                        ViewBag.ID = EncriptDecript.Encrypt(result.Id.ToString());
                        DataLayer.Login objl = new DataLayer.Login();
                        StudentLogin objs = new StudentLogin();
                        string stid = "";
                        if (objlogin.EncriptedID != "0" && objlogin.EncriptedID.Length > 0)
                        {
                            stid = EncriptDecript.Decrypt(objlogin.EncriptedID);
                            int finalID = (stid != "" ? Convert.ToInt32(stid) : 0);
                            objl = objs.BasicDetailwithrecruitmentByID(finalID);
                            ViewBag.bloodgroupid = objl.BloodGroup;
                            ViewBag.castid = objl.CastCategory;
                            ViewBag.genderid = objl.Gender;
                            ViewBag.eduid = objl.EducationType;
                            ViewBag.titileid = objl.title;
                            ViewBag.ftitileid = objl.Ftitle;
                            ViewBag.Nationalityid = objl.Nationality;
                            ViewBag.Religionid = objl.Religion;
                            ViewBag.MotherTongueid = objl.MotherTongue;
                            ViewBag.boardid = objl.boardtype;
                            ViewBag.prevoiustreamid = objl.previous_qua_id;
                            ViewBag.Studentid = objl.Id;
                            ViewBag.exist = false;
                            return View(objl);
                        }
                        return View(result);
                        // return RedirectToAction("BasicDetailManualAd");

                    }

                    else { TempData["StMessage"] = result.Message; }
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Details Edit post Method", ClsLanguage.GetCookies("NBCollegeName") + "   " + jsonstring);

            }
            return View(objlogin);
        }
        [BasicAuthentication]
        public JsonResult FindApplication(string ApplicationNo = "")
        {
            DataLayer.Login obj = new DataLayer.Login();
            Commn_master com = new Commn_master();
            if (ApplicationNo != "")
            {
                StudentLogin tblST = new StudentLogin();
                obj = tblST.BasicDetailwithrecruitment(ApplicationNo);
                //return Json( obj, JsonRequestBehavior.AllowGet);
                if (obj != null)
                {
                    obj.EncriptedID = EncriptDecript.Encrypt(obj.hid.ToString());
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        //   [VerifyUrlFilterCollegeAttribute]
        public ActionResult ManualAdmission()
        {
            return View();
        }
        // [VerifyUrlFilterCollegeAttribute]
        public ActionResult PreviousYearQualificationManualAd(string id = "", string qualiID = "")
        {

            try
            {

                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
                ViewBag.ID = id;
                string enID = "";
                int eID = 0;
                ViewBag.oneID = EncriptDecript.Encrypt("1");
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = Convert.ToInt32(enID);
                    objl = objs.BasicDetailByID(eID);
                    ViewBag.ID = id;
                    ViewBag.Name = objl.Name;

                }
                else
                {
                    TempData["StMessage"] = "Please Fill your Basic Details First!!!";
                    return RedirectToAction("BasicDetailManualAd");
                }

                if (objl.prevoiusboardid == 2)
                {
                    return RedirectToAction("PreviousyearQualificationO/", new { ID = id, qualiID = qualiID });
                }
                if (objl.prevoiusboardid == 3)
                {
                    return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
                }



                Commn_master com = new Commn_master();

                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.boardname = CommonMethod.Boradtype().ToList().Where(x => x.boardid == 1).FirstOrDefault().boardname;

                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();

                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1980; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 7; i++)
                {
                    list.Add(new StudentPreviousQualification());

                }
                ViewBag.NRBSubject = objp.GetNRBSubject();
                ViewBag.NBSubject = objp.GetNBSubject();
                ViewBag.LLSubject = objp.GetLLSubject();
                string enqualiID = "";
                int equaliID = 0;
                if (objl.Id > 0)
                {

                    var resultcheck = bl.sp_st_check_details(objl.ApplicationNo, ac.GetAcademiccurrentSession().ID.ToString());
                    if (qualiID != "0" && qualiID.Length > 0)
                    {
                        enqualiID = EncriptDecript.Decrypt(qualiID);
                        equaliID = Convert.ToInt32(enqualiID);
                        List<StudentPreviousQualification> result = new List<StudentPreviousQualification>();
                        Subject_bind(objl.previous_qua_id, objl.Id);
                        Quali_bind(objl.EducationType, objl.previous_qua_id, "college");
                        objst = obj.GetQualifiationByID(equaliID);
                        if (objst.QualicationType != 1)
                        {
                            result = objp.GetSubjectPercentageData(objl.ApplicationNo);
                        }
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }
                        else
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
                            return View(tuple);
                        }

                    }
                    else
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", new { ID = id });

                        }
                        Subject_bind(objl.previous_qua_id, objl.Id);
                        Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }

                }
                else
                {

                    Subject_bind(objl.previous_qua_id);
                    Quali_bind(objl.EducationType, objl.previous_qua_id, "college");

                    if (qualiID != "0" && qualiID.Length > 0)
                    {
                        enqualiID = EncriptDecript.Decrypt(qualiID);
                        equaliID = Convert.ToInt32(enqualiID);
                        List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(objl.ApplicationNo);
                        objst = obj.GetQualifiationByID(equaliID);
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }

                    }
                    else
                    {
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }

                }
                var tuple1 = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                return View(tuple1);
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification Manual Admission get action", id);
                //return View();
                return RedirectToAction("PreviousYearQualificationManualAd/");
            }
        }
        public void Subject_bind(int id = 0, int sid = 0)
        {
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<SubjectMaster> list = new List<SubjectMaster>();
            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            objl = objs.BasicDetailByID(sid);
            string board = "";
            if (objl.prevoiusboardid == 2)
            {
                board = "2";
            }
            else
            {
                board = "1";
            }
            if (id == 2)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Art12), board);
            }
            if (id == 3)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Science12), board);
            }
            if (id == 4)
            {
                list = obj.GetSubject(Convert.ToInt32(CommonSetting.Streamtype.Comm12), board);
            }
            List<SelectListItem> slist = new List<SelectListItem>();

            foreach (SubjectMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.SubjectName, Value = dr.ID.ToString() });
            }
            ViewBag.Subject = slist;
        }
        public void Quali_bind(int edu = 0, int pre = 0, string edit = "", int sid = 0)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            List<QualifiationMaster> list = obj.GetQualifiationMaster(edu, pre, edit, sid);

            List<SelectListItem> slist = new List<SelectListItem>();
            foreach (QualifiationMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.QualificationType, Value = dr.ID.ToString() });
            }
            ViewBag.Qualification = slist;
        }
        public ActionResult PreviousyearQualificationP(string id = "")
        {
            try
            {
                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                BL_student_formcomplete resultcheck = new BL_student_formcomplete();
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();

                string enID = "";
                int eID = 0;
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();
                Commn_master com = new Commn_master();

                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                StudentPreviousQualification objp = new StudentPreviousQualification();

                if (objl.prevoiusboardid == 2)
                {
                    return RedirectToAction("PreviousyearQualificationO/" + id, "Home");
                }
                //if (objl.prevoiusboardid == 3)
                //{
                //    return RedirectToAction("PreviousyearQualificationP/" + id, "Home");
                //}
                if (objl.prevoiusboardid == 1)
                {
                    return RedirectToAction("PreviousYearQualificationManualAd/" + id, "Home");
                }
                if (objl.prevoiusboardid == 0)
                {
                    return RedirectToAction("PreviousYearQualificationManualAd/" + id, "Home");
                }
                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1980; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new StudentPreviousQualification());

                }
                ViewBag.NRBSubject = objp.GetNRBSubject();
                ViewBag.NBSubject = objp.GetNBSubject();
                ViewBag.LLSubject = objp.GetLLSubject();
                if (id != "0" && id.Length > 0)
                {

                    if (enID != "")
                    {
                        enID = EncriptDecript.Decrypt(id);
                        eID = Convert.ToInt32(enID);
                        objl = objs.BasicDetailByID(eID);
                        bl.sp_st_check_details(objl.ApplicationNo, ac.GetAcademiccurrentSession().ID.ToString());
                        ViewBag.IsSubmit = objl.IsFeeSubmit;
                        List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(objl.ApplicationNo);
                    }
                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "Home");
                        }
                        if (objl != null)
                        {
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            // ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }
                    if (eID > 0)
                    {
                        objst = obj.GetQualifiationByID(eID);
                        List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(objl.ApplicationNo);
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }
                        else
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
                            return View(tuple);
                        }
                    }
                    else
                    {
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }

                }
                else
                {

                    if (eID == 0)
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", "Home");
                        }
                        if (objl != null)
                        {
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        }
                    }
                    else
                    {
                        if (objl != null)
                        {
                            //ViewBag.Subject = "";
                            Subject_bind(objl.previous_qua_id);
                            Quali_bind(objl.EducationType, eID, "edit");
                        }
                    }

                    var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                    return View(tuple);
                }

            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification get action", ClsLanguage.GetCookies("NBCollegeName"));
                //return View();
                return RedirectToAction("PreviousYearQualificationManualAd/");
            }

        }
        // [VerifyUrlFilterCollegeAttribute]
        public ActionResult PreviousyearQualificationO(string id = "", string qualiID = "")
        {
            try
            {

                BL_student_formcomplete bl = new BL_student_formcomplete();
                AcademicSession ac = new AcademicSession();
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
                ViewBag.ID = id;
                string enID = "";
                int eID = 0;
                ViewBag.oneID = EncriptDecript.Encrypt("1");
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = Convert.ToInt32(enID);
                    objl = objs.BasicDetailByID(eID);
                    ViewBag.ID = id;
                    ViewBag.Name = objl.Name;

                }



                Commn_master com = new Commn_master();

                ViewBag.check_admissionopen = com.check_admission_open(ac.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));

                ViewBag.IsSubmit = objl.IsFeeSubmit;
                ViewBag.boardname = CommonMethod.Boradtype().ToList().Where(x => x.boardid == 1).FirstOrDefault().boardname;

                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                StudentAdmissionQualification objst = new StudentAdmissionQualification();

                StudentPreviousQualification objp = new StudentPreviousQualification();
                List<SelectListItem> ob = new List<SelectListItem>();
                for (int i = System.DateTime.Now.Year; i >= 1980; i--)
                {
                    ob.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
                }
                ViewBag.year = ob;
                List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new StudentPreviousQualification());

                }
                ViewBag.NRBSubject = objp.GetNRBSubject();
                ViewBag.NBSubject = objp.GetNBSubject();
                ViewBag.LLSubject = objp.GetLLSubject();
                string enqualiID = "";
                int equaliID = 0;
                if (objl.Id > 0)
                {

                    var resultcheck = bl.sp_st_check_details(objl.ApplicationNo, ac.GetAcademiccurrentSession().ID.ToString());
                    if (qualiID != "0" && qualiID.Length > 0)
                    {
                        enqualiID = EncriptDecript.Decrypt(qualiID);
                        equaliID = Convert.ToInt32(enqualiID);
                        List<StudentPreviousQualification> result = new List<StudentPreviousQualification>();
                        Subject_bind(objl.previous_qua_id, objl.Id);
                        Quali_bind(objl.EducationType, objl.previous_qua_id, "college");
                        objst = obj.GetQualifiationByID(equaliID);
                        if (objst.QualicationType != 1)
                        {
                            result = objp.GetSubjectPercentageData(objl.ApplicationNo);
                        }
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }
                        else
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, list);
                            return View(tuple);
                        }

                    }
                    else
                    {
                        if (resultcheck.isqua_complete == true)
                        {
                            return RedirectToAction("StudentQualification", new { ID = id });

                        }
                        Subject_bind(objl.previous_qua_id, objl.Id);
                        Quali_bind(objl.EducationType, objl.previous_qua_id, "", objl.Id);
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }
                }
                else
                {

                    Subject_bind(objl.previous_qua_id);
                    Quali_bind(objl.EducationType, objl.previous_qua_id, "college");

                    if (qualiID != "0" && qualiID.Length > 0)
                    {
                        enqualiID = EncriptDecript.Decrypt(qualiID);
                        equaliID = Convert.ToInt32(enqualiID);
                        List<StudentPreviousQualification> result = objp.GetSubjectPercentageData(objl.ApplicationNo);
                        objst = obj.GetQualifiationByID(equaliID);
                        if (result.Count != 0)
                        {
                            var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(objst, result);
                            return View(tuple);
                        }

                    }
                    else
                    {
                        var tuple = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                        return View(tuple);
                    }

                }
                var tuple1 = new Tuple<StudentAdmissionQualification, List<StudentPreviousQualification>>(obj, list);
                return View(tuple1);
            }
            catch (Exception ex)
            {

                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "PreviousyearQualification Manual Admission get action", id);
                //return View();
                return RedirectToAction("PreviousYearQualificationManualAd/");
            }

        }

        //   [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentQualification(string id = "", string qualiID = "")
        {
            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            string enID = "";
            int eID = 0;
            ViewBag.oneID = EncriptDecript.Encrypt("1");
            if (id != "0" && id.Length > 0)
            {
                enID = EncriptDecript.Decrypt(id);
                eID = Convert.ToInt32(enID);
                objl = objs.BasicDetailByID(eID);
                ViewBag.ID = id;
                ViewBag.Name = objl.Name;

            }

            if (objl != null)
            {
                Subject_bind(objl.previous_qua_id, objl.Id);
            }
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<StudentPreviousQualification> list = new List<StudentPreviousQualification>();
            List<StudentPreviousQualification> list1 = new List<StudentPreviousQualification>();

            list = obj.GetSubjectPercentageData(objl.ApplicationNo);
            if (list.Count != 0)
            {
                return View(list);
            }
            else
            {
                return View(list1);
            }

        }

        public JsonResult SubjectTable(int id = 0, string sid = "")
        {

            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            if (sid != "")
            {
                string enid = EncriptDecript.Decrypt(sid);
                objl = objs.BasicDetailByID(Convert.ToInt32(enid));
            }

            if (objl.previous_qua_id == id)
            {
                var result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Subject_bindDanamic(int id = 0, string res = "", string sid = "")
        {
            StudentPreviousQualification obj = new StudentPreviousQualification();
            List<SubjectMaster> list = new List<SubjectMaster>();

            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            if (sid != "")
            {
                string enid = EncriptDecript.Decrypt(sid);
                objl = objs.BasicDetailByID(Convert.ToInt32(enid));
            }
            string board = "";
            if (objl.prevoiusboardid == 2)
            {
                board = "2";
            }
            else
            {
                board = "1";
            }

            if (id == 2)
            {
                list = obj.GetSubjectBYID(1, res, board);
            }
            if (id == 3)
            {
                list = obj.GetSubjectBYID(2, res, board);
            }
            if (id == 4)
            {
                list = obj.GetSubjectBYID(3, res, board);
            }
            List<SelectListItem> slist = new List<SelectListItem>();

            foreach (SubjectMaster dr in list)
            {
                slist.Add(new SelectListItem { Text = dr.SubjectName, Value = dr.ID.ToString() });
            }
            ViewBag.Subject = slist;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddNewQualification(string id = "", string qualiID = "")
        {
            StudentAdmissionQualification ob = new StudentAdmissionQualification();

            //ob.ApplicationNo = ClsLanguage.GetCookies("NBApplicationNo");


            if (Request.Form.Count > 0)
            {
                StudentAdmissionQualification doc = new StudentAdmissionQualification();

                doc.QualicationType = Convert.ToInt32(Request.Form["Qualification"] == "" ? "0" : Request.Form["Qualification"]);
                doc.Board_UniversityName = Request.Form["UniversityName"];
                doc.Percentage = Convert.ToDecimal(Request.Form["Percentage"] == "" ? "0" : Request.Form["Percentage"]);
                doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.PassingYear = Request.Form["PassingYear"];
                doc.sublist = Request.Form["sublist"];
                doc.RollNo = Request.Form["RollNo"];
                doc.subper = Request.Form["subper"];
                doc.TotalMarks = Request.Form["TotalMarks"];
                doc.MarksObtain = Request.Form["MarksObtain"];
                var EnID = Request.Form["EnID"];
                doc.hfile = Request.Form["hfile"];
                doc.SubID = Request.Form["SubID"];
                DataLayer.Login objl = new DataLayer.Login();
                StudentLogin objs = new StudentLogin();
                var sublistarr = doc.sublist.Split(',');

                if (doc.Percentage == 0)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = "Please Fill Aggregate Percentage ,Or Fill Again form !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }


                if (EnID != null)
                {

                    string eni = EncriptDecript.Decrypt(EnID);
                    objl = objs.BasicDetailByID(Convert.ToInt32(eni));

                }
                var resultyear = doc.Checkpassingyear(doc.PassingYear, objl.Id.ToString(), doc.ID.ToString());
                if (resultyear.Status)
                {
                    StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                    doc1.Msg = resultyear.Msg;
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }

                if (doc.QualicationType != 1)
                {
                    if (objl.boardtype == 2)
                    {
                        if (sublistarr.Length < 5)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (objl.boardtype == 1)
                    {
                        if (sublistarr.Length < 6)
                        {
                            StudentAdmissionQualification doc1 = new StudentAdmissionQualification();
                            doc1.Msg = "Please Fill minimum five Subject ,Or Fill Again form !!";
                            return Json(doc1, JsonRequestBehavior.AllowGet);
                        }
                    }

                }
                doc.SID = objl.Id;
                doc.ApplicationNo = objl.ApplicationNo;
                doc.session = objl.session;
                StudentAdmissionQualification result = new StudentAdmissionQualification();
                string jsonstring = JsonConvert.SerializeObject(doc);

                if (Request.Files.Count > 0)
                {
                    try
                    {
                        if (Request.Files.GetKey(0) == "file")
                        {
                            HttpPostedFileBase fileUpload = Request.Files.Get(0);
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                            }
                            Stream st1 = fileUpload.InputStream;
                            string name = Path.GetFileName(fileUpload.FileName);
                            try
                            {
                                string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                string s3DirectoryName = "Student/Document";
                                string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentDocument_" + objl.FirstName + @name;

                                s3FileName = s3FileName.Replace(" ", "");
                                doc.FileURl = s3FileName;
                                bool a;
                                AmazonUploader myUploader = new AmazonUploader();
                                a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                            }
                            catch (Exception ex)
                            {
                                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
                            }
                        }
                        else
                        {
                            StudentAdmissionQualification ob1 = new StudentAdmissionQualification();
                            ob1.Msg = "Error occurred. Error details: ";
                            return Json(ob1, JsonRequestBehavior.AllowGet);
                        }



                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", "DocumentType1" + jsonstring);
                        StudentAdmissionQualification ob3 = new StudentAdmissionQualification();
                        ob3.Msg = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    doc.FileURl = doc.hfile;
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    doc.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                result = ob.SaveQualificationDetailsManualadmission(doc);
                if (result.Status)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StudentAdmissionQualification logmsg = new StudentAdmissionQualification();
                logmsg.Msg = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult StudentQualificationDelete(string id = "", string qualiID = "")
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            string enID = "0";
            string enqualiID = "";
            if (id != "0" && id.Length > 0)
            {
                enID = EncriptDecript.Decrypt(id);
            }
            if (qualiID != "0" && qualiID.Length > 0)
            {
                enqualiID = EncriptDecript.Decrypt(qualiID);
            }
            int InsertedBy = 0;
            var insertby = ClsLanguage.GetCookies("ENNBUID");
            if (insertby != "")
            {
                InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
            }
            var result = obj.DeleteQualifiationByID(Convert.ToInt32(enqualiID), InsertedBy);
            TempData["Msg"] = result.Msg;
            return RedirectToAction("StudentQualification/" + id);

        }
        //  [VerifyUrlFilterCollegeAttribute]
        public ActionResult ChosseSubjectsmanualad(string id = "")

        {
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    ViewBag.show = true;

                    ViewBag.oneID = EncriptDecript.Encrypt("1");
                    ViewBag.ID = id;

                    BL_student_formcomplete bl = new BL_student_formcomplete();
                    AcademicSession ac = new AcademicSession();
                    Student_Admission_Choicesubject ob = new Student_Admission_Choicesubject();
                    Student_Admission_Choicesubject result = new Student_Admission_Choicesubject();
                    StudentLogin tblST = new StudentLogin();
                    var obj = tblST.BasicDetailByID(eID);
                    StudentPreviousQualification StudentPrevious = new StudentPreviousQualification();
                    var countlist = StudentPrevious.GetSubjectPercentageData(obj.ApplicationNo);
                    if (countlist.Count == 0)
                    {
                        ViewBag.hounousSubject = "";
                        ViewBag.subcomposition1 = "";

                        ViewBag.subsidiary1 = "";

                        ViewBag.subsidiary2 = "";
                        ViewBag.subcomposition2 = "";
                        TempData["StMessage"] = "Please First Upload your Qualification!!";

                        return View();
                    }
                    if (obj.EducationType == 13)
                    {
                        ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
                        ViewBag.volcational = true;
                    }
                    else
                    {
                        ViewBag.volcational = false;
                    }

                    ViewBag.Name = obj.Name;
                    string enCollegeID = ClsLanguage.GetCookies("ENNBCLID");

                    string encID = "";
                    int ecID = 0;
                    if (enCollegeID != "0" && enCollegeID.Length > 0)
                    {

                        encID = EncriptDecript.Decrypt(enCollegeID);
                        if (encID != "")
                        {
                            ecID = Convert.ToInt32(encID);
                        }
                    }
                    BL_StreamMaster objStream = new BL_StreamMaster();
                    if (obj.CourseCategory == 3)
                    { ViewBag.hounousSubject = objStream.getcollegesubjects(obj.Id, 20, ecID); }
                    else
                    { ViewBag.hounousSubject = objStream.getcollegesubjects(obj.Id, 10, ecID); }

                    ViewBag.subcomposition1 = objStream.getcollegesubjects(obj.Id, 13, ecID);

                    ViewBag.subsidiary1 = "";

                    ViewBag.subsidiary2 = "";
                    ViewBag.subcomposition2 = "";
                    ob = result.getSubjectChoiceDetailformanual(eID);
                    if (ob != null)
                    {
                        ViewBag.subsidiary1 = objStream.getcollegesubjects(ob.SID, 11, ob.CollegeID, ob.SubjectID);
                        ViewBag.subcomposition1 = objStream.getcollegesubjects(ob.SID, 13, ob.CollegeID);
                        ViewBag.subsidiary2 = objStream.getcollegesubjects(ob.SID, 12, ob.CollegeID, ob.SubjectID, ob.Subsidiary1_subjectid);
                        ViewBag.subcomposition2 = objStream.getcollegesubjects(ob.SID, 14, ob.CollegeID, 0, 0, ob.Compulsory1_subjectid);
                        return View(ob);
                    }
                    else
                    {
                        result.CollegeID = ecID;
                        return View(result);
                    }

                }
                else
                {
                    ViewBag.show = false;
                    TempData["StMessage"] = "Please Fill your Previous Qualification Details";
                    ViewBag.subsidiary1 = "";
                    ViewBag.subcomposition1 = "";
                    ViewBag.subsidiary2 = "";
                    ViewBag.subcomposition2 = "";
                    return View();
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Subject Details manual Admission Get Method", eID.ToString());
                return RedirectToAction("PreviousYearQualificationManualAd/" + id);
            }


        }
        public JsonResult getcollegesubjects(string sid = "", string id = "")
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();

            DataLayer.Login objl = new DataLayer.Login();
            StudentLogin objs = new StudentLogin();
            int stid = 0;
            string stdID = "";
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                stid = Convert.ToInt32(stdID);
            }
            objl = objs.BasicDetailByID(stid);
            var result = new List<BL_StreamMaster>();
            BL_StreamMaster objStream = new BL_StreamMaster();
            int clgid = id != "" ? Convert.ToInt32(id) : 0;
            //if (objl.CourseCategory == 3)
            //{ result = objStream.getcollegesubjects(objl.Id, 20, clgid); }
            //else
            //{ result = objStream.getcollegesubjects(objl.Id, 10, clgid); }
            if (objl.CourseCategory == 3)
            {
                result = objStream.getcollegesubjects(objl.Id, 20, clgid, 0, 0, 0, objl.session);//bcom
            }
            else if (objl.CourseCategory == 2)
            {
                result = objStream.getcollegesubjects(objl.Id, 24, clgid, 0, 0, 0, objl.session);//bsc
            }
            else
            {
                result = objStream.getcollegesubjects(objl.Id, 10, clgid, 0, 0, 0, objl.session);//ba
            }

            return Json(new { data = result, success = true });
        }
        public JsonResult getcollegesubsidiary1(int collegeid, int subjectid, string id)
        {
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin objs = new StudentLogin();
                    var objl = objs.BasicDetailByID(eID);
                    BL_StreamMaster objStream = new BL_StreamMaster();
                    //var result = objStream.getcollegesubjects(objl.Id, 11, collegeid, subjectid);
                    var result = new List<BL_StreamMaster>();
                    if (objl.CourseCategory == 3)
                    {
                        result = objStream.getcollegesubjects(objl.Id, 25, collegeid, subjectid, 0, 0, objl.session);//bcom
                    }
                    else if (objl.CourseCategory == 2)
                    {
                        result = objStream.getcollegesubjects(objl.Id, 26, collegeid, subjectid, 0, 0, objl.session);//bsc
                    }
                    else
                    {
                        result = objStream.getcollegesubjects(objl.Id, 28, collegeid, subjectid, 0, 0, objl.session);//ba
                    }
                    return Json(new { data = result, success = true });
                }
                else
                {
                    return Json(new { data = obj, success = true });
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "getcollegesubsidiary2 Get Method", eID.ToString());
                // return RedirectToAction("StudentSubjectsEdit/");
                return Json(new { data = obj, success = true });
            }

        }
        // [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult ChosseSubjectsmanualad(Student_Admission_Choicesubject sub, string id = "")
        {
            string enID = "";
            int eID = 0;
            try
            {
                if (sub.ID != "0" && sub.ID.Length > 0)
                {
                    ViewBag.show = true;
                    enID = EncriptDecript.Decrypt(sub.ID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    ViewBag.ID = sub.ID;
                    ViewBag.oneID = EncriptDecript.Encrypt("1");
                    Student_Admission_Choicesubject ob = new Student_Admission_Choicesubject();
                    Student_Admission_Choicesubject result = new Student_Admission_Choicesubject();
                    StudentLogin tblST = new StudentLogin();
                    var obj = tblST.BasicDetailByID(eID);
                    ViewBag.Name = obj.Name;
                    int UID = 0;
                    if (ClsLanguage.GetCookies("ENNBUID") != null)
                    {
                        UID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
                    }
                    sub.SID = eID;
                    sub.ModifyBy = UID;
                    sub.sessionid = obj.session;
                    BL_StreamMaster objStream = new BL_StreamMaster();
                    ViewBag.Name = obj.Name;
                    string enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
                    string encID = "";
                    int ecID = 0;
                    if (enCollegeID != "0" && enCollegeID.Length > 0)
                    {

                        encID = EncriptDecript.Decrypt(enCollegeID);
                        if (encID != "")
                        {
                            ecID = Convert.ToInt32(encID);
                        }
                    }
                    if (obj.EducationType == Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc))
                    {
                        ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
                        ViewBag.volcational = true;
                        if (obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bca))
                        {
                            sub.hounors_subjectid = 1124;
                        }
                        if (obj.CourseCategory == Convert.ToInt32(CommonSetting.coursecategory.bba))
                        {
                            sub.hounors_subjectid = 1125;
                        }
                    }
                    else
                    {
                        ViewBag.volcational = false;
                    }
                    if (obj.CourseCategory == 3)
                    { ViewBag.hounousSubject = objStream.getcollegesubjects(obj.Id, 20, ecID); }
                    else
                    { ViewBag.hounousSubject = objStream.getcollegesubjects(obj.Id, 10, ecID); }
                    ViewBag.subcomposition1 = objStream.getcollegesubjects(obj.Id, 13, ecID);
                    ViewBag.subsidiary1 = "";
                    ViewBag.subsidiary2 = "";
                    ViewBag.subcomposition2 = "";
                    result.CollegeID = ecID;
                    if (obj.EducationType != 13)
                    {
                        if (sub.hounors_subjectid == 0)
                        {
                            TempData["StMessage"] = "Please Select Honours Subject first";
                            return View(result);
                        }
                        if (sub.Subsidiary1_subjectid == 0)
                        {
                            TempData["StMessage"] = "Please Select Subsidiary Subject first";
                            return View(result);
                        }
                        if (sub.Subsidiary2_subjectid == 0)
                        {
                            TempData["StMessage"] = "Please Select Subsidiary Subject Second";
                            return View(result);
                        }
                        if (sub.Compulsory1_subjectid == 0)
                        {
                            TempData["StMessage"] = "Please Select Composition Subject first";
                            return View(result);
                        }
                        if (sub.Compulsory1_subjectid == 1063 || sub.Compulsory1_subjectid == 1066 || sub.Compulsory1_subjectid == 1067 || sub.Compulsory1_subjectid == 1068 || sub.Compulsory1_subjectid == 1069 || sub.Compulsory1_subjectid == 1070)
                        {

                        }
                        else
                        {
                            if (sub.Compulsory2_subjectid == 0)
                            {
                                TempData["StMessage"] = "Please Select Composition Subject Second";
                                return View(result);
                            }
                        }
                    }
                    var res = ob.insertSubjectChooseManualAD(sub);
                    TempData["StMessage"] = res.Msg;
                    if (res.Status == true)
                    {
                        var resultup = ob.getSubjectChoiceDetailformanual(eID);
                        if (resultup != null)
                        {
                            ViewBag.subsidiary1 = objStream.getcollegesubjects(resultup.SID, 11, resultup.CollegeID, ob.SubjectID);
                            ViewBag.subcomposition1 = objStream.getcollegesubjects(resultup.SID, 13, resultup.CollegeID);
                            ViewBag.subsidiary2 = objStream.getcollegesubjects(resultup.SID, 12, resultup.CollegeID, resultup.SubjectID, ob.Subsidiary1_subjectid);
                            ViewBag.subcomposition2 = objStream.getcollegesubjects(resultup.SID, 14, resultup.CollegeID, 0, 0, resultup.Compulsory1_subjectid);
                            return View(resultup);
                        }
                    }
                    return View(sub);
                }
                else
                {
                    ViewBag.show = false;
                    TempData["StMessage"] = "Please Fill your Previous Qualification Details";
                    ViewBag.subsidiary1 = "";
                    ViewBag.subcomposition1 = "";
                    ViewBag.subsidiary2 = "";
                    ViewBag.subcomposition2 = "";
                    return View();
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Subject Details manual Admission post Method", eID.ToString());
                return RedirectToAction("ChosseSubjectsmanualad/" + sub.ID);
            }
        }
        //[VerifyUrlFilterCollegeAttribute]
        public ActionResult FeeSubmitManualAd(string id = "")
        {
            string enID = "";
            FeesSubmit ob = new FeesSubmit();
            Commn_master com = new Commn_master();
            ViewBag.paymode = ob.GetPaymentType();
            BL_PrintAllRecord print = new BL_PrintAllRecord();
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            int eID = 0;
            AdmissionFeesSubmit stlogin = new AdmissionFeesSubmit();
            AdmissionFeesSubmit obj = new AdmissionFeesSubmit();
            List<AdmissionFeesSubmit> feestruckture = new List<AdmissionFeesSubmit>();
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin objs = new StudentLogin();
                    var objl = objs.BasicDetailByID(eID);
                    ViewBag.Name = objl.Name;
                    ViewBag.oneID = EncriptDecript.Encrypt("1");
                    ViewBag.ID = id;
                    BL_PrintApplication PritApp = new BL_PrintApplication();
                    ViewBag.show = true;
                    var obj1 = PritApp.GetAppLicationDataAdmin(objl.Id);
                    obj = stlogin.FeesDetails(objl.Id);
                    AcademicSession ac = new AcademicSession();
                    string enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
                    string encID = "";
                    int ecID = 0;
                    if (enCollegeID != "0" && enCollegeID.Length > 0)
                    {

                        encID = EncriptDecript.Decrypt(enCollegeID);
                        if (encID != "")
                        {
                            ecID = Convert.ToInt32(encID);
                        }
                    }
                    int sessionid = ac.GetAcademiccurrentSession().ID;

                    var hounres = objs.ByIDgethounors_subjectid(eID, sessionid);
                    if (hounres == null)
                    {
                        ViewBag.show = false;
                        TempData["Message"] = "Please fill your Subject Details First!! ";
                        //var tuple1 = new Tuple<FeesSubmit, BL_PrintAllRecord, AdmissionFeesSubmit>(ob, obj1, obj);
                        var tuple1 = new Tuple<FeesSubmit, BL_PrintAllRecord, AdmissionFeesSubmit>(ob, obj1, obj);
                        return View(tuple1);
                        //return RedirectToAction("ChosseSubjectsmanualad/" + id);
                    }
                    int hounors_subjectid = objs.ByIDgethounors_subjectid(eID, sessionid).hounors_subjectid;
                    feestruckture = stlogin.FeesDetailsstructure(ecID, objl.CourseCategory, sessionid, objl.CastCategory, hounors_subjectid);
                    ViewBag.Feeamount = Convert.ToDecimal(feestruckture.Sum(x => x.amount));
                    var tuple = new Tuple<FeesSubmit, BL_PrintAllRecord, AdmissionFeesSubmit>(ob, obj1, obj);
                    return View(tuple);
                }
                else
                {
                    ViewBag.show = false;
                    TempData["Message"] = "Please fill your Subject Details First!! ";
                    var tuple1 = new Tuple<FeesSubmit, BL_PrintAllRecord, AdmissionFeesSubmit>(ob, print, obj);
                    return View(tuple1);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "manual Fee Submit Get Method", eID.ToString());
                return RedirectToAction("ChosseSubjectsmanualad/" + id);
            }
        }
        //[VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult FeeSubmitManualAd([Bind(Prefix = "Item1")] FeesSubmit obj, string id = "")
        {
            string enID = "";
            FeesSubmit ob = new FeesSubmit();
            Commn_master com = new Commn_master();
            ViewBag.paymode = ob.GetPaymentType();
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            int eID = 0;
            try
            {
                if (obj.Id != "0" && obj.Id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(obj.Id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    if (obj.AdmissionFees == 0)
                    {
                        TempData["msgfees"] = "Addmission fee should be greater than 0!!!";
                        return RedirectToAction("FeeSubmitManualAd");
                    }
                    StudentLogin tblST = new StudentLogin();
                    var objst = tblST.BasicDetailByID(eID);
                    ViewBag.Name = objst.Name;
                    StudentAdmissionQualification stquali = new StudentAdmissionQualification();
                    List<StudentAdmissionQualification> list = stquali.GetQualifiationByApplication(objst.ApplicationNo);
                    List<QualifiationMaster> qualitypelist = stquali.GetQualifiation();
                    ob.Status = true;
                    DocumentUpload Doc = new DocumentUpload();
                    DocumentUploadList subdoc = new DocumentUploadList();
                    subdoc = Doc.DocumentdetailList(1, 10);
                    if (objst.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
                    {
                        var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                        if (list10.Count == 0)
                        {
                            ob.Status = false;
                            ViewBag.Status = false;
                            TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                            return RedirectToAction("FeeSubmitManualAd");
                        }
                    }
                    if (objst.EducationType == Convert.ToInt32(CommonSetting.Commonid.Educationtype))
                    {
                        var list10 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Ten)).ToList();
                        if (list10.Count == 0)
                        {
                            ob.Status = false;
                            ViewBag.Status = false;
                            TempData["msgfees"] = "Please upload your Secondary Board qualification certificate !!!";
                            return RedirectToAction("FeeSubmitManualAd");
                        }
                        var list12 = list.Where(m => m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Art12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Science12) || m.QualicationType == Convert.ToInt32(CommonSetting.commQualification.Comm12)).ToList();
                        if (list12.Count == 0)
                        {
                            ob.Status = false;
                            ViewBag.Status = false;
                            TempData["msgfees"] = "Please upload your Intermediate qualification certificate !!!";
                            return RedirectToAction("FeeSubmitManualAd");
                        }
                        Student_Admission_Choicesubject Choicesubject = new Student_Admission_Choicesubject();
                        var Choicesubjectlist = Choicesubject.viewst_choicesubject(objst.Id);
                        if (Choicesubjectlist.Count == 0)
                        {
                            ob.Status = false;
                            ViewBag.Status = false;
                            TempData["msgfees"] = "Please first select choices for subject and college !!!";
                            return RedirectToAction("FeeSubmitManualAd");
                        }
                        ViewBag.oneID = EncriptDecript.Encrypt("1");
                        ViewBag.ID = obj.Id;
                        obj.SID = eID;
                        string enCollegeID = "";

                        string encID = "";
                        int ecID = 0;
                        if (ClsLanguage.GetCookies("ENNBCLID") != null)
                        {
                            enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
                            encID = EncriptDecript.Decrypt(enCollegeID);
                            if (encID != "")
                            {
                                ecID = Convert.ToInt32(encID);
                            }
                        }
                        AcademicSession ac = new AcademicSession();
                        obj.Session = ac.GetAcademiccurrentSession().ID.ToString();
                        obj.collegeID = ecID;
                        if (ClsLanguage.GetCookies("ENNBUID") != null)
                        {
                            obj.Insertedby = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
                        }
                        obj.paymode = "Cash";
                        var result = ob.FeessubManualAd(obj);

                        if (result.Status == true)
                        {
                            TempData["msgfees"] = result.Msg;
                            return RedirectToAction("FeeSubmitManualAd");
                            //return View(ob);
                        }
                        else
                        {
                            TempData["msgfees"] = result.Msg;
                            BL_PrintApplication PritApp = new BL_PrintApplication();
                            var obj1 = PritApp.GetAppLicationDataAdmin(objst.Id);
                            var tuple = new Tuple<FeesSubmit, BL_PrintAllRecord>(result, obj1);
                            return RedirectToAction("FeeSubmitManualAd");
                        }
                    }
                }
                return View(ob);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "manual Fee submit Get Method", eID.ToString());
                return RedirectToAction("FeeSubmitManualAd/" + obj.Id);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult Designation(int pageIndex = 1, int page = 0)
        {
            DesignationMaster ob = new DesignationMaster();
            DesignationMaster res = new DesignationMaster();
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            int pageSize = 10;
            if (page > 0) pageIndex = page;
            var result = ob.DesignationDetailList(collegeID, pageIndex, pageSize);
            res.DesignationList = result.qlist;
            ViewBag.totalCount = result.totalCount;
            return View(res);
        }
        public JsonResult CheckURoleValue(string name = "")
        {
            DesignationMaster st = new DesignationMaster();
            var obj = st.CheckDesignationName(name);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddNewDesignation(DesignationMaster ob)
        {
            DesignationMaster st = new DesignationMaster();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    ob.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    ob.InsertedBY = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                var result = st.AddNewDesignation(ob);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add New Designation method", jsonstring);
                st.Msg = "Something Went Wrong";
                return Json(st, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult checkpassingyear(string year = "", string sid = "", string docID = "")
        {
            StudentAdmissionQualification objl = new StudentAdmissionQualification();
            StudentAdmissionQualification objs = new StudentAdmissionQualification();
            if (sid != "")
            {
                string enid = EncriptDecript.Decrypt(sid);
                objs = objl.Checkpassingyear(year, enid, docID);
            }
            return Json(objs, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintApplicationCallReceipt()
        {
            string InpuData = HttpContext.Request.Form["data1"];       //Div Content will be fetched from form data.
                                                                       //If You find this error in above line 
                                                                       //Error: A potentially dangerous Request.Form value was detected from the client (data="<html><head><title><...").
                                                                       //then add this in your Web.config file.
                                                                       // <httpRuntime requestValidationMode="2.0" /> inside   <system.web>  </system.web>
            string PageType = HttpContext.Request.Form["PageType1"];  //here we will recieve Page Type sent from front end.
            string dataname = HttpContext.Request.Form["dataname1"];  // here we declare for filename for download
            if (string.IsNullOrEmpty(InpuData))
                InpuData = "Some Error occured.Content not found.Please try again.";

            string appPath = HttpContext.Request.PhysicalApplicationPath;

            var htmlContent = InpuData.Replace("AppPath", appPath);
            var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

            if (string.IsNullOrEmpty(PageType))
                pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
            else
            {
                if (PageType == "Landscape")
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                else
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
            }

            pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
            NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
            pageMargins.Bottom = 05;
            pageMargins.Left = 05;
            pageMargins.Right = 05;
            pageMargins.Top = 05;
            pdfDoc.Margins = pageMargins;                      //margins added to PDF.

            //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
            var pdfBytes = pdfDoc.GeneratePdf(htmlContent);
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.BinaryWrite(pdfBytes);
            HttpContext.Response.End();
            return View();

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult PrintfeeReceipt()
        {
            string InpuData = HttpContext.Request.Form["data"];       //Div Content will be fetched from form data.
                                                                      //If You find this error in above line 
                                                                      //Error: A potentially dangerous Request.Form value was detected from the client (data="<html><head><title><...").
                                                                      //then add this in your Web.config file.
                                                                      // <httpRuntime requestValidationMode="2.0" /> inside   <system.web>  </system.web>
            string PageType = HttpContext.Request.Form["PageType"];  //here we will recieve Page Type sent from front end.
            string dataname = HttpContext.Request.Form["dataname"];  // here we declare for filename for download
            if (string.IsNullOrEmpty(InpuData))
                InpuData = "Some Error occured.Content not found.Please try again.";

            string appPath = HttpContext.Request.PhysicalApplicationPath;

            var htmlContent = InpuData.Replace("AppPath", appPath);
            var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

            if (string.IsNullOrEmpty(PageType))
                pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
            else
            {
                if (PageType == "Landscape")
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                else
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
            }

            pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
            NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
            pageMargins.Bottom = 05;
            pageMargins.Left = 05;
            pageMargins.Right = 05;
            pageMargins.Top = 05;
            pdfDoc.Margins = pageMargins;                      //margins added to PDF.

            //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
            var pdfBytes = pdfDoc.GeneratePdf(htmlContent);
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.BinaryWrite(pdfBytes);
            HttpContext.Response.End();
            return View();

        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult FeeStructureReport()
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));//        objmaster.GetEducationType();

            ViewBag.Coursetype = "";
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            ViewBag.sessionid = session.GetAcademiccurrentSession().ID;
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.Subject = "";
            ViewBag.CourseYear = "";
            FeeStructure fee = new FeeStructure();
            ViewBag.FeeTypeList = fee.FeeTypeList();
            return View();
        }
        [HttpPost]
        public ActionResult FeeStructureReport(string id = "")
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));// objmaster.GetEducationType();

            ViewBag.Coursetype = "";
            AcademicSession sess = new AcademicSession();
            ViewBag.Session = sess.GetSession();
            ViewBag.sessionid = sess.GetAcademiccurrentSession().ID;
            Commn_master com = new Commn_master();
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            ViewBag.Subject = "";
            ViewBag.CourseYear = "";
            FeeStructure fee = new FeeStructure();
            ViewBag.FeeTypeList = fee.FeeTypeList();
            string session = Request.Form["session"];
            string College = Request.Form["College"];
            string FeeType = Request.Form["FeeType"];
            string EducationTypeID = Request.Form["EducationTypeID"];
            string CourseCategoryID = Request.Form["CourseCategoryID"];
            string Subject = Request.Form["Subject"];
            string CourseYearID = Request.Form["CourseYearID"];
            string CasteCategory = Request.Form["CasteCategory"];
            FeeStructure obj = new FeeStructure();
            FeeStructureList sub = new FeeStructureList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.FeestructureDetail(10000000, 1, CourseCategoryID, session, collegeID, Convert.ToInt32(CasteCategory==""?"0": CasteCategory), Convert.ToInt32(Subject==""?"0": Subject), Convert.ToInt32(EducationTypeID==""?"0": EducationTypeID), FeeType, CourseYearID);

            var result = sub.qlist.Select(x => new { Session = x.Sessionname, CourseName = x.coursecategoryName, StreamName = x.streamCategory, CasteCategory = x.castecategoryname, FeeType = x.feetype, CourseYear = x.YearName, FeeAmount = x.amount, LateFee = x.latefee, ConcessionFee = x.ConcessionFee }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FeeDetailList" + System.DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult RoomMaster(string id = "")
        {
            RoomMaster ob = new RoomMaster();
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";
            string enID = "";
            RoomMaster result = new RoomMaster();
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    result = ob.getRoomDetailBYID(enID);
                    result.EncriptedID = EncriptDecript.Encrypt(result.ID.ToString());

                }
                RoomMasterList res = ob.RoomDetailList(1, 25, collegeID);
                for (int i = 0; i < res.qlist.Count; i++)
                {
                    res.qlist[i].EncriptedID = EncriptDecript.Encrypt(res.qlist[i].ID.ToString());
                }
                result.qlist = res.qlist;
                result.totalCount = res.totalCount;
            }
            catch (Exception ex)
            {
                RoomMaster st = new RoomMaster();
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Room Master Get method", enID);
                st.Msg = "Something Went Wrong";
                return Json(st, JsonRequestBehavior.AllowGet);
            }
            return View(result);
        }
        public JsonResult CheckRoomName(string name = "")
        {
            RoomMaster st = new RoomMaster();
            var obj = st.CheckRoomName(name);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddNewRoomName(RoomMaster ob)
        {
            RoomMaster st = new RoomMaster();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    ob.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    ob.InsertedBY = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                if (ob.EncriptedID != null)
                {
                    ob.ID = (ob.EncriptedID != "" ? Convert.ToInt32(EncriptDecript.Decrypt(ob.EncriptedID)) : ob.ID);
                }

                var result = st.AddNewRoom(ob);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add New Room Name method", jsonstring);
                st.Msg = "Something Went Wrong";
                return Json(st, JsonRequestBehavior.AllowGet);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeRoomSeat()
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollege();
            RoomSeatMaster ob = new RoomSeatMaster();
            RoomSeatMaster objroom = new RoomSeatMaster();
            string enCollegeID = "";
            string enID = "";
            int eID = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
            }
            enID = (enCollegeID.Length > 0 ? EncriptDecript.Decrypt(enCollegeID) : "");
            eID = (enID != "" ? Convert.ToInt32(enID) : 0);

            List<RoomSeatMaster> result = ob.getSeatetail(eID);
            objroom.seatList = result;
            return View(objroom);
        }
        public JsonResult RoomSeatlist(string id = "")
        {
            RoomSeatMaster ob = new RoomSeatMaster();
            string enCollegeID = "";
            string enID = "";
            int eID = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
            }
            enID = (enCollegeID.Length > 0 ? EncriptDecript.Decrypt(enCollegeID) : "");
            eID = (enID != "" ? Convert.ToInt32(enID) : 0);
            var result = ob.getRoomdetail(id, eID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RoomSeatDescription(string id = "")
        {
            RoomSeatMaster ob = new RoomSeatMaster();
            var result = ob.getRoomSeatdetailBYID(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RoomSeatDetail()
        {
            RoomSeatMaster ob = new RoomSeatMaster();
            List<RoomSeatMaster> result = new List<RoomSeatMaster>();
            string enCollegeID = "";
            string enID = "";
            int eID = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
            }
            enID = (enCollegeID.Length > 0 ? EncriptDecript.Decrypt(enCollegeID) : "");
            eID = (enID != "" ? Convert.ToInt32(enID) : 0);
            result = ob.getSeatetail(eID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRoomSeats()
        {
            if (Request.Form.Count > 0)
            {
                RoomSeatMaster doc = new RoomSeatMaster();
                RoomSeatMaster result = new RoomSeatMaster();
                doc.Roomidlist = Request.Form["Roomidlist"];
                doc.RowList = Request.Form["RowList"];
                doc.ColList = Request.Form["ColList"];
                doc.CapacityList = Request.Form["CapacityList"];
                var Roomidlist = doc.Roomidlist.Split(',');
                var RowList = doc.RowList.Split(',');
                var ColList = doc.ColList.Split(',');
                var CapacityList = doc.CapacityList.Split(',');
                string jsonstring = JsonConvert.SerializeObject(doc);
                var isvaild = 1;
                try
                {
                    string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                    string collegeID = "";
                    string enID = "";

                    if (encollegeID != "0" && encollegeID.Length > 0)
                    {
                        collegeID = EncriptDecript.Decrypt(encollegeID);
                    }
                    doc.CollegeID = (collegeID != "" ? Convert.ToInt32(collegeID) : 0);
                    RoomSeatMaster ob = new RoomSeatMaster();
                    for (int i = 0; i < Roomidlist.Length - 1; i++)
                    {
                        ob.CollegeID = doc.CollegeID;
                        if (Roomidlist[i] == "")
                        {
                            isvaild = 0;
                            result.Msg = "Room ID is not Selected";
                            break;
                        }
                        ob.RoomMasterID = Convert.ToInt32(Roomidlist[i]);
                        if (RowList[i] == "")
                        {
                            isvaild = 0;
                            result.Msg = "Row Value is not Entered";
                            break;
                        }
                        ob.Row = Convert.ToInt32(RowList[i]);
                        if (ColList[i] == "")
                        {
                            isvaild = 0;
                            result.Msg = "Column Value is not Entered";
                            break;
                        }
                        ob.Column = Convert.ToInt32(ColList[i]);
                        if (CapacityList[i] == "")
                        {
                            isvaild = 0;
                            result.Msg = "Capacity Value is not Entered";
                            break;
                        }
                        ob.Capacity = Convert.ToInt32(CapacityList[i]);
                        var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                        if (insertby != "")
                        {
                            doc.InsertedBY = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                        }
                    }
                    if (isvaild == 1)
                    {
                        result = ob.SaveSeatStructure(doc);
                        if (result.Status)
                        {
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Room Seat Detail Post method", jsonstring);
                    FeeStructure ob3 = new FeeStructure();
                    ob3.Msg = "Error occurred. Error details: " + ex.Message;
                    return Json(ob3, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                FeeStructure ob3 = new FeeStructure();
                ob3.Msg = "Error occurred. Error details: ";
                return Json(ob3, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult UpdateRoomSeat(RoomSeatMaster ob)
        {
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                if (insertby != "")
                {
                    ob.InsertedBY = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                RoomSeatMaster result = new RoomSeatMaster();
                result = ob.UpdateRoomSeat(ob);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Fee sturcture Detail  update Post method", jsonstring);
                FeeStructure ob3 = new FeeStructure();
                ob3.Msg = "Error occurred. Error details: " + ex.Message;
                return Json(ob3, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult FeeStructureReportPrint(string EducationTypeID = "", string CourseCategoryID = "", string session = "")
        {
            FeeStructure ob = new FeeStructure();
            ob.EducationTypeName = EncriptDecript.Encrypt(EducationTypeID);
            ob.coursecategoryName = EncriptDecript.Encrypt(CourseCategoryID);
            ob.Sessionname = EncriptDecript.Encrypt(session);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrintFeepdf(string EducationTypeID = "", string CourseCategoryID = "", string session = "")
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            FeeStructure re = new FeeStructure();
            string educationTypeID = "", courseCategoryID = "", Session = "", CollegeID = "";
            if (EducationTypeID != "")
            {
                educationTypeID = EncriptDecript.Decrypt(EducationTypeID);
            }
            if (CourseCategoryID != "")
            {
                courseCategoryID = EncriptDecript.Decrypt(CourseCategoryID);
            }
            if (session != "")
            {
                Session = EncriptDecript.Decrypt(session);
            }
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";
            string enID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            ViewBag.educationTypeID = educationTypeID;
            ViewBag.courseCategoryID = courseCategoryID;
            if (session != "")
            {
                var res = re.GetSessionBYID(Convert.ToInt32(Session));
                ViewBag.session = res.Session;
            }
            ViewBag.subject = "";
            List<FeeStructure> ob = new List<FeeStructure>();
            FeeStructureList fee = re.FeestructureDetail(100000, 1, courseCategoryID, Session, collegeID, 0, 0, Convert.ToInt32(educationTypeID));
            ob = fee.qlist;
            return View(ob);
        }
        // [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentAttendanceReport(string Year = "", int SubjectID = 0, int StudentID = 0)
        {
            var CollegeID = 0; var EmployeeID = 0;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    model.EduTypeList = objEmp.GetEmployeeDropdown("EducationType", 0, 0, CollegeID, EmployeeID).Distinct().ToList();
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {
                    model.EduTypeList = objCourse.GetEducationTypeList();
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }
                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                model.AttendanceReport = st.StudentAttendanceReport(CollegeID, StudentID, SubjectID, Year).AttendanceReport;

            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult StudentAttendanceReport(string Year = "", int SubjectID = 0, int StudentID = 0, int CourseCategoryID = 0, int EduTypeList = 0, int EduTypeID = 0)
        {
            // var obj = "";
            var CollegeID = 0; var EmployeeID = 0;
            ViewBag.SubID = SubjectID;
            ViewBag.course = CourseCategoryID;
            ViewBag.EduTypeID = EduTypeID;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    model.EduTypeList = objEmp.GetEmployeeDropdown("EducationType", 0, 0, CollegeID, EmployeeID).Distinct().ToList();
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {
                    model.EduTypeList = objCourse.GetEducationTypeList();
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }
                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                //model.CourseCategoryID = model.CourseCategoryID;
                model.AttendanceReport = st.StudentAttendanceReport(CollegeID, StudentID, SubjectID, Year).AttendanceReport;
                StudentAttendanceAdd totalstdetail = new StudentAttendanceAdd();
                var resultdata = st.total(CollegeID, StudentID, SubjectID, Year, CourseCategoryID);
                ViewBag.Total = resultdata.Total;
                ViewBag.StudentName = resultdata.StudentName;
                ViewBag.RollNo = resultdata.RollNo;
                ViewBag.SessionName = resultdata.SessionName;
                ViewBag.streamCategory = resultdata.streamCategory;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(EduTypeID);
                model.SubjectList = objSubject.GetSubjectList(CourseCategoryID, CollegeID);
                model.StudentList = objStudent.GetStudentBySubjectList(CollegeID, Convert.ToInt32(Year), ViewBag.SubID);

            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }

        public ActionResult StudentAttendanceReportMonthwise(string Year = "", int SubjectID = 0, string Month = "", int CourseCategoryID = 0, int EduTypeList = 0, int EduTypeID = 0, int StudentYear = 0)
        {

            ViewBag.CourseYear = "";
            CollegeExamCenter CouYr = new CollegeExamCenter();
            ViewBag.CourseYear = CouYr.CourseYear(CourseCategoryID);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));



            var CollegeID = 0; var EmployeeID = 0;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            ViewBag.MonthList = Yearsession.GetMonth();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    //model.EduTypeList = objEmp.GetEmployeeDropdown("EducationType", 0, 0, CollegeID, EmployeeID).Distinct().ToList();
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {
                    //model.EduTypeList = objCourse.GetEducationTypeList();
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }
                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);

                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                model.AttendanceReport = st.StudentAttendanceReportMonthly(CollegeID, Month, SubjectID, Year, StudentYear).AttendanceReport;
                var resultdata = st.totalMonthly(CollegeID, Month, SubjectID, Year, CourseCategoryID);
                ViewBag.Total = resultdata.Total;
                ViewBag.SessionName = resultdata.SessionName;
                ViewBag.streamCategory = resultdata.streamCategory;
                ViewBag.CourseCategoryName = resultdata.CourseCategoryName;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(EduTypeID);
                model.SubjectList = objSubject.GetSubjectList(CourseCategoryID, CollegeID);
            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult StudentAttendanceReportMonthwise(int ID = 0, string Year = "", int SubjectID = 0, string Month = "", int CourseCategoryID = 0, int EduTypeList = 0, int EduTypeID = 0, int StudentYear = 0)
        {
            ViewBag.CourseYear = "";
            CollegeExamCenter CouYr = new CollegeExamCenter();
            ViewBag.CourseYear = CouYr.CourseYear(CourseCategoryID);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));

            var CollegeID = 0; var EmployeeID = 0;
            ViewBag.SubID = SubjectID;
            ViewBag.course = CourseCategoryID;
            ViewBag.EduTypeID = EduTypeID;
            ViewBag.Monthname = Month;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            ViewBag.MonthList = Yearsession.GetMonth();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    //model.EduTypeList = objEmp.GetEmployeeDropdown("EducationType", 0, 0, CollegeID, EmployeeID).Distinct().ToList();
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {
                    //model.EduTypeList = objCourse.GetEducationTypeList();
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }
                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                //model.CourseCategoryID = model.CourseCategoryID;
                model.AttendanceReport = st.StudentAttendanceReportMonthly(CollegeID, Month, SubjectID, Year, StudentYear).AttendanceReport;
                StudentAttendanceAdd totalstdetail = new StudentAttendanceAdd();

                var resultdata = st.totalMonthly(CollegeID, Month, SubjectID, Year, CourseCategoryID, StudentYear);
                ViewBag.Total = resultdata.Total;
                ViewBag.SessionName = resultdata.SessionName;
                ViewBag.streamCategory = resultdata.streamCategory;
                ViewBag.CourseCategoryName = resultdata.CourseCategoryName;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(EduTypeID);
                model.SubjectList = objSubject.GetSubjectList(CourseCategoryID, CollegeID);

            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }
        public ActionResult StudentMonthlllyAttendanceStudentwise(int ID = 0, string Year = "", int SubjectID = 0, int StudentID = 0, string Month = "", int CourseCategoryID = 0, int EduTypeList = 0, int EduTypeID = 0, int StudentYear = 0)
        {
            ViewBag.CourseCategoryName = "";
            ViewBag.CourseYear = "";
            CollegeExamCenter CouYr = new CollegeExamCenter();
            ViewBag.CourseYear = CouYr.CourseYear(CourseCategoryID);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));


            var CollegeID = 0; var EmployeeID = 0;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            ViewBag.MonthList = Yearsession.GetMonth();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {

                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }
                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);

                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                model.AttendanceReport = st.AttendanceMonthlyStudentWise(CollegeID, Month, SubjectID, Year, StudentID, StudentYear).AttendanceReport;

            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult StudentMonthlllyAttendanceStudentwise(string Year = "", int SubjectID = 0, int StudentID = 0, string Month = "", int CourseCategoryID = 0, int EduTypeList = 0, int EduTypeID = 0, int StudentYear = 0)
        {
            ViewBag.CourseYear = "";
            CollegeExamCenter CouYr = new CollegeExamCenter();
            ViewBag.CourseYear = CouYr.CourseYear(CourseCategoryID);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));


            var CollegeID = 0; var EmployeeID = 0;
            ViewBag.SubID = SubjectID;
            ViewBag.course = CourseCategoryID;
            ViewBag.EduTypeID = EduTypeID;
            ViewBag.Monthname = Month;
            ViewBag.SubID = SubjectID;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            Recruitment objStudent = new Recruitment();
            AcademicSession session = new AcademicSession();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            ViewBag.MonthList = Yearsession.GetMonth();
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                if (usertype == 2)
                {
                    model.CourseCategoryList = objEmp.GetEmployeeDropdown("", 0, 0, 0, 0);
                    model.SubjectList = objEmp.GetEmployeeDropdown("Detail", 0, 0, 0, 0);
                }
                else if (usertype == 1)
                {
                    model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                    model.SubjectList = objSubject.GetSubjectList(0, 0);
                }


                model.StudentList = objStudent.GetStudentBySubjectList(0, 0, 0);
                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.MonthList = st.GetMonthList();
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;

                model.AttendanceReport = st.AttendanceMonthlyStudentWise(CollegeID, Month, SubjectID, Year, StudentID, StudentYear).AttendanceReport;
                StudentAttendanceAdd totalstdetail = new StudentAttendanceAdd();

                var resultdata = st.totalMonthlyStudentWise(CollegeID, Month, SubjectID, Year, CourseCategoryID, StudentID, StudentYear);
                ViewBag.Total = resultdata.Total;
                ViewBag.SessionName = resultdata.SessionName;
                ViewBag.streamCategory = resultdata.streamCategory;
                ViewBag.StudentName = resultdata.StudentName;
                ViewBag.RollNo = resultdata.RollNo;
                ViewBag.CourseCategoryName = resultdata.CourseCategoryName;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(EduTypeID);
                model.SubjectList = objSubject.GetSubjectList(CourseCategoryID, CollegeID);
                model.StudentList = objStudent.GetStudentBySubjectList(CollegeID, Convert.ToInt32(Year), ViewBag.SubID);

            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }
        public ActionResult StudentAttendancePercentageReport(string Year = "0", int SubjectID = 0, int StudentID = 0, int CourseCategoryID = 0, string PercentageSearch = "100", int StudentYear = 0)
        {
            ViewBag.CourseYear = "";
            CollegeExamCenter CouYr = new CollegeExamCenter();
            ViewBag.CourseYear = CouYr.CourseYear(CourseCategoryID);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));

            var CollegeID = 0; var EmployeeID = 0;
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            AcademicSession session = new AcademicSession();

            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            ViewBag.MonthList = Yearsession.GetMonth();
            AcademicSession AttendancePercentage = new AcademicSession();
            AttendancePercentage = Yearsession.AttendancePercentage();
            ViewBag.AttendancePercentage = AttendancePercentage.MinumPercentage;
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                model.SubjectList = objSubject.GetSubjectList(CourseCategoryID, CollegeID);

                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                model.AttendanceReport = st.StudentAttendancePercentageReport(CollegeID, Convert.ToInt32(Year), CourseCategoryID, PercentageSearch, StudentYear, SubjectID).AttendanceReport;

            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult StudentAttendancePercentageReport(string Year = "", int SubjectID = 0, string Month = "", int CourseCategoryID = 0, int EduTypeList = 0, int EduTypeID = 0, int StudentID = 0, string PercentageSearch = "", int StudentYear = 0)
        {
            ViewBag.CourseYear = "";
            CollegeExamCenter CouYr = new CollegeExamCenter();
            ViewBag.CourseYear = CouYr.CourseYear(CourseCategoryID);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            BL_StreamMaster objSubject = new BL_StreamMaster();
            // var obj = "";
            var CollegeID = 0; var EmployeeID = 0;
            ViewBag.course = CourseCategoryID;
            ViewBag.EduTypeID = EduTypeID;
            if (PercentageSearch == "")
            {
                PercentageSearch = "100";
            }
            var collegeid = ClsLanguage.GetCookies("ENNBCLID");
            if (collegeid != "")
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(collegeid));
            }
            var eid = ClsLanguage.GetCookies("ENNBUID");
            if (eid != "")
            {
                var eID = Convert.ToInt32(EncriptDecript.Decrypt(eid));
                EmployeeRegisteration ob = new EmployeeRegisteration();
                var result = ob.getdetailsByEID(eID);
                EmployeeID = Convert.ToInt32(result.EmployeeID);
            }
            StudentAttendanceAdd model = new StudentAttendanceAdd();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            AcademicSession session = new AcademicSession();

            UserLogin objEmp = new UserLogin();
            AcademicSession Yearsession = new AcademicSession();
            ViewBag.Session = Yearsession.GetSession();
            ViewBag.MonthList = Yearsession.GetMonth();
            AcademicSession AttendancePercentage = new AcademicSession();
            AttendancePercentage = Yearsession.AttendancePercentage();
            ViewBag.AttendancePercentage = AttendancePercentage.MinumPercentage;
            StudentAttendanceAdd st = new StudentAttendanceAdd();
            List<StudentAttendanceAdd> AttList = new List<StudentAttendanceAdd>();
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                model.SessionID = session.GetSession().Where(x => x.IsCurrent == true).FirstOrDefault().ID;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(0);
                model.SubjectList = objSubject.GetSubjectList(CourseCategoryID, CollegeID);


                model.EmployeeList = objEmp.GetEmployeeList(usertype, CollegeID);
                model.YearList = st.GetYearList();
                model.CollegeID = CollegeID;
                model.EmployeeID = EmployeeID;
                model.UserType = usertype;
                model.AttendanceReport = st.StudentAttendancePercentageReport(CollegeID, Convert.ToInt32(Year), CourseCategoryID, PercentageSearch, StudentYear, SubjectID).AttendanceReport; ;
                StudentAttendanceAdd totalstdetail = new StudentAttendanceAdd();

                var resultdata = st.StudentAttendancePercentageReporttotal(Convert.ToInt32(Year), CourseCategoryID, StudentYear, SubjectID);
                ViewBag.coursname = resultdata.CourseCategoryName;
                ViewBag.SessionName = resultdata.SessionName;
                ViewBag.streamCategory = resultdata.streamCategory;
                model.CourseCategoryList = objCourse.GetCourseCategoryList(EduTypeID);


            }
            catch (Exception ex)
            {
                TempData["StError"] = ex.Message.ToString();
            }
            return View(model);
        }

        public ActionResult CounsellingSheetReporCollegeWiset2()
        {
            Commn_master com = new Commn_master();
            AcademicSession session = new AcademicSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            return View();
        }
        [HttpPost]
        public JsonResult CounsellingSheetReporCollegeWiset2(string EducationType = "", string ddlsession = "", string Coursetype = "")
        {
            BL_CounsellingSheetReport PritApp = new BL_CounsellingSheetReport();
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = PritApp.GetCounsellingSheetReportCollegeWise2(EducationType, ddlsession, Coursetype, CollegeID);
            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CounsellingSeatReporCollegeWise()
        {
            Commn_master com = new Commn_master();
            AcademicSession session = new AcademicSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            return View();
        }
        [HttpPost]
        public JsonResult After1COUNSELLINGREMSEAT(string EducationType = "", string ddlsession = "", string Coursetype = "")
        {
            BL_CounsellingSheetReport PritApp = new BL_CounsellingSheetReport();
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = PritApp.After1COUNSELLINGREMSEAT(EducationType, ddlsession, Coursetype, CollegeID);
            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult FeeSubmitVocational(string id = "")
        {
            string enID = "";
            FeesSubmit ob = new FeesSubmit();
            Commn_master com = new Commn_master();
            ViewBag.paymode = ob.GetPaymentType();
            BL_PrintAllRecord print = new BL_PrintAllRecord();
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin objs = new StudentLogin();
                    var objl = objs.BasicDetailByID(eID);
                    var docstatus = ob.DocumentStatus(eID);
                    ViewBag.DocStatus = (docstatus == 1 ? "Verified" : (docstatus == 0 ? "Pending" : "Rejected"));
                    ViewBag.Name = objl.Name;
                    ViewBag.edu = objl.EducationType;
                    FeesSubmit feesubmitstaus = ob.FeeSubmitStatus(eID);
                    if (feesubmitstaus.Status)
                    {
                        ViewBag.feeStatus = feesubmitstaus.IsFeeSubmit;
                        ViewBag.feeStatusval = (feesubmitstaus.IsFeeSubmit == "True" ? "Paid" : "Pending");
                        return View(feesubmitstaus);
                    }
                    else
                    {
                        ViewBag.feeStatus = "false";
                        return View();
                    }

                    //return View();
                }
                else
                {

                    TempData["msgfees"] = "something Went Wrong!! ";
                    return View();
                }

                //return View(ob);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "fee Submit For Vocational Get Method", eID.ToString());
                return RedirectToAction("DocumentVerifyList");
            }



        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult FeeSubmitVocational(FeesSubmit obj, string id = "")
        {
            string enID = "";
            FeesSubmit ob = new FeesSubmit();
            Commn_master com = new Commn_master();
            ViewBag.paymode = ob.GetPaymentType();
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            int eID = 0;
            try
            {
                if (obj.Id != "0" && obj.Id.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(obj.Id);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }


                    StudentLogin tblST = new StudentLogin();
                    var objst = tblST.BasicDetailByID(eID);
                    var docstatus = ob.DocumentStatus(eID);
                    ViewBag.DocStatus = (docstatus == 1 ? "Verify" : (docstatus == 0 ? "Pending" : "Rejected"));
                    ViewBag.Name = objst.Name;

                    obj.SID = eID;
                    string enCollegeID = "";

                    string encID = "";
                    int ecID = 0;
                    if (ClsLanguage.GetCookies("ENNBCLID") != null)
                    {
                        enCollegeID = ClsLanguage.GetCookies("ENNBCLID");
                        encID = EncriptDecript.Decrypt(enCollegeID);
                        if (encID != "")
                        {
                            ecID = Convert.ToInt32(encID);
                        }
                    }
                    if (objst.EducationType == 13)// for vocatinal check
                    {
                        if (obj.Feesval <= 0)
                        {
                            TempData["msgfees"] = "Fee should be greater then 0!!! ";
                            return RedirectToAction("FeeSubmitVocational");
                        }
                    }
                    AcademicSession ac = new AcademicSession();
                    obj.Session = ac.GetAcademiccurrentSession().ID.ToString();
                    obj.collegeID = ecID;
                    if (ClsLanguage.GetCookies("ENNBUID") != null)
                    {
                        obj.Insertedby = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
                    }
                    var result = ob.FeessubVocational(obj);

                    if (result.Status == true)
                    {
                        TempData["msgfees"] = result.Msg;
                        return RedirectToAction("FeeSubmitVocational");
                        //return View(ob);
                    }
                    else
                    {
                        TempData["msgfees"] = result.Msg;
                        return View(obj);
                        // return View(result);
                    }

                }

                return View(ob);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "fee Submit For Vocational Get Method", eID.ToString());
                return RedirectToAction("DocumentVerifyList");
            }
        }
      //  [VerifyUrlFilterCollegeAttribute]
        public ActionResult GenerateRollNo()
        {
            //BL_courseMaster objmaster = new BL_courseMaster();
            //ViewBag.EducationType = objmaster.GetEducationType();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();

            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            return View();
        }
        [BasicAuthentication]
        public JsonResult generaterollnoforstudent(string EducationTypeID = "", string CourseCategoryID = "")
        {
            StudentRollNo ob = new StudentRollNo();
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = ob.InsertStudentRollNo(CollegeID, CourseCategoryID);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [BasicAuthentication]
        public JsonResult getRollNoStatus(string CourseCategoryID = "")
        {
            StudentRollNo ob = new StudentRollNo();
            string CollegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = ob.GETStudentRollNoStatus(CollegeID, CourseCategoryID);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentRollNoList()
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            return View();
        }

        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult StudentRollNoList(int id = 0)
        {

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();

            ViewBag.Coursetype = "";
            string EducationTypeID = Request.Form["program"];
            string session1 = Request.Form["session"];
            string CourseCategoryID = Request.Form["CoursetypeID"];
            string Name = Request.Form["Name"];
            string RollNo = Request.Form["RollNo"];

            StudentRollNo obj = new StudentRollNo();
            StudentRollNoList sub = new StudentRollNoList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.RollNoListOfStd(1, 1000000, collegeID, EducationTypeID, CourseCategoryID, Name, RollNo, session1);
            var result = sub.qlist.Select(x => new { ApplicationNo = x.ApplicationNo, RollNo = x.RollNo, StudentName = x.StudentName, Course = x.CourseCategory, HonoursSubject = x.StreamCategory, Session = x.SessionName }).ToList();

            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=StudentRollNoList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [BasicAuthentication]

        public JsonResult getStudentFeeDetail(string id = "")
        {
            BL_PrintRecipt ob = new BL_PrintRecipt();
            int sid = (id != "" ? Convert.ToInt32(id) : 0);
            var obj = ob.GetPaymentDetails(sid);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CollegeRemainingSeat(string sid = "", string CastCategory = "")
        {
            DataLayer.Login obj = new DataLayer.Login();

            Commn_master com = new Commn_master();
            if (sid != "0" && sid.Length > 0)
            {
                string eID = EncriptDecript.Decrypt(sid);
                int stdid = (eID != "" ? Convert.ToInt32(eID) : 0);
                string CollegeID = "";
                string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    CollegeID = EncriptDecript.Decrypt(encollegeID);
                }
                BL_StreamMaster objStream = new BL_StreamMaster();
                int CID = (CollegeID != "" ? Convert.ToInt32(CollegeID) : 0);
                StudentLogin objs = new StudentLogin();
                AcademicSession ac = new AcademicSession();
                obj = objs.BasicDetailwithrecruitmentByID(stdid);
                int sessionid = ac.GetAcademiccurrentSession().ID;
                int hounors_subjectid = objs.ByIDgethounors_subjectid(stdid, sessionid).hounors_subjectid;
                obj = objs.CollegeSeatDetail(hounors_subjectid, CollegeID, CastCategory, obj.CourseCategory, obj.AdmisitionCategory);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
            //   return Json(new { data = res, success = true });

        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult MigrationCertificate()
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            ViewBag.CuSession = session.GetAcademiccurrentSession().ID;
            return View();
        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult FindStudentDetail(string RegistrationNo = "", string session = "")
        {
            ExamForm ob = new ExamForm();
            ExamForm result = new ExamForm();
            int sess = (session != "" ? Convert.ToInt32(session) : 0);
            if (RegistrationNo != "")
            {
                result = ob.StudentDetailForCertificate(RegistrationNo, sess);

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult PrintStudentCertificate(string sid = "", string session = "", string Type = "")
        {
            ExamForm ob = new ExamForm();
            ExamForm result = new ExamForm();
            int stid = (sid != "" ? Convert.ToInt32(sid) : 0);
            int sess = (session != "" ? Convert.ToInt32(session) : 0);
            var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID"))) : 0);

            result = ob.StudentDetailForPrintCertificate(stid, sess, insertby, Type);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ProvisionalCertificate()
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            ViewBag.CuSession = session.GetAcademiccurrentSession().ID;
            return View();
        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult FindStudentDetailforProvisional(string RegistrationNo = "", string session = "")
        {
            ExamForm ob = new ExamForm();
            ExamForm result = new ExamForm();
            int sess = (session != "" ? Convert.ToInt32(session) : 0);
            if (RegistrationNo != "")
            {
                result = ob.StudentDetailForProvisional(RegistrationNo, sess);

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ExaminationAdmitCard()
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            ViewBag.CuSession = session.GetAcademiccurrentSession().ID;
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            ViewBag.Coursecategorydrop = "";
            ViewBag.CourseYear = "";
            ViewBag.Subject = "";
            return View();
        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult FindStudentDetailforAdmitCard(string RegistrationNo = "", string session = "")
        {
            ExamForm ob = new ExamForm();
            ExamForm result = new ExamForm();
            //int sess = (session != "" ? Convert.ToInt32(session) : 0);
            if (RegistrationNo != "")
            {
                result = ob.StudentDetailForAdmitCardCollege(RegistrationNo, session);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        //[BasicAuthentication]
        public ActionResult ExaminationAdmitCard(string session = "", int program = 0, int Course = 0, int CourseYearID = 0, string Examtype = "", int streamcategoryid = 0)
        {
            ExamForm ob = new ExamForm();
            ExamForm result = new ExamForm();
            PrintExamForm_admicard resultlist = new PrintExamForm_admicard();
            
            string CollegeID = "";
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            AcademicSession session44 = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.Session = session44.GetSession();
            ViewBag.CuSession = session44.GetAcademiccurrentSession().ID;
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            ViewBag.Subject = "";
            ViewBag.Coursecategorydrop = com.getcommonMaster("coursesfordrop");
            if (program > 0)
            {
                ViewBag.Coursecategorydrop = com.getcommonMaster("Course", program);
            }
            if (Course > 0)
            {
                ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Course);
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, Course, Convert.ToInt32(CollegeID));
            }


            if (Examtype == "" || Examtype == "0")
            {
                result.Status = false;
                TempData["Admitcard"] = "Please Select Exam Type !!";
                return View();

            }
            if (Examtype == "1")
            {
                resultlist = ob.StudentDetailForAdmitCardCollege_mainexam(program.ToString(), session, Course.ToString(), CourseYearID.ToString(), Examtype, CollegeID, streamcategoryid.ToString());
            }
            if (Examtype == "2")
            {
                resultlist = ob.StudentDetailForAdmitCardCollege_backexam(program.ToString(), session, Course.ToString(), CourseYearID.ToString(), Examtype, CollegeID, streamcategoryid.ToString());
            }
            if (resultlist.Studentlist == null)
            {
                result.Status = false;
                TempData["Admitcard"] = "Record Not Found !!";
                return View();
                // return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (resultlist.Studentlist.Count == 0)
            {
                result.Status = false;
                TempData["Admitcard"] = "Record Not Found !!";
                return View();
                // return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.Status = true;
                result.Msg = "";
                if (program == 11)// for UG
                {
                    GenerateAdmitcard_UG(resultlist);
                }
                else if (program == 40)// for B.ed
                {
                    GenerateAdmitcard_Bed(resultlist, Examtype);
                }
                else if (program == 41)// for LLB
                {
                    GenerateAdmitcard_LLB(resultlist, Examtype);
                }
                else if (program == 12)// for PG
                {
                    GenerateAdmitcard_PG(resultlist, Examtype);
                }
                else
                {
                    GenerateAdmitcard_Other(resultlist,Examtype);
                }
                TempData["Admitcard"] = "Download Successfully !!!!";
                return RedirectToAction("ExaminationAdmitCard");
            }

        }
        

        [ValidateInput(false)]
        public ActionResult DownloadAdmitcard(string applicationno = "", string exmtype = "", string session = "", string EducationTypeID = "",string CourseCategoryID = "0",string CourseYearID = "0")
        {
            ExamForm ob = new ExamForm();
            ExamForm result = new ExamForm();
            PrintExamForm_admicard resultlist = new PrintExamForm_admicard();
            string CollegeID = "";
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }

            AcademicSession session44 = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objmaster = new BL_courseMaster();
            if (exmtype == "" || exmtype == "0")
            {
                return View();
            }
            if (EducationTypeID == "" || EducationTypeID == "0")
            {
                return View();
            }
            if (session == "" || session == "0")
            {
                return View();
            }
            if (applicationno == "" || applicationno == "0")
            {
                return View();
            }
            if (exmtype == "1")
            {
                resultlist = ob.StudentDetailForAdmitCardCollege_mainexam_byapplicationo(EducationTypeID, session, CollegeID, applicationno.ToString(), CourseCategoryID, CourseYearID);
            }
            if (exmtype == "2")
            {
                resultlist = ob.StudentDetailForAdmitCardCollege_backexam_byapplicationo(EducationTypeID, session, CollegeID, applicationno.ToString(), CourseCategoryID, CourseYearID);
            }
            if (resultlist.Studentlist == null)
            {
                result.Status = false;
                TempData["Admitcard"] = "Record Not Found !!";
                return View();
                // return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (resultlist.Studentlist.Count == 0)
            {
                //result.Status = false;
                TempData["Admitcard"] = "Record Not Found !!";
                return RedirectToAction("ExaminationAdmitCard");
                // return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result.Status = true;
                result.Msg = "";
                if (EducationTypeID == "11")// for UG
                {
                    GenerateAdmitcard_UG(resultlist);
                }
                else if (EducationTypeID == "41")// for LLB
                {
                    GenerateAdmitcard_LLB(resultlist, exmtype);
                }
                else if (EducationTypeID == "40")// for B.ed
                {
                    GenerateAdmitcard_Bed(resultlist, exmtype);
                }
                else if (EducationTypeID == "12")// for PG
                {
                    GenerateAdmitcard_PG(resultlist, exmtype);
                }
                else
                {
                    GenerateAdmitcard_Other(resultlist, exmtype);
                }
                return View();
            }
            return View();
        }
        public void GenerateAdmitcard_UG(PrintExamForm_admicard resultlist)
        {
            //TempData["Admitcard"] = "No record found !!!!";
            //RedirectToAction("ExaminationAdmitCard");
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < resultlist.Studentlist.Count; i++)
            {
                string content = "";
                content += @"
                               <div class=""spacedesign"" style=""page-break-after:always"">";
                // with background Image
                //content += @" <div style='width:800px; padding:0 100px; margin:auto; background-image:url(https://collegeportal.mungeruniversity.ac.in/images/bg.jpg); background-position:top center; background-size:100%; background-repeat:no-repeat;'>";
                // without background Image
                content += @"   <div style='height:50px'>&nbsp;&nbsp;&nbsp; </div>
            <div style='width:800px; padding:0px 70px; margin:auto;border:5px groove #000 ;'>";

                content += @" <div style=''> <table width='100%' border='0' cellspacing='0' cellpadding='0' >
    <tr>
                        <td align='center' valign='middle'>&nbsp;</td>
                    </tr>
  <tr>
    <td align='center' valign='middle'>
    	<img src='https://collegeportal.mungeruniversity.ac.in/images/logotree.png' alt='' width='150'>
    </td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:36px;'>Munger University, MUNGER</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:14px;'>(Administrative Block, Shastrinagar, Munger (BIHAR) - 811201</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:18px;'><strong>" + resultlist.Studentlist[i].CourseCategory.ToString() + "  " + resultlist.Studentlist[i].YearName.ToString() + @"  Examination –  " + resultlist.Studentlist[i].Examyear.ToString()+" (Session : " + resultlist.Studentlist[i].SessionName + ")" + @"</strong></td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:22px;'>ADMIT CARD</td>
  </tr>
    <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
    <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
      <tr>
        <td width='76%' align='left' valign='top'  style='padding:5px'>
                 <table width='100%'>
                                                        <tr>
                                                            <td  width='30%'>
                                                                Registration Number :
                                                            </td>
                                                            <td >
                                                                <span style='border:0; width:100px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].RegistrationNo.ToString() + @"</span>
                                                            </td>
                                                            <td>
                                                                Year :   
                                                            </td>
                                                            <td>
                                                                <span style='border:0; width:160px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].Registrationyear.ToString() + @"</span>
                                                            </td>
                                                        </tr>
                </table>
 </td>
       

        <td width='5%' rowspan='6'> &nbsp;</td>
        <td width='19%' rowspan='5' style='border:1px solid #333; padding:5px 5px 2px;'>
        	<img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].photo.ToString() + @""" width='180' height='180'>
        </td>
      </tr>
      <tr align='left' valign='top' >
        <td style='padding:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Name <BR>(In Block Letter):
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].StudentName.ToUpper().ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>


</td>
        </tr>
      <tr align='left' valign='top'>
        <td  style='padding:5px'>
 <table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                              " + (resultlist.Studentlist[i].ftitle.ToString() == "20" ? " Father's " : (resultlist.Studentlist[i].ftitle.ToString() == "21" ? "  Husband's  " : " Father's ")) + @" Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].FatherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

                                            


</td>
        </tr>
      <tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Mother&rsquo;s Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].MotherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

</td>
        </tr>
<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                               College Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].CollegeName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

</td>
        </tr>
      <tr>
        <td align='left' valign='bottom'><strong>Subject of Examination:</strong></td>
        <td align='center' valign='middle' style='border:1px solid #333;'><img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].sign.ToString() + @"""  width='110'  height='50'></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>
 <div style='min-height:270px'>
<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #333;'>
      <tr>
        <td colspan='6' align='center' valign='middle' style='border-bottom:1px solid #333; padding:10px 0;'><strong>Subject Details</strong></td>
        </tr>";
                content += @" <tr>
        <td align='center' valign='middle' width='40' style='border-bottom:1px solid #333; border-right:1px solid #333;'>S. No</td>
        <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Subject</td>
        <td align='center' valign='middle' width='70' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Paper</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Date</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Sitting</td>
        <td align='center' valign='middle' style='border-bottom:1px solid #333;'>Examination Centre</td>
      </tr>";


                int iii = 1;

                foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours" && R.StreamCategoryID == resultlist.Studentlist[i].StreamCategoryID))
                {


                    content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + resultlist.Studentlist[i].HonoursSubject.ToString() + @" (Honours)</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; width:100px''>" + resultlist.Studentlist[i].HONSCENTER + @"</td>
                                                        </tr>";
                    iii = iii + 1;
                }

                if (resultlist.Studentlist[i].courseyearid == 8)// for bcom part 2
                {
                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.sessionid == resultlist.Studentlist[i].sessionid && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "SubSidiary" && R.StreamCategoryID == 1122))
                    {

                        content += @"
                                                           <tr >
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "Money and Banking" + @" (SUBS-1)</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; width:100px''>" + resultlist.Studentlist[i].SUBS1CENTER + @"</td>
                                                        </tr>";
                        iii = iii + 1;

                    }
                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.sessionid == resultlist.Studentlist[i].sessionid && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "SubSidiary" && R.StreamCategoryID == 1123))
                    {

                        content += @"
                                                           <tr >
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "Economics Development of India" + @"(SUBS-2)</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333;  '>" + resultlist.Studentlist[i].SUBS2CENTER + @"</td>
                                                        </tr>";
                        iii = iii + 1;
                    }
                }
                else
                {

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "SubSidiary" && R.StreamCategoryID == resultlist.Studentlist[i].Subsidiary1))
                    {

                        content += @"
                                                           <tr >
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + resultlist.Studentlist[i].Subsidiary1Subject.ToString() + @" (SUBS-1)</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; width:100px''>" + resultlist.Studentlist[i].SUBS1CENTER + @"</td>
                                                        </tr>";
                        iii = iii + 1;
                    }
                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "SubSidiary" && R.StreamCategoryID == resultlist.Studentlist[i].Subsidiary2))
                    {

                        content += @"
                                                           <tr >
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + resultlist.Studentlist[i].Subsidiary2Subject.ToString() + @" (SUBS-2)</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333;  '>" + resultlist.Studentlist[i].SUBS2CENTER+ @"</td>
                                                        </tr>";
                        iii = iii + 1;
                    }

                }



                foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Composition1" && R.StreamCategoryID == resultlist.Studentlist[i].Compulsory1))
                {

                    content += @"
                                                           <tr >
                                                            <td align='center' valign='middle' rowspan='2' style='border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' style='border-right:1px solid #333;  padding:5px 0;'>" + resultlist.Studentlist[i].Compulsory1Subject.ToString() + @" (COMPULSORY)</td>
                                                            <td align='center'   rowspan='2' valign='middle' style='border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center'   rowspan='2' valign='middle' style='border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center'  rowspan='2' valign='middle' style='border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            <td align='center'  rowspan='2' valign='middle' style=''>" + resultlist.Studentlist[i].COM1CENTER + @"</td>
                                                        </tr>";
                    iii = iii + 1;
                }


                if (resultlist.Studentlist[i].Compulsory2 > 0)
                {

                    content += @"
                                                           <tr >
                                                
                                                            <td align='center' valign='middle' style='border-right:1px solid #333; border-top:1px solid #333;padding:5px 0;''>" + resultlist.Studentlist[i].Compulsory2Subject.ToString() + @" (COMPULSORY)</td>
                            
                                                        </tr>";

                }
                else
                {
                    content += @"
                                                           <tr >
                                                
                                                            <td align='center' valign='middle' style='border-right:1px solid #333; '></td>
                            
                                                        </tr>";
                }

                content += @" 
      
";

                content += @" 
    </table>
</div>
</td>
  </tr>
 

 
  <tr>
    <td align='left' valign='middle'><strong>To be filled by office:</strong></td>
  </tr>
 
  <tr>
    <td align='left' valign='middle'>Exam Roll No <strong>" + resultlist.Studentlist[i].RollNo.ToString() + @"</strong> is allowed to appear at the " + resultlist.Studentlist[i].CourseCategory.ToString() + "           " + resultlist.Studentlist[i].YearName.ToString() + @" Exam " + resultlist.Studentlist[i].Examyear.ToString() + @" Commencing from <strong>" + resultlist.Studentlist[i].ExamStartDate.ToString() + @" </strong>
</td>
  </tr>
  
  <tr>
    <td align='left' valign='middle'>&nbsp;</td>
  </tr>
        <tr>
            <td align='left' valign='middle'>&nbsp;</td>
        </tr>
 
  <tr>
    <td align='left' valign='middle'>
    	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td align='left' valign='top' width='50% '> &nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td align='right' valign='top' width='50% '><img src=""https://collegeportal.mungeruniversity.ac.in/images/amar_examinationcontroller.png"" width='100'></td>
          </tr>
        <tr>
            <td align='left' valign='top' width='50% '>Principal</td>
            <td align='right' valign='top' width='50% '>Controller of Examinations<br>Munger University, Munger</td>
        </tr>
        </table>
    </td>
  </tr>
  
    <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
     <tr>
      <td align='left' valign='middle' style='padding-bottom:10px;'><strong>GUIDELINES FOR THE EXAMINEE</strong></td>
    </tr>
    <tr>
      <td align='left' valign='middle'>
      	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td width='4%' align='left' valign='top'>1.</td>
            <td width='96%' align='left' valign='top'>The examinee is expected to be present at the examination centre 30 minutes before the commencement of examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>2.</td>
            <td align='left' valign='top'>No examinee shall be admitted to the examination hall after 30 minutes of commencement of the examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>3.</td>
            <td align='left' valign='top'>The examinee shall have the proper Admit Card and the valid college identity card with him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>4.</td>
            <td align='left' valign='top'>Examinees are not permitted to leave examination hall during the initial 1 hour and last 15 minutes of the examination. </td>
          </tr>
          <tr>
            <td align='left' valign='top'>5.</td>
            <td align='left' valign='top'>The examinee shall ensure occupying the seat allotted to him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>6.</td>
            <td align='left' valign='top'>The examinee is prohibited from keeping in his possession in the examination hall any blank paper, notes, scribbles, chits, books, mobile phone, pager, programmable calculator, electronic communication device etc. except the Admit Card and Blue/ Black Ball Point pen.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>7.</td>
            <td align='left' valign='top'>	The examinee shall cross (×) the blank page(s) of Answer Book left after finishing writing answers.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>8.</td>
            <td align='left' valign='top'>Any unfair means adopted by the examinee shall invite necessary punitive action.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>9.</td>
            <td align='left' valign='top'>Each examinee shall ensure the use of mask and sanitizer at the exam centre.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>10.</td>
            <td align='left' valign='top'>The examinee shall fill all necessary information’s on the front page of the answer book clearly. </td>
          </tr>
        </table>

      </td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>

   

  </table></div>
</div>
    </div>
                            ";
                builder.Append(content);
            }
            
                try
                {
                    string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content=\"text/html;charset=utf-8\" /></head><body style='padding: 0; margin: 0; font-size:16px; '>" + builder.ToString() + "</body></html>";
                    //Div Content will be fetched from form data.                                                    
                    string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
                    string dataname = "ExaminationAdmitCard";  // here we declare for filename for download
                    if (string.IsNullOrEmpty(InpuData))
                        InpuData = "Some Error occured.Content not found.Please try again.";
                    string appPath = HttpContext.Request.PhysicalApplicationPath;

                    var htmlContent = InpuData.Replace("AppPath", appPath);
                    var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

                    if (string.IsNullOrEmpty(PageType))
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
                    else
                    {
                        if (PageType == "Landscape")
                            pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                        else
                            pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                    }
                    pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                    NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                    pageMargins.Bottom = 0;
                    pageMargins.Left = 0;
                    pageMargins.Right = 0;
                    pageMargins.Top = 0;
                    pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                                                                       //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
                    var pdfBytes = pdfDoc.GeneratePdf(htmlContent);

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(pdfBytes);
                    Response.Flush();
                    Response.End();
                    HttpContext.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    HttpContext.ApplicationInstance.CompleteRequest();
                }
            
        }

        public void GenerateAdmitcard_LLB(PrintExamForm_admicard resultlist, string ExamType)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < resultlist.Studentlist.Count; i++)
            {
                string content = "";
                content += @"
                               <div class=""spacedesign"" style=""page-break-after:always"">";
                // with background Image
                content += @" <div style='width:800px; padding:0 100px; margin:auto; background-image:url(https://collegeportal.mungeruniversity.ac.in/images/bg.jpg); background-position:top center; background-size:100%; background-repeat:no-repeat; background-size: cover;'>";
                // without background Image
                // content += @" <div style='width:800px; padding:0 100px; margin:auto; '>";

                content += @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>
    	<img src='https://collegeportal.mungeruniversity.ac.in/images/logotree.png' alt='' width='150'>
    </td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:36px;'>Munger University, MUNGER</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:14px;'>(Administrative Block, Shastrinagar, Munger (BIHAR) - 811201</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:18px;'><strong>" + resultlist.Studentlist[i].CourseCategory.ToString() + "  " + resultlist.Studentlist[i].YearName.ToString() + @"  Examination –  " + resultlist.Studentlist[i].Examyear.ToString() + @"</strong></td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:22px;'>ADMIT CARD</td>
  </tr>   
    <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
      <tr>
        <td width='76%' align='left' valign='top'  style='padding:5px'>
                 <table width='100%'>
                                                        <tr>
                                                            <td  width='30%'>
                                                                Registration Number :
                                                            </td>
                                                            <td >
                                                                <span style='border:0; width:100px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].RegistrationNo.ToString() + @"</span>
                                                            </td>
                                                            <td>
                                                                Year :   
                                                            </td>
                                                            <td>
                                                                <span style='border:0; width:160px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].Registrationyear.ToString() + @"</span>
                                                            </td>
                                                        </tr>
                </table>
 </td>
       

        <td width='5%' rowspan='7'> &nbsp;</td>
        <td width='19%' rowspan='6' style='border:1px solid #333; padding:5px 5px 2px;'>
        	<img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].photo.ToString() + @""" width='180' height='180'>
        </td>
      </tr>
      <tr align='left' valign='top' >
        <td style='padding:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Name <BR>(In Block Letter):
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].StudentName.ToUpper().ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>


</td>
        </tr>
      <tr align='left' valign='top'>
        <td  style='padding:5px'>
 <table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                              " + (resultlist.Studentlist[i].ftitle.ToString() == "20" ? " Father's " : (resultlist.Studentlist[i].ftitle.ToString() == "21" ? "  Husband's  " : " Father's ")) + @" Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].FatherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

                                            


</td>
        </tr>
      <tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Mother&rsquo;s Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].MotherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

</td>
        </tr>
<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                               College Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].CollegeName.ToString() + @"</span>
                                            </td>
                                            

                                        </tr>
                                    </table>

</td>
        </tr>

<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                      
                                            <td  width='30%'>
                                               Centre of Examination:
                                            </td>
                                            <td style='border:0;text-align:left;font-weight:bold'>" +
                                            resultlist.Studentlist[i].HONSCENTER
                                            + @"</td>

                                        </tr>
                                    </table>

</td>
        </tr>

      <tr>
        <td align='left' valign='bottom'><strong>Subject of Examination:</strong></td>
        <td align='center' valign='middle' style='border:1px solid #333;'><img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].sign.ToString() + @"""  width='110'  height='50'></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>
 <div style='min-height:270px'>
<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #333;'>
      <tr>
        <td colspan='6' align='center' valign='middle' style='border-bottom:1px solid #333; padding:10px 0;'><strong>Subject Details</strong></td>
        </tr>";
                content += @" <tr>
        <td align='center' valign='middle' width='40' style='border-bottom:1px solid #333; border-right:1px solid #333;'>S. No</td>
        <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>Subject</td>
        <td align='center' valign='middle' width='70' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Paper</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Date</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Sitting</td>
       
      </tr>";


                int iii = 1;

                if (ExamType == "2")  // back exam
                {
                    if (resultlist.Studentlist[i].IsBackSubjectStr == null || resultlist.Studentlist[i].IsBackSubjectStr == "")
                    {
                        resultlist.Studentlist[i].IsBackSubjectStr = ",";
                    }
                    string[] values = resultlist.Studentlist[i].IsBackSubjectStr.Split(',');

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.sessionid == resultlist.Studentlist[i].sessionid && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours" && values.Contains(R.SubjectCodeID.ToString())).OrderBy(s => s.paper))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.setting + @" </td>
                       

                        </tr>";
                        iii = iii + 1;
                    }

                }
                else
                {

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.sessionid == resultlist.Studentlist[i].sessionid && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours").OrderBy(s => s.paper))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>";
                                                            
                        if (item.sessionid == 39 && item.courseyearid == 32 && item.SubjectCodeID == 5)
                        { 
                            // for 3 rd semeter 2018
                            content += @" <td colspan='2' align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > TO BE CONDUCTED ON CONCERNED INSTITUTIONS </td> ";


                        }
                        else
                        {
                            
                            content += @" <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.ExamStartDate + @" </td> 
                                          <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>";
                        }


                        content += @"</tr>";
                        iii = iii + 1;
                    }

                }


                content += @" 
      
";

                content += @" 
    </table>
</div>
</td>
  </tr>
 

 
  <tr>
    <td align='left' valign='middle'><strong>To be filled by office:</strong></td>
  </tr>
 
  <tr>
    <td align='left' valign='middle'>Exam Roll No <strong>" + resultlist.Studentlist[i].RollNo.ToString() + @"</strong> is allowed to appear at the Bachelor of Law Exam " + resultlist.Studentlist[i].Examyear.ToString() + @" Commencing from " + resultlist.Studentlist[i].ExamStartDate.ToString() + @" 
</td>
  </tr>
  
  <tr>
    <td align='left' valign='middle'>&nbsp;</td>
  </tr>
        <tr>
            <td align='left' valign='middle'>&nbsp;</td>
        </tr>
 
  <tr>
    <td align='left' valign='middle'>
    	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td align='left' valign='top' width='50% '></td>
            <td align='right' valign='top' width='50% '><img src=""https://collegeportal.mungeruniversity.ac.in/images/amar_examinationcontroller.png"" width='100'></td>
          </tr>
        <tr>
            <td align='left' valign='top' width='50% '>Principal / Prof-in-charge</td>
            <td align='right' valign='top' width='50% '>Controller of Examinations<br>Munger University, Munger</td>
        </tr>
        </table>
    </td>
  </tr>
  
    <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
     <tr>
      <td align='left' valign='middle' style='padding-bottom:10px;'><strong>GUIDELINES FOR THE EXAMINEE</strong></td>
    </tr>
    <tr>
      <td align='left' valign='middle'>
      	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td width='4%' align='left' valign='top'>1.</td>
            <td width='96%' align='left' valign='top'>The examinee is expected to be present at the examination centre 30 minutes before the commencement of examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>2.</td>
            <td align='left' valign='top'>No examinee shall be admitted to the examination hall after 30 minutes of commencement of the examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>3.</td>
            <td align='left' valign='top'>The examinee shall have the proper Admit Card and the valid college identity card with him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>4.</td>
            <td align='left' valign='top'>Examinees are not permitted to leave examination hall during the initial 1 hour and last 15 minutes of the examination. </td>
          </tr>
          <tr>
            <td align='left' valign='top'>5.</td>
            <td align='left' valign='top'>The examinee shall ensure occupying the seat allotted to him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>6.</td>
            <td align='left' valign='top'>The examinee is prohibited from keeping in his possession in the examination hall any blank paper, notes, scribbles, chits, books, mobile phone, pager, programmable calculator, electronic communication device etc. except the Admit Card and Blue/ Black Ball Point pen.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>7.</td>
            <td align='left' valign='top'>	The examinee shall cross (×) the blank page(s) of Answer Book left after finishing writing answers.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>8.</td>
            <td align='left' valign='top'>Any unfair means adopted by the examinee shall invite necessary punitive action.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>9.</td>
            <td align='left' valign='top'>Each examinee shall ensure the use of mask and sanitizer at the exam centre.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>10.</td>
            <td align='left' valign='top'>The examinee shall fill all necessary information’s on the front page of the answer book clearly. </td>
          </tr>
        </table>

      </td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>

  </table>
</div>
    </div>
                            ";
                builder.Append(content);
            }

            try
            {
                string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content=\"text/html;charset=utf-8\" /></head><body style='padding: 0; margin: 0; font-size:16px; '>" + builder.ToString() + "</body></html>";
                //Div Content will be fetched from form data.                                                    
                string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
                string dataname = "ExaminationAdmitCard";  // here we declare for filename for download
                if (string.IsNullOrEmpty(InpuData))
                    InpuData = "Some Error occured.Content not found.Please try again.";
                string appPath = HttpContext.Request.PhysicalApplicationPath;

                var htmlContent = InpuData.Replace("AppPath", appPath);
                var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

                if (string.IsNullOrEmpty(PageType))
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
                else
                {
                    if (PageType == "Landscape")
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                    else
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                }
                pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                pageMargins.Bottom = 0;
                pageMargins.Left = 0;
                pageMargins.Right = 0;
                pageMargins.Top = 0;
                pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                                                                   //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
                var pdfBytes = pdfDoc.GeneratePdf(htmlContent);

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
                HttpContext.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                HttpContext.ApplicationInstance.CompleteRequest();
            }

        }
        public void GenerateAdmitcard_Bed(PrintExamForm_admicard resultlist, string ExamType)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < resultlist.Studentlist.Count; i++)
            {
                string content = "";
                content += @"
                               <div class=""spacedesign"" style=""page-break-after:always"">";
                // with background Image
                content += @" <div style='width:800px; padding:0 100px; margin:auto; background-image:url(https://demo.com/mungerpic/bg.jpg); background-position:top center; background-size:100%; background-repeat:no-repeat; background-size: cover;'>";
                // without background Image
                // content += @" <div style='width:800px; padding:0 100px; margin:auto; '>";

                content += @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>
    	<img src='https://demo.com/mungerpic/logotree.png' alt='' width='150'>
    </td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:36px;'>Munger University, MUNGER</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:14px;'>(Administrative Block, Shastrinagar, Munger (BIHAR) - 811201</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:18px;'><strong>" + resultlist.Studentlist[i].CourseCategory.ToString() + "  " + resultlist.Studentlist[i].YearName.ToString() + @"  Examination –  " + resultlist.Studentlist[i].Examyear.ToString() + @"</strong></td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:22px;'>ADMIT CARD</td>
  </tr>   
    <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
      <tr>
        <td width='76%' align='left' valign='top'  style='padding:5px'>
                 <table width='100%'>
                                                        <tr>
                                                            <td  width='30%'>
                                                                Registration Number :
                                                            </td>
                                                            <td >
                                                                <span style='border:0; width:100px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].RegistrationNo.ToString() + @"</span>
                                                            </td>
                                                            <td>
                                                                Year :   
                                                            </td>
                                                            <td>
                                                                <span style='border:0; width:160px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].Registrationyear.ToString() + @"</span>
                                                            </td>
                                                        </tr>
                </table>
 </td>
       

        <td width='5%' rowspan='7'> &nbsp;</td>
        <td width='19%' rowspan='6' style='border:1px solid #333; padding:5px 5px 2px;'>
        	<img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].photo.ToString() + @""" width='180' height='180'>
        </td>
      </tr>
      <tr align='left' valign='top' >
        <td style='padding:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Name <BR>(In Block Letter):
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].StudentName.ToUpper().ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>


</td>
        </tr>
      <tr align='left' valign='top'>
        <td  style='padding:5px'>
 <table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                              " + (resultlist.Studentlist[i].ftitle.ToString() == "20" ? " Father's " : (resultlist.Studentlist[i].ftitle.ToString() == "21" ? "  Husband's  " : " Father's ")) + @" Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].FatherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

                                            


</td>
        </tr>
      <tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Mother&rsquo;s Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].MotherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

</td>
        </tr>
<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                               College Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].CollegeName.ToString() + @"</span>
                                            </td>
                                            

                                        </tr>
                                    </table>

</td>
        </tr>

<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                      
                                            <td  width='30%'>
                                               Centre of Examination:
                                            </td>
                                            <td style='border:0;text-align:left;font-weight:bold'>" +
                                            resultlist.Studentlist[i].HONSCENTER
                                            + @"</td>

                                        </tr>
                                    </table>

</td>
        </tr>

      <tr>
        <td align='left' valign='bottom'><strong>Subject of Examination:</strong></td>
        <td align='center' valign='middle' style='border:1px solid #333;'><img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].sign.ToString() + @"""  width='110'  height='50'></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>
 <div style='min-height:270px'>
<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #333;'>
      <tr>
        <td colspan='6' align='center' valign='middle' style='border-bottom:1px solid #333; padding:10px 0;'><strong>Subject Details</strong></td>
        </tr>";
                content += @" <tr>
        <td align='center' valign='middle' width='40' style='border-bottom:1px solid #333; border-right:1px solid #333;'>S. No</td>
        <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>Subject</td>
        <td align='center' valign='middle' width='70' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Paper</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Date</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Sitting</td>
       
      </tr>";


                int iii = 1;

                if (ExamType == "2")  // back exam
                {
                    if (resultlist.Studentlist[i].IsBackSubjectStr == null || resultlist.Studentlist[i].IsBackSubjectStr == "")
                    {
                        resultlist.Studentlist[i].IsBackSubjectStr = ",";
                    }
                    string[] values = resultlist.Studentlist[i].IsBackSubjectStr.Split(',');

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid  && R.Type == "Honours" && values.Contains(R.SubjectCodeID.ToString())).OrderBy(s => s.paper))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.setting + @" </td>
                       

                        </tr>";
                        iii = iii + 1;
                    }

                }
                else
                {

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.subjecttype == 1 && R.Type == "Honours").OrderBy(s => s.SubjectCodeID))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.subjectcode + " " + item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>";



                        content += @" <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.ExamStartDate + @" </td> 
                                          <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>";


                        content += @"</tr>";
                        iii = iii + 1;
                    }
                    if (resultlist.Studentlist[i].courseyearid == 29)// part -2
                    {
                        foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.subjecttype == 2 && R.Substreamcategoryid == Convert.ToInt32(resultlist.Studentlist[i].electivesubjectid) && R.Type == "Honours").OrderBy(s => s.paper))
                        {
                            // c-11
                            content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "C-11" + " " + item.SubjectName + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>";
                            content += @" <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.ExamStartDate + @" </td> 
                                          <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>";
                            content += @"</tr>";
                            iii = iii + 1;
                        }
                        foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.subjecttype == 2 && R.Substreamcategoryid == resultlist.Studentlist[i].electivesubjectid_2 && R.Type == "Honours").OrderBy(s => s.paper))
                        {
                            // c-7(b)
                            content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "C-7(b)" + " " + item.SubjectName + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>";
                            content += @" <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.ExamStartDate + @" </td> 
                                          <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>";
                            content += @"</tr>";
                            iii = iii + 1;
                        }
                        // part -2 EPC-4
                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "EPC-4" + " " + "Understaing the Self" + @" </td>
                                                            <td align='center' colspan='3' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "TO BE CONDUCTED ON CONCERNED COLLEGE" + @" </td>";

                        content += @"</tr>";
                        iii = iii + 1;
                    }
                    if (resultlist.Studentlist[i].courseyearid == 28)// part -1
                    {
                        foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.subjecttype == 2 && R.Substreamcategoryid == resultlist.Studentlist[i].electivesubjectid && R.Type == "Honours").OrderBy(s => s.paper))
                        {
                            // c-7(a)
                            content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "C-7(a)" + " " + item.SubjectName + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>";
                            content += @" <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.ExamStartDate + @" </td> 
                                          <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>";
                            content += @"</tr>";
                            iii = iii + 1;
                        }
                        // part -1 EPC-1
                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "EPC-I" + " " + " Reading And Reflection on Texts" + @" </td>
                                                            <td align='center' colspan='3' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "TO BE CONDUCTED ON CONCERNED COLLEGE" + @" </td>";

                        content += @"</tr>";
                        iii = iii + 1;
                        // part -1 EPC-2
                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "EPC-II" + " " + "Drama and Art in Education" + @" </td>
                                                            <td align='center' colspan='3' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "TO BE CONDUCTED ON CONCERNED COLLEGE" + @" </td>";

                        content += @"</tr>";
                        iii = iii + 1;
                        // part -1 EPC-3
                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "EPC-III" + " " + "	Critical Understanding of ICT" + @" </td>
                                                            <td align='center' colspan='3' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "TO BE CONDUCTED ON CONCERNED COLLEGE" + @" </td>";

                        content += @"</tr>";
                        iii = iii + 1;
                    }

                }
            
                content += @" 
      
";

                content += @" 
    </table>
</div>
</td>
  </tr>

"; if (resultlist.Studentlist[i].courseyearid == 28)// part -1
      {
                    content += @" <tr>


<tr> 
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr> 
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>


";
                }
   content += @" 
 
  <tr>
    <td align='left' valign='middle'><strong>To be filled by office:</strong></td>
  </tr>
 
  <tr>
    <td align='left' valign='middle'>Exam Roll No <strong>" + resultlist.Studentlist[i].RollNo.ToString() + @"</strong> is allowed to appear at the Bachelor of Education Exam " + resultlist.Studentlist[i].Examyear.ToString() + @" Commencing from " + resultlist.Studentlist[i].ExamStartDate.ToString() + @" 
</td>
  </tr>
  
  
        <tr>
            <td align='left' valign='middle'>&nbsp;</td>
        </tr>
 
  <tr>
    <td align='left' valign='middle'>
    	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td align='left' valign='top' width='50% '></td>
            <td align='right' valign='top' width='50% '><img src=""https://collegeportal.mungeruniversity.ac.in/images/amar_examinationcontroller.png"" width='100'></td>
          </tr>
        <tr>
            <td align='left' valign='top' width='50% '>Principal / Prof-in-charge</td>
            <td align='right' valign='top' width='50% '>Controller of Examinations<br>Munger University, Munger</td>
        </tr>
        </table>
    </td>
  </tr>
  
  
    <tr>
      <td align='left' valign='middle'>";
                if (resultlist.Studentlist[i].courseyearid == 28)// part -1
                {
                    content += @" 

<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>


                 ";

                    if (ExamType == "2")  // back exam
                    {
                        content += @" 

<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr><tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr><tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
                 ";
                    }
                }
                else // part 2
                {
                    content += @" 

     <tr>
      <td align='left' valign='middle' style='padding-bottom:10px;'><strong>GUIDELINES FOR THE EXAMINEE</strong></td>
    </tr>         
<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td width='4%' align='left' valign='top'>1.</td>
            <td width='96%' align='left' valign='top'>The examinee is expected to be present at the examination centre 30 minutes before the commencement of examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>2.</td>
            <td align='left' valign='top'>No examinee shall be admitted to the examination hall after 30 minutes of commencement of the examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>3.</td>
            <td align='left' valign='top'>The examinee shall have the proper Admit Card and the valid college identity card with him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>4.</td>
            <td align='left' valign='top'>Examinees are not permitted to leave examination hall during the initial 1 hour and last 15 minutes of the examination. </td>
          </tr>
          <tr>
            <td align='left' valign='top'>5.</td>
            <td align='left' valign='top'>The examinee shall ensure occupying the seat allotted to him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>6.</td>
            <td align='left' valign='top'>The examinee is prohibited from keeping in his possession in the examination hall any blank paper, notes, scribbles, chits, books, mobile phone, pager, programmable calculator, electronic communication device etc. except the Admit Card and Blue/ Black Ball Point pen.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>7.</td>
            <td align='left' valign='top'>	The examinee shall cross (×) the blank page(s) of Answer Book left after finishing writing answers.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>8.</td>
            <td align='left' valign='top'>Any unfair means adopted by the examinee shall invite necessary punitive action.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>9.</td>
            <td align='left' valign='top'>Each examinee shall ensure the use of mask and sanitizer at the exam centre.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>10.</td>
            <td align='left' valign='top'>The examinee shall fill all necessary information’s on the front page of the answer book clearly. </td>
          </tr>
        </table>
";
                }

         
    content += @"
      </td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    

  </table>
</div>
    </div>
                            ";
                builder.Append(content);
            }
            if (resultlist.Studentlist[0].courseyearid == 28)// part -1 
            { 
                for (int i = 0; i < resultlist.Studentlist.Count; i++)
            {
                string content = "";
                content += @"
                               <div class=""spacedesign"" style=""page-break-after:always"">";
                // with background Image
                content += @" <div style='width:800px; padding:0 100px; margin:auto; background-image:url(https://demo.com/mungerpic/bg.jpg); background-position:top center; background-size:100%; background-repeat:no-repeat; background-size: cover;'>";
                // without background Image
                // content += @" <div style='width:800px; padding:0 100px; margin:auto; '>";

                content += @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>
    	<img src='https://demo.com/mungerpic/logotree.png' alt='' width='150'>
    </td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:36px;'>Munger University, MUNGER</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:14px;'>(Administrative Block, Shastrinagar, Munger (BIHAR) - 811201</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:18px;'><strong>" + resultlist.Studentlist[i].CourseCategory.ToString() + "  " + resultlist.Studentlist[i].YearName.ToString() + @"  Examination –  " + resultlist.Studentlist[i].Examyear.ToString() + @"</strong></td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
 
               
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
     <tr>
      <td align='left' valign='middle' style='padding-bottom:10px;'><strong>GUIDELINES FOR THE EXAMINEE</strong></td>
    </tr>         

               
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
               
  <tr>
        <td align='left' valign='middle'>&nbsp;
<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td width='4%' align='left' valign='top'>1.</td>
            <td width='96%' align='left' valign='top'>The examinee is expected to be present at the examination centre 30 minutes before the commencement of examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>2.</td>
            <td align='left' valign='top'>No examinee shall be admitted to the examination hall after 30 minutes of commencement of the examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>3.</td>
            <td align='left' valign='top'>The examinee shall have the proper Admit Card and the valid college identity card with him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>4.</td>
            <td align='left' valign='top'>Examinees are not permitted to leave examination hall during the initial 1 hour and last 15 minutes of the examination. </td>
          </tr>
          <tr>
            <td align='left' valign='top'>5.</td>
            <td align='left' valign='top'>The examinee shall ensure occupying the seat allotted to him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>6.</td>
            <td align='left' valign='top'>The examinee is prohibited from keeping in his possession in the examination hall any blank paper, notes, scribbles, chits, books, mobile phone, pager, programmable calculator, electronic communication device etc. except the Admit Card and Blue/ Black Ball Point pen.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>7.</td>
            <td align='left' valign='top'>	The examinee shall cross (×) the blank page(s) of Answer Book left after finishing writing answers.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>8.</td>
            <td align='left' valign='top'>Any unfair means adopted by the examinee shall invite necessary punitive action.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>9.</td>
            <td align='left' valign='top'>Each examinee shall ensure the use of mask and sanitizer at the exam centre.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>10.</td>
            <td align='left' valign='top'>The examinee shall fill all necessary information’s on the front page of the answer book clearly. </td>
          </tr>
        </table>
</td>
    </tr>
               
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
               
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
               
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
               
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
  <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr> 
<tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr> 



  </table>
</div>
    </div>
";
                builder.Append(content);
            }
            }


            try
            {
                string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content=\"text/html;charset=utf-8\" /></head><body style='padding: 0; margin: 0; font-size:16px; '>" + builder.ToString() + "</body></html>";
                //Div Content will be fetched from form data.                                                    
                string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
                string dataname = "ExaminationAdmitCard";  // here we declare for filename for download
                if (string.IsNullOrEmpty(InpuData))
                    InpuData = "Some Error occured.Content not found.Please try again.";
                string appPath = HttpContext.Request.PhysicalApplicationPath;

                var htmlContent = InpuData.Replace("AppPath", appPath);
                var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

                if (string.IsNullOrEmpty(PageType))
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
                else
                {
                    if (PageType == "Landscape")
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                    else
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                }
                pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                pageMargins.Bottom = 0;
                pageMargins.Left = 0;
                pageMargins.Right = 0;
                pageMargins.Top = 0;
                pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                                                                   //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
                var pdfBytes = pdfDoc.GeneratePdf(htmlContent);

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
                HttpContext.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                HttpContext.ApplicationInstance.CompleteRequest();
            }

        }
        public void GenerateAdmitcard_PG(PrintExamForm_admicard resultlist, string ExamType)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < resultlist.Studentlist.Count; i++)
            {
                string content = "";
                content += @"
                               <div class=""spacedesign"" style=""page-break-after:always"">";
                // with background Image
                content += @" <div style='width:800px; padding:0 100px; margin:auto; background-image:url(https://demo.com/mungerpic/bg.jpg); background-position:top center; background-size:100%; background-repeat:no-repeat;'>";
                // without background Image
                // content += @" <div style='width:800px; padding:0 100px; margin:auto; '>";

                content += @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>
    	<img src='https://demo.com/mungerpic/logotree.png' alt='' width='150'>
    </td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:36px;'>Munger University, MUNGER</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:14px;'>(Administrative Block, Shastrinagar, Munger (BIHAR) - 811201</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:18px;'><strong>" + resultlist.Studentlist[i].CourseCategory.ToString() + "  " + resultlist.Studentlist[i].YearName.ToString() + @"  Examination –  " + resultlist.Studentlist[i].Examyear.ToString()+" (Session : " + resultlist.Studentlist[i].SessionName + ")"+ @"</strong></td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:22px;'>ADMIT CARD</td>
  </tr>   
    <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
      <tr>
        <td width='76%' align='left' valign='top'  style='padding:5px'>
                 <table width='100%'>
                                                        <tr>
                                                            <td  width='30%'>
                                                                Registration Number :
                                                            </td>
                                                            <td >
                                                                <span style='border:0; width:100px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].RegistrationNo.ToString() + @"</span>
                                                            </td>
                                                            <td>
                                                                Year :   
                                                            </td>
                                                            <td>
                                                                <span style='border:0; width:160px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].Registrationyear.ToString() + @"</span>
                                                            </td>
                                                        </tr>
                </table>
 </td>
       

        <td width='5%' rowspan='7'> &nbsp;</td>
        <td width='19%' rowspan='6' style='border:1px solid #333; padding:5px 5px 2px;'>
        	<img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].photo.ToString() + @""" width='180' height='180'>
        </td>
      </tr>
      <tr align='left' valign='top' >
        <td style='padding:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Name <BR>(In Block Letter):
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].StudentName.ToUpper().ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>


</td>
        </tr>
      <tr align='left' valign='top'>
        <td  style='padding:5px'>
 <table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                              " + (resultlist.Studentlist[i].ftitle.ToString() == "20" ? " Father's " : (resultlist.Studentlist[i].ftitle.ToString() == "21" ? "  Husband's  " : " Father's ")) + @" Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].FatherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

                                            


</td>
        </tr>
      <tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Mother&rsquo;s Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].MotherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

</td>
        </tr>
<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                               College Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].CollegeName.ToString() + @"</span>
                                            </td>
                                            

                                        </tr>
                                    </table>

</td>
        </tr>

<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                      
                                            <td  width='30%'>
                                               Centre of Examination:
                                            </td>
                                            <td style='border:0;text-align:left;font-weight:bold'>" +
                                            resultlist.Studentlist[i].HONSCENTER
                                            + @"</td>

                                        </tr>
                                    </table>

</td>
        </tr>

      <tr>
        <td align='left' valign='bottom'><strong>Subject of Examination :  " + resultlist.Studentlist[i].HonoursSubject+ @"</strong></td>
        <td align='center' valign='middle' style='border:1px solid #333;'><img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].sign.ToString() + @"""  width='110'  height='50'></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>
 <div style='min-height:270px'>
<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #333;'>
      <tr>
        <td colspan='6' align='center' valign='middle' style='border-bottom:1px solid #333; padding:10px 0;'><strong>Subject Details</strong></td>
        </tr>";
                content += @" <tr>
        <td align='center' valign='middle' width='40' style='border-bottom:1px solid #333; border-right:1px solid #333;'>S. No</td>
        <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>Subject</td>
        <td align='center' valign='middle' width='70' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Paper</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Date</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Sitting</td>
       
      </tr>";


                int iii = 1;

                if (ExamType == "2")  // back exam
                {
                    
                    if (resultlist.Studentlist[i].IsBackSubjectStr == null || resultlist.Studentlist[i].IsBackSubjectStr == "")
                    {
                        resultlist.Studentlist[i].IsBackSubjectStr = ",";
                    }
                    string[] values = resultlist.Studentlist[i].IsBackSubjectStr.Split(',');

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.StreamCategoryID == resultlist.Studentlist[i].StreamCategoryID  && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours" && values.Contains(R.SubjectCodeID.ToString())).OrderBy(s => s.SubjectCodeID))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            
                                                        </tr>";
                        iii = iii + 1;
                    }

                }
                else
                {

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.StreamCategoryID== resultlist.Studentlist[i].StreamCategoryID && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid  && R.subjecttype == 1 && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours").OrderBy(s => s.SubjectCodeID))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            
                                                        </tr>";
                        iii = iii + 1;
                    }
                    // optional subject
                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.StreamCategoryID == resultlist.Studentlist[i].StreamCategoryID && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.subjecttype == 2 && R.Substreamcategoryid == resultlist.Studentlist[i].electivesubjectid && R.Type == "Honours").OrderBy(s => s.SubjectCodeID))
                        {
                          
                            content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + "" + " " + item.SubjectName + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>";
                            content += @" <td align = 'center' valign = 'middle' style = 'border-bottom:1px solid #333; border-right:1px solid #333;' > " + item.ExamStartDate + @" </td> 
                                          <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>";
                            content += @"</tr>";
                            iii = iii + 1;
                        }

                }











                content += @" 
      
";

                content += @" 
    </table>
</div>
</td>
  </tr>
 

 
  <tr>
    <td align='left' valign='middle'><strong>To be filled by office:</strong></td>
  </tr>
 
  <tr>
    <td align='left' valign='middle'>Exam Roll No <strong>" + resultlist.Studentlist[i].RollNo.ToString() + @"</strong> is allowed to appear at the PG Exam " + resultlist.Studentlist[i].Examyear.ToString() + @" Commencing from <strong> " + resultlist.Studentlist[i].ExamStartDate.ToString() + @" </strong>
</td>
  </tr>
 
 
  <tr>
    <td align='left' valign='middle'>
    	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td align='left' valign='top' width='50% '> </td>
            <td align='right' valign='top' width='50% '><img src=""https://collegeportal.mungeruniversity.ac.in/images/amar_examinationcontroller.png"" width='100'></td>
          </tr>
        <tr>
            <td align='left' valign='top' width='50% '>Principal</td>
            <td align='right' valign='top' width='50% '>Controller of Examinations<br>Munger University, Munger</td>
        </tr>
        </table>
    </td>
  </tr>
  
    <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
     <tr>
      <td align='left' valign='middle' style='padding-bottom:10px;'><strong>GUIDELINES FOR THE EXAMINEE</strong></td>
    </tr>
    <tr>
      <td align='left' valign='middle'>
      	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td width='4%' align='left' valign='top'>1.</td>
            <td width='96%' align='left' valign='top'>The examinee is expected to be present at the examination centre 30 minutes before the commencement of examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>2.</td>
            <td align='left' valign='top'>No examinee shall be admitted to the examination hall after 30 minutes of commencement of the examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>3.</td>
            <td align='left' valign='top'>The examinee shall have the proper Admit Card and the valid college identity card with him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>4.</td>
            <td align='left' valign='top'>Examinees are not permitted to leave examination hall during the initial 1 hour and last 15 minutes of the examination. </td>
          </tr>
          <tr>
            <td align='left' valign='top'>5.</td>
            <td align='left' valign='top'>The examinee shall ensure occupying the seat allotted to him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>6.</td>
            <td align='left' valign='top'>The examinee is prohibited from keeping in his possession in the examination hall any blank paper, notes, scribbles, chits, books, mobile phone, pager, programmable calculator, electronic communication device etc. except the Admit Card and Blue/ Black Ball Point pen.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>7.</td>
            <td align='left' valign='top'>	The examinee shall cross (×) the blank page(s) of Answer Book left after finishing writing answers.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>8.</td>
            <td align='left' valign='top'>Any unfair means adopted by the examinee shall invite necessary punitive action.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>9.</td>
            <td align='left' valign='top'>Each examinee shall ensure the use of mask and sanitizer at the exam centre.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>10.</td>
            <td align='left' valign='top'>The examinee shall fill all necessary information’s on the front page of the answer book clearly. </td>
          </tr>
        </table>

      </td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>

  </table>
</div>
    </div>
                            ";
                builder.Append(content);
            }

            try
            {
                string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content=\"text/html;charset=utf-8\" /></head><body style='padding: 0; margin: 0; font-size:16px; '>" + builder.ToString() + "</body></html>";
                //Div Content will be fetched from form data.                                                    
                string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
                string dataname = "ExaminationAdmitCard";  // here we declare for filename for download
                if (string.IsNullOrEmpty(InpuData))
                    InpuData = "Some Error occured.Content not found.Please try again.";
                string appPath = HttpContext.Request.PhysicalApplicationPath;

                var htmlContent = InpuData.Replace("AppPath", appPath);
                var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

                if (string.IsNullOrEmpty(PageType))
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
                else
                {
                    if (PageType == "Landscape")
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                    else
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                }
                pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                pageMargins.Bottom = 0;
                pageMargins.Left = 0;
                pageMargins.Right = 0;
                pageMargins.Top = 0;
                pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                                                                   //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
                var pdfBytes = pdfDoc.GeneratePdf(htmlContent);

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
                HttpContext.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                HttpContext.ApplicationInstance.CompleteRequest();
            }

        }
        public void GenerateAdmitcard_Other(PrintExamForm_admicard resultlist,string ExamType)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < resultlist.Studentlist.Count; i++)
            {
                string content = "";
                content += @"
                               <div class=""spacedesign"" style=""page-break-after:always"">";
                // with background Image
                content += @" <div style='width:800px; padding:0 100px; margin:auto; background-image:url(https://collegeportal.mungeruniversity.ac.in/images/bg.jpg); background-position:top center; background-size:100%; background-repeat:no-repeat;'>";
                // without background Image
                // content += @" <div style='width:800px; padding:0 100px; margin:auto; '>";

                content += @"<table width='100%' border='0' cellspacing='0' cellpadding='0'>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>
    	<img src='https://collegeportal.mungeruniversity.ac.in/images/logotree.png' alt='' width='150'>
    </td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:36px;'>Munger University, MUNGER</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:14px;'>(Administrative Block, Shastrinagar, Munger (BIHAR) - 811201</td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:18px;'><strong>" + resultlist.Studentlist[i].CourseCategory.ToString() + "  " + resultlist.Studentlist[i].YearName.ToString() + @"  Examination –  " + resultlist.Studentlist[i].Examyear.ToString() + @"</strong></td>
  </tr>
  <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td align='center' valign='middle' style='font-size:22px;'>ADMIT CARD</td>
  </tr>   
    <tr>
    <td align='center' valign='middle'>&nbsp;</td>
  </tr>
  <tr>
    <td><table width='100%' border='0' cellspacing='0' cellpadding='0'>
      <tr>
        <td width='76%' align='left' valign='top'  style='padding:5px'>
                 <table width='100%'>
                                                        <tr>
                                                            <td  width='30%'>
                                                                Registration Number :
                                                            </td>
                                                            <td >
                                                                <span style='border:0; width:100px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].RegistrationNo.ToString() + @"</span>
                                                            </td>
                                                            <td>
                                                                Year :   
                                                            </td>
                                                            <td>
                                                                <span style='border:0; width:160px; text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].Registrationyear.ToString() + @"</span>
                                                            </td>
                                                        </tr>
                </table>
 </td>
       

        <td width='5%' rowspan='7'> &nbsp;</td>
        <td width='19%' rowspan='6' style='border:1px solid #333; padding:5px 5px 2px;'>
        	<img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].photo.ToString() + @""" width='180' height='180'>
        </td>
      </tr>
      <tr align='left' valign='top' >
        <td style='padding:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Name <BR>(In Block Letter):
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].StudentName.ToUpper().ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>


</td>
        </tr>
      <tr align='left' valign='top'>
        <td  style='padding:5px'>
 <table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                              " + (resultlist.Studentlist[i].ftitle.ToString() == "20" ? " Father's " : (resultlist.Studentlist[i].ftitle.ToString() == "21" ? "  Husband's  " : " Father's ")) + @" Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].FatherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

                                            


</td>
        </tr>
      <tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                                Mother&rsquo;s Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].MotherName.ToString() + @"</span>
                                            </td>
                                        </tr>
                                    </table>

</td>
        </tr>
<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                        <tr>
                                            <td  width='30%'>
                                               College Name :
                                            </td>
                                          
                                            <td>
                                                <span style='border:0;text-align:left;font-weight:bold'>" + resultlist.Studentlist[i].CollegeName.ToString() + @"</span>
                                            </td>
                                            

                                        </tr>
                                    </table>

</td>
        </tr>

<tr align='left' valign='top' >
        <td style='padding-top:5px;padding-left:5px'>
<table width='100%'>
                                      
                                            <td  width='30%'>
                                               Centre of Examination:
                                            </td>
                                            <td style='border:0;text-align:left;font-weight:bold'>" +
                                            resultlist.Studentlist[i].HONSCENTER
                                            + @"</td>

                                        </tr>
                                    </table>

</td>
        </tr>

      <tr>
        <td align='left' valign='bottom'><strong>Subject of Examination:</strong></td>
        <td align='center' valign='middle' style='border:1px solid #333;'><img src=""" + System.Configuration.ConfigurationManager.AppSettings["AmozonbucketPath"] + "/Student/Photoandsign/" + resultlist.Studentlist[i].sign.ToString() + @"""  width='110'  height='50'></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>
 <div style='min-height:270px'>
<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border:1px solid #333;'>
      <tr>
        <td colspan='6' align='center' valign='middle' style='border-bottom:1px solid #333; padding:10px 0;'><strong>Subject Details</strong></td>
        </tr>";
                content += @" <tr>
        <td align='center' valign='middle' width='40' style='border-bottom:1px solid #333; border-right:1px solid #333;'>S. No</td>
        <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>Subject</td>
        <td align='center' valign='middle' width='70' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Paper</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Date</td>
        <td align='center' valign='middle' width='90' style='border-bottom:1px solid #333; border-right:1px solid #333;'>Sitting</td>
       
      </tr>";


                int iii = 1;
               
                if (ExamType == "2")  // back exam
                {
                    if (resultlist.Studentlist[i].IsBackSubjectStr == null || resultlist.Studentlist[i].IsBackSubjectStr == "")
                    {
                        resultlist.Studentlist[i].IsBackSubjectStr = ",";
                    }
                    string[] values = resultlist.Studentlist[i].IsBackSubjectStr.Split(',');

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType && R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours" && values.Contains(R.SubjectCodeID.ToString())).OrderBy(s => s.paper))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.subjectcode+" - " +item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            
                                                        </tr>";
                        iii = iii + 1;
                    }

                }
                else
                {

                    foreach (var item in resultlist.subjectlist.Where(R => R.EducationType == resultlist.Studentlist[i].EducationType &&  R.coursecategoryid == resultlist.Studentlist[i].coursecategoryid && R.courseyearid == resultlist.Studentlist[i].courseyearid && R.Type == "Honours").OrderBy(s => s.paper))
                    {


                        content += @"
                                                           <tr>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333; padding:10px 0;'>" + iii + @"</td>
                                                            <td align='center' valign='middle' width='200'  style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.subjectcode +" - " +item.SubjectName.ToString() + @" </td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.paper + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;'>" + item.ExamStartDate + @"</td>
                                                            <td align='center' valign='middle' style='border-bottom:1px solid #333; border-right:1px solid #333;' >" + item.setting + @"</td>
                                                            
                                                        </tr>";
                        iii = iii + 1;
                    }

                }

             

              
             




               

                content += @" 
      
";

                content += @" 
    </table>
</div>
</td>
  </tr>
 

 
  <tr>
    <td align='left' valign='middle'><strong>To be filled by office:</strong></td>
  </tr>
 
  <tr>
    <td align='left' valign='middle'>Exam Roll No <strong>" + resultlist.Studentlist[i].RollNo.ToString() + @"</strong> is allowed to appear at the " + resultlist.Studentlist[i].CourseCategory.ToString()+ " " + resultlist.Studentlist[i].YearName.ToString()  + @" Exam " + resultlist.Studentlist[i].Examyear.ToString() + @" Commencing from <strong>" + resultlist.Studentlist[i].ExamStartDate.ToString() + @"</strong> 
</td>
  </tr>
  
  <tr>
    <td align='left' valign='middle'>&nbsp;</td>
  </tr>
        <tr>
            <td align='left' valign='middle'>&nbsp;</td>
        </tr>
 
  <tr>
    <td align='left' valign='middle'>
    	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td align='left' valign='top' width='50% '> </td>
            <td align='right' valign='top' width='50% '><img src=""https://collegeportal.mungeruniversity.ac.in/images/amar_examinationcontroller.png"" width='100'></td>
          </tr>
        <tr>
            <td align='left' valign='top' width='50% '>Principal</td>
            <td align='right' valign='top' width='50% '>Controller of Examinations<br>Munger University, Munger</td>
        </tr>
        </table>
    </td>
  </tr>
  
    <tr>
        <td align='left' valign='middle'>&nbsp;</td>
    </tr>
     <tr>
      <td align='left' valign='middle' style='padding-bottom:10px;'><strong>GUIDELINES FOR THE EXAMINEE</strong></td>
    </tr>
    <tr>
      <td align='left' valign='middle'>
      	<table width='100%' border='0' cellspacing='0' cellpadding='0'>
          <tr>
            <td width='4%' align='left' valign='top'>1.</td>
            <td width='96%' align='left' valign='top'>The examinee is expected to be present at the examination centre 30 minutes before the commencement of examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>2.</td>
            <td align='left' valign='top'>No examinee shall be admitted to the examination hall after 30 minutes of commencement of the examination.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>3.</td>
            <td align='left' valign='top'>The examinee shall have the proper Admit Card and the valid college identity card with him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>4.</td>
            <td align='left' valign='top'>Examinees are not permitted to leave examination hall during the initial 1 hour and last 15 minutes of the examination. </td>
          </tr>
          <tr>
            <td align='left' valign='top'>5.</td>
            <td align='left' valign='top'>The examinee shall ensure occupying the seat allotted to him/her.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>6.</td>
            <td align='left' valign='top'>The examinee is prohibited from keeping in his possession in the examination hall any blank paper, notes, scribbles, chits, books, mobile phone, pager, programmable calculator, electronic communication device etc. except the Admit Card and Blue/ Black Ball Point pen.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>7.</td>
            <td align='left' valign='top'>	The examinee shall cross (×) the blank page(s) of Answer Book left after finishing writing answers.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>8.</td>
            <td align='left' valign='top'>Any unfair means adopted by the examinee shall invite necessary punitive action.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>9.</td>
            <td align='left' valign='top'>Each examinee shall ensure the use of mask and sanitizer at the exam centre.</td>
          </tr>
          <tr>
            <td align='left' valign='top'>10.</td>
            <td align='left' valign='top'>The examinee shall fill all necessary information’s on the front page of the answer book clearly. </td>
          </tr>
        </table>

      </td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>
    <tr>
      <td align='left' valign='middle'>&nbsp;</td>
    </tr>

  </table>
</div>
    </div>
                            ";
                builder.Append(content);
            }

            try
            {
                string InpuData = "<html><head><title></title><meta http-equiv='Content-Type' content=\"text/html;charset=utf-8\" /></head><body style='padding: 0; margin: 0; font-size:16px; '>" + builder.ToString() + "</body></html>";
                //Div Content will be fetched from form data.                                                    
                string PageType = "Portrait";  //here we will recieve Page Type sent from front end.
                string dataname = "ExaminationAdmitCard";  // here we declare for filename for download
                if (string.IsNullOrEmpty(InpuData))
                    InpuData = "Some Error occured.Content not found.Please try again.";
                string appPath = HttpContext.Request.PhysicalApplicationPath;

                var htmlContent = InpuData.Replace("AppPath", appPath);
                var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.

                if (string.IsNullOrEmpty(PageType))
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Default;  //setting orientation.
                else
                {
                    if (PageType == "Landscape")
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                    else
                        pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                }
                pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                pageMargins.Bottom = 0;
                pageMargins.Left = 0;
                pageMargins.Right = 0;
                pageMargins.Top = 0;
                pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                                                                   //Why I am adding this to page footer?? So we can get paging in footer section of each PDF page.how its working?? A Javascript code is written inside the DLL which is handling  Div's class i.e. page and topage.
                var pdfBytes = pdfDoc.GeneratePdf(htmlContent);

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + dataname.Replace(" ", "") + System.DateTime.Now.ToString() + ".pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
                Response.Flush();
                Response.End();
                HttpContext.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                HttpContext.ApplicationInstance.CompleteRequest();
            }

        }

        public ActionResult CounsellingSheetReporCollegeWiset3()
        {
            Commn_master com = new Commn_master();
            AcademicSession session = new AcademicSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            //BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollegeData(2);
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            return View();
        }
        [HttpPost]
        public JsonResult CounsellingSheetReporCollegeWiset3(string EducationType = "", string ddlsession = "", string Coursetype = "")
        {
            string CollegeID = "";
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = EncriptDecript.Decrypt(encollegeID);
            }
            BL_CounsellingSheetReport PritApp = new BL_CounsellingSheetReport();
            var obj = PritApp.GetCounsellingSheetReportCollegeWise3(EducationType, ddlsession, Coursetype, CollegeID);
            //return View(obj);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SpotSheetList()
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            //  CollegeExamCenter model = new CollegeExamCenter();
            Commn_master com = new Commn_master();
            ViewBag.Educationtype = com.getcommonMaster("EducationType");
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollegeData(2);
            ViewBag.CourseCategoryIDs = "";
            return View();
        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult GetRemainingSheet(int streamCatego = 0)
        {
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            int cid = (collegeID != null ? Convert.ToInt32(collegeID) : 0);
            BL_CollegeMaster com = new BL_CollegeMaster();
            var obj = com.GetRemainingSheet(cid, streamCatego);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentAchievement(string id = "", string esid = "")
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            AcademicSession session = new AcademicSession();
            ViewBag.Session = "";
            ViewBag.Display = "0";
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();

            Student_AchievementManagement ob = new Student_AchievementManagement();
            ViewBag.studentList = "";
            ViewBag.AchievementList = "";
            Commn_master com = new Commn_master();
            ViewBag.Course = com.getcommonMaster("coursesfordrop");
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var result = ob.findStudentDetail(eID);
                    if (result != null)
                    {
                        ViewBag.studentList = ob.StudentListBYID(result.SID);
                        ViewBag.SessionID = result.SessionID;
                        ViewBag.Session = session.GetSessionList(result.Course);
                        ViewBag.AchievementList = ob.AchievementListForEdit(result.SID);
                        ViewBag.CourseID = result.Course;
                        ViewBag.Display = "2";

                    }
                    return View(result);
                }
                if (esid != "0" && esid.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(esid);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var result = ob.findStudentDetail(eID);
                    if (result != null)
                    {
                        ViewBag.studentname = result.Name;
                        ViewBag.studentList = ob.StudentListBYID(result.SID);
                        ViewBag.SessionID = result.SessionID;
                        ViewBag.Session = session.GetSessionList(result.Course);
                        ViewBag.AchievementList = ob.AchievementList(result.SID);
                        ViewBag.CourseID = result.Course;
                        ViewBag.Display = "1";
                        result.Description = "";
                        result.DocumentURl = "";
                        result.hid = 0;


                    }
                    return View(result);
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student : Achievement Document Upload Get Method ID=", enID);
            }
            return View(ob);
        }
        public JsonResult AddStudentAchievement(string id = "")
        {
            Student_AchievementManagement ob = new Student_AchievementManagement();
            if (Request.Form.Count > 0)
            {
                Student_AchievementManagement doc = new Student_AchievementManagement();
                doc.SID = Convert.ToInt32(Request.Form["SID"] == "" ? "0" : Request.Form["SID"]);
                doc.SessionID = Convert.ToInt32(Request.Form["SessionID"] == "" ? "0" : Request.Form["SessionID"]);
                doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.AchievementMasterID = Convert.ToInt32(Request.Form["AchievementMasterID"] == "" ? "0" : Request.Form["AchievementMasterID"]);
                doc.Description = Request.Form["Description"];
                doc.hfile = Request.Form["hfile"];
                if (doc.SessionID == 0)
                {
                    Student_AchievementManagement doc1 = new Student_AchievementManagement();
                    doc1.Msg = "Please Select Session !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.SID == 0)
                {
                    Student_AchievementManagement doc1 = new Student_AchievementManagement();
                    doc1.Msg = "Please Select Student !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.SID == 0)
                {
                    Student_AchievementManagement doc1 = new Student_AchievementManagement();
                    doc1.Msg = "Please Select Student !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.AchievementMasterID == 0)
                {
                    Student_AchievementManagement doc1 = new Student_AchievementManagement();
                    doc1.Msg = "Please Select Achievement Category !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.Description == "")
                {
                    Student_AchievementManagement doc1 = new Student_AchievementManagement();
                    doc1.Msg = "Please Enter Discription !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                Student_AchievementManagement result = new Student_AchievementManagement();
                string jsonstring = JsonConvert.SerializeObject(doc);
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        if (Request.Files.GetKey(0) == "file")
                        {
                            HttpPostedFileBase fileUpload = Request.Files.Get(0);
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                            }
                            Stream st1 = fileUpload.InputStream;
                            string name = Path.GetFileName(fileUpload.FileName);
                            try
                            {
                                string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                string s3DirectoryName = "Student/Document";
                                string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_StudentAchievementDocument_" + doc.SID + @name;
                                s3FileName = s3FileName.Replace(" ", "");
                                doc.DocumentURl = s3FileName;
                                bool a;
                                AmazonUploader myUploader = new AmazonUploader();
                                a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                            }
                            catch (Exception ex)
                            {
                                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student : Achievement Document Upload", jsonstring);
                            }
                        }
                        else
                        {
                            Student_AchievementManagement ob1 = new Student_AchievementManagement();
                            ob1.Msg = "Please Upload Vaild Document !!!";
                            return Json(ob1, JsonRequestBehavior.AllowGet);
                        }

                    }
                    catch (Exception ex)
                    {

                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student : Achievement Document Upload", jsonstring);
                        Student_AchievementManagement ob3 = new Student_AchievementManagement();
                        ob3.Msg = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    doc.DocumentURl = doc.hfile;
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    doc.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                string collegeID = "";
                string encollegeID = ClsLanguage.GetCookies("ENNBCLID");

                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                doc.CollegeID = (collegeID != "" ? Convert.ToInt32(collegeID) : 0);
                result = ob.SaveAchievementDetail(doc);
                if (result.Status)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StudentAdmissionQualification logmsg = new StudentAdmissionQualification();
                logmsg.Msg = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult StudentAchievementDetail(string id = "")
        {
            Student_AchievementManagement ob = new Student_AchievementManagement();
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    ViewBag.ID = "";
                    var result = ob.findStudentachievementDetail(eID);
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i].EncriptedID = EncriptDecript.Encrypt(result[i].ID.ToString());
                        ViewBag.ID = result[0].EncriptedID;
                    }
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student : Achievement Document Detail Get Method SID=", enID);
            }
            return View(ob);
        }
        [BasicAuthentication]
        public JsonResult GetSessionList(string EducationTypeID = "", string CourseCategoryID = "")
        {
            int educationTypeID = (EducationTypeID != null ? Convert.ToInt32(EducationTypeID) : 0);
            int courseCategoryID = (CourseCategoryID != null ? Convert.ToInt32(CourseCategoryID) : 0);
            AcademicSession session = new AcademicSession();
            var obj = session.GetSessionListCourseWise(courseCategoryID);

            return Json(new { data = obj, success = true });
        }
        [BasicAuthentication]
        public JsonResult GetAchievementList(string SID = "")
        {
            int sID = (SID != null ? Convert.ToInt32(SID) : 0);
            Student_AchievementManagement ob = new Student_AchievementManagement();
            var obj = ob.AchievementList(sID);
            //  return Json(obj, JsonRequestBehavior.AllowGet);
            return Json(new { data = obj, success = true });
        }

        [BasicAuthentication]
        public JsonResult GetStudentList(string SessionID = "", string EducationTypeID = "", string CourseCategoryID = "")
        {
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            int sessionID = (SessionID != null ? Convert.ToInt32(SessionID) : 0);
            int educationTypeID = (EducationTypeID != null ? Convert.ToInt32(EducationTypeID) : 0);
            int courseCategoryID = (CourseCategoryID != null ? Convert.ToInt32(CourseCategoryID) : 0);
            int cid = (collegeID != null ? Convert.ToInt32(collegeID) : 0);
            Student_AchievementManagement com = new Student_AchievementManagement();
            var obj = com.StudentList(sessionID, educationTypeID, courseCategoryID, cid);
            //  return Json(obj, JsonRequestBehavior.AllowGet);
            return Json(new { data = obj, success = true });
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentAchievementList()
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.Session = "";
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            Student_AchievementManagement ob = new Student_AchievementManagement();
            ViewBag.studentList = "";
            Commn_master com = new Commn_master();
            ViewBag.Course = "";
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult StudentAchievementList(int id = 0)
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));// objmaster.GetEducationType();

            Student_AchievementManagement ob = new Student_AchievementManagement();
            ViewBag.studentList = ob.StudentList();
            ViewBag.AchievementList = ob.AchievementList();
            Commn_master com = new Commn_master();
            ViewBag.Course = com.getcommonMaster("coursesfordrop");
            string SID = Request.Form["SID"];
            string AchievementMasterID = Request.Form["AchievementMasterID"];
            Student_AchievementManagement obj = new Student_AchievementManagement();
            Student_AchievementManagementList sub = new Student_AchievementManagementList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.achievementList(1, 1000000, collegeID, SID, AchievementMasterID);
            var result = sub.qlist.Select(x => new { Name = x.Name, EnrollmentNo = x.EnrollmentNo, Course = x.CourseCategory, AchievementCategory = x.AchievementName, Description = x.Description, Session = x.SessionName }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=StudentAchievementList" + System.DateTime.Now.ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentAchievementMaster(int pageIndex = 1, int page = 0)
        {
            Achievementmaster ob = new Achievementmaster();
            Achievementmaster res = new Achievementmaster();
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            int pageSize = 10;
            if (page > 0) pageIndex = page;
            var result = ob.AchievementDetailList(collegeID, pageIndex, pageSize);
            res.AchievementList = result.qlist;
            ViewBag.totalCount = result.totalCount;

            return View(res);
        }
        public JsonResult CheckAchievementValue(string name = "")
        {
            Achievementmaster st = new Achievementmaster();
            var obj = st.CheckAchievementName(name);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddStudentAchievementMaster(Achievementmaster ob)
        {
            Achievementmaster st = new Achievementmaster();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    ob.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    ob.InsertedBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                var result = st.AddNewAchievement(ob);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add New Achievement master method", jsonstring);
                st.Msg = "Something Went Wrong";
                return Json(st, JsonRequestBehavior.AllowGet);
            }

        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult AddEvent(string id = "")
        {
            EventManagment ob = new EventManagment();

            string enID = "";
            int eID = 0;
            try
            {
                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";

                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                ViewBag.EventOrganiserID = ob.EventOrganiserList();
                ViewBag.EventTypeID = ob.EventTypeList(collegeID);
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var result = ob.findEventDetail(eID);
                    if (result != null)
                    {
                        ViewBag.EventType = result.EventTypeID;
                        ViewBag.EventOrganiser = result.EventOrganiserID;
                    }
                    return View(result);
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Event : Add Event Get Method ID=", enID);
            }
            return View(ob);
        }
        public ActionResult EventDescription(string id = "")
        {
            EventManagment ob = new EventManagment();
            string enID = "";
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var result = ob.findDescription(eID);
                    if (result != null)
                    {
                        ViewBag.EventType = result.EventTypeID;
                        ViewBag.EventOrganiser = result.EventOrganiserID;
                    }
                    return View(result);
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Event : Event Description Method ID=", enID);
            }
            return View(ob);
        }

        public JsonResult AddNewEvent(string id = "")
        {
            EventManagment ob = new EventManagment();
            if (Request.Form.Count > 0)
            {
                EventManagment doc = new EventManagment();
                doc.ID = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.EventName = Request.Form["EventName"];
                doc.Amount = Convert.ToDecimal(Request.Form["Amount"] == "" ? "0" : Request.Form["Amount"]);
                doc.EventOrganiserID = Convert.ToInt32(Request.Form["EventOrganiserID"] == "" ? "0" : Request.Form["EventOrganiserID"]);
                doc.EventTypeID = Convert.ToInt32(Request.Form["EventTypeID"] == "" ? "0" : Request.Form["EventTypeID"]);
                doc.Description = Request.Form["Description"];
                doc.FromDate = Request.Form["FromDate"];
                doc.ToDate = Request.Form["ToDate"];
                doc.IsPaid = Convert.ToBoolean(Request.Form["IsPaid"] == "" ? "0" : Request.Form["IsPaid"]);
                doc.Venue = Request.Form["Venue"];
                doc.hfile = Request.Form["hfile"];
                if (doc.EventName == "")
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Enter Event Name  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.Description == "")
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Enter Description  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.FromDate == "")
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Select From Date  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.ToDate == "")
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Enter To Date !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.IsPaid == true)
                {
                    if (doc.Amount == 0)
                    {
                        EventManagment doc1 = new EventManagment();
                        doc1.Msg = "Please Enter Amount   !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                }
                if (doc.EventTypeID == 0)
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Select Event Type  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.EventOrganiserID == 0)
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Select Event Organiser  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (doc.Venue == "")
                {
                    EventManagment doc1 = new EventManagment();
                    doc1.Msg = "Please Enter Venue  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }

                EventManagment result = new EventManagment();
                string jsonstring = JsonConvert.SerializeObject(doc);
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        if (Request.Files.GetKey(0) == "file")
                        {
                            HttpPostedFileBase fileUpload = Request.Files.Get(0);
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                            }
                            Stream st1 = fileUpload.InputStream;
                            string name = Path.GetFileName(fileUpload.FileName);
                            try
                            {
                                string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                string s3DirectoryName = "College";
                                string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_EventDocument_" + doc.EventName + @name;
                                s3FileName = s3FileName.Replace(" ", "");
                                doc.FileUrl = s3FileName;
                                bool a;
                                AmazonUploader myUploader = new AmazonUploader();
                                a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                            }
                            catch (Exception ex)
                            {
                                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Event : document Image Upload", jsonstring);
                            }
                        }
                        else
                        {
                            StudentAdmissionQualification ob1 = new StudentAdmissionQualification();
                            ob1.Msg = "Error occurred. Error details: ";
                            return Json(ob1, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Event : document Image Upload", jsonstring);
                        StudentAdmissionQualification ob3 = new StudentAdmissionQualification();
                        ob3.Msg = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    doc.FileUrl = doc.hfile;
                }
                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                doc.CollegeID = Convert.ToInt32(collegeID);
                string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                string InsertedBy = "";
                if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                {
                    InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                }
                doc.InsertedBy = Convert.ToInt32(InsertedBy);
                result = ob.SaveEventDetails(doc);
                if (result.Status)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                EventManagment logmsg = new EventManagment();
                logmsg.Msg = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EventList()
        {
            EventManagment ob = new EventManagment();
            ViewBag.EventOrganiserID = ob.EventOrganiserList();
            ViewBag.EventTypeID = ob.EventTypeList();
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult EventList(int id = 0)
        {
            EventManagment ob = new EventManagment();
            ViewBag.EventOrganiserID = ob.EventOrganiserList();
            ViewBag.EventTypeID = ob.EventTypeList();
            int EventTypeID = Convert.ToInt32(Request.Form["EventTypeID"] == "" ? "0" : Request.Form["EventTypeID"]);
            string EventName = Request.Form["Name"];
            int EventOrganiserID = Convert.ToInt32(Request.Form["EventOrganiserID"] == "" ? "0" : Request.Form["EventOrganiserID"]);
            EventManagment obj = new EventManagment();
            EventManagmentList sub = new EventManagmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.GetEventList(1, 1000000, EventTypeID.ToString(), EventOrganiserID.ToString(), collegeID, EventName);
            var result = sub.qlist.Select(x => new { EventName = x.EventName, EventCategory = x.EventCategoryName, OrganiserName = x.OrganiserName, FromDate = x.FromDate, toDate = x.ToDate, CreateDate = x.Createdate, Venue = x.Venue, EventFee = x.Amount }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=CollegeEventList" + System.DateTime.Now.ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        public JsonResult ActiveDeactiveEvent(string id, string type)
        {
            var result = false;
            var Id = Int32.Parse(id);
            result = EventManagment.activedeactiveEvent(Id);
            return Json(result);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult Eventinvitation(string id = "")
        {
            EventOrganiserMaster ob = new EventOrganiserMaster();
            string enID = "";
            int eID = 0;
            try
            {
                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                ViewBag.EventList = ob.EventList(collegeID);
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var result = ob.findGuestDetail(eID);
                    if (result != null)
                    {
                        ViewBag.EventType = result.EventID;
                    }
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Event : Add Event Get Method ID=", enID);
            }
            return View(ob);
        }
        public JsonResult AddNewGuest(EventOrganiserMaster ob)
        {
            EventOrganiserMaster obj = new EventOrganiserMaster();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                {
                    if (ob.EventID == 0)
                    {
                        EventOrganiserMaster doc1 = new EventOrganiserMaster();
                        doc1.Msg = "Please select Event !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                    if (ob.Name == "")
                    {
                        EventOrganiserMaster doc1 = new EventOrganiserMaster();
                        doc1.Msg = "Please Enter Guest Name  !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }

                    if (ob.MobileNo == "")
                    {
                        EventOrganiserMaster doc1 = new EventOrganiserMaster();
                        doc1.Msg = "Please Enter Guest Mobile No !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                    if (ob.Email == "")
                    {
                        EventOrganiserMaster doc1 = new EventOrganiserMaster();
                        doc1.Msg = "Please Enter Guest Email !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }
                    if (ob.Designation == "")
                    {
                        EventOrganiserMaster doc1 = new EventOrganiserMaster();
                        doc1.Msg = "Please Enter Guest Designation !!";
                        return Json(doc1, JsonRequestBehavior.AllowGet);
                    }

                    string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                    string collegeID = "";
                    if (encollegeID != "0" && encollegeID.Length > 0)
                    {
                        collegeID = EncriptDecript.Decrypt(encollegeID);
                    }
                    ob.CollegeID = Convert.ToInt32(collegeID);
                    string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                    string InsertedBy = "";
                    if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                    {
                        InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                    }
                    ob.InsertedBy = Convert.ToInt32(InsertedBy);
                    var result = obj.SaveGuestDetail(ob);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "AddnewGuest Method ", jsonstring);
            }

            return Json("error", JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult GuestList()
        {
            EventOrganiserMaster ob = new EventOrganiserMaster();
            string enID = "";
            int eID = 0;
            try
            {
                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                ViewBag.EventList = ob.EventList(collegeID);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "GuestList Get Method ID=", enID);
            }
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult GuestList(int id = 0)
        {
            EventOrganiserMaster ob = new EventOrganiserMaster();
            int EventID = Convert.ToInt32(Request.Form["EventID"] == "" ? "0" : Request.Form["EventID"]);
            string MobileNo = Request.Form["MobileNo"];
            string Name = Request.Form["Name"];
            EventOrganiserMaster obj = new EventOrganiserMaster();
            EventOrganiserMasterList sub = new EventOrganiserMasterList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            ViewBag.EventList = ob.EventList(collegeID);
            sub = obj.GetEventinvitationList(1, 1000000, collegeID, EventID.ToString(), MobileNo, Name);
            var result = sub.qlist.Select(x => new { EventName = x.EventName, GuestName = x.Name, GuestMobileNo = x.MobileNo }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=EventGuestList" + System.DateTime.Now.ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        public JsonResult ActiveDeactiveGuest(string id, string type)
        {
            var result = false;
            var Id = Int32.Parse(id);
            result = EventOrganiserMaster.activedeactiveGuest(Id);
            return Json(result);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EventCategory(int pageIndex = 1, int page = 0, string id = "")
        {
            EventTypeMaster ob = new EventTypeMaster();
            EventTypeMaster res = new EventTypeMaster();
            string enID = "";
            int eID = 0;
            try
            {
                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                int pageSize = 10;
                if (page > 0) pageIndex = page;
                var result = ob.GetEventCategoryList(pageIndex, pageSize, collegeID);
                for (int i = 0; i < result.EventCategoryList.Count; i++)
                {
                    result.EventCategoryList[i].EncriptedID = EncriptDecript.Encrypt(result.EventCategoryList[i].ID.ToString());
                }
                res.EventCategoryList = result.EventCategoryList;
                ViewBag.totalCount = result.totalCount;
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var resdata = ob.findEventCategoryDetail(eID);
                    resdata.EventCategoryList = result.EventCategoryList;
                    return View(resdata);
                }

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Event : EventCategory Get Method ID=", enID);
            }
            return View(res);
        }
        public JsonResult CheckEventCategory(string name = "")
        {
            EventTypeMaster st = new EventTypeMaster();
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var obj = st.CheckDesignationName(name, collegeID);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddNewEventCategory(EventTypeMaster ob)
        {
            EventTypeMaster obj = new EventTypeMaster();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                if (ob.EventName == "")
                {
                    EventTypeMaster doc1 = new EventTypeMaster();
                    doc1.Msg = "Please select Event Name !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                ob.CollegeID = Convert.ToInt32(collegeID);
                string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                string InsertedBy = "";
                if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                {
                    InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                }
                ob.InsertedBy = Convert.ToInt32(InsertedBy);
                var result = obj.SaveEventCategoryDetail(ob);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "AddNewEventCategory Method ", jsonstring);
            }

            return Json("error", JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActiveDeactiveEventCategory(string id, string type)
        {
            var result = false;
            var Id = Int32.Parse(id);
            result = EventTypeMaster.activedeactiveEventCategory(Id);
            return Json(result);
        }

        public JsonResult GetYear()
        {
            List<Employee_DocumentUpload> obyear = new List<Employee_DocumentUpload>();
            for (int i = System.DateTime.Now.Year; i >= 1980; i--)
            {
                obyear.Add(new Employee_DocumentUpload { ID = i, year = i.ToString() });
            }
            return Json(new { data = obyear, success = true });
        }
        public ActionResult EmployeeServiceBook(string id = "")
        {
            int eID = 0;
            try
            {
                if (id != "0" && id.Length > 0)
                {
                    eID = Convert.ToInt32(EncriptDecript.Decrypt(id));
                    EmployeeRegisteration emp = new EmployeeRegisteration();
                    var result = emp.getdetailsForBioData(eID);
                    if (result.empregistration.Nominnee != null)
                    {
                        var nomineeID = result.empregistration.Nominnee.Split(',');
                        var nomineeper = result.empregistration.NominneePercentage.Split(',');
                        CollegeEmployee_Information ob = new CollegeEmployee_Information();
                        ob.NominneeList = ob.RelationTypeList(eID);
                        List<CollegeEmployee_Information> nominee = new List<CollegeEmployee_Information>();
                        var name = "";
                        int k = 0;
                        for (int i = 0; i < nomineeID.Length - 1; i++)
                        {
                            if (ob.NominneeList[i].Id.ToString() == nomineeID[i])
                            {
                                name = ob.NominneeList[i].MemberName;
                                nominee.Add(new CollegeEmployee_Information
                                {
                                    Id = ob.NominneeList[i].Id,
                                    MemberName = ob.NominneeList[i].MemberName,
                                    NominneePercentage = nomineeper[i]
                                });
                                k++;
                            }

                        }
                        result.empregistration.NominneeList = nominee;
                        return View(result);
                    }
                    else
                    {
                        return View(result);
                    }

                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "EmployeeServiceBook get Method", eID.ToString());

            }

            return View();
        }
        public ActionResult EmployeeFamilyMember(int pageIndex = 1, int page = 0, string id = "")
        {
            Employee_FamilyDetail ob = new Employee_FamilyDetail();
            string enID = "";
            int eID = 0;
            try
            {
                ViewBag.Relation = ob.RelationTypeList();
                string enempID = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                string empID = "";
                if (enempID != "0" && enempID.Length > 0)
                {
                    empID = EncriptDecript.Decrypt(enempID);
                }

                int pageSize = 10;
                if (page > 0) pageIndex = page;
                var result = ob.FamilyMemberList(empID, pageIndex, pageSize);
                for (int i = 0; i < result.qlist.Count; i++)
                {
                    result.qlist[i].EncriptedID = EncriptDecript.Encrypt(result.qlist[i].ID.ToString());
                }
                ob.qlist = result.qlist;
                ViewBag.totalCount = (result.totalCount == null ? "" : result.totalCount);
                if (id != "0" && id.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(id);
                    eID = (enID != "" ? Convert.ToInt32(enID) : 0);
                    var resdata = ob.findfamilyMemberDetail(eID);
                    ViewBag.RelationID = resdata.RelationTypeID;
                    resdata.qlist = result.qlist;
                    return View(resdata);
                }
                return View(ob);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : EmployeeFamilyMember Get Method ID=", enID);
            }
            return View(ob);
        }

        public JsonResult AddFamilyMember(Employee_FamilyDetail ob)
        {
            Employee_FamilyDetail obj = new Employee_FamilyDetail();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {

                if (ob.MemberName == "")
                {
                    Employee_FamilyDetail doc1 = new Employee_FamilyDetail();
                    doc1.Msg = "Please Enter Family Member Name  !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (ob.Relation == "")
                {
                    Employee_FamilyDetail doc1 = new Employee_FamilyDetail();
                    doc1.Msg = "Please Enter Family Member Relation !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }
                if (ob.MobileNo == "")
                {
                    Employee_FamilyDetail doc1 = new Employee_FamilyDetail();
                    doc1.Msg = "Please Enter  Family Member Mobile No !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }

                if (ob.Age == "")
                {
                    Employee_FamilyDetail doc1 = new Employee_FamilyDetail();
                    doc1.Msg = "Please Enter Family Member Age !!";
                    return Json(doc1, JsonRequestBehavior.AllowGet);
                }

                string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
                string collegeID = "";
                if (encollegeID != "0" && encollegeID.Length > 0)
                {
                    collegeID = EncriptDecript.Decrypt(encollegeID);
                }
                ob.CollegeID = Convert.ToInt32(collegeID);
                string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                string InsertedBy = "";
                if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                {
                    InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                }
                ob.InsertedBy = Convert.ToInt32(InsertedBy);
                ob.EmployeeID = Convert.ToInt32(InsertedBy);
                var result = obj.SaveFamilymemberDetail(ob);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "AddFamilyMember Method ", jsonstring);
                Employee_FamilyDetail doc1 = new Employee_FamilyDetail();
                doc1.Msg = "Something Went Wrong !!";
                return Json(doc1, JsonRequestBehavior.AllowGet);
            }

            return Json("error", JsonRequestBehavior.AllowGet);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeInformation(string id = "")
        {
            CollegeEmployee_Information ob = new CollegeEmployee_Information();
            string InsertedBy = "";
            int eID = 0;
            try
            {
                string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                {
                    InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                }
                int empID = Convert.ToInt32(InsertedBy);
                //ViewBag.FamilyMember = ob.RelationTypeList(empID);

                ob.NominneeList = ob.RelationTypeList(empID);
                if (id != "0" && id.Length > 0)
                {
                    eID = Convert.ToInt32(EncriptDecript.Decrypt(id));
                    CollegeEmployee_Information emp = new CollegeEmployee_Information();
                    var result = emp.getdetails(eID);
                    ViewBag.IsVerify = result.IsVerify;
                    result.NominneeList = ob.NominneeList;
                    return View(result);
                }
                else
                {
                    var res = ob.getinfodetails(empID);
                    ViewBag.IsVerify = 0;
                    if (res == true)
                    {
                        return RedirectToAction("EmployeeInformationDetail");
                    }
                }
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "EmployeeInformation get Method", eID.ToString());

            }
            return View(ob);
        }
        [HttpPost]
        public JsonResult AddEmployeeInformation(string id = "")
        {
            CollegeEmployee_Information ob = new CollegeEmployee_Information();
            if (Request.Form.Count > 0)
            {
                CollegeEmployee_Information doc = new CollegeEmployee_Information();
                doc.Id = Convert.ToInt32(Request.Form["ID"] == "" ? "0" : Request.Form["ID"]);
                doc.MedicalReport = Request.Form["MedicalReport"];
                doc.CharacterCertificate = Request.Form["CharacterCertificate"];
                doc.GPFNo = Request.Form["GPFNo"];
                doc.GPFRemarks = Request.Form["GPFRemarks"];
                doc.IsConstitution = Convert.ToBoolean(Request.Form["IsConstitution"] == "" ? "0" : Request.Form["IsConstitution"]);
                doc.IsSecrecy = Convert.ToBoolean(Request.Form["IsSecrecy"] == "" ? "0" : Request.Form["IsSecrecy"]);
                doc.IsMarried = Convert.ToBoolean(Request.Form["IsMarried"] == "" ? "0" : Request.Form["IsMarried"]);
                doc.hfile = Request.Form["hfile"];
                doc.chfile = Request.Form["chfile"];
                doc.NominneePercentage = Request.Form["NominneePercentage"];
                doc.Nominnee = Request.Form["Nominnee"];
                //if (doc.MedicalReport == "")
                //{
                //    CollegeEmployee_Information doc1 = new CollegeEmployee_Information();
                //    doc1.Msg = "Please Enter Medical Report Detail !!";
                //    return Json(doc1, JsonRequestBehavior.AllowGet);
                //}
                //if (doc.CharacterCertificate == "")
                //{
                //    CollegeEmployee_Information doc1 = new CollegeEmployee_Information();
                //    doc1.Msg = "Please Enter Character Certificate Detail !!";
                //    return Json(doc1, JsonRequestBehavior.AllowGet);
                //}
                //if (doc.GPFNo == "")
                //{
                //    CollegeEmployee_Information doc1 = new CollegeEmployee_Information();
                //    doc1.Msg = "Please Enter GPFNo !!";
                //    return Json(doc1, JsonRequestBehavior.AllowGet);
                //}
                //if (doc.GPFRemarks == "")
                //{
                //    CollegeEmployee_Information doc1 = new CollegeEmployee_Information();
                //    doc1.Msg = "Please Enter GPF Remarks !!";
                //    return Json(doc1, JsonRequestBehavior.AllowGet);
                //}             

                CollegeEmployee_Information result = new CollegeEmployee_Information();
                string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                string InsertedBy = "";
                if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                {
                    InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                }
                doc.InsertedBy = Convert.ToInt32(InsertedBy);
                doc.EmployeeID = Convert.ToInt32(InsertedBy);
                string jsonstring = JsonConvert.SerializeObject(doc);
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            if (Request.Files.GetKey(i) == "file")
                            {
                                HttpPostedFileBase fileUpload = Request.Files.Get(0);
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                }
                                Stream st1 = fileUpload.InputStream;
                                string name = Path.GetFileName(fileUpload.FileName);
                                try
                                {
                                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                    string s3DirectoryName = "College";
                                    string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_MedDocument_EmpID_" + doc.EmployeeID + @name;
                                    s3FileName = s3FileName.Replace(" ", "");
                                    doc.MedicalReportDoc = s3FileName;
                                    bool a;
                                    AmazonUploader myUploader = new AmazonUploader();
                                    a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                                }
                                catch (Exception ex)
                                {
                                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : medical document Image Upload", jsonstring);
                                }
                            }


                            if (Request.Files.GetKey(i) == "file1")
                            {
                                HttpPostedFileBase fileUpload = Request.Files.Get(0);
                                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                                {
                                    string[] testfiles = fileUpload.FileName.Split(new char[] { '\\' });
                                }
                                Stream st1 = fileUpload.InputStream;
                                string name = Path.GetFileName(fileUpload.FileName);
                                try
                                {
                                    string myBucketName = "mungeruniversity"; //your s3 bucket name goes here  
                                    string s3DirectoryName = "College";
                                    string s3FileName = System.DateTime.Now.ToString("dd_MM_yyyy_HH:mm:ss") + "_CharDocument_EmpID_" + doc.EmployeeID + @name;
                                    s3FileName = s3FileName.Replace(" ", "");
                                    doc.CharacterCertificateDoc = s3FileName;
                                    bool a;
                                    AmazonUploader myUploader = new AmazonUploader();
                                    a = myUploader.sendMyFileToS3(st1, myBucketName, s3DirectoryName, s3FileName);
                                }
                                catch (Exception ex)
                                {
                                    CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : character certificate document Image Upload", jsonstring);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee : document Image Upload", jsonstring);
                        CollegeEmployee_Information ob3 = new CollegeEmployee_Information();
                        ob3.Msg = "Error occurred. Error details: " + ex.Message;
                        return Json(ob3, JsonRequestBehavior.AllowGet);
                    }
                }
                //else
                //{
                //    CollegeEmployee_Information ob1 = new CollegeEmployee_Information();
                //    ob1.Msg = "please Upload Document: ";
                //    return Json(ob1, JsonRequestBehavior.AllowGet);
                //}
                if (doc.MedicalReportDoc == null)
                {
                    doc.MedicalReportDoc = doc.hfile;
                }
                if (doc.CharacterCertificateDoc == null)
                {
                    doc.CharacterCertificateDoc = doc.chfile;
                }
                result = ob.SaveInformationDetails(doc);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CollegeEmployee_Information logmsg = new CollegeEmployee_Information();
                logmsg.Msg = "Error occurred. Error details: ";
                return Json(logmsg, JsonRequestBehavior.AllowGet);
            }
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeInformationDetail()
        {
            return View();
        }

        public ActionResult EmployeeInformationVerify(string id = "")
        {
            CollegeEmployee_Information ob = new CollegeEmployee_Information();
            int eID = 0;
            string stdID = "";
            string InsertedBy = "";
            try
            {
                string enInsertedBy = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
                if (enInsertedBy != "0" && enInsertedBy.Length > 0)
                {
                    InsertedBy = EncriptDecript.Decrypt(enInsertedBy);
                }
                int empID = Convert.ToInt32(InsertedBy);
                if (id != "0" && id.Length > 0)
                {
                    eID = Convert.ToInt32(EncriptDecript.Decrypt(id));
                    CollegeEmployee_Information emp = new CollegeEmployee_Information();
                    ob.NominneeList = ob.RelationTypeList(eID);
                    var result = emp.getdetailsData(eID);
                    ViewBag.IsVerify = result.IsVerify;
                    ViewBag.Reason = result.RejectReason;
                    var nomineeID = result.Nominnee.Split(',');
                    var nomineeper = result.NominneePercentage.Split(',');
                    List<CollegeEmployee_Information> nominee = new List<CollegeEmployee_Information>();
                    var name = "";
                    int k = 0;
                    for (int i = 0; i < nomineeID.Length - 1; i++)
                    {
                        if (ob.NominneeList[i].Id.ToString() == nomineeID[i])
                        {
                            name = ob.NominneeList[i].MemberName;
                            nominee.Add(new CollegeEmployee_Information
                            {
                                Id = ob.NominneeList[i].Id,
                                MemberName = ob.NominneeList[i].MemberName,
                                NominneePercentage = nomineeper[i]
                            });
                            k++;
                        }

                    }
                    result.NominneeList = nominee;
                    return View(result);
                }
                return View(ob);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee Information verify get action", stdID);
                return RedirectToAction("EmployeeInformationDetail/");
            }
        }
        public JsonResult EmployeeInformationVerifyed(string Id = "")
        {
            CollegeEmployee_Information ob = new CollegeEmployee_Information();
            CollegeEmployee_Information result = new CollegeEmployee_Information();
            int sid = 0;
            try
            {
                if (Id != "")
                {
                    sid = Convert.ToInt32(Id);
                }
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    result = ob.CertificateVerifyEmp(sid, eID);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                ob.Msg = "Somrething Wrong happened!!";
                return Json(ob, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee Certificate Document Verify get action ID=", Id);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something hdhsajd Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult EmployeeInformationReject(string Id = "", string reason = "")
        {
            CollegeEmployee_Information ob = new CollegeEmployee_Information();
            CollegeEmployee_Information result = new CollegeEmployee_Information();
            int sid = 0;
            try
            {
                if (Id != "")
                {
                    sid = Convert.ToInt32(Id);
                }
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    result = ob.CertificateRejectEmp(sid, reason, eID);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                ob.Msg = "Somrething Wrong happened!!";
                return Json(ob, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee Certificate Document Reject get action ID=", Id);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ExamFormList()
        {

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();

            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            return View();

        }
        [HttpPost]
        public ActionResult ExamFormList(string id = "")
        {

            AcademicSession sess = new AcademicSession();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            ViewBag.Session = sess.GetSession();
            var ses = sess.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            string session = Request.Form["session"];
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            string EducationTypeID = Request.Form["program"];
            string CourseCategoryID = Request.Form["Course"];
            string Subject = Request.Form["Subject"];
            string CourseYearID = Request.Form["CourseYearID"];
            string Application = Request.Form["Application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string paymentStatus = Request.Form["paymentStatus"];
            string BackStatus = Request.Form["BackStatus"];
            int BackStatus1 = Convert.ToInt32((BackStatus == "" ? "0" : BackStatus));
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            
            if (EducationTypeID == "11")
            {
                sub = obj.ExamFeeStudentdetailListexport(1, 10000000, collegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);

            }
            if (EducationTypeID == "41")
            {
                sub = obj.ExamFeeStudentdetailListexport_LLB(1, 10000000, collegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationTypeID == "40")
            {
                sub = obj.ExamFeeStudentdetailListexport_Bed(1, 10000000, collegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationTypeID == "12")
            {
                sub = obj.ExamFeeStudentdetailListexport_PG(1, 10000000, collegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationTypeID == "13")
            {
                sub = obj.ExamFeeStudentdetailListexport_BCA(1, 10000000, collegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }

            var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CollegeName = x.CollegeName, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, StudentName = x.StudentName, RollNo = x.RollNo, HounorsSubject = x.HounorsSubject, Subsidiary1 = x.Subsidiary1, Subsidiary2 = x.Subsidiary2, Compulsory1 = x.Compulsory1, Compulsory2 = x.Compulsory2, CasteCategory = x.StudentCasteCategoryName, Course = x.coursecategotyName, Session = x.Session, ResultStatus = x.ResultName, ExamFormVerifyStatus = (x.IsDocVerify == 1 ? "Verified" : (x.IsDocVerify == 2 ? "Rejected" : "Pending")), ExamFormVerificationDate = x.IsDocVerifyDatenew, ExamFeesAmount = x.Fees, ExamFeesStatus = (x.IsAdmissionFee == true ? "Paid" : "Pending"), ExamFeesSubmissionDate = x.IsfeesubmitDate }).ToList();

            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ExamFeeStudentdetailList" + System.DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();
        }

        [HttpPost]
        public ActionResult ExamFormListLLB(string id = "")
        {
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();
            AcademicSession sess = new AcademicSession();
            ViewBag.Session = sess.GetSession();
            var ses = sess.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollege();
            string session = Request.Form["session"];
            string CollegeID = Request.Form["CollegeID"];
            string EducationTypeID = Request.Form["EducationTypeID"];
            string CourseCategoryID = Request.Form["CourseCategoryID"];
            string Subject = Request.Form["Subject"];
            string CourseYearID = Request.Form["CourseYearID"];
            string Application = Request.Form["Application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string paymentStatus = Request.Form["paymentStatus"];
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();

            sub = obj.CollegeStudentdetailList(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus);
            var result = sub.qlist.Select(x => new { CollegeName = x.CollegeName, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, StudentName = x.StudentName, CasteCategory = x.CasteCategoryName, Course = x.coursecategotyName, Session = x.Session, CollegeFormStatus = (x.IsDocVerify == 1 ? "Verified" : (x.IsDocVerify == 2 ? "Rejected" : "Pending")), CollegeFormVerificationDate = x.IsDocVerifyDatenew, CollegeFeeAmount = x.Fees, FeesStatus = (x.IsAdmissionFee == true ? "Paid" : "Pending"), FeesSubmissionDate = x.IsfeesubmitDate }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CollegeFormFeeList" + System.DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();
        }


        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeFormList()
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            return View();
        }


        [HttpPost]
        public ActionResult CollegeFormList(string id = "")
        {
            //string collegCode =
            string CollegeID = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();

            AcademicSession sess = new AcademicSession();
            ViewBag.Session = sess.GetSession();
            //var ses = sess.GetAcademiccurrentSession();
            //ViewBag.CuSession = ses.ID;

            ViewBag.CourseYear = "";
            ViewBag.Course = "";

            //BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollege();

            string session = Request.Form["session"];

            string EducationTypeID = Request.Form["EducationTypeID"];
            string CourseCategoryID = Request.Form["CourseCategoryID"];

            string Subject = Request.Form["Subject"];

            string CourseYearID = Request.Form["CourseYearID"];

            string Application = Request.Form["Application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string paymentStatus = Request.Form["paymentStatus"];
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();

            //sub = obj.CollegeStudentdetailList(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus);

            sub = obj.CollegeStudentdetailList(1, 100000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus);


            var result = sub.qlist.Select(x => new
            {
                //CollegeName = x.CollegeName,
                ApplicationNo = x.ApplicationNo,
                RegistrationNo = x.EnrollmentNo,
                StudentName = x.StudentName,
                Fathername = x.FatherName,
                CasteCategory = x.StudentCasteCategoryName,
                Course = x.coursecategotyName,
                Session = x.Session,
                ExamFormStatus = (x.IsDocVerify == 1 ? "Verified" : (x.IsDocVerify == 2 ? "Rejected" : "Pending")),
                ExamFormVerificationDate = x.IsDocVerifyDatenew,
                ExamFeeAmount = x.Fees,
                FeesStatus = (x.IsAdmissionFee == true ? "Paid" : "Pending"),
                FeesSubmissionDate = x.IsfeesubmitDate


            }

            ).ToList();


            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CollegeAdmissionFormList" + System.DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View();
        }


        //  [VerifyUrlFilterCollegeAttribute]

        public ActionResult ExamFormVerify(string id = "")
        {
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var obj1 = ob.GetAppLicationDataForExamFee(sid);
                Commn_master com = new Commn_master();
                Commn_master datecom = new Commn_master();
                datecom = com.check_ExamFeeSubmit_check(obj1.objExamFrom.sessionid, Convert.ToInt32(obj1.objExamFrom.EducationType));
                ViewBag.isdateopen = 0;
                //ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
                //ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;

                //ViewBag.check_EnrollmentFeeSubmit_open = com.check_ExamFeeSubmit_open(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
                ////ViewBag.check_EnrollmentFeeSubmit_Close = com.check_ExamFeeSubmit_close(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
                if (datecom.isopendate == true && datecom.isclosedate == true)
                {
                    ViewBag.isdateopen = 1;
                }
                //else
                //{
                //    return RedirectToAction("ExamFormList");
                //}
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                QualifiationMasterList sub = new QualifiationMasterList();
                sub = obj.QualificationdetailList(1, 100000, obj1.objExamFrom.sid);
                ViewBag.previousqualification = sub;
                List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
                feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                List<EaxmFeesSubmit> subjectList = new List<EaxmFeesSubmit>();
                subjectList = ob.SubjectDetailList(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
                List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
                List<EaxmFeesSubmit> Electivesubjectlist_c7b = new List<EaxmFeesSubmit>();
                if(obj1.objExamFrom.coursecategoryid==28)// for B.ed
                { 

                    if (obj1.objExamFrom.courseyearid == 29)// for B.Ed. part2
                    {

                        Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist_bed_C11(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
                        Electivesubjectlist_c7b = ob.ElectiveFeesDetailSubjectlist_bed_c7b(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
                        ViewBag.Electivesubjectlist = Electivesubjectlist;
                        ViewBag.Electivesubjectlist_c7b = Electivesubjectlist_c7b;
                    }
                    if (obj1.objExamFrom.courseyearid == 28)// for B.Ed. part1
                    {

                        Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist_bed_c7a(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
                        ViewBag.Electivesubjectlist = Electivesubjectlist;
                     }
                }
                if (obj1.objExamFrom.coursecategoryid == 7 || obj1.objExamFrom.coursecategoryid == 9 || obj1.objExamFrom.coursecategoryid == 11)// for P.G.
                {
                    Electivesubjectlist = ob.ElectiveFeesDetailSubjectlist_pg(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, Convert.ToInt32(CommonMethod.SubSubjectType.Electivesubject));
                    ViewBag.Electivesubjectlist = Electivesubjectlist;
                }


                    var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectList);
                return View(tuple);


            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student ExamForm Verify get action", stdID);
                return RedirectToAction("ExamFormList/");
            }


        }
        public JsonResult ExamFormVerifyForStudent(string sid = "", string courseyearid = "",int collegeid=0,int  StreamCategoryID=0)
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                int coursecategoryid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(studID);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                    coursecategoryid= (obj1 != null ? obj1.CourseCategory : 0);
                }
                var result = ob.student_examform_apply(studID, Sessionid, coursecategoryid, collegeid, StreamCategoryID, courseid, Convert.ToInt32(0), Convert.ToInt32(0), "BCA", "", enID);


              //  var result = ob.ExamFromVerify(studID, courseid, eID, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam from Verify json action", sid);
                var obj = new ExamForm();
                obj.Status = false;
                obj.Msg = "Something hdhsajd Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExamFormVerifyback(string id = "",string courseyear="")
        {
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                BL_PrintApplication PritApp = new BL_PrintApplication();
                int courseyearid = 0;
                if (courseyear != "")
                {
                    courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyear));
                }
                var obj1 = ob.GetAppLicationDataForExamFeeBack(sid, courseyearid);
                Commn_master com = new Commn_master();
                Commn_master datecom = new Commn_master();
                datecom = com.check_BAckExamFeeSubmit_check(obj1.objExamFrom.sessionid, Convert.ToInt32(obj1.objExamFrom.EducationType));
                ViewBag.isdateopen = 0;
                if (datecom.isopendate == true && datecom.isclosedate == true)
                {
                    ViewBag.isdateopen = 1;
                }
                

                //int courseyearid = 0;
                // manaul change  course year value from  fetch tbl_CourseYear according to couryearid 
                //if (obj1.objExamFrom.coursecategoryid == 29)// for LLB which semester open back exam form 
                //{
                //    courseyearid = 31;//// couryearid 30 for first year; , kon se semester ke lia open krna h , manaul set semesterid
                //}
                //if (obj1.objExamFrom.coursecategoryid == 28)// for BED which semester open back exam form 
                //{
                //   // courseyearid = 28;//// couryearid 28 for first year; , kon se semester ke lia open krna h , manaul set semesterid
                //}
                //if (obj1.objExamFrom.coursecategoryid == 26)// for BCA which semester open back exam form 
                //{
                //    courseyearid = 11;//// couryearid 10 for first year; , kon se semester ke lia open krna h , manaul set semesterid
                //}
                //if (obj1.objExamFrom.coursecategoryid == 7)// for MA which semester open back exam form 
                //{
                //    courseyearid = 16;//// couryearid 16 for first sem; , kon se semester ke lia open krna h , manaul set semesterid
                //}
                //if (obj1.objExamFrom.coursecategoryid == 9)// for mcom which semester open back exam form 
                //{
                //    courseyearid = 26;//// couryearid 26 for sem year; , kon se semester ke lia open krna h , manaul set semesterid
                //}
                //if (obj1.objExamFrom.coursecategoryid == 11)// for msc which semester open back exam form 
                //{
                //    courseyearid = 24;//// couryearid 24 for first sem; , kon se semester ke lia open krna h , manaul set semesterid
                //}
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                QualifiationMasterList sub = new QualifiationMasterList();
                //sub = obj.QualificationdetailList(1, 100000, obj1.objExamFrom.sid);
                ViewBag.previousqualification = sub;
                List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
                feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                List<EaxmFeesSubmit> subjectList = new List<EaxmFeesSubmit>();

                if (obj1.objExamFrom.EducationType == 13)
                {
                     obj1 = ob.GetAppLicationDataForExamFeeBackbca(sid, courseyearid);
                    subjectList = ob.backFeesDetailSubjectlist_bca(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                }
                if (obj1.objExamFrom.EducationType == 41)
                {
                    subjectList = ob.backFeesDetailSubjectlist_LLB(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                }
                if (obj1.objExamFrom.EducationType == 40)
                {
                    subjectList = ob.backFeesDetailSubjectlist_BED(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                }
                if (obj1.objExamFrom.EducationType == 12)
                {
                    subjectList = ob.backFeesDetailSubjectlist_PG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, courseyearid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                }

                var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectList);
                return View(tuple);


            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student ExamForm Verify get action", stdID);
                return RedirectToAction("ExamFormList/");
            }


        }
        public JsonResult ExamFormVerifyForStudent_back(string sid = "", string courseyearid = "",string collegeid1="",string StreamCategoryID1="")
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                int StreamCategoryID = (StreamCategoryID1 != "" ? Convert.ToInt32(StreamCategoryID1) : 0);
                int collegeid = (collegeid1 != "" ? Convert.ToInt32(collegeid1) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                 
                }
                StudentLogin tblST = new StudentLogin();
                DataLayer.Login objstu = new DataLayer.Login();
                var obj1 = tblST.BasicDetailByID(studID);
                Sessionid = (obj1 != null ? obj1.session : 0);
                var result = ob.student_examform_applyBack1(studID, obj1.session, obj1.CourseCategory, collegeid, StreamCategoryID, courseid, Convert.ToInt32(0), Convert.ToInt32(0), "", "", enID);

                // var result = ob.ExamFromVerify(studID, courseid, eID, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam from Verify json action", sid);
                var obj = new ExamForm();
                obj.Status = false;
                obj.Msg = "Something hdhsajd Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ExamFormVerifyForStudentUG(int sid = 0, int sessionid = 0, int coursecategoryid = 0, int collegeid = 0, int StreamCategoryID = 0, int courseyearid = 0)
        {
            ExamForm ob = new ExamForm();
            try
            {
                string enID = "";
                string CollegeUserID = ClsLanguage.GetCookies("ENNBUID");
                if (CollegeUserID != "0" && CollegeUserID.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(CollegeUserID);

                }
                var result = ob.student_examform_apply(sid, sessionid, coursecategoryid, collegeid, StreamCategoryID, courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "", enID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam from Verify json action", sid.ToString());
                var obj = new ExamForm();
                obj.Status = false;
                obj.Msg = "Something Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExamFormVerifyForStudentUGBack(int sid = 0, int sessionid = 0, int coursecategoryid = 0, int collegeid = 0, int StreamCategoryID = 0, int courseyearid = 0)
        {
            ExamForm ob = new ExamForm();
            try
            {
                string enID = "";
                string CollegeUserID = ClsLanguage.GetCookies("ENNBUID");
                if (CollegeUserID != "0" && CollegeUserID.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(CollegeUserID);

                }
                var result = ob.student_examform_applyBack(sid, sessionid, coursecategoryid, collegeid, StreamCategoryID, courseyearid, Convert.ToInt32(0), Convert.ToInt32(0), "UG", "", enID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam from Verify json action", sid.ToString());
                var obj = new ExamForm();
                obj.Status = false;
                obj.Msg = "Something Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeFormVerify(string id = "")
        {
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                ExamForm ob = new ExamForm();
                AcademicSession ad = new AcademicSession();
                BL_PrintApplication PritApp = new BL_PrintApplication();
                var obj1 = ob.GetAppLicationDataForCollegeFee(sid);
                Commn_master com = new Commn_master();
              
                ViewBag.check_EnrollmentFeeSubmit_open = com.CheckStudentAddmisionStartDate(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
                ViewBag.check_EnrollmentFeeSubmit_Close = com.CheckStudentAddmisionExtendDate(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
                //if (ViewBag.check_EnrollmentFeeSubmit_open == true && ViewBag.check_EnrollmentFeeSubmit_Close == true)
                //{
                //}
                //else
                //{
                //    return RedirectToAction("CollegeFormList");
                //}
                StudentAdmissionQualification obj = new StudentAdmissionQualification();
                QualifiationMasterList sub = new QualifiationMasterList();
                sub = obj.QualificationdetailList(1, 100000, obj1.objExamFrom.sid);
                ViewBag.previousqualification = sub;
                List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
                feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
                List<EaxmFeesSubmit> subjectList = new List<EaxmFeesSubmit>();
                subjectList = ob.SubjectDetailList(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid);
                var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectList);
                return View(tuple);


            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student CollegeForm Verify get action", stdID);
                return RedirectToAction("CollegeFormList/");
            }


        }
        public JsonResult CollegeFormVerifyForStudent(string sid = "", string courseyearid = "", int coursecategoryid = 0, int collegeid1 = 0, int StreamCategoryID = 0)
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {
                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(studID);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }
                var result = ob.CollegeFromVerify(studID, courseid, eID, Sessionid, coursecategoryid, collegeid1, StreamCategoryID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam from Verify json action", sid);
                var obj = new ExamForm();
                obj.Status = false;
                obj.Msg = "Something hdhsajd Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CollegeFormForStudentrejects(string sid = "", string courseyearid = "", string reason = "")
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(studID);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.CollegeFromReject(studID, courseid, eID, reason, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam Form Reject get action", sid);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExamFormForStudentrejects(string sid = "", string courseyearid = "", string reason = "")
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(studID);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.ExamFromReject(studID, courseid, eID, reason, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam Form Reject get action", sid);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ExamFormFor_updateelective1subject(string sid = "", string courseyearid = "", int Substreamcategoryid = 0,int coursecategoryid=0)
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(studID);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.ExamFormFor_updateelective1subject(studID, courseid, eID, "", Sessionid, coursecategoryid, Substreamcategoryid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam Form Reject get action", sid);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ExamFormFor_updateelective1subject_2(string sid = "", string courseyearid = "", int Substreamcategoryid2 = 0, int coursecategoryid = 0)
        {
            ExamForm ob = new ExamForm();
            int studID = 0;
            try
            {
                studID = (sid != "" ? Convert.ToInt32(sid) : 0);
                int courseid = (courseyearid != "" ? Convert.ToInt32(courseyearid) : 0);
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(studID);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.ExamFormFor_updateelective1subject_2(studID, courseid, eID, "", Sessionid, coursecategoryid, Substreamcategoryid2);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Exam Form Reject get action", sid);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ExamFormVerifyUG(string id = "")
        {
            //return RedirectToAction("Index", "Home");


            BL_PrintApplication PritApp = new BL_PrintApplication();
            string stdID = "";

            int sid = 0;
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                sid = Convert.ToInt32(stdID);
            }
            if (sid == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();

            var obj1 = ob.GetAppLicationDataForExamFee(sid);
            ViewBag.id = id;
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_ExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.isdateopen = 0;
            //ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            //ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;

            //ViewBag.check_EnrollmentFeeSubmit_open = com.check_ExamFeeSubmit_open(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
            ////ViewBag.check_EnrollmentFeeSubmit_Close = com.check_ExamFeeSubmit_close(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
            if (datecom.isopendate == true && datecom.isclosedate == true)
            {
                ViewBag.isdateopen = 1;
            }

            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "Home");
            }
            //if (obj1.objExamFrom.sessionid == 39) // only for 2 year student check 
            //{
            //    if (obj1.objExamFrom.Currentyear_courseyarid == 3 || obj1.objExamFrom.Currentyear_courseyarid == 6 || obj1.objExamFrom.Currentyear_courseyarid == 9)
            //    {
            //        // first check exam fee payment before admission fee submit or not 
            //        int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            //        if (a <= 0)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //    }

            //}
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.FeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);


        }


        [VerifyUrlFilterCollegeAttribute]
        public ActionResult ExamFormVerifyBackUG(string id = "", string courseyear = "")
        {
            string stdID = "";
            int sid = 0;
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                sid = Convert.ToInt32(stdID);
            }
            if (sid == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int courseyearid = 0;
            if (courseyear != "")
            {
                courseyearid = Convert.ToInt32(EncriptDecript.Decrypt(courseyear));
            }
            ExamForm ob = new ExamForm();
            AcademicSession ad = new AcademicSession();
            BL_PrintApplication PritApp = new BL_PrintApplication();
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            var obj1 = ob.GetAppLicationDataForExamFeeBack(sid, courseyearid);
            ViewBag.id = id;
            Commn_master com = new Commn_master();
            Commn_master datecom = new Commn_master();
            datecom = com.check_BAckExamFeeSubmit_check(ad.GetAcademiccurrentSession().ID, Convert.ToInt32(CommonSetting.Commonid.Educationtype));
            ViewBag.isdateopen = 0;
            //ViewBag.check_EnrollmentFeeSubmit_open = datecom.isopendate;
            //ViewBag.check_EnrollmentFeeSubmit_Close = datecom.isclosedate;

            //ViewBag.check_EnrollmentFeeSubmit_open = com.check_ExamFeeSubmit_open(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
            ////ViewBag.check_EnrollmentFeeSubmit_Close = com.check_ExamFeeSubmit_close(obj1.objExamFrom.sessionid, obj1.objExamFrom.EducationType);
            if (datecom.isopendate == true && datecom.isclosedate == true)
            {
                ViewBag.isdateopen = 1;
            }
            if (obj1.objExamFrom == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.isfeesubmitregistration == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.RegistrationNo == null || obj1.objExamFrom.RegistrationNo == "")
            {
                return RedirectToAction("Index", "Home");
            }

            // only for session student check

            if (obj1.objExamFrom.sessionid != 39)
            {
                return RedirectToAction("Index", "Home");
            }
            if (obj1.objExamFrom.Currentyear_courseyarid == 2 || obj1.objExamFrom.Currentyear_courseyarid == 5 || obj1.objExamFrom.Currentyear_courseyarid == 8)
            {
                // allow only for 2 year student which have back paper
                // first check exam fee payment before admission fee submit or not 
                //int a = ob.check_examfeebefore_admissionfee(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.Currentyear_courseyarid, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
                //if (a <= 0)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            else
            { return RedirectToAction("Index", "Home"); }
            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            List<EaxmFeesSubmit> feestruckture = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> subjectlist = new List<EaxmFeesSubmit>();
            List<EaxmFeesSubmit> Electivesubjectlist = new List<EaxmFeesSubmit>();
            feestruckture = ob.backFeesDetailsstructure(obj1.objExamFrom.collegeid, obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.sessionid, obj1.objExamFrom.castecategoryid, obj1.objExamFrom.StreamCategoryID, obj1.objExamFrom.courseyearid, 0);
            // manaul change  course year value from  fetch tbl_CourseYear according to couryearid
            subjectlist = ob.backFeesDetailSubjectlist_UG(obj1.objExamFrom.coursecategoryid, obj1.objExamFrom.StreamCategoryID, 1, obj1.objExamFrom.sessionid, obj1.objExamFrom.collegeid, obj1.objExamFrom.sid);
            if (subjectlist.Count == 0)
            {
                // if subject count greater than 0 then is counted as back student else it not consider as back student
                return RedirectToAction("Index", "Home");
            }
            var tuple = new Tuple<PrintExamForm, QualifiationMasterList, List<EaxmFeesSubmit>, List<EaxmFeesSubmit>>(obj1, sub, feestruckture, subjectlist);
            return View(tuple);
        }
        public ActionResult ExamCheckList(int Id = 0, int session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int Examtype = 0, int SubjectType = 0)
        {
            PrintExamForm_ehceklist model = new PrintExamForm_ehceklist();

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            ViewBag.Subject = "";
            BL_courseMaster objmaster = new BL_courseMaster();
            AcademicSession sessioncls = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.Session = sessioncls.GetSession();
            ViewBag.Educationtype = objmaster.GetEducationType(); //objmaster.GetEducationType(Convert.ToInt32(collegCode));
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            ViewBag.SubjectType = 0;
            var ses = sessioncls.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            ViewBag.Examtype = Examtype;
            CheckListClass obj = new CheckListClass();
            //model.checkList = obj.ExamCheckList(session, EduTypeID, CourseCategoryID, CourseYearID, Examtype, Convert.ToInt32(collegCode.ToString()), SubjectType);
            model = obj.ExamCheckList_temp_rollnowise_new(session, EduTypeID, CourseCategoryID, CourseYearID, Examtype, Convert.ToInt32(collegCode.ToString()), SubjectType);
            return View(model);
        }
        [HttpPost]
        public ActionResult ExamCheckList(int session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int Examtype = 0, int SubjectType = 0)
        {

            PrintExamForm_ehceklist model = new PrintExamForm_ehceklist();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();
            AcademicSession sessioncls = new AcademicSession();
            Commn_master com = new Commn_master();
            ViewBag.Session = sessioncls.GetSession();
            ViewBag.Educationtype = objmaster.GetEducationType(); //objmaster.GetEducationType(Convert.ToInt32(collegCode));
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            BL_StreamMaster blstream = new BL_StreamMaster();
            ViewBag.Subject = blstream.getsubjectbycourse(24, CourseCategoryID, 0);

            var ses = sessioncls.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            if (CourseCategoryID > 0)
            {
                ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", CourseCategoryID);
            }
            ViewBag.Course = "";
            ViewBag.SubjectType = SubjectType;
            CheckListClass obj = new CheckListClass();
            ViewBag.Examtype = Examtype;
            //model.checkList = obj.ExamCheckList_temp(session, EduTypeID, CourseCategoryID, CourseYearID, Examtype, Convert.ToInt32(collegCode.ToString()), SubjectType);
            model = obj.ExamCheckList_temp_rollnowise_new(session, EduTypeID, CourseCategoryID, CourseYearID, Examtype, Convert.ToInt32(collegCode.ToString()), SubjectType);


            return View(model);
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult MigrationCertificateVerify(string id = "")
        {
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                StudentLogin tblST = new StudentLogin();
                DataLayer.Login objstu = new DataLayer.Login();
                var obj1 = tblST.BasicDetailByID(sid);
                if (obj1 != null)
                {
                    int sessionid = obj1.session;
                    objstu = tblST.migrationCertificateDetailID(sid, sessionid);
                    if (objstu != null)
                    {
                        ViewBag.IsAdmissionFee2 = objstu.IsAdmissionFee2;
                        ViewBag.incomecertificate_iseligible = objstu.migrationcertificate_iseligible;
                        ViewBag.Reason = objstu.Reason;
                    }
                }

                return View(objstu);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student migration Document Verify get action", stdID);
                return RedirectToAction("DocumentVerifyList/");
            }


        }
        public JsonResult migrationDocumentVerifyForStudent(string Id = "")
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            int sid = 0;
            try
            {
                if (Id != "")
                {
                    sid = Convert.ToInt32(Id);
                }
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(sid);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.migrationVerifyStudent(sid, eID, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student migration Document Verify get action", Id);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something hdhsajd Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult migrationDocumentVerifyForStudentrejects(string Id = "", string reason = "")
        {
            BL_PrintApplication ob = new BL_PrintApplication();
            int sid = 0;
            try
            {
                if (Id != "")
                {
                    sid = Convert.ToInt32(Id);
                }
                string enCollegeID = ClsLanguage.GetCookies("ENNBUID");
                string enID = "";
                int eID = 0;
                int Sessionid = 0;
                if (enCollegeID != "0" && enCollegeID.Length > 0)
                {

                    enID = EncriptDecript.Decrypt(enCollegeID);
                    if (enID != "")
                    {
                        eID = Convert.ToInt32(enID);
                    }
                    StudentLogin tblST = new StudentLogin();
                    DataLayer.Login objstu = new DataLayer.Login();
                    var obj1 = tblST.BasicDetailByID(sid);
                    Sessionid = (obj1 != null ? obj1.session : 0);
                }

                var result = ob.migrationVerifyStudentreject(sid, eID, reason, Sessionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student migration Document Verify get action", Id);
                var obj = new BL_PrintApplication();
                obj.Status = false;
                obj.Msg = "Something Wrong Happen!!!";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult googletranslate(string text)
        {
            if (text == "")
            {
                return Json(new { data = "", success = true });
            }
            string url = String.Format
           ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
            "en", "hi", Uri.EscapeUriString(text));
            //WebClient client = new WebClient();
            //string json = client.DownloadString(url);
            //JsonData jsonData = (new JavaScriptSerializer()).Deserialize<JsonData>(json);
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;

            // Get all json data
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);

            // Extract just the first array element (This is the only data we are interested in)
            var translationItems = jsonData[0];

            // Translation Data
            string translation = "";

            // Loop through the collection extracting the translated objects
            foreach (object item in translationItems)
            {
                // Convert the item array to IEnumerable
                IEnumerable translationLineObject = item as IEnumerable;

                // Convert the IEnumerable translationLineObject to a IEnumerator
                IEnumerator translationLineString = translationLineObject.GetEnumerator();

                // Get first object in IEnumerator
                translationLineString.MoveNext();

                // Save its value (translated text)
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }

            // Remove first blank character
            if (translation.Length > 1) { translation = translation.Substring(1); };
            //Response.Write(translation);
            return Json(new { data = translation, success = true });
        }
        // [VerifyUrlFilterCollegeAttribute]
        public ActionResult EmployeeDeatilsList()
        {
            EmployeeRegisteration ob = new EmployeeRegisteration();
            var collegeId = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            }
            ViewBag.Designation = ob.getDesignationMaster(collegeId);
            ViewBag.FacultyType = ob.FacultyTypeList1;
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeDeatilsList(int id = 0)
        {
            EmployeeRegisteration ob = new EmployeeRegisteration();
            var collegeId = 0;
            if (ClsLanguage.GetCookies("ENNBCLID") != null)
            {
                collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            }
            ViewBag.Designation = ob.getDesignationMaster(collegeId);
            ViewBag.FacultyType = ob.FacultyTypeList1;
            EmployeedetailsListList sub = new EmployeedetailsListList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            var ID = 0;
            var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
            if (insertby != "")
            {
                ID = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
            }
            string MobileNo = Request.Form["Status"];
            string Email = Request.Form["fromdate"];
            string Name = Request.Form["Name"];
            string FacultyType = Request.Form["facultyType"];
            string Designation = Request.Form["ApplicationNo"];
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            Employeedetialslist obj = new Employeedetialslist();
            sub = obj.EmployeedetailsListList(1, 100000000, MobileNo, Email, collegeID, Designation, FacultyType, "", Name, 0, ID);
            var result = sub.qlist.Select(x => new
            {
                //GetType().GetProperty("post_subject").DisplayAttribute.GetCustomAttribute.NamedArguments.FirstOrDefault().TypedValue.Value  =x.post_subject
                //Employeedetialslist.GetType().GetProperty("post_subject").CustomAttributes.FirstOrDefault(xx => xx.AttributeType.Name == "DisplayAttribute").NamedArguments.FirstOrDefault().TypedValue.Value.ToString()

                x.Ids
                        ,
                x.collegename
                        ,
                x.Employeetype
                        ,
                x.grade
                        ,
                x.post_subject
                        ,
                x.NoofsanctionPosts
                        ,
                x.ExistingSanctionedPost
                        ,
                x.Nameoftheteacher
                        ,
                x.PresentDesignation
                        ,
                x.Designation
                        ,
                x.BankACNo
                        ,
                x.IFSCCode
                        ,
                x.BankName
                        ,
                x.AppttonRecommendoftheCommissorbytheAbsorption
                        ,
                x.DateofBirth
                        ,
                x.Effectivedateofappointment
                        ,
                x.DateofJoining
                        ,
                x.DateofPromotionSrLect
                        ,
                x.DateofPromotionLectSelGrReader
                        ,
                x.DateofPromotionProfessor
                        ,
                x.DateofPromotion1stACP
                        ,
                x.DateofPromotion2stACP
                        ,
                x.DateofPromotionMACP
                        ,
                x.UnivServiceCommConcurrenceinpromotion
                        ,
                x.Designationatthetimeoffirstappointment
                        ,
                x.PayScaleatthetimeoffirstappointment
                        ,
                x.PaySlipReceiptNo
                        ,
                x.type
                        ,
                x.salSlNo1
                        ,
                x.OldBasicPayason01012016
                        ,
                x.Whetherlivinginquarter
                        ,
                x.PayinpayMatrixason01012016
                        ,
                x.ApplicablePayMatrixason0103019
                        ,
                x.Dateofnextincrement
                        ,
                x.BasicPayason01032020
                        ,
                x.DA25
                        ,
                x.HRA8
                        ,
                x.CTA25
                        ,
                x.MA
                        ,
                x.OtherAllow
                        ,
                x.TotalperMonthonMarch2020SumofCol22toCol27
                        ,
                x.NoofMonths
                        ,
                x.TotalSalaryWEFMarch20ToJune20
                        ,
                x.BasicPayason010720
                        ,
                x.DA25_2
                        ,
                x.HRA8_2
                        ,
                x.CTA25_2
                        ,
                x.MA1
                        ,
                x.OtherAllow1
                        ,
                x.TotalperMonthonJuly2020SumofCol31toCol36
                        ,
                x.NoofMonths1
                        ,
                x.TotalSalaryWEFJuly20ToDec20
                        ,
                x.BasicPayason01012021
                        ,
                x.DA25_3
                        ,
                x.HRA8_3
                        ,
                x.CTA25_3
                        ,
                x.MA2
                        ,
                x.OtherAllow2
                        ,
                x.TotalperMonthonJan2021SumofCol40toCol45
                        ,
                x.NoofMonths2
                        ,
                x.TotalSalaryWEFJan21ToFeb21
                        ,
                x.TotalPerYearCol303948

            }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (gv.Rows.Count > 0)
            {
                for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
                {
                    string gg = gv.HeaderRow.Cells[i].Text;

                    if (gg.ToLower() == "ids".ToLower()) { gg = "SL. No."; }
                    if (gg.ToLower() == "collegename".ToLower()) { gg = "College Name"; }
                    if (gg.ToLower() == "employeetype".ToLower()) { gg = "Employee Type"; }
                    if (gg.ToLower() == "grade".ToLower()) { gg = "Grade"; }
                    if (gg.ToLower() == "post_subject".ToLower()) { gg = "Post subject"; }
                    if (gg.ToLower() == "noofsanctionPosts".ToLower()) { gg = "No. of Sanc. Posts as per HRD Letter No.-1733 Dated - 24.8.2012 & 2264 dt. 06.11.201"; }
                    if (gg.ToLower() == "existingSanctionedPost".ToLower()) { gg = "Existing Sanctioned Post Since 16.8.76"; }
                    if (gg.ToLower() == "nameoftheteacher".ToLower()) { gg = "Name of the Teacher"; }
                    if (gg.ToLower() == "presentDesignation".ToLower()) { gg = "Present Designation"; }
                    if (gg.ToLower() == "designation".ToLower()) { gg = "Designation"; }
                    if (gg.ToLower() == "bankACNo".ToLower()) { gg = "Bank A/C No"; }
                    if (gg.ToLower() == "ifscCode".ToLower()) { gg = "IFSC Code"; }
                    if (gg.ToLower() == "bankName".ToLower()) { gg = "Bank Name"; }
                    if (gg.ToLower() == "appttonRecommendoftheCommissorbytheAbsorption".ToLower()) { gg = "Apptt. on Recommend. of the Commiss. or by the Absorption"; }
                    if (gg.ToLower() == "dateofBirth".ToLower()) { gg = "Date of Birth	"; }
                    if (gg.ToLower() == "effectivedateofappointment".ToLower()) { gg = "Effective date of appointment"; }
                    if (gg.ToLower() == "dateofJoining".ToLower()) { gg = "Date of Joining"; }
                    if (gg.ToLower() == "dateofPromotionSrLect".ToLower()) { gg = "Date of Promotion Sr. Lect."; }
                    if (gg.ToLower() == "dateofPromotionLectSelGrReader".ToLower()) { gg = "Date of Promotion Lect. Sel.Gr./ Reader"; }
                    if (gg.ToLower() == "dateofPromotionProfessor".ToLower()) { gg = "Date of Promotion Professor"; }
                    if (gg.ToLower() == "dateofPromotion1stACP".ToLower()) { gg = "Date of Promotion 1st ACP"; }
                    if (gg.ToLower() == "dateofPromotion2stACP".ToLower()) { gg = "Date of Promotion 2nd ACP"; }
                    if (gg.ToLower() == "dateofPromotionMACP".ToLower()) { gg = "Date of Promotion MACP"; }
                    if (gg.ToLower() == "univServiceCommConcurrenceinpromotion".ToLower()) { gg = "Univ. Service Comm. Concurrence in promotion"; }
                    if (gg.ToLower() == "designationatthetimeoffirstappointment".ToLower()) { gg = "Designation at the time of first appointment"; }
                    if (gg.ToLower() == "payScaleatthetimeoffirstappointment".ToLower()) { gg = "Pay Scale at the time of first appointment"; }
                    if (gg.ToLower() == "paySlipReceiptNo".ToLower()) { gg = "Pay Slip Receipt No."; }
                    if (gg.ToLower() == "type".ToLower()) { gg = "Continue..."; }
                    if (gg.ToLower() == "salSlNo1".ToLower()) { gg = "Sl.No. 1"; }
                    if (gg.ToLower() == "oldBasicPayason01012016".ToLower()) { gg = "Old Basic Pay as on 01.01.2016"; }
                    if (gg.ToLower() == "whetherlivinginquarter".ToLower()) { gg = "Whether living in quarter"; }
                    if (gg.ToLower() == "payinpayMatrixason01012016".ToLower()) { gg = "Pay in pay Matrix as on 01.01.2016"; }
                    if (gg.ToLower() == "applicablePayMatrixason0103019".ToLower()) { gg = "Applicable Pay Matrix as on 01.03.2019"; }
                    if (gg.ToLower() == "dateofnextincrement".ToLower()) { gg = "Date of next increment"; }
                    if (gg.ToLower() == "basicPayason01032020".ToLower()) { gg = "Basic Pay as on 01.03.2020"; }
                    if (gg.ToLower() == "dA25".ToLower()) { gg = "D.A. (25%)"; }
                    if (gg.ToLower() == "hrA8".ToLower()) { gg = "H.R.A (8%)"; }
                    if (gg.ToLower() == "ctA25".ToLower()) { gg = "CTA @25%"; }
                    if (gg.ToLower() == "ma".ToLower()) { gg = "MA"; }
                    if (gg.ToLower() == "otherAllow".ToLower()) { gg = "Other Allow"; }
                    if (gg.ToLower() == "totalperMonthonMarch2020SumofCol22toCol27".ToLower()) { gg = "Total per Month on March'.2020 (Sum of 35 to 40)	"; }
                    if (gg.ToLower() == "noofMonths".ToLower()) { gg = "No of Months"; }
                    if (gg.ToLower() == "totalSalaryWEFMarch20ToJune20".ToLower()) { gg = "Total Salary W.E.F. March'20 To June'20"; }
                    if (gg.ToLower() == "basicPayason010720".ToLower()) { gg = "Basic Pay as on 01.07.20"; }
                    if (gg.ToLower() == "dA25_2".ToLower()) { gg = "D.A. (25%)"; }
                    if (gg.ToLower() == "hrA8_2".ToLower()) { gg = "H.R.A (8%)"; }
                    if (gg.ToLower() == "ctA25_2".ToLower()) { gg = "CTA @ 25%"; }
                    if (gg.ToLower() == "mA1".ToLower()) { gg = "MA1"; }
                    if (gg.ToLower() == "otherAllow1".ToLower()) { gg = "Other Allow1"; }
                    if (gg.ToLower() == "totalperMonthonJuly2020SumofCol31toCol36".ToLower()) { gg = "Total per Month on July'.2020 (Sum of 40 to 49)"; }
                    if (gg.ToLower() == "noofMonths1".ToLower()) { gg = "No of Months1	"; }
                    if (gg.ToLower() == "totalSalaryWEFJuly20ToDec20".ToLower()) { gg = "Total Salary W.E.F. July'20 To Dec.'20"; }
                    if (gg.ToLower() == "basicPayason01012021".ToLower()) { gg = "Basic Pay as on 01.01.2021"; }
                    if (gg.ToLower() == "dA25_3".ToLower()) { gg = "D.A. (25%)"; }
                    if (gg.ToLower() == "hrA8_3".ToLower()) { gg = "H.R.A (8%)"; }
                    if (gg.ToLower() == "ctA25_3".ToLower()) { gg = "CTA @25%"; }
                    if (gg.ToLower() == "mA2".ToLower()) { gg = "MA2"; }
                    if (gg.ToLower() == "otherAllow2".ToLower()) { gg = "Other Allow2"; }
                    if (gg.ToLower() == "totalperMonthonJan2021SumofCol40toCol45".ToLower()) { gg = "Total per Month on Jan.2021 (Sum of 53 to 58 )"; }
                    if (gg.ToLower() == "noofMonths2".ToLower()) { gg = "No of Months2"; }
                    if (gg.ToLower() == "totalSalaryWEFJan21ToFeb21".ToLower()) { gg = "Total Salary W.E.F. Jan'21 To Feb.'21"; }
                    if (gg.ToLower() == "totalPerYearCol303948".ToLower()) { gg = "Total Per Year ( Col 43+52+61)"; }

                    gv.HeaderRow.Cells[i].Text = gg;
                }
            }
            gv.RowStyle.CssClass = "textmode";
            //gv.Rows.
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=EmployeeDeatilsList" + System.DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            Response.ContentType.ToString();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                GridViewRow row = gv.Rows[i];
                row.Attributes.Add("class", "textmode");
                row.Cells[10].Attributes.CssStyle.Add("mso-number-format", "\\@");
                row.Cells[26].Attributes.CssStyle.Add("mso-number-format", "\\@");


                //    row.Cells[2].Attributes.CssStyle.Add("mso-number-format", "\\@");

                //    row.Cells[3].Attributes.CssStyle.Add("mso-number-format", "\\@");
                //    //row.Cells[4].Attributes.CssStyle.Add("mso-number-format", "\\@");

                //   // row.Cells[5].Attributes.CssStyle.Add("mso-number-format", "\\@");

                //    ///row.Cells.Add("class", "textmode");
            }
            gv.RenderControl(htw);
            //format the excel cells to text format
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.ContentType = "application/text";
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult StudentList()
        {
            Commn_master com = new Commn_master();
            AcademicSession session = new AcademicSession();
            //ViewBag.Educationtype = com.getcommonMaster("EducationType");
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollegeData(2);
            ViewBag.Session = session.GetSession();

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            // ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            BL_courseMaster objmaster = new BL_courseMaster();

            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));  //objmaster.GetEducationType();

            return View();
        }
        //[VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult StudentList(int id = 0)
        {
            BL_StudentList objlist = new BL_StudentList();
            BL_courseMaster objmaster = new BL_courseMaster();
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            BL_StudentList sub = new BL_StudentList();
            string name = Request.Form["name"];

            int Subject = !string.IsNullOrEmpty(Request.Form["Subject"]) ? Convert.ToInt16(Request.Form["Subject"]) : 0;
            int ddStatus = !string.IsNullOrEmpty(Request.Form["ddStatus"]) ? Convert.ToInt16(Request.Form["ddStatus"]) : 2;
            string ApplicationNo = Request.Form["ApplicationNo"];
            string sessiontext = Request.Form["session"];
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            int Coursetype = !string.IsNullOrEmpty(Request.Form["Coursetype"]) ? Convert.ToInt16(Request.Form["Coursetype"]) : 0;
            string fromdate = Request.Form["fromdate"];
            string todate = Request.Form["todate"];
            BL_StudentList obj = new BL_StudentList();
            BL_StudentList objStudentList = new BL_StudentList();
            int EducationType = !string.IsNullOrEmpty(Request.Form["EducationType"]) ? Convert.ToInt32(Request.Form["EducationType"]) : 0;
            objStudentList = obj.StudentListPrint(1, 100000, ApplicationNo, sessiontext, ddStatus, name, Coursetype, Subject, collegeID, fromdate, todate, EducationType);
            // var result = objStudentList.qlistPrint.Select(x => new {ApplicationNo=x.ApplicationNo,   Name=x.Name,    NameInHindi=x.NameInHindi, Gender=x.Gender,  DOB=x.DOB, MobileNo=x.MobileNo,    Email=x.Email,   Session=x.Session, FeeStatus =x.FeeStatus,   Course=x.Course,  AddDate =x.adddate, BloodGroup =x.BloodGroup, CurrentAddress=x.CurrentAddress, CurrentPinCode = x.CA_PinCode, CurrentCountry = x.CA_Country, CurrentState = x.CA_State, CurrentCity = x.CA_City, PermanentAddress =x.PA_Address, PermanentPinCode = x.PA_PinCode, PermanentCountry = x.PA_Country, PermanentState = x.PA_State, PermanentCity = x.PA_City, FatherName=x.FatherName,  FatherNameInHindi =x.FatherNameInHindi,  FatherQualification=x.FatherQualification, FatherOccupation=x.FatherOccupation,    FatherMobile=x.FatherMobile,    FatherEmail=x.FatherEmail, MotherName =x.MotherName, MotherNameInHindi =x.MotherNameInHindi,  MotherQualification=x.MotherQualification, MotherOccupation=x.MotherOccupation,    MotherEmail=x.MotherEmail, AdmisitionCategory=x.AdmisitionCategory,  EducationType=x.EducationType,   CourseCategory =x.CourseCategory, FeeSubmit=x.IsFeeSubmit, MotherMobile =x.MotherMobile,   title =x.title,  Ftitle=x.Ftitle,  Nationality=x.Nationality, Religion =x.Religion,   ishandicapped =x.ishandicapped,  is_ncc_candidate=x.is_ncc_candidate,    previous_qua_id=x.previous_qua_id, ReligonOther =x.ReligonOther,   IsSports =x.IsSports ,  IsStaff=x.IsStaff }).ToList();
            var result = objStudentList.qlistPrint.Select(x => new { ApplicationNo = x.ApplicationNo,/* Password = x.Password,*/ StudentName = x.Name, Gender = x.Gender, DOB = x.DOB, MobileNo = x.MobileNo, Email = x.Email, Session = x.Session, FeeStatus = x.FeeStatus, RegistrationDate = x.adddate, BloodGroup = x.BloodGroup, CurrentAddress = x.CurrentAddress, PermanentAddress = x.PA_Address, FatherName = x.FatherName, FatherQualification = x.FatherQualification, FatherOccupation = x.FatherOccupation, FatherMobile = x.FatherMobile, FatherEmail = x.FatherEmail, MotherName = x.MotherName, MotherQualification = x.MotherQualification, MotherOccupation = x.MotherOccupation, MotherEmail = x.MotherEmail, MotherMobile = x.MotherMobile, AdmisitionCategory = x.AdmisitionCategory, EducationType = x.EducationType, CourseCategory = x.CourseCategory, Nationality = x.Nationality, Religion = x.Religion, Handicapped = x.ishandicapped, NccQuota = x.is_ncc_candidate, SportsQuota = x.IsSports, WordQuota = x.IsStaff, StudentCasteCategory = x.studentcaste }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=StudentList" + System.DateTime.Now.ToString() + ".xls");

            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("StudentList", "Home");
            //return View();
        }

        #region work mirgration verify pg
        //[VerifyUrlFilterCollegeAttribute]
        public ActionResult PGDOCVerify(string id = "")
        {
            //string ApplicationID = ClsLanguage.GetCookies("NBApplicationNo");
            string stdID = "";
            try
            {
                int sid = 0;
                if (id != "0" && id.Length > 0)
                {
                    stdID = EncriptDecript.Decrypt(id);
                }
                if (stdID != "")
                {
                    sid = Convert.ToInt32(stdID);
                }
                StudentLogin tblST = new StudentLogin();
                BL_PrintApplication ob = new BL_PrintApplication();
                AcademicSession ad = new AcademicSession();
                int sessionid = ad.GetAcademiccurrentSession().ID;
                StudentLogin STu = new StudentLogin();
                var obj1 = tblST.BasicDetailByID(sid);
                if (obj1 != null)
                {
                    int educationtype = obj1.EducationType;
                    var dateextend = ob.CheckStudentAddmisionExtendDate(sessionid, educationtype, sid);
                    ViewBag.addmissionExtenddate = dateextend.Status;
                    ViewBag.addmissionExtenddateValue = dateextend.ExtendDate;
                    var datestart = ob.CheckStudentAddmisionStartDate(sessionid, educationtype, sid);
                    ViewBag.addmissionStartdate = datestart.Status;
                    ViewBag.addmissionStartdateValue = datestart.startdate;
                    ViewBag.IsApplied = ob.CheckStudentApplied(obj1.session, sid).Status;
                    ViewBag.IsVerify = ob.CheckStudentVerify(obj1.session, sid).Status;

                }

                var objstu = ob.CheckStudentAdmission(obj1.session, sid);
                if (objstu.Status == true)
                {
                    ViewBag.Status = objstu.Status;
                    ViewBag.Course = objstu.CourseName;
                    ViewBag.College = objstu.CollegeName;
                    ViewBag.Stream = objstu.StreamName;
                }
                else
                {
                    ViewBag.Status = false;
                    ViewBag.Course = "";
                    ViewBag.College = "";
                }
                BL_PrintAllRecord result = ob.AdmissionDetail(obj1.session, sid);
                return View(result);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Student Document Verify get action", stdID);
                return RedirectToAction("DocumentVerifyList/");
            }


        }
        #endregion
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult clcallot(string id = "")
        {
            string stdID = "";
            int sid = 0;
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                sid = Convert.ToInt32(stdID);
            }
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailForclc(sid, 0);
            ViewBag.id = id;
            return View(result);


        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult tcallot(string id = "")
        {
            string stdID = "";
            int sid = 0;
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                sid = Convert.ToInt32(stdID);
            }
            ExamForm ob = new ExamForm();
            Commn_master com = new Commn_master();
            AcademicSession ad = new AcademicSession();
            var result = ob.StudentDetailFortc(sid, 0);
            ViewBag.id = id;
            return View(result);


        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult SaveCertificateclc(string SerialNo = "", string id = "",string Remarks="")
        {
            ExamForm ob = new ExamForm();
            string stdID = "";
            int sid = 0;
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                sid = Convert.ToInt32(stdID);
            }
            if (SerialNo == "")
            {
                ExamForm result1 = new ExamForm();
                result1.Status = false;
                result1.Msg = "Please Enter SerialNo /AllotNo";
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID"))) : 0);
            var result = ob.Savecertificateclc(sid, insertby, "CLC", 1, SerialNo, Remarks);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [BasicAuthentication]
        public JsonResult SaveCertificatetc(string SerialNo = "", string id = "", string Remarks = "")
        {
            ExamForm ob = new ExamForm();
            string stdID = "";
            int sid = 0;
            if (id != "0" && id.Length > 0)
            {
                stdID = EncriptDecript.Decrypt(id);
            }
            if (stdID != "")
            {
                sid = Convert.ToInt32(stdID);
            }
            if (SerialNo == "")
            {
                ExamForm result1 = new ExamForm();
                result1.Status = false;
                result1.Msg = "Please Enter SerialNo /AllotNo";
                return Json(result1, JsonRequestBehavior.AllowGet);
            }
            var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID"))) : 0);
            var result = ob.Savecertificateclc(sid, insertby, "TC", 2, SerialNo, Remarks);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [VerifyUrlFilterCollegeAttribute]

        public ActionResult clcReport()
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollege();
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult clcReport(int id = 0)
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
           // ViewBag.College = objcol.GetCollege();
            string EducationTypeID = Request.Form["program"];
            string session1 = Request.Form["session"];
            string CourseCategoryID = Request.Form["CoursetypeID"];
            string Name = Request.Form["Name"];
            string RegistrationNo = Request.Form["RegistrationNo"];
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");

            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            ExamForm obj = new ExamForm();
            ExamForm_Listlist sub = new ExamForm_Listlist();
            sub = obj.clcReportList(1, 1000000, collegeID, EducationTypeID, CourseCategoryID, Name, RegistrationNo, session1);
            var result = sub.qlist.Select(x => new { ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Collegename = x.CollegeName, StudentName = x.Name, Course = x.CourseApplied, Session = x.sessionname, CertificateType = x.certificateType, SerialNo = x.SerialNo, IssueDate = x.allotdate,Remarks=x.remarks }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=CLCAllotReport" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult clcearch()
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            
            return View();
        }
      //  [VerifyUrlFilterCollegeAttribute]
        public ActionResult Recruitment(string coursetype = "", string educationtype = "")
        {
            string Coursetype = "", educationtype1 = "";
            if (coursetype != "")
            {
                Coursetype = EncriptDecript.Decrypt(coursetype);
            }
            if (educationtype != "")
            {
                educationtype1 = EncriptDecript.Decrypt(educationtype);
            }
            ViewBag.Education = educationtype1;
            ViewBag.course = Coursetype;
            Commn_master com = new Commn_master();
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
           // ViewBag.coursedata = com.getcommonMaster("Course", Convert.ToInt32(educationtype1 == "" ? "0" : educationtype1));
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollege();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
           // ViewBag.College = objcol.GetCollegeData(2);
            ViewBag.CasteCategory = com.getcommonMaster("CastAndReservation");
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            return View();
        }
        [HttpPost]
        public ActionResult Recruitment(int id = 0)
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollege();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            ViewBag.College = objcol.GetCollegeData(2);
            Commn_master com = new Commn_master();
            ViewBag.coursedata = "";
            ViewBag.CasteCategory = com.getcommonMaster("CasteCategory");
            string subject = Request.Form["Subject"];
            string session1 = Request.Form["session"];
            string seatType = Request.Form["SeatType"];
            string cast = Request.Form["CastCategory"];
            string coursetype = Request.Form["Coursetype"];
           // string collegeID = Request.Form["CollegeID"];
            string applicationno = Request.Form["Applicationno"];
            string CounsellingNo = Request.Form["CounsellingNo"];
            string EducationTypeID = Request.Form["EducationTypeID"];
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            tbl_recruitment_cutoff cut = new tbl_recruitment_cutoff();
            ViewBag.CounsellingNo = cut.GetCounsellingNo();
            sub = obj.RecruitmentdetailList(1, 100000, coursetype, subject, session1, collegeID, cast, seatType, applicationno, CounsellingNo, EducationTypeID);
            var result = sub.qlist.Select(x => new { ReservationCategory = x.CasteCategoryName, ApplicationNo = x.ApplicationNo, StudentName = x.StudentName, Percentage = x.percenatge, CollegeName = x.CollegeName, StudentCategory = x.StudentCasteCategoryName, Course = x.coursecategotyName, Stream = x.StreamCategoryName, Session = x.Session, ReservationType = (x.ishandicapped == true ? "Handicapped" : "Normal"), Counsellingno = x.counsellingno }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ScrutinyList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            if (Request.Form["pdf"] == "pdf")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=ScrutinyList" + System.DateTime.Now.ToString() + "7.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
            //GridView1.AllowPaging = true;
            //GridView1.DataBind();
            return View();
        }
        public JsonResult RecruitmentSearch(string coursetype = "", string subject = "", string session = "", string cast = "", string collegeID = "", string seatType = "", string EducationTypeID = "", string CounsellingNo = "")
        {
            Recruitment ob = new Recruitment();
            ob.coursecategotyName = !string.IsNullOrEmpty(coursetype) ? EncriptDecript.Encrypt(coursetype) : "";
            ob.StreamCategoryName = !string.IsNullOrEmpty(subject) ? EncriptDecript.Encrypt(subject) : "";
            ob.Session = !string.IsNullOrEmpty(session) ? EncriptDecript.Encrypt(session) : "";
            ob.CasteCategoryName = !string.IsNullOrEmpty(cast) ? EncriptDecript.Encrypt(cast) : "";
            ob.CollegeName = !string.IsNullOrEmpty(collegeID) ? EncriptDecript.Encrypt(collegeID) : "";
            ob.seatType = !string.IsNullOrEmpty(seatType) ? EncriptDecript.Encrypt(seatType) : "";
            ob.educationtype1 = !string.IsNullOrEmpty(EducationTypeID) ? EncriptDecript.Encrypt(EducationTypeID) : "";
            ob.CounsellingNo1 = !string.IsNullOrEmpty(CounsellingNo) ? EncriptDecript.Encrypt(CounsellingNo) : "";
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        public ActionResult printpdfCollege(string coursetype = "", string subject = "", string session = "", string collegeID11 = "", string cast = "", string seatType = "", string EducationTypeID = "", string CounsellingNo = "")
        {

            Recruitment re = new Recruitment();
            string Coursetype = "", Subject = "", Session = "", CollegeID1 = "", Cast = "", SeatType = "", educationTypeID = "", CounsellingNo1 = "";
            if (coursetype != "")
            {
                Coursetype = EncriptDecript.Decrypt(coursetype);
            }
            if (subject != "")
            {
                Subject = EncriptDecript.Decrypt(subject);
            }
            if (session != "")
            {
                Session = EncriptDecript.Decrypt(session);
            }
            
            if (cast != "")
            {
                Cast = EncriptDecript.Decrypt(cast);
            }
            if (seatType != "")
            {
                SeatType = EncriptDecript.Decrypt(seatType);
            }
            if (EducationTypeID != "")
            {
                educationTypeID = EncriptDecript.Decrypt(EducationTypeID);
            }
            if (CounsellingNo != "")
            {
                CounsellingNo1 = EncriptDecript.Decrypt(CounsellingNo);
            }
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            ViewBag.collegename = "";
            ViewBag.session = Session;
            ViewBag.educationTypeID = "";
            BL_CollegeMaster obj = new BL_CollegeMaster();
            if (educationTypeID != "")
            {
                Commn_master com = new Commn_master();
                var comresult = com.getcommonMaster("selectbycommonid", Convert.ToInt32(educationTypeID)).ToList();
                ViewBag.educationTypeID = comresult.FirstOrDefault().Title;
            }
            if (collegeID !="")
            {
                obj = obj.GetCollegeDataBYID(3, Convert.ToInt32(collegeID));
                ViewBag.collegename = obj.CollegeName;
            }
            if (session != "")
            {
                var res = re.GetSessionBYID(Convert.ToInt32(Session));
                ViewBag.session = res.Session;
            }

            List<Recruitment> ob = new List<Recruitment>();
            ob = re.RecruitmentdetailforPrintCollege(Coursetype, Subject, Session, collegeID, Cast, SeatType, educationTypeID, CounsellingNo1);
            return View(ob);
        }
       /// [VerifyUrlFilterCollegeAttribute]
        public ActionResult EnrollmentRequestList()
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ///ViewBag.College = objcol.GetCollege();
            return View();
        }
        [HttpPost]
        public ActionResult EnrollmentRequestList(int id = 0)
        {
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            ViewBag.Coursetype = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollege();
            string EducationTypeID = Request.Form["program"];
            string session1 = Request.Form["session"];
            string CourseCategoryID = Request.Form["CoursetypeID"];
            string Name = Request.Form["Name"];
            string RegistrationNo = Request.Form["RegistrationNo"];
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");

            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            //StudentRollNo obj = new StudentRollNo();
            //StudentRollNoList sub = new StudentRollNoList();

            //sub = obj.RollNoListOfStd(1, 1000000, collegeID, EducationTypeID, CourseCategoryID, Name, RollNo, session1);
            EnrollmentRequest obj = new EnrollmentRequest();
            EnrollmentRequestList sub = new EnrollmentRequestList();
            sub = obj.EnrollmentDetailList1(1, 1000000, collegeID, EducationTypeID, CourseCategoryID, Name, RegistrationNo, session1);
            //for (int i = 0; i < sub.qlist.Count; i++)
            //{
            //    sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            //}
            var result = sub.qlist.Select(x => new { ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Collegename = x.collegeName, StudentName = x.Name, Course = x.CourseApplied, Session = x.sessionname }).ToList();

            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            if (Request.Form["Excel"] == "excel")
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=StudentRegistrationNoList" + System.DateTime.Now.ToString() + ".xls");

                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            return View();
        }
    }
}
