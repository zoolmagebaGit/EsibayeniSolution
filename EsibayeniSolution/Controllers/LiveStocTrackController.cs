using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EsibayeniSolution.Models.Datapoint;
using EsibayeniSolution.Models;

namespace EsibayeniSolution.Controllers
{
    public class LiveStocTrackController : Controller
    {
       private ApplicationDbContext db = new ApplicationDbContext();
        // GET: LiveStocTrack
        public ActionResult Index(int id)
        {
            LivesStock livesStock = db.LivesStocks.Find(id);
            LiveStockWeight liveStockWeight = new LiveStockWeight();
            List<LiveStockWeight> list = liveStockWeight.ValidWeights(db.LiveStockWeights.ToList(),id);
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();
            for (int i = 0; i<list.Count;i++)
            {
                dataPoints1.Add(new DataPoint(list[i].Date.ToString(),Convert.ToDouble(list[i].Weight)));
            }

            //dataPoints1.Add(new DataPoint("Jan", 72));
            //dataPoints1.Add(new DataPoint("Feb", 67));
            //dataPoints1.Add(new DataPoint("Mar", 55));
            //dataPoints1.Add(new DataPoint("Apr", 42));
            //dataPoints1.Add(new DataPoint("May", 40));
            //dataPoints1.Add(new DataPoint("Jun", 35));
            //dataPoints1.Add(new DataPoint("July", 80));
            //dataPoints1.Add(new DataPoint("Aug", 32));
            //dataPoints1.Add(new DataPoint("Sept", 45));
            //dataPoints1.Add(new DataPoint("Oct", 33));
            //dataPoints1.Add(new DataPoint("Nov", 63));
            //dataPoints1.Add(new DataPoint("Dec", 49));

            //dataPoints2.Add(new DataPoint("Jan", 48));
            //dataPoints2.Add(new DataPoint("Feb", 56));
            //dataPoints2.Add(new DataPoint("Mar", 50));
            //dataPoints2.Add(new DataPoint("Apr", 47));
            //dataPoints2.Add(new DataPoint("May", 65));
            //dataPoints2.Add(new DataPoint("Jun", 69));
            //dataPoints2.Add(new DataPoint("July", 72));
            //dataPoints2.Add(new DataPoint("Aug", 76));
            //dataPoints2.Add(new DataPoint("Sept", 56));
            //dataPoints2.Add(new DataPoint("Oct", 83));
            //dataPoints2.Add(new DataPoint("Nov", 58));
            //dataPoints2.Add(new DataPoint("Dec", 44));

            //dataPoints3.Add(new DataPoint("Jan", 38));
            //dataPoints3.Add(new DataPoint("Feb", 46));
            //dataPoints3.Add(new DataPoint("Mar", 55));
            //dataPoints3.Add(new DataPoint("Apr", 70));
            //dataPoints3.Add(new DataPoint("May", 77));
            //dataPoints3.Add(new DataPoint("Jun", 91));
            //dataPoints3.Add(new DataPoint("July", 76));
            //dataPoints3.Add(new DataPoint("Aug", 63));
            //dataPoints3.Add(new DataPoint("Sept", 46));
            //dataPoints3.Add(new DataPoint("Oct", 43));
            //dataPoints3.Add(new DataPoint("Nov", 66));
            //dataPoints3.Add(new DataPoint("Dec", 87));

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);
            ViewBag.Code = livesStock.Code;

            return View();
        }
    }
}