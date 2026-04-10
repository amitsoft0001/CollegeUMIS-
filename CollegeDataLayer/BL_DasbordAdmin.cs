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

namespace DataLayer
{
    public class BL_DasbordAdmin
    {
        public string BA_GEN { get; set; }
        public string BA_SC { get; set; }
        public string BA_ST { get; set; }
        public string BA_BC1 { get; set; }
        public string BA_BC2 { get; set; }
        public string BA_WBC { get; set; }
        public string BA_NCC { get; set; }
        public string BA_SPOARTQUOTA { get; set; }
        public string BA_EX_SERVICEMEN { get; set; }
        public string BA_WARD { get; set; }
        public string BA_GEW { get; set; }
        public string BSC_GEN { get; set; }
        public string BSC_SC { get; set; }
        public string BSC_ST { get; set; }
        public string BSC_BC1 { get; set; }
        public string BSC_BC2 { get; set; }
        public string BSC_WBC { get; set; }
        public string BSC_NCC { get; set; }
        public string BSC_SPOARTQUOTA { get; set; }
        public string BSC_EX_SERVICEMEN { get; set; }
        public string BSC_WARD { get; set; }
        public string BSC_GEW { get; set; }
        public string BCOME_GEN { get; set; }
        public string BCOME_SC { get; set; }
        public string BCOME_ST { get; set; }
        public string BCOME_BC1 { get; set; }
        public string BCOME_BC2 { get; set; }
        public string BCOME_WBC { get; set; }
        public string BCOME_NCC { get; set; }
        public string BCOME_SPOARTQUOTA { get; set; }
        public string BCOME_EX_SERVICEMEN { get; set; }
        public string BCOME_WARD { get; set; }
        public string BCOME_GEW { get; set; }
        public string BATOTAL { get; set; }
        public string BSCTOTAL { get; set; }
        public string BCATOTAL { get; set; }
        public string BBATOTAL { get; set; }
        public string ECOMETOTAL { get; set; }
        public string SessionName { get; set; }
        public string BCA_GEN { get; set; }
        public string BCA_SC { get; set; }
        public string BCA_ST { get; set; }
        public string BCA_BC1 { get; set; }
        public string BCA_BC2 { get; set; }
        public string BCA_WBC { get; set; }
        public string BCA_NCC { get; set; }
        public string BCA_SPOARTQUOTA { get; set; }
        public string BCA_EX_SERVICEMEN { get; set; }
        public string BBA_GEN { get; set; }
        public string BCA_WARD { get; set; }
        public string BCA_GEW { get; set; }
        public string BBA_SC { get; set; }
        public string BBA_ST { get; set; }

        public string BBA_BC1 { get; set; }
        public string BBA_BC2 { get; set; }
        public string BBA_WBC { get; set; }
        public string BBA_NCC { get; set; }
        public string BBA_SPOARTQUOTA { get; set; }
        public string BBA_EX_SERVICEMEN { get; set; }
        public string BBA_WARD { get; set; }
        public string BBA_GEW { get; set; }
        public int collegeid { get; set; }
        public string Collegename { get; set; }
        public string Stream { get; set; }
        public int streamcategoryid { get; set; }
        public string SubjectName { get; set; }
        public int TotalSeat { get; set; }
        public int TotalApply { get; set; }
        public int TotalAdmission { get; set; }
        public int coursecategoryid { get; set; }
        public string collegecode { get; set; }
        public int TotalDocVerify { get; set; }
        
        public int TotalApplybycourse { get; set; }
        public BL_DasbordAdmin GetTotalFormList()
        {
            BL_DasbordAdmin sub = new BL_DasbordAdmin();
            BL_DasbordAdmin OBJ = new BL_DasbordAdmin();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                OBJ = con.Query<BL_DasbordAdmin>("[sp_GetTotalApplicationFormDetail]", commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return OBJ;
        }
        public BL_DasbordAdminList UGAdmissionDetails(int pageIndex1 = 1, int pageSize1 = 25/*,int CollegeID=0*/)
        {

            BL_DasbordAdminList list = new BL_DasbordAdminList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_CollegeWaise_ugadmissiondetails]", new { Action = "view", PageIndex = pageIndex1, pageSize = pageSize1/*, @collegeid= CollegeID */}, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<BL_DasbordAdmin>().ToList();
                
            }
            return list;
        }
        public BL_DasbordAdminList CollegeAdmissionDetails(int CollegeID=0,int SessionID=0)
        {
            BL_DasbordAdminList list = new BL_DasbordAdminList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("sp_CollegeAdmissionDetails", new { Action = "View",  @collegeid= CollegeID, @sessionid= SessionID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<BL_DasbordAdmin>().ToList();
            }
            return list;
        }        
    }
    public class BL_DasbordAdminList
    {
        public List<BL_DasbordAdmin> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
