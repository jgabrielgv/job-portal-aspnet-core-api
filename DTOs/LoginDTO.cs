using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.DTOs {
    public class LoginDTO
        {
            [Required]
            [MaxLength(256)]
            public string Email { get; set; }

            [Required]
            [MaxLength(256)]
            public string Password { get; set; }

        }
}