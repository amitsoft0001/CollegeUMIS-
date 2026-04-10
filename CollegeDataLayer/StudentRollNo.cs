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
   public class StudentRollNo
    {
        public int RollNoId { get; set; }
        public int Sid { get; set; }
        public string RollNo { get; set; }      
        public string CurrentYear { get; set; }
        public string Semster { get; set; }
        public bool IsActive { get; set; }
        public int CourseCategoryID { get; set; }
        public int Session { get; set; }
        public int CollegeID { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string StudentName { get; set; }
        public string CourseCategory { get; set; }
        public string StreamCategory { get; set; }
        public string ApplicationNo { get; set; }
        public string SessionName { get; set; }
        public StudentRollNo InsertStudentRollNo(string CollegeID="", string CourseCategoryID="")
        {
            AcademicSession ac = new AcademicSession();
            int session = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentRollNo>("[sp_Student_RollNo]", new
                {
                    @Action = "Insert",              
                    @CollegeID= CollegeID,
                    @CourseCategoryID= CourseCategoryID,
                    @Session= session
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public StudentRollNo GETStudentRollNoStatus(string CollegeID = "", string CourseCategoryID = "")
        {
            AcademicSession ac = new AcademicSession();
            int session = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<StudentRollNo>("[sp_Student_RollNo]", new
                {
                    @Action = "GetRollNoStatus",
                    @CollegeID = CollegeID,
                    @CourseCategoryID = CourseCategoryID,
                    @Session = session
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }
        public StudentRollNoList RollNoListOfStd(int pageIndex = 1, int pageSize = 25, string CollegeID = "", string EducationTypeID = "", string CourseCategoryID = "", string Name = "", string RollNo = "", string session = "")
        {

            StudentRollNoList list = new StudentRollNoList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Student_RollNo]", new { @Action = "RollNoDetail",
                    @PageIndex = pageIndex,
                    @pageSize = pageSize,
                    @CollegeID = CollegeID,
                    @EducationTypeID = EducationTypeID,
                    @CourseCategoryID = CourseCategoryID,
                    @Name = Name, @RollNovar = RollNo ,
                    @Session = session }, 
                    
                    commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<StudentRollNo>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    public class StudentRollNoList
    {
        public List<StudentRollNo> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
