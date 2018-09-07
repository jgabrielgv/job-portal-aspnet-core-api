using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core.Domain;

namespace WebAPI.Persistence.EntityConfigurations
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(e => e.PartyId);

            builder.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(50);

            builder.Property(p => p.SecondLastName)
                .HasMaxLength(50);
       
            builder.Property(p => p.Type)
                .IsRequired();

            builder.HasMany(p => p.Orders)
                .WithOne(p => p.Party)
                .HasForeignKey(p => p.PartyId);

            builder.ToTable("Party");
        }
    }
}
