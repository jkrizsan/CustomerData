using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyData.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace CompanyData.Web.Controllers
{

    public class DataMapController : Controller
    {
        IDataMapService dataMapService;

        public DataMapController(IDataMapService dataMapService)
        {
            this.dataMapService = dataMapService;
        }

        // GET: DataMap
        public ActionResult Index()
        {
            return View();
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
            var data = dataMapService.GetAllCompanyData().ToList();
            return View(data);
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