using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Onlinehelpdesk.Data;
using Onlinehelpdesk.Helper;
using Onlinehelpdesk.Models;
using Onlinehelpdesk.Models.ViewModel;
using System.Security.Claims;

namespace Onlinehelpdesk.Controllers
{
   // [Authorize(Roles = "Adminisrator")]
    public class TicketController : Controller
    {
        private readonly Onlinehelpdeskdb DbContext;
        public TicketController(Onlinehelpdeskdb DbContext)
        {
            this.DbContext = DbContext;

        }

        [HttpGet]
        [Route("Send")]
        public IActionResult Send()
        {
            var ticketViewModel = new TicketViewModel();
            ticketViewModel.Ticket = new Ticket();

            var categories = DbContext.Category.Where(r => r.Status).ToList();
            ticketViewModel.Category = new SelectList(categories, "Id", "Name");
            var statuses = DbContext.Status.Where(r => r.Status1).ToList();
            ticketViewModel.Status = new SelectList(statuses, "Id", "Name");
            var periods = DbContext.Period.Where(r => r.Status).ToList();
            ticketViewModel.Period = new SelectList(periods, "Id", "Name");

            return View(ticketViewModel);
        }
        [HttpPost]
        [Route("Send")]
        public IActionResult Send(TicketViewModel ticketViewModel)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    TempData["Msg"] = "Failed: Username not found";
                    return RedirectToAction("History");
                }
                var account = DbContext.Account.FirstOrDefault(a => a.UserName.Equals(username));
                //var account = DbContext.Account.SingleOrDefault(a => a.UserName == username);
                if (account == null)
                {
                    TempData["Msg"] = "Failed: Account not found";
                    return RedirectToAction("History");
                }
                //if (ModelState.IsValid)
                //{
                    ticketViewModel.Ticket.CreateDate = DateTime.Now;
                    ticketViewModel.Ticket.EmployeeId = account.Id;

                    DbContext.Ticket.Add(ticketViewModel.Ticket);
                    DbContext.SaveChanges();

                TempData["Msg"] = "Done";
              //  }



               
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Failed: " + ex.Message;
                return RedirectToAction("Send");
            }

            return RedirectToAction("Send");
        }
        //[HttpPost]
        //[Route("Send")]
        //public IActionResult Send(TicketViewModel ticketViewModel)
        //{
        //    try
        //    {
        //        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        //        if (string.IsNullOrEmpty(username))
        //        {
        //            TempData["Msg"] = "Failed: Username not found";
        //            return RedirectToAction("History");
        //        }

        //        var account = DbContext.Account.SingleOrDefault(a => a.UserName == username);
        //        if (account == null)
        //        {
        //            TempData["Msg"] = "Failed: Account not found";
        //            return RedirectToAction("History");
        //        }

        //        //if (ModelState.IsValid)
        //        //{
        //        ticketViewModel.Ticket.CreateDate = DateTime.Now;
        //           // ticketViewModel.Ticket.EmployeeId = Account.Id;
        //           \ ticketViewModel.Ticket.EmployeeId = account.Id;

        //            DbContext.Ticket.Add(ticketViewModel.Ticket);
        //            DbContext.SaveChanges();

        //            ViewBag.msg = "Done";
        //        //}
        //        //else
        //        //{
        //        //    // Handle validation errors
        //        //    TempData["Msg"] = "Failed: Invalid data";
        //        //    return View("Send", ticketViewModel);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.msg = "Failed: " + ex.Message;
        //    }

        //    return RedirectToAction("Send");
        //}

        [HttpGet]
        [Route("History")]
        public IActionResult History()
        {
            var tickets = DbContext.Ticket.Include(t => t.Period)
                                     .Include(t => t.Status)
                                     .Include(t => t.Category)
                                    .ToList();

            //return View("History");
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var account = DbContext.Account.FirstOrDefault(a => a.UserName.Equals(username));
            //var account=DbContext.Account.SingleOrDefault(a=>a.UserName.Equals(username));
            ViewBag.ticket=DbContext.Ticket.OrderByDescending(t=>t.Id).Where(t=>t.EmployeeId==account.Id).ToList();
            return View("History");
        }
        //[Authorize(Roles="Administrator")]
        [HttpGet]
        public IActionResult List()
        {
            ViewBag.Ticket = DbContext.Ticket
    .Include(t => t.Employee)
    .Include(t => t.Status)
    .Include(t => t.Period)
    .Include(t => t.Category)
    .Where(t => t.Supporter == null)
    .OrderByDescending(t => t.Id)
    .ToList();
            // ViewBag.ticket = DbContext.Ticket.OrderByDescending(t => t.Id).ToList();

            
            return View("List");
        }
        //[Authorize(Roles="Administrator")]
        [HttpGet]
        [Route("Assign")]
        public IActionResult Assign()
        {

            ViewBag.ticket = DbContext.Ticket.Where(t => t.Supporter == null).OrderByDescending((t) => t.Id).ToList();
            //    List<Ticket> tickets = DbContext.Ticket
            //.Include(t => t.Status)
            //.Include(t => t.Period)
            //.Where(t => t.Supporter == null)
            //.OrderByDescending(t => t.Id)
            //.ToList();
            return View("Assign");
        }
        // [Authorize(Roles ="Administrator,Supporter,Employee")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var ticket = DbContext.Ticket
                .Include(t => t.Employee)
                .Include(t => t.Status)
                .Include(t => t.Period)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            ViewBag.ticket = ticket;
            return View(ticket);
        }

    }
}
