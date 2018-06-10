using System;
using JobPortal.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Persistence.EntityConfigurations {
    public class RefreshUserTokenConfiguration : IEntityTypeConfiguration<RefreshUserToken> {
        public void Configure (EntityTypeBuilder<RefreshUserToken> builder) {
            builder.HasKey(p => p.RefreshUserTokenId);

            builder.Property(p => p.RefreshUserTokenId)
            .HasMaxLength(50);
            builder.Property(p => p.RefreshToken)
            .HasMaxLength(50)
            .IsRequired();
            builder.Property(p => p.UserId)
            .HasMaxLength(450)
            .IsRequired();

            builder.HasOne(p => p.User)
            .WithMany(p => p.RefreshTokens)
            .HasForeignKey(p => p.UserId);

            builder.ToTable("RefreshUserToken");
        }

    }
}