using EsibayeniSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;




namespace EsibayeniSolution.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //if (User.IsInRole("Admin"))
            //{
            //    return View("AdminIndex");
            //}
            //else if (User.IsInRole("Customer"))
            //{
            //    return RedirectToAction("Index", "Shopping");
               
            //}
            //else
            //{
                
            //}
            return View();
        }
    
            public ActionResult AdminIndex()
            {
                return View();
            }
            public ActionResult About()
            {
            ViewBag.Message = "Your application description page.";

            return View();
             }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Customer(string searchString)
        {
            return View(db.LivesStocks.ToList());
        }
    }
}