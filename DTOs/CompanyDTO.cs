using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.DTOs
{
    public class CompanyDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
    }
}