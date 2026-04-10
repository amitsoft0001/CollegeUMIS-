using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Website.Models;

namespace Website.Areas.college.Models
{
    public class VerifyUrlFilterCollegeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            var list = verifyurlcheck();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] viewname = url.Split('/');
            if (viewname.Length > 4)
            {
                var viewname1 = "";
                List<string> _menus = new List<string>();
                if (viewname.Length == 5)
                {                   
                    viewname1 = "Index";
                }
                else
                {
                    viewname1 = viewname[5];
                }
                if (viewname1.ToLower() == "DocumentVerify".ToLower())
                {
                    viewname1 = "DocumentVerifyList";
                }
                if (viewname1.ToLower() == "PendingLeaveDetail".ToLower())
                {
                    viewname1 = "VerifyLeave";
                }
                if (viewname1.ToLower() == "StudentSubjectsEdit".ToLower())
                {
                    viewname1 = "FinalStudentList";
                }
                if (viewname1.ToLower() == "StudentDetailsEdit".ToLower())
                {
                    viewname1 = "FinalStudentList";
                }
                if (viewname1.ToLower() == "PreviousYearQualificationManualAd".ToLower())
                {
                    viewname1 = "ManualAdmission";
                }
                if (viewname1.ToLower() == "PreviousyearQualificationO".ToLower())
                {
                    viewname1 = "ManualAdmission";
                }
                if (viewname1.ToLower() == "ChosseSubjectsmanualad".ToLower())
                {
                    viewname1 = "ManualAdmission";
                }
                if (viewname1.ToLower() == "FeeSubmitManualAd".ToLower())
                {
                    viewname1 = "ManualAdmission";
                }
                if (viewname1.ToLower() == "StudentQualification".ToLower())
                {
                    viewname1 = "ManualAdmission";
                }                
                if (viewname1.ToLower() == "StudentSubjectsEdit".ToLower())
                {
                    viewname1 = "DocumentVerifyList";
                }
                if (viewname1.ToLower() == "StudentDetailsEdit".ToLower())
                {
                    viewname1 = "DocumentVerifyList";
                }
                if (viewname1.ToLower() == "EmployeeDocumentUpload".ToLower())
                {
                    viewname1 = "EmployeeList";
                }
                if (viewname1.ToLower() == "DocumentListEmployee".ToLower())
                {
                    viewname1 = "EmployeeList";
                }
                if (viewname1.ToLower() == "FeeSubmitVocational".ToLower())
                {
                    viewname1 = "DocumentVerifyList";
                }
                if (viewname1.ToLower() == "EmployeeInformationDetail".ToLower())
                {
                    viewname1 = "EmployeeInformation";
                }
                if (viewname1.ToLower() == "EmployeeInformationVerify".ToLower())
                {
                    viewname1 = "EmployeeInformation";
                }
                if (viewname1.ToLower() == "ExamFormVerify".ToLower())
                {
                    viewname1 = "ExamFormList";
                }
                if (viewname1.ToLower() == "ExamFormVerifyback".ToLower())
                {
                    viewname1 = "ExamFormList";
                }
                if (viewname1.ToLower() == "ExamFormVerifyUG".ToLower())
                {
                    viewname1 = "ExamFormList";
                }
                if (viewname1.ToLower() == "ExamFormVerifyBackUG".ToLower())
                {
                    viewname1 = "ExamFormList";
                }
                if (viewname1.ToLower() == "IncomeCertificateVerify".ToLower())
                {
                    viewname1 = "DocumentVerifyList";
                }
                if (viewname1.ToLower() == "MigrationCertificateVerify".ToLower())
                {
                    viewname1 = "DocumentVerifyList";
                }
                if (viewname1.ToLower() == "CollegeFormVerify".ToLower())
                {
                    viewname1 = "CollegeFormList";
                }
                if (viewname1.ToLower() == "clcallot".ToLower())
                {
                    viewname1 = "clcearch";
                }
                if (viewname1.ToLower() == "tcallot".ToLower())
                {
                    viewname1 = "clcearch";
                }
                
                list = list.Where(m => m != "").ToList();
                if(list.Count==0)
                {
                    filterContext.Result = new RedirectResult("~/College/Home/Index");
                }
                // _menus = list.Where(m => viewname1.ToLower().Contains(m.ToString().ToLower())).ToList();
                _menus = list.Where(m => m.ToString().ToLower().StartsWith(viewname1.ToLower())).ToList();

                if (_menus.Count == 0)
                {
                    filterContext.Result = new RedirectResult("~/College/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
        public List<string> verifyurlcheck()
        {           
            List<string> objallowedURLS = new List<string>();
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Request.Cookies["ENNBUID"] != null)
            {
                IEnumerable<College_MenuMaster> Menu = null;
                UserLogin obj = new UserLogin();
                var id = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
                obj = obj.userdata(id);
                College_MenuMaster menu = new College_MenuMaster();
                Menu = menu.getCollegeMenuList("CollegeViewAll").ToList();
                if (obj.menustr == null || obj.menustr == "")
                {
                    obj.menustr = ",";
                }
                string[] values = obj.menustr.Split(',');
                List<College_MenuMaster> i = Menu.Where(m => values.Contains(m.MenuID.ToString())).ToList();
                var list = i;
                foreach (DataLayer.College_MenuMaster item in list as List<DataLayer.College_MenuMaster>)
                {
                    objallowedURLS.Add(item.viewnamename);
                }
                objallowedURLS.Add("Index");
                objallowedURLS.Add("Dashboard");
                objallowedURLS.Add("Changepassword");
                objallowedURLS.Add("UpdateUserProfile");
                objallowedURLS.Add("ViewProfile");
                objallowedURLS.Add("AddStudentAttendance");
                objallowedURLS.Add("StudentAttendanceDetail");
                objallowedURLS.Add("EmployeeSubjectAssign");
                objallowedURLS.Add("ApplyLeave");
                objallowedURLS.Add("CheckLeaveStatus");
                //ApplyLeave
                //CheckLeaveStatus
            }
            return objallowedURLS;
        }
    }
}
