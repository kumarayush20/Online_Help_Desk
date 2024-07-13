using Microsoft.AspNetCore.Mvc.Rendering;

namespace Onlinehelpdesk.Models.ViewModel
{

    public class AccountViewModel
    {
        public Account Account { get; set; }
        public SelectList Role { get; set; }  // Holds the SelectList for roles
        public int SelectedRoleId { get; set; }  // Holds the selected role ID
        public bool Status { get; set; }
    }
}
