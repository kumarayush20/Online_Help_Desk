using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onlinehelpdesk.Data;
using System.Security;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
//[Authorize(Roles = "Administrator,Supporter,Employee")]
namespace Onlinehelpdesk.Controllers
{
    // [Authorize(Roles = "Administrator,Support,Employee")]
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly Onlinehelpdeskdb DbContext;

        public DashboardController(Onlinehelpdeskdb dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {


            return View();
        }

        [HttpGet]
        [Route("Test")] // Define the route for the Test action
        public IActionResult Test()
        {
            var userRoles = User.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();

            return View(userRoles);
        }
    }
    //public IActionResult Index(string returnUrl = null)
    //{
    //    ViewData["ReturnUrl"] = returnUrl;
    //    return View();
    //}
}

