using CompanyData.Data;
using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public class DataMapService: IDataMapService
    {

        CompanyDataDbContext context;


        public DataMapService(CompanyDataDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Company> GetAllCompanyData()
        {
            return context.Companys;
        }
        
    }
}
