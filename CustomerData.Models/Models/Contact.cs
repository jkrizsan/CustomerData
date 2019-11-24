using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }
        [Display(Name = "Number of Orders")]
        public int NumnerOfOrders { get; set; }
        public double Income { get; set; }
        public int CompanyId { get; set; }

        public bool UpdateByOrder(Order order)
        {
            var oldOrder = Orders.Where(o => o.Id.Equals(order.Id)).SingleOrDefault();
            if(oldOrder == null)
            {
                Orders.Add(order);
                NumnerOfOrders++;
                Income += order.OrderPrice;
                return true;
            }
            else
            {
                oldOrder.OrderDate = order.OrderDate;
                oldOrder.OrderPrice = order.OrderPrice;
                Income = 0;
                foreach (var o in Orders)
                {
                    Income += o.OrderPrice;
                }
                return false;
            }
        }
        public void DeleteOrder (Order order)
        {
            Income -= order.OrderPrice;
            NumnerOfOrders--;
            Orders.Remove(order);
        }
    }
}
