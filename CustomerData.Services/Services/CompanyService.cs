using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyData.Data;
using CompanyData.Data.Models;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<int> Create(Company company)
        {
            context.Companys.Add(company);
            await context.SaveChangesAsync();
            return company.Id;
        }

        public async Task Delete(Company company)
        {
            foreach (var item in company.Contacts)
            {
                await contactService.DeleteOrders(item);
            }
            context.Contacts.RemoveRange(company.Contacts);
            context.Companys.Remove(company);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return context.Companys.Include(c => c.Contacts).AsNoTracking().ToList();
        }

        public async Task<Company> GetCompanyById(int Id)
        {
            return await orderService.GetCompanyById(Id);
        }

        public async Task<IEnumerable<Contact>> GetContactsByCompanyId(int Id)
        {
            return context.Contacts.Where(c => c.CompanyId.Equals(Id))
                                            .Include(c => c.Orders);
        }

        public async Task Update(Company company)
        {
            var oldCompany = await GetCompanyById(company.Id);
            oldCompany.Name = company.Name;
            await context.SaveChangesAsync();
        }
    }
}
