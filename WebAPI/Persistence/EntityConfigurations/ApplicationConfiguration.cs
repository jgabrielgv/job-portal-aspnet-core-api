using System;
using WebAPI.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Persistence.EntityConfigurations
{
  public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
  {
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.HasKey(p => new { p.CandidateId, p.JobId });

        builder.Property(p => p.DateCreated)
        .HasDefaultValue(DateTimeOffset.Now)
        .IsRequired();
        builder.Property(p => p.CandidateId)
        .IsRequired();
        builder.Property(p => p.JobId)
        .IsRequired();

        builder.HasOne(p => p.Candidate)
        .WithMany(p => p.Applications)
        .HasForeignKey(p => p.CandidateId);

        builder.HasOne(p => p.Job)
        .WithMany(p => p.Applications)
        .HasForeignKey(p => p.JobId);

        builder.ToTable("Application");
    }
  }
}