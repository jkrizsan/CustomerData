﻿using CompanyData.Data.Models;
using CompanyData.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Tests.ServiceUnitTests
{
    class OrderServiceTest : ServiceTest
    {
        private CompanyService companyService;
        private ContactService contactService;
        private Company company;
        private Contact contact;
        private Order order;

        [SetUp]
        public void Setup()
        {
            Initialize();
            contactService = new ContactService(context, orderService);
            companyService = new CompanyService(context, contactService, orderService);
            company = new Company() { Id = TestInt, Name = TestString };
            contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            company.Contacts.Add(contact);
            order = new Order() { Id = TestInt, OrderPrice = TestDouble, ContactId = TestInt };
            context.Orders.Add(order);
            context.SaveChangesAsync();
        }

        #region Add

        [Test]
        public void Test_Create_1()
        {
            
            orderService.Create(order);
            var readOrder = context.Orders.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readOrder.Id);
            Assert.AreEqual(TestDouble, readOrder.OrderPrice);
        }

        #endregion Add

        #region GetOrderById

        [Test]
        public async Task TestGetOrderById_1()
        {
            var readOrder = await orderService.GetOrderById(TestInt);
            Assert.AreEqual(TestInt, readOrder.Id);
            Assert.AreEqual(TestDouble, readOrder.OrderPrice);
        }

        #endregion GetOrderById

        #region GetContactById

        [Test]
        public async Task Test_GetContactById_1()
        {

            var readContact = await orderService.GetContactById(TestInt);
            Assert.AreEqual(TestInt, readContact.Id);
            Assert.AreEqual(TestString, readContact.FirstName);
        }

        #endregion GetContactById

        #region GetCompanyByContactId

        [Test]
        public async Task Test_GetCompanyByContactId_1()
        {

            var readCompany =await orderService.GetCompanyByContactId(TestInt);
            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual(TestString, readCompany.Name);
        }

        #endregion GetCompanyByContactId

        #region DeleteOrder

        [Test]
        public async Task Test_Delete_1()
        {
            await orderService.Delete(order);
            var readOrder = context.Orders.Where(o => o.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readOrder);
        }

        #endregion DeleteOrder

        #region GetOrdersByContactId

        [Test]
        public async Task Test_GetOrdersByContactId_1()
        {
            var order2 = new Order() { Id = 2, OrderPrice = TestDouble, ContactId = TestInt };
            context.Orders.Add(order2);
            contact.Orders.Add(order2);
            await context.SaveChangesAsync();
            var orders = (await orderService.GetOrdersByContactId(TestInt)).ToList();
            Assert.AreEqual(2, orders.Count);
            Assert.AreEqual(1, orders[0].Id);
            Assert.AreEqual(2, orders[1].Id);
        }

        #endregion GetOrdersByContactId

        #region SaveOrder

        [Test]
        public async Task Test_Update_1()
        {
            var order2 = new Order() { Id = TestInt, OrderPrice = 2, ContactId = TestInt };
            
            await orderService.Update(order2);
            var readOrder = context.Orders.Where(o => o.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(2, readOrder.OrderPrice);
           
        }

        #endregion SaveOrder

    }
}
