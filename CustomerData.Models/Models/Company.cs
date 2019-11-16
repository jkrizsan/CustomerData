using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Data.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
        public int NumberOfContacts { get; set; }
        public int NumberOfOrders { get; set; }
        public double TotalIncome { get; set; }
    }
}
