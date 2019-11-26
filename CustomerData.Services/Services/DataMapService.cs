using CompanyData.Data;
using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public class DataMapService: IDataMapService
    {

        CompanyDataDbContext context;
        ICompanyService companyService;

        public DataMapService(CompanyDataDbContext context, ICompanyService companyService)
        {
            this.context = context;
            this.companyService = companyService;
        }

        public async Task<IEnumerable<Company>> GetAllCompanyData(bool byOrders = true)
        {
            return await companyService.GetAllCompanies(byOrders);
        }
    }
}
