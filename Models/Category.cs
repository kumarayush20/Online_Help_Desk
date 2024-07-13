using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models
{
    [Table("Category")]
    public partial class Category
    {
        
        public Category() => Ticket = new HashSet<Ticket>();
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status{ get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
