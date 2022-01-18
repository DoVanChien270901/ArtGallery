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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Title).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasColumnType("text");
            builder.Property(c => c.ViewCount).HasDefaultValue(0);
            builder.Property(c => c.Price).HasColumnType("decimal(15,2)");
            builder.Property(c => c.Status).HasDefaultValue(false);
            builder.HasOne(c => c.Account).WithMany(c => c.Products)
                .HasForeignKey(c => c.AccountId);
        }
    }
}
