using System;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Core.Domain
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public DateTimeOffset DateCreated { get; set; }
    }
}