using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyData.Data;
using CompanyData.Data.Models;

namespace CompanyData.Services.Services
{
    public class CompanyService : ICompanyService
    {
        CompanyDataDbContext context;
        public CompanyService(CompanyDataDbContext context)
        {
            this.context = context;
        }
        public Company GetCompanyById(int Id)
        {
            return context.Companys.Where(c => c.Id.Equals(Id)).SingleOrDefault();
        }

        public void SaveCompany(Company company)
        {
            var oldCompany = context.Companys.Where(c => c.Id.Equals(company.Id)).SingleOrDefault();
            oldCompany.Id = company.Id;
            oldCompany.Name = company.Name;
            oldCompany.NumberOfContacts = company.NumberOfContacts;
            oldCompany.NumberOfOrders = company.NumberOfOrders;
            oldCompany.TotalIncome = company.TotalIncome;
            context.SaveChanges();
        }
    }
}
