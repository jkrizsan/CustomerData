using CompanyData.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyData.Data
{
    public class CompanyDataDbContext : DbContext
    {
        public CompanyDataDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
