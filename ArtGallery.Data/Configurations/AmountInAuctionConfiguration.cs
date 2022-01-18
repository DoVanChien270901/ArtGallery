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
    public class AmountInAuctionConfiguration : IEntityTypeConfiguration<AmountInAuction>
    {
        public void Configure(EntityTypeBuilder<AmountInAuction> builder)
        {
            builder.ToTable("AmountInActions");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            //builder.Property(c => c.CurrentPrice).HasColumnType("decimal(15,2)");
            //builder.Property(c => c.PriceStep).HasColumnType("decimal(15,2)");
            builder.Property(c => c.NewPrice).HasColumnType("decimal(15,2)");
            builder.HasOne(c => c.Auction).WithMany(c => c.AmountInAcctions).HasForeignKey(c => c.AuctionId);
        }
    }
}
