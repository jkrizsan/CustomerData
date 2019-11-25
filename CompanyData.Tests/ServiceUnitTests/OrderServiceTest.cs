using CompanyData.Data.Models;
using CompanyData.Services.Services;
using CompanyData.Tests.ServiceUnitTests;
using NUnit.Framework;
using System.Linq;

namespace CompanyData.Tests
{
    public class OrderServiceTest : ServiceTest
    {
        private OrderService orderService;

        [SetUp]
        public void Setup()
        {
            Initialize();
            orderService = new OrderService(context);
        }

        [Test]
        public void Test1()
        {
            orderService.Add(new Order() { Id = 1, OrderPrice = 100 }) ;
            var order = context.Orders.Where(o => o.Id.Equals(1)).SingleOrDefault();
            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(100, order.OrderPrice);

        }
    }
}