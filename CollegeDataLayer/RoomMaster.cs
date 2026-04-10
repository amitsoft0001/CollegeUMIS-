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
   public class RoomMaster
    {
        public int ID { get; set; }
        public int RoomNo { get; set; }
        public int CollegeID { get; set; }
        public int InsertedBY { get; set; }
        public string IPAddress { get; set; }
        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string Description { get; set; }
        public List<RoomMaster> qlist { get; set; }
        public string totalCount { get; set; }
        public string EncriptedID { get; set; }
        public int Session { get; set; }
        public RoomMasterList RoomDetailList(int PageIndex=1,int PageSize=25,string CollegeID = "")
        {
            AcademicSession ac = new AcademicSession();
            int sess = ac.GetAcademiccurrentSession().ID;
            RoomMasterList List = new RoomMasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_College_RoomMaster]", new { @Action = "View", @PageSize= PageSize, @PageIndex= PageIndex, @CollegeID = CollegeID, @Session = sess }, commandType: CommandType.StoredProcedure);
                List.qlist = obj.Read<RoomMaster>().ToList();
                List.totalCount =  obj.Read<string>().FirstOrDefault();
            }
           
            return List;
        }
        public RoomMaster CheckRoomName(string name = "",string collegeID="")
        {
            AcademicSession ac = new AcademicSession();
            int sess = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<RoomMaster>("[Sp_College_RoomMaster]", new
                {
                    @Action = "RoomByName",
                    @RoomNo = name,
                    @Session= sess,
                    @CollegeID = collegeID
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public RoomMaster AddNewRoom(RoomMaster des)
        {
            AcademicSession ac = new AcademicSession();
            int sess = ac.GetAcademiccurrentSession().ID;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<RoomMaster>("[Sp_College_RoomMaster]", new
                {
                    @Action = "Insert",
                    @ID = des.ID,
                    @CollegeID = des.CollegeID,
                    @RoomNo = des.RoomNo,
                    @InsertedBY = des.InsertedBY,
                    @IPAddress = ip,
                    @Description= des.Description,
                    @Session = sess
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;




            }
        }        
        public RoomMaster getRoomDetailBYID(string id ="")
        {
            int ID = (id != "" ? Convert.ToInt32(id) : 0);
            RoomMaster obj = new RoomMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();

                obj = conn.Query<RoomMaster>("[Sp_College_RoomMaster]", new { @Action = "GetByID", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }


    }
    public class RoomMasterList
    {
        public List<RoomMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class RoomSeatMaster
    {
        public int ID { get; set; }
        public int RoomMasterID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Capacity { get; set; }
        public int CollegeID { get; set; }
        public int InsertedBY { get; set; }
        public string IPAddress { get; set; }
        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }
        public int ModifyBY { get; set; }
        public List<RoomSeatMaster> seatList { get; set; }
        public string Roomidlist { get; set; }
        public string RowList { get; set; }
        public string ColList { get; set; }
        public string CapacityList { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int RoomNo { get; set; }
        public string Description { get; set; }

        public List<RoomMaster> getRoomdetail(string id = "",int CollegeID=0)
        {
            List<RoomMaster> objdata = new List<RoomMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<RoomMaster>("sp_College_RoomSeat", new
                {
                    @Action = "RoomNoDrop",
                    @idvalues = id,
                    @CollegeID= CollegeID
                }, commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }
        public List<RoomSeatMaster> getSeatetail(int college = 0)
        {
            AcademicSession ac = new AcademicSession();
            int sess = ac.GetAcademiccurrentSession().ID;
            List<RoomSeatMaster> objdata = new List<RoomSeatMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<RoomSeatMaster>("sp_College_RoomSeat", new
                {
                    @Action = "seatReport",
                    @collegeid = college,
                    @Session = sess,
                }, commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }
        public RoomSeatMaster getRoomSeatdetailBYID(string id = "")
        {
            RoomSeatMaster objdata = new RoomSeatMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<RoomSeatMaster>("sp_College_RoomSeat", new
                {
                    @Action = "RoomDetailByID",
                    @ID = id
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
            
        }
        public RoomSeatMaster SaveSeatStructure(RoomSeatMaster ob)
        {
            AcademicSession ac = new AcademicSession();
            int sess = ac.GetAcademiccurrentSession().ID;
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<RoomSeatMaster>("sp_College_RoomSeat", new
                {
                    Action = "SaveSeatDetail",
                    @ID = ob.ID,
                    @Roomidlist = ob.Roomidlist,
                    @collegeid = ob.CollegeID,
                    @RowList = ob.RowList,
                    @ColList = ob.ColList,
                    @CapacityList = ob.CapacityList,
                    @InsertedBY = ob.InsertedBY,
                    @IPaddress = IP,
                    @Session = sess,

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public RoomSeatMaster UpdateRoomSeat(RoomSeatMaster ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<RoomSeatMaster>("sp_College_RoomSeat", new
                {
                    Action = "Update",
                    @ID = ob.ID,
                    @Row = ob.Row,
                    @Column = ob.Column,                   
                    @Capacity = ob.Capacity,                   
                    @InsertedBy = ob.InsertedBY,
                    @IPaddress = IP,  
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
    }

}
