using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigOptic.Models;
using System.IO;
using System.Web.Script.Serialization; // for serialize and deserialize 
using System.Data.Entity;

namespace BigOptic.Controllers
{
    public class CustomerController : Controller
    {

        BigOpticals DB = new BigOpticals();
        // GET: Customer
        public ActionResult Home()
        {
            try
            {
               
               
                if ((Session["UserName"] == null && Session["UserEmail"] == null))
                {
                    Response.Redirect("~/Account/SingUp");
                }

                return View();
            }
            catch(Exception ex) {
                throw;
            }
        }

        public ActionResult AboutUs()
        {
            try { 
            if (Session["UserName"] == null && Session["UserEmail"] == null)
            {
                Response.Redirect("~/Account/SingUp");
            }
            return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ContactUs()
        {
            try { 
            if (Session["UserName"] == null && Session["UserEmail"] == null)
            {
                Response.Redirect("~/Account/SingUp");
            }
            return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //-----------**********product Description page--------------------------------//
        public ActionResult DescriptionProduct()
        {
            try { 
            if (Session["UserName"] == null && Session["UserEmail"] == null)
            {
                Response.Redirect("~/Account/SingUp");
            }
            //var datPid = (from item in DB.Products select item.produc_ID);
            //Session["Pid"] = Convert.ToInt32(datPid);

            return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public JsonResult DescriptionList(Product Pid)
        {
            try { 

            var data = (from item in DB.Products
                        join Img in DB.ImageProducts on item.produc_ID equals Img.produc_ID
                        join Status in DB.ProductStatus on item.produc_ID equals Status.produc_ID
                        where item.produc_ID == Pid.produc_ID
                        select new
                                {
                                item.ProductName,
                                item.Color,
                                item.Decription,
                                item.Direction,
                                item.ItemWiegth,
                                item.Language1,
                                item.Language2,
                                item.Language3,
                                item.Language4,
                                item.Language5,
                                item.Language6,
                                item.MarketRate,
                                item.point1,
                                item.point2,
                                item.point3,
                                item.point4,
                                item.ProductCompany,
                                item.ProductPrice,
                                item.ProductState,
                                item.produc_ID,
                                item.ReturnState,
                                item.SaleBy,
                                
                                Img.ImgID,
                                Img.FrontImg,
                                Img.BackImg,
                                Img.SideImg,
                                Img.GroupImg,
                                
                                Status.PStatus_ID,
                                Status.productStock,
                                Status.warranttyTime,
                                Status.deliveryCharge
                                
                        }).ToList();

         
            return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //------------*****product page--------------------------------//
        public ActionResult Products()
        {
            try
            {
                if (Session["UserName"] == null && Session["UserEmail"] == null)
                {
                    Response.Redirect("~/Account/SingUp");
                }
                return View();
            }
            
            catch(Exception ex) {
                throw;
            }
        }

        public JsonResult ProductsList()
        {
            try
            {
                var data = (from item in DB.Products
                            join img in DB.ImageProducts on item.produc_ID equals img.produc_ID
                            orderby item.produc_ID descending
                            select new
                            {
                                item.ProductName,
                                item.Color,
                                item.Decription,
                                item.Direction,
                                item.ItemWiegth,
                                item.Language1,
                                item.Language2,
                                item.Language3,
                                item.Language4,
                                item.Language5,
                                item.Language6,
                                item.MarketRate,
                                item.point1,
                                item.point2,
                                item.point3,
                                item.point4,
                                item.ProductCompany,
                                item.ProductPrice,
                                item.ProductState,
                                item.produc_ID,
                                item.
                                ReturnState,
                                item.SaleBy,

                                img.ImgID,
                                img.FrontImg,
                                img.BackImg,
                                img.SideImg,
                                img.GroupImg
                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void CustomerOrder(CustmerOrder CusOrder)
        {
            try {

             if (Session["UserName"] == null && Session["UserEmail"] == null)
             {
                 Response.Redirect("~/Account/SingUp");
             }
            DB.CustmerOrders.Add(CusOrder);
            DB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult BuyNow()
        {
            try { 
                 if (Session["UserName"] == null && Session["UserEmail"] == null)
                 {
                     Response.Redirect("~/Account/SingUp");
                 }
                 return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public JsonResult BuyProduct(Product Pid)
         {
            try
            {
                var data = (from item in DB.Products
                            join Img in DB.ImageProducts on item.produc_ID equals Img.produc_ID
                            join Status in DB.ProductStatus on item.produc_ID equals Status.produc_ID
                            where item.produc_ID == Pid.produc_ID
                            select new
                            {
                                item.ProductName,
                                item.Color,
                                item.Decription,
                                item.Direction,
                                item.ItemWiegth,
                                item.Language1,
                                item.Language2,
                                item.Language3,
                                item.Language4,
                                item.Language5,
                                item.Language6,
                                item.MarketRate,
                                item.point1,
                                item.point2,
                                item.point3,
                                item.point4,
                                item.ProductCompany,
                                item.ProductPrice,
                                item.ProductState,
                                item.produc_ID,
                                item.ReturnState,
                                item.SaleBy,

                                Img.ImgID,
                                Img.FrontImg,
                                Img.BackImg,
                                Img.SideImg,
                                Img.GroupImg,

                                Status.PStatus_ID,
                                Status.productStock,
                                Status.warranttyTime,
                                Status.deliveryCharge

                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            
            }
             catch(Exception ex) {
                throw;
            }
        }

        //get customer datil for oder page
        public JsonResult CustomerDeatils()
        {
            try
            {
              if ((Session["UserName"] == null && Session["UserEmail"] == null))
              {
                  Response.Redirect("~/Account/SingUp");
              }
               string UserName = Convert.ToString(Session["UserName"].ToString());
               string UserEmail = Convert.ToString(Session["UserEmail"].ToString());
                var data = (from lits in DB.SignUpCustumers
                             where lits.Usernamer == UserName || lits.EmailCustomer== UserEmail 
                           // where lits.Usernamer == "Ravi123"
                            select lits).ToList();

                            return Json(data,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //View Order
        public ActionResult ViewOrder()
        {
            try {
               if (Session["UserName"] == null && Session["UserEmail"] == null)
               {
                   Response.Redirect("~/Account/SingUp");
               }

                return View();
            } catch (Exception ex) {

                throw;
            }


        }


        //-------View Order--------------
        public ActionResult ViewProfile()
        {
            try
            {
                if (Session["UserName"] == null && Session["UserEmail"] == null)
                {
                    Response.Redirect("~/Account/SingUp");
                }


                string file = Server.MapPath("~/Anglur/JSON/StateJson.json");
                //deserialize JSON from file  
                string Json1 = System.IO.File.ReadAllText(file);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                 var personlist = ser.Deserialize<List<StateData>>(Json1).ToList();


                ViewBag.CounrtyList = new SelectList(personlist, "SID", "name");
             

                // return Json(personlist, JsonRequestBehavior.AllowGet);

                return View();
            }
            catch (Exception ex)
            {
                throw;
            }


        }
       //*******---Update Profile------
        [HttpPost]
        public ActionResult ViewProfile(SignUpCustumer CustID, HttpPostedFileBase FrontImgPost)
        {
            try {
                if (Session["UserName"] == null && Session["UserEmail"] == null)
                {
                    Response.Redirect("~/Account/SingUp");
                }

                // CustID.CustumerID =Convert.ToInt32(HidID["CustID"]);, FormCollection HidID
                if (FrontImgPost!=null) 
                { 
                string filename = Path.GetFileNameWithoutExtension(FrontImgPost.FileName);
                string ext = Path.GetExtension(FrontImgPost.FileName);
                filename = filename + ext;
                CustID.PhotoCustomer = "../ImageUpload/" + filename;
                
                //save in folder
                filename = Path.Combine(Server.MapPath("../ImageUpload/"), filename);
                FrontImgPost.SaveAs(filename);
                }

                SignUpCustumer ExistingRecord = (from s in DB.SignUpCustumers
                where s.CustumerID == CustID.CustumerID select s).FirstOrDefault();

                if (CustID.PhotoCustomer!=null) 
                { 
                     ExistingRecord.PhotoCustomer = CustID.PhotoCustomer;
                }

                ExistingRecord.NameCustumer = CustID.NameCustumer;
                ExistingRecord.Usernamer = CustID.Usernamer;
                ExistingRecord.GanderCustomer= CustID.GanderCustomer;
                ExistingRecord.PhoneCustomer = CustID.PhoneCustomer;
                ExistingRecord.MobileCustomer = CustID.MobileCustomer;
                ExistingRecord.Adresss1Customer = CustID.Adresss1Customer;
                ExistingRecord.Adresss2Customer = CustID.Adresss2Customer;
                ExistingRecord.EmailCustomer = CustID.EmailCustomer;
                ExistingRecord.ZipCode = CustID.ZipCode;
                ExistingRecord.States= CustID.States;
                ExistingRecord.City = CustID.City;
            
                DB.SaveChanges();
               return View("ViewProfile");
            }
            catch (Exception ex) {
                throw;
            }
        }

        public JsonResult getCity(JsonData City)
        {
            try
            {

                //get the Json filepath  
                string file = Server.MapPath("~/Anglur/JSON/CityJson.json");
                //deserialize JSON from file  
                string Json1 = System.IO.File.ReadAllText(file);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var personlist = ser.Deserialize<List<JsonData>>(Json1);
                var data = (from list in personlist where City.SID == list.SID select list);
                // var data = (from list in personlist where City.name == list.state select list).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public JsonResult getState()
        {
            //get the Json filepath  
            //string file = Server.MapPath("~/Anglur/JSON/Fakejson.json");
            string file = Server.MapPath("~/Anglur/JSON/StateJson.json");
            //deserialize JSON from file  
            string Json1 = System.IO.File.ReadAllText(file);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var personlist = ser.Deserialize<List<StateData>>(Json1);

            return Json(personlist, JsonRequestBehavior.AllowGet);

        }
    }

}
