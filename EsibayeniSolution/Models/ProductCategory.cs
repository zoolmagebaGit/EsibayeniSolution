using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsibayeniSolution.Models
{
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Product Category ID")]
        public int PCatID { get; set; }     
        [DisplayName("Product Type")]
        [Required(ErrorMessage =("Enter Product Type to process"))]
        public string ProductType { get; set; }
    }
}