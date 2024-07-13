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
    [Route("Period")]
    public class PeriodController : Controller
    {
        private readonly Onlinehelpdeskdb DbContext;
        public PeriodController(Onlinehelpdeskdb DbContext)
        {
            this.DbContext = DbContext;

        }
        //  [Authorize(Policy = "Administrator")]

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.period = DbContext.Period.ToList();
            return View("Index");
        }
        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            //var viewModel = new Period
            //{
            //    ColourOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "success", Text = "Success" },
            //    new SelectListItem { Value = "warning", Text = "Warning" },
            //    new SelectListItem { Value = "danger", Text = "Danger" },
            //    new SelectListItem { Value = "info", Text = "Info" }
            //}
            //};
            return View("Add", new Period());
           
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Period period)
        {
            try
            {
                DbContext.Period.Add(period);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";
            //    period.ColourOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "success", Text = "Success" },
            //    new SelectListItem { Value = "warning", Text = "Warning" },
            //    new SelectListItem { Value = "danger", Text = "Danger" },
            //    new SelectListItem { Value = "info", Text = "Info" }
            //};
                ViewBag.period = DbContext.Period.ToList();
                return View("Index");
            }
        }
        [HttpGet]
        [Route("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var period = DbContext.Period.Find(Id);
                DbContext.Period.Remove(period);
                DbContext.SaveChanges();
                ViewBag.msg = "Done";
                return RedirectToAction("Index");
            }
            catch
            {

                ViewBag.msg = "Failed";
                ViewBag.period = DbContext.Period.ToList();
                return View("Index");
            }
        }
        [HttpGet]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            
            
                var period = DbContext.Period.Find(Id);
                return View("Edit",period);
            
           
        }
        [HttpPost]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id, Period period)
        {

            try
            {
                DbContext.Entry(period).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";

                return View("Edit", period);
            }

            }

    }


}


