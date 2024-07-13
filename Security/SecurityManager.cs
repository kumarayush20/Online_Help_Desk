using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Onlinehelpdesk.Models;
using System.Security.Claims;

namespace Onlinehelpdesk.Securiity
{
    public class SecurityManager
    {
        public async Task SignIn(HttpContext httpContext, Account account)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(GetUserClaims(account), CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            foreach (var claim in claimsPrincipal.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
        }

        private IEnumerable<Claim> GetUserClaims(Account account)
        {
            List<Claim> claims = new List<Claim>();

            // Add username claim
            if (!string.IsNullOrEmpty(account.UserName))
            {
                claims.Add(new Claim(ClaimTypes.Name, account.UserName));
            }

            // Add role claim
            if (account.Role != null && !string.IsNullOrEmpty(account.Role.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, account.Role.Name));
            }

            return claims;
        }
        public async Task SignOut(HttpContext httpContext)
        {
            // Your sign out logic here
            await Task.CompletedTask; // Example placeholder
        }
        //private IEnumerable<Claim> getUserClaim(Account account)
        //{
        //    if (account == null)
        //    {
        //        throw new ArgumentNullException(nameof(account));
        //    }
        //    List<Claim> claims = new List<Claim>();
        //    claims.Add(new Claim(ClaimTypes.Name,account.UserName)); 
        //    //claims.Add(new Claim(ClaimTypes.Role, account.Role.Name));

        //    return claims;
        //}

        
    }
}
