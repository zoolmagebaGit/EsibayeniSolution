using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsibayeniSolution.Models;

namespace EsibayeniSolution.Controllers
{
    public class MaintainanceStocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaintainanceStocks
        public ActionResult Index()
        {
            var maintainanceStocks = db.MaintainanceStocks.Include(m => m.ProductCategory);
            return View(maintainanceStocks.ToList());
        }

        // GET: MaintainanceStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintainanceStock maintainanceStock = db.MaintainanceStocks.Find(id);
            if (maintainanceStock == null)
            {
                return HttpNotFound();
            }
            return View(maintainanceStock);
        }

        // GET: MaintainanceStocks/Create
        public ActionResult Create()
        {
            ViewBag.PCatID = new SelectList(db.ProductCategories, "PCatID", "ProductType");
            return View();
        }

        // POST: MaintainanceStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,PCatID,Expirydate,StockPrice")] MaintainanceStock maintainanceStock)
        {
            if (ModelState.IsValid)
            {
                maintainanceStock.dateOfEntry = maintainanceStock.EntryDate();
                db.MaintainanceStocks.Add(maintainanceStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PCatID = new SelectList(db.ProductCategories, "PCatID", "ProductType", maintainanceStock.PCatID);
            return View(maintainanceStock);
        }

        // GET: MaintainanceStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintainanceStock maintainanceStock = db.MaintainanceStocks.Find(id);
            if (maintainanceStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.PCatID = new SelectList(db.ProductCategories, "PCatID", "ProductType", maintainanceStock.PCatID);
            return View(maintainanceStock);
        }

        // POST: MaintainanceStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,PCatID,dateOfEntry,Expirydate,StockPrice")] MaintainanceStock maintainanceStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintainanceStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PCatID = new SelectList(db.ProductCategories, "PCatID", "ProductType", maintainanceStock.PCatID);
            return View(maintainanceStock);
        }

        // GET: MaintainanceStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintainanceStock maintainanceStock = db.MaintainanceStocks.Find(id);
            if (maintainanceStock == null)
            {
                return HttpNotFound();
            }
            return View(maintainanceStock);
        }

        // POST: MaintainanceStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintainanceStock maintainanceStock = db.MaintainanceStocks.Find(id);
            db.MaintainanceStocks.Remove(maintainanceStock);
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
