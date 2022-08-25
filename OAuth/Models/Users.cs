using System.Collections.Generic;

namespace OAuth.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Role>? Roles { get; set; }
        public Users()
        {
            this.Roles = new HashSet<Role>();
        }
    }
}
