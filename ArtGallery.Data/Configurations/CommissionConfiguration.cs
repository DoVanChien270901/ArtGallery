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
    public class CommissionConfiguration : IEntityTypeConfiguration<Commission>
    {
        public void Configure(EntityTypeBuilder<Commission> builder)
        {
            builder.ToTable("Commissions");
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Amount).HasColumnType("decimal(15,2)");
            builder.HasOne(c => c.Order).WithOne(c => c.Commission).HasForeignKey<Commission>(c => c.OrderId);

        }
    }
}
