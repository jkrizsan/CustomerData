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
    public class CompanyController : Controller
    {
        private ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        // GET: Company
        public ActionResult Index()
        {
            return View(new Company());
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View(new Company());
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            var company = new Company();
            return View(company);
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Company company)
        {
            try
            {
                // TODO: Add insert logic here
                int Id = await companyService.Create(company);
                return RedirectToAction(ActionNames.Edit, ControllerNames.Company, new { Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var company = await companyService.GetCompanyById(id);
            return View(company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Company company)
        {
            try
            {
                // TODO: Add update logic here
                await companyService.Update(company);
                return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var company = await companyService.GetCompanyById(id);
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Company company)
        {
            try
            {
                await companyService.Delete(company);
                return RedirectToAction(ActionNames.Index, ControllerNames.DataMap);
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}