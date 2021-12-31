using ArtGallery.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(c => c.Name);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.Password).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Roles).HasColumnType("varchar(6)");
        }
    }
}
