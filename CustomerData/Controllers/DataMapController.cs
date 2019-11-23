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
        IIndexCompany indexCompany;

        public DataMapController(IDataMapService dataMapService, IIndexCompany indexCompany)
        {
            this.dataMapService = dataMapService;
            this.indexCompany = indexCompany;
            indexCompany.Companys = dataMapService.GetAllCompanyData().ToList();
        }

        // GET: DataMap
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            
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

            OrderByCompanyParameters(sortOrder);
            FilterByCompanyParameters(searchString);
            int pageSize = 10;
            var result = await PaginatedList<Company>.CreateAsync(indexCompany.Companys, pageNumber ?? 1, pageSize);
            return View(result);
        }

        private void FilterByCompanyParameters(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return;
            }

            indexCompany.Companys = indexCompany.Companys.Where(c => c.Name.Contains(searchString)).ToList();
        }

        private void OrderByCompanyParameters(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return;
            }
            switch (sortOrder)
            {
                case SortingParameters.NameDesc:
                    indexCompany.Companys = indexCompany.Companys.OrderByDescending(s => s.Name).ToList();
                    break;
                case SortingParameters.NameAsc:
                    indexCompany.Companys = indexCompany.Companys.OrderBy(s => s.Name).ToList();
                    break;
                case SortingParameters.NumberOfContactsDesc:
                    indexCompany.Companys = indexCompany.Companys.OrderByDescending(s => s.NumberOfContacts).ToList();
                    break;
                case SortingParameters.NumberOfContactsAsc:
                    indexCompany.Companys = indexCompany.Companys.OrderBy(s => s.NumberOfContacts).ToList();
                    break;
                case SortingParameters.NumberOfOrdersDesc:
                    indexCompany.Companys = indexCompany.Companys.OrderByDescending(s => s.NumberOfOrders).ToList();
                    break;
                case SortingParameters.NumberOfOrdersAsc:
                    indexCompany.Companys = indexCompany.Companys.OrderBy(s => s.NumberOfOrders).ToList();
                    break;
                case SortingParameters.IncomeDesc:
                    indexCompany.Companys = indexCompany.Companys.OrderByDescending(s => s.TotalIncome).ToList();
                    break;
                case SortingParameters.IncomeAsc:
                    indexCompany.Companys = indexCompany.Companys.OrderBy(s => s.TotalIncome).ToList();
                    break;
                default:
                    break;
            }
        }

        public ActionResult Edit()
        {
            indexCompany.Companys = dataMapService.GetAllCompanyData().ToList();
            return View(indexCompany);
        }

        // GET: DataMap/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DataMap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataMap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DataMap/Edit/5
        public ActionResult Edit(int id)
        {
             //indexCompany.OnGetAsync();
            indexCompany.Companys = dataMapService.GetAllCompanyData().ToList();
            return View(indexCompany);
        }

        // POST: DataMap/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DataMap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DataMap/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}