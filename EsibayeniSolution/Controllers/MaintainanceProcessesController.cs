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
    public class MaintainanceProcessesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaintainanceProcesses
        public ActionResult Index()
        {
            return View(db.MaintainanceProcesses.ToList());
        }

        // GET: MaintainanceProcesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintainanceProcess maintainanceProcess = db.MaintainanceProcesses.Find(id);
            if (maintainanceProcess == null)
            {
                return HttpNotFound();
            }
            return View(maintainanceProcess);
        }

        // GET: MaintainanceProcesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintainanceProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MainPId,MainName")] MaintainanceProcess maintainanceProcess)
        {
            if (ModelState.IsValid)
            {
                db.MaintainanceProcesses.Add(maintainanceProcess);
                db.SaveChanges();
                TempData["Message"] = "<script>alert('Maintainance process captured!');</script>";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "<script>alert('Check maintainance details and try again');</script>";
            return View(maintainanceProcess);
        }

        // GET: MaintainanceProcesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintainanceProcess maintainanceProcess = db.MaintainanceProcesses.Find(id);
            if (maintainanceProcess == null)
            {
                return HttpNotFound();
            }
            return View(maintainanceProcess);
        }

        // POST: MaintainanceProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MainPId,MainName")] MaintainanceProcess maintainanceProcess)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintainanceProcess).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintainanceProcess);
        }

        // GET: MaintainanceProcesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintainanceProcess maintainanceProcess = db.MaintainanceProcesses.Find(id);
            if (maintainanceProcess == null)
            {
                return HttpNotFound();
            }
            return View(maintainanceProcess);
        }

        // POST: MaintainanceProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintainanceProcess maintainanceProcess = db.MaintainanceProcesses.Find(id);
            db.MaintainanceProcesses.Remove(maintainanceProcess);
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
