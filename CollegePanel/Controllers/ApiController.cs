
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
using Website.Models;

namespace Website.Controllers
{
   [CustomApiAuthentication]
    
    public class AngularApiController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/get-student-list")]
        public BL_StudentList StudentList(int pageIndex1, int pageSize1, string ApplicationNo, string session, int IsFeeSubmit = 0, string name = "", int CourseCategory = 0, int Subject = 0,  string fromdate = "", string todate = "", string EducationType = "")
        {
            BL_StudentList obj = new BL_StudentList();
            BL_StudentList objStudentList = new BL_StudentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            objStudentList = obj.StudentList(pageIndex1, pageSize1, ApplicationNo, session, IsFeeSubmit, name, CourseCategory, Subject, collegeID, fromdate, todate, EducationType);
            for (int i = 0; i < objStudentList.qlist.Count; i++)
            {
                objStudentList.qlist[i].EncriptedID = EncriptDecript.Encrypt(objStudentList.qlist[i].Id.ToString());
            }
            return objStudentList;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/studentList")]
        public RecruitmentList studentList(int pageSize, int pageIndex,string coursetype="", string subject = "", string session = "",  string cast = "", string seatType = "",string application="",string ApplicationStatus="",string CounsellingNo="")
        {
             Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.studentdetailList(pageIndex, pageSize, coursetype , subject ,  session , collegeID, cast ,  seatType, application,  ApplicationStatus, CounsellingNo);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/recruitmentList")]
        public RecruitmentList RecruitmentList(int pageSize, int pageIndex, string coursetype = "", string subject = "", string session = "", string collegeID1 = "", string cast = "", string seatType = "", string Applicationno = "", string CounsellingNo = "", string EducationTypeID = "")
        {
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            sub = obj.RecruitmentdetailList(pageIndex, pageSize, coursetype, subject, session, collegeID, cast, seatType, Applicationno, CounsellingNo, EducationTypeID);
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/get-collegeseat-list")]
        public Bl_SeatList SeatListcollege(int pageSize, int pageIndex, int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0"/*, string collegeID = "0"*/)
        {
            string collegeID = "";
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            Bl_SeatMater obj = new Bl_SeatMater();
            Bl_SeatList sub = new Bl_SeatList();
            sub = obj.GetseatListCollege("Couserseatview", pageIndex, pageSize, Id, courseid, programid, sessionid, admissionid, collegeID);
            return sub;
        }
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/studentListForVerified")]
        public RecruitmentList studentListForVerified(int pageSize, int pageIndex, string coursetype = "", string subject = "", string session = "", string cast = "", string seatType = "", string application = "", string ApplicationStatus = "", string CounsellingNo = "",string FeeStatus="",string IncomeStatus="",string varEducationtype="")
        {
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.studentdetailListForVerification(pageIndex, pageSize, coursetype, subject, session, collegeID, cast, seatType, application, ApplicationStatus, CounsellingNo, FeeStatus, IncomeStatus,varEducationtype);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
                sub.qlist[i].migrationcertificate = (sub.qlist[i].migrationcertificate==null?"": sub.qlist[i].migrationcertificate);
            }
            return sub;
        }

       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/finalstudentList")]
        public RecruitmentList studentListafterfeeSubmit(int pageSize, int pageIndex, string coursetype = "", string subject = "", string session = "", string cast = "", string seatType = "", string application = "", /*string ApplicationStatus = "",*/ string CounsellingNo = "", string paymentStatus = "", string CourseYearID = "",string EducationType = "",string Registration="")
        {
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.feesubmittedstudentdetailList(pageIndex, pageSize, coursetype, subject, session, collegeID, cast, seatType, application, /*ApplicationStatus,*/ CounsellingNo, paymentStatus, CourseYearID, EducationType, Registration );
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;

        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/collegeuserList")]
        public UserList collegeuserList(int pageIndex, int pageSize , string MobileNo = "", string Email = "")
        {
            UserLogin obj = new UserLogin();
            int usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            UserList sub = new UserList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.UserdetailList(pageIndex, pageSize, usertype,  MobileNo, Email, collegeID);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/collegeempList")]
        public EmployeeRegisterationList collegeEmpList(int pageSize, int pageIndex, string MobileNo = "", string Email = "",string Designation="",string FacultyType="",string Code="",string Name="")
        {
            EmployeeRegisteration obj = new EmployeeRegisteration();
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));               
            }
            EmployeeRegisterationList sub = new EmployeeRegisterationList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var ID = 0;
            var insertby = (ClsLanguage.GetCookies("ENNBUID")!=null? ClsLanguage.GetCookies("ENNBUID") : "");
            if (insertby != "")
            {
                ID = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
            }
            sub = obj.EmployeedetailList(pageIndex, pageSize,  MobileNo, Email, collegeID, Designation , FacultyType, Code,Name, usertype,ID);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                
                sub.qlist[i].EnEID = EncriptDecript.Encrypt(sub.qlist[i].EID.ToString());
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].Id.ToString());
            }
            return sub;

        }
        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empdocList")]
        public Employee_DocumentUploadList empdocList( int pageIndex, int pageSize, string Document = "", string Mobile = "" , string EmployeeName="")
        {

            Employee_DocumentUpload obj = new Employee_DocumentUpload();

            Employee_DocumentUploadList sub = new Employee_DocumentUploadList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.EmployeedocList(pageIndex, pageSize,  Document, Mobile, EmployeeName, collegeID);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }

        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/leaveDetailList")]
        public EmployeeLeave_ManagementList leaveDetailList(int pageIndex, int pageSize, string Month = "", string Year = "")
        {
           
            
            EmployeeLeave_Management obj = new EmployeeLeave_Management();

            EmployeeLeave_ManagementList sub = new EmployeeLeave_ManagementList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            string EID = "";
            if (ClsLanguage.GetCookies("ENNBUID") != null)
            {
                EID = EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID"));

            }
            sub = obj.EmployeeLeaveList(pageIndex, pageSize, Month, Year,EID, collegeID);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }

      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/leaveList")]
        public EmployeeLeave_ManagementList leaveList(int pageIndex, int pageSize, string Month = "", string Year = "",string Name="",string LeaveStatus="")
        {
            EmployeeLeave_Management obj = new EmployeeLeave_Management();
            EmployeeLeave_ManagementList sub = new EmployeeLeave_ManagementList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            
            sub = obj.EmployeePendingLeaveList(pageIndex, pageSize,  collegeID, Month , Year,Name, LeaveStatus);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/paymentdetailList")]
        public BL_PrintReciptList paymentdetailList(int pageSize, int pageIndex, string fromdate = "", string todate = "", string ApplicationNo = "", string Status = "")
        {
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            BL_PrintRecipt obj = new BL_PrintRecipt();

            BL_PrintReciptList sub = new BL_PrintReciptList();
            sub = obj.paymentdetail(pageSize, pageIndex, fromdate, todate, ApplicationNo, Status, collegeID);
            return sub;

        }
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/loginDetailList")]
        public CollegeLoginHistoryList loginDetailList(int pageIndex , int pageSize)
        {

            CollegeLoginHistory obj = new CollegeLoginHistory();           
            CollegeLoginHistoryList sub = new CollegeLoginHistoryList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.UserLogindetailList(pageIndex, pageSize, collegeID);
            
            return sub;

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/qualifiationList")]
        public QualifiationMasterList QualifiationList(int pageSize, int pageIndex, string Id = "")
        {

            StudentAdmissionQualification obj = new StudentAdmissionQualification();            
            QualifiationMasterList sub = new QualifiationMasterList();
            string enID = "";
            int eID = 0;
            if(Id!="0"&& Id.Length>0)
            {
                enID = EncriptDecript.Decrypt(Id);
            }
            eID = Convert.ToInt32(enID);
            sub = obj.QualificationdetailList(pageIndex, pageSize, eID);
            for (int i = 0; i < sub.qlist.Count; i++)
            {

                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/designationList")]
        public DesignationMasterList designationList(int pageIndex, int pageSize)
        {
            DesignationMaster obj = new DesignationMaster();
            DesignationMasterList sub = new DesignationMasterList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }

            //sub = obj.DesignationDetailList(pageIndex, pageSize, collegeID);
           
            return sub;

        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/admissionfeeList")]
        public FeeStructureList admissionfeeList(int pageIndex, int pageSize, string CourseCategory = "", string Session = "",  string castecategory = "",string Subject="",string EducationTypeID="", string FeeType = "", string CourseYearID = "")
        {
            FeeStructure obj = new FeeStructure();
            var castecategory1 = "0";
            var Subject1 = "0";
            castecategory1 = ((castecategory == null ? "0" : castecategory) == "" ? "0" : castecategory);
            Subject1 = ((Subject == null ? "0" : Subject) == "" ? "0" : Subject);
            var EducationTypeID1 = "0";
            EducationTypeID1 = ((EducationTypeID == null ? "0" : EducationTypeID) == "" ? "0" : EducationTypeID);
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            FeeStructureList sub = new FeeStructureList();
            sub = obj.FeestructureDetail(pageSize, pageIndex,  CourseCategory, Session, collegeID, Convert.ToInt32(castecategory1), Convert.ToInt32(Subject1),Convert.ToInt32(EducationTypeID1), FeeType , CourseYearID);
            return sub;

        }

        //public FeeStructureList admissionfeeList(int pageIndex, int pageSize, string Session = "", string College = "", string FeeType = "", string EducationTypeID = "", string CourseCategory = "", string Subject = "", string CourseYearID = "", string castecategory = "")
        //{
        //    FeeStructure obj = new FeeStructure();
        //    //var castecategory1="0";
        //    //var Subject1 = "0";
        //    //castecategory1 =( (castecategory==null?"0": castecategory)==""?"0":castecategory);
        //    //Subject1 = ((Subject == null ? "0" : Subject) == "" ? "0" : Subject);
        //    FeeStructureList sub = new FeeStructureList();
        //    sub = obj.FeestructureDetail(pageIndex, pageSize, Session, College, FeeType, EducationTypeID, CourseCategory, Subject, CourseYearID, castecategory);
        //    return sub;
        //}
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/stdRollNoList")]
        public StudentRollNoList stdRollNoList(int pageIndex, int pageSize, string EducationTypeID="", string CourseCategoryID="",string Name="", string RollNo="",string session="")
        {
            StudentRollNo obj = new StudentRollNo();
            StudentRollNoList sub = new StudentRollNoList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.RollNoListOfStd(pageIndex, pageSize, collegeID, EducationTypeID, CourseCategoryID, Name, RollNo, session);
           
            return sub;

        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/achievementList")]
        public Student_AchievementManagementList achievementList(int pageIndex, int pageSize, string session="", string EducationTypeID="", string Course="", string SID = "", string Name="", string RegistrationNo="")
        {
            Student_AchievementManagement obj = new Student_AchievementManagement();
            Student_AchievementManagementList sub = new Student_AchievementManagementList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.GETachievementList(pageIndex, pageSize, session, EducationTypeID, Course, collegeID, SID, Name, RegistrationNo);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].SID.ToString());
            }
            return sub;

        }
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/eventList")]
        public EventManagmentList eventList(int pageIndex, int pageSize, string EventTypeID = "", string EventOrganiserID = "",string Name="")
        {
            EventManagment obj = new EventManagment();
            EventManagmentList sub = new EventManagmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";

            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.GetEventList(pageIndex, pageSize, EventTypeID, EventOrganiserID,collegeID,Name);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }        
       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/guestList")]
        public EventOrganiserMasterList guestList(int pageIndex, int pageSize, string EventID = "", string MobileNo = "", string Name = "")
        {
            EventOrganiserMaster obj = new EventOrganiserMaster();
            EventOrganiserMasterList sub = new EventOrganiserMasterList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.GetEventinvitationList(pageIndex, pageSize, collegeID, EventID , MobileNo, Name);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;
        }
    
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empInformationList")]
        public CollegeEmployee_Information empInformationList(int pageSize, int pageIndex)
        {
            CollegeEmployee_Information obj = new CollegeEmployee_Information();
            CollegeEmployee_Information sub = new CollegeEmployee_Information();          
            var ID = 0;
            var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
            if (insertby != "")
            {
                ID = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
            }
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            }
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.EmployeedetailList(pageIndex, pageSize,ID, usertype,Convert.ToInt32(collegeID));
            for (int i = 0; i < sub.qlist.Count; i++)
            {              
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].Id.ToString());
                 sub.qlist[i].encriptedeID = EncriptDecript.Encrypt(sub.qlist[i].EmployeeID.ToString());
                
            }
            return sub;

        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/empSalaryList")]
        public EmployeeSalaryMasterList empSalaryList(int pageSize, int pageIndex, string EmployeeID = "", string PaybandID = "",string Name="")
        {
            EmployeeSalaryMaster obj = new EmployeeSalaryMaster();
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            }
            EmployeeSalaryMasterList sub = new EmployeeSalaryMasterList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }                  
            sub = obj.EmployeeSalarydetailList(pageIndex, pageSize,collegeID, EmployeeID, PaybandID, Name, usertype);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].Id.ToString());
            }
            return sub;

        }
      
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/examfeestudentList")]        
        public RecruitmentList examfeestudentList( int pageIndex, int pageSize, string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "",string ApplicationStatus="", string paymentStatus = "", string BackStatus = "0")
        {
            Recruitment obj = new Recruitment(); 
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            int BackStatus1 = Convert.ToInt32((BackStatus==""?"0": BackStatus));
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            if (EducationType == "11")
            {
                sub = obj.ExamFeeStudentdetailList(pageIndex, pageSize, collegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationType == "41")
            {
                sub = obj.ExamFeeStudentdetailList_LLB(pageIndex, pageSize, collegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationType == "12")
            {
                sub = obj.ExamFeeStudentdetailList_PG(pageIndex, pageSize, collegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationType == "13")
            {
                sub = obj.ExamFeeStudentdetailList_BCA(pageIndex, pageSize, collegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            if (EducationType == "40")
            {
                sub = obj.ExamFeeStudentdetailList_BED(pageIndex, pageSize, collegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus1);
            }
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
                sub.qlist[i].EncriptedIDcourseyearid = EncriptDecript.Encrypt(sub.qlist[i].courseyearid.ToString());
            }
            return sub;

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/collegeformlist")]
        public RecruitmentList collegeformlist(int pageIndex, int pageSize, string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "")
        {
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            sub = obj.CollegeStudentdetailList(pageIndex, pageSize, collegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;

        }

        [CustomApiAuthentication]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/backstudentList")]
        public BackStudentList backstudentList(int pageIndex, int pageSize, string CollegeID = "", string session = "", string CourseCategoryID = "", string CourseYearID = "", string EducationTypeID = "", string Enrollmentno = "")
        {
            string encollegeID = (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) != null ? EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) : "");
            Recruitment obj = new Recruitment();
            BackStudentList sub = new BackStudentList();
            sub = obj.BackStudentList(pageIndex, pageSize, encollegeID, session, CourseCategoryID, CourseYearID, EducationTypeID, Enrollmentno);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Collegewise_CenterList")]
        public RecruitmentList Collegewise_CenterList(int pageIndex, int pageSize,  string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "")
        {
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) != null ? EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) : "");
            sub = obj.Collegewise_CenterList(pageIndex, pageSize, encollegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Collegewise_CenterList2")]
        public RecruitmentList Collegewise_CenterList2(int pageIndex, int pageSize, string session = "", string EducationType = "", string CourseCategoryID = "", string Subject = "", string CourseYearID = "", string Application = "", string ApplicationStatus = "", string paymentStatus = "", string BackStatus = "", string CenterType = "")
        {
            Recruitment obj = new Recruitment();
            RecruitmentList sub = new RecruitmentList();
            string encollegeID = (EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) != null ? EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")) : "");
            sub = obj.sp_Collegewise_CenterList_rollnowise(pageIndex, pageSize, encollegeID, session, EducationType, CourseCategoryID, Subject, CourseYearID, Application, ApplicationStatus, paymentStatus, BackStatus, CenterType);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/collegeemployeedetailsList")]
        public EmployeedetailsListList collegeemployeedetailsList(int pageSize, int pageIndex, string MobileNo = "", string Email = "", string Designation = "", string FacultyType = "", string Code = "", string Name = "")
        {
            Employeedetialslist obj = new Employeedetialslist();
            var usertype = 0;
            if (ClsLanguage.GetCookies("ENNBUserType") != null)
            {
                usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            }
            EmployeedetailsListList sub = new EmployeedetailsListList();
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            var ID = 0;
            var insertby = (ClsLanguage.GetCookies("ENNBUID") != null ? ClsLanguage.GetCookies("ENNBUID") : "");
            if (insertby != "")
            {
                ID = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
            }
            sub = obj.EmployeedetailsListList(pageIndex, pageSize, MobileNo, Email, collegeID, Designation, FacultyType, Code, Name, usertype, ID);
            //for (int i = 0; i < sub.qlist.Count; i++)
            //{

            //    sub.qlist[i].EnEID = EncriptDecript.Encrypt(sub.qlist[i].EID.ToString());
            //    sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].Id.ToString());
            //}
            return sub;

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/MigrationReportList")]
        public ExamForm_Listlist MigrationReportList(int pageIndex, int pageSize, string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RegistrationNo = "", string session = "")
        {
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            ExamForm obj = new ExamForm();
            ExamForm_Listlist sub = new ExamForm_Listlist();
           // ExamFormListList qlist = new List<ExamForm>();
            sub = obj.clcReportList(pageIndex, pageSize, collegeID, EducationTypeID, CourseCategoryID, Name, RegistrationNo, session);
            //for (int i = 0; i < sub.Studentlist.Count; i++)
            //{
            //    sub.Studentlist[i].EncriptedID = EncriptDecript.Encrypt(sub.Studentlist[i].ID.ToString());
            //}
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/clcAllotList")]
        public ExamForm_Listlist MigrationAllotList(int pageIndex, int pageSize, string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RegistrationNo = "", string session = "")
        {
            string encollegeID = ClsLanguage.GetCookies("ENNBCLID");
            string collegeID = "";
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            ExamForm obj = new ExamForm();
            ExamForm_Listlist sub = new ExamForm_Listlist();
            sub = obj.clcAllotList(pageIndex, pageSize, collegeID, EducationTypeID, CourseCategoryID, Name, RegistrationNo, session);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].sid.ToString());
            }
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/EnrollmentRequestDetailList")]
        public EnrollmentRequestList EnrollmentRequestDetailList(int pageIndex, int pageSize, string collegeID = "", string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RegistrationNo = "", string session = "")
        {
            string encollegeID = (ClsLanguage.GetCookies("ENNBCLID") != null ? ClsLanguage.GetCookies("ENNBCLID") : "");

          
            if (encollegeID != "0" && encollegeID.Length > 0)
            {
                collegeID = EncriptDecript.Decrypt(encollegeID);
            }
            EnrollmentRequest obj = new EnrollmentRequest();
            EnrollmentRequestList sub = new EnrollmentRequestList();
            sub = obj.EnrollmentDetailList1(pageIndex, pageSize, collegeID, EducationTypeID, CourseCategoryID, Name, RegistrationNo, session);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;
        }
    }
}
