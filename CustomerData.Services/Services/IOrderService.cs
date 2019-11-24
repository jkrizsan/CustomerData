using CompanyData.Data.Models;
using System.Collections.Generic;

namespace CompanyData.Services.Services
{
    public interface IOrderService
    {
        void Add(Order order);
        Order GetOrderById(int Id);
        void DeleteOrder(int Id);
        IEnumerable<Order> GetOrdersByContactId(int Id);
        void SaveOrder(Order order);
    }
}
