using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Areas.college.Models;
using Website.Models;

namespace Website.Areas.College.Controllers
{
    [CookiesExpireFilterCollegeAttribute]
    public class PayRollController : Controller
    {
        // GET: College/Salary       
        public ActionResult EmployeeSalaryMaster(string id="")
        {
            EmployeeSalaryMaster obj = new EmployeeSalaryMaster();
            string enEID = "";
            int eID = 0;
            try
            {
                var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
                var collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
                ViewBag.Usertype = usertype;
                ViewBag.UserList = obj.GetUsermenuList(Convert.ToInt32(usertype), Convert.ToInt32(collegeId));
                ViewBag.PaybandList = obj.PaybandNameList();
                if (id != "0" && id.Length > 0)
                {
                    enEID = EncriptDecript.Decrypt(id);
                    if (enEID != "")
                    {
                        eID = Convert.ToInt32(enEID);
                    }
                    var result = obj.getdetailsByID(eID);
                    var Deduction_MasterList = obj.DeductionNameList();
                    var Allowance_MasterList = obj.AllowanceNameList();
                    result.Deduction_MasterList = obj.DeductionNameList();
                    result.Allowance_MasterList = obj.AllowanceNameList();
                    var DAresult = obj.GetDaAndHraValue();
                    result.HRApercentge = DAresult.HRApercentge;
                    result.DApercentage = DAresult.DApercentage;
                    if (result!=null)
                    {
                        var DeductionPerList = obj.DeductionPercentageList(result.aid);
                        var AllowancePerList = obj.AllowancePercentageList(result.aid);                        
                        for (int i = 0; i < DeductionPerList.Count; i++)
                        {
                            for(int j=0;j< result.Deduction_MasterList.Count;j++)
                            {
                                if (DeductionPerList[i].DeductionID == result.Deduction_MasterList[j].id)
                                {
                                    result.Deduction_MasterList[j].deductionname = DeductionPerList[i].deductionname;
                                    result.Deduction_MasterList[j].Amount = DeductionPerList[i].Amount;
                                   // result.Deduction_MasterList[j].AmountType = DeductionPerList[i].AmountType;
                                   
                                }
                            } 
                        }
                        for (int i = 0; i < AllowancePerList.Count; i++)
                        {
                            for (int j = 0; j < result.Allowance_MasterList.Count; j++)
                            {
                                if (AllowancePerList[i].AllowanceID == result.Allowance_MasterList[j].id)
                                {
                                    result.Allowance_MasterList[j].deductionname = AllowancePerList[i].deductionname;
                                    result.Allowance_MasterList[j].Amount = AllowancePerList[i].Amount;
                                    //result.Allowance_MasterList[j].AmountType = AllowancePerList[i].AmountType;
                                }
                            }
                        }
                    }
                   
                    return View(result);
                }
                else
                {
                    EmployeeSalaryMaster empsal = new EmployeeSalaryMaster();
                    empsal.Deduction_MasterList = obj.DeductionNameList();
                    empsal.Allowance_MasterList = obj.AllowanceNameList();
                    var DAresult = obj.GetDaAndHraValue();
                    empsal.HRApercentge = DAresult.HRApercentge;
                    empsal.DApercentage = DAresult.DApercentage;
                    return View(empsal);
                }
            }   
            catch(Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Employee Salary Master method", enEID);
                obj.Msg = "Something Went Wrong";
                return View(obj);
            }
            

          
        }
        public JsonResult GetDAAndHRA()
        {
            EmployeeSalaryMaster st = new EmployeeSalaryMaster();
            var obj = st.GetDaAndHraValue();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNewSalaryMaster(EmployeeSalaryMaster ob)
        {
            EmployeeSalaryMaster st = new EmployeeSalaryMaster();
            string jsonstring = JsonConvert.SerializeObject(ob);
            try
            {
                if (ClsLanguage.GetCookies("ENNBCLID") != null)
                {
                    ob.CollegeID = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
                }
                var insertby = ClsLanguage.GetCookies("ENNBUID");
                if (insertby != "")
                {
                    ob.InsertBy = Convert.ToInt32(EncriptDecript.Decrypt(insertby));
                }
                var result = st.AddNewSalaryMaster(ob);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add New SalaryMaster method", jsonstring);
                st.Msg = "Something Went Wrong";
                return Json(st, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EmployeeSalaryMasterList()
        {
            EmployeeSalaryMaster obj = new EmployeeSalaryMaster();
            var usertype = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUserType")));
            var collegeId = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBCLID")));
            ViewBag.Usertype = usertype;
            ViewBag.UserList = obj.GetUsermenuList(Convert.ToInt32(usertype), Convert.ToInt32(collegeId));           
            ViewBag.PaybandList = obj.PaybandNameList();
            return View();
        }
        [HttpPost]
        public JsonResult EmployeeSalaryDetail(string sid="")
        {
            string enEID = "";
            int eID = 0;
            EmployeeSalaryMaster ob = new EmployeeSalaryMaster();
            EmployeeSalaryMaster result = new EmployeeSalaryMaster();
            try
            {
                if (sid != "0" && sid.Length > 0)
                {
                    enEID = EncriptDecript.Decrypt(sid);
                    if (enEID != "")
                    {
                        eID = Convert.ToInt32(enEID);
                        var res = ob.SalaryMasterDetail(eID);
                        result.Deduction_MasterList = ob.DeductionPercentageList(eID);                      
                        result.Allowance_MasterList = ob.AllowancePercentageList(eID);
                        result.GrossTotal = res.GrossTotal;
                        result.NetTotal = res.NetTotal;
                        return Json(new { data = result, success = true });
                    } 
                }
                ob.Msg = "Something Went Wrong";
                return Json(new { data = ob, success = true });

            }
            catch (Exception ex)
            {
                CommonMethod.WritetoNotepad(ex, HttpContext.Request.Url.AbsolutePath, "Add New SalaryMaster method", enEID);
                ob.Msg = "Something Went Wrong";
                return Json(new { data = ob, success = true });
            }
        }
    }
}