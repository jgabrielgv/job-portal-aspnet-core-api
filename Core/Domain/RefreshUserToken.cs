using System;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Core.Domain {

  public class RefreshUserToken
  {
    [Required]
    [MaxLength(50)]
    public string RefreshUserTokenId { get; set; }
    [Required]
    [MaxLength(50)]
    public string RefreshToken { get; set; }

    public virtual ApplicationUser User { get; set; }
    [Required]
    [MaxLength(450)]
    public string UserId { get; set; }
  }

}