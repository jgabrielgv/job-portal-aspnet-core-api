using System.Collections.Generic;

namespace JobPortal.Core.Domain
{
    public class Candidate
    {
        public Candidate()
        {
            this.Applications = new HashSet<Application>();
        }

        public virtual int CandidateId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}