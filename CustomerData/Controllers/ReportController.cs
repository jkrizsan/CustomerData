using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyData.Data.Models;
using CompanyData.Data.Parameters;
using CompanyData.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyData.Web.Controllers
{
    public class ReportController : Controller
    {
        private IReportService reportService;
        private List<Report> Reports;
        private readonly string Separator = " ";
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<ActionResult> View(int Id)
        {
            var report = await reportService.GetReportById(Id);
            return View(report);
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
                                        int? pageNumber, DateTime? startDate=null, DateTime? endDate = null)
        {
            Reports = (await reportService.GetAllReports()).ToList();

            ViewData[SortingParameters.TableNameParam] = sortOrder == SortingParameters.TableNameAsc
                ? SortingParameters.TableNameDesc
                : SortingParameters.TableNameAsc;

            ViewData[SortingParameters.DateTimeParam] = sortOrder == SortingParameters.DateTimeAsc
                ? SortingParameters.DateTimeDesc
                : SortingParameters.DateTimeAsc;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData[SortingParameters.CurrentFilter] = searchString;
            ViewData[SortingParameters.CurrentStartDateTime] = startDate;
            ViewData[SortingParameters.CurrentEndDateTime] = endDate;

            await FilterByCompanyParameters(searchString, startDate, endDate);
            await OrderByCompanyParameters(sortOrder);
            
            int pageSize = 100;
            var result = PaginatedList<Report>.Create(Reports, pageNumber ?? 1, pageSize);
            return View(result);
        }

        private async Task FilterByCompanyParameters(string searchString, DateTime? startDate=null, DateTime? endDate=null)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var words = searchString.Split(Separator).ToList();
                List<Report> tmpList = new List<Report>();
                foreach (var item in words)
                {
                    tmpList.AddRange( Reports.Where(c => c.TableName.Contains(item) || c.KeyValues.Contains(item)
                    || (c.NewValues != null && c.NewValues.Contains(item)) || (c.OldValues != null && c.OldValues.Contains(item))).ToList());
                }
                Reports = tmpList.Distinct().ToList();
            }
            if(startDate != null)
            {
                Reports = Reports.Where(c => c.DateTime > startDate).ToList();
            }
            if (endDate != null)
            {
                Reports = Reports.Where(c => c.DateTime < endDate).ToList();
            }
        }

        private async Task OrderByCompanyParameters(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return;
            }
            switch (sortOrder)
            {
                case SortingParameters.TableNameDesc:
                    Reports = Reports.OrderByDescending(s => s.TableName).ToList();
                    break;
                case SortingParameters.TableNameAsc:
                    Reports = Reports.OrderBy(s => s.TableName).ToList();
                    break;
                case SortingParameters.DateTimeDesc:
                    Reports = Reports.OrderByDescending(s => s.DateTime).ToList();
                    break;
                case SortingParameters.DateTimeAsc:
                    Reports = Reports.OrderBy(s => s.DateTime).ToList();
                    break;
                default:
                    Reports = Reports.OrderBy(s => s.DateTime).ToList();
                    break;
            }
        }


    }
}