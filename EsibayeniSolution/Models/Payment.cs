﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsibayeniSolution.Models
{
    public class Payment
    {

        [Key]
        [Display(Name = "ID")]
        public int payment_ID { get; set; }
        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date")]
        public System.DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public decimal AmountPaid { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string PaymentFor { get; set; }
        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        public virtual Customer Customer { get; set; }
    }
}