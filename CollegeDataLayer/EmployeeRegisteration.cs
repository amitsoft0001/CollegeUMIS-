using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EmployeeRegisteration
    {
        public int Id { get; set; }     
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FirstNameInHindi { get; set; }
        public string MiddleNameInHindi { get; set; }
        public string LastNameInHindi { get; set; }

        public int Gender { get; set; }
        public string DOB { get; set; }
        public int CastCategory { get; set; }
        public int BloodGroup { get; set; }

        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string CA_PinCode { get; set; }
        public int CA_Country { get; set; }
        public int CA_State { get; set; }
        public string CA_City { get; set; }
        public string PA_Address { get; set; }
        public string PA_PinCode { get; set; }
        public int PA_Country { get; set; }
        public int PA_State { get; set; }
        public string PA_City { get; set; }
        public string FatherName { get; set; }

        public string FatherNameInHindi { get; set; }
        public string FatherQualification { get; set; }
        public string FatherOccupation { get; set; }
        public string FatherMobile { get; set; }
        public string FatherEmail { get; set; }
        public string MotherName { get; set; }
        public string MotherNameInHindi { get; set; }

        public string MotherQualification { get; set; }
        public string MotherOccupation { get; set; }
        public string MotherEmail { get; set; }
        public string MotherMobile { get; set; }
        public string GargianName { get; set; }
        public string GargianContactNo { get; set; }
        public string GargianRelation { get; set; }
        public int session { get; set; }     
        public int IsLogin { get; set; }      
      
        public string Password { get; set; }
        public bool status { get; set; }
        public string Message { get; set; }
        public bool rememberMe { get; set; }
        public string stphoto { get; set; }
        public string stsign { get; set; }
        public int Ftitle { get; set; }
        public int title { get; set; }

        public int Nationality { get; set; }
        public int Religion { get; set; }
        public int MotherTongue { get; set; }   
       
        public string ReligonOther { get; set; }
        public byte[] U_password { get; set; }
        public string JoiningDate { get; set; }
        public int InsertedBy { get; set; }
        public string IPAddress { get; set; }
        public int facultyType { get; set; }
        public int Designation { get; set; }
        public int CollegeID { get; set; }
        public string UserName { get; set; }
        public string EncriptedID { get; set; }
        public bool IsActive { get; set; }
        public string DesignationName { get; set; }
        public string DOB1 { get; set; }
        public string JoiningDate1 { get; set; }
        public int hid { get; set; }
        public string EContactNo { get; set; }
        public string EID { get; set; }
        public string EnEID { get; set; }
        public string NationalityName { get; set; }
        public string ReligionName { get; set; }
        public string GenderName { get; set; }
        public string FacultyTypeName { get; set; }
        public string CastCategoryName { get; set; }
        public string BloodGroupName { get; set; }
        public string MotherTongueName { get; set; }
        public string MedicalReport { get; set; }
        public string IdentificationsMarks { get; set; }
        public decimal EmployeeHeight { get; set; }
        public string MedicalReportDoc { get; set; }
        public string CharacterCertificate { get; set; }
        public string CharacterCertificateDoc { get; set; }
        public bool IsConstitution { get; set; }
        public bool IsSecrecy { get; set; }
        public bool IsMarried { get; set; }
        public string GPFNo { get; set; }
        public string GPFRemarks { get; set; }
        public string Nominnee { get; set; }
        public string NominneePercentage { get; set; }     
        public string MemberName { get; set; } 
        public string EmployeeNo { get; set; }     
        public string NomineeName { get; set; }
        public string Verifydate { get; set; }
        public int IsVerify { get; set; }       
        public string RejectReason { get; set; }
        public int RoleType { get; set; }
        public int des { get; set; }
        public int IsInfo { get; set; }
        public List<CollegeEmployee_Information> NominneeList { get; set; }

        public  List<DesignationMaster> FacultyTypeList = new List<DesignationMaster>()
           {
         new DesignationMaster() { URole="Permanent",URoleID=1},
        new DesignationMaster() { URole = "Temporary",URoleID = 2}
    };
        
        public List<DesignationMaster> FacultyTypeList1 = new List<DesignationMaster>()
           {
                 new DesignationMaster() { URole="Teaching"},
                new DesignationMaster() { URole = "NonTeaching"}
             };

        public List<DesignationMaster> getDesignationMaster(int collegeid=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<DesignationMaster>("[sp_CollegeEmployee_Registration]",new { @Action = "Designation", @CollegeID=collegeid }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }

       
        public EmployeeRegisteration Employee_registration(EmployeeRegisteration emp)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                
                ////   ClsLanguage.SetCookies(EncriptDecript.Encrypt(userdata[6]), "ENNBUserID");
                //int StID = Convert.ToInt32(ClsLanguage.GetCookies("NBStID"));
                //if (StID == 0)
                //{
                //    emp.status = false;
                //    emp.Message = "Network Error !!";
                //    return emp;
                //}
                string ip = CommonMethod.GetIPAddress();
                if (emp.DOB == null)
                {
                    emp.DOB = "";
                }
                else
                {
                    emp.DOB = DateTime.ParseExact(emp.DOB, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                if (emp.JoiningDate == null)
                {
                    emp.JoiningDate = "";
                }
                else
                {
                    emp.JoiningDate = DateTime.ParseExact(emp.JoiningDate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                }
                var obj = conn.Query<EmployeeRegisteration>("[sp_CollegeEmployee_Registration]", new
                {
                    @Id = emp.Id,
                    @FirstName = CommonSetting.RemoveSpecialChars(emp.FirstName),
                    @MiddleName = CommonSetting.RemoveSpecialChars(emp.MiddleName),
                    @LastName = CommonSetting.RemoveSpecialChars(emp.LastName),
                    @Gender = emp.Gender,
                    @DOB = emp.DOB,
                    @CastCategory = emp.CastCategory,
                    @BloodGroup = emp.BloodGroup,
                    @MobileNo = CommonSetting.RemoveSpecialChars(emp.MobileNo),
                    @Email = CommonSetting.RemoveSpecialCharsemail(emp.Email),
                    @CurrentAddress = CommonSetting.RemoveSpecialCharsaddress(emp.CurrentAddress),
                    @CA_PinCode = CommonSetting.RemoveSpecialChars(emp.CA_PinCode),
                    @CA_Country = emp.CA_Country,
                    @CA_State = emp.CA_State,
                    @CA_City = CommonSetting.RemoveSpecialChars(emp.CA_City),
                    @PA_Address = CommonSetting.RemoveSpecialCharsaddress(emp.PA_Address),
                    @PA_PinCode = CommonSetting.RemoveSpecialChars(emp.PA_PinCode),
                    @PA_Country = emp.PA_Country,
                    @PA_State = emp.PA_State,
                    @PA_City = CommonSetting.RemoveSpecialChars(emp.PA_City),
                    @FatherName = CommonSetting.RemoveSpecialChars(emp.FatherName),
                    @FatherQualification = CommonSetting.RemoveSpecialChars(emp.FatherQualification),
                    @FatherOccupation = CommonSetting.RemoveSpecialChars(emp.FatherOccupation),
                    @FatherMobile = CommonSetting.RemoveSpecialChars( emp.FatherMobile),
                    @FatherEmail = CommonSetting.RemoveSpecialCharsemail(emp.FatherEmail),
                    @MotherName = CommonSetting.RemoveSpecialChars(emp.MotherName),
                    @MotherQualification = CommonSetting.RemoveSpecialChars(emp.MotherQualification),
                    @MotherOccupation = CommonSetting.RemoveSpecialChars(emp.MotherOccupation),
                    @MotherEmail = CommonSetting.RemoveSpecialCharsemail(emp.MotherEmail),
                    @MotherMobile = CommonSetting.RemoveSpecialChars(emp.MotherMobile),              
                  
                    @IsLogin = 0,
                    @Action = "Insert",                   
                    @stphoto = emp.stphoto,
                    @stsign = emp.stsign,
                    @title = emp.title,
                    @ftile = emp.Ftitle,
                    @Nationality = emp.Nationality,
                    @Religion = emp.Religion,
                    @MotherTongue = emp.MotherTongue,                
                    @FirstNameInHindi = CommonSetting.RemoveSpecialChars(emp.FirstNameInHindi),
                    @MiddleNameInHindi = emp.MiddleNameInHindi,
                    @LastNameInHindi = emp.LastNameInHindi,
                    @ReligonOther = emp.ReligonOther,
                    @FatherNameInHindi = emp.FatherNameInHindi,
                    @MotherNameInHindi = emp.MotherNameInHindi,
                    @Designation = emp.Designation,
                    @CollegeID= emp.CollegeID,
                    @JoiningDate=emp.JoiningDate,
                    @facultyType=emp.facultyType,
                    @UserName=emp.UserName.Replace(" ",""),
                    @U_password = emp.U_password,
                    @Password=emp.Password,
                   @IPAddress=ip,
                    @InsertedBy=emp.InsertedBy,
                    @EContactNo=emp.EContactNo,
                    @IdentificationsMarks= emp.IdentificationsMarks,
                    @EmployeeHeight=emp.EmployeeHeight,
                    @MedicalReport=emp.MedicalReport,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }

        public EmployeeRegisterationList EmployeedetailList(int pageIndex1 = 1, int pageSize1 = 25, string MobileNo = "", string Email = "", string collegeID = "", string Designation = "", string FacultyType = "",string Code="",string Name= "" ,int usertype = 0,int ID=0)
        {         
            EmployeeRegisterationList list = new EmployeeRegisterationList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_CollegeEmployee_Registration]", new { @Action = "view", @PageIndex = pageIndex1, @pageSize = pageSize1, @MobileNO = MobileNo, @Email = Email, @CollegeID = collegeID, @Designation= Designation , @FacultyType = FacultyType , @Code = Code,@Name= Name ,@UserType = usertype,@ID=ID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<EmployeeRegisteration>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public static bool activedeactiveuser(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<EmployeeRegisteration>("[sp_CollegeEmployee_Registration]", new { Action = "changeStatus", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.IsActive;
            }

        }
        public EmployeeRegisteration getdetailsByID(int id = 0)
        {
            EmployeeRegisteration obj = new EmployeeRegisteration();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {               
                obj = conn.Query<EmployeeRegisteration>("[sp_CollegeEmployee_Registration]", new { @Action = "GetByEmployeeID", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EmployeeRegisteration getdetailsByEID(int id = 0)
        {
            EmployeeRegisteration obj = new EmployeeRegisteration();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {              
                obj = conn.Query<EmployeeRegisteration>("[sp_CollegeEmployee_Registration]", new { @Action = "GetByEmployeeEID", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public bool getdetailsofDocument(int id = 0)
        {
            bool result=false;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                result = conn.Query<bool>("[sp_CollegeEmployee_Registration]", new { @Action = "DocDetail", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
        }
        public EmployeeRegisteration getdetailsForUpdate(int id = 0)
        {
            EmployeeRegisteration obj = new EmployeeRegisteration();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {             
                obj = conn.Query<EmployeeRegisteration>("[sp_CollegeEmployee_Registration]", new { @Action = "EmployeeIDforUpdate", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EmployeeRegisteration getdetailsForViewDetail(int id = 0)
        {
            EmployeeRegisteration obj = new EmployeeRegisteration();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<EmployeeRegisteration>("[sp_CollegeEmployee_Registration]", new { @Action = "EmployeeViewDetail", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public EmployeeBioData getdetailsForBioData(int id = 0)
        {
            EmployeeBioData objemp = new EmployeeBioData();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_CollegeEmployee_BioData]", new { @Action = "BioData", @ID = id }, commandType: CommandType.StoredProcedure);
                objemp.empregistration = obj.Read<EmployeeRegisteration>().FirstOrDefault();
                objemp.odjDocument = obj.Read<Employee_DocumentUpload>().ToList();               
                return objemp;
            }
        }
    }
    public class EmployeeBioData
    {
        public EmployeeRegisteration empregistration { get; set; }
        public List<Employee_DocumentUpload> odjDocument { get; set; }
    }
    public class EmployeeRegisterationList
    {
        public List<EmployeeRegisteration> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class DesignationMaster
    {
        public int URoleID { get; set; }
        public string URole { get; set; }
        public int CollegeID { get; set; }
        public int InsertedBY { get; set; }
        public string IPAddress { get; set; }
        public string CreateDate { get; set; }
        public int RoleType { get; set; }
        public string Msg { get; set; }
        public int Status { get; set; }
        public int SNO { get; set; }
        public List<DesignationMaster> DesignationList { get; set; }    
        public DesignationMaster CheckDesignationName(string name = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<DesignationMaster>("[Sp_College_Designation]", new
                {
                    @Action = "DesignationByName",
                    @URole = name
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public DesignationMaster AddNewDesignation(DesignationMaster des)
        {            
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<DesignationMaster>("[Sp_College_Designation]", new
                {
                    @Action = "Insert",
                    @URoleID = des.URoleID,
                    @CollegeID = des.CollegeID,
                    @URole = des.URole,
                    @InsertedBY = des.InsertedBY,
                    @IPAddress = ip, 
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public DesignationMasterList DesignationDetailList( string CollegeID = "", int pageIndex = 0, int pageSize = 25)
        {
            DesignationMasterList list = new DesignationMasterList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_College_Designation]", new { @Action = "View", @CollegeID = CollegeID, @PageSize = pageSize, @PageIndex = pageIndex }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<DesignationMaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }  
    }
    public class DesignationMasterList
    {
        public List<DesignationMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }

    public class CollegeEmployee_Information
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string MedicalReport { get; set; }
        public string MedicalReportDoc { get; set; }
        public string CharacterCertificate { get; set; }
        public string CharacterCertificateDoc { get; set; }
        public bool IsConstitution { get; set; }
        public bool IsSecrecy { get; set; }
        public bool IsMarried { get; set; }
        public string GPFNo { get; set; }
        public string GPFRemarks { get; set; }
        public string Nominnee { get; set; }
        public string NominneePercentage { get; set; }
        public int InsertedBy { get; set; }
        public int hid { get; set; }
        public string MemberName { get; set; }
        public bool Status { get; set; }
        public string Msg { get; set; }
        public string hfile { get; set; }
        public string chfile { get; set; }
        public List<CollegeEmployee_Information> qlist { get; set; }
        public string totalCount { get; set; }
        public string EncriptedID { get; set; }
        public string EmployeeNo { get; set; }
        public string Designation { get; set; }
        public string NomineeName { get; set; }
        public string Verifydate { get; set; }
        public int IsVerify { get; set; }
        public string encriptedeID { get; set; }
        public string RejectReason { get; set; }
        public List<CollegeEmployee_Information> NominneeList { get; set; }
        public List<CollegeEmployee_Information> RelationTypeList(int id=0)
        {
            List<CollegeEmployee_Information> obj = new List<CollegeEmployee_Information>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                CommonMethod cmn = new CommonMethod();
                obj = conn.Query<CollegeEmployee_Information>("[Sp_College_EmployeeFamilyDetail]", new { @Action = "FamilyDetail", @EmployeeID = id }, commandType: CommandType.StoredProcedure).ToList();
                return obj;
            }
        }
        public CollegeEmployee_Information SaveInformationDetails(CollegeEmployee_Information ob)
        {
            var IP = CommonMethod.GetIPAddress();           
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<CollegeEmployee_Information>("[Sp_College_EmployeeFamilyDetail]", new
                {
                    @Action = "InformationInsert",
                    @ID = ob.Id,
                    @MedicalReport = ob.MedicalReport,
                    @MedicalReportDoc = ob.MedicalReportDoc,
                    @CharacterCertificate = ob.CharacterCertificate,
                    @CharacterCertificateDoc = ob.CharacterCertificateDoc,
                    @IPAddress = IP,
                    @EmployeeID = ob.EmployeeID,
                    @GPFNo=ob.GPFNo,
                    @GPFRemarks = ob.GPFRemarks,
                    @Nominnee = ob.Nominnee,
                    @NominneePercentage = ob.NominneePercentage,
                    @IsConstitution = ob.IsConstitution,
                    @IsMarried = ob.IsMarried,
                    @IsSecrecy = ob.IsSecrecy,
                    @InsertedBY = ob.InsertedBy,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }

        }

        public CollegeEmployee_Information EmployeedetailList(int pageIndex1 = 1, int pageSize1 = 25,int EmployeeID=0,int usertype=0,int collegeID=0)
        {
            CollegeEmployee_Information list = new CollegeEmployee_Information();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[Sp_College_EmployeeFamilyDetail]", new { @Action = "InformationView", @PageIndex = pageIndex1, @pageSize = pageSize1 , @EmployeeID = EmployeeID, @usertype= usertype , @collegeID = collegeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeEmployee_Information>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public CollegeEmployee_Information getdetails(int id = 0)
        {
            CollegeEmployee_Information objemp = new CollegeEmployee_Information();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                 objemp = conn.Query<CollegeEmployee_Information>("[Sp_College_EmployeeFamilyDetail]", new { @Action = "DataForEdit", @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();                
                return objemp;
            }
        }
        public CollegeEmployee_Information getdetailsData(int EmployeeID = 0)
        {
            CollegeEmployee_Information objemp = new CollegeEmployee_Information();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objemp = conn.Query<CollegeEmployee_Information>("[Sp_College_EmployeeFamilyDetail]", new { @Action = "EmployeeData", @EmployeeID = EmployeeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return objemp;
            }
        }
        
        public bool getinfodetails(int @EmployeeID = 0)
        {           
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
               var  objemp = conn.Query<bool>("[Sp_College_EmployeeFamilyDetail]", new { @Action = "CheckData", @EmployeeID = EmployeeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return objemp;
            }
        }
        public CollegeEmployee_Information CertificateVerifyEmp(int ID = 0,int InsertedBY=0)
        {
            CollegeEmployee_Information obj = new CollegeEmployee_Information();
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeEmployee_Information>("[Sp_College_EmployeeFamilyDetail]", new
                {
                    @Action = "VerifyEmp",
                    @ID = ID,                   
                    @IPAddress = ip,
                    @InsertedBy = InsertedBY
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }
        public CollegeEmployee_Information CertificateRejectEmp(int ID = 0,string reason="",int InsertedBY=0)
        {
            string ip = CommonMethod.GetIPAddress();
            CollegeEmployee_Information obj = new CollegeEmployee_Information();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<CollegeEmployee_Information>("[Sp_College_EmployeeFamilyDetail]", new
                {
                    @Action = "RejectEmp",
                    @ID = ID,                    
                    @IPAddress = ip,
                    @reason = reason,
                    @InsertedBy = InsertedBY
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
        }

    }
    public class Employeedetialslist
    {
        public int Id { get; set; }
        public int Ids { get; set; }
        public string EmployeeID { get; set; }
        public string collegeid { get; set; }
        public string Employeetype { get; set; }
        public string grade { get; set; }
        [Display(Name = "Post/Subject")]
        public string post_subject { get; set; }
        public string NoofsanctionPosts { get; set; }
        public string ExistingSanctionedPost { get; set; }
        public string Nameoftheteacher { get; set; }
        public string PresentDesignation { get; set; }
        public string Designation { get; set; }
        public string BankACNo { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string AppttonRecommendoftheCommissorbytheAbsorption { get; set; }
        public string DateofBirth { get; set; }
        public string Effectivedateofappointment { get; set; }
        public string DateofJoining { get; set; }
        public string DateofPromotionSrLect { get; set; }
        public string DateofPromotionLectSelGrReader { get; set; }
        public string DateofPromotionProfessor { get; set; }
        public string DateofPromotion1stACP { get; set; }
        public string DateofPromotion2stACP { get; set; }
        public string DateofPromotionMACP { get; set; }
        public string UnivServiceCommConcurrenceinpromotion { get; set; }
        public string Designationatthetimeoffirstappointment { get; set; }
        public string PayScaleatthetimeoffirstappointment { get; set; }
        public string PaySlipReceiptNo { get; set; }
        public string type { get; set; }
        public string salSlNo1 { get; set; }
        public string OldBasicPayason01012016 { get; set; }
        public string Whetherlivinginquarter { get; set; }
        public string PayinpayMatrixason01012016 { get; set; }
        public string ApplicablePayMatrixason0103019 { get; set; }
        public string Dateofnextincrement { get; set; }
        public string BasicPayason01032020 { get; set; }
        public string DA25 { get; set; }
        public string HRA8 { get; set; }
        public string CTA25 { get; set; }
        public string MA { get; set; }
        public string OtherAllow { get; set; }
        public string TotalperMonthonMarch2020SumofCol22toCol27 { get; set; }
        public string NoofMonths { get; set; }
        public string TotalSalaryWEFMarch20ToJune20 { get; set; }
        public string BasicPayason010720 { get; set; }
        public string DA25_2 { get; set; }
        public string HRA8_2 { get; set; }
        public string CTA25_2 { get; set; }
        public string MA1 { get; set; }
        public string OtherAllow1 { get; set; }
        public string TotalperMonthonJuly2020SumofCol31toCol36 { get; set; }
        public string NoofMonths1 { get; set; }
        public string TotalSalaryWEFJuly20ToDec20 { get; set; }
        public string BasicPayason01012021 { get; set; }
        public string DA25_3 { get; set; }
        public string HRA8_3 { get; set; }
        public string CTA25_3 { get; set; }
        public string MA2 { get; set; }
        public string OtherAllow2 { get; set; }
        public string TotalperMonthonJan2021SumofCol40toCol45 { get; set; }
        public string NoofMonths2 { get; set; }
        public string TotalSalaryWEFJan21ToFeb21 { get; set; }
        public string TotalPerYearCol303948 { get; set; }
        public string collegename { get; set; }

        public EmployeedetailsListList EmployeedetailsListList(int pageIndex1 = 1, int pageSize1 = 25, string MobileNo = "", string Email = "", string collegeID = "", string Designation = "", string FacultyType = "", string Code = "", string Name = "", int usertype = 0, int ID = 0)
        {
            EmployeedetailsListList list = new EmployeedetailsListList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[sp_CollegeEmployee_detailsreport]", new { @Action = "view", @PageIndex = pageIndex1, @pageSize = pageSize1, @MobileNO = MobileNo, @Email = Email, @CollegeID = collegeID, @Designation = Designation, @FacultyType = FacultyType, @Code = Code, @Name = Name, @UserType = usertype, @ID = ID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<Employeedetialslist>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
    }
    public class EmployeedetailsListList
    {
        public List<Employeedetialslist> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
