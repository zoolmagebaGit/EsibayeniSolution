
using EsibayeniSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class AnalyticsViewModel
    {
        public List<OrderDateGroup> OrderData { get; set; }

        public List<OrderDateGroup> OrderDataForToday { get; set; }
    }
}