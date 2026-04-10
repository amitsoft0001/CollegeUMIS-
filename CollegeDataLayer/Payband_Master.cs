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
   public class Payband_Master
    {
        public int id { get; set; }
        public decimal fromamount { get; set; }
        public decimal toamount { get; set; }
        public int insertby { get; set; }
        public string Msg { get; set; }
        public int Status { get; set; }
        public int SNO { get; set; }
        public int hid { get; set; }
        public string EncriptedID { get; set; }
        public string PaybandName { get; set; }
        public List<Payband_Master> PaybandMasterList { get; set; }
        public List<Payband_Master> qlist { get; set; }
        public string totalCount { get; set; }
        public Payband_Master CheckPaybandName(string fromamount = "",string toamount="")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Payband_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "PaybandByValue",
                    @fromamount = fromamount,
                    @toamount= toamount
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public Payband_Master PaybandByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Payband_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "PaybandByID",
                    @id=id,                    
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public Payband_Master AddNewPayband(Payband_Master des)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<Payband_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "Insert",
                    @fromamount = des.fromamount,
                    @toamount = des.toamount,
                    @insertby = des.insertby,
                    @IPAddress = ip,
                    @id=des.id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Payband_Master PaybandDetailList( int pageIndex = 0, int pageSize = 25)
        {
            Payband_Master list = new Payband_Master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sl_Payband_Master]", new { @Action = "View",  @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Payband_Master>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    public class DA_Master
    {
        public int id { get; set; }
        public decimal percentage { get; set; }
        public string OrderNo { get; set; }
        public string OrderFile { get; set; }
        public int InsertBy { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int SNO { get; set; }
        public bool islast { get; set; }
        public int DAid { get; set; }
        public string hfile { get; set; }
        public string EncriptedID { get; set; }
        public string AddDate { get; set; }
        public string IsCurrent { get; set; }
        public List<DA_Master> DA_MasterList { get; set; }
        public List<DA_Master> qlist { get; set; }
        public string totalCount { get; set; }
        public DA_Master DAByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<DA_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "DAByID",
                    @id = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public DA_Master AddNewDA(DA_Master des)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<DA_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "DA_MasterInsert",
                    @id=des.id,
                    @percentage = des.percentage,
                    @OrderFile = des.OrderFile,
                    @OrderNo = des.OrderNo,
                    @insertby = des.InsertBy,
                    @IPAddress = ip,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public DA_Master DADetailList(int pageIndex = 0, int pageSize = 25)
        {
            DA_Master list = new DA_Master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sl_Payband_Master]", new { @Action = "ViewDA", @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<DA_Master>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    public class HRA_Master
    {
        public int id { get; set; }
        public decimal percentage { get; set; }
        public string OrderNo { get; set; }
        public string OrderFile { get; set; }
        public int InsertBy { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int SNO { get; set; }
        public bool islast { get; set; }
        public int DAid { get; set; }
        public string hfile { get; set; }
        public string EncriptedID { get; set; }
        public List<HRA_Master> HRA_MasterList { get; set; }
        public List<HRA_Master> qlist { get; set; }
        public string AddDate { get; set; }
        public string IsCurrent { get; set; }
        public string totalCount { get; set; }
        public HRA_Master HRAByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<HRA_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "HRAByID",
                    @id = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public HRA_Master AddNewHRA(HRA_Master des)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<HRA_Master>("[Sl_Payband_Master]", new
                {
                    @Action = "HRA_MasterInsert",
                    @id = des.id,
                    @percentage = des.percentage,
                    @OrderFile = des.OrderFile,
                    @OrderNo = des.OrderNo,
                    @insertby = des.InsertBy,
                    @IPAddress = ip,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public HRA_Master HRADetailList(int pageIndex = 0, int pageSize = 25)
        {
            HRA_Master list = new HRA_Master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sl_Payband_Master]", new { @Action = "ViewHRA", @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<HRA_Master>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    public class Allowance_Master
    {
        public int id { get; set; }
        public string allowancename { get; set; }
        public int InsertBy { get; set; }
        public string deductionname { get; set; }
        public string Msg { get; set; }
        public int Status { get; set; }
        public int SNO { get; set; }
        public string totalCount { get; set; }
        public string EncriptedID { get; set; }
        public string EnEncriptedID { get; set; }
        public int aid { get; set; }
        public int did { get; set; }
        public string Amount { get; set; }
        public bool AmountType { get; set; }
        public int DeductionID { get; set; }
        public int AllowanceID { get; set; }
      
        public List<Allowance_Master> qlist { get; set; }
        public List<Allowance_Master> DecList { get; set; }
        public Allowance_Master CheckAllowanceName(string name = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_Allowance_deduction_Master]", new
                {
                    @Action = "AllowanceByName",
                    @allowancename = name.TrimStart().TrimEnd()
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public Allowance_Master AddNewAllowancename(Allowance_Master des)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<Allowance_Master>("[Sl_Allowance_deduction_Master]", new
                {
                    @Action = "AllowanceInsert",
                    @id= des.id,
                    @allowancename = des.allowancename,
                    @InsertBy = des.InsertBy,
                    @IPAddress = ip,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj; 
            }
        }
        public Allowance_Master AllowanceDetailList (int pageIndex = 0, int pageSize = 25)
        {
            Allowance_Master list = new Allowance_Master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sl_Allowance_deduction_Master]", new { @Action = "View", @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Allowance_Master>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Allowance_Master CheckDeductionName(string name = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_Allowance_deduction_Master]", new
                {
                    @Action = "DeductionByName",
                    @deductionname = name.TrimStart().TrimEnd()
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public Allowance_Master AddNewDeductionname(Allowance_Master des)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<Allowance_Master>("[Sl_Allowance_deduction_Master]", new
                {
                    @Action = "DeductionInsert",
                    @id = des.id,
                    @deductionname = des.deductionname,
                    @InsertBy = des.InsertBy,
                    @IPAddress = ip,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Allowance_Master DeductionDetailList(int pageIndex = 0, int pageSize = 25)
        {
            Allowance_Master list = new Allowance_Master();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sl_Allowance_deduction_Master]", new { @Action = "DeductionView", @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Allowance_Master>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }

        public Allowance_Master AllowanceByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_Allowance_deduction_Master]", new
                {
                    @Action = "AllowanceByID",
                    @id = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public Allowance_Master DeductionByID(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<Allowance_Master>("[Sl_Allowance_deduction_Master]", new
                {
                    @Action = "DeductionByID",
                    @id = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
    }
}
