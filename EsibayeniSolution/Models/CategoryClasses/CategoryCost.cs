using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models.CategoryClasses
{
    public class CategoryCost
    {
     
            [Key]
            public int C_ID { get; set; }
            public int CategoryId { get; set; }
            public virtual Category Category { get; set; }
            [Required(ErrorMessage = "Basic cost required")]
            [DisplayName("Basic Cost")]
            public decimal BasicCost { get; set; }
            [Required(ErrorMessage = "cost Per KG")]
            [DisplayName("Cost Per KG")]
            public decimal CostPerKG { get; set; }
        public CategoryCost ValidategoryCost(List<CategoryCost> list, int? categoryID)
        {
            int count = list.Count;
            for(int i = 0;i<count;i++)
            {
                if(list[i].CategoryId==categoryID)
                {
                    return list[i];
                }
            }
            return null;
        }
    }
}