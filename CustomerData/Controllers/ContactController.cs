﻿using System;
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
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Create(Contact conatct)
        {
            try
            {

                contactService.Add(conatct);
                return RedirectToAction(ActionNames.Edit, ControllerNames.Company, new { Id = conatct.CompanyId });
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            var contact = contactService.GetContactById(id);
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contact contact)
        {
            try
            {
                contactService.SaveContact(contact);
                return RedirectToAction();
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int Id)
        {
            var contact = contactService.GetContactById(Id);
            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id, IFormCollection collection)
        {
            try
            {
                contactService.DeleteContact(Id);

                return RedirectToAction(ActionNames.Index, ControllerNames.DataMap);
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}