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
        [Display(Name = ("Number of Contacts"))]
        public int NumberOfContacts { get; set; }
        [Display(Name = ("Number of Orders"))]
        public int NumberOfOrders { get; set; }
        [Display(Name = ("Total Income"))]
        public double TotalIncome { get; set; }

        public void RemoveContact(Contact contact)
        {
            foreach (var item in contact.Orders)
            {
                TotalIncome -= item.OrderPrice;
                NumberOfOrders--;
            }
            NumberOfContacts--;
        }
        public void UpdateContactNumber()
        {
            NumberOfContacts++;
        }

        public void AddOrder(Order order)
        {
            NumberOfOrders++;
            TotalIncome += order.OrderPrice;
        }

        public void UpdateByOrder(Order order)
        {
            TotalIncome = 0;
            foreach (var contact in Contacts)
            {
                TotalIncome += contact.Income;
           }
        }
        public void DeleteOrder(Order order)
        {
            TotalIncome -= order.OrderPrice;
            NumberOfOrders--;
        }
    }
}
