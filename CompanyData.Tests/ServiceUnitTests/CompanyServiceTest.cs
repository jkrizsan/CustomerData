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
        public async Task Test_Add_1()
        {
            var company = new Company() {Id = TestInt, Name = TestString };
            await companyService.Create(company);
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual(TestString, readCompany.Name);
        }

        #endregion Add

        #region DeleteCompany
        [Test]
        public async Task Test_DeleteCompany_NoContactsNoOrders()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            context.Companys.Add(company);
            await context.SaveChangesAsync();
            
            await companyService.Delete(company);
  
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readCompany);
        }

        [Test]
        public async Task Test_DeleteCompany_WithContactsNoOrders()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            await context.SaveChangesAsync();
            company.Contacts.Add(contact);
            await context.SaveChangesAsync();
            
            await companyService.Delete(company);
            
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readCompany);
            var readContact = context.Contacts.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(null, readContact);
        }

        [Test]
        public async Task Test_DeleteCompany_WithContactsWithOrders()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var order = new Order() { Id = TestInt, ContactId = TestInt, OrderPrice = TestDouble, OrderDate = DateTime.Now };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            company.Contacts.Add(contact);
            await context.SaveChangesAsync();

            await companyService.Delete(company);

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
        public async Task Test_GetAllCompanies_ByOrderIsFalse()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var order = new Order() { Id = TestInt, ContactId = TestInt, OrderPrice = TestDouble, OrderDate = DateTime.Now };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            company.Contacts.Add(contact);
            await context.SaveChangesAsync();

            var companies = (await companyService.GetAllCompanies(false)).ToList();

            Assert.AreEqual(TestInt, companies.Count);
            Assert.AreEqual(TestInt, companies[0].Contacts.Count);
            Assert.AreEqual(0, companies[0].Contacts[0].Orders.Count);
        }

        [Test]
        public async Task Test_GetAllCompanies_ByOrderIsTrue()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var order = new Order() { Id = TestInt, ContactId = TestInt, OrderPrice = TestDouble, OrderDate = DateTime.Now };
            context.Companys.Add(company);
            context.Contacts.Add(contact);
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            company.Contacts.Add(contact);
            await context.SaveChangesAsync();

            var companies = (await companyService.GetAllCompanies(true)).ToList();

            Assert.AreEqual(TestInt, companies.Count);
            Assert.AreEqual(TestInt, companies[0].Contacts.Count);
            Assert.AreEqual(TestInt, companies[0].Contacts[0].Orders.Count);
        }

        #endregion GetAllCompanies

        #region GetCompanyById

        [Test]
        public async Task Test_GetCompanyById()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            context.Companys.Add(company);
            await context.SaveChangesAsync();

            var readCompany = await companyService.GetCompanyById(TestInt);

            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual(TestString, readCompany.Name);
        }

        #endregion GetCompanyById

        #region GetContactsByCompanyId

        [Test]
        public async Task Test_GetContactsByCompanyId_Id()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            var contact1 = new Contact() { Id = TestInt, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            var contact2 = new Contact() { Id = TestInt + 1, FirstName = TestString, MiddleName = TestString, LastName = TestString };
            context.Companys.Add(company);
            context.Contacts.Add(contact1);
            context.Contacts.Add(contact2);
            await context.SaveChangesAsync();
            company.Contacts.Add(contact1);
            company.Contacts.Add(contact2);
            await context.SaveChangesAsync();

            var contacts = (await companyService.GetContactsByCompanyId(TestInt)).ToList();

            Assert.AreEqual(2, contacts.Count);
            Assert.AreEqual(TestInt, contacts[0].Id);
            Assert.AreEqual(TestInt + 1, contacts[1].Id);
        }

        #endregion GetContactsByCompanyId

        #region SaveCompany

        [Test]
        public async Task Test_SaveCompany()
        {
            var company = new Company() { Id = TestInt, Name = TestString };
            context.Companys.Add(company);
            await context.SaveChangesAsync();
            company.Name = "test2";
            await companyService.Update(company);
            var readCompany = context.Companys.Where(c => c.Id.Equals(TestInt)).SingleOrDefault();
            Assert.AreEqual(TestInt, readCompany.Id);
            Assert.AreEqual("test2", readCompany.Name);
        }

        #endregion SaveCompany
    }
}
