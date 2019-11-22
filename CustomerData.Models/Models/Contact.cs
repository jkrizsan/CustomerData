using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Data.Models
{
   public class Contact
    {
        public Contact()
        {
            Orders = new List<Order>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }
        public int NumnerOfOrders { get; set; }
        public double Income { get; set; }
        public int CompanyId { get; set; }
    }
}
