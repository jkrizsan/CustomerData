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
using CompanyData.Data.DTOs;

namespace CompanyData.Tests.ControllerUnitTests
{
    class GenerateDataControllerTest : ControllerTest
    {
        [SetUp]
        public void Setup()
        {
            mockGenerateDataService = new Mock<IGenerateDataService>();
        }

        #region Edit

        [Test]
        public void Test_Edit()
        {
            var controller = new GenerateDataController(mockGenerateDataService.Object);

            var result = controller.Edit(TestInt);

            var viewResult = (ViewResult)result;
            var generatadata = (GenerateDataDto)viewResult.ViewData.Model;
            Assert.AreEqual(100, generatadata.CompanyNumber);
        }

        [Test]
        public async Task Test_Edit_generateData()
        {
            mockGenerateDataService.Setup(s => s.GenerataData(new GenerateDataDto()));
            var controller = new GenerateDataController(mockGenerateDataService.Object);
            var generatedata = new GenerateDataDto();

            var result = await controller.Edit(TestInt, generatedata, "Generate Data");

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(null, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Edit, RedirectToActionResult.ActionName);
        }

        [Test]
        public async Task Test_Edit_deleteData()
        {
            mockGenerateDataService.Setup(s => s.RemoveAllCompanyData());
            var controller = new GenerateDataController(mockGenerateDataService.Object);
            var generatedata = new GenerateDataDto();

            var result = await controller.Edit(TestInt, generatedata, "Delete Data");

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(null, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Edit, RedirectToActionResult.ActionName);
        }

        #endregion Edit

    }
}