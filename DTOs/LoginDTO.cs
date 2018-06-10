using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.DTOs {
    public class LoginDTO {
        [MaxLength (256)]
        public string Email { get; set; }

        [MinLength (6)]
        [MaxLength (256)]
        public string Password { get; set; }

        [Required]
        public string Grant_Type { get; set; }
        public string Refresh_Token { get; set; }
    }
}