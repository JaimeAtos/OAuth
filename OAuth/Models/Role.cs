using System.Collections.Generic;

namespace OAuth.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Users>? Users { get; set; }

        public Role()
        {
            this.Users = new HashSet<Users>();        
        }
    }
}
