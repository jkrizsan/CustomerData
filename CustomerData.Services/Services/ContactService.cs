using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyData.Data;
using CompanyData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyData.Services.Services
{
    public class ContactService : IContactService
    {
        CompanyDataDbContext context;
        IOrderService orderService;
        public ContactService(CompanyDataDbContext context, IOrderService orderService)
        {
            this.context = context;
            this.orderService = orderService;
        }

        public async Task DeleteOrders(Contact contact)
        {
            context.RemoveRange(contact.Orders);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Contact contact)
        {
            var oldContact = await GetContactById(contact.Id);
            var company = context.Companys.Where(c => c.Id.Equals(oldContact.CompanyId)).SingleOrDefault();
            company.RemoveContact(oldContact);
            await DeleteOrders(oldContact);
            context.Contacts.Remove(oldContact);
            await context.SaveChangesAsync();
        }

        public async Task<Contact> GetContactById(int Id)
        {
            return await orderService.GetContactById(Id);
        }

        public async Task Update(Contact contact)
        {
            var oldContact = await GetContactById(contact.Id);
            oldContact.FirstName = contact.FirstName;
            oldContact.MiddleName = contact.MiddleName;
            oldContact.LastName = contact.LastName;
            await context.SaveChangesAsync();
        }

        public async Task<int> Create(Contact conatct)
        {
            var company = context.Companys.Where(c => c.Id.Equals(conatct.CompanyId)).SingleOrDefault();
            company.Contacts.Add(conatct);
            company.UpdateContactNumber();
            await context.SaveChangesAsync();
            return conatct.Id;
        }
    }
}
