using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() {
            this.Companies = new HashSet<Company>();
            this.RefreshTokens = new HashSet<RefreshUserToken>();
        }
        public ApplicationUser(string userName) : base(userName) {
            this.Companies = new HashSet<Company>();
        }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<RefreshUserToken> RefreshTokens { get; set; }
    }
}