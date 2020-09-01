using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsibayeniSolution.Models;
using Microsoft.AspNet.Identity;

namespace EsibayeniSolution.Controllers
{
    public class Maintainances1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Maintainances1
        public ActionResult Index()
        {
            var maintainances = db.Maintainances.Include(m => m.LivesStock).Include(m => m.MaintainanceProcess).Include(m => m.MaitainanceStock);
            return View(maintainances.ToList());
        }

        // GET: Maintainances1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintainance maintainance = db.Maintainances.Find(id);
            if (maintainance == null)
            {
                return HttpNotFound();
            }
            return View(maintainance);
        }

        // GET: Maintainances1/Create
        public ActionResult Create(int id)
        {


            var livestockmaintainanceStock = new LivestockMaintainanceStock();
            var maintainanceStock = new MaintainanceStock();
            var maintainance = new Maintainance();
            var livestock = db.LivesStocks.Find(id);
            maintainance.User = User.Identity.GetUserName();
            maintainance.date = maintainance.DateTimeNow();
            maintainance.LivesStock = livestock;
            maintainance.LivestockID = livestock.LivestockID;

            List<LivestockMaintainanceStock> refinedMS = new List<LivestockMaintainanceStock>();
            refinedMS = livestockmaintainanceStock.ValidMaintainanceStock(db.LivestockMaintainanceStocks.ToList(), livestock.CategoryId);

            ViewBag.MainPId = new SelectList(db.MaintainanceProcesses, "MainPId", "MainName");
            ViewBag.ProductId = new SelectList(maintainanceStock.ValidMaintainanceStock(refinedMS, db.MaintainanceStocks.ToList()), "ProductID", "ProductName");
            return View(maintainance);
        }

        // POST: Maintainances1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MProcID,User,date,LivestockID,ProductId,ProductCategory,MainPId")] Maintainance maintainance)
        {
            if (ModelState.IsValid)
            {
                db.Maintainances.Add(maintainance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code", maintainance.LivestockID);
            ViewBag.MainPId = new SelectList(db.MaintainanceProcesses, "MainPId", "MainName", maintainance.MainPId);
            ViewBag.ProductId = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName", maintainance.ProductId);
            return View(maintainance);
        }

        // GET: Maintainances1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintainance maintainance = db.Maintainances.Find(id);
            if (maintainance == null)
            {
                return HttpNotFound();
            }
            ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code", maintainance.LivestockID);
            ViewBag.MainPId = new SelectList(db.MaintainanceProcesses, "MainPId", "MainName", maintainance.MainPId);
            ViewBag.ProductId = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName", maintainance.ProductId);
            return View(maintainance);
        }

        // POST: Maintainances1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MProcID,User,date,LivestockID,ProductId,ProductCategory,MainPId")] Maintainance maintainance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintainance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LivestockID = new SelectList(db.LivesStocks, "LivestockID", "Code", maintainance.LivestockID);
            ViewBag.MainPId = new SelectList(db.MaintainanceProcesses, "MainPId", "MainName", maintainance.MainPId);
            ViewBag.ProductId = new SelectList(db.MaintainanceStocks, "ProductID", "ProductName", maintainance.ProductId);
            return View(maintainance);
        }

        // GET: Maintainances1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maintainance maintainance = db.Maintainances.Find(id);
            if (maintainance == null)
            {
                return HttpNotFound();
            }
            return View(maintainance);
        }

        // POST: Maintainances1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maintainance maintainance = db.Maintainances.Find(id);
            db.Maintainances.Remove(maintainance);
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
