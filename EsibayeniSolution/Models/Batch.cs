using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class Batch
    {
        public void AutoBatch()
        {
            UncreatedQuantity = Quantity;
            BatchCode = GenerateBatchCode();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchID { get; set; }
       // [Required(ErrorMessage = "Batch number is required to process")]
        [DisplayName("Batch Code")]

        public string BatchCode { get; set; }
        [Required(ErrorMessage = "Supplier is required to process")]
        [DisplayName("Supplier Name")]
        public int? SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }
        [Required(ErrorMessage = "Category is required to process")]
        [DisplayName("Category Type")]
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
        [Required(ErrorMessage = "Quantity number is required to process")]
 
        public int Quantity { get; set; }
        public int UncreatedQuantity { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [DisplayName("Batch Cost")]
        [Required(ErrorMessage = "Batch cost is required to process")]
        public decimal BatchCost { get; set; }
        [DisplayName("Auto-Create Livestock")]
        public bool AutoCreate { get; set; }


        public DateTime DateNow()
        {
            return DateTime.Now;
        }
        public string GenerateBatchCode()
        {
            if(BatchID<10)
            {
                return "B000" + BatchID;
            }
            else if(BatchID < 100)
            {
                return "B00" + BatchID;
            }
            else if (BatchID < 1000)
            {
                return "B0" + BatchID;
            }
            else 
            {
                return "B" + BatchID;
            }

        }

        public List<Batch> ValidBatch(List<Batch> list)
        {
            List<Batch> refined = new List<Batch>();
            int count = list.Count;
            while(count > 0)
            {
                if(list[count-1].UncreatedQuantity>0)
                {
                    refined.Add(list[count-1]);
                }
                count--;
            }
            return refined;
        }


    }
}