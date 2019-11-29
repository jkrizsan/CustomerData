using CompanyData.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Tests.ControllerUnitTests
{
    public class ControllerTest
    {
        protected readonly int TestInt = 1;
        protected readonly double TestDouble = 1.0;
        protected readonly string TestString = "test";
        protected Mock<IOrderService> mockOrderService;
        protected Mock<IContactService> mockContactService;
        protected Mock<ICompanyService> mockCompanyService;
        protected Mock<IGenerateDataService> mockGenerateDataService;
        protected Mock<IDataMapService> mockDataMapService;
        protected Mock<IReportService> mockReportService;
    }
}
