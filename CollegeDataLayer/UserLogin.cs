using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataLayer
{
    public class UserLogin
    {
        #region Properties
        [EmailAddress(ErrorMessage = "Invalid LoginId")]
        [Required(ErrorMessage = "LoginId is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int UserType { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public bool IsActive { get; set; }
        public long ID { get; set; }

        [Required(ErrorMessage = "LoginId is required")]
        public string LoginID { get; set; }
        public string ProfilePic { get; set; }
        public bool rememberMe { get; set; }
        public int UserID { get; set; }
        public string msg { get; set; }
        public bool status { get; set; }
        public string menustr { get; set; }
        public string Mobile { get; set; }
        public string confpassword { get; set; }
        public int Permission { get; set; }
        public string Rolename { get; set; }
        [Required(ErrorMessage = "Captcha validation is required.")]
        public string txtCaptcha { get; set; }
        public string Fullname { get; set; }
        public string EncriptedID { get; set; }
        public int CollegeID { get; set; }
        public string IPAddress { get; set; }
        public int InsertedBy { get; set; }

        public string NewUserID { get; set; }

        public string EmployeeCode { get; set; }
        [Required(ErrorMessage = "Employee Required")]
        public int EmployeeID { get; set; }

        public int EmpAssignSubjectId { get; set; }
        public int SessionId { get; set; }
        //public int CollegeId { get; set; }

        [Required(ErrorMessage = "Education Type Required")]
        public int EducationTypeId { get; set; }

        [Required(ErrorMessage = "Course Category Required")]
        public int CourseCategoryId { get; set; }
        public int SubjectId { get; set; }
        public string EducationType { get; set; }
        public string CourseCategory { get; set; }
        public string Subject { get; set; }
        public string SubjectCode { get; set; }
        public string DesignationName { get; set; }
        public bool Checked { get; set; }

        [Required(ErrorMessage = "Subject Required")]
        public List<int> SubId { get; set; }
        public List<SelectListItem> EduTypeList { get; set; }
        public List<SelectListItem> CourseCategoryList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> SelectedSubjectList { get; set; }
        public List<UserLogin> AssignSubjectList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public string Message { get; set; }

        #endregion
        //public UserLogin Login(UserLogin objLogin)
        //{
        //    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
        //    {
        //        var tbl = conn.Query<UserLogin>("[USP_AdminLogin]", new { LoginID = objLogin.Email, Password = objLogin.Password }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        if (tbl.UserID > 0)
        //        {
        //            objLogin.status = true;
        //            objLogin.UserID = tbl.UserID;
        //            objLogin.msg = tbl.msg;
        //            objLogin.IsActive = tbl.IsActive;
        //            objLogin.UserType = tbl.UserType;
        //            objLogin.menustr = tbl.menustr;
        //            objLogin.LoginID = tbl.LoginID;
        //            objLogin.UserName = tbl.UserName;
        //            objLogin.ProfilePic = tbl.ProfilePic;
        //        }
        //        else
        //        {
        //            objLogin.msg = tbl.msg;
        //            objLogin.status = false;
        //        }



        //        return objLogin;
        //    }
        //}
        public UserLogin Login(UserLogin objLogin)
        {
            string ip = CommonMethod.GetIPAddress();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[USP_AdminLogin]", new { LoginID = objLogin.Email, Password = objLogin.Password, @IPAddress = ip }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (tbl.UserID > 0)
                {
                    objLogin.status = true;
                    objLogin.UserID = tbl.UserID;
                    objLogin.msg = tbl.msg;
                    objLogin.IsActive = tbl.IsActive;
                    objLogin.UserType = tbl.UserType;
                    objLogin.menustr = tbl.menustr;
                    objLogin.LoginID = tbl.LoginID;
                    objLogin.UserName = tbl.UserName;
                    objLogin.ProfilePic = tbl.ProfilePic;
                    objLogin.Permission = tbl.Permission;
                }
                else
                {
                    objLogin.msg = tbl.msg;
                    objLogin.status = false;
                }



                return objLogin;
            }
        }
        public UserLogin userdata(int id=0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[cl_Sp_CreateEmployee]", new {
                    @Action = "UserByID",
                    @ID=id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }

      
        public static bool logout(string id = "0")
        {
            ExpireAllCookies();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<UserLogin>("update tbl_userMaster set isLoggedin=0 where ID=@ID ", new { ID = id }, commandType: CommandType.Text).FirstOrDefault();

                return true;
            }

        }
        public static void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        if (cookie.Name != "rcpc")
                        {
                            var expiredCookie = new HttpCookie(cookie.Name)
                            {

                                Expires = DateTime.Now.AddDays(-1),
                                Domain = cookie.Domain

                            };
                            HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                        }

                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
        public ChangePasswordAdmin ChangePassword(ChangePasswordAdmin obj)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<ChangePasswordAdmin>("sp_ChangeCollegePassword", new { @CollegeCode = obj.ID, CurrentPassword = obj.CurrentPassword, NewPassword = obj.NewPassword, Status = "ChangePasswordAdmin" }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return ob;
            }
        }
        //GetUserRole
        public List<UserRole> GetUserRole()
        {

            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var ob = conn.Query<UserRole>("sp_UserMaster", new { @Action = "Userrole", @UserType = Convert.ToInt32(ClsLanguage.GetCookies("NUsertype")) }, commandType: CommandType.StoredProcedure).ToList();

                return ob;
            }
        }
        public UserLogin Addnewusertype(UserLogin objLogin, int InsertedBy = 0, int CollegeID=0)
        {
            if(InsertedBy==1)
            {
                objLogin.UserType = 2;
            }
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                string ip = CommonMethod.GetIPAddress();
                var obj = conn.Query<UserLogin>("[cl_Sp_CreateEmployee]", new
                {
                    @Action = "Insert",
                    @ID = objLogin.ID,
                    @CollegeID = CollegeID,
                    @UserType = objLogin.UserType,
                    @UserName = objLogin.UserName,
                    @Mobile = objLogin.Mobile,                   
                    @Email = objLogin.Email,
                    @Password = objLogin.Password,                    
                    @Fullname = objLogin.Fullname,
                    @MenuStr = ",",
                    @IPAddress = ip,
                    @InsertedBy = InsertedBy,
                   

                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;




            }
        }      
        public UserLogin Checkuserbyname(string id = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[cl_Sp_CreateEmployee]", new
                {
                    @Action = "UserByName",
                    @UserName = id
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public UserLogin Checkuserbymobile(string mobile = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[sp_CollegeEmployee_Registration]", new
                {
                    @Action = "UserByMobile",
                    @MobileNo = mobile
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public UserLogin Checkuserbyemail(string email = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var tbl = conn.Query<UserLogin>("[sp_CollegeEmployee_Registration]", new
                {
                    @Action = "UserByEmail",
                    @Email = email
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return tbl;
            }
        }
        public static bool activedeactiveuser(int id = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<UserLogin>("[cl_Sp_CreateEmployee]", new { Action = "changeStatus", ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.IsActive;
            }

        }


        public UserList UserdetailList(int pageIndex1 = 1, int pageSize1 = 25, int usertype = 0,  string MobileNo = "", string Email = "",string collegeID="")
        {
            var id = usertype;// Convert.ToInt32(ClsLanguage.GetCookies("NUsertype"));
            int uid = 0;
           
            UserList list = new UserList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("[cl_Sp_CreateEmployee]", new { @Action = "view", @PageIndex = pageIndex1, @pageSize = pageSize1, @UserType = id, @MobileNO = MobileNo, @EmailID = @Email , @CollegeID = collegeID }, commandType: CommandType.StoredProcedure);
                list.qlist = obj.Read<UserLogin>().ToList();
                list.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return list;
        }
        public UserList GetUsermenuList(int ID ,int CollegeID=0)
        {
            UserList userList = new UserList();
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.QueryMultiple("cl_Sp_CreateEmployee", new { Action = "viewmenu", @UserType = ID , @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure);
                userList.qlist = obj.Read<UserLogin>().ToList();
                userList.totalCount = obj.Read<string>().FirstOrDefault();
            }
            return userList;
        }
        public UserLogin GetUserList(Int32 id , int CollegeID = 0)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                if (id > 0)
                {
                    if (id == 1)
                    {
                        var obj = conn.Query<UserLogin>("cl_Sp_CreateEmployee", new { Action = "viewadmin", ID = id, @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        return obj;
                    }
                  
                    else
                    {
                        var obj = conn.Query<UserLogin>("cl_Sp_CreateEmployee", new { Action = "viewmenuList", ID = id , @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        return obj;
                    }
                }
                else
                {
                    var obj = conn.Query<UserLogin>("cl_Sp_CreateEmployee", new { Action = "viewmenuList" }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return obj;
                }
            }
        }
        public bool GetUserchagemenustr(Int32 id = 0, string menustr = "")
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {
                var obj = conn.Query<UserLogin>("cl_Sp_CreateEmployee", new { Action = "changemenustr", ID = id, @MenuStr = menustr }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj.status;
            }
            return false;
        }
        public List<UserLogin> GetEmployeeAssignedSubject(int CollegeID = 0, int EmployeeID = 0, int CourseCategoryId = 0, int SessionId = 0)
        {
            List<UserLogin> lst = new List<UserLogin>();
            try
            {
                if (CollegeID > 0 && EmployeeID > 0 && CourseCategoryId > 0)
                {
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("sp_CollegeEmployee_AssignSubject", new { Action = "GetEmployeeAssignedSubject", @CollegeID = CollegeID, @EmployeeID = EmployeeID, @SubjectID = 0, @SessionId = SessionId, EducationTypeId = 0, @CourseCategoryId = CourseCategoryId }, commandType: CommandType.StoredProcedure);
                        lst = obj.Read<UserLogin>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        // Amit Kr Yadav
        #region Employee DropDown
        public List<SelectListItem> GetEmployeeList(int UserType, int CollegeID)
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                if (CollegeID > 0)
                {
                    UserList userList = new UserList();
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("cl_Sp_CreateEmployee", new { Action = "viewmenu", @UserType = UserType, @CollegeID = CollegeID }, commandType: CommandType.StoredProcedure);
                        userList.qlist = obj.Read<UserLogin>().ToList();
                        userList.totalCount = obj.Read<string>().FirstOrDefault();
                    }

                    if (userList != null)
                    {
                        foreach (var p in userList.qlist)
                        {
                            items.Add(new SelectListItem { Value = p.EmployeeID.ToString(), Text = p.UserName.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        public List<UserLogin> GetEmployeeAssignedSubject(int CollegeID = 0, int EmployeeID = 0)
        {
            List<UserLogin> lst = new List<UserLogin>();
            try
            {
                if (CollegeID > 0 && EmployeeID > 0)
                {
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("sp_CollegeEmployee_AssignSubject", new { Action = "GetEmployeeAssignedSubject", @CollegeID = CollegeID, @EmployeeID = EmployeeID }, commandType: CommandType.StoredProcedure);
                        lst = obj.Read<UserLogin>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public List<SelectListItem> GetEmployeeDropdown(string Type = "", int EduTypeID = 0, int CourseCategoryID = 0, int CollegeID = 0, int EmployeeID = 0)
        {
            //BL_Expert objExpert = new BL_Expert();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                if (CollegeID > 0 && EmployeeID > 0)
                {
                    List<UserLogin> userList = new List<UserLogin>();
                    List<UserLogin> userList1 = new List<UserLogin>();
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("sp_CollegeEmployee_AssignSubject", new { Action = "GetDetail", @CollegeID = CollegeID, @EmployeeID = EmployeeID }, commandType: CommandType.StoredProcedure);
                        userList = obj.Read<UserLogin>().ToList();
                    }

                    if (userList != null)
                    {
                        if (Type == "EducationType")
                        {
                            var lst = userList.Select(o => new { EducationTypeId = o.EducationTypeId, EducationType = o.EducationType }).Distinct().ToList();
                            foreach (var p in lst)
                            {
                                items.Add(new SelectListItem { Value = p.EducationTypeId.ToString(), Text = p.EducationType.ToString() });
                            }
                        }
                        else if (EduTypeID > 0)
                        {
                            userList = userList.Where(x => x.EducationTypeId == EduTypeID).ToList();
                            var lst = userList.Select(o => new { CourseCategoryId = o.CourseCategoryId, CourseCategory = o.CourseCategory }).Distinct().ToList();
                            foreach (var p in lst)
                            {
                                items.Add(new SelectListItem { Value = p.CourseCategoryId.ToString(), Text = p.CourseCategory.ToString() });
                            }
                        }
                        else if (CourseCategoryID > 0)
                        {
                            userList = userList.Where(x => x.CourseCategoryId == CourseCategoryID).ToList();
                            var lst = userList.Select(o => new { SubjectId = o.SubjectId, Subject = o.Subject, SubjectCode = o.SubjectCode }).Distinct().ToList();
                            if (Type == "Detail")
                            {
                                foreach (var p in lst)
                                {
                                    items.Add(new SelectListItem { Value = p.SubjectId.ToString(), Text = p.Subject.ToString() + "  (" + p.SubjectCode + ")" });
                                }
                            }
                            else
                            {
                                foreach (var p in lst)
                                {
                                    items.Add(new SelectListItem { Value = p.SubjectCode.ToString(), Text = p.Subject.ToString() + "  (" + p.SubjectCode + ")" });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }
        public List<SelectListItem> GetEmployeeBySubject(int CollegeID = 0, int SubjectID = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Value = "", Text = "-- Select --" });
            try
            {
                if (CollegeID > 0 && SubjectID > 0)
                {
                    List<UserLogin> userList = new List<UserLogin>();
                    using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
                    {
                        var obj = conn.QueryMultiple("sp_CollegeEmployee_AssignSubject", new { @Action = "GetEmployeeBySubject", @CollegeID = CollegeID, @EmployeeID = 0, @SubjectID = SubjectID }, commandType: CommandType.StoredProcedure);
                        userList = obj.Read<UserLogin>().ToList();
                    }
                    if (userList != null)
                    {
                        foreach (var p in userList)
                        {
                            items.Add(new SelectListItem { Value = p.EmployeeID.ToString(), Text = p.UserName.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return items;
        }

        public UserLogin EmployeeSubjectAssign_Add(UserLogin ul)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                var obj = conn.Query<UserLogin>("[sp_CollegeEmployee_AssignSubject]", new
                {
                    @Action = "Insert",
                    @CollegeId = ul.CollegeID,
                    @EmployeeID = ul.EmployeeID,
                    @SubjectID = ul.SubjectId,
                    @SessionId = ul.SessionId,
                    @EducationTypeId = ul.EducationTypeId,
                    @CourseCategoryId = ul.CourseCategoryId,
                    @IsActive = ul.IsActive,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }
        public UserLogin EmployeeSubjectAssign_Edit(UserLogin ul)
        {
            using (IDbConnection conn = new SqlConnection(CommonSetting.constr))
            {

                var obj = conn.Query<UserLogin>("[sp_CollegeEmployee_AssignSubject]", new
                {
                    @Action = "Update",
                    @CollegeId = ul.CollegeID,
                    @EmployeeID = ul.EmployeeID,
                    @SubjectID = ul.SubjectId,
                    @SessionId = ul.SessionId,
                    @EducationTypeId = 0,
                    @CourseCategoryId = 0,
                    @IsActive = ul.IsActive,
                    @EmpAssignSubjectId = ul.EmpAssignSubjectId,
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return obj;
            }
        }
        #endregion
    }

    public class UserList
    {
        public List<UserLogin> qlist { get; set; }
        public string totalCount { get; set; }
    }
    public class ChangePasswordAdmin
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string CurrentPassword { get; set; }
        [StringLength(20, ErrorMessage = "Password must be at least {5} characters long.", MinimumLength = 5)]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        public string ApplicationID { get; set; }
        public string Email { get; set; }
        public string Msg { get; set; }
        public bool Status { get; set; }
        public int ID { get; set; }


    }
    public class UserRole
    {
        public int URoleID { get; set; }
        public string URole { get; set; }
    }
}
