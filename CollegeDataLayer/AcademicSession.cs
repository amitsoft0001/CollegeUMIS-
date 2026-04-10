using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer
{
  public  class AcademicSession
    {
        public int ID { get; set; }
        public string Session { get; set; }
        public int IsActive { get; set; }
        public int IsDelete { get; set; }
        public string year { get; set; }
        public bool IsCurrent { get; set; }
        public int nID { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
        public string Month { get; set; }
        public decimal MinumPercentage { get; set; }
        public string sessionname { get; set; }
        public int sessionid { get; set; }
        public AcademicSession GetAcademiccurrentSession()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<AcademicSession>("sp_AcademicSession", new { Action = "View"}, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public AcademicSession AddDetail(AcademicSession obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obje = conn.Query<AcademicSession>("sp_AcademicSession", new { Action = "add" ,@ID=obj.nID,
                    @Session=obj.Session,
                    @year=obj.year

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obje;
            }

        }
               
        //public AcademicSession GetSession()
        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var obj = conn.Query<AcademicSession>("[sp_GetSession]", new { Action = "View" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        return obj;
        //    }

        //}
        public List<AcademicSession> GetSession()
        {
            List<AcademicSession> obj = new List<AcademicSession>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                 obj = conn.Query<AcademicSession>("[sp_GetSession]", new { Action = "View" }, commandType: CommandType.StoredProcedure).ToList();
               
            }
            return obj;
        }
        public List<AcademicSession> GetSessionList(int CourseCategoryID=0)
        {
            List<AcademicSession> obj = new List<AcademicSession>();          
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<AcademicSession>("[sp_StudentAchievement]", new { Action = "Session", @CourseCategoryID= CourseCategoryID }, commandType: CommandType.StoredProcedure).ToList();

            }
           
            return obj;
        }
        public List<AcademicSession> GetSessionListCourseWise(int CourseCategoryID=0)
        {
            List<AcademicSession> obj = new List<AcademicSession>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<AcademicSession>("[sp_StudentAchievement]", new { Action = "SessionList", @CourseCategoryID= CourseCategoryID }, commandType: CommandType.StoredProcedure).ToList();

            }
            return obj;
        }

        public AcademicSession AttendancePercentage()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obje = conn.Query<AcademicSession>("sp_AttendancePercentage", new
                {
                    

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obje;
            }

        }
        public List<AcademicSession> GetMonth()
        {
            List<AcademicSession> obj = new List<AcademicSession>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<AcademicSession>("[sp_Month]", new { Action = "View" }, commandType: CommandType.StoredProcedure).ToList();

            }
            return obj;
        }
    }
}
