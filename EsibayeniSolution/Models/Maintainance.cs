using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class Maintainance
    {
        [Key]
        public int MProcID { get; set; }
        [DisplayName("Groomer")]
        public string User { get; set; }
       
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [Required]
        [DisplayName("Livestock Code")]
        public int LivestockID { get; set; }
        public virtual LivesStock LivesStock { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public int ProductId { get; set; } 
        public virtual MaintainanceStock MaitainanceStock { get; set; }
     
        [DisplayName("Product Category")]
        public string ProductCategory { get; set; }
        [Required]
        [DisplayName("Maintainance Process")]
        public int MainPId { get; set; }
        public virtual MaintainanceProcess MaintainanceProcess { get; set; }
      
        public DateTime DateTimeNow()
        {
            return DateTime.Now;
        }


    }
}