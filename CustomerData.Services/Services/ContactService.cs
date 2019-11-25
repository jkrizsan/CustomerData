using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void DeleteOrders(Contact contact)
        {
            context.RemoveRange(contact.Orders);
            context.SaveChanges();
        }

        public void DeleteContact(int Id)
        {
            var contact = GetContactById(Id);
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            company.RemoveContact(contact);
            DeleteOrders(contact);
            context.Contacts.Remove(contact);
            context.SaveChanges();
        }

        public Contact GetContactById(int Id)
        {
            return orderService.GetContactById(Id);
        }

        public void SaveContact(Contact contact)
        {
            var oldContact = GetContactById(contact.Id);
            oldContact.FirstName = contact.FirstName;
            oldContact.MiddleName = contact.MiddleName;
            oldContact.LastName = contact.LastName;
            context.SaveChanges();
        }

        public int Add(Contact conatct)
        {
            var company = context.Companys.Where(c => c.Id.Equals(conatct.CompanyId)).SingleOrDefault();
            company.Contacts.Add(conatct);
            company.UpdateContactNumber();
            context.SaveChanges();
            return conatct.Id;
        }
    }
}
