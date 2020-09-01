using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class LivestockImage
    {
        [Key]
        public int ImageId { get; set; }
        public int LivestockID { get; set; }
        public byte[] Image { get; set; }
        public System.DateTime UploadtDate { get; set; }

        public byte[] setImage(HttpPostedFileBase image1)
        {
            return new byte[image1.ContentLength];
        }  

        public string FileName(HttpPostedFileBase image1)
        {
            image1.InputStream.Read(Image, 0, image1.ContentLength);
            var filename = Path.GetFileName(image1.FileName);
            return filename;
        }
    }
}