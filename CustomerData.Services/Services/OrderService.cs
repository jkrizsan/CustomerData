﻿using CompanyData.Data;
using CompanyData.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public class OrderService : IOrderService
    {
        CompanyDataDbContext context;

        public OrderService(CompanyDataDbContext context)
        {
            this.context = context;
        }
        public async Task Create(Order order)
        {
            if (order is null)
            {
                return;
            }
            var contact = context.Contacts.Where(c => c.Id.Equals(order.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.AddOrder(order);
            await context.SaveChangesAsync();
            company.AddOrder(order);
            await context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int Id)
        {
            return context.Orders.Where(o => o.Id.Equals(Id)).SingleOrDefault();
        }

        public async Task<Contact> GetContactById(int Id)
        {
            var contact = context.Contacts.Where(c => c.Id.Equals(Id)).SingleOrDefault();
            contact.Orders = (await GetOrdersByContactId(contact.Id)).ToList();
            return contact;
        }

        public async Task<Company> GetCompanyById(int Id)
        {
            return context.Companys.Where(c => c.Id.Equals(Id)).Include(c => c.Contacts).SingleOrDefault();
        }

        public async Task Delete(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException();
            }

            var oldOrder = context.Orders.Where(o => o.Id.Equals(order.Id)).SingleOrDefault();
            var contact = context.Contacts.Where(c => c.Id.Equals(oldOrder.ContactId)).SingleOrDefault();
            var company = context.Companys.Where(c => c.Id.Equals(contact.CompanyId)).SingleOrDefault();
            contact.DeleteOrder(oldOrder);
            company.DeleteOrder(oldOrder);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByContactId(int Id)
        {
            return context.Orders.Where(o => o.ContactId.Equals(Id));
        }

        public async Task Update(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException();
            }

            var oldOrder = await GetOrderById(order.Id);
            var contact = await GetContactById(oldOrder.ContactId);
            var company = await GetCompanyById(contact.CompanyId);
            contact.UpdateByOrder(oldOrder, order);
            await context.SaveChangesAsync();
            company.UpdateByOrder(order);
            await context.SaveChangesAsync();
        }
    }
}