using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public interface IContactService
    {
        Contact GetContactById(int Id);
        void Update(Contact contact);
        void Delete(int id);
        void DeleteOrders(Contact contact);
        int Create(Contact conatct);
    }
}
