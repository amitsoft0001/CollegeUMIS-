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
   public class EmployeeSalaryMaster
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public int PaybandID { get; set; }
        public string BasicSalary { get; set; }
        public string HRA { get; set; }
        public string DA { get; set; }
        public int InsertBy { get; set; }
        public int SalaryMasterID { get; set; }
        public int AllowanceID { get; set; }
        public string Amount { get; set; }
        public bool AmountType { get; set; }
        public int hid { get; set; }
        public decimal DApercentage { get; set; }
        public decimal HRApercentge { get; set; }
        public string DAAmount { get; set; }
        public string DAAmountType { get; set; }
        public string DAID { get; set; }
        public string AlAmount { get; set; }
        public string AlAmountType { get; set; }
        public string AlID { get; set; }
        public int CollegeID { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string EncriptedID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string Designation { get; set; }
        public int aid { get; set; }
        public int DeductionID { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal NetTotal { get; set; }
        public List<Allowance_Master> Allowance_MasterList { get; set; }
        public List<Allowance_Master> Deduction_MasterList { get; set; }     

        public List<Allowance_Master> AllowanceNameList()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "AllowanceList",                    
                }, commandType: CommandType.StoredProcedure).ToList();
                return tbl;
            }
        }
        public List<Allowance_Master> DeductionNameList()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "DeductionList",
                }, commandType: CommandType.StoredProcedure).ToList();
                return tbl;
            }
        }
        public List<Allowance_Master> DeductionPercentageList(int empid=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "DeductionPercentageList",
                    @id=empid,
                }, commandType: CommandType.StoredProcedure).ToList();
                return tbl;
            }
        }
        public List<Allowance_Master> AllowancePercentageList(int empid = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "AllowancePercentageList",
                    @id = empid,
                }, commandType: CommandType.StoredProcedure).ToList();
                return tbl;
            }
        }
        public List<Payband_Master> PaybandNameList()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Payband_Master>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "PayBandList",
                }, commandType: CommandType.StoredProcedure).ToList();
                return tbl;
            }
        }
        public List<UserLogin> GetUsermenuList(int ID, int CollegeID = 0)
        {
            List<UserLogin> userList = new List<UserLogin>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                userList = conn.Query<UserLogin>("Sl_EmployeeSalary_Master", new { Action = "EmployeeList", @UserType = ID, @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
            }
            return userList;
        }
        public EmployeeSalaryMaster GetDaAndHraValue()
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<EmployeeSalaryMaster>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "DAHRAValue",
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public EmployeeSalaryMaster AddNewSalaryMaster(EmployeeSalaryMaster des)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<EmployeeSalaryMaster>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "Insert",
                    @id=des.Id,
                    @EmployeeID = des.EmployeeID,
                    @PaybandID=des.PaybandID,
                    @CollegeID = des.CollegeID,
                    @BasicSalary = des.BasicSalary,
                    @InsertBy = des.InsertBy,
                    @HRA=des.HRA,
                    @DA=des.DA,
                    @DAAmount=des.DAAmount,
                   @DAAmountType=des.DAAmountType,                 
                    @DAID = des.DAID,
                    @AlAmount = des.AlAmount,
                    @AlAmountType = des.AlAmountType,
                    @AlID = des.AlID,
                    @IPAddress = ip,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EmployeeSalaryMasterList EmployeeSalarydetailList(int pageIndex1 = 1, int pageSize1 = 25, string collegeID = "", string EmployeeID = "", string PaybandID = "",string Name="",int usertype=0)
        {
            EmployeeSalaryMasterList list = new EmployeeSalaryMasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sl_EmployeeSalary_Master]", new { @Action = "view", @PageIndex = pageIndex1, @PageSize = pageSize1, @CollegeID = collegeID, @EmployeeID = EmployeeID, @PaybandID = PaybandID , @Name= Name, @UserType=usertype }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EmployeeSalaryMaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EmployeeSalaryMaster getdetailsByID(int id=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<EmployeeSalaryMaster>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "GetByID",
                    @Id=id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public EmployeeSalaryMaster SalaryMasterDetail(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<EmployeeSalaryMaster>("[Sl_EmployeeSalary_Master]", new
                {
                    @Action = "SalaryMaster",
                    @Id = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
    }
    public class EmployeeSalaryMasterList
    {        public List<EmployeeSalaryMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
