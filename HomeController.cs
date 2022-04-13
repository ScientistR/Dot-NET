using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcentity14220.Models;

namespace mvcentity14220.Controllers
{
    public class HomeController : Controller
    {
        mvc14220Entities db = new mvc14220Entities();
        Emp _emp = new Emp();
        country _ctr = new country();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCountry()
        {
            var data = db.countries.ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public void InsertUpdate(Emp _emp)
        {
            if (_emp.empid > 0)
            {
                db.Entry(_emp).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Emps.Add(_emp);
                db.SaveChanges();
            }
        }

        public void Delete(Emp _emp)
        {
            var data = db.Emps.Find(_emp.empid);
            db.Emps.Remove(data);
            db.SaveChanges();
        }

        public JsonResult GetEmp()
        {
            var data = (from a in db.Emps
                        join b in db.countries
                            on a.countryid equals b.cid
                        select new {a.empid,a.name,b.cname }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(Emp _emp)
        {
            var data = (from a in db.Emps where a.empid==_emp.empid select a).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
