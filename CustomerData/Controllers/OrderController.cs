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
    public class OrderController : Controller
    {
        IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: Order/Create
        public ActionResult Create(int contactId)
        {
            var order = new Order() { ContactId = contactId };
            return View(order);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order)
        {
            try
            {

                await orderService.Create(order);
                return RedirectToAction(ActionNames.Edit, ControllerNames.Contact, new { Id = order.ContactId } );
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var order = await orderService.GetOrderById(id);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Order order)
        {
            try
            {
                await orderService.Update(order);
                return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var order = await orderService.GetOrderById(id);
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Order order)
        {
            try
            {

                await orderService.Delete(order);
                return RedirectToAction(ActionNames.Index, ControllerNames.DataMap);
            }
            catch
            {
                return View();
            }
        }
    }
}