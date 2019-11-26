using CompanyData.Data.Models;
using CompanyData.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyData.Tests.ServiceUnitTests
{
    class ContactServiceTest : ServiceTest
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
            context.SaveChanges();
        }

        #region DeleteOrders

        [Test]
        public void Test_DeleteOrders_1()
        {
            contactService.DeleteOrders(contact);
            var readOrder = context.Orders.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readOrder);
        }

        #endregion DeleteOrders

        #region DeleteContact

        [Test]
        public void Test_DeleteContact_1()
        {
            contactService.Delete(TestInt);
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readContact);
        }

        #endregion DeleteContact

        #region GetContactById

        [Test]
        public void Test_GetContactById_1()
        {
            var readContact = contactService.GetContactById(TestInt);
            Assert.AreEqual(TestInt, readContact.Id);
            Assert.AreEqual(TestString, readContact.FirstName);
        }

        #endregion GetContactById

        #region SaveContact

        [Test]
        public void Test_SaveContact_1()
        {
            var contact2 = new Contact() { Id = TestInt, FirstName = "test2" };
            contactService.Update(contact2);
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readContact.Id);
            Assert.AreEqual("test2", readContact.FirstName);
        }

        #endregion SaveContact

        #region Add

        [Test]
        public void Test_Add_1()
        {
            var contact2 = new Contact() { Id = 2, FirstName = "test2", CompanyId=TestInt };
            context.Contacts.Add(contact2);
            context.SaveChanges();
            contactService.Create(contact2);
            var readContact = context.Contacts.Where(c => c.Id.Equals(2)).SingleOrDefault();
            Assert.AreEqual(2, readContact.Id);
            Assert.AreEqual("test2", readContact.FirstName);
        }

        #endregion Add

    }
}
