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
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(c => new { c.ProductId, c.CategoryId });
            builder.HasOne(c => c.Product).WithMany(c => c.ProductInCategories)
                .HasForeignKey(c => c.ProductId);
            builder.HasOne(c => c.Category).WithMany(c => c.ProductInCategories)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
