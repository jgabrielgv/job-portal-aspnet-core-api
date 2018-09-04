using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Domain
{
    public class Application
    {
        // public virtual int ApplicationId { get; set; }
        [Required]
        public virtual DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public virtual Candidate Candidate { get; set; }
        public virtual int CandidateId { get; set; }
        public virtual Job Job { get; set; }
        public virtual int JobId { get; set; }
    }
}