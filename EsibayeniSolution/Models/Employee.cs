using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EsibayeniSolution.Models
{
    public class Employee 
    {


        [Key]
        [Required(ErrorMessage ="Employee Ideintification key is rquired")]
        public int EmployeeID { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your FirstName ")]
        public string FirstName { get; set; }
        [DisplayName("Surname")]
        [Required(ErrorMessage = "Please Enter your Surname")]
        public string LastName { get; set; }
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [DisplayName("Residential Address")]
        public string Address { get; set; }
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
    }
}