using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace JobPortal.Core.Domain {
  public class Job {
    public Job () {
      this.Applications = new HashSet<Application>();
    }
    [Required]
    public int JobId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Title { get; set; }

    public virtual Company Company { get; set; }

    [Required]
    public int CompanyId { get; set; }

    public virtual ICollection<Application> Applications { get; set; }
  }
}