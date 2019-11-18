using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyData.Data.DTOs;
using CompanyData.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyData.Web.Controllers
{
    public class GenerateDataController : Controller
    {
        private IGenerateDataService generateDataService;
        private GenerateDataDto generateData;

        public GenerateDataController(IGenerateDataService generateDataServic)
        {
            this.generateDataService = generateDataServic;
        }
        // GET: GenerateDataService
        public ActionResult Index()
        {
            var generateData = new GenerateDataDto();
            return View(generateData);
        }

        // GET: GenerateDataService/Details/5
        public ActionResult Details(int id)
        {
            var generateData = new GenerateDataDto();
            return View(generateData);
        }

        // GET: GenerateDataService/Create
        public ActionResult Create()
        {
            generateData = new GenerateDataDto();
            return View(generateData);
        }

        // POST: GenerateDataService/Create
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

        // GET: GenerateDataService/Edit/5
        public ActionResult Edit(int id)
        {
            var generateData = new GenerateDataDto();
            return View(generateData);
        }

        
        // POST: GenerateDataService/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, GenerateDataDto collection, string button)
        {
            try
            {
                if(button.Equals("Generate Data"))
                {
                    await generateDataService.GenerataData(collection);
                }
                else if(button.Equals("Delete Data"))
                {
                    await generateDataService.RemoveAllCompanyData();
                }
                
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return View();
            }
        }

        // GET: GenerateDataService/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GenerateDataService/Delete/5
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
        [HttpPost]
        public ActionResult Save(GenerateDataDto objSave)
        {

            ViewBag.Msg = "Details saved successfully.";
            return View();
        }
        [HttpPost]
        public ActionResult Draft(GenerateDataDto objDraft)
        {
            ViewBag.Msg = "Details saved as draft.";
            return View();
        }
    }
}