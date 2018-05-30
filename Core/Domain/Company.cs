using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JobPortal.Core.Domain
{
    public class Company
    {
        public Company()
        {
            this.Jobs = new HashSet<Job>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}