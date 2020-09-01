using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsibayeniSolution.Models;
using EsibayeniSolution.Models.CategoryClasses;

namespace EsibayeniSolution.Controllers
{
    public class LiveStockWeightsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LiveStockWeights
        public ActionResult Index()
        {
            var liveStockWeights = db.LiveStockWeights.Include(l => l.LivesStock);
            return View(liveStockWeights.ToList());
        }

        public ActionResult LivestockWeight(int id)
        {
            LiveStockWeight livestockWeight = new LiveStockWeight();
            return View(livestockWeight.ValidWeights(db.LiveStockWeights.ToList(),id));
        }

        // GET: LiveStockWeights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiveStockWeight liveStockWeight = db.LiveStockWeights.Find(id);
            if (liveStockWeight == null)
            {
                return HttpNotFound();
            }
            return View(liveStockWeight);
        }

        // GET: LiveStockWeights/Create
        public ActionResult Create(int id)
        {
            LiveStockWeight liveStockWeight = new LiveStockWeight();
            liveStockWeight.LivestockID = id;
           // ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code");
            return View(liveStockWeight);
        }

        // POST: LiveStockWeights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LiveStockWeightID,LivestockID,Weight,Date")] LiveStockWeight liveStockWeight)
        {
            if (ModelState.IsValid)
            {

                liveStockWeight.Date = liveStockWeight.EntryDate();
                db.LiveStockWeights.Add(liveStockWeight);
                db.SaveChanges();
                LivesStock livesStock = db.LivesStocks.Find(liveStockWeight.LivestockID);
                livesStock.Weight = liveStockWeight.Weight;
                CategoryCost categoryCost = new CategoryCost();
                livesStock.SellingPrice = livesStock.calcSellingPrice(categoryCost.ValidategoryCost(db.CategoryCosts.ToList(), livesStock.CategoryId));
                db.Entry(livesStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code", liveStockWeight.LivestockID);
            return View(liveStockWeight);
        }

        // GET: LiveStockWeights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiveStockWeight liveStockWeight = db.LiveStockWeights.Find(id);
            if (liveStockWeight == null)
            {
                return HttpNotFound();
            }
            ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code", liveStockWeight.LivestockID);
            return View(liveStockWeight);
        }

        // POST: LiveStockWeights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LiveStockWeightID,LivestockID,Weight,Date")] LiveStockWeight liveStockWeight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liveStockWeight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code", liveStockWeight.LivestockID);
            return View(liveStockWeight);
        }

        // GET: LiveStockWeights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiveStockWeight liveStockWeight = db.LiveStockWeights.Find(id);
            if (liveStockWeight == null)
            {
                return HttpNotFound();
            }
            return View(liveStockWeight);
        }

        // POST: LiveStockWeights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LiveStockWeight liveStockWeight = db.LiveStockWeights.Find(id);
            db.LiveStockWeights.Remove(liveStockWeight);
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
