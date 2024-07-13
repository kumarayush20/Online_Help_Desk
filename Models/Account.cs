using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models
{
    [Table("Account")]
    public partial class Account
    {
        public Account() {
            Discussion = new HashSet<Discussion>();
            TicketEmployee= new HashSet<Ticket>();
            TicketSupporter= new HashSet<Ticket>();
        }
        public  int Id { get; set; }
        public string UserName { get; set; }
        public string  Password{ get; set; }
        public string FullName { get; set; }
        
         public bool Status  { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public  Role Role { get; set; }
        public virtual ICollection<Discussion> Discussion { get; set; }
        [NotMapped]
        public virtual ICollection<Ticket> TicketEmployee { get; set; }
        [NotMapped]
        public virtual ICollection<Ticket> TicketSupporter { get; set; }
    }
}
