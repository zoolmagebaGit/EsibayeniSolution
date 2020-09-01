using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsibayeniSolution.Models;
using System.IO;

namespace EsibayeniSolution.Controllers
{
    public class LivestockImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Renders view with the selected livestock's images
         public ActionResult ViewLivestockImage(int id)
        {
            //joins livestock with the respective images that have that livestock's ID
            var listWthEmpty = from l in db.LivesStocks
                               where l.LivestockID == id
                               join li in db.LivestockImages
                               on l.LivestockID equals li.LivestockID
                               into ThisList
                               from li in ThisList.DefaultIfEmpty()
                               select new LivestockImagesVM
                               {
                                   ImageId = li.ImageId,
                                   LivestockCode = l.Code,
                                   UploadDate = li.UploadtDate
                               };
                return View(listWthEmpty);
            

        }

        //Downloads and displays the image with the selected ID
        public ActionResult Download(int id)
        {
            var file = db.LivestockImages.Find(id);
            byte[] contents = file.Image;
            return File(contents, "image/jpg");
        }

        // GET: LivestockImages/Create
        [Authorize(Roles = "Stock Controller")]
        public ActionResult Create(LivesStock livestock)
        {
            LivestockImage livestockImage = new LivestockImage();
            //links the image to be uploaded, with the selected livestock
            livestockImage.LivestockID = livestock.LivestockID;
            //takes the livestockImage with the livestockID to the image upload screen
            return View(livestockImage);
        }

        // POST: LivestockImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,LivestockID,Image,UploadtDate")] LivestockImage livestockImage, HttpPostedFileBase image1)
        {
            if (ModelState.IsValid)
            {
                //if an image is selected
                if (image1 != null && image1.ContentLength > 0)
                {
                    livestockImage.UploadtDate = DateTime.Now;
                    livestockImage.Image = livestockImage.setImage(image1);

                    var path = Path.Combine(Server.MapPath("~/App_Data"), livestockImage.FileName(image1));
                    //byte[] data = null;
                    //data = new byte[image1.ContentLength];
                    //image1.InputStream.Read(data, 0, image1.ContentLength);
                    //livestockImage.Image = data;
                    image1.SaveAs(path);
                }
                else
                {
                    TempData["Message"] = "<script>alert('No Image was selected!');</script>";
                    return RedirectToAction("Index", "LivesStocks");
                }
                db.LivestockImages.Add(livestockImage);
                db.SaveChanges();
                TempData["Message"] = "<script>alert('Livestock image added!');</script>";
                return RedirectToAction("Index", "LivesStocks");
            }

            return View(livestockImage);
        }
        // GET: LivestockImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivestockImage livestockImage = db.LivestockImages.Find(id);
            if (livestockImage == null)
            {
                return HttpNotFound();
            }
            return View(livestockImage);
        }

        // POST: LivestockImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LivestockImage livestockImage = db.LivestockImages.Find(id);
            db.LivestockImages.Remove(livestockImage);
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
