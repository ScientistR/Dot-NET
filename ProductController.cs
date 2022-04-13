using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigOptic.Models;
using System.IO;
//using System.Data.Entity;


namespace BigOptic.AdminController
{
    public class ProductController : Controller
    {
        BigOpticals DB = new BigOpticals();




        public ActionResult SaveView()
        {
            return View();
        }

        public void SavePoductDetail(Product Prd)
        {
            DB.Products.Add(Prd);
            DB.SaveChanges();
        }

        public JsonResult ShowList()
        {
           // var data = (from item in DB.Products orderby item.produc_ID descending select item).ToList();
           var data = (from item in DB.Products
                       join Img in DB.ImageProducts on item.produc_ID equals Img.produc_ID 
                       join Status in DB.ProductStatus on item.produc_ID equals Status.produc_ID
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

        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(ImageProduct P)
        {
            try
            {

                // Frong Image
                string filename = Path.GetFileNameWithoutExtension(P.FrontImgPost.FileName);
                string ext = Path.GetExtension(P.FrontImgPost.FileName);
                filename = filename + ext;
                P.FrontImg = "../ImageUpload/" + filename;
                //save in folder
                filename = Path.Combine(Server.MapPath("../ImageUpload/"), filename);
                P.FrontImgPost.SaveAs(filename);

                // Back Image
                string filename2 = Path.GetFileNameWithoutExtension(P.BackImgPost.FileName);
                string ext2 = Path.GetExtension(P.BackImgPost.FileName);
                filename2 = filename2 + ext2;
                P.BackImg = "../ImageUpload/" + filename2;
                filename2 = Path.Combine(Server.MapPath("../ImageUpload/"), filename2);
                P.BackImgPost.SaveAs(filename2);

                // Side Image
                string filename3 = Path.GetFileNameWithoutExtension(P.SideImgPost.FileName);
                string ext3 = Path.GetExtension(P.SideImgPost.FileName);
                filename3 = filename3 + ext3;
                P.SideImg = "../ImageUpload/" + filename3;
                //save in folder
                filename3 = Path.Combine(Server.MapPath("../ImageUpload/"), filename3);
                P.SideImgPost.SaveAs(filename3);

                // Thourth Image
                string filename4 = Path.GetFileNameWithoutExtension(P.GroupImgPost.FileName);
                string ext4 = Path.GetExtension(P.GroupImgPost.FileName);
                filename4 = filename4 + ext4;
                P.GroupImg = "../ImageUpload/" + filename4;
                //save in folder
                filename4 = Path.Combine(Server.MapPath("../ImageUpload/"), filename4);
                P.GroupImgPost.SaveAs(filename4);

                // DB.ImageProducts.Add(P);
                //  DB.Entry(P).State = System.Data.Entity.EntityState.Modified;
                //DB.SaveChanges();

                if (P.ImgID > 0)
                {
                    DB.Entry(P).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();

                }
                else
                {
                    DB.ImageProducts.Add(P);
                    DB.SaveChanges();
                    if (P.produc_ID > 0)
                    {
                        Product Pd = new Product();
                        Pd.produc_ID = P.produc_ID;
                        ImageStats(Pd);
                    }
                }

                return View("UploadImage");

            }

            catch (Exception ex)
            {
                throw;
            }
        }



        public JsonResult ImageList()
        {
            try
            {
                var data = (from item in DB.ImageProducts
                            join Lists in DB.Products on item.produc_ID equals Lists.produc_ID
                            orderby item.ImgID descending
                            select new
                            {
                                item.FrontImg,
                                item.BackImg,
                                item.SideImg,
                                item.GroupImg,
                                item.produc_ID,
                                item.ImgID,
                                Lists.ProductName

                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteProductList(Product Pid)
        {
            try
            {
                var row = DB.Products.Find(Pid.produc_ID);
                DB.Products.Remove(row);
                DB.SaveChanges();
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public JsonResult EditProductTable(Product Pid)
        {
            try
            {
                //var Data = (from Record in DB.Products where Record.produc_ID == Pid.produc_ID select Record).ToList(); // ek single record aa rha h
                var data = (from item in DB.Products
                            join Img in DB.ImageProducts on item.produc_ID equals Img.produc_ID
                            join Status in DB.ProductStatus on item.produc_ID equals Status.produc_ID
                            where item.produc_ID == Pid.produc_ID
                            select
                                new
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

        public void UpdateProductTable(Product Pid)
        {
            try
            {
                DB.Entry(Pid).State = System.Data.Entity.EntityState.Modified;
                //DB.Entry(Pid).State =EntityState.Modified;
                DB.SaveChanges();
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        //public void ImageStats(Product Pid)//pid and  pImage
        //{
        //    using (BigOpticals DB = new BigOpticals())
        //    {
        //        var result = DB.Products.SingleOrDefault(b => b.produc_ID == Pid.produc_ID);
        //        if (result != null)
        //        {
        //            result.ProductImage = "1";
        //            DB.SaveChanges();
        //        }
        //     
        //    }
        //}


        public void ImageStats(Product Pid)//pid and  pImage
        {
            try
            {
                using (BigOpticals DB = new BigOpticals())
                {

                    var result = DB.Products.SingleOrDefault(b => b.produc_ID == Pid.produc_ID);
                    var image = result.ProductImage;
                    if (image == "0")
                    {
                        result.ProductImage = "1";
                    }
                    else
                    {

                        result.ProductImage = "0";
                    }
                    DB.SaveChanges();

                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public JsonResult ViewImage(ImageProduct Pid)
        {
            try
            {
                var Data = (from Record in DB.ImageProducts where Record.produc_ID == Pid.produc_ID select Record).ToList(); // ek single record aa rha h
                return Json(Data, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw;
            }
        }


        public void DeleteImgList(ImageProduct Pid)
        {
            try
            {
                var row = DB.ImageProducts.Find(Pid.ImgID);
                DB.ImageProducts.Remove(row);
                DB.SaveChanges();
                if (Pid.produc_ID > 0)
                {
                    Product Pd = new Product();
                    Pd.produc_ID = Pid.produc_ID;

                    ImageStats(Pd);
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public JsonResult EditImge(ImageProduct MID)
        {
            try
            {
                var data = (from item in DB.ImageProducts  //jis table m referce ids hogi usi table k baki table  join ki jayegi
                            join Lists in DB.Products on item.produc_ID equals Lists.produc_ID
                            where item.ImgID == MID.ImgID
                            select new
                            {
                                item.FrontImg,
                                item.BackImg,
                                item.SideImg,
                                item.GroupImg,
                                item.produc_ID,
                                item.ImgID,
                                Lists.ProductName

                            }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
        //*******------States Products----------*********************
        public ActionResult ProductStats()
        {
            return View();
        }
        //show table status 3 join required
        public JsonResult ProductStatsList()
        {
            try
            {
                var data = (from Img in DB.ImageProducts
                            join PrdList in DB.Products on Img.produc_ID equals PrdList.produc_ID

                            join status in DB.ProductStatus on PrdList.produc_ID equals status.produc_ID into PS
                            from CID in PS.DefaultIfEmpty()
                            orderby CID.PStatus_ID descending

                            select new
                            {

                                produc_ID = PrdList.produc_ID,
                                ProductName = PrdList.ProductName,

                                FrontImg = Img.FrontImg,
                                Img.ImgID,


                                //STCK =CID.productStock,
                                // STCK = CID != null ? CID.productStock : "N/A ",
                                PStatus_ID = CID != null ? CID.PStatus_ID : 0,
                                productStock = CID != null ? CID.productStock : "N/A ",
                                deliveryCharge = CID != null ? CID.deliveryCharge : 0,
                                warranttyTime = CID != null ? CID.warranttyTime : "N/A "
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        public void ProductStatsSave(ProductStatu PrdStatus)
        {
            try
            {

                var data = (from item in DB.Products select item).ToList();
                int PID = Convert.ToInt32(data.Max(x => x.produc_ID));

                var data1 = (from item in DB.ImageProducts select item).ToList();
                int MgID = Convert.ToInt32(data1.Max(x => x.ImgID));

                PrdStatus.produc_ID = PID;
                PrdStatus.ImgID = MgID;

                DB.ProductStatus.Add(PrdStatus);
                DB.SaveChanges();

            }

            catch (Exception ex)
            {
                throw;
            }

        }

        public JsonResult ProductStatsAdd(ImageProduct MID) //jis table referce ids usi table  ko liya jayega object m
        {
            try
            {
                var data = (from item in DB.ImageProducts
                            join Lists in DB.Products on item.produc_ID equals Lists.produc_ID
                            where item.ImgID == MID.ImgID
                            select new
                            {
                                item.FrontImg,
                                item.ImgID,
                                Lists.ProductName,
                                Lists.produc_ID
                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult ProductStatsEdit(ProductStatu StatuId)
        {
            try
            {
                var data = (from status in DB.ProductStatus
                            join Prd in DB.Products on status.produc_ID equals Prd.produc_ID
                            join Img in DB.ImageProducts on status.ImgID equals Img.ImgID
                            where status.PStatus_ID == StatuId.PStatus_ID
                            select new
                            {
                                status.deliveryCharge,
                                status.warranttyTime,
                                status.productStock,
                                status.PStatus_ID,// upload k liye Id leni padti h

                                Prd.ProductName,
                                Prd.produc_ID,

                                Img.ImgID,
                                Img.FrontImg

                            }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public void ProductStatsUpdate(ProductStatu StatuId)
        {

            try
            {

                var data = DB.Entry(StatuId).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
            }

            catch (Exception ex)
            {
                throw;
            }
            //DB.Entry(Pid).State = System.Data.Entity.EntityState.Modified;
            ////DB.Entry(Pid).State =EntityState.Modified;
            //DB.SaveChanges();
        }

        //*******Customer Oder**----------//

        public ActionResult ViewCustomerOrder()
        {

            return View();

        }
        public JsonResult OrderList()
        {
            try
            {
                // double data le kr aa rha h 
                var data = (from List in DB.CustmerOrders
                            join Customer in DB.CustomerDatails on List.CustumerID equals Customer.CustumerID
                            orderby List.orderID descending
                            select new
                            {

                                Customer.CustomerName,
                                Customer.CustomerMobile,
                                Customer.CustomerPhone,
                                Customer.CustomerEmail,
                                Customer.CustomerAddres,
                                Customer.CustomerCity,
                                Customer.CustomerState,
                                Customer.CustomerCounty,



                                List.orderID,
                                List.ProdoctImg,
                                List.ProductsName,
                                List.ProductsPrice,
                                List.ImgID,
                                List.StatusID,
                                List.DeliveryCharge,
                                List.warranty,
                                List.DateTime1,
                                List.Qty,
                                List.CustumerID,
                                List.ProductID,
                                List.TotalAmount
                            }).ToList();
                //int count = (from x in DB.CustmerOrders select x).Count();

                return Json(data, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                throw;
            }

        }


        //OrderDetails page 
        public ActionResult OrderDetails()
        {
            return View();
        }
        // single Deatil k liye
        public JsonResult Customer_OrderDetails(CustmerOrder order)
        {
            try
            {

                var data = (from List in DB.CustmerOrders
                            join Customer in DB.CustomerDatails on List.CustumerID equals Customer.CustumerID
                            where List.orderID == order.orderID
                            select new
                            {

                                Customer.CustomerName,
                                Customer.CustomerPhone,
                                Customer.CustomerMobile,
                                Customer.CustomerEmail,
                                Customer.CustomerAddres,
                                Customer.CustomerCity,
                                Customer.CustomerState,
                                Customer.CustomerCounty,



                                List.orderID,
                                List.ProdoctImg,
                                List.ProductsName,
                                List.ProductsPrice,
                                List.ImgID,
                                List.StatusID,
                                List.DeliveryCharge,
                                List.warranty,
                                List.DateTime1,
                                List.Qty,
                                List.CustumerID,
                                List.ProductID,
                                List.TotalAmount
                            }).ToList();
                //int count = (from x in DB.CustmerOrders select x).Count();

                return Json(data, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                throw;
            }

        }



        public ActionResult GenerateBill()
        {
            return View();
        }
        public ActionResult GetPDF()
        {
            //return new Rotativa.ActionAsPdf("GenerateBill");
            return new Rotativa.ViewAsPdf("GenerateBill");
        }

        //add Insex image
        public ActionResult UploadIndexPage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadIndexPage(IndexImage Images)
        {

            try
            {
                //Barner 1
                if (Images.PostBanner1_Img != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(Images.PostBanner1_Img.FileName);
                    string ext = Path.GetExtension(Images.PostBanner1_Img.FileName);
                    filename = filename + ext;
                    Images.Banner1_Img = "../ImageUpload/" + filename;//Culom name giver "Banner1_Img"
                                                                      //save in folder
                    filename = Path.Combine(Server.MapPath("../ImageUpload/"), filename);
                    Images.PostBanner1_Img.SaveAs(filename);
                }

                //Barner 2
                if (Images.PostBanner2_Img != null)
                {
                    string Barnerfilename2 = Path.GetFileNameWithoutExtension(Images.PostBanner2_Img.FileName);
                    string Barner2ext = Path.GetExtension(Images.PostBanner2_Img.FileName);
                    Barnerfilename2 = Barnerfilename2 + Barner2ext;
                    Images.Banner2_Img = "../ImageUpload/" + Barnerfilename2;//Culom name giver "Banner1_Img"
                    //save in folder
                    Barnerfilename2 = Path.Combine(Server.MapPath("../ImageUpload/"), Barnerfilename2);
                    Images.PostBanner2_Img.SaveAs(Barnerfilename2);
                }

                if (Images.PostBanner3_Img != null)
                {
                    //Barner 3
                    string Barner3filename = Path.GetFileNameWithoutExtension(Images.PostBanner3_Img.FileName);
                    string Barner3ext = Path.GetExtension(Images.PostBanner3_Img.FileName);
                    Barner3filename = Barner3filename + Barner3ext;
                    Images.Banner3_Img = "../ImageUpload/" + Barner3filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Barner3filename = Path.Combine(Server.MapPath("../ImageUpload/"), Barner3filename);
                    Images.PostBanner3_Img.SaveAs(Barner3filename);
                }

                //Barner 4
                if (Images.PostBanner4_Img != null)
                {
                    string Barner4filename = Path.GetFileNameWithoutExtension(Images.PostBanner4_Img.FileName);
                    string Barner4ext = Path.GetExtension(Images.PostBanner4_Img.FileName);
                    Barner4filename = Barner4filename + Barner4ext;
                    Images.Banner4_Img = "../ImageUpload/" + Barner4filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Barner4filename = Path.Combine(Server.MapPath("../ImageUpload/"), Barner4filename);
                    Images.PostBanner4_Img.SaveAs(Barner4filename);
                }

                //Barner 5
                if (Images.PostBanner5_Img != null)
                {
                    string Barner5filename = Path.GetFileNameWithoutExtension(Images.PostBanner5_Img.FileName);
                    string Barner5ext = Path.GetExtension(Images.PostBanner5_Img.FileName);
                    Barner5filename = Barner5filename + Barner5ext;
                    Images.Banner5_Img = "../ImageUpload/" + Barner5filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Barner5filename = Path.Combine(Server.MapPath("../ImageUpload/"), Barner5filename);
                    Images.PostBanner5_Img.SaveAs(Barner5filename);
                }



                //Barner 6
                if (Images.PostBanner6_Img != null)
                {
                    string Barner6filename = Path.GetFileNameWithoutExtension(Images.PostBanner6_Img.FileName);
                    string Barner6ext = Path.GetExtension(Images.PostBanner6_Img.FileName);
                    Barner6filename = Barner6filename + Barner6ext;
                    Images.Banner6_Img = "../ImageUpload/" + Barner6filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Barner6filename = Path.Combine(Server.MapPath("../ImageUpload/"), Barner6filename);
                    Images.PostBanner6_Img.SaveAs(Barner6filename);
                }
                //**** *********Slide 1****************
                //Slide 1 
                if (Images.PostSlide1_Img1 != null)
                {
                    string Slide1filename = Path.GetFileNameWithoutExtension(Images.PostSlide1_Img1.FileName);
                    string Slide1ext = Path.GetExtension(Images.PostSlide1_Img1.FileName);
                    Slide1filename = Slide1filename + Slide1ext;
                    Images.Slide1_Img1 = "../ImageUpload/" + Slide1filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Slide1filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide1filename);
                    Images.PostSlide1_Img1.SaveAs(Slide1filename);
                }

                //Slide 2
                if (Images.PostSlide1_Img2 != null)
                {
                    string Slide2filename = Path.GetFileNameWithoutExtension(Images.PostSlide1_Img2.FileName);
                    string Slide2ext = Path.GetExtension(Images.PostSlide1_Img1.FileName);
                    Slide2filename = Slide2filename + Slide2ext;
                    Images.Slide1_Img2 = "../ImageUpload/" + Slide2filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Slide2filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide2filename);
                    Images.PostSlide1_Img2.SaveAs(Slide2filename);
                }


                //Slide 3
                if (Images.PostSlide1_Img3 != null)
                {
                    string Slide3filename = Path.GetFileNameWithoutExtension(Images.PostSlide1_Img3.FileName);
                    string Slide3ext = Path.GetExtension(Images.PostSlide1_Img3.FileName);
                    Slide3filename = Slide3filename + Slide3ext;
                    Images.Slide1_Img3 = "../ImageUpload/" + Slide3filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Slide3filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide3filename);
                    Images.PostSlide1_Img3.SaveAs(Slide3filename);
                }


                //Slide 4 
                if (Images.PostSlide1_Img4 != null)
                {
                    string Slide4filename = Path.GetFileNameWithoutExtension(Images.PostSlide1_Img4.FileName);
                    string Slide4ext = Path.GetExtension(Images.PostSlide1_Img4.FileName);
                    Slide4filename = Slide4filename + Slide4ext;
                    Images.Slide1_Img4 = "../ImageUpload/" + Slide4filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Slide4filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide4filename);
                    Images.PostSlide1_Img4.SaveAs(Slide4filename);
                }

                //Slide 5
                if (Images.PostSlide1_Img5 != null)
                {
                    string Slide5filename = Path.GetFileNameWithoutExtension(Images.PostSlide1_Img5.FileName);
                    string Slide5ext = Path.GetExtension(Images.PostSlide1_Img5.FileName);
                    Slide5filename = Slide5filename + Slide5ext;
                    Images.Slide1_Img5 = "../ImageUpload/" + Slide5filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Slide5filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide5filename);
                    Images.PostSlide1_Img5.SaveAs(Slide5filename);
                }


                //Slide 6 
                if (Images.PostSlide1_Img6 != null)
                {
                    string Slide6filename = Path.GetFileNameWithoutExtension(Images.PostSlide1_Img6.FileName);
                    string Slide6ext = Path.GetExtension(Images.PostSlide1_Img6.FileName);
                    Slide6filename = Slide6filename + Slide6ext;
                    Images.Slide1_Img6 = "../ImageUpload/" + Slide6filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Slide6filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide6filename);
                    Images.PostSlide1_Img6.SaveAs(Slide6filename);
                }
                //************* Slide 2**********
                //Slide 1   
                if (Images.PostSlide2_Img1 != null)
                {
                    string Slide1_filename = Path.GetFileNameWithoutExtension(Images.PostSlide2_Img1.FileName);
                    string Slide1_ext = Path.GetExtension(Images.PostSlide2_Img1.FileName);
                    Slide1_filename = Slide1_filename + Slide1_ext;
                    Images.Slide2_Img1 = "../ImageUpload/" + Slide1_filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Slide1_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide1_filename);
                    Images.PostSlide2_Img1.SaveAs(Slide1_filename);
                }

                //Slide 2
                if (Images.PostSlide2_Img2 != null)
                {
                    string Slide2_filename = Path.GetFileNameWithoutExtension(Images.PostSlide2_Img2.FileName);
                    string Slide2_ext = Path.GetExtension(Images.PostSlide2_Img1.FileName);
                    Slide2_filename = Slide2_filename + Slide2_ext;
                    Images.Slide2_Img2 = "../ImageUpload/" + Slide2_filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Slide2_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide2_filename);
                    Images.PostSlide1_Img2.SaveAs(Slide2_filename);
                }


                //Slide 3
                if (Images.PostSlide2_Img3 != null)
                {
                    string Slide3_filename = Path.GetFileNameWithoutExtension(Images.PostSlide2_Img3.FileName);
                    string Slide3_ext = Path.GetExtension(Images.PostSlide2_Img3.FileName);
                    Slide3_filename = Slide3_filename + Slide3_ext;
                    Images.Slide2_Img3 = "../ImageUpload/" + Slide3_filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Slide3_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide3_filename);
                    Images.PostSlide2_Img3.SaveAs(Slide3_filename);
                }


                //Slide 4 
                if (Images.PostSlide2_Img4 != null)
                {
                    string Slide4_filename = Path.GetFileNameWithoutExtension(Images.PostSlide2_Img4.FileName);
                    string Slide4_ext = Path.GetExtension(Images.PostSlide2_Img4.FileName);
                    Slide4_filename = Slide4_filename + Slide4_ext;
                    Images.Slide2_Img4 = "../ImageUpload/" + Slide4_filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Slide4_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide4_filename);
                    Images.PostSlide2_Img4.SaveAs(Slide4_filename);
                }


                //Slide 5
                if (Images.PostSlide2_Img5 != null)
                {
                    string Slide5_filename = Path.GetFileNameWithoutExtension(Images.PostSlide2_Img5.FileName);
                    string Slide5_ext = Path.GetExtension(Images.PostSlide2_Img5.FileName);
                    Slide5_filename = Slide5_filename + Slide5_ext;
                    Images.Slide2_Img5 = "../ImageUpload/" + Slide5_filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Slide5_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide5_filename);
                    Images.PostSlide2_Img5.SaveAs(Slide5_filename);
                }


                //Slide 6 
                if (Images.PostSlide2_Img6 != null)
                {
                    string Slide6_filename = Path.GetFileNameWithoutExtension(Images.PostSlide2_Img6.FileName);
                    string Slide6_ext = Path.GetExtension(Images.PostSlide2_Img6.FileName);
                    Slide6_filename = Slide6_filename + Slide6_ext;
                    Images.Slide1_Img6 = "../ImageUpload/" + Slide6_filename;//Culom name giver "Banner1_Img"
                                                                             //save in folder
                    Slide6_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Slide6_filename);
                    Images.PostSlide2_Img6.SaveAs(Slide6_filename);
                }
                //********Other Item*****
                //long image
                if (Images.PostLong_Img != null)
                {
                    string Long_filename = Path.GetFileNameWithoutExtension(Images.PostLong_Img.FileName);
                    string Long_ext = Path.GetExtension(Images.PostLong_Img.FileName);
                    Long_filename = Long_filename + Long_ext;
                    Images.Long_Img = "../ImageUpload/" + Long_filename;//Culom name giver "Banner1_Img"
                                                                        //save in folder
                    Long_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Long_filename);
                    Images.PostLong_Img.SaveAs(Long_filename);
                }

                //Other Image1
                if (Images.PostOther_Img1 != null)
                {
                    string Other_filename = Path.GetFileNameWithoutExtension(Images.PostOther_Img1.FileName);
                    string Other_ext = Path.GetExtension(Images.PostOther_Img1.FileName);
                    Other_filename = Other_filename + Other_ext;
                    Images.Other_Img1 = "../ImageUpload/" + Other_filename;//Culom name giver "Banner1_Img"
                                                                           //save in folder
                    Other_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Other_filename);
                    Images.PostOther_Img1.SaveAs(Other_filename);
                }
                //Other Image2
                if (Images.PostOther_Img2 != null)
                {
                    string Other2_filename = Path.GetFileNameWithoutExtension(Images.PostOther_Img2.FileName);
                    string Other2_ext = Path.GetExtension(Images.PostOther_Img2.FileName);
                    Other2_filename = Other2_filename + Other2_ext;
                    Images.Other_Img2 = "../ImageUpload/" + Other2_filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Other2_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Other2_filename);
                    Images.PostOther_Img2.SaveAs(Other2_filename);
                }


                //Other Image3
                if (Images.PostOther_Img3 != null)
                {
                    string Other3_filename = Path.GetFileNameWithoutExtension(Images.PostOther_Img3.FileName);
                    string Other3_ext = Path.GetExtension(Images.PostOther_Img3.FileName);
                    Other3_filename = Other3_filename + Other3_ext;
                    Images.Other_Img3 = "../ImageUpload/" + Other3_filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Other3_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Other3_filename);
                    Images.PostOther_Img3.SaveAs(Other3_filename);
                }
                //Other Image4

                if (Images.PostOther_Img4 != null)
                {
                    string Other4_filename = Path.GetFileNameWithoutExtension(Images.PostOther_Img4.FileName);
                    string Other4_ext = Path.GetExtension(Images.PostOther_Img4.FileName);
                    Other4_filename = Other4_filename + Other4_ext;
                    Images.Other_Img4 = "../ImageUpload/" + Other4_filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Other4_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Other4_filename);
                    Images.PostOther_Img4.SaveAs(Other4_filename);
                }
                //Other Image5

                if (Images.PostOther_Img5 != null)
                {
                    string Other5_filename = Path.GetFileNameWithoutExtension(Images.PostOther_Img5.FileName);
                    string Other5_ext = Path.GetExtension(Images.PostOther_Img5.FileName);
                    Other5_filename = Other5_filename + Other5_ext;
                    Images.Other_Img1 = "../ImageUpload/" + Other5_filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Other5_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Other5_filename);
                    Images.PostOther_Img5.SaveAs(Other5_filename);
                }



                //Other Image 6
                if (Images.PostOther_Img6 != null)
                {
                    string Other6_filename = Path.GetFileNameWithoutExtension(Images.PostOther_Img6.FileName);
                    string Other6_ext = Path.GetExtension(Images.PostOther_Img6.FileName);
                    Other6_filename = Other6_filename + Other6_ext;
                    Images.Other_Img6 = "../ImageUpload/" + Other6_filename;//Culom name giver "Banner1_Img"
                                                                            //save in folder
                    Other6_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Other6_filename);
                    Images.PostOther_Img6.SaveAs(Other6_filename);
                }

                //**********Bussiness*****
                //Bussiness 1
                if (Images.PostBussiness_Img1 != null)
                {
                    string Bussiness1_filename = Path.GetFileNameWithoutExtension(Images.PostBussiness_Img1.FileName);
                    string Bussiness1_ext = Path.GetExtension(Images.PostBussiness_Img1.FileName);
                    Bussiness1_filename = Bussiness1_filename + Bussiness1_ext;
                    Images.Bussiness_Img1 = "../ImageUpload/" + Bussiness1_filename;//Culom name giver "Banner1_Img"
                                                                                    //save in folder
                    Bussiness1_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Bussiness1_filename);
                    Images.PostBussiness_Img1.SaveAs(Bussiness1_filename);
                }
                //Bussiness 2
                if (Images.PostBussiness_Img2 != null)
                {
                    string Bussiness2_filename = Path.GetFileNameWithoutExtension(Images.PostBussiness_Img2.FileName);
                    string Bussiness2_ext = Path.GetExtension(Images.PostBussiness_Img2.FileName);
                    Bussiness2_filename = Bussiness2_filename + Bussiness2_ext;
                    Images.Bussiness_Img2 = "../ImageUpload/" + Bussiness2_filename;//Culom name giver "Banner1_Img"
                                                                                    //save in folder
                    Bussiness2_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Bussiness2_filename);
                    Images.PostBussiness_Img2.SaveAs(Bussiness2_filename);
                }

                //Bussiness 3
                if (Images.PostBussiness_Img3 != null)
                {
                    string Bussiness3_filename = Path.GetFileNameWithoutExtension(Images.PostBussiness_Img3.FileName);
                    string Bussiness3_ext = Path.GetExtension(Images.PostBussiness_Img3.FileName);
                    Bussiness3_filename = Bussiness3_filename + Bussiness3_ext;
                    Images.Bussiness_Img3 = "../ImageUpload/" + Bussiness3_filename;//Culom name giver "Banner1_Img"

                    //save in folder
                    Bussiness3_filename = Path.Combine(Server.MapPath("../ImageUpload/"), Bussiness3_filename);
                    Images.PostBussiness_Img3.SaveAs(Bussiness3_filename);
                }

                // DB.IndexImages.Add(Images);
                // DB.SaveChanges();

                IndexImage ExistingRecord = (from s in DB.IndexImages where s.IndexID == Images.IndexID select s).FirstOrDefault();

                //******Banner Imge*****
                ExistingRecord.Banner1_Img = Images.Banner1_Img;
                ExistingRecord.Banner2_Img = Images.Banner2_Img;
                ExistingRecord.Banner3_Img = Images.Banner3_Img;
                ExistingRecord.Banner4_Img = Images.Banner4_Img;
                ExistingRecord.Banner5_Img = Images.Banner5_Img;
                ExistingRecord.Banner6_Img = Images.Banner6_Img;

                //******Slider 1 Imge*****
                ExistingRecord.Slide1_Img1 = Images.Slide1_Img1;
                ExistingRecord.Slide1_Img2 = Images.Slide1_Img2;
                ExistingRecord.Slide1_Img3 = Images.Slide1_Img3;
                ExistingRecord.Slide1_Img4 = Images.Slide1_Img4;
                ExistingRecord.Slide1_Img5 = Images.Slide1_Img5;
                ExistingRecord.Slide1_Img6 = Images.Slide1_Img6;



                //******Slider 2 Imge*****
                ExistingRecord.Slide2_Img1 = Images.Slide2_Img1;
                ExistingRecord.Slide2_Img2 = Images.Slide2_Img2;
                ExistingRecord.Slide2_Img3 = Images.Slide2_Img3;
                ExistingRecord.Slide2_Img4 = Images.Slide2_Img4;
                ExistingRecord.Slide2_Img5 = Images.Slide2_Img5;
                ExistingRecord.Slide2_Img6 = Images.Slide2_Img6;


                //*****Other Imge******
                ExistingRecord.Long_Img = Images.Long_Img;
                ExistingRecord.Other_Img1 = Images.Other_Img1;
                ExistingRecord.Other_Img2 = Images.Other_Img2;
                ExistingRecord.Other_Img3 = Images.Other_Img3;
                ExistingRecord.Other_Img4 = Images.Other_Img4;
                ExistingRecord.Other_Img5 = Images.Other_Img5;
                ExistingRecord.Other_Img6 = Images.Other_Img6;

                //******Business******
                ExistingRecord.Bussiness_Img1 = Images.Bussiness_Img1;
                ExistingRecord.Bussiness_Img2 = Images.Bussiness_Img2;
                ExistingRecord.Bussiness_Img3 = Images.Bussiness_Img3;

                ExistingRecord.Imge_Category = Images.Imge_Category;

                DB.SaveChanges();

                return View("UploadIndexPage");

            }

            catch (Exception ex)
            {
                throw;
            }
        }





        //public ActionResult AddProducts()
        //{

        //    return View();

        //}


        //[HttpPost]
        //public ActionResult AddProducts(Product Prd)
        //{

        //    DB.Products.Add(Prd);
        //    DB.SaveChanges();
        //    return View();
        //}


        public void AddProducts(addProducts add)//empty not used in project
        {
            var Product = new Product()
            {

                ProductName = add.ProductName,
                SaleBy = add.SaleBy,
                ProductPrice = add.ProductPrice,
                MarketRate = add.MarketRate,
                ReturnState = add.ReturnState,
                ProductState = add.ProductState,
                Language1 = add.Language1,
                Language2 = add.Language2,
                Language3 = add.Language3,
                Language4 = add.Language4,
                Language5 = add.Language5,
                Language6 = add.Language6,
                Direction = add.Direction,
                Color = add.Color,
                ItemWiegth = add.ItemWiegth,
                ProductCompany = add.ProductCompany,
                point1 = add.point1,
                point2 = add.point2,
                point3 = add.point3,
                point4 = add.point4,
                Decription = add.Decription,
                ProductImage = add.ProductImage,

            };
            DB.Products.Add(Product);
            DB.SaveChanges();// ye bhut jaruri kqi last(max) id fetch k liye

            var data = (from item in DB.Products select item).ToList();
            int PID = Convert.ToInt32(data.Max(x => x.produc_ID));

            var ImageProduct = new ImageProduct()
            {
                produc_ID = PID,
                FrontImg = add.FrontImg,
                BackImg = add.BackImg,
                SideImg = add.SideImg,
                GroupImg = add.GroupImg,
                
            };

            DB.ImageProducts.Add(ImageProduct);
            DB.SaveChanges();// ye bhut jaruri kqi last(max) id fetch k liye

            var data1 = (from item in DB.ImageProducts select item).ToList();
            int MgID = Convert.ToInt32(data1.Max(x => x.ImgID));

            var ProductStatu = new ProductStatu()
            {
                produc_ID = PID,
              //produc_ID = add.produc_ID,
                ImgID = MgID,
                productStock = add.productStock,
                deliveryCharge = add.deliveryCharge,
                warranttyTime = add.warranttyTime,

            };
            DB.ProductStatus.Add(ProductStatu);
            DB.SaveChanges();
        }

        [HttpPost]
        public ActionResult SaveView(ImageProduct P)
        {

            try
            {

                // Frong Image
                string filename = Path.GetFileNameWithoutExtension(P.FrontImgPost.FileName);
                string ext = Path.GetExtension(P.FrontImgPost.FileName);
                filename = filename + ext;
                P.FrontImg = "../ImageUpload/" + filename;
                //save in folder
                filename = Path.Combine(Server.MapPath("../ImageUpload/"), filename);
                P.FrontImgPost.SaveAs(filename);

                // Back Image
                string filename2 = Path.GetFileNameWithoutExtension(P.BackImgPost.FileName);
                string ext2 = Path.GetExtension(P.BackImgPost.FileName);
                filename2 = filename2 + ext2;
                P.BackImg = "../ImageUpload/" + filename2;
                filename2 = Path.Combine(Server.MapPath("../ImageUpload/"), filename2);
                P.BackImgPost.SaveAs(filename2);

                // Side Image
                string filename3 = Path.GetFileNameWithoutExtension(P.SideImgPost.FileName);
                string ext3 = Path.GetExtension(P.SideImgPost.FileName);
                filename3 = filename3 + ext3;
                P.SideImg = "../ImageUpload/" + filename3;
                //save in folder
                filename3 = Path.Combine(Server.MapPath("../ImageUpload/"), filename3);
                P.SideImgPost.SaveAs(filename3);

                // Thourth Image
                string filename4 = Path.GetFileNameWithoutExtension(P.GroupImgPost.FileName);
                string ext4 = Path.GetExtension(P.GroupImgPost.FileName);
                filename4 = filename4 + ext4;
                P.GroupImg = "../ImageUpload/" + filename4;
                //save in folder
                filename4 = Path.Combine(Server.MapPath("../ImageUpload/"), filename4);
                P.GroupImgPost.SaveAs(filename4);

                // DB.ImageProducts.Add(P);
                //  DB.Entry(P).State = System.Data.Entity.EntityState.Modified;
                //DB.SaveChanges();

                if (P.ImgID > 0)
                {
                    DB.Entry(P).State = System.Data.Entity.EntityState.Modified;
                    DB.SaveChanges();

                }
                else
                {
                    var data = (from item in DB.Products select item).ToList();
                    int PID = Convert.ToInt32(data.Max(x => x.produc_ID));
                    P.produc_ID = PID;
                    DB.ImageProducts.Add(P);
                    DB.SaveChanges();
                    if (P.produc_ID > 0)
                    {
                        Product Pd = new Product();
                        Pd.produc_ID = P.produc_ID;
                        ImageStats(Pd);
                    }
                }

                return View("SaveView");

            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ImageIndex()
        {
            return View();
        }



    }
}