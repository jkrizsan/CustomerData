using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyData.Data.Models
{
    public class Company
    {
        public Company()
        {
            Contacts = new List<Contact>();
        }
        public int Id { get; set; }
        [Display(Name = ("Name"))]
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
        [Display(Name=("Number of Contacts"))]
        public int NumberOfContacts { get; set; }
        [Display(Name = ("Number of Orders"))]
        public int NumberOfOrders { get; set; }
        [Display(Name = ("Total Income"))]
        public double TotalIncome { get; set; }
    }
}
