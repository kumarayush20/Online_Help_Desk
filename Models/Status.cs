using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models
{
    [Table("Status")]
    public partial class Status
    {
        public Status() { 
        Ticket=new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string? Colour { get; set; }
        public string Name { get; set; }
        //public bool Status1 { get; set; }

        // This property maps to the VARCHAR column in the database
        public bool Status1 { get; set; }

        //// This property will be used in the application
        //[NotMapped]
        //public bool IsStatus1
        //{
        //    get => Status1 == "true";
        //    set => Status1 = value ? "true" : "false";
        //}
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
