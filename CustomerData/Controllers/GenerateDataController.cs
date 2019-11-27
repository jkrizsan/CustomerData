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

        public GenerateDataController(IGenerateDataService generateDataService)
        {
            this.generateDataService = generateDataService;
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
    }
}