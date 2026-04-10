using DataLayer;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Areas.college.Models;
using Website.Models;
using Newtonsoft.Json;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;

namespace Website.Areas.College.Controllers
{
    [CookiesExpireFilterCollegeAttribute]
    public class ExamController : Controller
    {
        // GET: College/Exam
        #region CollegeTabulationRegisterUG
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeTabulationRegister(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.Educationtype = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollegeExam();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            if (CourseYearID > 0)
            {
                ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", CourseCategoryID);
            }
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            int pageSize1 = pagesize;
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            if (ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.BA3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bsc3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bcom3rd))
            {
                model.StudentListNew = model.GetTabulationRegister3rdyear("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                model.StudentListNew = model.GetTabulationRegister("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.Select(x => x.Collegename).FirstOrDefault();
                ViewBag.Coursename = model.StudentListNew.Select(x => x.CourseCategoryName).FirstOrDefault();
                ViewBag.CourseYearname = model.StudentListNew.Select(x => x.YearName).FirstOrDefault();
                ViewBag.sessionname = model.StudentListNew.Select(x => x.SessionName).FirstOrDefault();
            }
            else
            {
                if (CourseCategoryID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult CollegeTabulationRegister(int id = 0, int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollegeExam();
            ViewBag.Session = session.GetSession();

            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.Educationtype = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            //ViewBag.Educationtype = com.getcommonMaster("EducationTypeUG");
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            if (CourseYearID > 0)
            {
                ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", CourseCategoryID);
            }
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            if (HonoursSubjectID > 0)
            {

                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize = pagesize;
            int PageIndex = 1;
            PageIndex = page != 0 ? Convert.ToInt32(page) : 1;
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            // ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            if (ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.BA3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bsc3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bcom3rd))
            {
                if (searchType == false)
                {
                    model.StudentListNew = model.GetTabulationRegister3rdyear("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
                }
                else
                {
                    if (enrollmentno != "" || ApplicationNo != "")
                    {
                        model.StudentListNew = model.GetTabulationRegister3rdyear("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    }
                }
            }
            else
            {
                if (searchType == false)
                {
                    model.StudentListNew = model.GetTabulationRegister("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
                }
                else
                {
                    if (enrollmentno != "" || ApplicationNo != "")
                    {
                        model.StudentListNew = model.GetTabulationRegister("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    }
                }

            }

            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.Select(x => x.Collegename).FirstOrDefault();
                ViewBag.Coursename = model.StudentListNew.Select(x => x.CourseCategoryName).FirstOrDefault();
                ViewBag.CourseYearname = model.StudentListNew.Select(x => x.YearName).FirstOrDefault();
                ViewBag.sessionname = model.StudentListNew.Select(x => x.SessionName).FirstOrDefault();
            }
            else
            {
                if (CourseCategoryID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion
        public JsonResult getcousre(int id)
        {
            Commn_master com = new Commn_master();
            var obj = com.getcommonMaster("Course", id);
            return Json(new { data = obj, success = true });
        }
        public JsonResult getsubjtectbycousrescollege(int id)
        {
            BL_StreamMaster com = new BL_StreamMaster();
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            var obj = com.getsubjectbycourse(18, id, CollegeID);
            return Json(new { data = obj, success = true });
        }

        public JsonResult getsubjtectbycousrescollegeallsubject(int id)
        {
            BL_StreamMaster com = new BL_StreamMaster();           
            var obj = com.getsubjectbycourse(24, id, 0);
            return Json(new { data = obj, success = true });
        }
        public JsonResult CourseYear(int id)
        {
            CollegeExamCenter com = new CollegeExamCenter();
            var obj = com.CourseYear(id);
            return Json(new { data = obj, success = true });
        }
        #region CollegeTabulationRegisterBCA
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeTabulationRegisterBCA(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(Convert.ToInt32(CommonSetting.coursecategory.bca));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.bca));

            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;


            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;

            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.showdiv = 1;
            obj = obj.GetBCASubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            model.StudentListNew = model.GetTabulationRegisterBCA("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);


            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;

            }
            else
            {
                if (CourseYearID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }


            return View(model);
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult CollegeTabulationRegisterBCA(int id = 0, int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.bca));
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            obj = obj.GetBCASubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;

            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;


            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.showdiv = 1;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterBCA("FullSearch", Session, Convert.ToInt32(CommonSetting.Commonid.EducationtypeVoc), Convert.ToInt32(CommonSetting.coursecategory.bca), CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    ViewBag.showdiv = 0;
                    model.StudentListNew = model.GetTabulationRegisterBCA("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    if (model.StudentListNew.Count() > 0)
                    {
                        obj = obj.GetBCASubject(model.StudentListNew.ToList().FirstOrDefault().CourseYearID);
                        ViewBag.PaperI = obj.PaperI;
                        ViewBag.PaperII = obj.PaperII;
                        ViewBag.PaperIII = obj.PaperIII;
                        ViewBag.PaperIV = obj.PaperIV;
                        ViewBag.PaperV = obj.PaperV;
                        ViewBag.PaperVI = obj.PaperVI;
                    }
                }
            }
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
            }
            else
            {
                if (CourseYearID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion
        #region CollegeTabulationRegisterLLB
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeTabulationRegisterLLB(int page=0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
           
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;

            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.llb));

            obj = obj.GetLLBSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }            
            model.StudentListNew = model.GetTabulationRegisterLLB("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
             
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;

            }
            else
            {
                if (CourseYearID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult CollegeTabulationRegisterLLB(int id = 0, int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.llb));

            obj = obj.GetLLBSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;

            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;

            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterLLB("FullSearch", Session, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB), Convert.ToInt32(CommonSetting.coursecategory.llb), CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    model.StudentListNew = model.GetTabulationRegisterLLB("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    if (model.StudentListNew.Count()>0)
                    {
                        obj = obj.GetLLBSubject(model.StudentListNew.ToList().FirstOrDefault().CourseYearID);
                        ViewBag.PaperI = obj.PaperI;
                        ViewBag.PaperII = obj.PaperII;
                        ViewBag.PaperIII = obj.PaperIII;
                        ViewBag.PaperIV = obj.PaperIV;
                        ViewBag.PaperV = obj.PaperV;
                    }
                }
            }

      
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
            }
            else
            {
                if (CourseYearID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion
        #region CollegeTabulationRegisterBED
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult CollegeTabulationRegisterBED(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", 28);
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";

            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;       
            int pageSize1 = pagesize;
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.PaperVI = "";
            ViewBag.PaperVII = "";
            ViewBag.PaperEPC1 = "";
            ViewBag.PaperEPC2 = "";
            ViewBag.PaperEPC3 = "";

            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", 28);
            obj = obj.GetBEDSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            ViewBag.PaperVII = obj.PaperVII;
            ViewBag.PaperEPC1 = obj.PaperEPC1;
            ViewBag.PaperEPC2 = obj.PaperEPC2;
            ViewBag.PaperEPC3 = obj.PaperEPC3;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            model.StudentListNew = model.GetTabulationRegisterBED("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
            }
            else
            {
                if (CourseYearID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        [VerifyUrlFilterCollegeAttribute]
        [HttpPost]
        public ActionResult CollegeTabulationRegisterBED(int id = 0, int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", 28);

            obj = obj.GetBEDSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            ViewBag.PaperVII = obj.PaperVII;
            ViewBag.PaperEPC1 = obj.PaperEPC1;
            ViewBag.PaperEPC2 = obj.PaperEPC2;
            ViewBag.PaperEPC3 = obj.PaperEPC3;
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;         
            int pageSize1 = pagesize;
            int PageIndex = 1;
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;

            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterBED("FullSearch", Session, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBED), Convert.ToInt32(CommonSetting.coursecategory.bed), CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    model.StudentListNew = model.GetTabulationRegisterBED("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    if (model.StudentListNew.Count()>0)
                    {
                        obj = obj.GetBEDSubject(model.StudentListNew.FirstOrDefault().CourseYearID);
                        ViewBag.PaperI = obj.PaperI;
                        ViewBag.PaperII = obj.PaperII;
                        ViewBag.PaperIII = obj.PaperIII;
                        ViewBag.PaperIV = obj.PaperIV;
                        ViewBag.PaperV = obj.PaperV;
                        ViewBag.PaperVI = obj.PaperVI;
                        ViewBag.PaperVII = obj.PaperVII;
                        ViewBag.PaperEPC1 = obj.PaperEPC1;
                        ViewBag.PaperEPC2 = obj.PaperEPC2;
                        ViewBag.PaperEPC3 = obj.PaperEPC3;
                    }
                }

            }
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
            }
            else
            {
                if (CourseYearID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion
        [VerifyUrlFilterCollegeAttribute]
        public ActionResult BackStudentList()
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));

            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            //ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();

            return View();
        }
        [HttpPost]
        public ActionResult BackStudentList(string id = "")
        {
            ViewBag.CollegeName = (ClsLanguage.GetCookies("NBCollegeName") != null ? ClsLanguage.GetCookies("NBCollegeName") : "");

            string collegCode = EncriptDecript.Decrypt((ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : ""));



            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType(Convert.ToInt32(collegCode));
            //ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession sess = new AcademicSession();
            ViewBag.Session = sess.GetSession();
            var ses = sess.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollege();
            string session = Request.Form["session"];
            string CollegeID = (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) != null ? EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) : ""); ;
            string EducationTypeID = Request.Form["EducationTypeID"];
            string CourseCategoryID = Request.Form["CourseCategoryID"];
            string Subject = Request.Form["Subject"];
            string CourseYearID = Request.Form["CourseYearID"];
            string Application = Request.Form["Application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string paymentStatus = Request.Form["paymentStatus"];
            string Enrollmentno = Request.Form["enrollmentno"];
            Recruitment obj = new Recruitment();
            BackStudentList sub = new BackStudentList();
            if (EducationTypeID == "11")
            {
                sub = obj.BackStudentList(1, 10000000, CollegeID, session, CourseCategoryID, CourseYearID, EducationTypeID, Enrollmentno);
                var result = sub.qlist.Select(x => new { Row = x.Row, ApplicationNo = x.ApplicationNo, EnrollmentNo = x.EnrollmentNo, CollegeName = x.CollegeName, YearName = x.YearName, Session = x.Session, Name = x.Name, HounorsSubject = x.HounorsSubject, Subsidiary1 = x.Subsidiary1, Subsidiary2 = x.Subsidiary2, Compulsory1 = x.Compulsory1, Compulsory2 = x.Compulsory2 }).ToList();
                var gv = new GridView();
                gv.DataSource = result;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=UGBackStudentList" + System.DateTime.Now.ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            if (EducationTypeID == "13")
            {
                sub = obj.BackStudentList(1, 10000000, CollegeID, session, CourseCategoryID, CourseYearID, EducationTypeID, Enrollmentno);
                var result = sub.qlist.Select(x => new { Row = x.Row, ApplicationNo = x.ApplicationNo, EnrollmentNo = x.EnrollmentNo, CollegeName = x.CollegeName, Semester = x.YearName, Session = x.Session, Name = x.Name, BCA101 = x.Paper1, BCA102 = x.Paper2, BCA103 = x.Paper3, BCA104 = x.Paper4, BCA105 = x.Paper5, BCA106 = x.Paper6 }).ToList();
                var gv = new GridView();
                gv.DataSource = result;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=VocationalBackStudentList" + System.DateTime.Now.ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            if (EducationTypeID == "41")
            {
                sub = obj.BackStudentList(1, 10000000, CollegeID, session, CourseCategoryID, CourseYearID, EducationTypeID, Enrollmentno);
                var result = sub.qlist.Select(x => new { Row = x.Row, ApplicationNo = x.ApplicationNo, EnrollmentNo = x.EnrollmentNo, CollegeName = x.CollegeName, Semester = x.YearName, Session = x.Session, Name = x.Name, Paper1 = x.Paper1, Paper2 = x.Paper2, Paper3 = x.Paper3, Paper4 = x.Paper4, Paper5 = x.Paper5, Paper6 = x.Paper6 }).ToList();
                var gv = new GridView();
                gv.DataSource = result;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=LLBBackStudentList" + System.DateTime.Now.ToString() + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            if (EducationTypeID == "40")
            {
                sub = obj.BackStudentList(1, 10000000, CollegeID, session, CourseCategoryID, CourseYearID, EducationTypeID, Enrollmentno);
                var result = sub.qlist.Select(x => new { Row = x.Row, ApplicationNo = x.ApplicationNo, EnrollmentNo = x.EnrollmentNo, CollegeName = x.CollegeName, Semester = x.YearName, Session = x.Session, Name = x.Name, Paper1 = x.Paper1, Paper2 = x.Paper2, Paper3 = x.Paper3, Paper4 = x.Paper4, Paper5 = x.Paper5, Paper6 = x.Paper6, Paper7 = x.Paper7, Paper8 = x.Paper8, Paper9 = x.Paper9, Paper10 = x.Paper10 }).ToList();
                var gv = new GridView();
                gv.DataSource = result;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=BEdBackStudentList" + System.DateTime.Now.ToString() + ".xls");
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
        //[VerifyUrlFilterCollegeAttribute]
        public ActionResult Collegewise_CenterList()
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollege();
            return View();
        }
        [HttpPost]
        public ActionResult Collegewise_CenterList(string id = "")
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession sess = new AcademicSession();
            ViewBag.Session = sess.GetSession();
            var ses = sess.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
           // ViewBag.College = objcol.GetCollege();
            string session = Request.Form["session"];
            string CollegeID = (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) != null ? EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) : "");
            string EducationTypeID = Request.Form["program"];
            string CourseCategoryID = Request.Form["Course"];
            string Subject = Request.Form["Subject"];
            string CourseYearID = Request.Form["CourseYearID"];
            string Application = Request.Form["Application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string paymentStatus = Request.Form["paymentStatus"];
            string BackStatus = Request.Form["BackStatus"];
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            sub = obj.Collegewise_CenterList(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus);
            var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CollegeName = x.CollegeName, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Rollno = x.RollNo, StudentName = x.StudentName, CasteCategory = x.StudentCasteCategoryName, Course = x.coursecategotyName, Session = x.Session, HonoursExamcenter = x.honscenter, SubsidiaryExamCenter = x.subsidiarycenter }).ToList();
            var gv = new GridView();
            gv.DataSource = result;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Collegewise__and_studentwise_CenterList" + System.DateTime.Now.ToString() + ".xls");
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
        public ActionResult Collegewise_CenterList2()
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession session = new AcademicSession();
            ViewBag.Session = session.GetSession();
            var ses = session.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            //ViewBag.College = objcol.GetCollege();
            return View();
        }
        [HttpPost]
        public ActionResult Collegewise_CenterList2(string id = "")
        {
            BL_courseMaster objmaster = new BL_courseMaster();
            ViewBag.EducationType = objmaster.GetEducationType();
            AcademicSession sess = new AcademicSession();
            ViewBag.Session = sess.GetSession();
            var ses = sess.GetAcademiccurrentSession();
            ViewBag.CuSession = ses.ID;
            ViewBag.CourseYear = "";
            ViewBag.Course = "";
            BL_CollegeMaster objcol = new BL_CollegeMaster();
           // ViewBag.College = objcol.GetCollege();
            string session = Request.Form["session"];
            string CollegeID =  (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) != null ? EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) : "");
            string EducationTypeID = Request.Form["program"];
            string CourseCategoryID = Request.Form["Course"];
            string Subject = Request.Form["Subject"];
            string CourseYearID = Request.Form["CourseYearID"];
            string Application = Request.Form["Application"];
            string ApplicationStatus = Request.Form["ApplicationStatus"];
            string paymentStatus = Request.Form["paymentStatus"];
            string BackStatus = Request.Form["BackStatus"];
            string CenterType = Request.Form["CenterType"];
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            var gv = new GridView();
            if (EducationTypeID == "40")//b.ed
            {
                if (BackStatus == "0")//main exam
                {
                    if (CourseYearID == "28")//b.ed. part-1
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CenterName = x.honscenter, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Rollno = x.RollNo, StudentName = x.StudentName, Course = x.coursecategotyName, Session = x.Session, CollegeName = x.CollegeName, CenterType = x.subsidiarycenter, HonoursSubject = x.StreamCategoryName,
                            Paper1 = "(C-1)Childhood and Growing up  ",
                            Paper2= "(C-2)Contemporary India and Education"
                            ,
                            Paper3 = "(C-3)Learning and Teaching"
                            ,
                            Paper4="(C-4)"+ "Language across the Curriculum "
                            ,
                            Paper5 = "(C-5)" + "Understanding Disciplines and subjects"
                            ,
                            Paper6 = "(C-6)" + "Gender,School and Society "

                            ,                            Paper7_a = "(C-7(a))" + x.electivesubjectname,

                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                    if (CourseYearID == "29")//b.ed. part-2
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CenterName = x.honscenter, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Rollno = x.RollNo, StudentName = x.StudentName, Course = x.coursecategotyName, Session = x.Session, CollegeName = x.CollegeName, CenterType = x.subsidiarycenter, HonoursSubject = x.StreamCategoryName,
                            Paper8 = "(C-8)Knowledge and Curriculum ",
                            Paper9 = "(C-9)Assessment for Learning"
                            ,Paper10 = "(C-10)Creating and Inclusive School"
                            ,Paper11 = "(C-11)" + x.electivesubjectname
                            ,
                            Paper7_b = "(C-7(b))" + x.electivesubjectname_2,
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                }
                if (BackStatus =="1")// back exam
                {
                    if (CourseYearID == "28")//b.ed. part-1
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CenterName = x.honscenter, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Rollno = x.RollNo, StudentName = x.StudentName, Course = x.coursecategotyName, Session = x.Session, CollegeName = x.CollegeName, CenterType = x.subsidiarycenter, HonoursSubject = x.StreamCategoryName,
                            Paper1=x.Paper1
                             ,Paper2 = x.Paper2
                             ,
                            Paper3 = x.Paper3
                            ,
                            Paper4 = x.Paper4
                            ,
                            Paper5 = x.Paper5
                            ,
                            Paper6 = x.Paper6
                            ,
                            Paper7 = x.Paper7
                            ,
                            Paper8 = x.Paper8
                            ,
                            Paper9= x.Paper9
                            ,
                            Paper10 = x.Paper10

                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                    if (CourseYearID == "29")//b.ed. part-2
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CenterName = x.honscenter, ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Rollno = x.RollNo, StudentName = x.StudentName, Course = x.coursecategotyName, Session = x.Session, CollegeName = x.CollegeName, CenterType = x.subsidiarycenter, HonoursSubject = x.StreamCategoryName,
                            Paper1 = x.Paper1
                             ,
                            Paper2 = x.Paper2
                             ,
                            Paper3 = x.Paper3
                            ,
                            Paper4 = x.Paper4
                            ,
                            Paper5 = x.Paper5
                            ,
                            Paper6 = x.Paper6
                            ,
                            Paper7 = x.Paper7
                            ,
                            Paper8 = x.Paper8
                            ,
                            Paper9 = x.Paper9
                            ,
                            Paper10 = x.Paper10
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                }

                

           
            }
            else if (EducationTypeID == "41")//l.l.b.
            {
                if (BackStatus == "0")//main exam
                {
                    if (CourseYearID == "30")//semester. part-1 llb
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = "Jurisprudence ",
                            Paper2 = "Constitutional law "
                            ,
                            Paper3 = "Law of Contract	"
                            ,
                            Paper4 = "Maritime Law (Optional) "
                            ,
                            Paper5 = "General English"


                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                    if (CourseYearID == "31")//l.lb semester -2
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID,
                            Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new
                        {

                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = "Law of Crime( Indian Panel court) ",
                            Paper2 = "Constitutional Law-II",
                            Paper3 = "Family Law (Hindu Law)",
                            Paper4 = "Women and Crime Law(Optional) ",
                            Paper5 = "Legal Language"

                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }

                    if (CourseYearID == "32")//l.lb semester -3
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = "Muslim Law ",
                            Paper2 = "Property  law ",
                            Paper3 = "Law of Torts, M.V. Accident and Consumer Protection Law",
                            Paper4 = "Banking Law(Optional)",
                            Paper5 = "Professional Ethics and Professional A/C System (Practical)"

                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                    if (CourseYearID == "33")//l.lb semester -4
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = "CR.P.C.",
                            Paper2 = "Public International law",
                            Paper3 = "Company Law",
                            Paper4 = "International Humanitarian Law",
                            Paper5 = ""

                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                }
                if (BackStatus == "1")// back exam
                {
                    if (CourseYearID == "30" || CourseYearID == "31") //l.l.b. semeter - 1
                    {
                        sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = x.Paper1
                             ,
                            Paper2 = x.Paper2
                             ,
                            Paper3 = x.Paper3
                            ,
                            Paper4 = x.Paper4
                            ,
                            Paper5 = x.Paper5
                            
                          

                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();
                    }
                    
                }




            }
            else if (EducationTypeID == "12")//P.G.
            {
                if (BackStatus == "0")//main exam
                {
                    if (CourseYearID == "17" || CourseYearID == "27" || CourseYearID == "25" )//p.g.. semeter - 2
                    {

                        var subjectlist = obj.sp_Collegewise_CenterList_subejctlist(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        sub = obj.sp_Collegewise_CenterList_rollnowise_new(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new
                        {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 1).FirstOrDefault().SubjectName,
                            Paper2 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 2).FirstOrDefault().SubjectName,
                            Paper3 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 3).FirstOrDefault().SubjectName,
                            Paper4 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 4).FirstOrDefault().SubjectName,
                            Paper5 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 5).FirstOrDefault().SubjectName,
                            ElectivePaper = x.electivesubjectname
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();

                    }
                    if (CourseYearID == "16" || CourseYearID == "26" || CourseYearID == "24")//p.g.. semeter - 1
                    {

                        var subjectlist = obj.sp_Collegewise_CenterList_subejctlist(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        sub = obj.sp_Collegewise_CenterList_rollnowise_new(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new
                        {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 1).FirstOrDefault().SubjectName,
                            Paper2 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 2).FirstOrDefault().SubjectName,
                            Paper3 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 3).FirstOrDefault().SubjectName,
                            Paper4 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 4).FirstOrDefault().SubjectName,
                             ElectivePaper = x.electivesubjectname
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();

                    }
                }
                if (BackStatus == "1")// back exam
                {
                    if (CourseYearID == "16" || CourseYearID == "26" || CourseYearID == "24")//p.g.. semeter - 1
                    {

                        var subjectlist = obj.sp_Collegewise_CenterList_subejctlist(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        sub = obj.sp_Collegewise_CenterList_rollnowise_new(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new
                        {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = x.Paper1,
                            Paper2 = x.Paper2,
                            Paper3 = x.Paper3,
                            Paper4 = x.Paper4,
                            Paper5 = x.Paper5,
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();

                    }
                }
            }
            else if (EducationTypeID == "13")//BCA
            {
                if (BackStatus == "0")//main exam
                {
                    if (CourseYearID == "10" || CourseYearID == "11" || CourseYearID == "12" ||CourseYearID == "13" || CourseYearID == "14" || CourseYearID == "15")//bca
                    {

                        var subjectlist = obj.sp_Collegewise_CenterList_subejctlist(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        sub = obj.sp_Collegewise_CenterList_rollnowise_new(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new
                        {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 1).FirstOrDefault().SubjectName+" " + subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 1).FirstOrDefault().subjectcode,
                            Paper2 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 2).FirstOrDefault().SubjectName + " " + subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 2).FirstOrDefault().subjectcode,
                            Paper3 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 3).FirstOrDefault().SubjectName + " " + subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 3).FirstOrDefault().subjectcode,
                            Paper4 = subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 4).FirstOrDefault().SubjectName + " " + subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 4).FirstOrDefault().subjectcode,
                            Paper5 = (x.courseyearid == 10 ? subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 5).FirstOrDefault().SubjectName : (x.courseyearid == 11 ? subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 5).FirstOrDefault().SubjectName+" "+ subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 5).FirstOrDefault().subjectcode : (x.courseyearid == 12 ? subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 5).FirstOrDefault().SubjectName+" "+ subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 5).FirstOrDefault().subjectcode : ""))),
                             Paper6 = (x.courseyearid == 10?subjectlist.Where(R => R.EducationType == x.educationtype && R.coursecategoryid == x.coursecategoryid && R.subjecttype == 1 && R.courseyearid == x.courseyearid && R.Type == "Honours" && R.StreamCategoryID == x.StreamCategoryID && R.SubjectCodeID == 6).FirstOrDefault().SubjectName:""),
                            // ElectivePaper = x.electivesubjectname
                            // Paper655=
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();

                    }
                    
                }
                if (BackStatus == "1")// back exam
                {
                    if (CourseYearID == "10" || CourseYearID == "11" || CourseYearID == "12" || CourseYearID == "13" || CourseYearID == "14" || CourseYearID == "15")//bca
                    {

                        var subjectlist = obj.sp_Collegewise_CenterList_subejctlist(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        sub = obj.sp_Collegewise_CenterList_rollnowise_new(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                        var result = sub.qlist.Select(x => new
                        {
                            ExamType = x.ExamStatus,
                            CenterName = x.honscenter,
                            ApplicationNo = x.ApplicationNo,
                            RegistrationNo = x.EnrollmentNo,
                            Rollno = x.RollNo,
                            StudentName = x.StudentName,
                            Course = x.coursecategotyName,
                            Session = x.Session,
                            CollegeName = x.CollegeName,
                            CenterType = x.subsidiarycenter,
                            HonoursSubject = x.StreamCategoryName,
                            Paper1 = x.Paper1,
                            Paper2 = x.Paper2,
                            Paper3 = x.Paper3,
                            Paper4 = x.Paper4,
                            Paper5 = x.Paper5,
                            Paper6 = x.Paper6,
                        }).ToList();
                        gv.DataSource = result;
                        gv.DataBind();

                    }
                }
            }
            else
            {
                sub = obj.sp_Collegewise_CenterList_rollnowise(1, 10000000, CollegeID, session, EducationTypeID, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
                var result = sub.qlist.Select(x => new { ExamType = x.ExamStatus, CenterName = ClsLanguage.GetCookies("NBCollegeName"), ApplicationNo = x.ApplicationNo, RegistrationNo = x.EnrollmentNo, Rollno = x.RollNo, StudentName = x.StudentName, Course = x.coursecategotyName, Session = x.Session, CollegeName = x.CollegeName, CenterType = x.subsidiarycenter, HonoursSubject = x.StreamCategoryName, SubsidiarySubject1 = x.Subsidiary1, SubsidiarySubject2 = x.Subsidiary2, CompositionSubject1 = x.Compulsory1, CompositionSubject2 = x.Compulsory2 }).ToList();
                gv.DataSource = result;
                gv.DataBind();
            }
            
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Collegewise__and_studentwise_CenterList2" + System.DateTime.Now.ToString() + ".xls");
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
        #region GetCollegeTabulationRegister UG BACk
      //  [VerifyUrlFilterAdminAttribute]
        public ActionResult CollegeTabulationRegisterUGBACK(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0,  int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollegeExam();
            ViewBag.Session = session.GetSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationTypeUG");
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";

            //ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            //if (CourseCategoryID > 0)
            //{
            ViewBag.coursesfordrop = com.getcommonMaster("Course", Convert.ToInt32(DataLayer.CommonSetting.Commonid.Educationtype));
            //}
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = "";// com.getcommonMaster("yearDrop");
            if (CourseYearID > 0)
            {
                ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", CourseCategoryID);
            }
            if (HonoursSubjectID > 0)
            {

                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;

            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            if (ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.BA3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bsc3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bcom3rd))
            {
                model.StudentListNew = model.GetTabulationRegisterUGBack3rdyear("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                model.StudentListNew = model.GetTabulationRegisterUGBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }

            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;

            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.Select(x => x.Collegename).FirstOrDefault();
                ViewBag.Coursename = model.StudentListNew.Select(x => x.CourseCategoryName).FirstOrDefault();
                ViewBag.CourseYearname = model.StudentListNew.Select(x => x.YearName).FirstOrDefault();
                ViewBag.sessionname = model.StudentListNew.Select(x => x.SessionName).FirstOrDefault();

            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult CollegeTabulationRegisterUGBACK(int id = 0, int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.College = objcol.GetCollegeExam();
            ViewBag.Session = session.GetSession();
            ViewBag.Educationtype = com.getcommonMaster("EducationTypeUG");
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            //ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            //if (CourseCategoryID > 0)
            //{
            ViewBag.coursesfordrop = com.getcommonMaster("Course", Convert.ToInt32(DataLayer.CommonSetting.Commonid.Educationtype));
            //}
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = "";//com.getcommonMaster("yearDrop");
            if (CourseYearID > 0)
            {
                ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", CourseCategoryID);
            }
            if (HonoursSubjectID > 0)
            {

                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize = pagesize;
            int PageIndex = 1;
            PageIndex = page != 0 ? Convert.ToInt32(page) : 1;
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            if (ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.BA3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bsc3rd) || ViewBag.YearID == Convert.ToInt32(DataLayer.CommonSetting.CourseYearID.Bcom3rd))
            {
                if (searchType == false)
                {
                    model.StudentListNew = model.GetTabulationRegisterUGBack3rdyear("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
                }
                else
                {
                    if (enrollmentno != "" || ApplicationNo != "")
                    {
                        model.StudentListNew = model.GetTabulationRegisterUGBack3rdyear("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    }
                }
            }
            else
            {
                if (searchType == false)
                {
                    model.StudentListNew = model.GetTabulationRegisterUGBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
                }
                else
                {
                    if (enrollmentno != "" || ApplicationNo != "")
                    {
                        model.StudentListNew = model.GetTabulationRegisterUGBack("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    }
                }

            }
            //var studentList = model.GetTabulationRegister(Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID,HonoursSubjectID);
            //StudentListobj = studentList.ToPagedList(PageIndex, pageSize);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;

            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;

            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();

            ViewBag.HonoursSubjectID = HonoursSubjectID;
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.Select(x => x.Collegename).FirstOrDefault();
                ViewBag.Coursename = model.StudentListNew.Select(x => x.CourseCategoryName).FirstOrDefault();
                ViewBag.CourseYearname = model.StudentListNew.Select(x => x.YearName).FirstOrDefault();
                ViewBag.sessionname = model.StudentListNew.Select(x => x.SessionName).FirstOrDefault();



            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }


            return View(model);
        }

        #endregion
        #region CollegeTabulationRegisterBCABack
        //[VerifyUrlFilterAdminAttribute]
        public ActionResult CollegeTabulationRegisterBCAback(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            ViewBag.showdiv = 1;
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(Convert.ToInt32(CommonSetting.coursecategory.bca));
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.bca));
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;

            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.bca));

            obj = obj.GetBCASubjectCode(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            model.StudentListNew = model.GetTabulationRegisterBCABack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);

            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;

            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult CollegeTabulationRegisterBCAback(int id = 0, int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(Convert.ToInt32(CommonSetting.coursecategory.bca));
            ViewBag.coursesfordrop = com.getcommonMaster("coursesfordrop");
            if (CourseCategoryID > 0)
            {
                ViewBag.coursesfordrop = com.getcommonMaster("Course", EduTypeID);
            }
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CommonSetting.coursecategory.bca));

            obj = obj.GetBCASubjectCode(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;

            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.showdiv = 1;

            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterBCABack("FullSearch", Session, 13, Convert.ToInt32(CommonSetting.coursecategory.bca), CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                ViewBag.showdiv = 0;
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    model.StudentListNew = model.GetTabulationRegisterBCABack("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);
                    if (model.StudentListNew.Count() > 0)
                    {
                        obj = obj.GetBCASubjectCode(model.StudentListNew.ToList().FirstOrDefault().CourseYearID);
                        ViewBag.PaperI = obj.PaperI;
                        ViewBag.PaperII = obj.PaperII;
                        ViewBag.PaperIII = obj.PaperIII;
                        ViewBag.PaperIV = obj.PaperIV;
                        ViewBag.PaperV = obj.PaperV;
                        ViewBag.PaperVI = obj.PaperVI;
                    }
                }
            }
            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }

        #endregion
        #region CollegeTabulationRegisterBEdBack
        //[VerifyUrlFilterAdminAttribute]
        public ActionResult CollegeTabulationRegisterBEDBack(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            ViewBag.showdiv = 1;
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(0, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBED));

            //if (CourseCategoryID > 0)
            //{
            //    ViewBag.coursesfordrop = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            //}
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.CourseCategoryIDs = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypeBED));
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;

            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            //obj = obj.GetPGSubject(CourseYearID);
            //ViewBag.PaperI = obj.PaperI;
            //ViewBag.PaperII = obj.PaperII;
            //ViewBag.PaperIII = obj.PaperIII;
            //ViewBag.PaperIV = obj.PaperIV;
            //ViewBag.PaperV = obj.PaperV;
            //ViewBag.PaperVI = obj.PaperVI;
            model.StudentListNew = model.GetTabulationRegisterBEdBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);

            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = 0;// session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;

            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult CollegeTabulationRegisterBEDBack(int id = 0, int page = 0, int Session = 0, int EduTypeID = 40, int CourseCategoryID = 0,  int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(0, Convert.ToInt32(CommonSetting.Commonid.EducationtypeBED));
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            ViewBag.CourseCategoryIDs = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypeBED));
            obj = obj.GetBEDSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            ViewBag.PaperVII = obj.PaperVII;
            ViewBag.PaperEPC1 = obj.PaperEPC1;
            ViewBag.PaperEPC2 = obj.PaperEPC2;
            ViewBag.PaperEPC3 = obj.PaperEPC3;
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = CourseYearID;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.showdiv = 1;

            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterBEdBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                ViewBag.showdiv = 0;
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    model.StudentListNew = model.GetTabulationRegisterBEdBack("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);

                }
            }
            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = 0;//;session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
                ViewBag.streamCategoryName = model.StudentListNew.FirstOrDefault().streamCategoryName;
            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion
        #region CollegeTabulationRegisterPGBack
       // [VerifyUrlFilterAdminAttribute]
        public ActionResult CollegeTabulationRegisterPGBack(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            ViewBag.showdiv = 1;
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(0, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));

            //if (CourseCategoryID > 0)
            //{
            //    ViewBag.coursesfordrop = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            //}
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.CourseCategoryIDs = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }

            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            //obj = obj.GetPGSubject(CourseYearID);
            //ViewBag.PaperI = obj.PaperI;
            //ViewBag.PaperII = obj.PaperII;
            //ViewBag.PaperIII = obj.PaperIII;
            //ViewBag.PaperIV = obj.PaperIV;
            //ViewBag.PaperV = obj.PaperV;
            //ViewBag.PaperVI = obj.PaperVI;
            model.StudentListNew = model.GetTabulationRegisterPGBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);

            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = 0;// session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;

            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult CollegeTabulationRegisterPGBack(int id = 0, int page = 0, int Session = 0, int EduTypeID = 12, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(0, Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            ViewBag.CourseCategoryIDs = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            obj = obj.GetPGSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = CourseYearID;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.showdiv = 1;

            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterPGBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                ViewBag.showdiv = 0;
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    model.StudentListNew = model.GetTabulationRegisterPGBack("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);

                }
            }
            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = 0;//;session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
                ViewBag.streamCategoryName = model.StudentListNew.FirstOrDefault().streamCategoryName;
            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion

        #region CollegeTabulationRegisterLLBBack
        //[VerifyUrlFilterAdminAttribute]
        public ActionResult CollegeTabulationRegisterLLBBack(int page = 0, int Session = 0, int EduTypeID = 0, int CourseCategoryID = 0, int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            ViewBag.showdiv = 1;
            obj = model.DateMonthYear(model);
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(0, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));

            //if (CourseCategoryID > 0)
            //{
            //    ViewBag.coursesfordrop = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypePG));
            //}
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.CourseCategoryIDs = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            ViewBag.CourseYearID = 0;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;

            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.PaperI = "";
            ViewBag.PaperII = "";
            ViewBag.PaperIII = "";
            ViewBag.PaperIV = "";
            ViewBag.PaperV = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            int CollegeID = 0;
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            ViewBag.collegeid = CollegeID;
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            //obj = obj.GetPGSubject(CourseYearID);
            //ViewBag.PaperI = obj.PaperI;
            //ViewBag.PaperII = obj.PaperII;
            //ViewBag.PaperIII = obj.PaperIII;
            //ViewBag.PaperIV = obj.PaperIV;
            //ViewBag.PaperV = obj.PaperV;
            //ViewBag.PaperVI = obj.PaperVI;
            model.StudentListNew = model.GetTabulationRegisterLLBBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);

            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = 0;// session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;

            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }


            return View(model);
        }
        [HttpPost]
        public ActionResult CollegeTabulationRegisterLLBBack(int id = 0, int page = 0, int Session = 0, int EduTypeID = 41, int CourseCategoryID = 0,  int CourseYearID = 0, int pagesize = 50, int HonoursSubjectID = 0, string enrollmentno = "", string ApplicationNo = "", bool searchType = false)
        {
            CollegeExamCenter model = new CollegeExamCenter();
            AcademicSession session = new AcademicSession();
            Commn_master com = new Commn_master();
            BL_courseMaster objCourse = new BL_courseMaster();
            BL_StreamMaster objSubject = new BL_StreamMaster();
            BL_CollegeMaster objcol = new BL_CollegeMaster();
            ViewBag.Session = session.GetSession();
            CollegeExamCenter obj = new CollegeExamCenter();
            obj = model.DateMonthYear(model);
            int CollegeID = 0;
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(encollegeID));
            }
            ViewBag.collegeid = CollegeID;
            ViewBag.MonthName = obj.MonthName;
            ViewBag.CurrentYearName = obj.CurrentYearName;
            ViewBag.CurrentDate = obj.CurrentDate;
            ViewBag.College = objcol.GetCollegeExam(0, Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            ViewBag.eduid = 0;
            ViewBag.CourseCategoryID = 0;
            ViewBag.CourseCategoryIDs = "";
            ViewBag.StreamCategoryIDs = "";
            ViewBag.Subject = "";
            ViewBag.sessionname = "";
            ViewBag.CourseYearname = "";
            ViewBag.Coursename = "";
            ViewBag.StreamCategoryID = 0;
            ViewBag.CourseYear = com.getcommonMaster("yearDrop");
            ViewBag.CourseYear = com.getcommonMaster("yearDropbyid", Convert.ToInt32(CourseCategoryID));
            ViewBag.CourseCategoryIDs = com.getcommonMaster("Course", Convert.ToInt32(CommonSetting.Commonid.EducationtypeLLB));
            obj = obj.GetLLBSubject(CourseYearID);
            ViewBag.PaperI = obj.PaperI;
            ViewBag.PaperII = obj.PaperII;
            ViewBag.PaperIII = obj.PaperIII;
            ViewBag.PaperIV = obj.PaperIV;
            ViewBag.PaperV = obj.PaperV;
            ViewBag.PaperVI = obj.PaperVI;
            if (HonoursSubjectID > 0)
            {
                BL_StreamMaster blstream = new BL_StreamMaster();
                ViewBag.Subject = blstream.getsubjectbycourse(18, CourseCategoryID, CollegeID);
            }
            ViewBag.CourseYearID = CourseYearID;
            ViewBag.HonourseNames = "";
            ViewBag.HonourseNamesID = 0;
            IPagedList<CollegeExamCenter> StudentListobj = null;
            int pageSize1 = pagesize;
            int PageIndex = 1;
            List<CollegeExamCenter> objList = new List<CollegeExamCenter>();
            List<CollegeExamCenter> studentList = new List<CollegeExamCenter>();
            ViewBag.Collegename = "";
            ViewBag.educationtypeID1 = EduTypeID;
            ViewBag.coursecategoryID = CourseCategoryID;
            ViewBag.collegeid = CollegeID;
            ViewBag.yearid = CourseYearID;
            ViewBag.page = page;
            ViewBag.sessionID = Session;
            ViewBag.pagesize = pageSize1;
            ViewBag.enrollmentno = enrollmentno;
            ViewBag.HonoursSubjectID = HonoursSubjectID;
            ViewBag.ApplicationNo = ApplicationNo;
            ViewBag.searchType = searchType;
            ViewBag.showdiv = 1;

            if (searchType == false)
            {
                model.StudentListNew = model.GetTabulationRegisterLLBBack("FullSearch", Session, EduTypeID, CourseCategoryID, CollegeID, CourseYearID, HonoursSubjectID, enrollmentno, ApplicationNo);
            }
            else
            {
                ViewBag.showdiv = 0;
                if (enrollmentno != "" || ApplicationNo != "")
                {
                    model.StudentListNew = model.GetTabulationRegisterLLBBack("CustomSearch", 0, 0, 0, 0, 0, 0, enrollmentno, ApplicationNo);

                }
            }
            StudentListobj = studentList.ToPagedList(PageIndex, pageSize1);
            if (Session != 0)
            {
                ViewBag.CurrentSession = Session;
            }
            else
            {
                ViewBag.CurrentSession = 0;//;session.GetAcademiccurrentSession().ID;
            }
            model.Session = ViewBag.CurrentSession;
            model.PageIndex = page;
            model.PageSize = pagesize;
            model.totalCount = model.StudentListNew.Count;
            int startIndex = (page - 1) * model.PageSize;
            model.StudentListNew = model.StudentListNew.Skip(startIndex).Take(model.PageSize).ToList();
            if (model.StudentListNew.Count() > 0)
            {
                ViewBag.result = model.StudentListNew;
                ViewBag.totalCount = model.StudentListNew.Count;
                ViewBag.Collegename = model.StudentListNew.FirstOrDefault().Collegename;
                ViewBag.Coursename = model.StudentListNew.FirstOrDefault().CourseCategoryName;
                ViewBag.CourseYearname = model.StudentListNew.FirstOrDefault().YearName;
                ViewBag.sessionname = model.StudentListNew.FirstOrDefault().SessionName;
                ViewBag.streamCategoryName = model.StudentListNew.FirstOrDefault().streamCategoryName;
            }
            else
            {
                if (CollegeID != 0)
                {
                    ViewBag.totalCount = 0;
                }
                ViewBag.result = null;
            }
            return View(model);
        }
        #endregion
        #region CollegeTabulationRegisterList       
        public ActionResult __CollegeTabulationRegisterList(string Id = "", int sessionID = 0, int educationtype = 0, int coursecategoryID = 0, int collegeid = 0, int yearid = 0, int page = 0)
        {
            return View("__CollegeTabulationRegisterList");
        }
        #endregion
    }
}
