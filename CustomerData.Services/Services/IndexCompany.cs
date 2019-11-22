using CompanyData.Data;
using CompanyData.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyData.Services
{
    public class IndexCompany : PageModel, IIndexCompany
    {
        private readonly CompanyDataDbContext context;

        public IndexCompany(CompanyDataDbContext context)
        {
            this.context = context;
        }

        public IList<Company> Companys { get; set; }

        public async Task OnGetAsync()
        {
            Companys = await context.Companys.ToListAsync();
        }
    }
}
