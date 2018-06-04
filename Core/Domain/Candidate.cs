using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Core.Domain
{
    public class Candidate
    {
        public Candidate()
        {
            this.Applications = new HashSet<Application>();
        }

        [Required]
        public virtual int CandidateId { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string FirstName { get; set; }
        [MaxLength(50)]
        public virtual string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string Email { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}