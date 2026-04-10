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
    public class BL_StreamMaster
    {
        public int Flag { get; set; }
        [Required(ErrorMessage = "Type is Required")]
        public int CommonId { get; set; }
        public int StreamCategoryID { get; set; } // PK
        public string Title { get; set; }
        [Required(ErrorMessage = "Category   is Required")]
        public int CourseCategoryID { get; set; }
        [Required(ErrorMessage = "Stream is Required")]
        public string streamCategory { get; set; }
        public string IPaddress { get; set; }
        public int InsertBy { get; set; }
        public int Marks { get; set; }
        public bool IsCompulsory { get; set; }

        public string CourseCategory { get; set; }
        public string SubjectCode { get; set; }
        public bool Ispractical { get; set; }
      
        public int SaveStreamData(BL_StreamMaster obj)
        {
            int Result = 0;
            //@,@
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_StreamMaster", new
                {
                    @flag = obj.Flag,
                    CourseCategoryID = obj.CourseCategoryID,
                    streamCategory = obj.streamCategory,
                    IsActive = true,
                    IsDelete = 1,
                    IPaddress = obj.IPaddress,
                    InsertBy = obj.InsertBy,
                    IsCompulsory = obj.IsCompulsory,
                    Marks = obj.Marks
                }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }
        public class StreamListType
        {
            public List<BL_StreamMaster> honourslist { get; set; }
            public List<BL_StreamMaster> subsidiarylist { get; set; }
        }
        public class SubjectList
        {
            public List<BL_StreamMaster> qlist { get; set; }
            public string totalCount { get; set; }
        }
        public class SubStreamMaster
        {
            public int SubjectType { get; set; }
            public int subjectcategoryid { get; set; }
            public int Substreamcategoryid { get; set; }
            public int CourseCategoryID { get; set; }
            public int StreamCategoryID { get; set; }
            public int Courseyearid { get; set; }
            public int InsertBy { get; set; } = 0;
            public string SubjectName { get; set; }
            public string SubjectCode { get; set; }
            public string SubjectCategory { get; set; } = string.Empty;
            public string Marks { get; set; }
            public bool Ispractical { get; set; }
        }
        public class SubStreamList
        {
            public string SubjectCategory { get; set; }
            public List<SubStreamMaster> subStreamMasters { get; set; }
        }
        public List<SubStreamList> getcollegesubjectscopy(int sid, int varflag, int collegeid, int subjectid = 0, int subsidiary1 = 0, int compulsory1 = 0, int coursecategoryid = 0)
        {
            List<SubStreamList> SubStreamMaster = new List<SubStreamList>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                // With New Rule 
                var sessionid = 0;
                var objdata = conn.Query<SubStreamMaster>("USP_StreamMaster_choicefill_JC_new",
                    new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1, @sessionid = sessionid }, commandTimeout: 500000, commandType: CommandType.StoredProcedure).ToList();
                var parent = objdata.GroupBy(x => x.SubjectCategory).ToList();
                foreach (var item in parent)
                {
                    var notify = new SubStreamList();
                    notify.SubjectCategory = item.Key;
                    notify.subStreamMasters = objdata.Where(x => x.SubjectCategory == item.Key).ToList();
                    SubStreamMaster.Add(notify);
                }
                //var objdata = conn.Query<BL_StreamMaster>("[USP_StreamMaster]",
                //   new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1 }, commandType: CommandType.StoredProcedure).ToList();
                return SubStreamMaster;
            }
        }
        public List<BL_StreamMaster> GetSubjectData(int varflag)
        {
            List<BL_StreamMaster> objdata = new List<BL_StreamMaster>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster",
                    new { @flag = varflag },
                        commandType: CommandType.StoredProcedure).ToList();

            }
            return objdata;
        }

        public BL_StreamMaster GetCollegeDataBYID(int varflag, int StreamID)
        {
            BL_StreamMaster objdata = new BL_StreamMaster();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = varflag, @StreamCategoryID = StreamID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return objdata;
        }
        public List<BL_StreamMaster> getsubjectbycourse(int varflag, int courseid,int collegeid =0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = varflag, @CourseCategoryID = courseid, @college=collegeid }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }

        }
        //public List<BL_StreamMaster> getcollegesubjects(int sid, int varflag, int collegeid, int subjectid = 0, int subsidiary1 = 0, int compulsory1 = 0)
        //{

        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1 }, commandType: CommandType.StoredProcedure).ToList();
        //        return objdata;
        //    }

        //}
        public List<BL_StreamMaster> getcollegesubjects(int sid, int varflag, int collegeid, int subjectid = 0, int subsidiary1 = 0, int compulsory1 = 0, int sessionid = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster_choicefill",
                    new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1, @sessionid = sessionid }, commandType: CommandType.StoredProcedure).ToList();

                //var objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1 }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }

        }
        public List<BL_StreamMaster> getcollegesubjects_old(int sid, int varflag, int collegeid, int subjectid = 0, int subsidiary1 = 0, int compulsory1 = 0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<BL_StreamMaster>("[USP_StreamMaster_old]", new { @SID = sid, @flag = varflag, @CollegeID = collegeid, @subjectid = subjectid, @subsidiary1 = subsidiary1, @compulsory1 = compulsory1 }, commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }

        }
        public List<BL_StreamMaster> getcollegesubjects_old_for_session2018(int sid, int varflag, int collegeid, int subjectid = 0, int subsidiary1 = 0, int compulsory1 = 0,int sessionid=0)
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var objdata = conn.Query<BL_StreamMaster>("[USP_StreamMaster_old_2020]",
                    new
                    {
                        @SID = sid,
                        @flag = varflag,
                        @CollegeID = collegeid,
                        @subjectid = subjectid,
                        @subsidiary1 = subsidiary1,
                        @compulsory1 = compulsory1,
                        @sessionid=sessionid
                    },

                    commandType: CommandType.StoredProcedure).ToList();
                return objdata;
            }

        }
        public int UpdateSubjectdata(BL_StreamMaster obj)
        {
            int Result = 0;
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                Result = conn.Execute("USP_StreamMaster",
                    new
                    {

                        flag = obj.Flag,
                        CourseCategoryID = obj.CourseCategoryID,
                        streamCategory = obj.streamCategory,
                        IPaddress = obj.IPaddress,
                        InsertBy = obj.InsertBy,
                        StreamCategoryID = obj.StreamCategoryID,
                        IsCompulsory = obj.IsCompulsory,
                        Marks = obj.Marks,
                        Ispractical = obj.Ispractical,
                        SubjectCode = obj.SubjectCode

                    }, commandType: CommandType.StoredProcedure);

            }
            return Result;
        }
        public StreamListType GetSubjectDataByType(int courseid = 0)
        {
            StreamListType objdata = new StreamListType();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_StreamMaster]", new { @flag = 7, @type = "Honours", @type1 = "Subsidiary", @courseid = courseid }, commandType: CommandType.StoredProcedure);
                objdata.honourslist = obj.Read<BL_StreamMaster>().ToList();
                objdata.subsidiarylist = obj.Read<BL_StreamMaster>().ToList();
            }
            return objdata;
        }
        public List<CollegeCourseAllocation> GetSubjectDataforBind(int courseid = 0,int edu=0, int college = 0)
        {
            List<CollegeCourseAllocation> objdata = new List<CollegeCourseAllocation>();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[USP_StreamMaster]", new { @flag = 19, @CourseCategoryID = courseid , @EducationTypeID =edu, @CollegeID = college }, commandType: CommandType.StoredProcedure);
                objdata= obj.Read<CollegeCourseAllocation>().ToList();
                //objdata.subsidiarylist = obj.Read<BL_StreamMaster>().ToList();
            }
            return objdata;
        }
        public CollegeCourseAllocation SaveData(CollegeCourseAllocation obj)
        {
            AcademicSession ac = new AcademicSession();
            var session = ac.GetAcademiccurrentSession();
            var IP = CommonMethod.GetIPAddress();
            var app =Convert.ToInt32(ClsLanguage.GetCookies("NUserId"));
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var Result = conn.Query<CollegeCourseAllocation>("USP_StreamMaster", new
                {
                    @flag = 8,
                    @EducationTypeID = obj.EducationTypeID,
                    @CourseCategoryID = obj.CourseCategoryID,
                    @HonoursSubject = obj.HonoursSubject,
                    @SubsidiarySubject = obj.SubsidiarySubject,
                    @IPaddress = IP,
                    @InsertBy = app,
                    @CollegeID = obj.CollegeID,
                    @ID = obj.ID,
                    @sessionid = session.ID

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Result;
            }
        }

        //-- bharti 01/04/2019
        public SubjectList GetSubjectListing(int pageIndex, int pageSize, string search = "", string searchcode = "", string searchname = "", string practical = "", string compulsory = "")
        {

            SubjectList sub = new SubjectList();
            using (IDbConnection con = new SqlConnection(CommonSetting.constr))
            {
                var result = con.QueryMultiple("USP_StreamMaster", new { @flag = 16, PageIndex = pageIndex, pageSize = pageSize, search = search, searchcode = searchcode, searchname = searchname, practical = practical, compulsory = compulsory }, commandType: CommandType.StoredProcedure);
                sub.qlist = result.Read<BL_StreamMaster>().ToList();
                sub.totalCount = result.Read<String>().FirstOrDefault();
            }
            return sub;
        }
        //-- til here 
        // Amit Kr Yadav
        #region Subject Dropdown
        public List<SelectListItem> GetSubjectList(int CourseCategoryID, int CollegeID)
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                if (CourseCategoryID > 0 && CollegeID > 0)
                {
                    List<BL_StreamMaster> objdata = new List<BL_StreamMaster>();
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = 18, @CourseCategoryID = CourseCategoryID, @college = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                    }

                    if (objdata != null)
                    {
                        foreach (var p in objdata)
                        {
                            items.Add(new SelectListItem { Value = p.StreamCategoryID.ToString(), Text = p.streamCategory + "  (" + p.SubjectCode + ")".ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        public List<SelectListItem> GetSubjectCodeList(int CourseCategoryID, int CollegeID)
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                if (CourseCategoryID > 0 && CollegeID > 0)
                {
                    List<BL_StreamMaster> objdata = new List<BL_StreamMaster>();
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = 18, @CourseCategoryID = CourseCategoryID, @college = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                    }

                    if (objdata != null)
                    {
                        foreach (var p in objdata)
                        {
                            items.Add(new SelectListItem { Value = p.SubjectCode.ToString(), Text = p.streamCategory.ToString() + " (" + p.SubjectCode.ToString() + ")" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        public List<BL_StreamMaster> AllSubjectList(int CourseCategoryID, int CollegeID)
        {
            List<BL_StreamMaster> objdata = new List<BL_StreamMaster>();
            try
            {
                if (CourseCategoryID > 0 && CollegeID > 0)
                {

                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = 18, @CourseCategoryID = CourseCategoryID, @college = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return objdata;
        }
        public List<SelectListItem> AllSubjectListDropdown(int CourseCategoryID, int CollegeID)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<BL_StreamMaster> objdata = new List<BL_StreamMaster>();
            try
            {
                if (CourseCategoryID > 0 && CollegeID > 0)
                {

                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        objdata = conn.Query<BL_StreamMaster>("USP_StreamMaster", new { @flag = 18, @CourseCategoryID = CourseCategoryID, @college = CollegeID }, commandType: CommandType.StoredProcedure).ToList();
                    }
                    if (objdata != null)
                    {
                        foreach (var p in objdata)
                        {
                            items.Add(new SelectListItem { Value = p.StreamCategoryID.ToString(), Text = p.streamCategory.ToString() + " (" + p.SubjectCode.ToString() + ")" });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        #endregion
    }
    public class StreamListType
    {
        public List<BL_StreamMaster> honourslist { get; set; }
        public List<BL_StreamMaster> subsidiarylist { get; set; }
    }
    public class SubjectList
    {
        public List<BL_StreamMaster> qlist { get; set; }
        public string totalCount { get; set; }
    }
}
