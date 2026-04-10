using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
namespace DataLayer
{
    public class Bl_SeatMater
    {
        public int Flag { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Type is Required")]
        public int SeatsAvaible { get; set; } //  
        [Required(ErrorMessage = "Category   is Required")]
        public int CourseCategoryID { get; set; }
      
        public string AdmissionStartDate { get; set; }
        public string AdmissionEndDate { get; set; }
        public int SessionId { get; set; }
        public int TotalSeats { get; set; }

        public string CreateDate { get; set; }
        public int updateby { get; set; }
        public string IpAddress { get; set; }
        public string OrderNo { get; set; }
        public string OrderFile { get; set; }
        public int OpenAdmissionID { get; set; }
        public string CasteID { get; set; }

        public string Seatas { get; set; }
        public int Admissiontype { get; set; }
        public int EducationType { get; set; }
        public int commonid { get; set; }
        public string title { get; set; }
        
        public bool status { get; set; }
        public string message { get; set; }
        public int AddmissionCategoryid { get; set; }
        public string CourseCategory { get; set; }
        public string session { get; set; }
        public string EducationTypename { get; set; }
        public string AddmissionCategoryname { get; set; }
        public int StreamCategoryID { get; set; }
        public string streamCategory { get; set; }
        public bool ADDornot { get; set; }
        public int days { get; set; }
        public string username { get; set; }
        public int handicappedseats { get; set; }
        public string handicappedSeatas { get; set; }
        public string handiSeatas { get; set; }
        public int handiSeatasdis { get; set; }
        public int Seatasdis { get; set; }
        public bool islast { get; set; }
        public string type { get; set; }
        public int collegeid { get; set; }
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public int Addseat_manual { get; set; }

        public string FeeStartDate { get; set; }
        public string FeeEndDate { get; set; }
        public int add_manualseat { get; set; }
        public int consume_seat { get; set; }
        public int remainingseat { get; set; }

        public Bl_SeatMater SaveseatData(Bl_SeatMater obj,string action )
        {
           
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
               var Result = conn.Query<Bl_SeatMater>("USP_Seatsaster", new
                {
                    @action= action,
                    @ID = obj.ID,
                    @CourseCategoryID =obj.CourseCategoryID,
                    @SessionId =obj.SessionId,
                    @IPaddress = ip,
                    @InsertBy = ClsLanguage.GetCookies("NUserId"),
                    @SeatsAvaible =obj.SeatsAvaible,
                    @AdmissionStartDate =obj.AdmissionStartDate,
                    @AdmissionEndDate =obj.AdmissionEndDate,
                    @OrderNo =obj.OrderNo,
                    @Orderfile =obj.OrderFile,
                    @CasteID =obj.CasteID,
                    @Seatas =obj.Seatas,
                    @AddmissionCategoryid=AddmissionCategoryid,
                    @steamid= obj.StreamCategoryID,
                    @handicappedSeatas=obj.handicappedSeatas
               }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Result;
            }
           
        }
        public Bl_SeatMater Saveadmissiondate(Bl_SeatMater obj, string action,string flag)
        {

            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<Bl_SeatMater>("USP_Seatsaster_extended_date", new
                {
                    @action = "insert",
                    @flag= flag,
                    @OpenAdmissionID= obj.OpenAdmissionID,
                    @ID = obj.ID,
                    @CourseCategoryID = obj.CourseCategoryID,
                    @SessionId = obj.SessionId,
                    @IPaddress = ip,
                    @InsertBy = 0,//ClsLanguage.GetCookies("NUserId"),
                    @SeatsAvaible = obj.SeatsAvaible,
                    @AdmissionStartDate = obj.AdmissionStartDate,
                    @AdmissionEndDate = obj.AdmissionEndDate,
                    @OrderNo = obj.OrderNo,
                    @Orderfile = obj.OrderFile,
                    @CasteID = obj.CasteID,
                    @Seatas = obj.Seatas,
                    @AddmissionCategoryid = obj.AddmissionCategoryid,
                    @steamid = obj.StreamCategoryID,
                    @days=obj.days,
                    @educationtypeidid=obj.EducationType
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Result;
            }

        }
        public List<Bl_SeatMater> getdateseat(int courseidid, string action,int  sessionid,int id,int stearamid,int dis_totseat=0)
        {
            string ip = DataLayer.CommonMethod.IPAddress;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
               var Result = conn.Query<Bl_SeatMater>("USP_Seatsaster", new
                {
                    @action = action,
                   @CourseCategoryID = courseidid,
                   @SessionId= sessionid
                   , @ID = id,
                   @steamid = stearamid,
                   @disseats=dis_totseat
               }, commandType: CommandType.StoredProcedure).ToList();
                return Result;
            }
       
        }
        public List<Bl_SeatMater> getadmissionextendate( int id)
        {
            string ip = DataLayer.CommonMethod.IPAddress;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<Bl_SeatMater>("USP_Seatsaster_extended_date", new
                {
                    @action = "listviewextend",
                    @ID = id,
                }, commandType: CommandType.StoredProcedure).ToList();
                return Result;
            }

        }
        public Bl_SeatMater getadmissionate_already(int sessionid, int admissionid, int educationid)
        {
             using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<Bl_SeatMater>("USP_Seatsaster_extended_date", new
                {
                    @action = "alreadysave",
                    @SessionId = sessionid,
                    @AddmissionCategoryid= admissionid,
                    @educationtypeidid= educationid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Result;
            }

        }
        
        public Bl_SeatList GetseatList(string action= "Couserseatview", int pageIndex1 = 1, int pageSize1 = 25,int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0")
        {
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Bl_SeatList list = new Bl_SeatList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.QueryMultiple("USP_Seatsaster", new
                {
                    @action = action,
                    @PageSize = pageSize1,
                    @PageIndex = pageIndex1,
                    @CourseCategoryID= courseid,
                    @SessionId=sessionid,
                    @AddmissionCategoryid= admissionid,
                    @educationtypeidid=programid
                }, commandType: CommandType.StoredProcedure);
                list.qlist = Result.Read<Bl_SeatMater>().ToList();
                list.totalCount = Result.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Bl_SeatList Getadmissiondateslist(string action = "Couserseatview", int pageIndex1 = 1, int pageSize1 = 25, int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0")
        {
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Bl_SeatList list = new Bl_SeatList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.QueryMultiple("[USP_Seatsaster_extended_date]", new
                {
                    @action = action,
                    @PageSize = pageSize1,
                    @PageIndex = pageIndex1,
                    @CourseCategoryID = courseid,
                    @SessionId = sessionid,
                    @AddmissionCategoryid = admissionid,
                    @educationtypeidid = programid
                }, commandType: CommandType.StoredProcedure);
                list.qlist = Result.Read<Bl_SeatMater>().ToList();
                list.totalCount = Result.Read<string>().FirstOrDefault();
            }
            return list;
        }

        public Bl_SeatMater SaveseatDataCollege(Bl_SeatMater obj, string action)
        {

            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<Bl_SeatMater>("USP_College_Seatmaster", new
                {
                    @action = action,
                    @ID = obj.ID,
                    @CourseCategoryID = obj.CourseCategoryID,
                    @SessionId = obj.SessionId,
                    @IPaddress = ip,
                    @InsertBy = ClsLanguage.GetCookies("NUserId"),
                    @SeatsAvaible = obj.SeatsAvaible,
                    @AdmissionStartDate = obj.AdmissionStartDate,
                    @AdmissionEndDate = obj.AdmissionEndDate,
                    @OrderNo = obj.OrderNo,
                    @Orderfile = obj.OrderFile,
                    @CasteID = obj.CasteID,
                    @Seatas = obj.Seatas,
                    @AddmissionCategoryid = AddmissionCategoryid,
                    @steamid = obj.StreamCategoryID,
                    @handicappedSeatas = obj.handicappedSeatas,
                    @Collegeid = obj.collegeid
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return Result;
            }

        }
        public List<Bl_SeatMater> getdateseatcollge(int courseidid, string action, int sessionid, int id, int stearamid, int dis_totseat = 0,int collegeid= 0)
        {
            string ip = DataLayer.CommonMethod.IPAddress;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<Bl_SeatMater>("USP_College_Seatmaster", new
                {
                    @action = action,
                    @CourseCategoryID = courseidid,
                    @SessionId = sessionid
                    ,
                    @ID = id,
                    @steamid = stearamid,
                    @disseats = dis_totseat,
                    @Collegeid= collegeid
                }, commandType: CommandType.StoredProcedure).ToList();
                return Result;
            }

        }
        public Bl_SeatList GetseatListCollege(string action = "Couserseatview", int pageIndex1 = 1, int pageSize1 = 25, int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0",string collegeid="0")
        {
            var app = ClsLanguage.GetCookies("NBApplicationNo");
            Bl_SeatList list = new Bl_SeatList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.QueryMultiple("USP_College_Seatmaster", new
                {
                    @action = action,
                    @PageSize = pageSize1,
                    @PageIndex = pageIndex1,
                    @CourseCategoryID = courseid,
                    @SessionId = sessionid,
                    @AddmissionCategoryid = admissionid,
                    @educationtypeidid = programid
                    ,@Collegeid= collegeid
                }, commandType: CommandType.StoredProcedure);
                list.qlist = Result.Read<Bl_SeatMater>().ToList();
                list.totalCount = Result.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    public class Bl_SeatList
    {
        public List<Bl_SeatMater> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
