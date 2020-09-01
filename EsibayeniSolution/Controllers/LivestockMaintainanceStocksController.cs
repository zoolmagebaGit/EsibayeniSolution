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
    public class LivestockMaintainanceStocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LivestockMaintainanceStocks
        public ActionResult Index()
        {
            var livestockMaintainanceStocks = db.LivestockMaintainanceStocks.Include(l => l.Category).Include(l => l.MaintainaceStock);
            return View(livestockMaintainanceStocks.ToList());
        }

        // GET: LivestockMaintainanceStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivestockMaintainanceStock livestockMaintainanceStock = db.LivestockMaintainanceStocks.Find(id);
            if (livestockMaintainanceStock == null)
            {
                return HttpNotFound();
            }
            return View(livestockMaintainanceStock);
        }

        // GET: LivestockMaintainanceStocks/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType");
            ViewBag.ProductID = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName");
            return View();
        }

        // POST: LivestockMaintainanceStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LivestockMaintainanceStockID,CategoryId,ProductID")] LivestockMaintainanceStock livestockMaintainanceStock)
        {
            if (ModelState.IsValid)
            {
                db.LivestockMaintainanceStocks.Add(livestockMaintainanceStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", livestockMaintainanceStock.CategoryId);
            ViewBag.ProductID = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName", livestockMaintainanceStock.ProductID);
            return View(livestockMaintainanceStock);
        }

        // GET: LivestockMaintainanceStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivestockMaintainanceStock livestockMaintainanceStock = db.LivestockMaintainanceStocks.Find(id);
            if (livestockMaintainanceStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", livestockMaintainanceStock.CategoryId);
            ViewBag.ProductID = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName", livestockMaintainanceStock.ProductID);
            return View(livestockMaintainanceStock);
        }

        // POST: LivestockMaintainanceStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LivestockMaintainanceStockID,CategoryId,ProductID")] LivestockMaintainanceStock livestockMaintainanceStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livestockMaintainanceStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", livestockMaintainanceStock.CategoryId);
            ViewBag.ProductID = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName", livestockMaintainanceStock.ProductID);
            return View(livestockMaintainanceStock);
        }

        // GET: LivestockMaintainanceStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivestockMaintainanceStock livestockMaintainanceStock = db.LivestockMaintainanceStocks.Find(id);
            if (livestockMaintainanceStock == null)
            {
                return HttpNotFound();
            }
            return View(livestockMaintainanceStock);
        }

        // POST: LivestockMaintainanceStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LivestockMaintainanceStock livestockMaintainanceStock = db.LivestockMaintainanceStocks.Find(id);
            db.LivestockMaintainanceStocks.Remove(livestockMaintainanceStock);
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
