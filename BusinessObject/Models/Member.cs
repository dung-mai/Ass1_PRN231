using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    public class Member : IdentityUser<int>
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        [StringLength(15)]
        public string FirstName { get; set; } = null!;
        [StringLength(15)]
        public string LastName { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }

    }
}
