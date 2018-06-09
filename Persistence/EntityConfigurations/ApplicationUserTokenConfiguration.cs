using System;
using JobPortal.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Persistence.EntityConfigurations
{
    public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder) {
            builder.Property(p => p.DateCreated)
            .HasDefaultValue(DateTimeOffset.Now)
            .IsRequired();
        }
    }
}