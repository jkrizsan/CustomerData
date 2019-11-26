using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyData.Data.Models;
using CompanyData.Data.Parameters;
using CompanyData.Services;
using CompanyData.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyData.Web.Controllers
{

    public class DataMapController : Controller
    {
        IDataMapService dataMapService;
        List<Company> Companies { get; set; }

        public DataMapController(IDataMapService dataMapService)
        {
            this.dataMapService = dataMapService;
        }

        // GET: DataMap
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            Companies = (await dataMapService.GetAllCompanyData(false)).ToList();

            ViewData[SortingParameters.NameParam] = sortOrder == SortingParameters.NameAsc
                ? SortingParameters.NameDesc 
                : SortingParameters.NameAsc;

            ViewData[SortingParameters.NumberOfContactsParam] = sortOrder == SortingParameters.NumberOfContactsAsc
                ? SortingParameters.NumberOfContactsDesc
                : SortingParameters.NumberOfContactsAsc;

            ViewData[SortingParameters.NumberOfOrdersParam] = sortOrder == SortingParameters.NumberOfOrdersAsc
                ? SortingParameters.NumberOfOrdersDesc
                : SortingParameters.NumberOfOrdersAsc;

            ViewData[SortingParameters.IncomeParam] = sortOrder == SortingParameters.IncomeAsc
                ? SortingParameters.IncomeDesc
                : SortingParameters.IncomeAsc;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData[SortingParameters.CurrentFilter] = searchString;

            await OrderByCompanyParameters(sortOrder);
            await FilterByCompanyParameters(searchString);
            int pageSize = 10;
            var result = PaginatedList<Company>.Create(Companies, pageNumber ?? 1, pageSize);
            return View(result);
        }

        private async Task FilterByCompanyParameters(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return;
            }

            Companies = Companies.Where(c => c.Name.Contains(searchString)).ToList();
        }

        private async Task OrderByCompanyParameters(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return;
            }
            switch (sortOrder)
            {
                case SortingParameters.NameDesc:
                    Companies = Companies.OrderByDescending(s => s.Name).ToList();
                    break;
                case SortingParameters.NameAsc:
                    Companies = Companies.OrderBy(s => s.Name).ToList();
                    break;
                case SortingParameters.NumberOfContactsDesc:
                    Companies = Companies.OrderByDescending(s => s.NumberOfContacts).ToList();
                    break;
                case SortingParameters.NumberOfContactsAsc:
                    Companies = Companies.OrderBy(s => s.NumberOfContacts).ToList();
                    break;
                case SortingParameters.NumberOfOrdersDesc:
                    Companies = Companies.OrderByDescending(s => s.NumberOfOrders).ToList();
                    break;
                case SortingParameters.NumberOfOrdersAsc:
                    Companies = Companies.OrderBy(s => s.NumberOfOrders).ToList();
                    break;
                case SortingParameters.IncomeDesc:
                    Companies = Companies.OrderByDescending(s => s.TotalIncome).ToList();
                    break;
                case SortingParameters.IncomeAsc:
                    Companies = Companies.OrderBy(s => s.TotalIncome).ToList();
                    break;
                default:
                    break;
            }
        }

        public async Task<ActionResult> Edit()
        {
            Companies = (await dataMapService.GetAllCompanyData()).ToList();
            return View(Companies);
        }

        // GET: DataMap/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
             //indexCompany.OnGetAsync();
            Companies = (await dataMapService.GetAllCompanyData()).ToList();
            return View(Companies);
        }

    }
}