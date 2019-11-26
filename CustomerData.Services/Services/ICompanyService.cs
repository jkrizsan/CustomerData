using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies(bool byOrders = true);
        IEnumerable<Contact> GetContactsByCompanyId(int Id, bool byOrders = true);
        Company GetCompanyById(int Id);
        void Update(Company company);
        void Delete(Company company);
        int Create(Company company);
    }
}
