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
   public class FeeStructure
    {
        public int ID { get; set; }
        public decimal amount { get; set; }
        public string headname { get; set; }
        public int Sessionid { get; set; }
        public string Sessionname { get; set; }
        public int coursecategoryid { get; set; }
        public string coursecategoryName { get; set; }
        public int CollegeID { get; set; }
        public int CastCategory { get; set; }
        public string castecategoryname { get; set; }
        public string CourseCategorycast { get; set; }        
        public string Headidlist { get; set; }
        public string amountList { get; set; }
        public int headid { get; set; }
        public int InsertedBy { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string IPAddress { get; set; }
        public string collegeName { get; set; }
        public int castecategory { get; set; }
        public string streamCategory { get; set; }
        public string EducationTypeName { get; set; }
        public string feetype { get; set; }
        public int CourseYearID { get; set; }
        public string YearName { get; set; }
        public decimal ConcessionFee { get; set; }
        public decimal latefee { get; set; }
        public List<FeeStructure> getfeedetail(string courseid = "", string session = "", int college = 0,string CastCategory="")
        {
            int cid = (courseid != "" ? Convert.ToInt32(courseid) : 0);
            int sid = (session != "" ? Convert.ToInt32(session) : 0);
            int castid = (CastCategory != "" ? Convert.ToInt32(CastCategory) : 0);
            AcademicSession ob = new AcademicSession();
           
            List<FeeStructure> objdata = new List<FeeStructure>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<FeeStructure>("sp_FeeReport_College", new { @Action= "feeReport",
                    @collegeid = college,
                    @SessionID  = sid,
                    @courseCategoryid= cid,
                    @castecategory = castid,

                }, commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }
        public FeeStructureList FeestructureDetail(int pageSize, int pageIndex, string CourseCategory = "", string Session = "", string College = "", int castecategory = 0,int Subject=0,int EducationTypeID=0, string FeeType = "", string CourseYearID = "")
        {
            FeeStructureList list = new FeeStructureList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_FeeReport_College]", new { @Action = "FeeStructureList", @pageIndex = pageIndex, @pageSize = pageSize, @courseCategoryid = CourseCategory, @SessionID = Session, @collegeid = College, @castecategory = castecategory , @streamcategoryid = Subject, @EducationTypeID = EducationTypeID, @feetypeid = FeeType, @CourseYearID = CourseYearID }, commandType: CommandType.StoredProcedure);
                //var obj = conn.QueryMultiple("[sp_FeeReport_College]", new { @Action = "FeeStructureList", @pageIndex = pageIndex, @pageSize = pageSize, @EducationTypeID = EducationTypeID, @courseCategoryid = CourseCategory, @SessionID = Session, @collegeid = College, @castecategory = castecategory, @streamcategoryid = Subject, @feetypeid = FeeType, @CourseYearID = CourseYearID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<FeeStructure>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public List<Recruitment> RecruitmentdetailforPrint(string coursetype = "", string subject = "", string session = "", string collegeID = "", string cast = "", string seatType = "")
        {

            List<Recruitment> list = new List<Recruitment>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                list = conn.Query<Recruitment>("[Sp_RecruitmentReport]", new { Action = "Print", @coursetype = coursetype, @subject = subject, @session = session, @collegeID = collegeID, @cast = cast, @seatType = seatType }, commandType: CommandType.StoredProcedure).ToList();

            }
            return list;
        }
        public List<FeeStructure> FeeTypeList()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<FeeStructure>("sp_FeeReport_College", new
                {
                    Action = "FeetypeList",
                }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public AcademicSession GetSessionBYID(int session)
        {
            AcademicSession objdata = new AcademicSession();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<AcademicSession>("Sp_RecruitmentReport", new { Action = "Session", @session = session }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }
    }

    public class FeeStructureList
    {
        public List<FeeStructure> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
