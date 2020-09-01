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
    //[Authorize(Roles = "Stock Controller")]
    public class BatchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Batches
        public ActionResult Index()
        {
            var batches = db.Batches.Include(b => b.Category).Include(b => b.Supplier);
            return View(batches.ToList());
        }

        // GET: Batches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // GET: Batches/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryType");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "FirstName");
            return View();
        }

        // POST: Batches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchID,SupplierID,CategoryID,Quantity,BatchCost,AutoCreate")] Batch batch)
        {
            if (ModelState.IsValid)
            {

                batch.Date = batch.DateNow();
                db.Batches.Add(batch);
                db.SaveChanges();
                batch.AutoBatch();
                if (batch.AutoCreate)
                {
                    batch.UncreatedQuantity = 0;
                }
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                if (batch.AutoCreate)
                {
                    return RedirectToAction("AutoCreate", "LivesStocks", batch);
                }
                   else
                {
                  return RedirectToAction("Create", "LivesStocks", batch);
                }
                
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryType", batch.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "FirstName", batch.SupplierID);
            return View(batch);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,BatchNo,SupplierID,CategoryID,Quantity,Date,BatchCost")] Batch batch)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Batches.Add(batch);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryType", batch.CategoryID);
        //    ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "FirstName", batch.SupplierID);
        //    return View(batch);
        //}

        // GET: Batches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryType", batch.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "FirstName", batch.SupplierID);
            return View(batch);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BatchCode,SupplierID,CategoryID,Quantity,Date,BatchCost")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryType", batch.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "FirstName", batch.SupplierID);
            return View(batch);
        }

        // GET: Batches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batch batch = db.Batches.Find(id);
            db.Batches.Remove(batch);
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
