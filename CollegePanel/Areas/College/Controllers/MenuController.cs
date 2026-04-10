using DataLayer;
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
    public class MenuController : Controller
    {
        // GET: Administrator/Menu
        public ActionResult Index()
        {
            return View();
        }
       
        public  PartialViewResult _CollegeSideMenu()
        {
            IEnumerable<College_MenuMaster> Menu = null;
            UserLogin obj = new UserLogin();
            var lastlogin = "";
           
            var id = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
            obj = obj.userdata(id);
            College_MenuMaster menu = new College_MenuMaster();           
            Menu = menu.getCollegeMenuList("CollegeViewAll").ToList();          
            //Menu.(s => s.la).LastLogin = lastlogin;
            if (obj.menustr == null || obj.menustr == "")
            {
                obj.menustr = ",";
            }
            string[] values = obj.menustr.Split(',');
            List<College_MenuMaster> i = Menu.Where(m => values.Contains(m.MenuID.ToString())).ToList();
            //Menu.ToList();
           
            return PartialView("_CollegeSideMenu", i);
        }
      
        public PartialViewResult _CollegeMenuView()
        {
            IEnumerable<College_MenuMaster> Menu = null;
            UserLogin obj = new UserLogin();
            var id = Convert.ToInt32(EncriptDecript.Decrypt(ClsLanguage.GetCookies("ENNBUID")));
            obj = obj.userdata(id);
            College_MenuMaster menu = new College_MenuMaster();
            Menu = menu.getCollegeMenuList("CollegeViewAll").ToList();

            if (obj.menustr == null || obj.menustr == "")
            {
                obj.menustr = ",";
            }
            string[] values = obj.menustr.Split(',');
            List<College_MenuMaster> i = Menu.Where(m => values.Contains(m.MenuID.ToString())).ToList();
            //Menu.ToList();
            return PartialView("_CollegeMenuView", i);
        }
    }
}