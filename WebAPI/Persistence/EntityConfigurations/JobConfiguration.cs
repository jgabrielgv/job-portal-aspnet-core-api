using WebAPI.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Persistence.EntityConfigurations
{
  public class JobConfiguration : IEntityTypeConfiguration<Job>
  {
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(p => p.JobId);

        builder.Property(p => p.Title)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasOne(p => p.Company)
        .WithMany(p => p.Jobs)
        .HasForeignKey(p => p.CompanyId);

        builder.HasMany(p => p.Applications)
        .WithOne(p => p.Job)
        .HasForeignKey(p => p.JobId);

        builder.ToTable("Job");
    }
  }
}