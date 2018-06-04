using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.ViewModels
{
    public class JobViewModel
    {
        [Required]
        public virtual int JobId { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string Title { get; set; }
        [Required]
        public virtual int? CompanyId { get; set; }
    }
}