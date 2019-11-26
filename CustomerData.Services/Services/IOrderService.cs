using CompanyData.Data.Models;
using System.Collections.Generic;

namespace CompanyData.Services.Services
{
    public interface IOrderService
    {
        void Create(Order order);
        Order GetOrderById(int Id);
        void Delete(Order order);
        IEnumerable<Order> GetOrdersByContactId(int Id);
        Contact GetContactById(int Id);
        Company GetCompanyByContactId(int Id);
        void Update(Order order);
    }
}
