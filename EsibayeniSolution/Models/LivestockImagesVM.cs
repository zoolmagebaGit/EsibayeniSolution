using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class LivestockImagesVM
    {
        [Key]
         [DisplayName("Image Id")]
        public int ImageId { get; set; }
        [DisplayName("Livestock Code")]
        public string LivestockCode { get; set; }
        public DateTime UploadDate { get; set; }
    }
}