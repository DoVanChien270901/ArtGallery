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
    public class ProductInCartConfiguration : IEntityTypeConfiguration<ProductInCart>
    {
        public void Configure(EntityTypeBuilder<ProductInCart> builder)
        {
            builder.ToTable("ProductInCarts");
            builder.HasKey(c => new { c.ProductId, c.CartId });
            builder.HasOne(c => c.Product).WithMany(c => c.ProductInCarts)
                .HasForeignKey(c => c.ProductId);
            builder.HasOne(c => c.Cart).WithMany(c => c.ProductInCarts)
                .HasForeignKey(c => c.CartId);
        }
    }
}
