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
    public class AmountInAuctionConfiguration : IEntityTypeConfiguration<AmountAuction>
    {
        public void Configure(EntityTypeBuilder<AmountAuction> builder)
        {
            builder.ToTable("AmountInActions");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.NewPrice).HasColumnType("decimal(15,2)");
            builder.HasOne(c => c.Auction).WithMany(c => c.AmountAcctions).HasForeignKey(c => c.AuctionId);
        }
    }
}
