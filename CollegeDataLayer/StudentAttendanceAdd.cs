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
using System.Web.Mvc;

namespace DataLayer
{
    public class StudentAttendanceAdd
    {
        [Required(ErrorMessage = "Select File First!")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx|.XLS|.XLSX)$", ErrorMessage = "Only Excel files allowed.")]
        public HttpPostedFileBase ExcelPath { get; set; }

        public int CollegeID { get; set; }
        [Required(ErrorMessage = "Select Employee First!")]
        public int EmployeeID { get; set; }
        public int SessionID { get; set; }
        [Required(ErrorMessage = "Select Student First!")]
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Select Subject First!")]
        public int SubjectID { get; set; }
     
        [Required(ErrorMessage = "Select Course Year First!")]
        public int StudentYear { get; set; }

        [Required(ErrorMessage = "Select Subject First!")]
        public string SubjectCode { get; set; }
        public int CommonId { get; set; }
        public string Title { get; set; }
        public string CourseCategoryName { get; set; }
        
        //public int MONTH { get; set; }



        public string StudentRollNo { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string AttendanceType { get; set; }

        public bool status { get; set; }
        public string Message { get; set; }

        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public string UserName { get; set; }
        public string Fullname { get; set; }
        public string Session { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Attendance_Date { get; set; }
        public int UserType { get; set; }
        public string Subject { get; set; }
        public int SSession { get; set; }
        
        public string Month { get; set; }
        public string Year { get; set; }
        public string Day { get; set; }
        public string Holiday { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string _4 { get; set; }
        public string _5 { get; set; }
        public string _6 { get; set; }
        public string _7 { get; set; }
        public string _8 { get; set; }
        public string _9 { get; set; }
        public string _10 { get; set; }
        public string _11 { get; set; }
        public string _12 { get; set; }
        public string _13 { get; set; }
        public string _14 { get; set; }
        public string _15 { get; set; }
        public string _16 { get; set; }
        public string _17 { get; set; }
        public string _18 { get; set; }
        public string _19 { get; set; }
        public string _20 { get; set; }
        public string _21 { get; set; }
        public string _22 { get; set; }
        public string _23 { get; set; }
        public string _24 { get; set; }
        public string _25 { get; set; }
        public string _26 { get; set; }
        public string _27 { get; set; }
        public string _28 { get; set; }
        public string _29 { get; set; }
        public string _30 { get; set; }
        public string _31 { get; set; }
        public string Total { get; set; }
        public string SessionName { get; set; }
        public string DayName { get; set; }         
        public string StudentName { get; set; }
        public string RollNo { get; set; }
        public string streamCategory { get; set; }
        public string Percentage { get; set; }
        public string PercentageSearch { get; set; }
        public string SNO { get; set; }

        [Required(ErrorMessage = "Select Education Type First!")]
        public int EduTypeID { get; set; }
        [Required(ErrorMessage = "Select Course Category First!")]
        public int CourseCategoryID { get; set; }

        public List<StudentAttendanceAdd> AttendanceList { get; set; }
        public List<StudentAttendanceAdd> AttendanceReport { get; set; }
        public List<StudentAttendanceAdd> Totalrecord { get; set; }
        public List<SelectListItem> EduTypeList { get; set; }
        public List<SelectListItem> CourseCategoryList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> SubjectCodeList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public string totalCount { get; set; }

        public StudentAttendanceAdd StudentAttendance_Add(StudentAttendanceAdd sta)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                var obj = conn.Query<StudentAttendanceAdd>("[sp_CollegeEmployee_StudentAttendance]", new
                {
                    @Action = "Insert",
                    @CollegeID = sta.CollegeID,
                    @EmployeeID = sta.EmployeeID,
                    @SessionID = sta.SessionID,
                    @SubjectCode = sta.SubjectCode,
                    @StudentRollNo = sta.StudentRollNo,
                    @AttendanceDate = sta.AttendanceDate,
                    @AttendanceType = sta.AttendanceType,
                    @StudentYear=sta.StudentYear,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }

        public StudentAttendanceAdd StudentAttendanceDetailList(int CollegeID = 0, int EmployeeID = 0)
        {

            StudentAttendanceAdd list = new StudentAttendanceAdd();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_CollegeEmployee_StudentAttendance]", new { @Action = "GetDetail", @CollegeID = CollegeID, @EmployeeID = EmployeeID }, commandType: CommandType.StoredProcedure);
                list.AttendanceList = obj.Read<StudentAttendanceAdd>().ToList();
                // list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public StudentAttendanceAdd StudentAttendanceDetailList_FilterDate(int CollegeID = 0, int EmployeeID = 0, string Month = "", string Year = "")
        {

            StudentAttendanceAdd list = new StudentAttendanceAdd();
            if (CollegeID > 0)
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[sp_CollegeEmployee_StudentAttendance]", new { @Action = "Filter", @CollegeID = CollegeID, @EmployeeID = EmployeeID, @Month = Month, @Year = Year }, commandType: CommandType.StoredProcedure);
                    list.AttendanceList = obj.Read<StudentAttendanceAdd>().ToList();
                    // list.totalCount = obj.Read<string>().FirstOrDefault();
                }
            }
            return list;
        }

        public StudentAttendanceAdd StudentAttendanceReport(int CollegeID = 0, int StudentID = 0, int SubjectID = 0, string SessionID = "0")
        {

            StudentAttendanceAdd list = new StudentAttendanceAdd();
            if (CollegeID > 0)
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[sp_StudentAttendanceReport]", new { @CollegeID = CollegeID, @StudentID = StudentID, @SubjectID = SubjectID, @SessionID = SessionID }, commandType: CommandType.StoredProcedure);
                    list.AttendanceReport = obj.Read<StudentAttendanceAdd>().ToList();
                    // list.totalCount = obj.Read<string>().FirstOrDefault();
                }
            }
            return list;
        }


        public StudentAttendanceAdd StudentAttendancePercentageReport(int CollegeID = 0, int SessionID = 0, int CourseCategoryID=0, string PercentageSearch = "", int StudentYear=0, int SubjectID=0)
        {
            StudentAttendanceAdd list = new StudentAttendanceAdd();
            if (CollegeID > 0)
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[sp_StudentAttendancePercentage]", new { @CollegeID = CollegeID, @CourseCategoryID = CourseCategoryID, @SessionID = SessionID , @MinumPercentage =Convert.ToDecimal(PercentageSearch), @StudentYear= StudentYear, @SubjectID= SubjectID }, commandType: CommandType.StoredProcedure);
                    list.AttendanceReport = obj.Read<StudentAttendanceAdd>().ToList();
                }
            }
            return list;
        }
        public StudentAttendanceAdd StudentAttendancePercentageReporttotal(int SessionID = 0, int CourseCategoryID = 0, int StudentYear=0, int SubjectID=0)
        {
            StudentAttendanceAdd list = new StudentAttendanceAdd();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAttendanceAdd>("[sp_GetSessionAndStreem]", new { @SessionID = SessionID, @CourseCategoryID = CourseCategoryID, @StudentYear= StudentYear, @SubjectID= SubjectID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }






        public StudentAttendanceAdd StudentAttendanceReportMonthly(int CollegeID = 0, string Month = "", int SubjectID = 0, string SessionID = "0", int StudentYear=0)
        {

            StudentAttendanceAdd list = new StudentAttendanceAdd();
            if (CollegeID > 0)
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[sp_StudentAttendanceReportMonthwaise]", new { @CollegeID = CollegeID, @Month = Month, @SubjectID = SubjectID, @SessionID = SessionID, @StudentYear=StudentYear }, commandType: CommandType.StoredProcedure);
                    list.AttendanceReport = obj.Read<StudentAttendanceAdd>().ToList();
                    // list.totalCount = obj.Read<string>().FirstOrDefault();
                }
            }
            return list;
        }


        public StudentAttendanceAdd total(int CollegeID = 0, int StudentID = 0, int SubjectID = 0, string SessionID = "0", int CourseCategoryID = 0)
        {
            StudentAttendanceAdd list = new StudentAttendanceAdd();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAttendanceAdd>("[sp_countAttendus]", new { @CollegeID = CollegeID, @StudentID = StudentID, @SubjectID = SubjectID, @SessionID = SessionID, @CourseCategoryID = CourseCategoryID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public StudentAttendanceAdd totalMonthly(int CollegeID = 0,  string Month="", int SubjectID = 0, string SessionID = "0", int CourseCategoryID = 0, int StudentYear=0)
        {
            StudentAttendanceAdd list = new StudentAttendanceAdd();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAttendanceAdd>("[sp_countAttendusMonthly]", new { @CollegeID = CollegeID, @Month = Month, @SubjectID = SubjectID, @SessionID = SessionID, @CourseCategoryID = CourseCategoryID, @StudentYear= StudentYear }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public StudentAttendanceAdd StudentAttendanceReportnew(int CollegeID = 0, int StudentID = 0, int SubjectID = 0, string SessionID = "0")
        {

            StudentAttendanceAdd list = new StudentAttendanceAdd();

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_StudentAttendanceReport]", new { @CollegeID = CollegeID, @StudentID = StudentID, @SubjectID = SubjectID, @SessionID = SessionID }, commandType: CommandType.StoredProcedure);
                list.AttendanceReport = obj.Read<StudentAttendanceAdd>().ToList();
                // list.totalCount = obj.Read<string>().FirstOrDefault();

            }
            return list;
        }








        public StudentAttendanceAdd AttendanceMonthlyStudentWise(int CollegeID = 0, string Month = "", int SubjectID = 0, string SessionID = "0", int StudentID=0, int StudentYear=0)
        {

            StudentAttendanceAdd list = new StudentAttendanceAdd();
            if (CollegeID > 0)
            {
                using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                {
                    var obj = conn.QueryMultiple("[sp_StudentAttendanceMonthlyStudentwise]", new { @CollegeID = CollegeID, @Month = Month, @SubjectID = SubjectID, @SessionID = SessionID, @StudentID= StudentID, @StudentYear= StudentYear }, commandType: CommandType.StoredProcedure);
                    list.AttendanceReport = obj.Read<StudentAttendanceAdd>().ToList();
                    // list.totalCount = obj.Read<string>().FirstOrDefault();
                }
            }
            return list;
        }      
        public StudentAttendanceAdd totalMonthlyStudentWise(int CollegeID = 0, string Month = "", int SubjectID = 0, string SessionID = "0", int CourseCategoryID = 0, int StudentID = 0, int StudentYear=0)
        {
            StudentAttendanceAdd list = new StudentAttendanceAdd();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentAttendanceAdd>("[sp_countAttendusMonthlyStudentwise]", new { @CollegeID = CollegeID, @Month = Month, @SubjectID = SubjectID, @SessionID = SessionID, @CourseCategoryID = CourseCategoryID, @StudentID = StudentID, @StudentYear= StudentYear }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }














        public List<SelectListItem> GetMonthList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            items.Add(new SelectListItem { Value = "1", Text = "January" });
            items.Add(new SelectListItem { Value = "2", Text = "February" });
            items.Add(new SelectListItem { Value = "3", Text = "March" });
            items.Add(new SelectListItem { Value = "4", Text = "April" });
            items.Add(new SelectListItem { Value = "5", Text = "May" });
            items.Add(new SelectListItem { Value = "6", Text = "June" });
            items.Add(new SelectListItem { Value = "7", Text = "July" });
            items.Add(new SelectListItem { Value = "8", Text = "August" });
            items.Add(new SelectListItem { Value = "9", Text = "September" });
            items.Add(new SelectListItem { Value = "10", Text = "October" });
            items.Add(new SelectListItem { Value = "11", Text = "November" });
            items.Add(new SelectListItem { Value = "12", Text = "December" });
            return items;
        }
        public List<SelectListItem> GetYearList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "2019", Text = "2019" });
            return items;
        }
    }
}
