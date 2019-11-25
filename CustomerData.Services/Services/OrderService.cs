using CompanyData.Data;
using CompanyData.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace CompanyData.Services.Services
{
    public class OrderService : IOrderService
    {
        CompanyDataDbContext context;
        public OrderService(CompanyDataDbContext context)
        {
            this.context = context;
        }
        public void Add(Order order)
        {
            var contact = context.Contacts.Where(c => c.Id.Equals(order.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            var isNew = contact.UpdateByOrder(order);
            context.SaveChanges();
            company.UpdateByOrder(isNew, order);
            context.SaveChanges();
        }

        public Order GetOrderById(int Id)
        {
            return context.Orders.Where(o => o.Id.Equals(Id)).SingleOrDefault();
        }
        public void DeleteOrder(int Id)
        {
            var order = context.Orders.Where(o => o.Id.Equals(Id)).SingleOrDefault();
            var contact = context.Contacts.Where(c => c.Id.Equals(order.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.DeleteOrder(order);
            company.DeleteOrder(order);
            context.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByContactId(int Id)
        {
            return context.Orders.Where(o => o.ContactId.Equals(Id));
        }

        public void SaveOrder(Order order)
        {
            var oldOrder = GetOrderById(order.Id);
            var contact = context.Contacts.Where(c => c.Id.Equals(oldOrder.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            var isNew = contact.UpdateByOrder(order);
            context.SaveChanges();
            company.UpdateByOrder(isNew, order);
            context.SaveChanges();
        }
    }
}