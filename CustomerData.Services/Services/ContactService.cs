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
            oldOldContact.Income = contact.Income;
            oldOldContact.NumnerOfOrders = contact.NumnerOfOrders;
            context.SaveChanges();
        }
    }
}
