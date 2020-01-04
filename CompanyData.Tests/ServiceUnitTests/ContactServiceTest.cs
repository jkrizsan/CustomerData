using CompanyData.Data;
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
    class ContactServiceTest : ServiceTest
    {
        private IContactService contactService;
        private Company company;
        private Contact contact;
        private Order order;


        [SetUp]
        public void Setup()
        {
            Initialize();
            
            company = new Company() { Id = TestInt, Name = TestString };
            contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            company.Contacts.Add(contact);
            order = new Order() { Id = TestInt, OrderPrice = TestDouble, ContactId = TestInt };
            context.Orders.Add(order);
            context.SaveChangesAsync();
            orderService = new OrderService(context);
            contactService = new ContactService(context, orderService);
        }

        #region DeleteOrders

        [Test]
        public void Test_DeleteOrders_NoResult()
        {
            contactService.DeleteOrders(null);
            var readOrder = context.Orders.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreNotEqual(null, readOrder);
        }

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
        public async Task Test_DeleteContact_Delete()
        {
            Assert.That(async () => await contactService.Delete(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task Test_DeleteContact_1()
        {
            await contactService.Delete(new Contact() {Id = TestInt });
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readContact);
        }

        #endregion DeleteContact

        #region GetContactById

        [Test]
        public async Task Test_GetContactById_1()
        {
            var readContact = await contactService.GetContactById(TestInt);
            Assert.AreEqual(TestInt, readContact.Id);
            Assert.AreEqual(TestString, readContact.FirstName);
        }

        #endregion GetContactById

        #region SaveContact
        [Test]
        public async Task Test_Update_Exception()
        {
            Assert.That(async () => await contactService.Update(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task Test_Update_1()
        {
            var contact2 = new Contact() { Id = TestInt, FirstName = "test2" };
            await contactService.Update(contact2);
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readContact.Id);
            Assert.AreEqual("test2", readContact.FirstName);
        }

        #endregion SaveContact

        #region Add

        [Test]
        public async Task Test_Create_Exception()
        {
            Assert.That(async () => await contactService.Create(null), Throws.ArgumentNullException);
        }

        [Test]
        public async Task Test_Create_1()
        {
            var contact2 = new Contact() { Id = 2, FirstName = "test2", CompanyId=TestInt };
            context.Contacts.Add(contact2);
            await context.SaveChangesAsync();
            await contactService.Create(contact2);
            var readContact = context.Contacts.Where(c => c.Id.Equals(2)).SingleOrDefault();
            Assert.AreEqual(2, readContact.Id);
            Assert.AreEqual("test2", readContact.FirstName);
        }

        #endregion Add

    }
}
