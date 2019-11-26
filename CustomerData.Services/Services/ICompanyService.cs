using CompanyData.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies(bool byOrders = true);
        Task<IEnumerable<Contact>> GetContactsByCompanyId(int Id, bool byOrders = true);
        Task<Company> GetCompanyById(int Id);
        Task Update(Company company);
        Task Delete(Company company);
        Task<int> Create(Company company);
    }
}
