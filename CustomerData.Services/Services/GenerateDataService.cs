﻿using CompanyData.Data;
using CompanyData.Data.DTOs;
using CompanyData.Data.Models;
using CompanyData.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void GenerataCompaniesData(GenerateDataDTO data)
        {
            for (int i = 0; i < data.CompanyNumber; i++)
            {
                var company = new Company();
                company.Name = GenerateCompanyName();
                company.NumberOfContacts = rnd.Next(data.MinContactNumber, data.MaxContactNumber);
                context.Companys.Add(company);
                context.SaveChanges();
                for (int j = 0; j < company.NumberOfContacts; j++)
                {
                    var contact = new Contact();
                    contact.FirstName = firstNames[rnd.Next(firstNames.Count)];
                    contact.MiddleName = rnd.Next(0, 10) > 5 ? rnd.Next(0, 10) > 5 ? firstNames[rnd.Next(firstNames.Count)]
                                                                                    : lastNames[rnd.Next(lastNames.Count)]
                                                             : "";
                    contact.LastName = lastNames[rnd.Next(lastNames.Count)];
                    contact.NumnerOfOrders = rnd.Next(data.MinOrderNumber, data.MaxOrderNumber);
                    context.Contacts.Add(contact);
                    context.SaveChanges();
                    for (int k = 0; k < contact.NumnerOfOrders; k++)
                    {
                        var order = new Order();
                        order.OrderDate = DateTime.Now.AddDays(-rnd.Next(0,100));
                        order.OrderPrice = rnd.Next(data.MinOrderPrice, data.MaxOrderPrice);
                        context.Orders.Add(order);
                        context.SaveChanges();
                        contact.Orders.Add(order);
                        contact.Income += order.OrderPrice;
                        company.NumberOfOrders++;
                        company.TotalIncome += order.OrderPrice;
                        context.SaveChanges();
                    }
                    company.Contacts.Add(contact);
                    context.SaveChanges();
                }
            }
        }

        private string GenerateCompanyName()
        {
            var numberOfNames = rnd.Next(1,3);
            StringBuilder companyName = new StringBuilder();
            
            for (int i = 0; i < numberOfNames; i++)
            {
                companyName.Append(companyNames[rnd.Next(companyNames.Count)]);
                companyName.Append(" ");
            }
            companyName.Append(companyNameEndding[rnd.Next(companyNameEndding.Count)]);

            return companyName.ToString();
        }

        public void RemoveAllCompanyData()
        {
            var companies = context.Companys.ToList();
            context.Companys.RemoveRange(companies);

            var conatcts = context.Contacts.ToList();
            context.Contacts.RemoveRange(conatcts);

            var orders = context.Orders.ToList();
            context.Orders.RemoveRange(orders);

            context.SaveChanges();

        }
    }
}
