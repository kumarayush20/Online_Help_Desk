using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Onlinehelpdesk.Data;
using Onlinehelpdesk.Models;
using Onlinehelpdesk.Models.ViewModel;
using System.Security.Claims;

namespace Onlinehelpdesk.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly Onlinehelpdeskdb DbContext;
        public AccountController(Onlinehelpdeskdb DbContext)
        {
            this.DbContext = DbContext;

        }
        //  [Authorize("Roles = Administrator")]
    
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var accounts = DbContext.Account.Include(a => a.Role).ToList(); // Ensure you are including the Role navigation property
            ViewBag.Account = accounts;
            return View();
            //var accounts = DbContext.Account.Include(a => a.Role).ToList();
            //return View(accounts);
            // ViewBag.accounts=DbContext.Account.Where(a=>a.RoleId!=1).ToList();

        }
        //[Authorize(Policy = "RequireAdministratorRole")]
        // [Authorize("Roles = Administrator")]
        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {

            var accountViewModel = new AccountViewModel();
            accountViewModel.Account = new Account();


            // Populate roles, excluding the role with ID 1 (if necessary)
            var roles = DbContext.Role.Where(r => r.Id != 1).ToList();
            accountViewModel.Role = new SelectList(roles, "Id", "Name");

            return View("Add", accountViewModel);
        }

        //[HttpPost]
        //[Route("Add")]
        //public IActionResult Add(AccountViewModel accountViewModel)
        //{
        //    try
        //    {
        //        // Hash the password
        //        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(accountViewModel.Account.Password);
        //        accountViewModel.Account.Password = hashedPassword;

        //        DbContext.Add(accountViewModel.Account);
        //        DbContext.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    catch
        //    {
        //        ViewBag.msg = "Failed";
        //        return View("Add", accountViewModel);
        //    }
        //}
    
    [HttpPost]
    [Route("Add")]
    public IActionResult Add(AccountViewModel accountViewModel)
    {
        try
        {
                accountViewModel.Account.Password=BCrypt.Net.BCrypt.HashPassword(accountViewModel.Account.Password,BCrypt.Net.BCrypt.GenerateSalt());
            DbContext.Add(accountViewModel.Account);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        catch
        {
            ViewBag.msg = "Failed";
            return View("Add");
        }
    }
    [Route("Delete/{Id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var account=DbContext.Account.SingleOrDefault(a=>a.Id==id && a.RoleId!=1);
                DbContext.Account.Remove(account);
                DbContext.SaveChanges();
                ViewBag.msg = "Done";
            }
            catch
            {
                ViewBag.msg = "Failed";
               
            }
            ViewBag.accounts = DbContext.Account.Where(a => a.RoleId != 1).ToList();
            return View("Index");
        }
        //[Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            var accountViewModel = new AccountViewModel();

            accountViewModel.Account =  DbContext.Account.Find(Id);
            var roles = DbContext.Role.Where(r => r.Id != 1).ToList();
            accountViewModel.Role = new SelectList(roles, "Id", "Name");

            return View("Edit", accountViewModel);
        }
        //[Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        [Route("Edit/{Id}")]
        public IActionResult Edit(int Id,AccountViewModel accountViewModel)
        {
            try
            {
                var password=DbContext.Account.AsNoTracking().SingleOrDefault(a=>a.Id==Id).Password;
                if (!string.IsNullOrEmpty(accountViewModel.Account.Password))
                {
                    password=BCrypt.Net.BCrypt.HashPassword(accountViewModel.Account.Password, BCrypt.Net.BCrypt.GenerateSalt());
                }
                accountViewModel.Account.Password=password;

                    DbContext.Entry(accountViewModel.Account).State =EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.msg = "Failed";
                return View("Edit", accountViewModel);
            }
        }
      //  [Authorize(Roles = "Administrator,Support,Employee")]
       
        [HttpPost]
        [Route("Profile")]
        public IActionResult Profile(Account account)
        {
            var username = User.FindFirst(ClaimTypes.Name).Value;
            var currentAccount = DbContext.Account.SingleOrDefault(a => a.UserName.Equals(username));

            if (currentAccount != null)
            {
                currentAccount.UserName = account.UserName;
               if (!string.IsNullOrEmpty(account.Password))
                {
                    currentAccount.Password = BCrypt.Net.BCrypt.HashPassword(account.Password, BCrypt.Net.BCrypt.GenerateSalt());
               }
                currentAccount.FullName = account.FullName;
                currentAccount.Email = account.Email;

                DbContext.SaveChanges(); // Save changes to the database
                ViewBag.msg = "Done";
            }

            return View("Profile", currentAccount);
        }
      //  [Authorize(Roles = "Administrator,Support,Employee")]
        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile()
        {
            var userNameClaim = User.FindFirst(ClaimTypes.Name);
                if (userNameClaim == null)
                {
            
                    return RedirectToAction("Login", "Account");
                }

                var username = userNameClaim.Value;

            
                var account = DbContext.Account.SingleOrDefault(a => a.UserName.Equals(username));

                if (account == null)
                {
                    
                   return RedirectToAction("Error", "Home");
                }

               return View("Profile",account);
                }
        }

    


}
  




