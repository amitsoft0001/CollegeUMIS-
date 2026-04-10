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
    public class Student_AchievementManagement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AchievementMasterID { get; set; }
        public int SID { get; set; }
        public string Description { get; set; }
        public string Createdate { get; set; }
        public int InsertedBy { get; set; }
        public string IPaddress { get; set; }
        public int SessionID { get; set; }
        public int CollegeID { get; set; }
        public string DocumentURl { get; set; }
        public string modify_date { get; set; }
        public int modify_By { get; set; }
        public string modify_IPaddress { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public string hfile { get; set; }
        public string ApplicationNo { get; set; }
        public string CourseCategory { get; set; }
        public string SessionName { get; set; }
        public string AchievementName { get; set; }
        public string CollegeName { get; set; }
        public string EnrollmentNo { get; set; }
        public string EncriptedID { get; set; }
        public int hid { get; set; }
        public int program { get; set; }
        public int Course { get; set; }     
        public List<Student_AchievementManagement> StudentList(int SessionID =0, int EducationTypeID = 0, int CourseCategoryID=0,int CollegeID=0)
        {
            List<Student_AchievementManagement> obj = new List<Student_AchievementManagement>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {                
                obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new { @Action = "StudentList",@SessionID= SessionID,@EducationTypeID= EducationTypeID , @CourseCategoryID= CourseCategoryID , @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<Student_AchievementManagement> StudentListBYID(int SID=0)
        {
            List<Student_AchievementManagement> obj = new List<Student_AchievementManagement>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new { @Action = "StudentListBYID", @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
         public List<Student_AchievementManagement> AchievementListForEdit(int SID = 0)
        {

            List<Student_AchievementManagement> obj = new List<Student_AchievementManagement>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new { @Action = "Achievement", @SID = SID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<Student_AchievementManagement> AchievementList(int SID=0)
        {

            List<Student_AchievementManagement> obj = new List<Student_AchievementManagement>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new { @Action = "AchievementList", @SID= SID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public Student_AchievementManagement SaveAchievementDetail(Student_AchievementManagement ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @Description =ob.Description,
                    @IPaddress = IP,
                    @DocumentURl = ob.DocumentURl,
                    @AchievementMasterID = ob.AchievementMasterID,
                    @SID = ob.SID,
                    @InsertedBy = ob.InsertedBy,
                    @CollegeID = ob.CollegeID,
                   @SessionID= ob.SessionID,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Student_AchievementManagementList GETachievementList(int pageIndex = 1, int pageSize = 25, string session = "", string EducationTypeID = "", string Course = "", string CollegeID = "", string SID = "",string Name="", string RegistrationNo = "")
        {

            Student_AchievementManagementList list = new Student_AchievementManagementList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_StudentAchievement]", new { @Action = "StudentAchievementListDetail", @PageIndex = pageIndex, @pageSize = pageSize,@SessionID = session,@EducationTypeID = EducationTypeID, @CourseCategoryID = Course, @CollegeID = CollegeID, @SID = SID, @Name = Name , @RegistrationNo = RegistrationNo }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Student_AchievementManagement>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Student_AchievementManagementList achievementList(int pageIndex = 1, int pageSize = 25, string CollegeID = "", string SID = "", string AchievementMasterID = "")
        {

            Student_AchievementManagementList list = new Student_AchievementManagementList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_StudentAchievement]", new { @Action = "AchievementListDetail", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID = CollegeID, @SID = SID, @AchievementMasterID = AchievementMasterID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Student_AchievementManagement>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Student_AchievementManagement findStudentDetail(int Id=0)
        {
            Student_AchievementManagement obj = new Student_AchievementManagement();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new { @Action = "StudentDetail",@ID= Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public List<Student_AchievementManagement> findStudentachievementDetail(int SId = 0)
        {
            List<Student_AchievementManagement> obj = new List<Student_AchievementManagement>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<Student_AchievementManagement>("[sp_StudentAchievement]", new { @Action = "StudentAchievementDetail", @SID = SId }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
    }
    public class Student_AchievementManagementList
    {
        public List<Student_AchievementManagement> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class Achievementmaster
    {
        public int Id { get; set; }
        public string AchievementName { get; set; }
        public bool IsActive { get; set; }
        public int InsertedBy { get; set; }
        public string IPAddress { get; set; }
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int SNO { get; set; }
        public List<Achievementmaster> AchievementList { get; set; }
        public Achievementmaster CheckAchievementName(string name = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Achievementmaster>("[Sp_AchievementMaster]", new
                {
                    @Action = "AchievementByName",
                    @AchievementName = name
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public Achievementmaster AddNewAchievement(Achievementmaster des)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<Achievementmaster>("[Sp_AchievementMaster]", new
                {
                    @Action = "Insert",
                    @Id = des.Id,
                    @AchievementName = des.AchievementName,                  
                    @InsertedBY = des.InsertedBy,
                    @IPAddress = ip,
                    @CollegeID=des.CollegeID,


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;




            }
        }
        public AchievementmasterList AchievementDetailList(string CollegeID = "",int pageIndex=0,int pageSize=25)
        {
            AchievementmasterList list = new AchievementmasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj= conn.QueryMultiple("[Sp_AchievementMaster]", new { @Action = "View", @CollegeID = CollegeID,@PageSize= pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Achievementmaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }   
    }
    public class AchievementmasterList
    {
        public List<Achievementmaster> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
