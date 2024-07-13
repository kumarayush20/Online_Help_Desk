using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Onlinehelpdesk.Data;
using Onlinehelpdesk.Models;

using System.Security.Claims;

namespace Onlinehelpdesk.Controllers
{
    [Route("Status")]
    public class StatusController : Controller
    {
        private readonly Onlinehelpdeskdb DbContext;
        public StatusController(Onlinehelpdeskdb DbContext)
        {
            this.DbContext = DbContext;

        }
        //  [Authorize(Policy = "Administrator")]

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.status = DbContext.Status.ToList();
            return View("Index");
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View("Add", new Status());
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Status status)
        {
            try
            {
                DbContext.Status.Add(status);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";
                ViewBag.status = DbContext.Status.ToList();
                return View("Index");
            }
        }
        [HttpGet]
        [Route("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var status = DbContext.Status.Find(Id);
                DbContext.Status.Remove(status);
                DbContext.SaveChanges();
                ViewBag.msg = "Done";
                return RedirectToAction("Index");
            }
            catch
            {

                ViewBag.msg = "Failed";
                ViewBag.status = DbContext.Status.ToList();
                return View("Index");
            }
        }
        [HttpGet]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            
            
                var status = DbContext.Status.Find(Id);
                return View("Edit",status);
            
           
        }
        [HttpPost]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id, Status status)
        {

            try
            {
                DbContext.Entry(status).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";

                return View("Edit", status);
            }

            }

    }


}


