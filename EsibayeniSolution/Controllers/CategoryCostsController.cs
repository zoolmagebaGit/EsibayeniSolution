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
    public class CategoryCostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoryCosts
        public ActionResult Index()
        {
            var categoryCosts = db.CategoryCosts.Include(c => c.Category);
            return View(categoryCosts.ToList());
        }

        // GET: CategoryCosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryCost categoryCost = db.CategoryCosts.Find(id);
            if (categoryCost == null)
            {
                return HttpNotFound();
            }
            return View(categoryCost);
        }

        // GET: CategoryCosts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType");
            return View();
        }

        // POST: CategoryCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_ID,CategoryId,BasicCost,CostPerKG")] CategoryCost categoryCost)
        {
            if (ModelState.IsValid)
            {
                db.CategoryCosts.Add(categoryCost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", categoryCost.CategoryId);
            return View(categoryCost);
        }

        // GET: CategoryCosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryCost categoryCost = db.CategoryCosts.Find(id);
            if (categoryCost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", categoryCost.CategoryId);
            return View(categoryCost);
        }

        // POST: CategoryCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_ID,CategoryId,BasicCost,CostPerKG")] CategoryCost categoryCost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryCost).State = EntityState.Modified;
                db.SaveChanges();

                LivesStock livesStock = new LivesStock();
                List<LivesStock> listLivestocks = livesStock.LivestocksInCategory(db.LivesStocks.ToList(), categoryCost);
                for(int i = 0;i<listLivestocks.Count;i++)
                {
                    livesStock = listLivestocks[i];
                    livesStock.SellingPrice = livesStock.calcSellingPrice(categoryCost);
                    db.Entry(livesStock).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", categoryCost.CategoryId);
            return View(categoryCost);
        }

        // GET: CategoryCosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryCost categoryCost = db.CategoryCosts.Find(id);
            if (categoryCost == null)
            {
                return HttpNotFound();
            }
            return View(categoryCost);
        }

        // POST: CategoryCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryCost categoryCost = db.CategoryCosts.Find(id);
            db.CategoryCosts.Remove(categoryCost);
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
