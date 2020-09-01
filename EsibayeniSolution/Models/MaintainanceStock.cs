using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsibayeniSolution.Models
{
    public class MaintainanceStock
    {
        [Key]
        [DisplayName("Product ID")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product category is required to process")]
        public int PCatID { get; set; }
        public virtual ProductCategory ProductCategory { get; set;  }

        [DisplayName("ProductName")]
        [Required]
        public string ProductName { get; set; }    
        public DateTime dateOfEntry { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Expirydate { get; set; }
        [DisplayName("Cost Price")]
        [Required(ErrorMessage = "Stock price is required to process")]
        public double StockPrice { get; set; }

        public DateTime EntryDate()
        {
            return DateTime.Now;
        }
        public List<MaintainanceStock> ValidMaintainanceStock(List<LivestockMaintainanceStock> lmslist, List<MaintainanceStock> mslist)
        {
            List<MaintainanceStock> refined = new List<MaintainanceStock>();
            int lmscount = lmslist.Count;
            int countms;
            while (lmscount > 0)
            {
                countms = mslist.Count;
                while (countms > 0)
                {
                    if (lmslist[lmscount - 1].ProductID == mslist[countms - 1].ProductID)
                    {
                        refined.Add(mslist[countms - 1]);
                    }
                    countms--;
                }

                lmscount--;
            }
            return refined;
        }


    }
}