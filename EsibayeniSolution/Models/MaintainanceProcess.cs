using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class MaintainanceProcess
    {
        [Key]
        [DisplayName("Maintainance Process ID")]
        public int MainPId { get; set; }
        [DisplayName("Maintainance Process Name")]
        [Required(ErrorMessage = "Maintainance name is required to process")]
        public string MainName { get; set; }

    }
}