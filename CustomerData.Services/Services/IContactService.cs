﻿using CompanyData.Data.Models;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IContactService
    {
        Task<Contact> GetContactById(int Id);
        Task Update(Contact contact);
        Task Delete(Contact contact);
        Task DeleteOrders(Contact contact);
        Task<int> Create(Contact conatct);
    }
}
