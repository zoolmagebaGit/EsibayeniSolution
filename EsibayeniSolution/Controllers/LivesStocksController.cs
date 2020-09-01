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
    public class LivesStocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string shoppingCartID { get; set; }

        public const string CartSessionKey = "CartId";

        public LivesStocksController() { }

        // GET: LivesStocks
        public ActionResult Index(string searchString)
        {
            var livestock = from l in db.LivesStocks select l;
            if (!string.IsNullOrEmpty(searchString))
            {
                livestock = livestock.Where(a => a.Code.Contains(searchString));
            }

            var livesStocks = db.LivesStocks.Include(l => l.Batch).Include(l => l.Category);
            return View(livesStocks.ToList());
        }

        // GET: LivesStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivesStock livesStock = db.LivesStocks.Find(id);
            if (livesStock == null)
            {
                return HttpNotFound();
            }
            return View(livesStock);
        }

        // GET: LivesStocks/Create
        //[Authorize(Roles = "Stock Controller")]
        public ActionResult AutoCreate(Batch batch)
        {
            //loop this create process as many times as there are livestock in the batch
            for (int i = 0; i < batch.Quantity; i++)
            {
                Category category = db.Categories.Find(batch.CategoryID);
                //use the constructor to create default livestock
                //using correct batch number and correct category
                LivesStock livestock = new LivesStock(batch, category);
                livestock.DateOfBirth = DateTime.Now;
                db.LivesStocks.Add(livestock);
                db.SaveChanges();
                livestock.Code = livestock.GenerateCode(category);
                db.Entry(livestock).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        //[Authorize(Roles = "Stock Controller")]
        public ActionResult Create()
        {
            Batch batch = new Batch();
            List<Batch> batches = db.Batches.ToList<Batch>();
            ViewBag.BatchID = new SelectList(batch.ValidBatch(batches), "BatchID", "BatchCode");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType");
            return View();
        }

        // POST: LivesStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LivestockID,CategoryId,Weight,BatchID,DateOfBirth,Sex,CostPrice")] LivesStock livesStock, HttpPostedFileBase img_upload)
        {
            livesStock.sold = false;
            livesStock.addedtocart = false;
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            livesStock.Picture = data;
            if (ModelState.IsValid)
            {
                Category category = db.Categories.Find(livesStock.CategoryId);


                db.LivesStocks.Add(livesStock);
                if (livesStock.BatchID != null)
                {
                    //Decrement the number of uncreated livestock for that batch
                    db.Batches.Find(livesStock.BatchID).UncreatedQuantity--;
                }
                db.SaveChanges();

                livesStock.Code = livesStock.GenerateCode(category);
                db.Entry(livesStock).State = EntityState.Modified;

                LiveStockWeight liveStockWeight = new LiveStockWeight();
                liveStockWeight.LivestockID = livesStock.LivestockID;
                liveStockWeight.Date = liveStockWeight.EntryDate();
                liveStockWeight.Weight = livesStock.Weight;
                db.LiveStockWeights.Add(liveStockWeight);
                db.SaveChanges();
               // livesStock.Weight = liveStockWeight.Weight;
                CategoryCost categoryCost = new CategoryCost();
                livesStock.SellingPrice = livesStock.calcSellingPrice(categoryCost.ValidategoryCost(db.CategoryCosts.ToList(), livesStock.CategoryId));
                db.Entry(livesStock).State = EntityState.Modified;
                db.SaveChanges();

                if (livesStock.BatchID != null)
                    //check whether there are still any ucreated livestock for that batch
                    if (db.Batches.Find(livesStock.BatchID).UncreatedQuantity > 0)
                    {
                        //if there are, return to the livestock create screen
                        return RedirectToAction("Create", db.Batches.Find(livesStock.BatchID));
                    }
                TempData["Message"] = "<script>alert('Livestock added!');</script>";
                return RedirectToAction("Index");
            }
            Batch batch = new Batch();
            List<Batch> batches = db.Batches.ToList<Batch>();
            ViewBag.BatchID = new SelectList(batch.ValidBatch(batches), "BatchID", "BatchCode");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", livesStock.CategoryId);
            return View(livesStock);
        }
        //[Authorize(Roles = "Stock Controller")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivesStock livesStock = db.LivesStocks.Find(id);
            if (livesStock == null)
            {
                return HttpNotFound();
            }
           // ViewBag.BatchID = new SelectList(db.Batches, "BatchID", "BatchCode", livesStock.BatchID);
            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", livesStock.CategoryId);
            return View(livesStock);

        }

        // POST: LivesStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LivestockID,CategoryId,Weight,BatchID,DateOfBirth,Sex,CostPrice,SellingPrice,Code")] LivesStock livesStock, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            livesStock.Picture = data;
            if (ModelState.IsValid)
            {
                db.Entry(livesStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchID = new SelectList(db.Batches, "BatchID", "BatchCode", livesStock.BatchID);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryType", livesStock.CategoryId);
            return View(livesStock);
        }

        // GET: LivesStocks/Delete/5
        //[Authorize(Roles = "Stock Controller")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivesStock livesStock = db.LivesStocks.Find(id);
            if (livesStock == null)
            {
                return HttpNotFound();
            }
            return View(livesStock);
        }

        // POST: LivesStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LivesStock livesStock = db.LivesStocks.Find(id);
            db.LivesStocks.Remove(livesStock);
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

        public string GetCartID()
        {
            if (System.Web.HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid temp_cart_ID = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[CartSessionKey] = temp_cart_ID.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[CartSessionKey].ToString();
        }

    }
}
