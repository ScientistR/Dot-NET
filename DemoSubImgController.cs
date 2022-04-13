using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BigOptic.Models;

namespace BigOptic.Controllers
{
    public class DemoSubImgController : Controller
    {
        private BigOpticals db = new BigOpticals();

        // GET: DemoSubImg
        public ActionResult Index()
        {
            return View(db.SubImageIndexPages.ToList());
        }

        // GET: DemoSubImg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubImageIndexPage subImageIndexPage = db.SubImageIndexPages.Find(id);
            if (subImageIndexPage == null)
            {
                return HttpNotFound();
            }
            return View(subImageIndexPage);
        }

        // GET: DemoSubImg/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DemoSubImg/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SubImage,Slide1_Img1,BackImg_Slide1_Img1,SideImgS_lide1_Img1,GroupImg_Slide1_Img1,Name_Slide1_Img1,Rate_Slide1_Img1,Slide1_Img2,BackImg_Slide1_Img2,SideImg_Slide1_Img2,GroupImg_Slide1_Img2,name_Slide1_Img2,rate_Slide1_Img2,Slide1_Img3,BackImg_Slide1_Img3,SideImg_Slide1_Img3,GroupImg_Slide1_Img3,name_Slide1_Img3,rate_Slide1_Img3,Slide1_Img4,BackImg_Slide1_Img4,SideImg_Slide1_Img4,GroupImg_Slide1_Img4,name_Slide1_Img4,rate_Slide1_Img4,Slide1_Img5,BackImg_Slide1_Img5,SideImg_Slide1_Img5,GroupImg_Slide1_Img5,name_Slide1_Img5,rate_Slide1_Img5,Slide1_Img6,BackImg_Slide1_Img6,SideImg_Slide1_Img6,GroupImg_Slide1_Img6,name_Slide1_Img6,rate_Slide1_Img6,Slide2_Img1,BackImg_Slide2_Img1,SideImg_Slide2_Img1,GroupImg_Slide2_Img1,name_Slide2_Img1,rate_Slide2_Img1,Slide2_Img2,BackImg_Slide2_Img2,SideImg_Slide2_Img2,GroupImg_Slide2_Img2,name_Slide2_Img2,rate_Slide2_Img2,Slide2_Img3,BackImg_Slide2_Img3,SideImg_Slide2_Img3,GroupImg_Slide2_Img3,name_Slide2_Img3,rate_Slide2_Img3,Slide2_Img4,BackImg_Slide2_Img4,SideImg_Slide2_Img4,GroupImg_Slide2_Img4,name_Slide2_Img4,rate_Slide2_Img4,Slide2_Img5,BackImg_Slide2_Img5,SideImg_Slide2_Img5,GroupImg_Slide2_Img5,name_Slide2_Img5,rate_Slide2_Img5,Slide2_Img6,BackImg_Slide2_Img6,SideImg_Slide2_Img6,GroupImg_Slide2_Img6,name_Slide2_Img6,rate_Slide2_Img6,Long_Img,BackImgLong_Img,SideImgLong_Img,GroupImgLong_Img,Other_Img1,BackImgOther_Img1,SideImgOther_Img1,GroupImg_,Other_Img2,BackImgOther_Img2,SideImgOther_Img2,GroupImgOther_Img2,Other_Img3,BackImgOther_Img3,SideImgOther_Img3,GroupImgOther_Img3,Other_Img4,BackImgOther_Img4,SideImgOther_Img4,GroupImgOther_Img4,Other_Img5,BackImgOther_Img5,SideImgOther_Img5,GroupImgOther_Img5,Other_Img6,BackImgOther_Img6,SideImgOther_Img6,GroupImgOther_Img6,IndexID")] SubImageIndexPage subImageIndexPage)
        {
            if (ModelState.IsValid)
            {
                db.SubImageIndexPages.Add(subImageIndexPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subImageIndexPage);
        }

        // GET: DemoSubImg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubImageIndexPage subImageIndexPage = db.SubImageIndexPages.Find(id);
            if (subImageIndexPage == null)
            {
                return HttpNotFound();
            }
            return View(subImageIndexPage);
        }

        // POST: DemoSubImg/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SubImage,Slide1_Img1,BackImg_Slide1_Img1,SideImgS_lide1_Img1,GroupImg_Slide1_Img1,Name_Slide1_Img1,Rate_Slide1_Img1,Slide1_Img2,BackImg_Slide1_Img2,SideImg_Slide1_Img2,GroupImg_Slide1_Img2,name_Slide1_Img2,rate_Slide1_Img2,Slide1_Img3,BackImg_Slide1_Img3,SideImg_Slide1_Img3,GroupImg_Slide1_Img3,name_Slide1_Img3,rate_Slide1_Img3,Slide1_Img4,BackImg_Slide1_Img4,SideImg_Slide1_Img4,GroupImg_Slide1_Img4,name_Slide1_Img4,rate_Slide1_Img4,Slide1_Img5,BackImg_Slide1_Img5,SideImg_Slide1_Img5,GroupImg_Slide1_Img5,name_Slide1_Img5,rate_Slide1_Img5,Slide1_Img6,BackImg_Slide1_Img6,SideImg_Slide1_Img6,GroupImg_Slide1_Img6,name_Slide1_Img6,rate_Slide1_Img6,Slide2_Img1,BackImg_Slide2_Img1,SideImg_Slide2_Img1,GroupImg_Slide2_Img1,name_Slide2_Img1,rate_Slide2_Img1,Slide2_Img2,BackImg_Slide2_Img2,SideImg_Slide2_Img2,GroupImg_Slide2_Img2,name_Slide2_Img2,rate_Slide2_Img2,Slide2_Img3,BackImg_Slide2_Img3,SideImg_Slide2_Img3,GroupImg_Slide2_Img3,name_Slide2_Img3,rate_Slide2_Img3,Slide2_Img4,BackImg_Slide2_Img4,SideImg_Slide2_Img4,GroupImg_Slide2_Img4,name_Slide2_Img4,rate_Slide2_Img4,Slide2_Img5,BackImg_Slide2_Img5,SideImg_Slide2_Img5,GroupImg_Slide2_Img5,name_Slide2_Img5,rate_Slide2_Img5,Slide2_Img6,BackImg_Slide2_Img6,SideImg_Slide2_Img6,GroupImg_Slide2_Img6,name_Slide2_Img6,rate_Slide2_Img6,Long_Img,BackImgLong_Img,SideImgLong_Img,GroupImgLong_Img,Other_Img1,BackImgOther_Img1,SideImgOther_Img1,GroupImg_,Other_Img2,BackImgOther_Img2,SideImgOther_Img2,GroupImgOther_Img2,Other_Img3,BackImgOther_Img3,SideImgOther_Img3,GroupImgOther_Img3,Other_Img4,BackImgOther_Img4,SideImgOther_Img4,GroupImgOther_Img4,Other_Img5,BackImgOther_Img5,SideImgOther_Img5,GroupImgOther_Img5,Other_Img6,BackImgOther_Img6,SideImgOther_Img6,GroupImgOther_Img6,IndexID")] SubImageIndexPage subImageIndexPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subImageIndexPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subImageIndexPage);
        }

        // GET: DemoSubImg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubImageIndexPage subImageIndexPage = db.SubImageIndexPages.Find(id);
            if (subImageIndexPage == null)
            {
                return HttpNotFound();
            }
            return View(subImageIndexPage);
        }

        // POST: DemoSubImg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubImageIndexPage subImageIndexPage = db.SubImageIndexPages.Find(id);
            db.SubImageIndexPages.Remove(subImageIndexPage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
