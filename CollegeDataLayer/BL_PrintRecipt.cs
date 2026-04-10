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
    public class BL_PrintRecipt
    {
        public int Id { get; set; }
        public string ApplicationNo { get; set; }
        public string Session { get; set; }
        public string Title { get; set; }
        public string Fees { get; set; }
        public string SubjecCategory { get; set; }
        public string CastCategory { get; set; }
        public string FeeStatus { get; set; }
        public string PaymentHolderName { get; set; }
        public string Expires { get; set; }
        public string NuthNum { get; set; }
        public string PaymentType { get; set; }
        public string CardNumber { get; set; }
        public string TransactionId { get; set; }
        public string banktrxid { get; set; }
        public string status { get; set; }
        public string trxdate { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public string Amount { get; set; }
        public string Clienttrxid { get; set; }
        public string requesttime { get; set; }
        public string banktxndate { get; set; }
        public string requesttime1 { get; set; }
        public string StudentName { get; set; }
        public string yearname { get; set; }
        public BL_PrintRecipt GetPaymentRecipt(int id=0) 
        {
            //int id = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
            //string SessionID = ClsLanguage.GetCookies("NBSission");
            AcademicSession ac = new AcademicSession();
            int SessionID = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {            
                var Obj = conn.Query<BL_PrintRecipt>("[sp_StudentRegistration]", new { @Action = "PrintRecipt", @Id = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();              
                return Obj;
            }
        }

        public BL_PrintReciptList paymentdetail(int pageSize, int pageIndex, string fromdate = "", string todate = "", string ApplicationNo = "", string Status = "",string Collegeid="")
        {

            BL_PrintReciptList list = new BL_PrintReciptList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_PaymentDetail_College_admincheck]", new { @pageIndex = pageIndex, @pageSize = pageSize, @fromDate = fromdate, @toDate = todate, @ApplicationNo = ApplicationNo, @Status = Status,@Collegeid= Collegeid }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<BL_PrintRecipt>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public BL_PrintRecipt GetPaymentDetails(int id = 0)
        {
           
            AcademicSession ac = new AcademicSession();
            int SessionID = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Obj = conn.Query<BL_PrintRecipt>("[Sp_RecruitmentReport_College]", new { @Action = "FeeStatusDetail", @SID = id, @session = SessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Obj;
            }
        }

    }
    public class BL_PrintReciptList
    {
        public List<BL_PrintRecipt> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
