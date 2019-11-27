using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using CompanyData.Services.Services;
using CompanyData.Data.Models;
using CompanyData.Web.Controllers;
using NUnit.Framework;
using CompanyData.Data.Parameters;

namespace CompanyData.Tests.ControllerUnitTests
{
    class DataMapControllerTest : ControllerTest
    {
        [SetUp]
        public void Setup()
        {
            mockDataMapService = new Mock<IDataMapService>();
        }

        #region Index

        [Test]
        public async Task Test_Index_1()
        {
            mockDataMapService.Setup(s => s.GetAllCompanyData(false))
                .ReturnsAsync(new List<Company>(){ new Company() {Name = TestString } });
            var controller = new DataMapController(mockDataMapService.Object);


            var result = await controller.Index("", "", "",1);

            var viewResult =(ViewResult)result;
            var datamap = (List<Company>)viewResult.ViewData.Model;
            Assert.AreEqual(TestString, datamap[0].Name);
        }

        #endregion Index


    }
}

