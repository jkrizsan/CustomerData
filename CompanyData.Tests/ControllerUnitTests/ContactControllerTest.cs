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
    class ContactControllerTest : ControllerTest
    {
        [SetUp]
        public void Setup()
        {
            mockContactService = new Mock<IContactService>();
        }

        #region Create

        [Test]
        public void Test_Create_int()
        {

            var controller = new ContactController(mockContactService.Object);


            var result = controller.Create(1);

            var viewResult = (ViewResult)result;
            var contact = (Contact)viewResult.ViewData.Model;
            Assert.AreEqual(1, contact.CompanyId);
        }

        [Test]
        public async Task Test_Create_contact()
        {

            var controller = new ContactController(mockContactService.Object);
            var contact = new Contact()
            {
                Id = TestInt,
                FirstName = TestString
            };

            var result = await controller.Create(contact);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(ControllerNames.Contact, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Edit, RedirectToActionResult.ActionName);
        }

        #endregion Create

        #region Edit

        [Test]
        public async Task Test_Edit_int()
        {
            mockContactService.Setup(s => s.GetContactById(TestInt))
                .ReturnsAsync(new Contact() { CompanyId = TestInt });
            var controller = new ContactController(mockContactService.Object);


            var result = await controller.Edit(1);

            var viewResult = (ViewResult)result;
            var order = (Contact)viewResult.ViewData.Model;
            Assert.AreEqual(1, order.CompanyId);
        }

        [Test]
        public async Task Test_Edit_contact()
        {
            mockContactService.Setup(s => s.Update(new Contact()));
            var controller = new ContactController(mockContactService.Object);
            var contact = new Contact()
            {
                Id = TestInt,
                FirstName = TestString
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
            mockContactService.Setup(s => s.GetContactById(TestInt))
                .ReturnsAsync(new Contact() { CompanyId = TestInt });
            var controller = new ContactController(mockContactService.Object);


            var result = await controller.Delete(1);

            var viewResult = (ViewResult)result;
            var contact = (Contact)viewResult.ViewData.Model;
            Assert.AreEqual(1, contact.CompanyId);
        }

        [Test]
        public async Task Test_Delete_contact()
        {
            mockContactService.Setup(s => s.Delete(new Contact() {Id = TestInt }));
            var controller = new ContactController(mockContactService.Object);
            var contact = new Contact()
            {
                Id = TestInt,
                FirstName = TestString
            };

            var result = await controller.Delete(TestInt, contact);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(ControllerNames.DataMap, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Index, RedirectToActionResult.ActionName);
        }

        #endregion Delete

    }
}

