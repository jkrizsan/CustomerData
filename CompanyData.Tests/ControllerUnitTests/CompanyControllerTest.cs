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
    class CompanyControllerTest : ControllerTest
    {
        [SetUp]
        public void Setup()
        {
            mockCompanyService = new Mock<ICompanyService>();
        }

        #region Create

        [Test]
        public void Test_Create_int()
        {

            var controller = new CompanyController(mockCompanyService.Object);


            var result = controller.Create();

            var viewResult = (ViewResult)result;
            var company = (Company)viewResult.ViewData.Model;
            Assert.AreEqual(0, company.Id);
        }

        [Test]
        public async Task Test_Create_company()
        {

            var controller = new CompanyController(mockCompanyService.Object);
            var company = new Company()
            {
                Id = TestInt,
                Name = TestString
            };

            var result = await controller.Create(company);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(ControllerNames.Company, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Edit, RedirectToActionResult.ActionName);
        }

        #endregion Create

        #region Edit

        [Test]
        public async Task Test_Edit_int()
        {
            mockCompanyService.Setup(s => s.GetCompanyById(TestInt))
                .ReturnsAsync(new Company() { Id = TestInt });
            var controller = new CompanyController(mockCompanyService.Object);


            var result = await controller.Edit(1);

            var viewResult = (ViewResult)result;
            var company = (Company)viewResult.ViewData.Model;
            Assert.AreEqual(TestInt, company.Id);
        }

        [Test]
        public async Task Test_Edit_company()
        {
            mockCompanyService.Setup(s => s.Update(new Company()));
            var controller = new CompanyController(mockCompanyService.Object);
            var contact = new Company()
            {
                Id = TestInt,
                Name = TestString
            };

            var result = await controller.Edit(TestInt, contact);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(null, RedirectToActionResult.ControllerName);
            Assert.AreEqual(null, RedirectToActionResult.ActionName);
        }

        #endregion Edit

        #region Delete

        [Test]
        public async Task Test_Delete_int()
        {
            mockCompanyService.Setup(s => s.GetCompanyById(TestInt))
                .ReturnsAsync(new Company() { Id = TestInt });
            var controller = new CompanyController(mockCompanyService.Object);


            var result = await controller.Delete(1);

            var viewResult = (ViewResult)result;
            var company = (Company)viewResult.ViewData.Model;
            Assert.AreEqual(1, company.Id);
        }

        [Test]
        public async Task Test_Delete_company()
        {
            mockCompanyService.Setup(s => s.Delete(new Company() { Id = TestInt }));
            var controller = new CompanyController(mockCompanyService.Object);
            var company = new Company()
            {
                Id = TestInt,
                Name = TestString
            };

            var result = await controller.Delete(TestInt, company);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(ControllerNames.DataMap, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Index, RedirectToActionResult.ActionName);
        }

        #endregion Delete

    }
}

