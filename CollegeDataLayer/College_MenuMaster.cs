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
   public class College_MenuMaster
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int MenuLevel { get; set; }
        public int ParentID { get; set; }       
        public string MenuLink { get; set; }
        public string MenuStr { get; set; }
        public bool IsActive { get; set; }       
        public int orderposition { get; set; }
        public string controllername { get; set; }
        public string viewnamename { get; set; }
        public string class1 { get; set; }
        public bool checkchecked { get; set; }

        public string LastLogin { get; set; }

        public IList<College_MenuMaster> getCollegeMenuList(string action)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<College_MenuMaster>("[sp_CollegeMenu]", new { @Action=action }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public IList<College_MenuMaster> getmenuadminlist(int CollegeID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<College_MenuMaster>("[cl_Sp_CreateEmployee]", new { @Action = "adminmenumaster", @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public IList<College_MenuMaster> getempmenulist(int CollegeID=0, int ID=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<College_MenuMaster>("[cl_Sp_CreateEmployee]", new { @Action = "menumaster" , @CollegeID = CollegeID,@ID=ID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

    }
}
