using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace JobPortal.Core.Domain
{
    public class Company
    {
        public Company()
        {
            this.Jobs = new HashSet<Job>();
        }

        [Required]
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser User { get; set; }

        [JsonIgnore]
        [Required]
        public string UserId { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}