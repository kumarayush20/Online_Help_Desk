using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models
{
    [Table("Period")]
    public partial class Period
    {
        public Period() { 
        Ticket=new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string? Colour { get; set; }
       // public IEnumerable<SelectListItem> ColourOptions { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
