using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models
{
    [Table("Ticket")]
    public partial class Ticket
    {
        public Ticket() {
            Discussions = new HashSet<Discussion>();
            Photo = new HashSet<Photo>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public int PeriodId { get; set; }
        public int EmployeeId { get; set; }
        public int? SupporterId { get; set; }
       // [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //[ForeignKey("PeriodId")]
        public virtual Period Period { get; set; }

      //  [ForeignKey("EmployeeId")]
        public virtual Account Employee { get; set; }

        //[ForeignKey("SupporterId")]
        public virtual Account Supporter { get; set; }

        // [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }

    }
}
