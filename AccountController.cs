using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigOptic.Models;

namespace BigOptic.Controllers
{
    public class AccountController : Controller
    {
        BigOpticals DB = new BigOpticals();

        // GET: Account
        public ActionResult login()
        {
           
            return View();
        }

        public ActionResult SingUp()
        {
           
            return View();

        }



        public void CustomerSingUp(SignUpCustumer CustomerObj)
        {
            try {
                Session["UserName"] = Convert.ToString(CustomerObj.Usernamer);
                Session["Name"] = Convert.ToString(CustomerObj.NameCustumer);
                Session["UserID"] = Convert.ToString(CustomerObj.CustumerID);
                Session["UserEmail"] = Convert.ToString(CustomerObj.EmailCustomer);
                DB.SignUpCustumers.Add(CustomerObj);
                DB.SaveChanges();
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public JsonResult CustomerSingIN(SignUpCustumer Obj)
        {
            try
            {
                Session["UserName"] = Convert.ToString(Obj.Usernamer);
                Session["UserEmail"] = Convert.ToString(Obj.EmailCustomer);
                
               // Session["CustomerID"] = (from row in DB.SignUpCustumers select row); //not working this line
                var data = (from  row in DB.SignUpCustumers where (row.EmailCustomer==Obj.EmailCustomer || row.Usernamer==Obj.Usernamer) && row.Paswd==Obj.Paswd select 
                            new {
                                row.Usernamer,
                                row.NameCustumer,
                                row.CustumerID
                            }).ToList();
                
                return Json(data, JsonRequestBehavior.AllowGet);
               

            }
            catch (Exception ex)
            {
                throw;
            }


        }
    }
}