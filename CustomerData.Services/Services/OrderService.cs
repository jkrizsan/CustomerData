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
        public void Create(Order order)
        {
            var contact = context.Contacts.Where(c => c.Id.Equals(order.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.AddOrder(order);
            context.SaveChanges();
            company.AddOrder(order);
            context.SaveChanges();
        }

        public Order GetOrderById(int Id)
        {
            return context.Orders.Where(o => o.Id.Equals(Id)).SingleOrDefault();
        }

        public Contact GetContactById(int Id)
        {
            var contact = context.Contacts.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            contact.Orders = GetOrdersByContactId(contact.Id).ToList();
            return contact;
        }

        public Company GetCompanyByContactId(int Id)
        {
            var company = context.Companys.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            company.Contacts = context.Contacts.Where(c => c.CompanyId.Equals(company.Id)).ToList();
            return company;
        }

        public void Delete(Order order)
        {
            var oldOrder = context.Orders.Where(o => o.Id.Equals(order.Id)).SingleOrDefault();
            var contact = context.Contacts.Where(c => c.Id.Equals(oldOrder.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.DeleteOrder(oldOrder);
            company.DeleteOrder(oldOrder);
            context.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByContactId(int Id)
        {
            return context.Orders.Where(o => o.ContactId.Equals(Id));
        }

        public void Update(Order order)
        {
            var oldOrder = GetOrderById(order.Id);
            var contact = GetContactById(oldOrder.ContactId);
            var company = GetCompanyByContactId(contact.CompanyId);
            contact.UpdateByOrder(oldOrder, order);
            context.SaveChanges();
            company.UpdateByOrder(order);
            context.SaveChanges();
        }
    }
}