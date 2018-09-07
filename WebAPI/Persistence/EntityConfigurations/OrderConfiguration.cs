using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core.Domain;

namespace WebAPI.Persistence.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.OrderId);

            builder.Property(p => p.Description)
                .HasMaxLength(200);

            builder.Property(p => p.DateCreated)
                .HasDefaultValue(DateTimeOffset.Now)
                .IsRequired();

            builder.HasOne(p => p.Party)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.PartyId);

            builder.ToTable("Order");
        }
    }
}
