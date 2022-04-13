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
    public class DemoDescriptionsController : Controller
    {
        private BigOpticals db = new BigOpticals();

        // GET: DemoDescriptions
        public ActionResult Index()
        {
           
            return View(db.IndexDescriptions.ToList());
        }

        // GET: DemoDescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexDescription indexDescription = db.IndexDescriptions.Find(id);
            if (indexDescription == null)
            {
                return HttpNotFound();
            }
            return View(indexDescription);
        }

        // GET: DemoDescriptions/Create
        public ActionResult Create()
        {
            var colorList = new List<string> { "Black", "White", "Gray", "Red" };
            ViewBag.colorList = colorList;

            return View();
        }

        // POST: DemoDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "produc_ID,ProductName,SaleBy,ProductPrice,MarketRate,ReturnState,ProductState,Language1,Language2,Language3,Language4,Language5,Language6,Direction,Color,ItemWiegth,ProductCompany,point1,point2,point3,point4,Decription,ID_SubImage")] IndexDescription indexDescription)
        {
           
            if (ModelState.IsValid)
            {
                db.IndexDescriptions.Add(indexDescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indexDescription);
        }

        // GET: DemoDescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexDescription indexDescription = db.IndexDescriptions.Find(id);
            if (indexDescription == null)
            {
                return HttpNotFound();
            }
            return View(indexDescription);
        }

        // POST: DemoDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "produc_ID,ProductName,SaleBy,ProductPrice,MarketRate,ReturnState,ProductState,Language1,Language2,Language3,Language4,Language5,Language6,Direction,Color,ItemWiegth,ProductCompany,point1,point2,point3,point4,Decription,ID_SubImage")] IndexDescription indexDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indexDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indexDescription);
        }

        // GET: DemoDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IndexDescription indexDescription = db.IndexDescriptions.Find(id);
            if (indexDescription == null)
            {
                return HttpNotFound();
            }
            return View(indexDescription);
        }

        // POST: DemoDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IndexDescription indexDescription = db.IndexDescriptions.Find(id);
            db.IndexDescriptions.Remove(indexDescription);
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
