using CompanyData.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IOrderService
    {
        Task Create(Order order);
        Task<Order> GetOrderById(int Id);
        Task Delete(Order order);
        Task<IEnumerable<Order>> GetOrdersByContactId(int Id);
        Task<Contact> GetContactById(int Id);
        Task<Company> GetCompanyByContactId(int Id);
        Task Update(Order order);
    }
}
