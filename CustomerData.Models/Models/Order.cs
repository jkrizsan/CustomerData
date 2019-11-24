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
        public DateTime OrderDate { get; set; }
        public double OrderPrice { get; set; }
        public int ContactId { get; set; }
    }
}
