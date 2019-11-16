using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerData.Data.Models
{
   public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public int NumnerOfOrders { get; set; }
        public double Income { get; set; }
    }
}
