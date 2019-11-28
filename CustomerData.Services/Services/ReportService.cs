using CompanyData.Data;
using CompanyData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyData.Services.Services
{
    public class ReportService : IReportService
    {
        CompanyDataDbContext context;

        public ReportService(CompanyDataDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return context.Reports.ToList();
        }

        public async Task<Report> GetReportById(int Id)
        {
            return context.Reports.Where(r => r.Id.Equals(Id)).SingleOrDefault();
        }
    }
}
