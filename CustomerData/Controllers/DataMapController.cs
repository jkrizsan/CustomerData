using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyData.Data.Models;
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
        }

        // GET: DataMap
        public ActionResult Index()
        {
            indexCompany.Companys = dataMapService.GetAllCompanyData().ToList();
            return View(indexCompany);
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