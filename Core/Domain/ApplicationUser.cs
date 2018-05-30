using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() {
            this.Companies = new HashSet<Company>();
        }
        public ApplicationUser(string userName) : base(userName) {
            this.Companies = new HashSet<Company>();
        }

        public virtual ICollection<Company> Companies { get; set; }
    }
}