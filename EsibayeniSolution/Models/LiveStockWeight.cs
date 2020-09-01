using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace EsibayeniSolution.Models
{
    public class LiveStockWeight
    {
        [Key]
        [DisplayName("LiveStock ID")]
        [Required]
        public int LiveStockWeightID { get; set; }
        [DisplayName("Livestock Code")]
        [Required(ErrorMessage = "Livestock is required to process")]
        public int LivestockID { get; set; }
        public virtual LivesStock LivesStock { get; set; }
        [DisplayName("LiveStock Weight")]
        [Required(ErrorMessage = "Weight is required to process")]
        public decimal Weight { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public DateTime EntryDate()
        {
            return DateTime.Now;
        }
        public List<LiveStockWeight> ValidWeights(List<LiveStockWeight> list, int? id)
        {
            return list.Where(
                c => c.LivestockID == id).ToList();
        }



    }

}