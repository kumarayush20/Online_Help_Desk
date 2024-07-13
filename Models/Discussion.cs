using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models
{
    [Table("Discussion")]
    public partial class Discussion
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime?    CreateDate { get; set; }
        public int? TicketId { get; set; }
        public int  AccountId { get; set; }
        [NotMapped]
        public virtual Ticket? Ticket { get; set; }
        [NotMapped]
        public virtual Account? Account { get; set; }

    }
}
