using CompanyData.Data.Models;
using CompanyData.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyData.Tests.ServiceUnitTests
{
    class CompanyServiceTest : ServiceTest
    {
        private CompanyService companyService;
        private ContactService contactService;

        [SetUp]
        public void Setup()
        {
            Initialize();
            contactService = new ContactService(context, orderService);
            companyService = new CompanyService(context, contactService, orderService);
        }

        #region Add

        [Test]
        public void Test_Add_1()
        {
            var company = new Company() {Id = TestInt, Name = TestString };
            companyService.Create(company);
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual(TestString, readCompany.Name);
        }

        #endregion Add

        #region DeleteCompany
        [Test]
        public void Test_DeleteCompany_NoContactsNoOrders()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            context.Companys.Add(company);
            context.SaveChanges();
            
            companyService.Delete(company);
  
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readCompany);
        }

        [Test]
        public void Test_DeleteCompany_WithContactsNoOrders()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.SaveChanges();
            company.Contacts.Add(contact);
            context.SaveChanges();
            
            companyService.Delete(company);
            
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readCompany);
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readContact);
        }

        [Test]
        public void Test_DeleteCompany_WithContactsWithOrders()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var order = new Order() { Id = TestInt, ContactId = TestInt, OrderPrice = TestDouble, OrderDate = DateTime.Now };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.Orders.Add(order);
            context.SaveChanges();
            company.Contacts.Add(contact);
            context.SaveChanges();

            companyService.Delete(company);

            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readCompany);
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readContact);
            var readOrder = context.Orders.Where(o => o.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readOrder);
        }
        #endregion DeleteCompany

        #region GetAllCompanies

        [Test]
        [Ignore("There are some problems")]
        public void Test_GetAllCompanies_ByOrderIsFalse()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var order = new Order() { Id = TestInt, ContactId = TestInt, OrderPrice = TestDouble, OrderDate = DateTime.Now };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.Orders.Add(order);
            context.SaveChanges();
            company.Contacts.Add(contact);
            context.SaveChanges();

            var companies = companyService.GetAllCompanies(false).ToList();

            Assert.AreEqual(TestInt, companies.Count);
            Assert.AreEqual(TestInt, companies[0].Contacts.Count);
            Assert.AreEqual(0, companies[0].Contacts[0].Orders.Count);
        }

        [Test]
        public void Test_GetAllCompanies_ByOrderIsTrue()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var order = new Order() { Id = TestInt, ContactId = TestInt, OrderPrice = TestDouble, OrderDate = DateTime.Now };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.Orders.Add(order);
            context.SaveChanges();
            company.Contacts.Add(contact);
            context.SaveChanges();

            var companies = companyService.GetAllCompanies(true).ToList();

            Assert.AreEqual(TestInt, companies.Count);
            Assert.AreEqual(TestInt, companies[0].Contacts.Count);
            Assert.AreEqual(TestInt, companies[0].Contacts[0].Orders.Count);
        }

        #endregion GetAllCompanies

        #region GetCompanyById

        [Test]
        public void Test_GetCompanyById()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            context.Companys.Add(company);
            context.SaveChanges();

            var readCompany = companyService.GetCompanyById(TestInt);

            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual(TestString, readCompany.Name);
        }

        #endregion GetCompanyById

        #region GetContactsByCompanyId

        [Test]
        public void Test_GetContactsByCompanyId_Id()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact1 = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var contact2 = new Contact() { Id = TestInt + 1, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            context.Companys.Add(company);
            context.Contacts.Add(contact1);
            context.Contacts.Add(contact2);
            context.SaveChanges();
            company.Contacts.Add(contact1);
            company.Contacts.Add(contact2);
            context.SaveChanges();

            var contacts = companyService.GetContactsByCompanyId(TestInt).ToList();

            Assert.AreEqual(2, contacts.Count);
            Assert.AreEqual(TestInt, contacts[0].Id);
            Assert.AreEqual(TestInt + 1, contacts[1].Id);
        }

        #endregion GetContactsByCompanyId

        #region SaveCompany

        [Test]
        public void Test_SaveCompany()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            context.Companys.Add(company);
            context.SaveChanges();
            company.Name = "test2";
            companyService.Update(company);
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual("test2", readCompany.Name);
        }

        #endregion SaveCompany
    }
}
