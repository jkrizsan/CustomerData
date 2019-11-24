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
        public ContactService(CompanyDataDbContext context)
        {
            this.context = context;
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
            var contact = context.Contacts.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            contact.Orders = GetOrdersByContactId(contact.Id).ToList();
            return contact;
        }

        public IEnumerable<Order> GetOrdersByContactId(int Id)
        {
            return context.Orders.Where( o => o.ContactId.Equals(Id) );
        }

        public void SaveContact(Contact contact)
        {
            var oldOldContact = GetContactById(contact.Id);
            oldOldContact.FirstName = contact.FirstName;
            oldOldContact.MiddleName = contact.MiddleName;
            oldOldContact.LastName = contact.LastName;
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
