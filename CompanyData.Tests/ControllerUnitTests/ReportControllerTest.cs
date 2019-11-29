using CompanyData.Data.Models;
using CompanyData.Services.Services;
using CompanyData.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Tests.ControllerUnitTests
{
    class ReportControllerTest : ControllerTest
    {
        [SetUp]
        public void Setup()
        {
            mockReportService = new Mock<IReportService>();
        }

        #region View

        [Test]
        public async Task Test_View_int()
        {
            mockReportService.Setup(s => s.GetReportById(TestInt))
                .ReturnsAsync(new Report() { Id = TestInt });

            var controller = new ReportController(mockReportService.Object);

            var result = await controller.View(1);

            var viewResult = (ViewResult)result;
            var report = (Report)viewResult.ViewData.Model;
            Assert.AreEqual(1, report.Id);
        }

        #endregion View

        #region Index

        [Test]
        public async Task Test_Index_1()
        {
            mockReportService.Setup(s => s.GetAllReports())
                .ReturnsAsync(new List<Report>() { new Report() { OldValues = TestString } });
            var controller = new ReportController(mockReportService.Object);

            var result = await controller.Index("", "", "", 1);

            var viewResult = (ViewResult)result;
            var report = (List<Report>)viewResult.ViewData.Model;
            Assert.AreEqual(TestString, report[0].OldValues);
        }

        #endregion Index

    }
}
