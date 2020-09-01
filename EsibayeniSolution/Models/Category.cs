using EsibayeniSolution.Models.CategoryClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [DisplayName("Type")]
        [Required(ErrorMessage = "Category type is required to process")]
        public string CategoryType { get; set; }
        
       
        public virtual ICollection<LivesStock> Items { get; set; }
        public virtual ICollection<CategoryCost> CategoryCost { get; set; }

    }
}