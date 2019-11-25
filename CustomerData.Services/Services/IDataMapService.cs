using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyData.Services.Services
{
    public interface IDataMapService
    {
        IEnumerable<Company> GetAllCompanyData(bool byOrders = true);
    }
    
}
