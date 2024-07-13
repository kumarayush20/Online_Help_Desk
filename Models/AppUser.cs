using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Onlinehelpdesk.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        override public string? Email { get; set; }
    }
}
