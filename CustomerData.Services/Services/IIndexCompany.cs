using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyData.Services
{
    public interface IIndexCompany
    {
        IList<Company> Companys { get; set; }
        Task OnGetAsync();
    }
}
