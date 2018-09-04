using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs {
    public class RegisterDTO {
        [Required]
        [MaxLength (256)]
        public string Email { get; set; }

        [Required]
        [MaxLength (256)]
        [StringLength (100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}