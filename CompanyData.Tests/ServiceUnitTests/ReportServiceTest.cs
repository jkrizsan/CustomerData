using CompanyData.Data.Models;
using CompanyData.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Tests.ServiceUnitTests
{
    class ReportServiceTest : ServiceTest
    {
        private IReportService reportService;


        [SetUp]
        public void Setup()
        {
            Initialize();
            reportService = new ReportService(context);
        }

        #region GetAllReports

        [Test]
        public async Task Test_GetAllReports()
        {

            var report = new Report() {Id = TestInt };
            context.Reports.Add(report);
            await context.SaveChangesAsync();

            var reports = (await reportService.GetAllReports()).ToList();

            Assert.AreEqual(TestInt, reports.Count);
            Assert.AreEqual(TestInt, reports[0].Id);
        }

        #endregion GetAllReports

        #region GetReportById

        [Test]
        public async Task Test_GetCompanyById()
        {
            var report = new Report() { Id = TestInt };
            context.Reports.Add(report);
            await context.SaveChangesAsync();

            var readReport = await reportService.GetReportById(TestInt);

            Assert.AreEqual(TestInt, readReport.Id);
        }

        #endregion GetReportById

    }
}
