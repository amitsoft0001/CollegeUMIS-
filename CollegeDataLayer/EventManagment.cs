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
    public class EventManagment
    {
        public int ID { get; set; }
        public string EventName { get; set; }
        public int EventTypeID { get; set; }
        public int EventOrganiserID { get; set; }
        public bool IsPaid { get; set; }     
        public decimal Amount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public string Createdate { get; set; }
        public string Modifydate { get; set; }
        public string IPAddress { get; set; }
        public string ModifyByIPAddress { get; set; }
        public int InsertedBy { get; set; }
        public int ModifyBy { get; set; }
        public int CollegeID { get; set; }
        public string OrganiserName { get; set; }
        public string hfile { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int hid { get; set; }
        public string EncriptedID { get; set; }
        public string EventFee { get; set; }
        public string EventCategoryName { get; set; }
        public bool IsActive { get; set; }
        public List<EventManagment> EventTypeList(string CollegeID="")
        {
            List<EventManagment> obj = new List<EventManagment>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            { 
                obj = conn.Query<EventManagment>("[Sp_Event_Managment]", new { @Action = "EventTypeMaster" , @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public List<EventManagment> EventOrganiserList()
        {
            List<EventManagment> obj = new List<EventManagment>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<EventManagment>("[Sp_Event_Managment]", new { @Action = "EventOrganiser" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public EventManagment SaveEventDetails(EventManagment ob)
        {
            var IP = CommonMethod.GetIPAddress();
            if (ob.FromDate == null)
            {
                ob.FromDate = "";
            }
            else
            {
                ob.FromDate = DateTime.ParseExact(ob.FromDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }
            if (ob.ToDate == null)
            {
                ob.ToDate = "";
            }
            else
            {
                ob.ToDate = DateTime.ParseExact(ob.ToDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
            }
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventManagment>("[Sp_Event_Managment]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @EventName = ob.EventName,
                    @Description = ob.Description,
                    @FromDate = ob.FromDate,
                    @ToDate = ob.ToDate,
                    @IPaddress = IP,
                    @EventTypeID = ob.EventTypeID,
                    @EventOrganiserID = ob.EventOrganiserID,
                    @IsPaid = ob.IsPaid,
                    @Amount = ob.Amount,
                    @Venue = ob.Venue,
                    @FileUrl = ob.FileUrl,
                    @CollegeID = ob.CollegeID,
                    @InsertedBy = ob.InsertedBy,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public EventManagmentList GetEventList(int pageIndex = 1, int pageSize = 25,string EventTypeID = "", string EventOrganiserID = "",string CollegeID = "",string Name="")
        {
            EventManagmentList list = new EventManagmentList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_Event_Managment]", new { @Action = "EventList", @PageIndex = pageIndex, @pageSize = pageSize, @EventTypeID = EventTypeID, @EventOrganiserID = EventOrganiserID, @CollegeID = CollegeID, @EventName=Name }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EventManagment>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EventManagment findEventDetail(int Id = 0)
        {
            EventManagment obj = new EventManagment();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<EventManagment>("[Sp_Event_Managment]", new { @Action = "EventDetail", @ID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EventManagment findDescription(int Id = 0)
        {
            EventManagment obj = new EventManagment();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<EventManagment>("[Sp_Event_Managment]", new { @Action = "findDescription", @ID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public static bool activedeactiveEvent(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventManagment>("[Sp_Event_Managment]", new { Action = "changeStatus", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.IsActive;
            }

        }

    }
    public class EventManagmentList
    {
        public List<EventManagment> qlist { get; set; }
        public string totalCount { get; set; }
    }

    public class EventOrganiserMasterList
    {
        public List<EventOrganiserMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }

    public class EventOrganiserMaster
    {
        public int ID { get; set; }
        public string OrganiserName { get; set; }
        public int EventID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public int InsertedBy { get; set; }
        public int hid { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int CollegeID { get; set; }
        public string EncriptedID { get; set; }
        public bool IsActive { get; set; }
        public string EventName { get; set; }
        public string Designation { get; set; }
        public List<EventOrganiserMaster> OrganiserList { get; set; }
        public EventOrganiserMaster findOrganiserDetail(int Id = 0)
        {
            EventOrganiserMaster obj = new EventOrganiserMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<EventOrganiserMaster>("[Sp_EventOrganiserMaster]", new { @Action = "OrganiserDetail", @ID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EventOrganiserMaster findGuestDetail(int Id = 0)
        {
            EventOrganiserMaster obj = new EventOrganiserMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<EventOrganiserMaster>("[Sp_Eventinvitation]", new { @Action = "GuestDetail", @ID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EventOrganiserMaster SaveOrganiserDetail(EventOrganiserMaster ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventOrganiserMaster>("[Sp_EventOrganiserMaster]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @OrganiserName = ob.OrganiserName,
                    @Email = ob.Email,
                    @MobileNo = ob.MobileNo,
                    @Address = ob.Address,
                    @IPaddress = IP,
                    @InsertedBy = ob.InsertedBy,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public static bool activedeactiveOrganiser(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventOrganiserMaster>("[Sp_EventOrganiserMaster]", new { Action = "changeStatus", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.IsActive;
            }
        }
        public EventOrganiserMasterList GetOrganiserList(int pageIndex = 1, int pageSize = 25, string MobileNo = "", string Email = "", string Name = "")
        {
            EventOrganiserMasterList list = new EventOrganiserMasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_EventOrganiserMaster]", new { @Action = "OrganiserList", @PageIndex = pageIndex, @pageSize = pageSize, @MobileNo = MobileNo, @Email = Email, @OrganiserName = Name }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EventOrganiserMaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public EventOrganiserMasterList GetEventinvitationList(int pageIndex = 1, int pageSize = 25, string CollegeID = "", string EventID = "", string MobileNo = "", string Name = "")
        {

            EventOrganiserMasterList list = new EventOrganiserMasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_Eventinvitation]", new { @Action = "GuestList", @CollegeID = CollegeID, @PageSize = pageSize, @PageIndex = pageIndex , @EventID = EventID , @MobileNo = MobileNo , @Name =Name }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EventOrganiserMaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public List<EventOrganiserMaster> EventList(string CollegeID="")
        {
            List<EventOrganiserMaster> obj = new List<EventOrganiserMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<EventOrganiserMaster>("[Sp_Eventinvitation]", new { @Action = "EventList",@CollegeID=CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public EventOrganiserMaster SaveGuestDetail(EventOrganiserMaster ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventOrganiserMaster>("[Sp_Eventinvitation]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @Name = ob.Name,                   
                    @MobileNo = ob.MobileNo,
                    @EventID = ob.EventID,
                    @IPaddress = IP,
                    @InsertedBy = ob.InsertedBy,
                    @CollegeID=ob.CollegeID,
                    @Email=ob.Email,
                    @Designation=ob.Designation,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public static bool activedeactiveGuest(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventManagment>("[Sp_Eventinvitation]", new { Action = "changeStatus", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.IsActive;
            }

        }
    }
    public class EventTypeMaster
    {
        public int ID { get; set; }
        public string EventName { get; set; }    
        public string Createdate { get; set; }      
        public int InsertedBy { get; set; }
        public int hid { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int CollegeID { get; set; }
        public string EncriptedID { get; set; }
        public bool IsActive { get; set; }
        public string totalCount { get; set; }
        public int SNO { get; set; }
      
        public List<EventTypeMaster> EventCategoryList { get; set; }   
        public EventTypeMaster findEventCategoryDetail(int Id = 0)
        {
            EventTypeMaster obj = new EventTypeMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<EventTypeMaster>("[Sp_EventOrganiserMaster]", new { @Action = "EventCategoryDetail", @ID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EventTypeMaster SaveEventCategoryDetail(EventTypeMaster ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventTypeMaster>("[Sp_EventOrganiserMaster]", new
                {
                    @Action = "EventCategoryInsert",
                    @ID = ob.ID,
                    @EventName = ob.EventName,
                    @CollegeID = ob.CollegeID,                  
                    @IPaddress = IP,
                    @InsertedBy = ob.InsertedBy,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
         public DesignationMaster CheckDesignationName(string name = "",string CollegeID="")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<DesignationMaster>("[Sp_EventOrganiserMaster]", new
                {
                    @Action = "EventCategoryByName",
                    @EventName = name,
                    @CollegeID= CollegeID
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public EventTypeMaster GetEventCategoryList(int pageIndex = 1, int pageSize = 25, string CollegeID = "")
        {
            EventTypeMaster list = new EventTypeMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_EventOrganiserMaster]", new { @Action = "EventCategoryList", @PageIndex = pageIndex, @pageSize = pageSize, @CollegeID= CollegeID }, commandType: CommandType.StoredProcedure);
                list.EventCategoryList = obj.Read<EventTypeMaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public static bool activedeactiveEventCategory(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EventManagment>("[Sp_EventOrganiserMaster]", new { Action = "changeStatusEventCategory", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.IsActive;
            }

        }
    }
}
