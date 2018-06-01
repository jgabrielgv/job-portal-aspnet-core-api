using System;
using System.Collections.Generic;

namespace JobPortal.Core.Domain {
  public class Job {
    public Job () {
      this.Applications = new HashSet<Application>();
    }
    public int JobId { get; set; }
    public string Title { get; set; }

    public virtual Company Company { get; set; }
    public int CompanyId { get; set; }
    public virtual ICollection<Application> Applications { get; set; }
  }
}