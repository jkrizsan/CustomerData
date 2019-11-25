using CompanyData.Data;
using CompanyData.Services.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace CompanyData.Tests.ServiceUnitTests
{
    public class ServiceTest
    {
        protected readonly int TestInt = 1;
        protected readonly double TestDouble = 1.0;
        protected readonly string TestString = "test";

        protected  CompanyDataDbContext context;
        protected IOrderService orderService;
        protected void Initialize()
        {
            var options = new DbContextOptionsBuilder<CompanyDataDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            context = new CompanyDataDbContext(options);
            orderService = new OrderService(context);
        }
    }
}
