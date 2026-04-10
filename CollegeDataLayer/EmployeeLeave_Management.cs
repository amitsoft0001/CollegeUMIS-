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
   public class EmployeeLeave_Management
    {
        public int ID { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public string Create_Date { get; set; }
        public int LeaveType { get; set; }
        public string Subject { get; set; }
        public string Reason { get; set; }
        public string Day { get; set; }
        public int InsertedBy { get; set; }
        public int CollegeID { get; set; }
        public int EmployeeID { get; set; }
        public string IPAddress { get; set; }
        public string DayName { get; set; }
        public string Modify_Date { get; set; }
        public int LeaveStatus { get; set; }//0 pending ,1 Approve,2 rejected

        public string LeaveName { get; set; }
        public int LID { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string Name { get; set; }
        public string EncriptedID { get; set; }
        public string Email { get; set; }
        public string EmployeeCode { get; set; }
        public int halftime { get; set; }
        public string halfdaytime { get; set; }
        public EmployeeLeave_Management AddLeave(EmployeeLeave_Management obj)
        {
            string ip = CommonMethod.GetIPAddress();           
            if (obj.Fromdate == null)
            {
                obj.Fromdate = "";
            }
            else
            {
                obj.Fromdate = DateTime.ParseExact(obj.Fromdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }
            if (obj.Todate == null)
            {
                obj.Todate = "";
            }
            else
            {
                obj.Todate = DateTime.ParseExact(obj.Todate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var res = conn.Query<EmployeeLeave_Management>("[sp_College_Leave_Management]", new
                {
                    @Action = "Insert",
                    @ID = obj.ID,
                    @Fromdate = obj.Fromdate,
                    @Todate = obj.Todate,
                    @Reason = obj.Reason,
                    @Subject = obj.Subject,
                    @InsertedBy = obj.InsertedBy,
                    @IPAddress = ip,
                    @LeaveType = obj.LeaveType,
                    @CollegeID = obj.CollegeID,
                    @EmployeeID = obj.EmployeeID,
                    @LeaveStatus = obj.LeaveStatus,
                    @day = obj.Day,
                    @halftime=obj.halftime

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return res;




            }
        }

        public List<EmployeeLeave_Management> getleaveTypeList()
        {
            List<EmployeeLeave_Management> userList = new List<EmployeeLeave_Management>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_College_Leave_Management]", new { @Action = "LeveType" }, commandType: CommandType.StoredProcedure);
                userList = obj.Read<EmployeeLeave_Management>().ToList();

            }
            return userList;
        }
        
        public EmployeeLeave_ManagementList EmployeeLeaveList(int pageIndex = 1, int pageSize = 25, string Month = "", string Year = "", string EmployeeID = "",string CollegeID="")
         {
            if (Month == "13")
            {
                if( Year != "1951")
                Month = "";
              
            }
            if (Month != "13" )
            {
                if(Year == "1951")
                Year = "";
            }
            if (Month == "13")
                Month = DateTime.Now.Month.ToString();
            if (Year == "1951")
                Year = DateTime.Now.Year.ToString();
            EmployeeLeave_ManagementList list = new EmployeeLeave_ManagementList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_College_Leave_Management]", new { @Action = "view", @PageIndex = pageIndex, @pageSize = pageSize, @Month = Month, @Year = Year, @EmployeeID = EmployeeID, @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EmployeeLeave_Management>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EmployeeLeave_Management GetByEmployeeID(int id = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EmployeeLeave_Management>("sp_College_Leave_Management", new
                {
                    Action = "GetByID",
                    @ID = id,


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public EmployeeLeave_ManagementList EmployeePendingLeaveList(int pageIndex = 1, int pageSize = 25,  string CollegeID = "", string Month = "", string Year = "",string Name="",string LeaveStatus="")
        {
         
            EmployeeLeave_ManagementList list = new EmployeeLeave_ManagementList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_College_Leave_Management]", new { @Action = "PendingLeave", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID = CollegeID, @Month = Month, @Year = Year,@Name=Name, @LeaveStatus = LeaveStatus }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EmployeeLeave_Management>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EmployeeLeave_Management getLeaveByID(int id = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EmployeeLeave_Management>("sp_College_Leave_Management", new
                {
                    Action = "GetLeave",
                    @ID = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public EmployeeLeave_Management Verifyleave(int ID = 0, int ModifyBy = 0)
        {
            EmployeeLeave_Management obj = new EmployeeLeave_Management();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<EmployeeLeave_Management>("[sp_College_Leave_Management]", new
                {
                    @Action = "ApproveLeave",
                    @ID = ID,
                    @ModifyBy= ModifyBy,
                    @LeaveStatus=1,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public EmployeeLeave_Management Rejectleave(int ID = 0, int ModifyBy = 0)
        {
            EmployeeLeave_Management obj = new EmployeeLeave_Management();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<EmployeeLeave_Management>("[sp_College_Leave_Management]", new
                {
                    @Action = "RejectLeave",
                    @ID = ID,
                    @ModifyBy = ModifyBy,
                    @LeaveStatus = 2,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public EmployeeLeave_Management deleteLeaveByID(int id = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EmployeeLeave_Management>("sp_College_Leave_Management", new
                {
                    Action = "DeleteLeave",
                    @ID = id,


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }
    public class EmployeeLeave_ManagementList
    {
        public List<EmployeeLeave_Management> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
