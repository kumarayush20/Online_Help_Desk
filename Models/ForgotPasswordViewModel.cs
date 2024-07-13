using System.ComponentModel.DataAnnotations;

namespace Onlinehelpdesk.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
