using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
    public class CollegeExamCenter
    {
        public int ID { get; set; }
        public int CollegeID { get; set; }
        public int Session { get; set; }
        public int InsertedBY { get; set; }
        public string IPAddress { get; set; }
        public string CreateDate { get; set; }
        public int ModifyBY { get; set; }
        public bool IsActive { get; set; }
        public List<BL_CollegeMaster> CollegeList { get; set; }
        public string CollegeIDList { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string CollegeName { get; set; }
        public string EncriptedID { get; set; }
        public string EduTypeID { get; set; }
        public int CourseCategoryID { get; set; }
        public string SubjectID { get; set; }
        public int HounorsSubjectID { get; set; }
        public int StreamCategoryID { get; set; }
        public int CourseYearID { get; set; }
        public string YearName { get; set; }
        public int IsHonourse { get; set; }
        public int Paper1 { get; set; }
        public int Paper2 { get; set; }
        public int Paper3 { get; set; }
        public int Paper4 { get; set; }
        public int PaperMinMark { get; set; }
        public int PraticalMarks { get; set; }
        public int PraticalMinMarks { get; set; }
        public int PaperTotalMark { get; set; }
        public string HonourseName { get; set; }
        public int Ispractical { get; set; }
        public int IsCompulsory { get; set; }
        public int IsCompulsory100 { get; set; }
        public int PaperTotalPassingMark { get; set; }
        public int totalEnq { get; set; }
        public int EducationType { get; set; }
        public int SessionID { get; set; }
        public string StudentName { get; set; }
        public string MonthName { get; set; }
        public string CurrentYearName { get; set; }
        public string CurrentDate { get; set; }
        public string Title { get; set; }
        public string CourseCategoryName { get; set; }
        public string SessionName { get; set; }
        public string UpdateDate { get; set; }
        public string streamCategoryName { get; set; }
        public string Fullname { get; set; }
        public string IpNumber { get; set; }
        public string AddDate { get; set; }
        public string HonoursP1 { get; set; }
        public string Honoursp2 { get; set; }
        public string Honoursp3 { get; set; }
        public string Honoursp4 { get; set; }
        public string HonoursPer { get; set; }
        public string Subsidiary1P1 { get; set; }
        public string Subsidiary1Per { get; set; }
        public string Subsidiary2P1 { get; set; }
        public string Subsidiary2Per { get; set; }
        public string CompulsoryP1 { get; set; }
        public string CompulsoryP2 { get; set; }
        public string GenStudies { get; set; }
        public string TotalObetand { get; set; }
        public string Name { get; set; }
        public string EnrollmentNo { get; set; }
        public string RollNo { get; set; }
        public string Honours { get; set; }
        public string Subsidiary1 { get; set; }
        public string Subsidiary2 { get; set; }
        public string Compulsory1 { get; set; }
        public string Compulsory2 { get; set; }
        public string TotalPaperObetand { get; set; }
        public string SID { get; set; }
        public string Subsidiary1Pass { get; set; }
        public string HonoursPass { get; set; }
        public string Subsidiary2Pass { get; set; }
        public string CompulsoryPass { get; set; }
        public string SumHonours { get; set; }
        public string SumSubsidiary1 { get; set; }
        public string SumSubsidiary2 { get; set; }
        public string SumCompulsory { get; set; }
        public string TotalHonours { get; set; }
        public string Result { get; set; }
        public string HonoursP12nd { get; set; }
        public string Honoursp22nd { get; set; }
        public string HonoursPer2nd { get; set; }
        public string Subsidiary1P12nd { get; set; }
        public string Subsidiary1Per2nd { get; set; }
        public string Subsidiary2P12nd { get; set; }
        public string Subsidiary2Per2nd { get; set; }
        public string CompulsoryP12nd { get; set; }
        public string CompulsoryP22nd { get; set; }
        public string GenStudies2nd { get; set; }
        public string TotalPaperObetand2nd { get; set; }
        public string TotalObetand2nd { get; set; }
        public string SumHonours2nd { get; set; }
        public string SumSubsidiary12nd { get; set; }
        public string SumSubsidiary22nd { get; set; }
        public string SumCompulsory2nd { get; set; }
        public string TotalHonours2nd { get; set; }
        public string EducationType2nd { get; set; }
        public string CourseCategoryID2nd { get; set; }
        public string SessionID2nd { get; set; }
        public string CourseYearID2nd { get; set; }
        public string CollegeID2nd { get; set; }
        public int SID2nd { get; set; }
        public string Result2nd { get; set; }
        public string HonoursP13rd { get; set; }
        public string Honoursp23rd { get; set; }
        public string Honoursp33rd { get; set; }
        public string Honoursp43rd { get; set; }
        public string HonoursPer3rd { get; set; }
        public string GenStudies3rd { get; set; }
        public string TotalPaperObetand3rd { get; set; }
        public string TotalObetand3rd { get; set; }
        public string SumHonours3rd { get; set; }
        public string TotalHonours3rd { get; set; }
        public string SID3rd { get; set; }
        public int EducationType3rd { get; set; }
        public int CourseCategoryID3rd { get; set; }
        public int SessionID3rd { get; set; }
        public int CourseYearID3rd { get; set; }
        public int CollegeID3rd { get; set; }
        public string Result3rd { get; set; }
        public string FinalResult { get; set; }
        public int TotalCompulsory { get; set; }
        public string Honoursp32nd { get; set; }
        public int GrandTotal { get; set; }
        public int HounorsTotalMarks { get; set; }
        public int HounorsPraTotalMarks { get; set; }
        public int Subsidiary1TotalMarks { get; set; }
        public int Subsidiary1PraTotalMarks { get; set; }
        public int Subsidiary2TotalMarks { get; set; }
        public int Subsidiary2PraTotalMarks { get; set; }
        public int Compulsory1TotalMarks { get; set; }
        public int Compulsory2TotalMarks { get; set; }
        public int ResultFinal { get; set; }
        public int ResultFinalNew { get; set; }
        public string Remarks { get; set; }
        public string division { get; set; }
        public string SerialNo { get; set; }
        public List<SelectListItem> ExamNumberList { get; set; }
        public List<SelectListItem> EduTypeList { get; set; }
        public List<SelectListItem> CourseCategoryList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> CourseYearIDList { get; set; }
        public List<SelectListItem> CollegeStudentList { get; set; }
        public List<CollegeExamCenter> StudentList { get; set; }
        public List<CollegeExamCenter> StudentListNew { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int totalCount { get; set; }
        public int Row { get; set; }
        public string ResultName { get; set; }
        public string ApplicationNo { get; set; }
        public bool searchType { get; set; }
        public string YearValue { get; set; }


        //LLB TR keys Start
        //public int Id { get; set; }
        public string Paper_P1_Th_Max { get; set; }
        public string Paper_P1_Internal_Max { get; set; }
        public string Paper_P1_Th_Min { get; set; }
        public string Paper_P1_Internal_Min { get; set; }
        public string Paper_P1_Th_Obtained { get; set; }
        public string Paper_P1_Internal_Obtained { get; set; }
        public string Paper_P2_Th_Max { get; set; }
        public string Paper_P2_Internal_Max { get; set; }
        public string Paper_P2_Th_Min { get; set; }
        public string Paper_P2_Internal_Min { get; set; }
        public string Paper_P2_Th_Obtained { get; set; }
        public string Paper_P2_Internal_Obtained { get; set; }
        public string Paper_P3_Th_Max { get; set; }
        public string Paper_P3_Internal_Max { get; set; }
        public string Paper_P3_Th_Min { get; set; }
        public string Paper_P3_Internal_Min { get; set; }
        public string Paper_P3_Th_Obtained { get; set; }
        public string Paper_P3_Internal_Obtained { get; set; }
        public string Paper_P4_Th_Max { get; set; }
        public string GPA { get; set; }
        public string Paper_P4_Internal_Max { get; set; }
        public string Paper_P4_Th_Min { get; set; }
        public string Paper_P4_Internal_Min { get; set; }
        public string Paper_P4_Th_Obtained { get; set; }
        public string Paper_P4_Internal_Obtained { get; set; }
        public string Paper_P5_Th_Max { get; set; }
        public string Paper_P5_Internal_Max { get; set; }
        public string Paper_P5_Th_Min { get; set; }
        public string Paper_P5_Internal_Min { get; set; }
        public string Paper_P5_Th_Obtained { get; set; }
        public string Paper_P5_Internal_Obtained { get; set; }
        public string Paper_P6_Th_Max { get; set; }
        public string Paper_P6_Internal_Max { get; set; }
        public string Paper_P6_Th_Min { get; set; }
        public string Paper_P6_Internal_Min { get; set; }
        public string Paper_P6_Th_Obtained { get; set; }
        public int show { get; set; }
        public string Paper_P6_Internal_Obtained { get; set; }
        public int InsertedBy { get; set; }
        public string TotalPaperMarks { get; set; }
        public string SerialNumber { get; set; }
        public string TotalPaperMinMarks { get; set; }
        public string Collegename { get; set; }
        public string PaperI { get; set; }
        public string PaperII { get; set; }
        public string PaperIII { get; set; }
        public string PaperIV { get; set; }
        public string PaperV { get; set; }
        //LLB TR keys End
        //bed key start
        public string PaperVI { get; set; }
        public string PaperVII { get; set; }
        public string PaperEPC1 { get; set; }
        public string PaperEPC2 { get; set; }
        public string PaperEPC3 { get; set; }
        public string Paper_C1_Th_Max { get; set; }
        public string Paper_C1_Internal_Max { get; set; }
        public string Paper_C1_Th_Min { get; set; }
        public string Paper_C1_Internal_Min { get; set; }
        public string Paper_C1_Th_Obtained { get; set; }
        public string Paper_C1_Internal_Obtained { get; set; }
        public string Paper_C2_Th_Max { get; set; }
        public string Paper_C2_Internal_Max { get; set; }
        public string Paper_C2_Th_Min { get; set; }
        public string Paper_C2_Internal_Min { get; set; }
        public string Paper_C2_Th_Obtained { get; set; }
        public string Paper_C2_Internal_Obtained { get; set; }
        public string Paper_C3_Th_Max { get; set; }
        public string Paper_C3_Internal_Max { get; set; }
        public string Paper_C3_Th_Min { get; set; }
        public string Paper_C3_Internal_Min { get; set; }
        public string Paper_C3_Th_Obtained { get; set; }
        public string Paper_C3_Internal_Obtained { get; set; }
        public string Paper_C4_Th_Max { get; set; }
        public string Paper_C4_Internal_Max { get; set; }
        public string Paper_C4_Th_Min { get; set; }
        public string Paper_C4_Internal_Min { get; set; }
        public string Paper_C4_Th_Obtained { get; set; }
        public string Paper_C4_Internal_Obtained { get; set; }
        public string Paper_C5_Th_Max { get; set; }
        public string Paper_C5_Internal_Max { get; set; }
        public string Paper_C5_Th_Min { get; set; }
        public string Paper_C5_Internal_Min { get; set; }
        public string Paper_C5_Th_Obtained { get; set; }
        public string Paper_C5_Internal_Obtained { get; set; }
        public string Paper_C6_Th_Max { get; set; }
        public string Paper_C6_Internal_Max { get; set; }
        public string Paper_C6_Th_Min { get; set; }
        public string Paper_C6_Internal_Min { get; set; }
        public string Paper_C6_Th_Obtained { get; set; }
        public string Paper_C6_Internal_Obtained { get; set; }
        public string Paper_C7_Th_Max { get; set; }
        public string Paper_C7_Internal_Max { get; set; }
        public string Paper_C7_Th_Min { get; set; }
        public string Paper_C7_Internal_Min { get; set; }
        public string Paper_C7_Th_Obtained { get; set; }
        public string Paper_C7_Internal_Obtained { get; set; }
        public string Paper_EPC1_Internal_Max { get; set; }
        public string Paper_EPC1_Th_Min { get; set; }
        public string Paper_EPC1_Internal_Min { get; set; }
        public string Paper_EPC1_Th_Obtained { get; set; }
        public string Paper_EPC1_Internal_Obtained { get; set; }
        public string Paper_EPC2_Th_Max { get; set; }
        public string Paper_EPC2_Internal_Max { get; set; }
        public string Paper_EPC2_Th_Min { get; set; }
        public string Paper_EPC2_Internal_Min { get; set; }
        public string Paper_EPC2_Th_Obtained { get; set; }
        public string Paper_EPC2_Internal_Obtained { get; set; }
        public string Paper_EPC3_Th_Max { get; set; }
        public string Paper_EPC3_Internal_Max { get; set; }
        public string Paper_EPC3_Th_Min { get; set; }
        public string Paper_EPP3_Internal_Min { get; set; }
        public string Paper_EPC3_Th_Obtained { get; set; }
        public string Paper_EPC3_Internal_Obtained { get; set; }
        //bed key End

        public CollegeExamCenter GetLLBSubject(int CourceYearID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CollegeExamCenter obj = new CollegeExamCenter();
                 obj = conn.Query<CollegeExamCenter>("[sp_GetLLBSubject]", new { @Courseyearid = CourceYearID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenter GetBCASubject(int CourceYearID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CollegeExamCenter obj = new CollegeExamCenter();
                obj = conn.Query<CollegeExamCenter>("[sp_GetBCASubject]", new { @Courseyearid = CourceYearID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenter GetBEDSubject(int CourceYearID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CollegeExamCenter obj = new CollegeExamCenter();
                obj = conn.Query<CollegeExamCenter>("[sp_GetBEDSubject]", new { @Courseyearid = CourceYearID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenter getSubjectDetailmaster(int Id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeExamCenter>("[sp_getSubjectDetail]", new { @StreamCategoryID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenter SaveAllocatedCollege(CollegeExamCenter ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeExamCenter>("sp_CollegeExamCenter", new
                {
                    Action = "SaveExamCenter",
                    @ID = ob.ID,
                    @Session = ob.Session,
                    @InsertedBY = ob.InsertedBY,
                    @IPaddress = IP,
                    @CollegeIDList = ob.CollegeIDList


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenter GetAllocatedExamCenter()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeExamCenter>("sp_CollegeExamCenter", new { Action = "View" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenterList centerallocationdetailList(int pageIndex = 1, int pageSize = 25, string Session = "")
        {
            CollegeExamCenterList list = new CollegeExamCenterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_CollegeExamCenter]", new { @Action = "centereport", PageIndex = pageIndex, pageSize = pageSize, @Session = Session }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeExamCenter>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public CollegeExamCenter DeleteExamCenterID(int id = 0)
        {
            CollegeExamCenter ob = new CollegeExamCenter();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeExamCenter>("sp_CollegeExamCenter", new { @Action = "Delete", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<CollegeExamCenter> CourseYear(int id)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<CollegeExamCenter>("sp_CourseYear", new { @CourseID = id }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }
        }
        public List<CollegeExamCenter> GetHonourseName(int id)
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_Honourse]", new { Action = "View", @Id = id }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public CollegeExamCenter CheckExamNumberExisist(int EducationType = 0, int CourseCategoryID = 0, int SessionID = 0, int StreamCategoryID = 0, int IsHonourse = 0, int CourseYearId = 0)
        {
            CollegeExamCenter obj = new CollegeExamCenter();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_CheckExamNumber]", new { @EducationType = EducationType, @CourseCategoryID = CourseCategoryID, @SessionID = SessionID, @StreamCategoryID = StreamCategoryID, @IsHonourse = IsHonourse, @CourseYearId = CourseYearId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return obj;
        }
        public CollegeExamCenter saveexamnumbersheet(string UserID = "", string Flag = "", int Id = 0, int EduTypeID = 0, int CourseCategoryID = 0, int StreamCategoryID = 0, int Session = 0, int HonourseName = 0, int CourseYearID = 0, int Paper1 = 0, int Paper2 = 0, int Paper3 = 0, int Paper4 = 0, int PaperMinMark = 0, int PraticalMarks = 0, int PraticalMinMarks = 0, int PaperTotalMark = 0, int PaperTotalPassingMark = 0)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeExamCenter>("sp_ExamManageNumberSheet", new
                {
                    @Id = Id,
                    @EducationType = EduTypeID,
                    @CourseCategoryID = CourseCategoryID,
                    @SessionID = Session,
                    @StreamCategoryID = StreamCategoryID,
                    @IsHonourse = HonourseName,
                    @Paper1 = Paper1,
                    @Paper2 = Paper2,
                    @Paper3 = Paper3,
                    @Paper4 = Paper4,
                    @PaperMinMark = PaperMinMark,
                    @PraticalMarks = PraticalMarks,
                    @PraticalMinMarks = PraticalMinMarks,
                    @PaperTotalMark = PaperTotalMark,
                    @Action = Flag,
                    @CourseYearId = CourseYearID,
                    @UserID = UserID,
                    @IpNumber = IP,
                    @PaperTotalPassingMark = PaperTotalPassingMark
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public CollegeExamCenterList managemxamnumbersheet(int pageIndex1 = 1, int pageSize1 = 25, string EduTypeID = "", string StreamCategoryID = "", string Session = "", string HonourseName = "", string CourseYearID = "")
        {
            CollegeExamCenterList list = new CollegeExamCenterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_ExamManageNumberSheet]", new { @Action = "View", PageIndex = pageIndex1, pageSize = pageSize1, @EducationType = EduTypeID, @StreamCategoryID = StreamCategoryID, @SessionID = Session, @IsHonourse = HonourseName, @CourseYearId = CourseYearID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeExamCenter>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public CollegeExamCenter emxamnumbersheetDEtailbyid(int id = 0)
        {
            CollegeExamCenter obj = new CollegeExamCenter();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<CollegeExamCenter>("[sp_ExamManageNumberSheet]", new { @Action = "ViewByID", @Id = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeExamCenterList GetCollegeStudent(string action= "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int pageIndex = 1, int PageSize = 20, string enrollmentno = "",string YearValue="")
        { 

            CollegeExamCenterList list = new CollegeExamCenterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
             
                var obj = conn.QueryMultiple("[sp_GetstudentList]", new { @Action = action,@EducationType = Educationtype, @CourseCategory = CourseCategoryID, @College = CollegeID, @Session = Session, @CourseYearID = yearid, @PageSize = PageSize, @PageIndex = pageIndex, @enrollmentno= enrollmentno }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeExamCenter>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();

                foreach (var item in list.qlist)
                {
                    var obj1 = conn.Query<CollegeExamCenter>("[sp_ExamStudentExamNumber]", new
                    {
                        @Action = "ViewBySID",
                        @EducationType = item.EducationType,
                        @CourseCategoryID = item.CourseCategoryID,
                        @CollegeID = item.CollegeID,
                        @SessionID = item.Session,
                        @CourseYearID = item.CourseYearID,
                        @SID = Convert.ToInt32(item.ID),
                        @YearValue= YearValue,
                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (obj1 != null)
                    {
                        item.HonoursP1 = obj1.HonoursP1;
                        item.Honoursp2 = obj1.Honoursp2;
                        item.Honoursp3 = obj1.Honoursp3;
                        item.Honoursp4 = obj1.Honoursp4;

                        item.HonoursPer = obj1.HonoursPer;
                        item.Subsidiary1P1 = obj1.Subsidiary1P1;
                        item.Subsidiary1Per = obj1.Subsidiary1Per;
                        item.Subsidiary2P1 = obj1.Subsidiary2P1;

                        item.Subsidiary2Per = obj1.Subsidiary2Per;
                        item.CompulsoryP1 = obj1.CompulsoryP1;
                        item.CompulsoryP2 = obj1.CompulsoryP2;
                        item.GenStudies = obj1.GenStudies;
                        item.TotalObetand = obj1.TotalObetand;
                        item.Remarks = obj1.Remarks;
                        item.ResultFinal = obj1.ResultFinal;
                        item.YearName = obj1.YearName;
                        item.CourseYearID = obj1.CourseYearID;
                        item.YearValue = obj1.YearValue;

                    }
                    else
                    {
                        item.HonoursP1 = "0";
                        item.Honoursp2 = "0";
                        item.Honoursp3 = "0";
                        item.Honoursp4 = "0";

                        item.HonoursPer = "0";
                        item.Subsidiary1P1 = "0";
                        item.Subsidiary1Per = "0";
                        item.Subsidiary2P1 = "0";

                        item.Subsidiary2Per = "0";
                        item.CompulsoryP1 = "0";
                        item.CompulsoryP2 = "0";
                        item.GenStudies = "0";
                        item.TotalObetand = "0";
                        item.Remarks = "";
                        item.ResultFinal = 0;
                        //item.YearValue = "0";
                    }
                }

            }
            if(action=="CustomSearch")
            {
                if (YearValue != "")
                {
                    CollegeExamCenterList finallist = new CollegeExamCenterList();
                    if (list.qlist.Count > 0)
                    {
                        if (list.qlist.FirstOrDefault().YearValue == "0")
                        {
                            finallist.qlist = list.qlist.Where(x => x.YearValue.ToString() == YearValue).ToList();
                        }
                        else
                        {
                            finallist.qlist = list.qlist.Where(x => x.YearValue.ToString() == YearValue).ToList();
                        }
                    }
                    else
                    { 
                     finallist.qlist = list.qlist;
                    }
                    finallist.totalCount = finallist.qlist.Count.ToString();
                    return finallist;
                }

            }

            return list;
        }

        public CollegeExamCenterList GetCollegeStudentLLB(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int pageIndex = 1, int PageSize = 20, string enrollmentno = "", string YearValue = "")
        {

            CollegeExamCenterList list = new CollegeExamCenterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                var obj = conn.QueryMultiple("[sp_GetstudentListLLB]", new { @Action = action, @EducationType = Educationtype, @CourseCategory = CourseCategoryID, @College = CollegeID, @Session = Session, @CourseYearID = yearid, @PageSize = PageSize, @PageIndex = pageIndex, @enrollmentno = enrollmentno }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeExamCenter>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();

                foreach (var item in list.qlist)
                {
                    var obj1 = conn.Query<CollegeExamCenter>("[sp_ExamStudentExamNumberLLB]", new
                    {
                        @Action = "ViewBySID",
                        @EducationType = item.EducationType,
                        @CourseCategoryID = item.CourseCategoryID,
                        @CollegeID = item.CollegeID,
                        @SessionID = item.Session,
                        @CourseYearID = item.CourseYearID,
                        @SID = Convert.ToInt32(item.ID),
                        @YearValue = YearValue,
                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (obj1 != null)
                    {
                        item.Paper_P1_Th_Obtained = obj1.Paper_P1_Th_Obtained;
                        item.Paper_P1_Internal_Obtained = obj1.Paper_P1_Internal_Obtained;
                        item.Paper_P2_Th_Obtained = obj1.Paper_P2_Th_Obtained;
                        item.Paper_P2_Internal_Obtained = obj1.Paper_P2_Internal_Obtained;
                        item.Paper_P3_Th_Obtained = obj1.Paper_P3_Th_Obtained;
                        item.Paper_P3_Internal_Obtained = obj1.Paper_P3_Internal_Obtained;
                        item.Paper_P4_Th_Obtained = obj1.Paper_P4_Th_Obtained;
                        item.Paper_P4_Internal_Obtained = obj1.Paper_P4_Internal_Obtained;
                        item.Paper_P5_Th_Obtained = obj1.Paper_P5_Th_Obtained;
                        item.Paper_P5_Internal_Obtained = obj1.Paper_P5_Internal_Obtained;
                        item.ResultFinal = obj1.ResultFinal;
                        item.Remarks = obj1.Remarks;
                        item.TotalObetand = obj1.TotalObetand;
                        item.YearValue = obj1.YearValue;
                        item.YearName = obj1.YearName;

                    }
                    else
                    {
                       
                        item.Paper_P1_Th_Obtained = "0";
                        item.Paper_P1_Internal_Obtained = "0";
                        item.Paper_P2_Th_Obtained = "0";
                        item.Paper_P2_Internal_Obtained = "0";
                        item.Paper_P3_Th_Obtained = "0";
                        item.Paper_P3_Internal_Obtained = "0";
                        item.Paper_P4_Th_Obtained = "0";
                        item.Paper_P4_Internal_Obtained = "0";
                        item.Paper_P5_Th_Obtained = "0";
                        item.Paper_P5_Internal_Obtained = "0";
                        item.Result = "0";
                        item.Remarks = "0";
                        item.TotalObetand = "0";
                        //item.YearValue = "0";
                    
                    }
                }

            }
            if (action == "CustomSearch")
            {
                if (YearValue != "")
                {
                    CollegeExamCenterList finallist = new CollegeExamCenterList();
                    if (list.qlist.Count > 0)
                    {
                        if (list.qlist.FirstOrDefault().YearValue == "0")
                        {
                            finallist.qlist = list.qlist.Where(x => x.YearValue.ToString() == YearValue).ToList();
                        }
                        else
                        {
                            finallist.qlist = list.qlist.Where(x => x.YearValue.ToString() == YearValue).ToList();
                        }
                    }
                    else
                    {
                        finallist.qlist = list.qlist;
                    }
                    finallist.totalCount = finallist.qlist.Count.ToString();
                    return finallist;
                }

            }

            return list;
        }

        public List<CollegeExamCenter> GetTabulationRegister(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int streamcategoryid = 0,string enrollmentno="", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegister]", new {@Action= action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @streamcategoryid = streamcategoryid , @enrollmentno = enrollmentno, @ApplicationNo= ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        //public CollegeExamCenterList GetTabulationRegister(int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int pageIndex = 1, int PageSize = 20)
        //{
        //    CollegeExamCenterList list = new CollegeExamCenterList();
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //       // obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegister]", new { @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid }, commandType: CommandType.StoredProcedure).ToList();
        //        var obj = conn.QueryMultiple("[sp_TabulationRegister]", new { @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @PageSize = PageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
        //        list.qlist = obj.Read<CollegeExamCenter>().ToList();
        //        list.totalCount = obj.Read<string>().FirstOrDefault();
        //    }
        //    return list;
        //}
        public List<CollegeExamCenter> GetCollegeMarksSheet(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string YearValue = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_ExamManageNumberMarksSheet]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @Streemcategory = StreamCategory , @enrollmentno = enrollmentno, @YearValue = YearValue, }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public List<CollegeExamCenter> GetCollegeMarksSheet3rd(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string YearValue = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_ExamManageNumberMarksSheet3rdYear]", new { @Action= action,@EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @Streemcategory = StreamCategory , @enrollmentno = enrollmentno.TrimEnd().TrimStart(), @YearValue = YearValue, }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public List<CollegeExamCenter> GetTabulationRegister3rdyear(string action="",int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0,string enrollmentno="",string ApplicationNo="")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegister3rdyear]", new { @Action=action,@EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno= enrollmentno.TrimEnd().TrimStart(), @ApplicationNo= ApplicationNo.TrimEnd().TrimStart() }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public List<CollegeExamCenter> GetTabulationRegisterLLB(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterLLB]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }

        public List<CollegeExamCenter> GetTabulationRegisterBCA(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterBCA]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }

        public List<CollegeExamCenter> GetTabulationRegisterBED(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterBED]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public CollegeExamCenter InsertStudentExamNumber(string EducationType = "", string CourseCategoryID = "", string SessionID = "", string CourseYearID = "", string CollegeID = "", string SID_ = "", string Paper1_ = "", string Paper2_ = "", string Paper3_ = "", string Paper4_ = "", string PaperPratical_ = "", string Sub1Paper1_ = "", string Sub1Prac_ = "", string Sub2Paper1_ = "", string Sub2Prac_ = "", string CompPaper1_ = "", string CompPaper2_ = "", string GenPaper_ = "", int UserID = 0, string ResultFinal_ = "", string Remarks_ = "")
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Paper1_ = (Paper1_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper1_);
                var obj = conn.Query<CollegeExamCenter>("sp_ExamStudentExamNumber", new
                {
                    @Action = "InsertUpdate",
                    @EducationType = Convert.ToInt32(EducationType),
                    @CourseCategoryID = Convert.ToInt32(CourseCategoryID),
                    @SessionID = Convert.ToInt32(SessionID),
                    @CourseYearID = Convert.ToInt32(CourseYearID),
                    @CollegeID = Convert.ToInt32(CollegeID),
                    @HonoursP1 = Convert.ToString(Paper1_),
                    @Honoursp2 = Convert.ToString((Paper2_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper2_)),
                    @Honoursp3 = Convert.ToString((Paper3_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper3_)),
                    @Honoursp4 = Convert.ToString((Paper4_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper4_)),
                    @HonoursPer = Convert.ToString((PaperPratical_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : PaperPratical_)),
                    @Subsidiary1P1 = Convert.ToString((Sub1Paper1_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Sub1Paper1_)),
                    @Subsidiary1Per = Convert.ToString((Sub1Prac_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Sub1Prac_)),
                    @Subsidiary2P1 = Convert.ToString((Sub2Paper1_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Sub2Paper1_)),
                    @Subsidiary2Per = Convert.ToString((Sub2Prac_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Sub2Prac_)),
                    @CompulsoryP1 = Convert.ToString((CompPaper1_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : CompPaper1_)),
                    @CompulsoryP2 = Convert.ToString((CompPaper2_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : CompPaper2_)),
                    @GenStudies = Convert.ToString((GenPaper_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : GenPaper_)),
                    @InsertedBy = Convert.ToInt32(UserID),
                    @IpNumber = IP,
                    @SID = Convert.ToInt32(SID_),
                    @ResultFinal = Convert.ToInt32(ResultFinal_),
                    @Remarks = Remarks_,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }

        public CollegeExamCenter InsertStudentExamNumberLLB(string EducationType = "", string CourseCategoryID = "", string SessionID = "", string CourseYearID = "", string CollegeID = "", string SID_ = "", string Paper1_ = "", string Paper1Int_ = "", string Paper2_ = "", string Paper2Int_ = "", string Paper3_ = "", string Paper3Int_ = "", string Paper4_ = "", string Paper4Int_ = "", string Paper5_ = "", string Paper5Int_ = "", int UserID = 0, string ResultFinal_ = "", string Remarks_ = "")
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Paper1_ = (Paper1_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper1_);
                var obj = conn.Query<CollegeExamCenter>("sp_ExamStudentExamNumberLLB", new
                {
                    @Action = "InsertUpdate",
                    @EducationType = Convert.ToInt32(EducationType),
                    @CourseCategoryID = Convert.ToInt32(CourseCategoryID),
                    @SessionID = Convert.ToInt32(SessionID),
                    @CourseYearID = Convert.ToInt32(CourseYearID),
                    @CollegeID = Convert.ToInt32(CollegeID),

                    @C1_th = Convert.ToString(Paper1_),
                    @C1_int = Convert.ToString((Paper1Int_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper1Int_)),
                    @C2_th = Convert.ToString((Paper2_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper2_)),
                    @C2_int = Convert.ToString((Paper2Int_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper2Int_)),
                    @C3_th = Convert.ToString((Paper3_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper3_)),
                    @C3_int = Convert.ToString((Paper3Int_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper3Int_)),
                    @C4_th = Convert.ToString((Paper4_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper4_)),
                    @C4_int = Convert.ToString((Paper4Int_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper4Int_)),
                    @C5_th = Convert.ToString((Paper5_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper5_)),
                    @C5_int = Convert.ToString((Paper5Int_.Replace("AA", "A").Replace("AAA", "A").IndexOf('A') > -1 ? "A" : Paper5Int_)),                   
                    @InsertedBy = Convert.ToInt32(UserID),
                    @IpNumber = IP,
                    @SID = Convert.ToInt32(SID_),
                    @ResultFinal = Convert.ToInt32(ResultFinal_),
                    @Remarks = Remarks_,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }


        public CollegeExamCenter GetStudentExamNumber(string EducationType = "", string CourseCategoryID = "", string SessionID = "", string CourseYearID = "", string CollegeID = "", string SID_ = "")
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeExamCenter>("sp_ExamStudentExamNumber", new
                {
                    @Action = "ViewBySID",
                    @EducationType = Convert.ToInt32(EducationType),
                    @CourseCategoryID = Convert.ToInt32(CourseCategoryID),
                    @CollegeID = Convert.ToInt32(CollegeID),
                    @SessionID = Convert.ToInt32(SessionID),
                    @CourseYearID = Convert.ToInt32(CourseYearID),
                    @SID = Convert.ToInt32(SID_)
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }
        public CollegeExamCenter DateMonthYear(CollegeExamCenter obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obje = conn.Query<CollegeExamCenter>("sp_DateMonthYear", commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obje;
            }
        }
        public CollegeExamCenter StudentDetailForPrintmarkSheet( string enrollmentno = "", int insertby = 0)
        {
            CollegeExamCenter Obj = new CollegeExamCenter();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                string ip = CommonMethod.GetIPAddress();
                Obj = conn.Query<CollegeExamCenter>("[sp_ExamStudentExamNumber]", new
                {
                    @Action = "PrintCertificate",
                    @enrollmentno = enrollmentno,                  
                    @IPAddress = ip,
                    @InsertedBy = insertby,
                    @CertificateType="Marksheet",
                    @CertificateTypeID = 3,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Obj;
            }
        }

        public List<CollegeExamCenter> GetTabulationRegisterUGBack(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegister_UGBACk]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }


        public List<CollegeExamCenter> GetTabulationRegisterUGBack3rdyear(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public CollegeExamCenter GetBCASubjectCode(int CourceYearID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CollegeExamCenter obj = new CollegeExamCenter();
                obj = conn.Query<CollegeExamCenter>("[sp_GetBCASubjectcode]", new { @Courseyearid = CourceYearID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<CollegeExamCenter> GetTabulationRegisterPGBack(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterPG_back]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public CollegeExamCenter GetPGSubject(int CourceYearID = 0, int streamcategoryid = 0, int coursecategoryid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CollegeExamCenter obj = new CollegeExamCenter();
                obj = conn.Query<CollegeExamCenter>("[sp_GetPGSubject]", new { @Courseyearid = CourceYearID, @streamcategoryid = streamcategoryid, @coursecategoryid = coursecategoryid }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<CollegeExamCenter> GetTabulationRegisterLLBBack(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterLLB_back]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }


        public List<CollegeExamCenter> GetTabulationRegisterBEdBack(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterBEd_back]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        public List<CollegeExamCenter> GetTabulationRegisterBCABack(string action = "", int Session = 0, int Educationtype = 0, int CourseCategoryID = 0, int CollegeID = 0, int yearid = 0, int StreamCategory = 0, string enrollmentno = "", string ApplicationNo = "")
        {
            List<CollegeExamCenter> obj = new List<CollegeExamCenter>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeExamCenter>("[sp_TabulationRegisterBCA_back]", new { @Action = action, @EducationType = Educationtype, @CourseCategoryID = CourseCategoryID, @CollegeID = CollegeID, @SessionID = Session, @CourseYearID = yearid, @StreamCategoryid = StreamCategory, @enrollmentno = enrollmentno, @ApplicationNo = ApplicationNo }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }

    }
    public class CollegeExamCenterList
    {
        public List<CollegeExamCenter> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
