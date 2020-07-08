using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<ExternalLogin> ExternalLogins { get; set; }
    }
}
