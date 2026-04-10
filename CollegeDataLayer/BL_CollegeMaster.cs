using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class BL_CollegeMaster
    {
        #region Properties
        public int ID { get; set; }
        [Required(ErrorMessage ="College Code is Required")]
        public string CollegeCode { get; set; }
        [Required(ErrorMessage = "College Name is Required")]
        public string CollegeName { get; set; }
        public int InsertBy { get; set; }
        public string Ipaddress { get; set; }
        public bool Isactive { get; set; }
        public int Flag { get; set; }
        public int NameTitle { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public int Gender { get; set; }
        public string Photo { get; set; }
        public bool status { get; set; }
        public string Msg { get; set; }
        public string fullname { get; set; }
        public string gen { get; set; }
        public string statename { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfSeats { get; set; }
        public string NodalOfficerEmail { get; set; }
        public string NodalOfficerName { get; set; }
        public string NodalOfficerMobile { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalEmail { get; set; }
        public string PrincipalMobile { get; set; }
        public string Password { get; set; }
        public string NewAddress { get; set; } // use for full address
        public string EncriptedID { get; set; } // use for full address

        public string UserID { get; set; }
        public string SubjectName { get; set; }
        public string Stream { get; set; }
        public int TotalSeat { get; set; }
        public int TotalApply { get; set; }
        public int TotalAdmission { get; set; }



        public string streamCategory { get; set; }
        public string Reaming { get; set; }
        public string NewREm { get; set; }
        #endregion

        public int SaveCollegeData(BL_CollegeMaster obj)
        {
            int Result = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                 Result = conn.Execute("USP_CollegeMaster", new {
                    @flag = obj.Flag,
                    CollegeCode = obj.CollegeCode,
                    CollegeName = obj.CollegeName,
                    InsertBy=obj.InsertBy,
                    Ipaddress=obj.Ipaddress,
                    Isactive=obj.Isactive

                }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }
        public List<SelectListItem> GetCollegeExam(int coursecategory = 0, int educationtypeid = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
                var Types = objdata;
                if (coursecategory > 0)
                {
                    Types = GetCollegeDatabycollegealloted(6, coursecategory);
                }
                else if (educationtypeid > 0)
                {
                    Types = GetCollegeDatabycollegealloted(7, educationtypeid);
                }
                else
                {
                    Types = GetCollegeData(5);
                }


                if (Types != null)
                {
                    foreach (var p in Types)
                    {
                        items.Add(new SelectListItem { Value = p.ID.ToString(), Text = p.CollegeName });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return items;
        }
        //public List<SelectListItem> GetCollegeExam(int coursecategory = 0, int educationtypeid = 0)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    try
        //    {
        //        List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
        //        var Types = objdata;
        //        if (coursecategory > 0)
        //        {
        //            Types = GetCollegeDatabycollegealloted(6, coursecategory);
        //        }
        //        else if (educationtypeid > 0)
        //        {
        //            Types = GetCollegeDatabycollegealloted(7, educationtypeid);
        //        }
        //        else
        //        {
        //            Types = GetCollegeData(5);
        //        }


        //        if (Types != null)
        //        {
        //            foreach (var p in Types)
        //            {
        //                items.Add(new SelectListItem { Value = p.ID.ToString(), Text = p.CollegeName });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return items;
        //}
        public int UpdateCollegedata(BL_CollegeMaster obj)
        {
            int Result = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_CollegeMaster", 
                    new {

                        flag = obj.Flag,
                        CollegeCode = obj.CollegeCode,
                        CollegeName = obj.CollegeName ,
                        ID=obj.ID

                    }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }

        public List<BL_CollegeMaster> GetCollegeData(int varflag)
        {
            List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_CollegeMaster>("USP_CollegeMaster", new { @flag = varflag }, commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }
        public BL_CollegeMaster GetCollegeDataBYID(int varflag, int CollegeID)
        {
            BL_CollegeMaster objdata = new BL_CollegeMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_CollegeMaster>("USP_CollegeMaster", new { @flag = varflag, @ID = CollegeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }

        //
        public BL_CollegeMaster SaveCollegeDataajax(BL_CollegeMaster obj)
        {

            var IP = CommonMethod.GetIPAddress();
            var app = 0;
            //if (ClsLanguage.GetCookies("ENNBCLID") == null)
            //{
            //    app = Convert.ToInt32(ClsLanguage.GetCookies("NBCollegeCode"));
            //}
          
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<BL_CollegeMaster>("[cl_CollegeMaster]", new
                {
                    @Action = "insert",
                    @ID = obj.ID,
                  
                    //CollegeCode = obj.CollegeCode,
                    //CollegeName = obj.CollegeName,
                    InsertBy = app,
                    Ipaddress = IP,
                    Isactive = obj.Isactive,
                    @Title = obj.NameTitle,
                    @Name = obj.Name,
                    @ContactNo = obj.ContactNo,
                    @Email = obj.Email,
                    @Address = obj.Address,
                    @State = obj.State,
                    @City = obj.City,
                    @Pincode = obj.PinCode,
                    @Gender = obj.Gender,
                    @Photo = obj.Photo,
                    @NoOfSeats = obj.NoOfSeats,
                    @NoOfRooms = obj.NoOfRooms,
                    @NodalOfficerEmail = obj.NodalOfficerEmail,
                    @NodalOfficerMobile = obj.NodalOfficerMobile,
                    @NodalOfficerName = obj.NodalOfficerName,
                    @PrincipalEmail = obj.PrincipalEmail,
                    @PrincipalMobile = obj.PrincipalMobile,
                    @PrincipalName = obj.PrincipalName
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Result;
            }
        }
        public CollageList  collagedetailList(int pageIndex1 = 1, int pageSize1 = 25)
        {
           
            CollageList list = new CollageList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_CollegeMaster]", new { Action = "view", flag = 0, PageIndex = pageIndex1, pageSize = pageSize1 }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<BL_CollegeMaster>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public List<SelectListItem> GetCollege()
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                var Types = GetCollegeData(2);
                if (Types != null)
                {
                    foreach (var p in Types)
                    {
                        items.Add(new SelectListItem { Value = p.ID.ToString(), Text = p.CollegeName + '(' + p.CollegeCode + ')' });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return items;
        }
        public CollageList collagedetailviewlistdrop(int pageIndex1 = 1, int pageSize1 = 25)
        {

            CollageList list = new CollageList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_CollegeMaster]", new { Action = "viewlistdrop", flag=0,PageIndex = pageIndex1, pageSize = pageSize1 }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<BL_CollegeMaster>().ToList();
                //   list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public CollageList collagedetailviewlistdropalloted(int coursecategoryid = 0, int sessionid = 0)
        {

            CollageList list = new CollageList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_CollegeMaster]", new { Action = "viewlistalloted", flag = 0, @coursecategoryid = coursecategoryid, @sessionid = sessionid }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<BL_CollegeMaster>().ToList();
               
            }
            return list;
        }
        public List<BL_CollegeMaster> GetRemainingSheet(int CollegeID = 0, int streamCatego = 0)
        {
            List<BL_CollegeMaster> obj = new List<BL_CollegeMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                obj = conn.Query<BL_CollegeMaster>("[sp_RemainingSheet]", new { @CollegeID = CollegeID, @coursecategoryid = streamCatego }, commandType: CommandType.StoredProcedure).ToList();
            }
            return obj;
        }
        //public List<SelectListItem> GetCollegeExam(int coursecategory = 0)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    try
        //    {
        //        List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
        //        var Types = objdata;
        //        if (coursecategory > 0)
        //        {
        //            Types = GetCollegeDatabycollegealloted(6, coursecategory);
        //        }
        //        else
        //        {
        //            Types = GetCollegeData(5);
        //        }


        //        if (Types != null)
        //        {
        //            foreach (var p in Types)
        //            {
        //                items.Add(new SelectListItem { Value = p.ID.ToString(), Text = p.CollegeName });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return items;
        //}
        public List<BL_CollegeMaster> GetCollegeDatabycollegealloted(int varflag, int coursecategory)
        {
            List<BL_CollegeMaster> objdata = new List<BL_CollegeMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_CollegeMaster>("USP_CollegeMaster", new { @flag = varflag, @coursecategoryid = coursecategory }, commandType: CommandType.StoredProcedure).ToList();
            }
            return objdata;
        }
    }
    public class CollageList
    {
        public List<BL_CollegeMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }
   
    public class CollegeCourseAllocation
    {
        public int ID { get; set; }
        public int EducationTypeID { get; set; }
        public int CourseCategoryID { get; set; }
        public string HonoursSubject { get; set; }
        public string SubsidiarySubject { get; set; }
        public DateTime CreateDate { get; set; }
        public string IPaddress { get; set; }
        public int InsertBy { get; set; }
        public int CollegeID { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public string StreamName { get; set; }
        public string CollegeName { get; set; }
        public string Title { get; set; }
        public string CourseCategory { get; set; }

        public CollegeCourseAllocation GetSubjectDataByCourse(int courseid = 0, int eduid = 0, int college = 0)
        {
            AcademicSession ob = new AcademicSession();
            var session=ob.GetAcademiccurrentSession();
            int sessionID = session.ID;
            CollegeCourseAllocation objdata = new CollegeCourseAllocation();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<CollegeCourseAllocation>("USP_StreamMaster", new { @flag = 9, @courseid = courseid, @eduid = eduid, @college = college, @sessionID= sessionID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }
        public CollegeCourseAllocationList collageallocationdetailList(int pageIndex1 = 1, int pageSize1 = 25)
        {

            CollegeCourseAllocationList list = new CollegeCourseAllocationList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_StreamMaster]", new { @flag = 15, PageIndex = pageIndex1, pageSize = pageSize1 }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<CollegeCourseAllocation>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }



    }
    public class CollegeCourseAllocationList
    {
        public List<CollegeCourseAllocation> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class SelectStreamCategory
    {
        public int StreamCategoryID { get; set; }
        public string streamCategory { get; set; }
        public List<SelectStreamCategory> GetStreamCategory(int Id = 0)
        {
            List<SelectStreamCategory> StreamCategory = new List<SelectStreamCategory>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                StreamCategory = conn.Query<SelectStreamCategory>("sp_select_StreamCategory", new { @Id = Id }, commandType: CommandType.StoredProcedure).ToList();

            }
            return StreamCategory;
        }
    }
}
