using JobPortal.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Persistence.EntityConfigurations
{
  public class JobConfiguration : IEntityTypeConfiguration<Job>
  {
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(p => p.JobId);

        builder.Property(p => p.Title)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasOne(p => p.Company)
        .WithMany(p => p.Jobs)
        .HasForeignKey(p => p.CompanyId);

        builder.ToTable("Job");
    }
  }
}