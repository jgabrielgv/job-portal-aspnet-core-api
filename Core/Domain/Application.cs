using System;

namespace JobPortal.Core.Domain
{
    public class Application
    {
        // public virtual int ApplicationId { get; set; }
        public virtual DateTimeOffset DateCreated { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual int CandidateId { get; set; }
        public virtual Job Job { get; set; }
        public virtual int JobId { get; set; }
    }
}