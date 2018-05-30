using JobPortal.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Persistence.EntityConfigurations
{
  public class CompanyConfiguration : IEntityTypeConfiguration<Company>
  {
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(p => p.CompanyId);

        builder.Property(p => p.CompanyId)
        .IsRequired();
        // builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Name)
        .HasMaxLength(50);
        builder.Property(p => p.City)
        .HasMaxLength(30);
        builder.Property(p => p.Address)
        .HasMaxLength(50);

        builder.Property(p => p.UserId).IsRequired();

        // relashionships
        builder.HasMany(p => p.Jobs)
        .WithOne(p => p.Company);
        builder.HasOne(p => p.User)
        .WithMany(p => p.Companies)
        .HasForeignKey(p => p.UserId);

        builder.ToTable("Company");
    }
  }
}