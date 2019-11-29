using CompanyData.Data;
using CompanyData.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public class OrderService : IOrderService
    {
        CompanyDataDbContext context;

        public OrderService(CompanyDataDbContext context)
        {
            this.context = context;
        }
        public async Task Create(Order order)
        {
            var contact = context.Contacts.Where(c => c.Id.Equals(order.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.AddOrder(order);
            await context.SaveChangesAsync();
            company.AddOrder(order);
            await context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int Id)
        {
            return context.Orders.Where(o => o.Id.Equals(Id)).SingleOrDefault();
        }

        public async Task<Contact> GetContactById(int Id)
        {
            var contact = context.Contacts.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            contact.Orders = (await GetOrdersByContactId(contact.Id)).ToList();
            return contact;
        }

        public async Task<Company> GetCompanyByContactId(int Id)
        {
            var company = context.Companys.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            company.Contacts = context.Contacts.Where(c => c.CompanyId.Equals(company.Id)).ToList();
            return company;
        }

        public async Task Delete(Order order)
        {
            var oldOrder = context.Orders.Where(o => o.Id.Equals(order.Id)).SingleOrDefault();
            var contact = context.Contacts.Where(c => c.Id.Equals(oldOrder.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.DeleteOrder(oldOrder);
            company.DeleteOrder(oldOrder);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByContactId(int Id)
        {
            return context.Orders.Where(o => o.ContactId.Equals(Id));
        }

        public async Task Update(Order order)
        {
            var oldOrder = await GetOrderById(order.Id);
            var contact = await GetContactById(oldOrder.ContactId);
            var company = await GetCompanyByContactId(contact.CompanyId);
            contact.UpdateByOrder(oldOrder, order);
            await context.SaveChangesAsync();
            company.UpdateByOrder(order);
            await context.SaveChangesAsync();
        }
    }
}