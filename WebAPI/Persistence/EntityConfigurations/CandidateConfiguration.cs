using System;
using WebAPI.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Persistence.EntityConfigurations
{
  public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
  {
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(p => p.CandidateId);
        builder.HasIndex(p => p.Email)
        .IsUnique();

        builder.Property(p => p.FirstName)
        .HasMaxLength(50)
        .IsRequired();
        builder.Property(p => p.LastName)
        .HasMaxLength(50);
        builder.Property(p => p.Email)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasMany(p => p.Applications)
        .WithOne(p => p.Candidate)
        .HasForeignKey(p => p.CandidateId);

        builder.ToTable("Candidate");
    }
  }
}