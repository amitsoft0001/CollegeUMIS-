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
    public class Employee_DocumentUpload
    {
        public int ID { get; set; }
        public int DocType { get; set; }
        public string Narration { get; set; }
        public string Createdate { get; set; }
        public string IPaddress { get; set; }
        public string EmployeeID { get; set; }
        public string DocumentURl { get; set; }
        public int EID { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Documentidlist { get; set; }
        public string narrationList { get; set; }
        public string filevalueslist { get; set; }
        public string YearvaluesList { get; set; }
        public string GradevaluesList { get; set; }
        
        public int InsertedBy { get; set; }
        public int CollegeID { get; set; }
        public string EncriptedID { get; set; }
        public string Date { get; set; }
        public string Mobile { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string DocName { get; set; }
        public int hid { get; set; }
        public string year { get; set; }
        public string Grade { get; set; }
        public string PassingYear { get; set; }
        public int DocTypeID { get; set; }
        public List<UserLogin> GetEmpmenuList(int CollegeID = 0)
        {
            List<UserLogin> userList = new List<UserLogin>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Employee_DocumentUpload]", new { @Action = "EmpList", @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure);
                userList = obj.Read<UserLogin>().ToList();

            }
            return userList;
        }
        public List<UserLogin> GetEmpmenuListAdmin(int CollegeID = 0)
        {
            List<UserLogin> userList = new List<UserLogin>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_College_Leave_Management]", new { @Action = "UserListAdmin", @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure);
                userList = obj.Read<UserLogin>().ToList();

            }
            return userList;
        }
        public List<UserLogin> GetEmpmenuListUser(int CollegeID = 0,int id=0)
        {
            List<UserLogin> userList = new List<UserLogin>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_College_Leave_Management]", new { @Action = "UserList", @CollegeID = CollegeID ,@ID=id}, commandType: CommandType.StoredProcedure);
                userList = obj.Read<UserLogin>().ToList();

            }
            return userList;
        }
        public List<Employee_Document_Master> GetDocumentList(string action,int id=0)
        {
            List<Employee_Document_Master> docList = new List<Employee_Document_Master>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Employee_DocumentUpload]", new { @Action = action,@ID=id }, commandType: CommandType.StoredProcedure);
                docList = obj.Read<Employee_Document_Master>().ToList();

            }
            return docList;
        }
        public List<Employee_Document_Master> GetDocumentBYID(string res = "", int type = 0,int EID=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Employee_Document_Master>("sp_Employee_DocumentUpload", new { Action = "DocumentByID", @Type = type, @DocIDs = res , @EID = EID }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }

        }
        public Employee_DocumentUpload SaveDocumentType1(Employee_DocumentUpload ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Employee_DocumentUpload>("sp_Employee_DocumentUpload", new
                {
                    Action = "SaveDocType1",
                    @ID = ob.ID,
                    @DocType = ob.DocType,
                    @Narration = ob.Narration,
                    @IPaddress = IP,
                    @CollegeID = ob.CollegeID,
                    @DocumentURl = ob.DocumentURl,
                    @EID = ob.EID,
                    @InsertedBy = ob.InsertedBy,
                    @Grade = ob.Grade,
                    @PassingYear=ob.PassingYear

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Employee_DocumentUploadList EmployeedocList(int pageIndex = 1, int pageSize = 25, string Document = "", string Mobile = "", string EmployeeName = "", string collegeID = "")
        {

            Employee_DocumentUploadList list = new Employee_DocumentUploadList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Employee_DocumentUpload]", new { @Action = "view", @PageIndex = pageIndex, @pageSize = pageSize, @Document = Document, @Mobile = Mobile, @EmployeeName = EmployeeName, @CollegeID = collegeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Employee_DocumentUpload>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Employee_DocumentUpload GetByID(int id = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Employee_DocumentUpload>("sp_Employee_DocumentUpload", new
                {
                    Action = "GetByID",
                    @ID = id,


                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Employee_DocumentUpload DeleteDocumentByID(int id = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Employee_DocumentUpload>("sp_Employee_DocumentUpload", new
                {
                    Action = "Delete",
                    @ID = id,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Employee_DocumentUpload EditDocumentupload(Employee_DocumentUpload ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Employee_DocumentUpload>("sp_Employee_DocumentUpload", new
                {
                    Action = "SaveDocType1",
                    @ID = ob.ID,
                    @DocType = ob.DocType,
                    @Narration = ob.Narration,
                    @IPaddress = IP,
                    @CollegeID = ob.CollegeID,
                    @DocumentURl = ob.DocumentURl,
                    @EID = ob.EID,
                    @InsertedBy = ob.InsertedBy,
                    @Grade = ob.Grade,
                    @PassingYear = ob.PassingYear,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public Employee_DocumentUploadList EmployeedocListByUser( string EmployeeName = "", string collegeID = "")
        {

            Employee_DocumentUploadList list = new Employee_DocumentUploadList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_Employee_DocumentUpload]", new { @Action = "viewById",  @EmployeeName = EmployeeName, @CollegeID = collegeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Employee_DocumentUpload>().ToList();
               
            }
            return list;
        }
    }
    public class Employee_Document_Master
    {
        public int DocID { get; set; }
        public string DocTitle { get; set; }
        public int DocType { get; set; }

        

    }
    public class Employee_DocumentUploadList
    {
        public List<Employee_DocumentUpload> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class Employee_FamilyDetail
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string MemberName { get; set; }
        public string Relation { get; set; }
        public int RelationTypeID { get; set; }
        public string Age { get; set; }
        public int InsertedBy { get; set; }
        public string MobileNo { get; set; }
        public int hid { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public int CollegeID { get; set; }
        public string EncriptedID { get; set; }
        public int SNO { get; set; }
        public string Name { get; set; }
        public List<Employee_FamilyDetail> qlist { get; set; }
        public string totalCount { get; set; }
        public List<Employee_FamilyDetail> RelationTypeList()
        {
            List<Employee_FamilyDetail> obj = new List<Employee_FamilyDetail>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<Employee_FamilyDetail>("[Sp_College_EmployeeFamilyDetail]", new { @Action = "RelationTypeDetail" }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public Employee_FamilyDetail SaveFamilymemberDetail(Employee_FamilyDetail ob)
        {
            var IP = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<Employee_FamilyDetail>("[Sp_College_EmployeeFamilyDetail]", new
                {
                    @Action = "Insert",
                    @ID = ob.ID,
                    @MemberName = ob.MemberName,
                    @MobileNo = ob.MobileNo,
                    @RelationTypeID = ob.RelationTypeID,
                    @Age = ob.Age,
                    @IPaddress = IP,
                    @InsertedBy = ob.InsertedBy,
                    @CollegeID = ob.CollegeID,
                    @EmployeeID = ob.EmployeeID                   
                   
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }
        public Employee_FamilyDetail FamilyMemberList(string empID = "", int pageIndex = 0, int pageSize = 25)
        {

            Employee_FamilyDetail list = new Employee_FamilyDetail();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_College_EmployeeFamilyDetail]", new { @Action = "View", @EmployeeID = empID, @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Employee_FamilyDetail>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public Employee_FamilyDetail findfamilyMemberDetail(int Id = 0)
        {
            Employee_FamilyDetail obj = new Employee_FamilyDetail();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<Employee_FamilyDetail>("[Sp_College_EmployeeFamilyDetail]", new { @Action = "Detail", @ID = Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
    }
}
