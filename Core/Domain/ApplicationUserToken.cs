using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Core.Domain
{
    [Obsolete]
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        [Required]
        public string GrantType { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}