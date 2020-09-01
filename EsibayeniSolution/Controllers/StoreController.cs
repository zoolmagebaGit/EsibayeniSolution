﻿using System.Linq;
using System.Web.Mvc;
using EsibayeniSolution.Models;

namespace EsibayeniSolution.Controllers
{
    public class StoreController : Controller
    {
           ApplicationDbContext storeDB = new ApplicationDbContext();
        //
        // GET: /Store/

        public ActionResult MyCat()
        {
            var catagories = storeDB.Categories.ToList();
            return PartialView("MyCat",catagories);
            //return View(catagories);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string catagorie)
        {
            // Retrieve Genre and its Associated Items from database
            var catagorieModel = storeDB.Categories.Include("Items")
                .SingleOrDefault(g => g.CategoryType == catagorie);

            return View(catagorieModel);
        }
        //public ActionResult BrowseGenger(string Gender)
        //{
        //    // Retrieve Genre and its Associated Items from database
            

        //    return View(storeDB.Items.ToList().Single(X=>X.Gender == Gender));
        //}

        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var item = storeDB.LivesStocks.Find(id);

            return View(item);
        }

        //
        // GET: /Store/GenreMenu
        //[ChildActionOnly]
        public ActionResult CatagorieMenu()
        {
            var catagories = storeDB.Categories.ToList();

            return PartialView(catagories);
        }
    }
}