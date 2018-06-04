using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.DTOs
{
    public class ApplicationDTO
    {
        [Required]
        public virtual int? CandidateId { get; set; }
        [Required]
        public virtual int? JobId { get; set; }
    }
}