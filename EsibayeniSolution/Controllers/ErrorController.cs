using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsibayeniSolution.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bad_Request()
        {
            return View();
        }
        public ActionResult Not_Found()
        {
            return View();
        }
    }
}