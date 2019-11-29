using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IDataMapService
    {
        Task<IEnumerable<Company>> GetAllCompanyData();
    }
}
