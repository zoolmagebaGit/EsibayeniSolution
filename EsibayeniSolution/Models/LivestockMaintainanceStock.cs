using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class LivestockMaintainanceStock
    {
        [Key]

        public int LivestockMaintainanceStockID { get; set; }
        [DisplayName("Livestock Type")]
        [Required(ErrorMessage = "Livestock type is required to process")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [DisplayName("Product Type")]
        [Required(ErrorMessage = "Product is required to process")]
        public int ProductID { get; set; }
        public virtual MaintainanceStock MaintainaceStock { get; set; }
        public List<LivestockMaintainanceStock> ValidMaintainanceStock(List<LivestockMaintainanceStock> list, int? categoryID)
        {
            return list.Where(
                c => c.CategoryId == categoryID).ToList();
        }
    }
}