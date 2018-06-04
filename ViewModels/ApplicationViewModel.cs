using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.ViewModels
{
    public class ApplicationViewModel
    {
        [Required]
        public virtual int? CandidateId { get; set; }
        [Required]
        public virtual int? JobId { get; set; }
    }
}