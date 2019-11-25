using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyData.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Date of Order")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Price of Order")]
        public double OrderPrice { get; set; }
        [Display(Name = "Contact Id")]
        public int ContactId { get; set; }
    }
}
