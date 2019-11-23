using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies();
        IEnumerable<Contact> GetContactsByCompanyId(int Id);
        Company GetCompanyById(int Id);
        void SaveCompany(Company company);
    }
}
