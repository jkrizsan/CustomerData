using CompanyData.Data;
using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report> GetReportById(int Id);
    }
}
