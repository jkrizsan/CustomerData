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

        public CompanyService()
        {
        }

        public CompanyService(CompanyDataDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var companies = context.Companys.ToList();
            foreach (var item in companies)
            {
                item.Contacts = GetContactsByCompanyId(item.Id).ToList();
            }
            return companies;
        }

        public Company GetCompanyById(int Id)
        {
            var company = context.Companys.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            company.Contacts = GetContactsByCompanyId(company.Id).ToList();
            return company;
        }

        public IEnumerable<Contact> GetContactsByCompanyId(int Id)
        {
            return context.Contacts.Where(c => c.CompanyId.Equals(Id));
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
