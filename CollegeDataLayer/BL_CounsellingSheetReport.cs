using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataLayer
{
    public class BL_CounsellingSheetReport
    {
        public string CollegeName { get; set; }
        public string TotasheetGEN { get; set; }
        public string TotalHandiGEN { get; set; }
        public string consumeseatHinGEN { get; set; }
        public string consumeseatGEN { get; set; }
        public string TotaSCsheet { get; set; }
        public string TotalHandiCS { get; set; }
        public string consumeseatHinCS { get; set; }
        public string consumeseatSC { get; set; }
        public string TotasheetTS { get; set; }
        public string TotalHandiST { get; set; }
        public string consumeseatHinST { get; set; }
        public string consumeseatST { get; set; }
        public string TotasheetBC1 { get; set; }
        public string TotalHandiBC1 { get; set; }
        public string consumeseatHinBC1 { get; set; }
        public string consumeseatBC1 { get; set; }
        public string TotasheetBC2 { get; set; }
        public string TotalHandiBC2 { get; set; }
        public string consumeseatHinBC2 { get; set; }
        public string consumeseatBC2 { get; set; }
        public string TotasheetWBC { get; set; }
        public string TotalHandiWBC { get; set; }
        public string consumeseatHinWBC { get; set; }
        public string consumeseatWBC { get; set; }
        public string TotasheetNCC { get; set; }
        public string TotalHandiNCC { get; set; }
        public string consumeseatHinNCC { get; set; }
        public string consumeseatNCC { get; set; }
        public string TotasheetSPORTS { get; set; }
        public string TotalHandiSPORTS { get; set; }
        public string consumeseatHinSPORTS { get; set; }
        public string consumeseatSPORTS { get; set; }
        public string TotasheetEXSERVICEMEN { get; set; }
        public string TotalHandiEXSERVICEMEN { get; set; }
        public string consumeseatHinEXSERVICEMEN { get; set; }
        public string consumeseatEXSERVICEMEN { get; set; }
        public string TotasheetUSTAFF { get; set; }
        public string TotalHandiUSTAFF { get; set; }
        public string consumeseatHinUSTAFF { get; set; }
        public string consumeseatUSTAFF { get; set; }



        public string RemainingGEN { get; set; }
        public string RemainingSC { get; set; }
        public string RemainingST { get; set; }
        public string RemainingBC1 { get; set; }
        public string RemainingBC2 { get; set; }
        public string RemainingWBC { get; set; }
        public string RemainingNCC { get; set; }
        public string RemainingSports { get; set; }
        public string RemainingExserviceman { get; set; }
        public string RemainingUniversityStaff { get; set; }


        public string CollegeID { get; set; }
        public string streamCategory { get; set; }

        public string TotasheetGEW { get; set; }
        public string TotalHandGEW { get; set; }
        public string consumeseathandiGEW { get; set; }
        public string consumeseatGEW { get; set; }
        public string RemainingGEW { get; set; }

        public string RemainingGENHandi { get; set; }
        public string RemainingSChandi { get; set; }
        public string RemainingSTHandi { get; set; }
        public string RemainingBC1Handi { get; set; }

        public string RemainingBC2Handi { get; set; }
        public string RemainingWBChandi { get; set; }
        public string RemainingNCCHandi { get; set; }

        public string RemainingSportshandi { get; set; }
        public string RemainingExservicemanHandi { get; set; }
        public string RemainingUniversityStaffhandi { get; set; }
        public string RemainingGEWHandi { get; set; }
        public string Education { get; set; }

        public List<BL_CounsellingSheetReport> GetCounsellingSheetReportCollegeWise2(string EducationType = "", string ddlsession = "", string Coursetype = "", string CollegeID = "")
        {
            List<BL_CounsellingSheetReport> objdata = new List<BL_CounsellingSheetReport>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var OBJ = conn.Query<BL_CounsellingSheetReport>("sp_CounsellingSheetReportCollegeWaise2", new { @sessionid = ddlsession, @coursecategoryid = Coursetype, @collegeid = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return OBJ;
            }
        }
        public List<BL_CounsellingSheetReport> GetCounsellingSheetReport(string EducationType = "", string ddlsession = "", string Coursetype = "", string Subject = "", string CollegeID = "")
        {
            List<BL_CounsellingSheetReport> objdata = new List<BL_CounsellingSheetReport>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var OBJ = conn.Query<BL_CounsellingSheetReport>("[cl_CounsellingSheetReport]", new { @sessionid = ddlsession, @coursecategoryid = Coursetype,  @streamcategoryid = Subject , @collegeid = CollegeID}, commandType: CommandType.StoredProcedure).ToList();
                return OBJ;
            }
        }

        public List<BL_CounsellingSheetReport> After1COUNSELLINGREMSEAT(string EducationType = "", string ddlsession = "", string Coursetype = "", string CollegeID = "")
        {
            List<BL_CounsellingSheetReport> objdata = new List<BL_CounsellingSheetReport>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var OBJ = conn.Query<BL_CounsellingSheetReport>("sp_After1COUNSELLINGREMSEAT", new { @sessionid = ddlsession, @coursecategoryid = Coursetype, @collegeid = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return OBJ;
            }
        }
        public List<BL_CounsellingSheetReport> GetCounsellingSheetReportCollegeWise(string EducationType = "", string ddlsession = "", string Coursetype = "", string CollegeID = "")
        {
          
            List<BL_CounsellingSheetReport> objdata = new List<BL_CounsellingSheetReport>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var OBJ = conn.Query<BL_CounsellingSheetReport>("sp_CounsellingSheetReportCollegeWaise", new { @sessionid = ddlsession, @coursecategoryid = Coursetype, @collegeid = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return OBJ;
            }
        }
        public List<BL_CounsellingSheetReport> GetCounsellingSheetReportCollegeWise3(string EducationType = "", string ddlsession = "", string Coursetype = "", string CollegeID = "")
        {
            List<BL_CounsellingSheetReport> objdata = new List<BL_CounsellingSheetReport>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var OBJ = conn.Query<BL_CounsellingSheetReport>("sp_CounsellingSheetReportCollegeWaise3", new { @sessionid = ddlsession, @coursecategoryid = Coursetype, @collegeid = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return OBJ;
            }
        }
    }
}
