using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onlinehelpdesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using Onlinehelpdesk.Data;
using Onlinehelpdesk.Securiity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Mail;



namespace Onlinehelpdesk.Controllers
{
    //[Route("Login")]
    public class Login : Controller
    {
        ///   private readonly SignInManager<AppUser> signInManager;
        //    private readonly UserManager<AppUser> userManager;
        //    private readonly ILogger<AppUser> logger;
        private readonly Onlinehelpdeskdb DbContext;
        private readonly SecurityManager SecurityManager;

        public Login(Onlinehelpdeskdb DbContext, SecurityManager SecurityManager)
        {
            this.DbContext = DbContext;
            this.SecurityManager= SecurityManager;

        }
        //public Login(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<AppUser> logger)
        //{
        //    this.signInManager = signInManager;
        //    this.userManager = userManager;
        //    this.logger = logger;


        //}
        
        [Route("Index")]
        [Route("")]
        [Route("~/ ")]
        [HttpGet]
        public IActionResult Index()
        {

            //string hash = BCrypt.Net.BCrypt.HashPassword("abc", BCrypt.Net.BCrypt.GenerateSalt());
            //ViewData["Hash"] = hash;
            return View();
        }

        public IActionResult Ind()
        {
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            return Json(userRoles);
        }
        //public string HashPassword(string password)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}
        [HttpPost]
        [Route("Process")]
        public IActionResult Process(string UserName, string Password)
        {
            var account = Check(UserName, Password);
            if (account != null)
            {
                Task task = SecurityManager.SignIn(HttpContext, account);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.error = "Invalid";
                return View("Index");
            }
        }
        [Route("SignOut")]
       public async Task<IActionResult> SignOut()
{
            SecurityManager SecurityManager = new SecurityManager();
            // Inject SecurityManager via constructor injection
            await SecurityManager.SignOut(HttpContext);
    return RedirectToAction("Index");
}
        [HttpGet]
        [Route("Login/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private Models.Account Check(string username, string password)
        {
            var account = DbContext.Account.FirstOrDefault(a => a.UserName.Equals(username));
            //var account = DbContext.Account.SingleOrDefault(a => a.UserName.Equals(username));

            if (account != null) // Check if account exists
           {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account; // Return the account if password matches
                }
            }

           return null; // Return null if account doesn't exist or password doesn't match
        }
        //public IActionResult ResetPassword(string userEmail)
        //{
        //    // Generate a new random password (you can customize this as needed)
        //    string newPassword = GenerateRandomPassword();

        //    // Hash the new password
        //    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt());

        //    // Update the user's password in the database
        //    var user = DbContext.Users.FirstOrDefault(u => u.Email == userEmail);
        //    if (user != null)
        //    {
        //        user.PasswordHash = hashedPassword;
        //        DbContext.SaveChanges();

        //        // Notify the user about the password reset, optionally send the new password via email
        //        //SendPasswordResetNotification(user.Email, newPassword);

        //        return RedirectToAction("Login", "Account"); // Redirect to login page or notify success
        //    }

        //    // Handle case where user with given email is not found
        //    return RedirectToAction("ForgotPassword", "Account"); // Redirect to forgot password page
        //}

        //// Generate a random password method
        //private string GenerateRandomPassword()
        //{
        //    // Generate a random password with desired complexity (this is a basic example)
        //    string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=";
        //    Random random = new Random();
        //    string password = new string(Enumerable.Repeat(allowedChars, 10)
        //        .Select(s => s[random.Next(s.Length)]).ToArray());

        //    return password;
        //}
        //[HttpGet]
        //[Route("ResetPassword")]
        //public IActionResult ResetPassword(string token)
        //{
        //    if (string.IsNullOrWhiteSpace(token))
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var model = new ResetPassword { Token = token };
        //    return View(model);
        //}
        //[HttpPost]
        //[Route("ResetPassword")]
        //public IActionResult ResetPassword(ResetPassword model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = DbContext.Users.SingleOrDefault(u => u.Email == model.Email);
        //    if (user == null)
        //    {
        //        // User not found
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }

        //    user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, BCrypt.Net.BCrypt.GenerateSalt());
        //    DbContext.SaveChanges();

        //    return RedirectToAction("Index", "Account");
        //}
        //public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = DbContext.Users.SingleOrDefault(u => u.Email == model.Email);
        //    if (user == null)
        //    {
        //        // User not found
        //        return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    var token = Guid.NewGuid().ToString();
        //    user.PasswordResetToken = token;
        //    user.PasswordResetTokenExpiration = DateTime.UtcNow.AddHours(1);
        //    DbContext.SaveChanges();

        //    var callback = Url.Action("ResetPassword", "Account", new { token }, protocol: HttpContext.Request.Scheme);

        //    // Here you should send the email with the callback link
        //    SendPasswordResetEmail(model.Email, callback);

        //    return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //}
        //public void SendPasswordResetEmail(string email, string callbackUrl)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("YourAppName", "no-reply@yourapp.com"));
        //    message.To.Add(new MailboxAddress(email));
        //    message.Subject = "Reset Password";
        //    message.Body = new TextPart("html")
        //    {
        //        Text = $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>."
        //    };

        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.your-email-provider.com", 587, false);
        //        client.Authenticate("your-email", "your-email-password");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}
    }
}





