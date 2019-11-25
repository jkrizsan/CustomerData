using CompanyData.Data;
using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public IEnumerable<Company> GetAllCompanyData(bool byOrders = true)
        {
            return companyService.GetAllCompanies(byOrders);
        }
    }
}
