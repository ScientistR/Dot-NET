using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigOptic.Models;

namespace BigOptic.AdminController
{
    public class LoingController : Controller
    {
        BigOpticals DB = new BigOpticals();
        //AdminTable Tbl = new AdminTable(); 
        Product Pd = new Product();

        public ActionResult LoginIndex()
        {
            return View();
        }

        public JsonResult Login(AdminTable tbl)
        {
            
            
            var data = (from a in DB.AdminTables where a.UserName == tbl.UserName && a.Password == tbl.Password select a).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult showData()
        {
            var data = DB.AdminTables.ToList();
          
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}