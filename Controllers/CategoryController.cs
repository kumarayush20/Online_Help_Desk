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
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly Onlinehelpdeskdb DbContext;
        public CategoryController(Onlinehelpdeskdb DbContext)
        {
            this.DbContext = DbContext;

        }
        //  [Authorize(Policy = "Administrator")]

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.category = DbContext.Category.ToList();
            return View("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add", new Category());
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            try
            {
                DbContext.Category.Add(category);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";
                ViewBag.category = DbContext.Category.ToList();
                return View("Index");
            }
        }
        [HttpGet]
        [Route("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var category = DbContext.Category.Find(Id);
                DbContext.Category.Remove(category);
                DbContext.SaveChanges();
                ViewBag.msg = "Done";
                return RedirectToAction("Index");
            }
            catch
            {

                ViewBag.msg = "Failed";
                ViewBag.category = DbContext.Category.ToList();
                return View("Index");
            }
        }
        [HttpGet]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            
            
                var category = DbContext.Category.Find(Id);
                return View("Edit",category);
            
           
        }
        [HttpPost]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id,Category category)
        {

            try
            {
                DbContext.Entry(category).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";

                return View("Edit", category);
            }

            }

    }


}


