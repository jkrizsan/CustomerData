﻿using CompanyData.Data;
using CompanyData.Data.DTOs;
using CompanyData.Data.Models;
using CompanyData.Services.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Services
{
    public class GenerateDataService : IGenerateDataService
    {
        CompanyDataDbContext context;

        private List<string> companyNames = new List<string>()
        {"Tree", "Branch", "Technologies", "Flower", "Leaf", "Consulting", "Company", "Bird", "Ant", "Holding", "Big", "Small", "Dog", "Cat" };

        private List<string> companyNameEndding = new List<string>()
        {"Oy", "AG", "GMBH", "KFT", "BT", "INC", "Ltd", "NL", "SRL", "LP", "LLP", "CORP" };

        private List<string> firstNames = new List<string>()
        {"Jon", "Liam", "Noah", " William", "James", "Logan", "Benjamin", "Mason", "Elijah", "Oliver", "Jacob", "Ida", "Emma", "Alma", "Ella", "Sofia", "Freja", "Josefine", "Clara", "Anna" };

        private List<string> lastNames = new List<string>()
        {"Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Rodriguez", "Davis", "Gonzales", "Lopez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "London", "Lee", "Perez", "Sanchez", "Harris","Clark", "Young", "Walker", "Lie", "Wright", "King", "Scott" };

        private Random rnd;

        public GenerateDataService(CompanyDataDbContext context)
        {
            this.context = context;
            rnd = new Random();
        }

        public async Task GenerataData(GenerateDataDto data)
        {
            if (data is null)
            {
                throw new ArgumentNullException();
            }

            for (int i = 0; i < data.CompanyNumber; i++)
            {
                await GenerataCompanies(data);
            }
        }

        private string GenerateCompanyName()
        {
            var numberOfNames = rnd.Next(1, 3);
            StringBuilder companyName = new StringBuilder();

            for (int i = 0; i < numberOfNames; i++)
            {
                companyName.Append(companyNames[rnd.Next(companyNames.Count)]);
                companyName.Append(" ");
            }
            companyName.Append(companyNameEndding[rnd.Next(companyNameEndding.Count)]);

            return companyName.ToString();
        }

        public async Task RemoveAllCompanyData()
        {
            var orders = await context.Orders.ToListAsync();
            context.Orders.RemoveRange(orders);

            var conatcts = await context.Contacts.ToListAsync();
            context.Contacts.RemoveRange(conatcts);

            var companies = await context.Companys.ToListAsync();
            context.Companys.RemoveRange(companies);

            await context.SaveChangesAsync();

            var reports = await context.Reports.ToListAsync();
            context.Reports.RemoveRange(reports);

            await context.SaveChangesAsync();
        }

        public async Task GenerataCompanies(GenerateDataDto data)
        {
            if (data is null)
            {
                throw new ArgumentNullException();
            }

            var company = new Company();
            company.Name = GenerateCompanyName();
            company.NumberOfContacts = rnd.Next(data.MinContactNumber, data.MaxContactNumber);
            context.Companys.Add(company);
            for (int j = 0; j < company.NumberOfContacts; j++)
            {
                var contact = await GenerataContact(company, data);
                for (int k = 0; k < contact.NumnerOfOrders; k++)
                {
                    var order = await GenerataOrder(contact, data);
                    company.NumberOfOrders++;
                    company.TotalIncome += order.OrderPrice;
                }
                company.Contacts.Add(contact);
            }
            await context.SaveChangesAsync();
        }

        public async Task<Contact> GenerataContact(Company comnay, GenerateDataDto data)
        {
            if (data is null || comnay is null)
            {
                throw new ArgumentNullException();
            } 
            var contact = new Contact();
            contact.FirstName = firstNames[rnd.Next(firstNames.Count)];
            contact.MiddleName = rnd.Next(0, 10) > 5 ? rnd.Next(0, 10) > 5 ? firstNames[rnd.Next(firstNames.Count)]
                                                                            : lastNames[rnd.Next(lastNames.Count)]
                                                     : "";
            contact.LastName = lastNames[rnd.Next(lastNames.Count)];
            contact.NumnerOfOrders = rnd.Next(data.MinOrderNumber, data.MaxOrderNumber);
            context.Contacts.Add(contact);
            return contact;
        }

        public async Task<Order> GenerataOrder(Contact contact, GenerateDataDto data)
        {
            if (data is null || contact is null)
            {
                throw new ArgumentNullException();
            }
            var order = new Order();
            order.OrderDate = DateTime.Now.AddDays(-rnd.Next(0, 100));
            order.OrderPrice = rnd.Next(data.MinOrderPrice, data.MaxOrderPrice);
            context.Orders.Add(order);
            contact.Orders.Add(order);
            contact.Income += order.OrderPrice;
            return order;
        }
    }
}
