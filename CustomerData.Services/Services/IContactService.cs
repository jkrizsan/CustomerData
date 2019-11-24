using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public interface IContactService
    {
        Contact GetContactById(int Id);
        IEnumerable<Order> GetOrdersByContactId(int Id);
        void SaveContact(Contact contact);
        void DeleteContact(int id);
        void DeleteOrders(Contact contact);
        int Add(Contact conatct);
    }
}
