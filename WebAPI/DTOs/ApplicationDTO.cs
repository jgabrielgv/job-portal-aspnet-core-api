using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class ApplicationDTO
    {
        [Required]
        public virtual int? JobId { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string FirstName { get; set; }
        [MaxLength(50)]
        public virtual string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual string Email { get; set; }
    }
}