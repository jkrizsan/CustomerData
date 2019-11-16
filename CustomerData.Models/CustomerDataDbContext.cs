using CustomerData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerData.Data
{
    public class CustomerDataDbContext : DbContext
    {
        public CustomerDataDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }


    }
}
