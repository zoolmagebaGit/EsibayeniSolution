using EsibayeniSolution.Models.CategoryClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class LivesStock
    {
        public LivesStock()
        {
        
        }
        //Constructor used for auto-creation of livestock 
        public LivesStock(Batch batch, Category category)
        {
            BatchID = batch.BatchID;
            CategoryId = batch.CategoryID;
            CostPrice = calcUnitCost(batch);
            Code = GenerateCode(category);
            Sex = "Male";
            addedtocart = false;
            sold = false;
            SellingPrice = 0;
            Weight = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Livestock ID")]
        public int LivestockID { get; set; }
       
        [DisplayName("Code")]
        public string Code { get; set; }
        
        [DisplayName("Type")]
        [Required(ErrorMessage = "Category is required to process")]
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
       
        [DisplayName("Batch No")]
        public int? BatchID { get; set; }
        public virtual Batch Batch { get; set; }
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of birth is required to process")]
        public DateTime DateOfBirth { get; set; }
        public byte[] Picture { get; set; }
       public decimal Weight { get; set; }
        [Required(ErrorMessage = "Livestock sex is required to process")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Livestock cost is required to process")]
        [DisplayName("Cost Price")]
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool addedtocart { get; set; }
        public bool sold { get; set; }



        //private bool ValidateDate(string DateOfBirth)
        //{
        //    try
        //    {
        //        // for US, alter to suit if splitting on hyphen, comma, etc.
        //        // Determine if Date String is an actual date
        //        // Date format = MM/DD/YYYY
        //        string[] dateParts = DateOfBirth.Split('/');

        //        // create new date from the parts; if this does not fail
        //        // the method will return true and the date is valid
        //        DateTime testDate = new
        //            DateTime(Convert.ToInt32(dateParts[2]),
        //            Convert.ToInt32(dateParts[0]),
        //            Convert.ToInt32(dateParts[1]));

        //        return true;
        //    }
        //    catch
        //    {
        //        // if a test date cannot be created, the
        //        // method will return false
        //        return false;
        //    }
        //}
        public virtual ICollection<Cart_Item> Cart_Items { get; set; }
        public string GenerateCode(Category category)
        {
            string type = category.CategoryType;
            string code = "L" + type.Substring(0, 1) + type.Substring(type.Length - 1, 1);
            return code.ToUpper()+LivestockID;
        }
        public decimal calcUnitCost(Batch batch)
        {
            return batch.BatchCost / batch.Quantity;
        }
        public decimal calcSellingPrice(CategoryCost categoryCost)
        {
            return (Weight * categoryCost.CostPerKG) + categoryCost.BasicCost;
        }

        public List<LivesStock> LivestocksInCategory(List<LivesStock> list,CategoryCost categoryCost)
        {
            List<LivesStock> refinedList = new List<LivesStock>();
            int count = list.Count;

            while(count > 0)
            {
                if(list[count-1].CategoryId==categoryCost.CategoryId)
                {
                    refinedList.Add(list[count-1]);
                }
                count--;
            }
            return refinedList;
        }
    }
}