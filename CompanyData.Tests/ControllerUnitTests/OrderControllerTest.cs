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
    class OrderControllerTest : ControllerTest
    {
        [SetUp]
        public void Setup()
        {
            mockOrderService = new Mock<IOrderService>();
        }

        #region Create

        [Test]
        public  void Test_Create_int()
        {
            
            var controller = new OrderController(mockOrderService.Object);
            

            var result = controller.Create(1);

            var viewResult = (ViewResult)result;
            var order  = (Order)viewResult.ViewData.Model;
            Assert.AreEqual(1, order.ContactId);
        }

        [Test]
        public async Task Test_Create_order()
        {

            var controller = new OrderController(mockOrderService.Object);
            var order = new Order()
            {
                Id = TestInt,
                OrderPrice = TestDouble
            };

            var result = await controller.Create(order);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(ControllerNames.Contact, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Edit, RedirectToActionResult.ActionName);
        }

        #endregion Create

        #region Edit

        [Test]
        public async Task Test_Edit_int()
        {
            mockOrderService.Setup(s => s.GetOrderById(TestInt))
                .ReturnsAsync(new Order() {ContactId = TestInt });
            var controller = new OrderController(mockOrderService.Object);


            var result = await controller.Edit(1);

            var viewResult = (ViewResult)result;
            var order = (Order)viewResult.ViewData.Model;
            Assert.AreEqual(1, order.ContactId);
        }

        [Test]
        public async Task Test_Edit_order()
        {
            mockOrderService.Setup(s => s.Update(new Order()));
            var controller = new OrderController(mockOrderService.Object);
            var order = new Order()
            {
                Id = TestInt,
                OrderPrice = TestDouble
            };

            var result = await controller.Edit(TestInt, order);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(null, RedirectToActionResult.ControllerName);
            Assert.AreEqual(null, RedirectToActionResult.ActionName);
        }

        #endregion Edit

        #region Delete

        [Test]
        public async Task Test_Delete_int()
        {
            mockOrderService.Setup(s => s.GetOrderById(TestInt))
                .ReturnsAsync(new Order() { ContactId = TestInt });
            var controller = new OrderController(mockOrderService.Object);


            var result = await controller.Delete(1);

            var viewResult = (ViewResult)result;
            var order = (Order)viewResult.ViewData.Model;
            Assert.AreEqual(1, order.ContactId);
        }

        [Test]
        public async Task Test_Delete_order()
        {
            mockOrderService.Setup(s => s.Delete(new Order()));
            var controller = new OrderController(mockOrderService.Object);
            var order = new Order()
            {
                Id = TestInt,
                OrderPrice = TestDouble
            };

            var result = await controller.Delete(TestInt, order);

            var RedirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual(ControllerNames.DataMap, RedirectToActionResult.ControllerName);
            Assert.AreEqual(ActionNames.Index, RedirectToActionResult.ActionName);
        }

        #endregion Delete

    }
}

