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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Status).HasMaxLength(20);
            builder.HasOne(c => c.Account).WithMany(c => c.Orders).HasForeignKey(c => c.AccountId);
            builder.HasOne(c => c.Product).WithMany(c => c.Orders).HasForeignKey(c => c.ProductId);
        }
    }
}
