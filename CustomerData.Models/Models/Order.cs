using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerData.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderPrice { get; set; }
    }
}
