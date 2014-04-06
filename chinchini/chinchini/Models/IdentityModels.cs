using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace chinchini.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public float Balance { get; set; }

        public virtual Status Status { get; set; }
    }

    
}