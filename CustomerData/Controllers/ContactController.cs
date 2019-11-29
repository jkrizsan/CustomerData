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
    public class ContactController : Controller
    {
        private IContactService contactService;
        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        // GET: Contact/Create
        public ActionResult Create(int companyId)
        {
            var contact = new Contact() { CompanyId = companyId };
            return View(contact);
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact conatct)
        {
            try
            {

                var contactId = await contactService.Create(conatct);
                return RedirectToAction(ActionNames.Edit, ControllerNames.Contact, new { Id = contactId });
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Contact/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var contact = await contactService.GetContactById(id);
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Contact contact)
        {
            try
            {
                await contactService.Update(contact);
                return RedirectToAction();
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Contact/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            var contact = await contactService.GetContactById(Id);
            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, Contact contact)
        {
            try
            {
                await contactService.Delete(contact);

                return RedirectToAction(ActionNames.Index, ControllerNames.DataMap);
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}