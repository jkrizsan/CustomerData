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
        IContactService contactService;
        IOrderService orderService;

        public CompanyService(CompanyDataDbContext context,
                             IContactService contactService,
                             IOrderService orderService)
        {
            this.context = context;
            this.contactService = contactService;
            this.orderService = orderService;
        }

        public int Create(Company company)
        {
            context.Companys.Add(company);
            context.SaveChanges();
            return company.Id;
        }

        public void Delete(Company company)
        {
            foreach (var item in company.Contacts)
            {
                contactService.DeleteOrders(item);
            }
            context.Contacts.RemoveRange(company.Contacts);
            context.Companys.Remove(company);
            context.SaveChanges();
        }

        public IEnumerable<Company> GetAllCompanies(bool byOrders = true)
        {
            var companies = context.Companys.ToList();
            foreach (var item in companies)
            {
                item.Contacts = GetContactsByCompanyId(item.Id, byOrders).ToList();
            }
            return companies;
        }

        public Company GetCompanyById(int Id)
        {
            var company = context.Companys.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            company.Contacts = GetContactsByCompanyId(company.Id).ToList();
            foreach (var contact in company.Contacts)
            {
                contact.Orders = orderService.GetOrdersByContactId(contact.Id).ToList();
            }
            return company;
        }

        public IEnumerable<Contact> GetContactsByCompanyId(int Id, bool byOrders = true)
        {
            var contacts = context.Contacts.Where(c => c.CompanyId.Equals(Id));
            if (byOrders)
            {
                foreach (var item in contacts)
                {
                    item.Orders = orderService.GetOrdersByContactId(item.Id).ToList();
                }
            }
            return contacts;
        }

        public void Update(Company company)
        {
            var oldCompany = GetCompanyById(company.Id);
            oldCompany.Name = company.Name;
            context.SaveChanges();
        }
    }
}
