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


        public DataMapService(CompanyDataDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Company> GetAllCompanyData()
        {
            var companys = context.Companys.ToList();
            foreach (var item in companys)
            {
                var contacts = context.Contacts.Where( c => c.CompanyId.Equals(item.Id) );
                item.Contacts = contacts.ToList();
            }

            return companys;
        }
        
    }
}
