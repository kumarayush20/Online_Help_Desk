using System.ComponentModel.DataAnnotations.Schema;

namespace Onlinehelpdesk.Models

{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Account=new HashSet<Account>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Account> Account { get; set; }
    }
}
