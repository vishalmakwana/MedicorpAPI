
using Microsoft.AspNetCore.Identity;

namespace Medicorp.Core.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
